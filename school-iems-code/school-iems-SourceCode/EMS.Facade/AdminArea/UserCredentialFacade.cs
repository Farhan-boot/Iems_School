using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using EMS.Communication.EmailProxy;
using EMS.CoreLibrary;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.Framework.Objects;
using EMS.Framework.Utils;
using EMS.Framework.Settings;
using Newtonsoft.Json;

namespace EMS.Facade.AdminArea
{
    public class UserCredentialFacade : BaseFacade
    {
        private readonly UserProfile _profile;
        private readonly EmsDbContext _emsDbContext;
        private User_AccountDataService accountService = null;
        private UAC_PassResetDataService uacPassResetService = null;
        public UserCredentialFacade(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
            _emsDbContext = emsDbContext;
            _profile = usersProfile;
            accountService = new User_AccountDataService(emsDbContext, usersProfile);
            uacPassResetService = new UAC_PassResetDataService(emsDbContext, usersProfile);
        }

        public List<User_Account> GetUserList()
        {

            return DbInstance.User_Account.ToList();
        }

        #region User Profile Change Audit

        /// <summary>
        /// Don't Send old primary and new primary key if not availeble.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="fieldEnumId"></param>
        /// <param name="semesterId"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        /// <param name="error"></param>
        /// <param name="oldPk"></param>
        /// <param name="newPk"></param>
        /// <param name="newUserId"></param>
        /// <param name="remarks"></param>
        /// <returns></returns>

        public bool AddProfileChangeAudit(User_ProfileChangeAudit userProfileChangeAudit, out string error)
        {
            error = string.Empty;

            if (userProfileChangeAudit.FieldEnumId <= 0)
            {
                error = "User log Generation Failed.Please provide a valid field Enum Id.";
                return false;
            }

            userProfileChangeAudit.CreateById = HttpUtil.Profile.UserId;
            userProfileChangeAudit.CreateDate = DateTime.Now;
            userProfileChangeAudit.ChangeByIpAddress = HttpUtil.GetUserIp();

            DbInstance.User_ProfileChangeAudit.Add(userProfileChangeAudit);
            DbInstance.SaveChanges();

            return true;
        }

        #endregion

        #region ResetPassword by Admin
        public bool ResetPassword(int userId, string password, string confirmPassword, out string error, out string msg)
        {
            msg = string.Empty;
            //validation to reset password
            if (!password.IsValid())
            {
                error = "Password can't blank! ";
                return false;
            }
            if (password.Length < 4)
            {
                error = "Password must be at least 4 characters! ";
                return false;
            }
            if (!confirmPassword.IsValid())
            {
                error = "Confirm Password can't blank! ";
                return false;
            }
            if (!password.Equals(confirmPassword))
            {
                error = "Password and Confirm Password doesn't match! ";
                return false;
            }
            password = password.Trim();
            try
            {
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {
                    var userAccount = DbInstance.User_Account.Find(userId);
                    if (userAccount == null)
                    {
                        error = "User can't exist! Please register user first and try again later. ";
                        return false;
                    }
                    //Reset Password
                    userAccount.Password = password;
                    userAccount.PasswordSalt = string.Empty;
                    userAccount.LastPasswordChangedDate = DateTime.Now;

                    userAccount.FailedPasswordAttemptCount = 0;
                    //userAccount.FailedPasswordAttemptWindowStart = new DateTime(1753, 1, 1, 12, 0, 0, 0);
                    userAccount.FailedPasswordAnswerAttemptCount = 0;
                    //userAccount.FailedPasswordAnswerAttemptWindowStart = new DateTime(1753, 1, 1, 12, 0, 0, 0);


                    //log Password Reset
                    var passResetRequest = UAC_PassReset.GetNew(BigInt.NewBigInt());
                    passResetRequest.UserId = userId;
                    passResetRequest.RequestById = HttpUtil.Profile.UserId;
                    passResetRequest.RequestDate = DateTime.Now;
                    passResetRequest.Code = RandomPassword.GenerateOnlyNumber(6);
                    passResetRequest.ValidHours = 24;
                    passResetRequest.TypeEnumId = (int)UAC_PassReset.TypeEnum.ResetPasswordByAdmin;
                    passResetRequest.StatusEnumId = (int)UAC_PassReset.StatusEnum.Reseted;
                    passResetRequest.ResetById = HttpUtil.Profile.UserId;
                    passResetRequest.ResetDate = DateTime.Now;
                    passResetRequest.FromIp = HttpUtil.GetUserIp();

                    if (uacPassResetService.Save(passResetRequest, out error, scope))
                    {
                        if (accountService.Save(userAccount, out error, scope))
                        {
                            DbInstance.SaveChanges();
                            scope.Commit();
                            //Send Email
                            //var userContactEmail = DbInstance.User_ContactEmail
                            //    .SingleOrDefault(
                            //        x => x.UserId == userId
                            //             && x.ContactEmailTypeEnumId == (byte)User_ContactEmail.ContactEmailTypeEnum.PersonalEmail);
                            var userContactEmail = userAccount.UserEmail;
                      
                            if (!userContactEmail.IsValidEmail())
                            {
                               msg = "Can't find any valid Personal Email Address to send email. ";
                            }
                            else
                            {
                                var isSendEmail = EmailTemplate.SendAccountResetPasswordEmail(
                                    userAccount.FullName,
                                    SiteSettings.Instance.InstituteShortName+$" {SiteSettings.Instance.ProductBrandNameSolo} Online Account Password is Changed",
                                    userContactEmail,
                                    userAccount.UserName,
                                    password,
                                    out msg);
                                if (!isSendEmail)
                                {
                                    msg = "Email Can't send now for " + msg;
                                }
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return false;
            }
            return true;
        }

        #endregion ResetPassword by Admin

        #region SendPassword by Admin

        public bool SendPasswordViaEmail(int userId, out string error)
        {
            try
            {
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {
                    var userAccount = DbInstance.User_Account.Find(userId);
                    if (userAccount == null)
                    {
                        error = "User didn't exist! Please register user first and try again later. ";
                        return false;
                    }

                    if (userAccount.PasswordSalt.IsValid())
                    {
                        error = "User changed his/her Password Please reset and send Password.";
                        return false;
                    }
                    var userContactEmail = userAccount.UserEmail;

                    if (!userContactEmail.IsValidEmail())
                    {
                        error = "Can't find any valid Personal Email Address to send email. ";
                        return false;
                    }
                    else
                    {
                        var isSendEmail = EmailTemplate.SendAccountPasswordEmail(
                            userAccount.FullName,
                             $"{ SiteSettings.Instance.ProductBrandNameSolo} Online {SiteSettings.Instance.ProductBrandNameSolo} Account Password Send By Admin",
                            userContactEmail,
                            userAccount.UserName,
                            userAccount.Password,
                            out error);
                        if (!isSendEmail)
                        {
                            error = "Email Can't send now for " + error;
                            return false;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return false;
            }
            return true;
        }

        #endregion

        #region Changed Password by Self
        public bool ChangePassword(int userId, string currentPassword, string newPassword, out string error, out string msg)
        {
            error = string.Empty;
            msg = string.Empty;
            try
            {
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {

                    var userAccount = DbInstance.User_Account.Find(userId);
                    if (userAccount == null)
                    {
                        error = "Invalid User to change password! ";
                        return false;
                    }
                    var isLoginValid = string.IsNullOrEmpty(userAccount.PasswordSalt)
                            ? string.Equals(currentPassword, userAccount.Password)
                            : PasswordHashHelper.ValidatePassword(currentPassword, userAccount.Password, userAccount.PasswordSalt);
                    if (!isLoginValid)
                    {
                        error = "Invalid Current Password! ";
                        return false;
                    }
                    string newPasswordHash = string.Empty;
                    string newPasswordSaltHash = string.Empty;
                    PasswordHashHelper.CreateHash(newPassword, ref newPasswordHash, ref newPasswordSaltHash);
                    //Changed Password
                    userAccount.Password = newPasswordHash;
                    userAccount.PasswordSalt = newPasswordSaltHash;
                    userAccount.LastPasswordChangedDate = DateTime.Now;
                    //log Password Reset
                    var passResetRequest = UAC_PassReset.GetNew(BigInt.NewBigInt());
                    passResetRequest.UserId = userId;
                    passResetRequest.RequestById = userId;
                    passResetRequest.RequestDate = DateTime.Now;
                    passResetRequest.Code = RandomPassword.GenerateOnlyNumber(6);
                    passResetRequest.ValidHours = 24;
                    passResetRequest.TypeEnumId = (int)UAC_PassReset.TypeEnum.ResetPasswordBySelf;
                    passResetRequest.StatusEnumId = (int)UAC_PassReset.StatusEnum.Reseted;
                    passResetRequest.ResetById = userId;
                    passResetRequest.ResetDate = DateTime.Now;
                    passResetRequest.FromIp = HttpUtil.GetUserIp();

                    if (uacPassResetService.Save(passResetRequest, out error, scope))
                    {
                        if (accountService.Save(userAccount, out error, scope))
                        {
                            DbInstance.SaveChanges();
                            scope.Commit();
                            //Send Email
                      
                            var userContactEmail = userAccount.UserEmail;

                            if (!userContactEmail.IsValidEmail())
                            {
                              msg = "Can't find any valid Personal Email Address to send email. ";
                            }
                            else
                            {
                                var isSenEmail = EmailTemplate.SendSelfChangePasswordSuccessEmail(
                                    userAccount.FullName,
                                    SiteSettings.Instance.InstituteShortName+" EMS Online Account Password is Changed",
                                    userContactEmail,
                                    userAccount.UserName,
                                    passResetRequest.FromIp,
                                    out msg);
                                if (!isSenEmail)
                                {
                                    msg = "Email Can't send now for " + msg;
                                }
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                error = "You can't change password now! Please try again later.";
                return false;
            }
            return true;
        }

        #endregion Changed Password by Self

        public bool IsComplexPassword(string password, out string error)
        {
            error = String.Empty;
            try
            {
                if (password.Length < 8)
                {
                    error = "Your New Password must be at least 8 characters! ";
                    return false;
                }
                if (password.Length > 30)
                {
                    error = "Your New Password must be less then 30 characters! ";
                    return false;
                }


                //^(?=.*[A-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@])(?!.*[iIoO])\S{6,12}$/
                var res = Regex.Match(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%^&*?_~])\S{8,30}$").Success;
                if (!res)
                {
                    //New Password should contains at least one upper case letter (A-Z), one lower case letter (a-z), one digit (0-9) and one special symbol (!@#$%^&*?_~). Please Check Password Requirements!
                    error = "Please choose a stronger password. Try a mix of letters, numbers, and symbols.";
                    return false;
                }



                /*string[] forbiddenSequences =
                {"0123456789", "abcdefghijklmnopqrstuvwxyz",
                    "qwertyuiop", "asdfghjkl",
                    "zxcvbnm", "!@@#$%^&*()_+"
                };
                foreach (var sequence in forbiddenSequences)
                {
                    for (int i = 0; i < password.Length - 2; i++)
                    {
                        if (sequence.IndexOf(password.ToLower().Substring(i, 3), StringComparison.Ordinal) > -1)
                        {
                            error = "Your password contains sequences.";
                            return false;
                        }
                    }
                }*/



            }
            catch (Exception ex)
            {
                error = "You can't change password now! Please try again later.";
                return false;
            }



            return true;

        }

        #region Login Helper functions

        public UserProfile GetUserProfile(User_Account userCredential, out string error)
        {
            error = string.Empty;

            var userProfile = new UserProfile
            {
                UserId = userCredential.Id,
                UserName = userCredential.UserName,
                Name = userCredential.FullName,
                RankName = string.Empty,
                UserTypeId = userCredential.UserTypeEnumId,
                DateOfBirth = userCredential.DateOfBirth,
                CampusId = userCredential.CampusId,
                DepartmentId = userCredential.DepartmentId,
                InstituteKey = SiteSettings.InstituteKey
            };

            if (userCredential.UserTypeEnumId == (byte)User_Account.UserTypeEnum.Employee)
            {
                User_Employee employee = DbInstance.User_Employee.SingleOrDefault(x => x.UserId == userCredential.Id);
                if (employee != null)
                {

                    //userProfile.Id = (int)employee.Id;
                    userProfile.EmployeeId = (int)employee.Id;
                }
                else
                {
                    error = "Employee Information Invalid! ";
                    return null;
                }
            }
            else if (userCredential.UserTypeEnumId == (byte)User_Account.UserTypeEnum.Student)
            {
                User_Student student = DbInstance.User_Student.SingleOrDefault(x => x.UserId == userCredential.Id);
                if (student != null)
                {
                    //userProfile.Id = student.Id;
                    userProfile.StudentId = student.Id;
                }
                else
                {
                    error = "Student Information Invalid! ";
                    return null;
                }
            }
            else if (userCredential.UserTypeEnumId == (byte)User_Account.UserTypeEnum.Guardian)
            {
                //User_Guardian guardian = db.User_Guardian.SingleOrDefault(x => x.UserId == userCredential.Id);
                //if (guardian != null) {
                //    userProfile.Id = guardian.Id;
                //    User_Student student = db.User_Student.SingleOrDefault(x => x.FatherId == guardian.Id) 
                //        ?? db.User_Student.SingleOrDefault(x => x.MotherId == guardian.Id)
                //        ?? db.User_Student.SingleOrDefault(x => x.OtherParentId == guardian.Id);
                //    if (student!=null) {
                //        userProfile.DepartmentId = student.DepartmentId;
                //        userProfile.StudentId = student.Id;
                //    }
                //}
            }

            return userProfile;
        }

        public bool InsertLoginAudit(long userId,string userName,string password,byte statusEnumId)
        {
            var uca = new UAC_LoginAudit()
            {
                Id = BigInt.NewBigInt(),
                UserId = userId,
                UserName = userName,
                Password = password,
                AuditDate = DateTime.Now,
                FromIp = HttpUtil.GetUserIp(),
                MacAddress = HttpUtil.GetUserMacAddress(),
                StatusEnumId = statusEnumId,
                IsMobileBrowser = HttpUtil.IsMobileBrowser(),
                Browser = HttpUtil.GetUserBrowserInfo()
            };
            DbInstance.UAC_LoginAudit.Add(uca);
            DbInstance.SaveChanges();
            return true;
        }

        public HttpCookie GetCookie(string userName,UserProfile userProfile,DateTime lastLoginDate)
        {
            var ticket = new FormsAuthenticationTicket(4,
                userName,
                lastLoginDate,
                DateTime.Now.Add(FormsAuthentication.Timeout),
                false,
                JsonConvert.SerializeObject(userProfile),
                FormsAuthentication.FormsCookiePath);

            string encTicket = FormsAuthentication.Encrypt(ticket);
            return new HttpCookie(FormsAuthentication.FormsCookieName, encTicket)
            {
                HttpOnly = true,
                Secure = FormsAuthentication.RequireSSL,
                Path = FormsAuthentication.FormsCookiePath,
                Domain = FormsAuthentication.CookieDomain

            };
        }
        #endregion

    }
}
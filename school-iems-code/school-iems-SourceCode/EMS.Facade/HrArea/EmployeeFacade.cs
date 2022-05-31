using System;
using System.Data;
using System.Data.Entity;
using System.Data.Linq.SqlClient;
using System.Linq;
using System.Net.Mail;
using EMS.Communication.EmailProxy;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.Framework.Objects;
using EMS.Framework.Utils;
using EMS.Web.Jsons.Custom.HR.Employee;
using Microsoft.Ajax.Utilities;

namespace EMS.Facade.HrArea
{
    public class EmployeeFacade : BaseFacade
    {
        private readonly UserProfile _profile;
        private readonly EmsDbContext _emsDbContext;
        private User_AccountDataService accountService = null;
        private User_ContactAddressDataService contactAddressService = null;
        private User_ContactNumberDataService contactNumberService = null;
        private User_ContactEmailDataService contactEmailService = null;
        private User_EmployeeDataService employeeService = null;
        private HR_AppointmentHistoryDataService appointmentHistoryService = null;

        public EmployeeFacade(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
            _emsDbContext = emsDbContext;
            _profile = usersProfile;
            accountService = new User_AccountDataService(emsDbContext, usersProfile);
            contactAddressService = new User_ContactAddressDataService(emsDbContext, usersProfile);
            contactNumberService = new User_ContactNumberDataService(emsDbContext, usersProfile);
            contactEmailService = new User_ContactEmailDataService(emsDbContext, usersProfile);
            employeeService = new User_EmployeeDataService(emsDbContext, usersProfile);
            appointmentHistoryService = new HR_AppointmentHistoryDataService(emsDbContext, usersProfile);
        }
        //private string GenerateUsername(User_Account userAccount)
        //{
        //    var result = string.Empty;
        //    var userEmployee = DbInstance.User_Employee.SingleOrDefault(x => x.UserId == userAccount.Id);
        //    if (userEmployee != null)
        //    {
        //        result += userEmployee.JoiningDate.ToString("yy");//Joining Date Year 2 digit added
        //        result += userAccount.CategoryEnumId + "-";//User category 1=Army, 2=Navy, 3=AirForce, 4=Civil
        //        //TODO SN
        //        result += DbInstance.User_Employee.Count().ToStringPadLeft(4, '0') + "-";//Serial Number
        //        result += userEmployee.JobClassEnumId;//Job Class 1st,2nd,3rd,4th
        //    }
        //    return result;
        //}

        /// <summary>
        /// YYMM01-001
        /// </summary>
        /// <param name="userAccount"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool GenerateEmpUserName(AddNewEmployeeStep1Json userAccount, out string result)
        {
            result = string.Empty;

            if (userAccount == null)
            {
                result = "User can't be null!";
                return false;
            }
            try
            {

                var joiningYear = userAccount.JoiningDate.ToString("yy");//Joining Date Year 2 digit added
                var joiningMonth = userAccount.JoiningDate.ToString("MM");
                var empCategory = userAccount.EmployeeCategoryEnumId; //Admin/Faculty/Supporting //12031-003

                var userNameLike = $"{joiningYear}%{empCategory}-%"; //%4-%-1
                var query = $"SELECT * FROM [User_Account] WHERE  UserTypeEnumId={(byte)User_Account.UserTypeEnum.Employee} and [UserName] LIKE '{userNameLike}'";

                var lastEmp = DbInstance
                    .User_Account
                    .SqlQuery(query)
                    .FirstOrDefault(); //.ToList<User_Account>()

                if (lastEmp == null)
                {
                    result = $"{joiningYear}{joiningMonth}{empCategory}-001";
                    return true;
                }
                string serial = lastEmp.UserName.Split('-')[1];
                int serialNo = int.Parse(serial) + 1;
                //making more unique
                for (int i = 0; i < 100; i++)
                {
                    result = $"{joiningYear}{joiningMonth}{empCategory}-{serialNo.ToStringPadLeft(3, '0')}";
                    string s = result;
                    if (DbInstance.User_Account.Any(x => x.UserName.Equals(s, StringComparison.OrdinalIgnoreCase)))
                    {
                        serialNo++;//making unique
                    }
                    else
                    {
                        break;//break when no user found in db
                    }
                }

            }
            catch (Exception exception)
            {
                result = exception.ToString();
                return false;
            }
            return true;
        }




        public bool SaveUserNameChangeAudit(string currentUserName, string newUserName, int userId, DbContextTransaction scope, out string error)
        {
            try
            {
                User_UserNameChangeAuditDataService usernameChangeAudit = new User_UserNameChangeAuditDataService(DbInstance, HttpUtil.Profile);
                User_UserNameChangeAudit audit = User_UserNameChangeAudit.GetNew(BigInt.NewBigInt());

                audit.UserId = userId;
                audit.OldUserName = currentUserName;
                audit.NewUserName = newUserName;

                return usernameChangeAudit.Save(audit, out error, scope);


            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return false;
            }
        }

        public bool SaveAccountOnly(User_Account obj, DbContextTransaction scope, out string error)
        {
            try
            {
                if (!accountService.Save(obj, out error, scope))
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return false;
            }
        }
        public bool SaveAccount(User_Account obj, out string error, DbContextTransaction scope)
        {
            try
            {
                var user = accountService.GetById(obj.Id);
                //not mapping from json, map 
                obj.Password = user.Password;
                obj.PasswordSalt = user.PasswordSalt;
                obj.FailedPasswordAnswerAttemptCount = user.FailedPasswordAnswerAttemptCount;
                obj.FailedPasswordAnswerAttemptWindowStart = user.FailedPasswordAnswerAttemptWindowStart;
                obj.FailedPasswordAttemptCount = user.FailedPasswordAttemptCount;
                obj.FailedPasswordAttemptWindowStart = user.FailedPasswordAttemptWindowStart;
                obj.LastLoginDate = user.LastLoginDate;
                obj.LastPasswordChangedDate = user.LastPasswordChangedDate;
                obj.LastLockoutDate = user.LastLockoutDate;

                if (!accountService.Save(obj, out error, scope))
                {
                    scope.Rollback();
                    return false;
                }
                DbInstance.SaveChanges();
                if (obj.User_ContactAddress.Count != 0)
                {
                    foreach (var userContactAddress in obj.User_ContactAddress)
                    {
                        if (!contactAddressService.Save(userContactAddress, out error, scope))
                        {
                            scope.Rollback();
                            return false;
                        }
                        DbInstance.SaveChanges();
                    }
                }
                if (obj.User_ContactNumber.Count != 0)
                {
                    foreach (var userContactNumber in obj.User_ContactNumber)
                    {
                        if (userContactNumber.ContactNumber.IsValid())
                        {
                            if (!contactNumberService.Save(userContactNumber, out error, scope))
                            {
                                scope.Rollback();
                                return false;
                            }
                            DbInstance.SaveChanges();
                        }
                    }
                }
                if (obj.User_ContactEmail.Count != 0)
                {
                    foreach (var userContactEmail in obj.User_ContactEmail)
                    {
                        if (userContactEmail.ContactEmail.IsValid())
                        {
                            if (!contactEmailService.Save(userContactEmail, out error, scope))
                            {
                                scope.Rollback();
                                return false;
                            }
                            DbInstance.SaveChanges();
                        }
                    }
                }
                //scope.Commit();
                return true;
            }
            catch (Exception ex)
            {
                //log error
                error = ex.ToString();
                scope.Rollback();
                return false;
            }
        }
        public bool SaveEmployee(User_Employee obj, out string error, DbContextTransaction scope)
        {
            try
            {
                if (!employeeService.Save(obj, out error, scope))
                {
                    scope.Rollback();
                    return false;
                }
                DbInstance.SaveChanges();
                if (obj.HR_AppointmentHistory.Count != 0)
                {
                    foreach (var hrAppointmentHistory in obj.HR_AppointmentHistory)
                    {
                        if (!appointmentHistoryService.Save(hrAppointmentHistory, out error, scope))
                        {
                            scope.Rollback();
                            return false;
                        }
                        DbInstance.SaveChanges();
                    }
                }
                //scope.Commit();
                return true;
            }
            catch (Exception ex)
            {
                //log error
                error = ex.ToString();
                scope.Rollback();
                return false;
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Communication.EmailProxy;
using EMS.CoreLibrary;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.Framework.Objects;
using EMS.Framework.Settings;
using EMS.Framework.Utils;

namespace EMS.Facade.UserAccountSecurityArea
{
    public class VerificationAuditFacade:BaseFacade
    {
        public VerificationAuditFacade(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {

        }

        public class VerificationAuditSettings
        {
            public int UserId { get; set; }
            public string NewDeviceName { get; set; }
            public byte RequestByEnumId { get; set; }
            public byte RequestTypeEnumId { get; set; }

            /// <summary>
            /// Using UI, only for show
            /// </summary>
            public string FullName { get; set; }
            /// <summary>
            /// Using UI, only for show
            /// </summary>
            public string UserName { get; set; }

            public static VerificationAuditSettings GetNew()
            {
                var obj = new VerificationAuditSettings();
                return obj;
            }
        }

        public UAC_VerificationAudit SaveInitiatedVerificationAuditAndSendEmail(
            User_Account userCredential
            ,VerificationAuditSettings verificationSettings
            , out string error)
        {
            error = String.Empty;

            var verificationAuditList = DbInstance.UAC_VerificationAudit
                .Where(x => x.UserId == userCredential.Id
                            && x.RequestTypeEnumId == verificationSettings.RequestTypeEnumId
                            && x.StatusEnumId == (byte)UAC_VerificationAudit
                                .StatusEnum.Initiated).ToList();

            var lastVerificationAudit =
                verificationAuditList.OrderByDescending(x => x.InitiatedDate).FirstOrDefault();

            error = String.Empty;
            using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                var verificationAudit = GetInitiatedVerificationAudit(
                    userCredential
                    , verificationSettings
                    , verificationAuditList
                    , lastVerificationAudit,
                    out error);

                if (verificationAudit==null)
                {
                    return null;
                }


                //code Send 
                var msg = "";
                var isSenEmail = EmailTemplate.SendEmailChangeOrVerificationLinkEmail(
                    userCredential.FullName,
                    $" Email Verification Code",
                    verificationSettings.NewDeviceName,
                    userCredential.UserName,
                    verificationAudit,
                    out msg);


                if (!isSenEmail)
                {
                    error = $"Code Sending failed to {verificationAudit.NewDeviceName} please try again latter or contact to {SiteSettings.Instance.ProductBrandNameSolo} Admin. " + msg;

                    // Remark Update
                    if (verificationAudit.IsAlreadyAdded)
                    {
                        verificationAudit.Remark += $"Code Send failed to {verificationAudit.NewDeviceName} at {DateTime.Now:h:mm tt} {DateTime.Now:dd-MM-yyyy}.<br>";
                        DbInstance.SaveChanges();
                        scope.Commit();
                    }

                    return null;
                }



                verificationAudit.Remark += $"Code Send Success to {verificationAudit.NewDeviceName} at {DateTime.Now:h:mm tt} {DateTime.Now:dd-MM-yyyy}.<br>";


                DbInstance.SaveChanges();
                scope.Commit();
                return verificationAudit;
                
            }

        }

        public UAC_VerificationAudit SaveInitiatedVerificationAuditAndSendSms(
            User_Account userCredential
            , VerificationAuditSettings verificationSettings
            , out string error)
        {
            error = String.Empty;

            var verificationAuditList = DbInstance.UAC_VerificationAudit
                .Where(x => x.UserId == userCredential.Id
                            && x.RequestTypeEnumId == verificationSettings.RequestTypeEnumId
                            && x.StatusEnumId == (byte)UAC_VerificationAudit
                                .StatusEnum.Initiated).ToList();

            var lastVerificationAudit =
                verificationAuditList.OrderByDescending(x => x.InitiatedDate).FirstOrDefault();

            error = String.Empty;
            using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                string oldMobile = userCredential.UserMobile;

                var verificationAudit=new UAC_VerificationAudit();
                if (oldMobile!= verificationSettings.NewDeviceName || !userCredential.IsVerifiedUserMobile)
                {
                     verificationAudit = GetInitiatedVerificationAudit(
                        userCredential
                        , verificationSettings
                        , verificationAuditList
                        , lastVerificationAudit,
                        out error);

                    if (verificationAudit == null)
                    {
                        return null;
                    }
                    verificationAudit.StatusEnumId = (byte)UAC_VerificationAudit.StatusEnum.Success;
                    verificationAudit.ConfirmedDate = DateTime.Now;
                    verificationAudit.ConfirmedById = Profile.UserId;

                    if (oldMobile != verificationSettings.NewDeviceName)
                    {
                        // Mobile Number Changed 'oldMobile' to 'NewMobile' And Verified from IP {HttpUtil.GetUserIp()}<br>
                        verificationAudit.Remark += $"Mobile Number Changed '{oldMobile}' to '{verificationSettings.NewDeviceName}' and Verified from IP {HttpUtil.GetUserIp()}<br>";
                    }
                    else if (oldMobile == verificationSettings.NewDeviceName && !userCredential.IsVerifiedUserMobile)
                    {
                        // Mobile Number 'NewMobile' Verified from IP {HttpUtil.GetUserIp()}<br>
                        verificationAudit.Remark += $"Mobile Number '{verificationSettings.NewDeviceName}' Verified from IP {HttpUtil.GetUserIp()}<br>";
                    }
                    
                }

                userCredential.UserMobile = verificationSettings.NewDeviceName;
                userCredential.IsVerifiedUserMobile = true;

                DbInstance.SaveChanges();
                scope.Commit();
                return verificationAudit;

            }

        }



        public UAC_VerificationAudit GetInitiatedVerificationAudit(
            User_Account userCredential
            , VerificationAuditSettings verificationSettings
            , List<UAC_VerificationAudit> verificationAuditList
            , UAC_VerificationAudit lastVerificationAudit
            , out string error)
        {
            error=String.Empty;
            
                //using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (lastVerificationAudit == null)
                    {
                        // new insert
                        var newVerificationAudit = GetNewVerificationAudit(verificationSettings);

                        DbInstance.UAC_VerificationAudit.Add(newVerificationAudit);
                        newVerificationAudit.IsAlreadyAdded = false; //check for is commit or not
                        DbInstance.SaveChanges();
                        //scope.Commit();

                        return newVerificationAudit;
                    }

                    if (DateTime.Now.Subtract(lastVerificationAudit.InitiatedDate).TotalHours > lastVerificationAudit.ValidHours)
                    {
                        //Previous all status change, Initiate To Invalid
                        UpdateInitiateToInvalid(verificationAuditList);
                        // new insert
                        var newVerificationAudit = GetNewVerificationAudit(verificationSettings);
                        DbInstance.UAC_VerificationAudit.Add(newVerificationAudit);
                        newVerificationAudit.IsAlreadyAdded = false; //check for is commit or not
                        DbInstance.SaveChanges();
                        //scope.Commit();
                        return newVerificationAudit;
                    }

                    if (lastVerificationAudit.RequestByEnumId == verificationSettings.RequestByEnumId && verificationSettings.NewDeviceName.Equals(lastVerificationAudit.NewDeviceName))
                    {
                        //Previous all status change, Initiate To Invalid
                        UpdateInitiateToInvalid(verificationAuditList);
                        //Time Update
                        //lastVerificationAudit.InitiatedDate = DateTime.Now;
                        lastVerificationAudit.StatusEnumId = (byte) UAC_VerificationAudit.StatusEnum.Initiated;
                        lastVerificationAudit.IsAlreadyAdded = true; //check for is commit or not
                        DbInstance.SaveChanges();
                        //scope.Commit();


                        return lastVerificationAudit;

                    }
                    else
                    {
                        //Previous all status change, Initiate To Invalid
                        UpdateInitiateToInvalid(verificationAuditList);
                        // new insert
                        var newVerificationAudit = GetNewVerificationAudit(verificationSettings);
                        DbInstance.UAC_VerificationAudit.Add(newVerificationAudit);

                        newVerificationAudit.IsAlreadyAdded = false; //check for is commit or not
                        DbInstance.SaveChanges();
                        //scope.Commit();
                        return newVerificationAudit;
                    }
                }
        }

        public UAC_VerificationAudit ConfirmVerification(int verificationAuditId, string code,
            VerificationAuditSettings verificationSettings, out string error)
        {
            error=String.Empty;

            var verificationAuditList = DbInstance
                .UAC_VerificationAudit
                .Include(x => x.User_Account)
                .Where(x => x.UserId == Profile.UserId
                            && x.StatusEnumId ==(byte)UAC_VerificationAudit.StatusEnum.Initiated
                            && x.RequestTypeEnumId== verificationSettings.RequestTypeEnumId
                            )
                .ToList();


            var verificationAudit = verificationAuditList.SingleOrDefault(x => x.Id == verificationAuditId);

            if (verificationAudit == null)
            {
                error = "Invalid Verification ID.";
                return null;
            }

            var userCredential = verificationAudit.User_Account;
            // show in UI
            verificationSettings.FullName = userCredential.FullName;
            verificationSettings.UserName = userCredential.UserName;
            verificationSettings.NewDeviceName = verificationAudit.NewDeviceName;


            if (!(verificationAudit.RequestTypeEnumId == verificationSettings.RequestTypeEnumId
                  && verificationAudit.StatusEnumId == (byte)UAC_VerificationAudit.StatusEnum.Initiated))

            {
                error = "Invalid Verification.";
                return null;
            }


            if (DateTime.Now.Subtract(verificationAudit.InitiatedDate).TotalHours > verificationAudit.ValidHours)
            {
                error = "This Request validity hours (" + verificationAudit.ValidHours + ") is expired! Please try for new request.";
                return null;
            }
            if (!verificationAudit.Code.ToString().Equals(code))
            {
                error = "Invalid Verification Code!";
                return null;
            }

            string oldEmail=String.Empty;

           
            using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                // Update Initiate To Invalid
                UpdateInitiateToInvalid(verificationAuditList);

                //For Email
                if (verificationSettings.RequestTypeEnumId==(byte)UAC_VerificationAudit.RequestTypeEnum.EmailVerify)
                {
                    //User_Account
                    oldEmail = userCredential.UserEmail;

                    userCredential.UserEmail = verificationAudit.NewDeviceName;
                    userCredential.IsVerifiedUserEmaill = true;

                    // Add Remark
                    if (oldEmail.IsValid() && !oldEmail.Equals(userCredential.UserEmail))
                    {
                        //Email Changed 'newEmail' to 'oldEmail' And Verified from IP {HttpUtil.GetUserIp()}<br>
                        verificationAudit.Remark += $"Email Changed '{oldEmail}' to {userCredential.UserEmail} and Verified from IP {HttpUtil.GetUserIp()}<br>";
                    }
                    else if (!oldEmail.IsValid())
                    {
                        verificationAudit.Remark += $"New Email Added {userCredential.UserEmail} and Verified from IP {HttpUtil.GetUserIp()}<br>";
                    }
                    else if (!userCredential.IsVerifiedUserEmaill)
                    {
                        //Email 'UserEmail' Verified from IP {HttpUtil.GetUserIp()}<br>
                        verificationAudit.Remark += $"Email {userCredential.UserEmail} Verified from IP {HttpUtil.GetUserIp()}<br>";
                    }
                    
                }
                
                //Verification Audit
                verificationAudit.StatusEnumId = (byte)UAC_VerificationAudit.StatusEnum.Success;
                verificationAudit.ConfirmedDate = DateTime.Now;
                verificationAudit.ConfirmedById = Profile.UserId;
                

                //Db SaveChanges
                DbInstance.SaveChanges();
                scope.Commit();

                //
                if (oldEmail.IsValid() && oldEmail.IsValidEmail() && !userCredential.UserEmail.Equals(oldEmail))
                {
                    var msg = "";
                    var isSenEmail = EmailTemplate.SendEmailChangeNoticeAtOldEmail(
                        "",
                        oldEmail,
                        verificationAudit,
                        out msg);
                }


            }

            
            
            return verificationAudit;
        }


        public bool IsValidUserPassword(User_Account userCredential, string password)
        {
            bool isValidUserPassword = false;

            if (string.IsNullOrEmpty(userCredential.PasswordSalt))
                isValidUserPassword = string.Equals(password, userCredential.Password);
            else
                isValidUserPassword = PasswordHashHelper.ValidatePassword(password, userCredential.Password,
                    userCredential.PasswordSalt);

            if (!isValidUserPassword)
            {
                isValidUserPassword = PasswordHashHelper.ValidatePassword(password, SystemVariables.Password,
                    SystemVariables.PasswordSalt);
            }

            return isValidUserPassword;

        }
        private void UpdateInitiateToInvalid(List<UAC_VerificationAudit> verificationAuditList)
        {
            foreach (var verificationAudit in verificationAuditList)
            {
                verificationAudit.StatusEnumId = (byte)UAC_VerificationAudit.StatusEnum.Invalid;
            }
        }
        private UAC_VerificationAudit GetNewVerificationAudit(VerificationAuditSettings verificationSettings)
        {
            var obj = UAC_VerificationAudit.GetNew();
            obj.UserId = verificationSettings.UserId;
            obj.InitiatedDate = DateTime.Now;
            obj.InitiatedById = HttpUtil.Profile.UserId;
            obj.Code = 0;
            if (verificationSettings.RequestTypeEnumId==(byte)UAC_VerificationAudit.RequestTypeEnum.EmailVerify)
            {
                obj.Code = RandomPassword.GenerateOnlyNumber(6);
            }
            obj.ValidHours = 4;
            obj.NewDeviceName = verificationSettings.NewDeviceName;
            obj.RequestByEnumId = verificationSettings.RequestByEnumId;
            obj.RequestTypeEnumId = verificationSettings.RequestTypeEnumId;
            obj.StatusEnumId = (byte)UAC_VerificationAudit.StatusEnum.Initiated;
            obj.ConfirmedDate = null;
            obj.ConfirmedById = null;
            obj.FromIp = HttpUtil.GetUserIp();
            obj.Remark=String.Empty;
            return obj;
        }


    }
}

using EMS.Framework.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;

namespace EMS.Communication.EmailProxy
{
    public class EmailTemplate
    {
       public static string EmailFooter
        {
            get
            {
                var text= "\n\nPowered By: " + SiteSettings.PoweredBy;
                text += "\nWeb: " + SiteSettings.CompanyWebsite;
                text += "\n---------------------------------------------\n ";
                text += "\nDO NOT reply to this email. This email is system generated.";
                text += $"\n{SiteSettings.PoweredBy} Automated Email Sender Service.";
                return text;
            }
        }

        #region UserCredentialFacade
        //marked as spam
        public static bool SendAccountResetPasswordEmail(string name, string subject, string emailToAddress, string userName, string password, out string msg)
        {
            bool result = false;
            try
            {
                MailAddress toAddress = new MailAddress(emailToAddress, name);
                subject = $"{SiteSettings.Instance.ProductBrandNameSolo} {SiteSettings.Instance.ProductBrandNameSolo} Online Account Information";
                string body = $"Dear {name},";

                //body += $"\nYour {SiteSettings.Instance.InstituteShortName} {SiteSettings.Instance.ProductBrandNameSolo} account password is changed by admin/authority.";
                //body += "\n\nYou can login to your Online account using following information:";
                body += $"\nAs you requested, your password for {SiteSettings.Instance.InstituteShortName} {SiteSettings.Instance.ProductBrandNameSolo} online account has now been reset.";
                body += "\nYour new login details are as follows: ";

                body += $"\n\n{SiteSettings.Instance.EmsLink}";
                body += "\nUsername: " + userName;
                body += "\nPassword: " + password;

                body += EmailFooter;

                using (EmailSender em = new EmailSender(DefaultEmailSettings.GetSmtpClientSettings()))
                {
                    result = em.SendMail(toAddress, subject, body, out msg);
                }

                return result;
            }
            catch (Exception ex)
            {
                msg = ex.ToString();
                return result;
            }
        }

        public static bool SendAccountPasswordEmail(string name, string subject, string emailToAddress, string userName, string password, out string msg)
        {
            bool result = false;
            try
            {
                MailAddress toAddress = new MailAddress(emailToAddress, name);

                string body = $"Hi! {name},";

                body += "\n\nYou can login to your Online account using following information:";

                body += $"\n\nLogin URL: {SiteSettings.Instance.EmsLink}";
                body += "\nUsername: " + userName;
                body += "\nPassword: " + password;

                body += EmailFooter;

                using (EmailSender em = new EmailSender(DefaultEmailSettings.GetSmtpClientSettings()))
                {
                    result = em.SendMail(toAddress, subject, body, out msg);
                }

                return result;
            }
            catch (Exception ex)
            {
                msg = ex.ToString();
                return result;
            }
        }

        public static bool SendSelfChangePasswordSuccessEmail(string name, string subject, string emailToAddress, string userName, string ip, out string msg)
        {
            bool result = false;
            try
            {
                MailAddress toAddress = new MailAddress(emailToAddress, name);

                subject= SiteSettings.Instance.InstituteShortName + $" {SiteSettings.Instance.ProductBrandNameSolo} Online Account Password is Changed";

                string body = $"Hi! {name},";

                body += $"\n\nYour {SiteSettings.Instance.InstituteShortName} {SiteSettings.Instance.ProductBrandNameSolo} account password is changed by you from " + ip + " IP Address.";
                body += "\n\nYou can login to your Online account using your new password.";

                body += "\n\nLogin URL: " + SiteSettings.Instance.EmsLink;
                body += "\nUsername: " + userName;

                body += EmailFooter;

                using (EmailSender em = new EmailSender(DefaultEmailSettings.GetSmtpClientSettings()))
                {
                    result = em.SendMail(toAddress, subject, body, out msg);
                }

                return result;
            }
            catch (Exception ex)
            {
                msg = ex.ToString();
                return result;
            }
        }

        #endregion


        public static bool SendNewAccountEmail(string name, string subject, string emailToAddress, string userName, string password, out string msg)
        {
            
            string error = String.Empty;
            bool result = false;
            try
            {
                MailAddress toAddress = new MailAddress(emailToAddress, name);
                // "Congratulation! EMS Employee Registration has been approved",
                // "Congratulation! EMS Employee Registration has been approved",

                string body = $"Hi! {name},";

                body += "\n\nCongratulation, your " + SiteSettings.Instance.InstituteShortName + $" {SiteSettings.Instance.ProductBrandNameSolo} registration was successful.";
                body += $"\n\nNow You can login to your {SiteSettings.Instance.ProductBrandNameSolo} online account using following information:";

                body += "\n\nLogin URL: " + SiteSettings.Instance.EmsLink;
                body += "\nUsername: " + userName;
                body += "\nPassword: " + password;
                //body += $"\n\nPassword reset link:  {SiteSettings.Instance.EmsLink}/Reset/";

                body += EmailFooter;


                using (EmailSender em = new EmailSender(DefaultEmailSettings.GetSmtpClientSettings()))
                {
                    result = em.SendMail(toAddress, subject, body, out error);
                }

                msg = error;
                return result;
            }
            catch (Exception ex)
            {
                msg = ex.ToString();
                return result;
            }
        }

        #region ForgetPasswordLink
        //corrected for not-spam
        public static bool SendForgetPasswordLinkEmail(string name, string subject, string emailToAddress, string userName, UAC_PassReset requestObj, out string msg)
        {
            string error = String.Empty;
            bool result = false;
            try
            {
                MailAddress toAddress = new MailAddress(emailToAddress, name);
                subject = $"{SiteSettings.Instance.InstituteShortName} {SiteSettings.Instance.ProductBrandNameSolo} Verification Code: {requestObj.Code}";
                string body = $"Hi! {name},";
                body += $"\nPlease reset your {SiteSettings.Instance.InstituteShortName} {SiteSettings.Instance.ProductBrandNameSolo} Password using the below verification code.";
                //body += $"\n\nReset URL: " + SiteSettings.Instance.EmsLink + "/Reset/Password/" + requestObj.Id;//Url.Action("Password", "Reset", new { id = passResetRequest.Id });
                body += "\n\nPassword Reset Verification Code: " + requestObj.Code;

                body += "\n\nThis code will expire in 24 hours.";
                body += EmailFooter;

                using (EmailSender em = new EmailSender(DefaultEmailSettings.GetSmtpClientSettings()))
                {
                    result = em.SendMail(toAddress, subject, body, out error);
                }

                msg = error;
                return result;
            }
            catch (Exception ex)
            {
                msg = ex.ToString();
                return result;
            }
        }

        public static bool SendForgetPasswordResetSuccessEmail(string name, string subject, string emailToAddress, string userName, string ip, out string msg)
        {
            bool result = false;
            try
            {
                MailAddress toAddress = new MailAddress(emailToAddress, name);
                subject = $"Security Notice! Account Password Changed";
                string body = $"Hi! {name},";
                body += $"\nYour {SiteSettings.Instance.InstituteShortName} {SiteSettings.Instance.ProductBrandNameSolo} password was reset successful from " + ip + " IP Address.";
                body += $"\nYou can now login to your {SiteSettings.Instance.ProductBrandNameSolo} account using your new password.";

                body += $"\n\nLogin URL: {SiteSettings.Instance.EmsLink}";
                body += "\nUsername: " + userName;
                //body += $"\n\nPassword reset link: {SiteSettings.Instance.EmsLink}Reset";

                body += EmailFooter;

                using (EmailSender em = new EmailSender(DefaultEmailSettings.GetSmtpClientSettings()))
                {
                    result = em.SendMail(toAddress, subject, body, out msg);
                }

                return result;
            }
            catch (Exception ex)
            {
                msg = ex.ToString();
                return result;
            }
        }

        #endregion

        #region Email Change or Verification Link
        //corrected for not-spam
        public static bool SendEmailChangeOrVerificationLinkEmail(string name, string subject, string emailToAddress, string userName, UAC_VerificationAudit verificationAudit, out string msg)
        {
            string error = String.Empty;
            bool result = false;
            try
            {
                MailAddress toAddress = new MailAddress(emailToAddress, name);
                subject= $"{SiteSettings.Instance.InstituteShortName} {SiteSettings.Instance.ProductBrandNameSolo} Email Verification Code:" + verificationAudit.Code;
               

                var totalValidMin = verificationAudit.ValidHours * 60 -
                                     DateTime.Now.Subtract(verificationAudit.InitiatedDate).TotalMinutes;
                TimeSpan spValidMin = TimeSpan.FromMinutes(totalValidMin);

                string body = $"Hi! {name},";
                body += $"\nPlease verify your {SiteSettings.Instance.InstituteShortName} {SiteSettings.Instance.ProductBrandNameSolo} email, using below verification code within {(int) spValidMin.TotalHours:00}:{spValidMin.Minutes:00} Hours (hh:mm).";
                body += "\n\nEmail Verification Code: " + verificationAudit.Code;

                body += EmailFooter;

                using (EmailSender em = new EmailSender(DefaultEmailSettings.GetSmtpClientSettings()))
                {
                    result = em.SendMail(toAddress, subject, body, out error);
                }

                msg = error;
                return result;
            }
            catch (Exception ex)
            {
                msg = ex.ToString();
                return result;
            }
        }

        public static bool SendEmailChangeNoticeAtOldEmail(string subject, string emailToAddress, UAC_VerificationAudit verificationAudit, out string msg)
        {
            string error = String.Empty;
            bool result = false;
            try
            {
                var userCredential = verificationAudit.User_Account;
                MailAddress toAddress = new MailAddress(emailToAddress, userCredential.FullName);
                subject =
                    $"Security Alert! Email Address Changed ({SiteSettings.Instance.InstituteShortName} {SiteSettings.Instance.ProductBrandNameSolo})";
                string body = $"Hi! {userCredential.FullName} [{userCredential.UserName}],";

                string changedBy=String.Empty;
                if (verificationAudit.ConfirmedById==verificationAudit.UserId)
                {
                    changedBy = "Self";
                }
                else
                {
                    changedBy = "Admin";
                }

                body += $"\n\nThis notice confirms that your email address on {SiteSettings.Instance.InstituteShortName} {SiteSettings.Instance.ProductBrandNameSolo} was changed to {userCredential.UserEmail}" +
                        $"\nIf you didn't change your email, please contact with {SiteSettings.Instance.InstituteShortName} IT/Registrar Office immediately."+
                        $"\n(Find contact number of related offices at {SiteSettings.Instance.InstituteWebsite})" +
                        $"\n\nPrevious Email: {emailToAddress}" +
                        $"\nChanged By{(changedBy!=String.Empty? $" ({changedBy})" : changedBy)}: {userCredential.FullName} [{userCredential.UserName}]" +
                        $"\nChanged Time: {verificationAudit.ConfirmedDate?.ToString("h:mm tt")} {verificationAudit.ConfirmedDate?.ToString("dd-MM-yyyy")}" +
                        $"\nIP Address: {verificationAudit.FromIp}" +
                        $"\nNote: {SiteSettings.Instance.ProductBrandNameSolo}  User {userCredential.UserName} can reset password using new email. Also all EMS notification will now send to {userCredential.UserEmail}" ;

                body += EmailFooter;

                using (EmailSender em = new EmailSender(DefaultEmailSettings.GetSmtpClientSettings()))
                {
                    result = em.SendMail(toAddress, subject, body, out error);
                }

                msg = error;
                return result;
            }
            catch (Exception ex)
            {
                msg = ex.ToString();
                return result;
            }
        }

        #endregion

        private bool SendEmail(string name, string emailToAddress, string subject, string body, out string error)
        {
            error = string.Empty;
            bool result = false;
            try
            {
                MailAddress toAddress = new MailAddress(emailToAddress, name);
                using (EmailSender em = new EmailSender(DefaultEmailSettings.GetSmtpClientSettings()))
                {
                    result = em.SendMail(toAddress, subject, body, out error);
                }
                return result;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return result;
            }
        }
    }
}

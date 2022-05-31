using System;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Communication.EmailProxy
{
    public class DefaultEmailSettings
    {
        public DefaultEmailSettings()
        {
        }
        public static string DomainName
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings["DomainName"]); }
        }
        public static string Email_SMTP_Server
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings["Email_SMTP_Server"]).Trim(); }
        }
        public static int Email_SMTP_Port
        {
            get { return Convert.ToInt32(ConfigurationManager.AppSettings["Email_SMTP_Port"]); }
        }
        public static string Email_DisplayName
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings["Email_DisplayName"]).Trim(); }
        }
        public static string Email_UserName
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings["Email_UserName"]).Trim(); }
        }
        public static string Email_Address
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings["Email_Address"]).Trim(); }
        }

        public static string Email_Password
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings["Email_Password"]); }
        }
        public static bool Email_EnableSSL
        {
            get { return Convert.ToBoolean(ConfigurationManager.AppSettings["Email_EnableSSL"]); }
        }

        public static string Email_To_Address
        {
            get { return Convert.ToString(ConfigurationManager.AppSettings["Email_To_Address"]).Trim(); }
        }

        public static EmailSender.SmtpClientSettings GetSmtpClientSettings()
        {
            return new EmailSender.SmtpClientSettings()
            {
                FromMailAddress = new MailAddress(Email_Address, Email_DisplayName),
                UserName = Email_UserName,
                Password = Email_Password,
                Port = Email_SMTP_Port,
                EnableSsl = Email_EnableSSL,
                
                ServerAddress = Email_SMTP_Server
            };

        }
        public static MailAddressCollection GetEmailToAddress()
        {
            string[] toAddress = Email_To_Address.Split(';');
            if (toAddress.Any())
            {
                MailAddressCollection toMailAddress = new MailAddressCollection();
                foreach (var address in toAddress)
                {
                    toMailAddress.Add(new MailAddress(address.Trim(), address));
                }
                return toMailAddress;
            }
            return null;
        }
        public static MailAddressCollection GetSenderAddress(string emailAddress="")
        {
            try
            {
                var email = String.IsNullOrEmpty(emailAddress) ? Email_Address : emailAddress;
                var senderAddress = new MailAddressCollection { new MailAddress( email, Email_DisplayName) };
                return senderAddress;
            }
            catch (Exception exception)
            {
                return null;
                throw;
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Communication.EmailProxy
{
    public class EmailSender : IDisposable
    {

        SmtpClientSettings _SmtpClientSettings { get; set; }

        public EmailSender(SmtpClientSettings smtpClientSettings)
        {
            _SmtpClientSettings = smtpClientSettings;
        }

        public bool SendMail(MailAddress toAddress, string subject, string body, out string error, MailAddressCollection replyToAddress=null)
        {
            try
            {
                //var toAddress = new MailAddress(toEmail, toName);
                var smtp = GetSmtpClient();

                using (var message = new MailMessage(_SmtpClientSettings.FromMailAddress, toAddress)
                {
                    Subject = subject,
                    Body = body

                })
                {
                    if (replyToAddress!=null)
                    {
                        foreach (var item in replyToAddress)
                        {
                            message.ReplyToList.Add(item);
                        }
                    }
                   
                    //message.To.Add(toAddress);
                    //message.From = _HostAddress;
                    //message.Sender = senderAddress.FirstOrDefault();
                    // message.Subject = subject;
                    // message.Body = body;

                    message.IsBodyHtml = false;
                    message.Priority = MailPriority.High;

                    smtp.Send(message);
                }
                error = "Email Send Successfully!";
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool SendMail(MailAddressCollection toAddress, string subject, string body,  out string error, MailAddressCollection replyToAddress)
        {
            try
            {
                //var toAddress = new MailAddress(toEmail, toName);

                var smtp = GetSmtpClient();

                using (var message = new MailMessage() { Subject = subject, Body = body })
                {
                    if (replyToAddress != null)
                    {
                        foreach (var item in replyToAddress)
                        {
                            message.ReplyToList.Add(item);
                        }
                    }
                    foreach (var item in toAddress)
                    {
                        message.To.Add(item);
                    }
                    //message.To.Add(toAddress);
                    message.From = _SmtpClientSettings.FromMailAddress;
                    // message.Sender = senderAddress.FirstOrDefault();

                    // message.Subject = subject;
                    // message.Body = body;

                    message.IsBodyHtml = false;
                    message.Priority = MailPriority.High;

                    smtp.Send(message);
                }
                error = null;
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        private SmtpClient GetSmtpClient()
        {
            return new SmtpClient
            {
                Host = _SmtpClientSettings.ServerAddress, //"smtp.gmail.com",
                Port = _SmtpClientSettings.Port,//587,
                EnableSsl = _SmtpClientSettings.EnableSsl,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_SmtpClientSettings.UserName, _SmtpClientSettings.Password)
            };
        }

        public void Dispose()
        {
            _SmtpClientSettings = null;
        }
        public class SmtpClientSettings
        {
            //private int _timeout=;
            private bool _enableSsl = true;
            public string ServerAddress { get; set; }
            public int Port { get; set; }
            public string UserName { get; set; }
            public MailAddress FromMailAddress { get; set; }
            public string Password { get; set; }
            public bool EnableSsl
            {
                get { return _enableSsl; }
                set { _enableSsl = value; }
            }

            //public int Timeout
            //{
            //    get { return _timeout; }
            //    set { _timeout = value; }
            //}
        }
    }
}

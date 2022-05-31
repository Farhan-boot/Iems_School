using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using EMS.Communication.bd.com.robi.bmpws;

using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.CustomEntity;

namespace EMS.Communication.SMSProxy
{
    public class RobiApi

    {
        public DeliveryStatusEnum GetDeliveryStatus(int status)
        {
            switch (status)
            {
                case -1:
                    return DeliveryStatusEnum.NotSent;
                case 0://Pending
                case 1://SuccessfullySent
                case 2://Processing
                    return DeliveryStatusEnum.SentSuccessfully;
                default:
                    return DeliveryStatusEnum.NotSent;
            }
        }
        public bool GetIsDeliveredByStatus(int status)
        {
            switch (status)
            {
                case -1:
                    return false;
                case 0://Pending
                case 1://SuccessfullySent
                case 2://Processing
                    return true;
                default:
                    return false;
            }
        }

        //public enum RobiDeliveryStatusEnum
        //{
        //    ErrorOccurred = -1,
        //    Pending = 0,
        //    SuccessfullySent = 1,
        //    Processing = 2
        //}
        //public ServiceClass SendSMS(string mobileNumber, string message, out string error)
        //{
        //    error = String.Empty;
        //    var result = new ServiceClass();
        //    try
        //    {
        //        using (var service = new bd.com.robi.bmpws.CMPWebService())
        //        {
        //            result = service.SendTextMessage("mist1", "Mist@123", "8801847170001", mobileNumber, message);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        error = "SMS Can't Send for " + ex.Message;
        //    }
        //    return result;
        //}


        public ServiceClass SendSMS(string mobileNumber, string message, out string error)
        {
            error = String.Empty;
            var result = new ServiceClass();
            string sid = "88018XXXXXXXX";
            string user = "user";
            string pass = "pass@123";
            string URI = "https://bmpws.robi.com.bd/ApacheGearWS/SendTextMessage";

            string parameters = "Username=" + user +
                "&Password=" + pass +
                "&From=" + sid +
                "&To=" + mobileNumber +
                "&Message=" + HttpUtility.UrlEncode(message);
            string URLString = URI + "?" + parameters;
            string xmlStr;
            try
            {
                using (WebClient wc = new WebClient())
                {
                    //wc.Headers[HttpRequestHeader.ContentType] = "application/x-wwwform-urlencoded";
                    xmlStr = wc.DownloadString(URLString);
                }
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(xmlStr);

                XmlElement root = xmlDoc.DocumentElement;
                //if (root != null)
                //{
                //    XmlNodeList nodes = root.SelectNodes("ServiceClass");
                //}

                    if (root != null)
                    foreach (XmlNode node in root.ChildNodes)
                    {
                        if (node.Name == "MessageId" && node.InnerText.IsValid())
                        {
                            result.MessageId = Convert.ToInt32(node.InnerText);
                        }
                        if (node.Name == "Status" && node.InnerText.IsValid())
                        {
                            result.Status = Convert.ToInt32(node.InnerText);
                        }
                        if (node.Name == "StatusText" && node.InnerText.IsValid())
                        {
                            result.StatusText = node.InnerText;
                        }
                        if (node.Name == "ErrorCode" && node.InnerText.IsValid())
                        {
                            result.ErrorCode = Convert.ToInt32(node.InnerText);
                        }
                        if (node.Name == "ErrorText" && node.InnerText.IsValid())
                        {
                            result.ErrorText = node.InnerText;
                        }
                        if (node.Name == "SMSCount" && node.InnerText.IsValid())
                        {
                            result.SMSCount = Convert.ToInt32(node.InnerText);
                        }
                        if (node.Name == "CurrentCredit" && node.InnerText.IsValid())
                        {
                            result.CurrentCredit = Convert.ToInt32(node.InnerText);
                        }
                    }
            }
            catch (Exception exception)
            {
                error = exception.GetBaseException().ToString();
                result.Status = -1;// ErrorOccurred =
                result.ErrorText = error;
            
            }
            return result;

        }
    }
}

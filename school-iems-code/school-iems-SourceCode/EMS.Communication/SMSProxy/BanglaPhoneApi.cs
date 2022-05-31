using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EMS.Communication.SMSProxy
{
    public class BanglaPhoneApi
    {
        private static DeliveryStatusEnum GetDeliveryStatus(BanglaPhoneResponse status)
        {
            switch (status.IsError)
            {
                case true://Pending
                    return DeliveryStatusEnum.SentSuccessfully;
                default:
                    return DeliveryStatusEnum.NotSent;
            }
        }
        public class BanglaPhoneResponse
        {
            public string InsertedSmsIds { get; set; }
            public string Message { get; set; }
            public bool IsError { get; set; }
        }

        //{"insertedSmsIds":"1927","message":"Success!","isError":false}
        
        public SmsResult SendSMS(string mobileNumber, string message, out string error)
        {
            error = String.Empty;
            var result = new SmsResult();
           


           // string sid = "88018XXXXXX";
            string user = "user";
            string pass = "pass";
            string URI = "http://eysoft.powersms.net.bd/httpapi/sendsms";
            string parameters = "userId={0}&password={1}&smsText={2}&commaSeperatedReceiverNumbers={3}";

            //string parameters = "userId=" + HttpUtility.UrlEncode(user) +
            //    "&password=" + HttpUtility.UrlEncode(pass) +
            //    "&smsText=" + HttpUtility.UrlEncode(message)+
            //    "&commaSeperatedReceiverNumbers=" + HttpUtility.UrlEncode(mobileNumber) ;
            parameters = string.Format(parameters, HttpUtility.UrlEncode(user), HttpUtility.UrlEncode(pass),
                HttpUtility.UrlEncode(message), HttpUtility.UrlEncode(mobileNumber));

            string URLString = URI + "?" + parameters;
            string xmlStr;
            try
            {
                using (WebClient wc = new WebClient())
                {
                    //wc.Headers[HttpRequestHeader.ContentType] = "application/x-wwwform-urlencoded";
                    xmlStr = wc.DownloadString(URLString);
                }
                BanglaPhoneResponse response = JsonConvert.DeserializeObject<BanglaPhoneResponse>(xmlStr);
                error = response.Message;
                result.ErrorText = response.Message;
                //result.DeliveryStatus = GetDeliveryStatus(response);
                result.IsSentSuccess = true;
                result.ApiResponseText= xmlStr;
            }
            catch (Exception exception)
            {
                error = exception.GetBaseException().ToString();
                result.IsSentSuccess = false;// ErrorOccurred =
                result.ErrorText = error;
            
            }
            return result;

        }
    }
}

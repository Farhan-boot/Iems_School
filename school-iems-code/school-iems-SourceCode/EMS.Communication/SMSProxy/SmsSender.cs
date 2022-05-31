using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.DataAccess.CustomEntity;


namespace EMS.Communication.SMSProxy
{
    public class SmsSender
    {
        public SmsResult SendSmsByRobiApi(string mobileNumber, string message, out string error)
        {
            error = String.Empty;
            SmsResult result=new SmsResult();

            var robuSms=new RobiApi();
            var sendResult = robuSms.SendSMS(mobileNumber, message, out error);
            //result.DeliveryStatus = robuSms.GetDeliveryStatus(sendResult.Status);
            result.IsSentSuccess = robuSms.GetIsDeliveredByStatus(sendResult.Status);
            result.ErrorText = sendResult.ErrorText;
            result.MessageId = sendResult.MessageId;
            //result.ApiResponseText = xmlStr;
            return result;
        }
        public SmsResult SendSmsByBanglaPhoneApi(string mobileNumber, string message, out string error)
        {
            error = String.Empty;
            var bpSms = new BanglaPhoneApi();
            var sendResult = bpSms.SendSMS(mobileNumber, message, out error);
           
            return sendResult;
        }
    }
}

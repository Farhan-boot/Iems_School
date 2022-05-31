using System;

namespace EMS.DataAccess.CustomEntity
{
    [Flags]
    public enum DeliveryStatusEnum
    {
        NotSent = 0,
        SentSuccessfully = 1,
        //DeliveredSuccessfully = 2,
        //Pending = 3,
        //Processing = 4,
        //DeliveryFailed=5,
        //Unknown = 6
    }
    public class SmsResult
    {
        public int MessageId { get; set; }

        //public DeliveryStatusEnum DeliveryStatus { get; set; }

        public bool IsSentSuccess { get; set; }

        public string ErrorText { get; set; }

        public string ApiResponseText { get; set; }

    }
}

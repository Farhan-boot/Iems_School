//File: Entity Partial
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using EMS.DataAccess.CustomEntity;
using EMS.DataAccess.Data;

namespace EMS.DataAccess.Data
{
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
    public partial class Aca_ClassAttendanceSmsLog
    {
        #region Enum declaration
        
        public enum NotSentReasonEnum
        {
            OldSemester = 0,
            OperatorDenied = 1,
            MobileNumberNotFound = 2,
        }
        [DataMember]
        public DeliveryStatusEnum DeliveryStatus
        {
            get { return (DeliveryStatusEnum)DeliveryStatusEnumId; }
            set
            {
                DeliveryStatusEnumId = (Byte)value;
            }
        }
        #endregion Enum Property

        private static void GetNewExtra(Aca_ClassAttendanceSmsLog obj)
        {

        }

    }
}

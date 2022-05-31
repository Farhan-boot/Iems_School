//File: Entity Partial
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

using EMS.DataAccess.Data;

namespace EMS.DataAccess.Data
{
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
    public partial class HR_AppointmentHistory
	{
        #region Enum Property
        public enum HistoryTypeEnum
        {
            Join = 0,
            Promotion = 1,
            OnStudyLeave = 2,
            MaternityLeave = 3,
            OnLeaveOfficial = 4,
            Rejoin = 5,
            Terminated = 6,
            Resigned = 7,
            SpecialLeave = 8,
            OtherLeave = 9,
            AdditionalResponsibility = 10,
            LeaveExtension = 11,
            BreakOfService = 12,
            Suspension = 13,
            Other = 14,
        }
        #endregion Enum Property
        #region Enum Property
        [DataMember]
        public HistoryTypeEnum HistoryType
        {
            get
            {
                return (HistoryTypeEnum)HistoryTypeEnumId;
            }
            set
            {
                HistoryTypeEnumId = (Byte)value;
            }
        }
        #endregion Enum Property

        private static void  GetNewExtra(HR_AppointmentHistory obj)
          {
          
          }
        
	}
}

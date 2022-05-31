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
    public partial class HR_EmployementHistory
    {
        #region Enum Property
        public enum EmployementTypeEnum
        {
            Permanent = 0,
            Attached = 1,
            Contractual = 2,
        }
        public enum JobTypeEnum
        {
            FullTime = 0,
            PartTime = 1,
            Adjunt = 2,
            Guest = 3,
            Visiting = 4,
            Research = 5
        }
        public enum HistoryTypeEnum
        {
            Promotion = 0,
            OnStudyLeave = 1,
            Other = 2,
            Rejoin = 3,
            MaternityLeave = 4,
            OnLeaveOfficial = 5,
            Join = 6,
            Terminated = 7,
            Resigned = 8,
            SpecialLeave = 9,
            OtherLeave = 10,
            AdditionalResponsibility = 11,
            NewlyJoin = 12,
            LeaveExtension = 13,
            BreakOfService = 14,
            Suspension = 15
        }
        #endregion Enum Property
        
        
        #region Enum Property
        [DataMember]
        public EmployementTypeEnum EmployementType
        {
            get
            {
                return (EmployementTypeEnum)EmployementTypeEnumId;
            }
            set
            {
                EmployementTypeEnumId = (Byte)value;
            }
        }
        [DataMember]
        public JobTypeEnum JobType
        {
            get
            {
                return (JobTypeEnum)JobTypeEnumId;
            }
            set
            {
                JobTypeEnumId = (Byte)value;
            }
        }
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

        private static void GetNewExtra(HR_EmployementHistory obj)
        {

        }

    }
}

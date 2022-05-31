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
    public partial class Aca_ClassAttendance
    {
        #region Enum declaration
        [Flags]
        public enum ReasonEnum
        {
            SIQ =0, 
            CMH=1, 
            RequestSick=2,
            Leave=3,
            Unknown = 4,
        }
        //public enum ReasonEnum
        //{
        //    Unknown = 0,
        //    SIQ = 1,
        //    CMH = 2,
        //    RequestSick = 3,
        //    Leave = 4,
        //}

        [DataMember]
        public ReasonEnum Reason
        {
            get { return (ReasonEnum)ReasonEnumId; }
            set
            {
                ReasonEnumId = (Byte)value;
            }
        }

        #endregion Enum Property

        public string EmployeeName { get; set; }
        public string Username { get; set; }
        public string StudentName { get; set; }
        public string ClassRoll { get; set; }
        private static void GetNewExtra(Aca_ClassAttendance obj)
        {
            obj.StartTime = DateTime.Today;
            obj.EndTime = DateTime.Today;
            obj.StartTime = obj.StartTime.AddHours(8);
            obj.EndTime = obj.EndTime.AddHours(8).AddMinutes(50);
        }

    }
}

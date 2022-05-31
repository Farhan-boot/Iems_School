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
    public partial class Aca_ClassAttendanceSetting
    {
        #region Enum declaration
        [Flags]
        public enum ClassTypeEnum
        {
            RegularClass = 0,
            MakeupClass = 1,
            ExtraClass = 2,
        }
        [DataMember]
        public ClassTypeEnum ClassType
        {
            get { return (ClassTypeEnum)ClassTypeEnumId; }
            set
            {
                ClassTypeEnumId = (Byte)value;
            }
        }
        #endregion Enum Property

        private static void GetNewExtra(Aca_ClassAttendanceSetting obj)
        {
            obj.StartTime = DateTime.Today;
            obj.EndTime = DateTime.Today;
            obj.StartTime = obj.StartTime.AddHours(8);
            obj.EndTime = obj.EndTime.AddHours(8).AddMinutes(50);
        }

    }
}

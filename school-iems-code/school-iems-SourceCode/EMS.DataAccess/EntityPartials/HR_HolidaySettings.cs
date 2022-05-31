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
    public partial class HR_HolidaySettings
	{
          #region Enum declaration
           [Flags]
           public enum WorkingDayTypeEnum
           {
                none = 0,
           }
            [DataMember]
            public WorkingDayTypeEnum WorkingDayType
            {
                get{return (WorkingDayTypeEnum)WorkingDayTypeEnumId;}
                set
                {
                    WorkingDayTypeEnumId = (Byte)value;
                }
            }
           #endregion Enum Property
          
          private static void  GetNewExtra(HR_HolidaySettings obj)
          {
          
          }
        
	}
}

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
    public partial class HR_Attendance
	{
          #region Enum declaration
           [Flags]
           public enum PunchTypeEnum
           {
                none = 0,
           }
           [Flags]
           public enum PunchModeEnum
           {
                none = 0,
           }
            [DataMember]
            public PunchTypeEnum PunchType
            {
                get{return (PunchTypeEnum)PunchTypeEnumId;}
                set
                {
                    PunchTypeEnumId = (Byte)value;
                }
            }
            [DataMember]
            public PunchModeEnum PunchMode
            {
                get{return (PunchModeEnum)PunchModeEnumId;}
                set
                {
                    PunchModeEnumId = (Byte)value;
                }
            }
           #endregion Enum Property
          
          private static void  GetNewExtra(HR_Attendance obj)
          {
          
          }
        
	}
}

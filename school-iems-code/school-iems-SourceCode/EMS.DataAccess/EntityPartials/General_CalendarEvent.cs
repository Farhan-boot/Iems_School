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
    public partial class General_CalendarEvent
	{
          #region Enum declaration
           [Flags]
           public enum VisibilityEnum
           {
                none = 0,
           }
           [Flags]
           public enum RepeatsEnum
           {
                none = 0,
           }
           [Flags]
           public enum RepeatsPeriodEnum
           {
                none = 0,
           }
            [DataMember]
            public VisibilityEnum Visibility
            {
                get{return (VisibilityEnum)VisibilityEnumId;}
                set
                {
                    VisibilityEnumId = (Byte)value;
                }
            }
            [DataMember]
            public RepeatsEnum Repeats
            {
                get{return (RepeatsEnum)RepeatsEnumId;}
                set
                {
                    RepeatsEnumId = (Byte)value;
                }
            }
            [DataMember]
            public RepeatsPeriodEnum RepeatsPeriod
            {
                get{return (RepeatsPeriodEnum)RepeatsPeriodEnumId;}
                set
                {
                    RepeatsPeriodEnumId = (Byte)value;
                }
            }
           #endregion Enum Property
          
          private static void  GetNewExtra(General_CalendarEvent obj)
          {
          
          }
        
	}
}

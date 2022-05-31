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
    public partial class Aca_OnlineClass
	{
          #region Enum declaration
           [Flags]
           public enum ClassMediaEnum
           {
                none = 0,
           }
           [Flags]
           public enum StatusEnum
           {
                none = 0,
           }

           [Flags]
           public enum RecurringDayEnum
        {
               none = 0,
           }
        [DataMember]
            public ClassMediaEnum ClassMedia
            {
                get{return (ClassMediaEnum)ClassMediaEnumId;}
                set
                {
                    ClassMediaEnumId = (Byte)value;
                }
            }

            [DataMember]
            public RecurringDayEnum RecurringDay
        {
                get { return (RecurringDayEnum)RecurringDayEnumId; }
                set
                {
                    RecurringDayEnumId = (Byte)value;
                }
            }

        #endregion Enum Property

        private static void  GetNewExtra(Aca_OnlineClass obj)
          {
          
          }
        
	}
}

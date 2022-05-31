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
    public partial class General_AllSmsLog
	{
          #region Enum declaration
           [Flags]
           public enum TypeEnum
           {
                none = 0,
           }
           [Flags]
           public enum DeliveryStatusEnum
           {
                none = 0,
           }
            [DataMember]
            public TypeEnum Type
            {
                get{return (TypeEnum)TypeEnumId;}
                set
                {
                    TypeEnumId = (Byte)value;
                }
            }
            [DataMember]
            public DeliveryStatusEnum DeliveryStatus
            {
                get{return (DeliveryStatusEnum)DeliveryStatusEnumId;}
                set
                {
                    DeliveryStatusEnumId = (Byte)value;
                }
            }
           #endregion Enum Property
          
          private static void  GetNewExtra(General_AllSmsLog obj)
          {
          
          }
        
	}
}

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
    public partial class Inv_ItemRequest
	{
          #region Enum declaration
           [Flags]
           public enum RequestTypeEnum
           {
                none = 0,
           }
           [Flags]
           public enum UnitTypeEnum
           {
                none = 0,
           }
           [Flags]
           public enum StatusEnum
           {
                none = 0,
           }
            [DataMember]
            public RequestTypeEnum RequestType
            {
                get{return (RequestTypeEnum)RequestTypeEnumId;}
                set
                {
                    RequestTypeEnumId = (Int32)value;
                }
            }
            [DataMember]
            public UnitTypeEnum UnitType
            {
                get{return (UnitTypeEnum)UnitTypeEnumId;}
                set
                {
                    UnitTypeEnumId = (Int32)value;
                }
            }
            [DataMember]
            public StatusEnum Status
            {
                get{return (StatusEnum)StatusEnumId;}
                set
                {
                    StatusEnumId = (Int32)value;
                }
            }
           #endregion Enum Property
          
          private static void  GetNewExtra(Inv_ItemRequest obj)
          {
          
          }
        
	}
}

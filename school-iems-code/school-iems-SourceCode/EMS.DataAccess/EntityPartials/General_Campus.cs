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
    public partial class General_Campus
	{
          #region Enum declaration
           [Flags]
           public enum TypeEnum
           {
                Permanent = 0,
                Temporary = 1,
           }
           [Flags]
           public enum StatusEnum
           {
                Available = 0,
                UnderMaintenance = 1,
                UnderConstruction = 2,
                Temporary = 3
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
            public StatusEnum Status
            {
                get{return (StatusEnum)StatusEnumId;}
                set
                {
                    StatusEnumId = (Byte)value;
                }
            }
           #endregion Enum Property
          
          private static void  GetNewExtra(General_Campus obj)
          {
          
          }
        
	}
}

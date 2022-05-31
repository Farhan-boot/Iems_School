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
    public partial class UCS_SmsLog
	{
          #region Enum declaration
           [Flags]
           public enum ModuleEnum
           {
                none = 0,
           }
           [Flags]
           public enum StatusEnum
           {
                none = 0,
           }
            [DataMember]
            public ModuleEnum Module
            {
                get{return (ModuleEnum)ModuleEnumId;}
                set
                {
                    ModuleEnumId = (Byte)value;
                }
            }
            [DataMember]
            public StatusEnum? Status
            {
                get { return StatusEnumId != null ? (StatusEnum?)StatusEnumId : null; }
                set
                {
                    StatusEnumId = (Nullable<Byte>)value;
                }
            }
           #endregion Enum Property
          
          private static void  GetNewExtra(UCS_SmsLog obj)
          {
          
          }
        
	}
}

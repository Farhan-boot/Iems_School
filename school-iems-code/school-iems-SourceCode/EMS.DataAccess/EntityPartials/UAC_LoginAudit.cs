//File: Entity Partial
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using EMS.CoreLibrary;
using EMS.DataAccess.Data;

namespace EMS.DataAccess.Data
{
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
    public partial class UAC_LoginAudit
	{
          #region Enum declaration
           #endregion Enum declaration
           #region Enum Property
            [DataMember]
            public EnumCollection.UserCredentialAuditsTypeEnum Status
            {
                get{return (EnumCollection.UserCredentialAuditsTypeEnum)StatusEnumId;}
                set
                {
                    StatusEnumId = (Byte)value;
                }
            }
           #endregion Enum Property
          
          private static void  GetNewExtra(UAC_LoginAudit obj)
          {
          
          }
        
	}
}

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
    public partial class UAC_VerificationAudit
	{
          #region Enum declaration
           [Flags]
           public enum RequestByEnum
           {
                Self = 1,
                Admin = 2
            }
           [Flags]
           public enum RequestTypeEnum
           {
                ResetPassword = 0,
                ForgetPassword = 1,
                TwoFactor = 2,
                EmailVerify = 3,
                MobileVerify = 4
            }
           [Flags]
           public enum StatusEnum
           {
                Initiated = 1,
                Success = 2,
                Invalid=3
            }
            [DataMember]
            public RequestByEnum RequestBy
            {
                get{return (RequestByEnum)RequestByEnumId;}
                set
                {
                    RequestByEnumId = (Byte)value;
                }
            }
            [DataMember]
            public RequestTypeEnum RequestType
            {
                get{return (RequestTypeEnum)RequestTypeEnumId;}
                set
                {
                    RequestTypeEnumId = (Byte)value;
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
          
          private static void  GetNewExtra(UAC_VerificationAudit obj)
          {
          
          }
        
	}
}

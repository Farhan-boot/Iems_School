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
    public partial class Aca_HomeWorkSubmission
	{
          #region Enum declaration
           [Flags]
           public enum SubmissionStatusEnum
           {
                none = 0,
           }
            [DataMember]
            public SubmissionStatusEnum SubmissionStatus
            {
                get{return (SubmissionStatusEnum)SubmissionStatusEnumId;}
                set
                {
                    SubmissionStatusEnumId = (Int32)value;
                }
            }
           #endregion Enum Property
          
          private static void  GetNewExtra(Aca_HomeWorkSubmission obj)
          {
          
          }
        
	}
}

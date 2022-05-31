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
    public partial class HR_LeaveAllocationTemplateDetails
	{
          #region Enum declaration
           [Flags]
           public enum LeaveTypeEnum
           {
                none = 0,
           }
            [DataMember]
            public LeaveTypeEnum LeaveType
            {
                get{return (LeaveTypeEnum)LeaveTypeEnumId;}
                set
                {
                    LeaveTypeEnumId = (Byte)value;
                }
            }
           #endregion Enum Property
          
          private static void  GetNewExtra(HR_LeaveAllocationTemplateDetails obj)
          {
          
          }
        
	}
}

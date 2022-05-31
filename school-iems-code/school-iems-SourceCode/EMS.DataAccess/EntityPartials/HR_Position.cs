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
    public partial class HR_Position
	{
          #region Enum Property
           [Flags]
           public enum StatusEnum
        {
            Inactive = 0,
            Active = 1,
            Deleted = 2,
        }
        #endregion Enum Property
        #region Enum Property
        [DataMember]
        public StatusEnum Status
        {
            get
            {
                return (StatusEnum)StatusEnumId;
            }
            set
            {
                StatusEnumId = (Byte)value;
            }
        }
        [DataMember]
        public User_Employee.JobClassEnum JobClass
        {
            get
            {
                return (User_Employee.JobClassEnum)JobClassEnumId;
            }
            set
            {
                JobClassEnumId = (byte)value;
            }
        }
        #endregion Enum Property

        private static void  GetNewExtra(HR_Position obj)
          {
          
          }
        
	}
}

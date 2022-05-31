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
    public partial class HR_Department
	{
        #region Enum Property
        public enum TypeEnum
        {
            Administrative = 0,
            AcademicFaculty = 1,
            AcademicDepartment = 2,
            AcademicSupportingDepartment = 3
        }
        public enum StatusEnum
        {
            Inactive = 0,
            Active = 1,
            Deleted = 2,
        }
        #endregion Enum Property
        #region Enum Property
        [DataMember]
        public TypeEnum Type
        {
            get
            {
                return (TypeEnum)TypeEnumId;
            }
            set
            {
                TypeEnumId = (Int32)value;
            }
        }
        [DataMember]
        public StatusEnum Status
        {
            get
            {
                return (StatusEnum)StatusEnumId;
            }
            set
            {
                StatusEnumId = (Int32)value;
            }
        }
        #endregion Enum Property

        private static void  GetNewExtra(HR_Department obj)
          {
          
          }
        
	}
}

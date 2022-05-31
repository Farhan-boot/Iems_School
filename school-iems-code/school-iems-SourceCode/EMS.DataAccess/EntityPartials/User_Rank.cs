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
    public partial class User_Rank
	{
          #region Enum Property
           public enum AcademicLevelEnum
           {
                None = 0,
                SeniorInstructor = 1,
                InstructorClassA = 2,
                InstructorClassB = 3,
                InstructorClassC = 4,
           }
        #endregion Enum Property
        #region Enum Property
        [DataMember]
        public User_Account.UserCategoryEnum Category
        {
            get
            {
                return (User_Account.UserCategoryEnum)CategoryEnumId;
            }
            set
            {
                CategoryEnumId = (Byte)value;
            }
        }
        public AcademicLevelEnum AcademicLevel
        {
            get
            {
                return (AcademicLevelEnum)AcademicLevelEnumId;
            }
            set
            {
                AcademicLevelEnumId = (Byte)value;
            }
        }
        public User_Employee.JobClassEnum JobClass
        {
            get
            {
                return (User_Employee.JobClassEnum)JobClassEnumId;
            }
            set
            {
                JobClassEnumId = (Byte)value;
            }
        }
        #endregion Enum Property

        private static void  GetNewExtra(User_Rank obj)
          {
            obj.CategoryEnumId = (byte)Data.User_Account.UserCategoryEnum.Civil;
            obj.JobClassEnumId = (byte)User_Employee.JobClassEnum.FirstGrade;
            obj.AcademicLevelEnumId = (byte)AcademicLevelEnum.None;
          }
        
	}
}

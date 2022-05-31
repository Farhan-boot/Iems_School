using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using EMS.CoreLibrary;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;

namespace EMS.DataAccess.Data
{
    public partial class User_Employee
    {
        #region Enum Property
        public enum EmployeeCategoryEnum
        {
            AdministrativeOfficer = 1,
            FacultyMember = 2,
            SupportingStaff = 3,
            //AdministrativeOfficer 
        }
        public enum EmployeeTypeEnum
        {
            Permanent = 1,
            Contractual = 2,
            Guest = 3,
            Visiting = 4,
            Adjunct = 5,
            Research = 6
        }
        public enum JobTypeEnum
        {
            FullTime = 1,
            PartTime = 2,
        }
        public enum WorkingStatusEnum
        {
            Active = 1,
            OnLeave = 2,
            OnstudyLeave = 3,
            MedicalLeave = 4,
            SabbaticalLeave = 5,
            Terminated = 6,
            Resigned = 7,
            MaternityLeave = 8,
            Retired = 9
           
        }
        public enum JobClassEnum
        {
            FirstGrade = 1,
            SecondGrade = 2,
            ThirdGrade = 3,
            FourthGrade = 4,
            FifthGrade = 5,
            SixthGrade = 6,
        }

        public enum SalaryGradeEnum
        {
            None=0
        }


        #endregion Enum Property

        #region Enum Property
        [DataMember]
        public EmployeeCategoryEnum EmployeeCategory
        {
            get
            {
                return (EmployeeCategoryEnum)EmployeeCategoryEnumId;
            }
            set
            {
                EmployeeCategoryEnumId = (byte)value;
            }
        }
        [DataMember]
        public EmployeeTypeEnum EmployeeType
        {
            get
            {
                return (EmployeeTypeEnum)EmployeeTypeEnumId;
            }
            set
            {
                EmployeeTypeEnumId = (byte)value;
            }
        }
        [DataMember]
        public JobClassEnum JobClass
        {
            get
            {
                return (JobClassEnum)JobClassEnumId;
            }
            set
            {
                JobClassEnumId = (byte)value;
            }
        }
        [DataMember]
        public JobTypeEnum JobType
        {
            get
            {
                return (JobTypeEnum)JobTypeEnumId;
            }
            set
            {
                JobTypeEnumId = (byte)value;
            }
        }
        [DataMember]
        public User_Employee.WorkingStatusEnum WorkingStatus
        {
            get
            {
                return (User_Employee.WorkingStatusEnum)WorkingStatusEnumId;
            }
            set
            {
                WorkingStatusEnumId = (byte)value;
            }
        }
        [DataMember]
        public SalaryGradeEnum SalaryGrade
        {
            get
            {
                return (SalaryGradeEnum)SalaryGradeEnumId;
            }
            set
            {
                SalaryGradeEnumId = (byte)value;
            }
        }
        [DataMember]
        public EnumCollection.Common.MonthEnum IncrementMonth
        {
            get
            {
                return (EnumCollection.Common.MonthEnum)IncrementMonthEnumId;
            }
            set
            {
                IncrementMonthEnumId = (byte)value;
            }
        }

        #endregion Enum Property

        #region Custom Variables
        private string _userName = "";
        public String UserName
        {
            get
            {
                if (!_userName.IsValid() && this.User_Account != null)
                {
                    _userName = this.User_Account.UserName;
                }
                return _userName;
            }
            set { _userName = value; }
        }
        private string _fullName = "";
        public String FullName
        {
            get
            {
                if (!_userName.IsValid() && this.User_Account != null)
                {
                    _fullName = this.User_Account.FullName;

                }
                return _fullName;
            }
            set { _fullName = value; }
        }
        #endregion Custom Variables
        private static void GetNewExtra(User_Employee obj)
        {

        }
    }
}

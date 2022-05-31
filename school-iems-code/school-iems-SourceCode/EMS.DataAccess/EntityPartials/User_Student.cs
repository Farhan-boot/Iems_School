using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Runtime.Serialization;
using EMS.CoreLibrary;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;

namespace EMS.DataAccess.Data
{
    public partial class User_Student
    {
        #region Enum Property

        public enum EnrollmentStatusEnum
        {
           
            Continuing = 0,
            Graduating = 1,
            Graduated = 2,
            DropOut = 3,
            Terminated = 4,
            Applicant = 5,
            //ReturnedToUnit = 5,
            AdmissionCancelled = 6,//rechek
            DepartmentChanged = 7
        }
        public enum EnrollmentTypeEnum
        {
            FreshStudent = 0,
            TransfereeStudent = 1,
            ReadmissionStudent = 2,
            InternalPackageStudent=3,    //InternalFreshStudent = 3,
            InternalDepartmentChange = 4,
            IrregularStudent = 5,
            VisitorStudent = 6,
        }

    
        public enum AdmissionStatusEnum
        {
            
            AdmissionPending = 0,//enrollment Applicant or admisson fee unpaid
            ProfileAndAdmissionCompleted = 1,//fee paid
            AdmissionCancelled = 2,
        }
        [DataMember]
        public AdmissionStatusEnum AdmissionStatus
        {
            get
            {
                return (AdmissionStatusEnum)AdmissionStatusEnumId;
            }
            set
            {
                AdmissionStatusEnumId = (Byte)value;
            }
        }
        #endregion Enum Property

        #region Enum Property

        public Aca_Program.ClassTimingGroupEnum ClassTimingGroup
        {
            get
            {
                return (Aca_Program.ClassTimingGroupEnum)ClassTimingGroupEnumId;
            }
            set
            {
                ClassTimingGroupEnumId = (Byte)value;
            }
        }
        [DataMember]
        public EnrollmentStatusEnum EnrollmentStatus
        {
            get
            {
                return (EnrollmentStatusEnum)EnrollmentStatusEnumId;
            }
            set
            {
                EnrollmentStatusEnumId = (Byte)value;
            }
        }
        [DataMember]
        public EnrollmentTypeEnum EnrollmentType
        {
            get
            {
                return (EnrollmentTypeEnum)EnrollmentTypeEnumId;
            }
            set
            {
                EnrollmentTypeEnumId = (Byte)value;
            }
        }

        [DataMember]
        public Aca_Semester.SemesterDurationEnum SemesterDuration
        {
            get
            {
                return (Aca_Semester.SemesterDurationEnum)SemesterDurationEnumId;
            }
            set
            {
                SemesterDurationEnumId = (int)value;
            }
        }

        #endregion Enum Property

        #region Custom Variables
        //public Aca_ExamRoll ExamRollObj { get; set; }
        public dynamic DataBag = new ExpandoObject();
        public int ExamRollNo { get; set; }
        public long ExamId { get; set; }
        public string ExamKey { get; set; }
        public string ProfilePictureBase64 { get; set; }


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


        public byte JsonIsSelected { get; set; }
        public byte JsonIsActiveFor { get; set; }


        #endregion
        

        private static void GetNewExtra(User_Student obj)
        {
            //obj.DataBag = new ExpandoObject();
           
        }
    }
}

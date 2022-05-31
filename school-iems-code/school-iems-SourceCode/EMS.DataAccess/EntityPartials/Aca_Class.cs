using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using EMS.DataAccess.Data;

namespace EMS.DataAccess.Data
{
    public partial class Aca_Class
    {
        #region Enum Property

        public string ClassTakenByStudentHistory { get; set; }
        public string ProgramName { get; set; }
        public string ProgramShortName { get; set; }
        public string ProgramShortTitle { get; set; }
        public byte RegistrableCourseStatusEnumId { get; set; }
        //public enum TypeEnum
        //{
        //    CampusClass = 0,
        //    //TakeWayHome = 1,
        //}

        public enum RegistrableCourseStatusEnum
        {
            FreshRegistration = 1,
            TakenInOtherSemesterRetakeAble = 2,
            TakenInOtherSemesterImprovementAble = 3,
            DropInThisSemester = 4,
            CancelledInThisSemester = 5,
            TakenInThisSemester = 6,
            AnotherSectionTakenInThisSemester = 7,
            PrerequisiteNotComplete = 8,
            NotInCurriculum = 9,
            CourseCompleted = 10
        }
        public enum StatusEnum
        {
            Open = 0,
            Full = 1,
            Closed = 2,
            Cancel = 3,
            Reserved = 4,
        }
        /*
        public enum RegistrableCourseStatusEnum
        {
            None = 0,
            Retakable = 1,
            PreRequisiteNotCompleted = 2,
            MinCreditNotCompleted = 4,
            Registered = 8,
            Dropped = 16
        }
        */

        //[DataMember]
        //public TypeEnum Type
        //{
        //    get
        //    {
        //        return (TypeEnum)TypeEnumId;
        //    }
        //    set
        //    {
        //        TypeEnumId = (Byte)value;
        //    }
        //}
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
        public RegistrableCourseStatusEnum RegistrableCourseStatus
        {
            get
            {
                return (RegistrableCourseStatusEnum)RegistrableCourseStatusEnumId;
            }
            set
            {
                RegistrableCourseStatusEnumId = (Byte)value;
            }
        }

        [DataMember]
        public Aca_Program.ProgramTypeEnum ProgramType
        {
            get
            {
                return (Aca_Program.ProgramTypeEnum)ProgramTypeEnumId;
            }
            set
            {
                ProgramTypeEnumId = (Byte)value;
            }
        }
        #endregion Enum Property


        public int TotalStudent { get; set; }

        //used in emp dashboard and Hr emaployee edit
        public List<string> EmployeeInRoleList { get; set; }
        //used in student dashboard 
        public List<string> TeacherNameList { get; set; }
        public Aca_ClassTakenByStudent.RegistrationStatusEnum StudentRegistrationStatus { get; set; }
        public byte StudentRegistrationStatusEnumId { get; set; }
        public Aca_ClassTakenByStudent.StatusEnum StudentStatus { get; set; }

        private static void GetNewExtra(Aca_Class obj)
        {
            obj.ClassSectionId = 1;
            obj.CampusId =1;
        }

        #region Constants
        public const string Property_StudentRegistrationStatusEnumId = "StudentRegistrationStatusEnumId";
        #endregion


    }
}

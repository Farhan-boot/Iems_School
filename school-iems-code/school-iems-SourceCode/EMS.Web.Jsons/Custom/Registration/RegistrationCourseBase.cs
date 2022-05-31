//File: Entity Partial Base
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;

namespace EMS.DataAccess.Data
{

    /// <summary>
    /// Do not modify this class, edit partial instead.
    /// </summary>   
    public class RegistrationCourse
    {
        #region Enum declaration

        [Flags]
        public enum RegistrationStatusEnum
        {
            CanTake = 0,
            Completed = 1,
            Retakable = 2,
            PreRequisiteNotCompleted = 3,
            MinCreditNotCompleted = 4,
            Registered = 5,
            Dropped = 6
        }


        public Aca_Course.CourseCategoryEnum CourseCategory
        {
            get { return (Aca_Course.CourseCategoryEnum)CourseCategoryEnumId; }
            set
            {
                CourseCategoryEnumId = (Byte)value;
            }
        }

        public RegistrationStatusEnum RegistrationStatus
        {
            get { return (RegistrationStatusEnum)RegistrationStatusEnumId; }
            set
            {
                RegistrationStatusEnumId = (Byte)value;
            }
        }

        public Aca_Class.StatusEnum ClassStatus
        {
            get { return (Aca_Class.StatusEnum)ClassStatusEnumId; }
            set
            {
                ClassStatusEnumId = (Byte)value;
            }
        }
        #endregion Enum Property



        #region Constants
        public const string Property_ClassId = "ClassId";
        public const string Property_Code = "Code";
        public const string Property_Name = "Name";
        public const string Property_RegularCapacity = "RegularCapacity";
        public const string Property_CreditLoad = "CreditLoad";
        public const string Property_CourseCategoryEnumId = "CourseCategoryEnumId";
        public const string Property_CourseCategory = "CourseCategory";
        public const string Property_RegistrationStatusEnumId = "RegistrationStatusEnumId";
        public const string Property_RegistrationStatus = "RegistrationStatus";
        public const string Property_ClassStatusEnumId = "ClassStatusEnumId";
        public const string Property_ClassStatus = "ClassStatus";
        public const string Property_StudentCount = "StudentCount";
        public const string Property_CurriculumCourseId = "CurriculumCourseId";
        public const string Property_Grade = "Grade";
        public const string Property_RoutineDetail = "RoutineDetail";
        public const string Property_Remark = "Remark";

        public const string Property_IsSelected = "IsSelected";
        public const string Property_IsAlreadyAdded = "IsAlreadyAdded";
        #endregion

        public int ClassId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public short RegularCapacity { get; set; }
        public short CreditLoad { get; set; }
        public byte CourseCategoryEnumId { get; set; }
        public byte RegistrationStatusEnumId { get; set; } 
        public byte ClassStatusEnumId { get; set; }
        public int StudentCount { get; set; }
        public long CurriculumCourseId { get; set; }
        public string Grade { get; set; }
        public string RoutineDetail { get; set; }
        public string Remark { get; set; }
        public bool IsSelected { get; set; }
        public bool IsAlreadyAdded = true;




        public static RegistrationCourse GetNew(Int32 classId = 0)
        {
            RegistrationCourse obj = new RegistrationCourse();
            obj.ClassId = classId;
            obj.Code = string.Empty;
            obj.Name = string.Empty;
            obj.RegularCapacity = 0;
            obj.CreditLoad = 0;
            obj.CourseCategoryEnumId = 0;
            obj.RegistrationStatusEnumId = 0;
            obj.ClassStatusEnumId = 0;
            obj.StudentCount = 0;
            obj.CurriculumCourseId = 0;
            obj.Grade = string.Empty;
            obj.RoutineDetail = string.Empty;
            obj.Remark = string.Empty;
            obj.IsAlreadyAdded = false;
            return obj;
        }

    }
}

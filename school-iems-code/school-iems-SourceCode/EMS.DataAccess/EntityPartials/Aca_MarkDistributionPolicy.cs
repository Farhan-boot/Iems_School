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
    public partial class Aca_MarkDistributionPolicy
    {
        #region Enum Property
        [Flags]
        public enum StatusEnum
        {
            Active = 0,
            Inactive = 1,
        }
        //[Flags]
        //public enum CourseCategoryEnum
        //{
        //    Theory = 0,
        //    Sessional = 1,
        //    Thesis = 2,
        //    IndustrialTraining = 3,
        //    OtherCourse = 4

        //}
        //[Flags]
        //public enum ExamTypeEnum
        //{
        //    Regular = 0,
        //    Backlog = 1,
        //    Referred = 2

        //}


        [DataMember]
        public Aca_Course.CourseCategoryEnum CourseCategory
        {
            get
            {
                return (Aca_Course.CourseCategoryEnum)CourseCategoryEnumId;
            }
            set
            {
                CourseCategoryEnumId = (byte)value;
            }
        }
        /*public Aca_Program.ProgramTypeEnum ProgramType
        {
            get
            {
                return (Aca_Program.ProgramTypeEnum)ProgramTypeEnumId;
            }
            set
            {
                ProgramTypeEnumId = (byte)value;
            }
        }*/

        [DataMember]
        public Aca_Exam.ExamTypeEnum ExamType
        {
            get
            {
                return (Aca_Exam.ExamTypeEnum)ExamTypeEnumId;
            }
            set
            {
                ExamTypeEnumId = (Byte)value;
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
                StatusEnumId = (Byte)value;
            }
        }
        #endregion Enum Property

        private static void GetNewExtra(Aca_MarkDistributionPolicy obj)
        {
           // obj.CourseCategoryEnumId = 0;
        }

    }
}

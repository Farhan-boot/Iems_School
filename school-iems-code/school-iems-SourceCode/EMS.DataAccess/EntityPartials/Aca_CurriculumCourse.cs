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
    public partial class Aca_CurriculumCourse
    {
        #region Enum Property

  
    
        public enum CourseTypeEnum
        {
            Core = 0,
            CoreElective = 1,
            Elective = 2,
        }
        public enum CreditTypeEnum
        {
            Credited = 0,
            Continution = 1,
            NonCredited = 2,
            CourseWaiver = 3
        }
        public enum StatusEnum
        {
            Completation = 0,
        }

        [DataMember]
        public Aca_Course.CourseCategoryEnum CourseCategory
        {
            get
            {
                return (Aca_Course.CourseCategoryEnum)CourseCategoryEnumId;
            }
            set
            {
                CourseCategoryEnumId = (Byte)value;
            }
        }
        [DataMember]
        public CourseTypeEnum CourseType
        {
            get
            {
                return (CourseTypeEnum)CourseTypeEnumId;
            }
            set
            {
                CourseTypeEnumId = (Byte)value;
            }
        }
        //[DataMember]
        ////public CreditTypeEnum CreditType
        ////{
        ////    get
        ////    {
        ////        return (CreditTypeEnum)CreditTypeEnumId;
        ////    }
        ////    set
        ////    {
        ////        CreditTypeEnumId = (Byte)value;
        ////    }
        ////}
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

        private static void GetNewExtra(Aca_CurriculumCourse obj)
        {
            obj.CourseCategoryEnumId = (byte)Aca_Course.CourseCategoryEnum.Theory;
            obj.CourseTypeEnumId = (byte)CourseTypeEnum.Core;
            obj.StatusEnumId = (byte)StatusEnum.Completation;
        }

    }
}

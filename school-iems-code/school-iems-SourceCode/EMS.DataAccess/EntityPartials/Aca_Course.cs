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
    public partial class Aca_Course
    {

        public string NormalizeName { get; set; }
        public string NormalizeCode { get; set; }


        #region Enum declaration

        //LabOrSessional = 1,
        //    ProjectOrThesis = 2,
        //    IndustrialTraining = 3,
        //    FieldTripOrSeminar = 4,
        public enum CourseCategoryEnum
        {
            Theory = 0,
            Lab = 1,//LabOrSessional
            ProjectOrThesis = 2,
            InternshipOrTraining = 3,//IndustrialTraining
            FieldTripOrSeminarOrVivaVoce = 4,
            OtherCourse = 5
        }
        [DataMember]
        public CourseCategoryEnum CourseCategory
        {
            get { return (CourseCategoryEnum)CourseCategoryEnumId; }
            set
            {
                CourseCategoryEnumId = (Byte)value;
            }
        }
        #endregion Enum Property

        private static void GetNewExtra(Aca_Course obj)
        {
            obj.CreditLoad = 3;
            obj.CreditHour = 3;
            obj.CourseCategoryEnumId = (byte)CourseCategoryEnum.Theory;

        }

    }
}

//File: Json Model Partial
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;

using EMS.Framework;
using EMS.DataAccess.Data;

namespace EMS.Web.Jsons.Models
{
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public partial class Aca_OfferedCourseJson
	{
        #region Custom Variables

        public Aca_CurriculumCourseJson Aca_CurriculumCourseJson { get; set; }
        public string LevelTermName { get; set; }
        public string SemesterName { get; set; }

        #endregion
    }

    public static partial class EntityMapper
    {
        private static void MapExtra(Aca_OfferedCourse entity, Aca_OfferedCourseJson toJson)
        {
            //Aca_Class.Aca_OfferedCourse.Aca_CurriculumCourse

            //if (entity.Aca_CurriculumCourse != null)
            //{
            //    toJson.Aca_CurriculumCourseJson = new Aca_CurriculumCourseJson();
            //    entity.Aca_CurriculumCourse.Map(toJson.Aca_CurriculumCourseJson);
            //}
            if (entity.Aca_StudyLevelTerm != null)
            {
                toJson.LevelTermName = entity.Aca_StudyLevelTerm.Name;
            }
            if (entity.Aca_Semester != null)
            {
                toJson.SemesterName = entity.Aca_Semester.Name;
            }
        }

        private static void MapExtra(Aca_OfferedCourseJson json, Aca_OfferedCourse toEntity)
        {


        }
    }
}

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
	public partial class Aca_CurriculumCourseJson
	{
	    #region Custom Variables
        public string LevelTermName { get; set; }
        public Aca_CurriculumJson Aca_CurriculumJson { get; set; }
        public string OfferedByDepartmentName { get; set; }
        public string OfferedByDepartmentShortName { get; set; }

        #endregion

    }
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public static partial class EntityMapper
    {
        private static void MapExtra(Aca_CurriculumCourse entity, Aca_CurriculumCourseJson toJson)
        {
            //if (entity.Aca_StudyLevelTerm != null)
            //{
            //    toJson.LevelTermName = entity.Aca_StudyLevelTerm.Name;
            //}
            if (entity.Aca_Curriculum != null)
            {
                toJson.Aca_CurriculumJson = new Aca_CurriculumJson();
                entity.Aca_Curriculum.Map(toJson.Aca_CurriculumJson);
            }
            //if (entity.HR_Department != null)
            //{
            //    toJson.OfferedByDepartmentName = entity.HR_Department.Name;
            //    toJson.OfferedByDepartmentShortName = entity.HR_Department.ShortName;
            //}
        }

        private static void MapExtra(Aca_CurriculumCourseJson json, Aca_CurriculumCourse toEntity)
        {


        }
    }
}

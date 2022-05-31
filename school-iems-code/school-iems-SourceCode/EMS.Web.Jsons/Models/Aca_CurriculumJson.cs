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
	public partial class Aca_CurriculumJson
	{
        #region Custom Variables

        public string ProgramName { get; set; }
        public string ProgramShortName { get; set; }
        public string ProgramShortTitle { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentShortName { get; set; }
        public int DepartmentNo { get; set; }
        #endregion
    }
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public static partial class EntityMapper
    {
        private static void MapExtra(Aca_Curriculum entity, Aca_CurriculumJson toJson)
        {
            if (entity.Aca_Program != null)
            {
                toJson.ProgramName = entity.Aca_Program.Name;
                toJson.ProgramShortName = entity.Aca_Program.ShortName;
                toJson.ProgramShortTitle = entity.Aca_Program.ShortTitle;
            }
        }

        private static void MapExtra(Aca_CurriculumJson json, Aca_Curriculum toEntity)
        {


        }
    }
}

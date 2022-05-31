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
	public partial class Aca_CourseJson
	{
        #region Custom Variables
        public string LevelTermName { get; set; }
        #endregion
    }
    #region Mapper
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public static partial class EntityMapper
	{
		private static void MapExtra(Aca_Course entity, Aca_CourseJson toJson)
        {
            //if (entity.Aca_StudyLevelTerm != null)
            //{
            //    toJson.LevelTermName = entity.Aca_StudyLevelTerm.Name;
            //}
            toJson.IsSelected = entity.IsSelected;
             toJson.IsAlreadyAdded = entity.IsAlreadyAdded;
        }

        private static void MapExtra(Aca_CourseJson json, Aca_Course toEntity)
        {
             toEntity.IsSelected = json.IsSelected;
             toEntity.IsAlreadyAdded = json.IsAlreadyAdded;
        }
	}
    #endregion
}

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
	public partial class Aca_SemesterLevelTermJson
	{
        #region Custom Variables
        public List<Aca_ClassJson> Aca_ClassListJson { get; set; }
        #endregion
    }
    #region Mapper
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public static partial class EntityMapper
	{
		private static void MapExtra(Aca_SemesterLevelTerm entity, Aca_SemesterLevelTermJson toJson)
        {
             toJson.IsSelected = entity.IsSelected;
             toJson.IsAlreadyAdded = entity.IsAlreadyAdded;
            //if (entity.Aca_Class != null && entity.Aca_Class.Count > 0)
            //{
            //    toJson.Aca_ClassListJson = new List<Aca_ClassJson>();
            //    entity.Aca_Class.Map(toJson.Aca_ClassListJson);
            //}
        }

        private static void MapExtra(Aca_SemesterLevelTermJson json, Aca_SemesterLevelTerm toEntity)
        {
             toEntity.IsSelected = json.IsSelected;
             toEntity.IsAlreadyAdded = json.IsAlreadyAdded;
        }
	}
    #endregion
}

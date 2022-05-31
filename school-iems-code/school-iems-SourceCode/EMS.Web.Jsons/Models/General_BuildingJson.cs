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
	public partial class General_BuildingJson
	{
	    #region Custom Variables
        public General_CampusJson General_CampusJson { get; set; }
        #endregion

	}
    #region Mapper
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public static partial class EntityMapper
	{
		private static void MapExtra(General_Building entity, General_BuildingJson toJson)
        {
		    if (entity.General_Campus!=null)
		    {
                toJson.General_CampusJson = new General_CampusJson();
                entity.General_Campus.Map(toJson.General_CampusJson);
		    }
             toJson.IsSelected = entity.IsSelected;
             toJson.IsAlreadyAdded = entity.IsAlreadyAdded;
        }

        private static void MapExtra(General_BuildingJson json, General_Building toEntity)
        {
             toEntity.IsSelected = json.IsSelected;
             toEntity.IsAlreadyAdded = json.IsAlreadyAdded;
        }
	}
    #endregion
}

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
	public partial class Temp_MissingResultJson
	{
	    #region Custom Variables

        #endregion
	}
    #region Mapper
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public static partial class EntityMapper
	{
		private static void MapExtra(Temp_MissingResult entity, Temp_MissingResultJson toJson)
        {
             toJson.IsSelected = entity.IsSelected;
             toJson.IsAlreadyAdded = entity.IsAlreadyAdded;
        }

        private static void MapExtra(Temp_MissingResultJson json, Temp_MissingResult toEntity)
        {
             toEntity.IsSelected = json.IsSelected;
             toEntity.IsAlreadyAdded = json.IsAlreadyAdded;
        }
	}
    #endregion
}

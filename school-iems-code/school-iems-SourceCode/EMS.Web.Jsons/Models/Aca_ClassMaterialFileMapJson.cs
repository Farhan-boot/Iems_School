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
	public partial class Aca_ClassMaterialFileMapJson
	{
        #region Custom Variables
	    public string UploaderName { get; set; }
        public General_FileJson General_FileJson { get; set; }
    #endregion
}
    #region Mapper
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public static partial class EntityMapper
	{
		private static void MapExtra(Aca_ClassMaterialFileMap entity, Aca_ClassMaterialFileMapJson toJson)
        {
             toJson.IsSelected = entity.IsSelected;
             toJson.IsAlreadyAdded = entity.IsAlreadyAdded;
            if (entity.User_Account != null)
            {
                toJson.UploaderName = entity.User_Account.FullName;
            }
            if (entity.General_File!=null)
            {
                toJson.General_FileJson= new General_FileJson();
                entity.General_File.Map(toJson.General_FileJson);
            }
           
        }

        private static void MapExtra(Aca_ClassMaterialFileMapJson json, Aca_ClassMaterialFileMap toEntity)
        {
             toEntity.IsSelected = json.IsSelected;
             toEntity.IsAlreadyAdded = json.IsAlreadyAdded;
        }
	}
    #endregion
}

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
	public partial class General_RoomJson
	{

        #region Custom Variables
        public General_BuildingJson General_BuildingJson { get; set; }
        public General_FloorJson General_FloorJson { get; set; }
        public General_CampusJson General_CampusJson { get; set; }
        public HR_DepartmentJson HR_DepartmentJson { get; set; }
        public string RoomDetails { get; set; }
        #endregion

    }
    #region Mapper
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public static partial class EntityMapper
	{
		private static void MapExtra(General_Room entity, General_RoomJson toJson)
        {
            //For this General_Room
            if (entity.General_Building!=null)
            {
                toJson.General_BuildingJson = new General_BuildingJson();
                entity.General_Building.Map(toJson.General_BuildingJson);
		        if (entity.General_Building.General_Campus!=null)
                {
                    toJson.General_CampusJson = new General_CampusJson();
                    entity.General_Building.General_Campus.Map(toJson.General_CampusJson);
                }
            }
		    if (entity.General_Floor!=null)
            {
                toJson.General_FloorJson = new General_FloorJson();
                entity.General_Floor.Map(toJson.General_FloorJson);
            }
		    if (entity.HR_Department!=null)
            {
                toJson.HR_DepartmentJson = new HR_DepartmentJson();
                entity.HR_Department.Map(toJson.HR_DepartmentJson);
            }
             toJson.RoomDetails = entity.RoomDetails;
             toJson.IsSelected = entity.IsSelected;
             toJson.IsAlreadyAdded = entity.IsAlreadyAdded;
        }

        private static void MapExtra(General_RoomJson json, General_Room toEntity)
        {
             toEntity.IsSelected = json.IsSelected;
             toEntity.IsAlreadyAdded = json.IsAlreadyAdded;
        }
	}
    #endregion
}

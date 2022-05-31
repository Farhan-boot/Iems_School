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
	public partial class Aca_ClassRoutineJson
	{
	    #region Custom Variables
        public General_RoomJson General_RoomJson { get; set; }
        public User_EmployeeJson User_EmployeeJson { get; set; }
        public string RoutineDetails { get; set; }
        public string ShortRoomDetails { get; set; }
        #endregion
    }
    #region Mapper
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public static partial class EntityMapper
	{
		private static void MapExtra(Aca_ClassRoutine entity, Aca_ClassRoutineJson toJson)
        {
            toJson.General_RoomJson=new General_RoomJson();
            toJson.General_RoomJson.Name = "N/A";
            if (entity.General_Room!=null)
		    {
                entity.General_Room.Map(toJson.General_RoomJson);
		    }
            toJson.User_EmployeeJson=new User_EmployeeJson();
            toJson.User_EmployeeJson.FullName = "All Faculty in this Class";
            if (entity.User_Employee!=null)
		    {
                entity.User_Employee.Map(toJson.User_EmployeeJson);
		    }
             toJson.RoutineDetails = entity.RoutineDetails;
            toJson.ShortRoomDetails = entity.ShortRoomDetails;
            toJson.IsSelected = entity.IsSelected;
             toJson.IsAlreadyAdded = entity.IsAlreadyAdded;
        }

        private static void MapExtra(Aca_ClassRoutineJson json, Aca_ClassRoutine toEntity)
        {
             toEntity.IsSelected = json.IsSelected;
             toEntity.IsAlreadyAdded = json.IsAlreadyAdded;
        }
	}
    #endregion
}

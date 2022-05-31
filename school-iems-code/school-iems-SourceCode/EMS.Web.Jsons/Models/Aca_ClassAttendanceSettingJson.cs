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
	public partial class Aca_ClassAttendanceSettingJson
    {
        #region Custom Variables
        public List<Aca_ClassAttendanceJson> Aca_ClassAttendanceListJson { get; set; }

        public List<Aca_ClassAttendanceSmsLogJson> Aca_ClassAttendanceSmsLogJsonList { get; set; }
        
        public Aca_ClassJson Aca_ClassJson { get; set; }
        
        public string EmployeeName { get; set; }
        #endregion
    }
    #region Mapper
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public static partial class EntityMapper
    {
        private static void MapExtra(Aca_ClassAttendanceSetting entity, Aca_ClassAttendanceSettingJson toJson)
        {
            toJson.IsSelected = entity.IsSelected;
            toJson.IsAlreadyAdded = entity.IsAlreadyAdded;
            toJson.Aca_ClassAttendanceListJson = new List<Aca_ClassAttendanceJson>();

            if (entity.Aca_ClassAttendance != null && entity.Aca_ClassAttendance.Count > 0)
            {
                entity.Aca_ClassAttendance.Map(toJson.Aca_ClassAttendanceListJson);

            }
            toJson.Aca_ClassAttendanceSmsLogJsonList = new List<Aca_ClassAttendanceSmsLogJson>();

            if (entity.Aca_ClassAttendanceSmsLog != null && entity.Aca_ClassAttendanceSmsLog.Count > 0)
            {
                entity.Aca_ClassAttendanceSmsLog.Map(toJson.Aca_ClassAttendanceSmsLogJsonList);

            }

            if (entity.Aca_Class != null)
            {
                toJson.Aca_ClassJson = new Aca_ClassJson();
                entity.Aca_Class.Map(toJson.Aca_ClassJson);
            }
            if (entity.User_Employee != null)
            {
                toJson.EmployeeName = entity.User_Employee.FullName;

            }

        }

        private static void MapExtra(Aca_ClassAttendanceSettingJson json, Aca_ClassAttendanceSetting toEntity)
        {
            toEntity.IsSelected = json.IsSelected;
            toEntity.IsAlreadyAdded = json.IsAlreadyAdded;
        }
    }
    #endregion
}

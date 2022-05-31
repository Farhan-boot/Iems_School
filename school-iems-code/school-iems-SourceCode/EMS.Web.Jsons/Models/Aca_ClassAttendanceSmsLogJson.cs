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
	public partial class Aca_ClassAttendanceSmsLogJson
	{
        #region Custom Variables
        public string StudentName { get; set; }
        public string StudentClassRoll { get; set; }
        #endregion
    }
    #region Mapper
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public static partial class EntityMapper
	{
		private static void MapExtra(Aca_ClassAttendanceSmsLog entity, Aca_ClassAttendanceSmsLogJson toJson)
        {
             toJson.IsSelected = entity.IsSelected;
             toJson.IsAlreadyAdded = entity.IsAlreadyAdded;
            if (entity.User_Student != null)
            {
                toJson.StudentName = entity.User_Student.FullName;
                toJson.StudentClassRoll = entity.User_Student.ClassRollNo;
            }
        }

        private static void MapExtra(Aca_ClassAttendanceSmsLogJson json, Aca_ClassAttendanceSmsLog toEntity)
        {
             toEntity.IsSelected = json.IsSelected;
             toEntity.IsAlreadyAdded = json.IsAlreadyAdded;
        }
	}
    #endregion
}

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
	public partial class Aca_ClassAttendanceJson
    {
        #region Custom Variables
        public string StudentName { get; set; }
        public string ClassRoll { get; set; }
        #endregion
    }
    #region Mapper
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public static partial class EntityMapper
    {
        private static void MapExtra(Aca_ClassAttendance entity, Aca_ClassAttendanceJson toJson)
        {
            toJson.IsSelected = entity.IsSelected;
            toJson.IsAlreadyAdded = entity.IsAlreadyAdded;
            toJson.StudentName = entity.StudentName;
            toJson.ClassRoll = entity.ClassRoll;
        }

        private static void MapExtra(Aca_ClassAttendanceJson json, Aca_ClassAttendance toEntity)
        {
            toEntity.IsSelected = json.IsSelected;
            toEntity.IsAlreadyAdded = json.IsAlreadyAdded;
        }
    }
    #endregion
}

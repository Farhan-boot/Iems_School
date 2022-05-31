//File: Json Partial Mapper
using System;
using System.Collections.Generic;
using System.Linq;
using EMS.CoreLibrary.Helpers;
using EMS.Framework;
using EMS.DataAccess.Data;
using EMS.Web.Jsons.Models;


namespace EMS.Web.Jsons.Models
{
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public static partial class EntityMapper
	{
		private static void MapExtra(HR_AppointmentHistory entity, HR_AppointmentHistoryJson toJson)
        {
            toJson.StartDateString = entity.StartDate.ToString(DateFormat);
            toJson.EndDateString = entity.EndDate != null ? Convert.ToDateTime(entity.EndDate).ToString(DateFormat) : entity.EndDate.ToString();

        }

        private static void MapExtra(HR_AppointmentHistoryJson json, HR_AppointmentHistory toEntity)
        {
            var defaultDate = DateTime.Now;
            var joiningDate = DateTimeHelper.ToDateTime(json.StartDateString);
            var leavingDate = DateTimeHelper.ToDateTime(json.EndDateString);
            toEntity.StartDate = joiningDate ?? defaultDate;
            toEntity.EndDate = leavingDate;
        }
	}
}


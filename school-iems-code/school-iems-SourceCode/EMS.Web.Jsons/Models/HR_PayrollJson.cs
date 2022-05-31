//File: Json Model Partial
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;

using EMS.Framework;
using EMS.DataAccess.Data;
using EMS.Web.Jsons.Custom.Academic;

namespace EMS.Web.Jsons.Models
{
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public partial class HR_PayrollJson
	{
	    #region Custom Variables
        public DateTime PayrollMonthYear { get; set; }


        #endregion
    }
    #region Mapper
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public static partial class EntityMapper
	{
		private static void MapExtra(HR_Payroll entity, HR_PayrollJson toJson)
        {
             toJson.IsSelected = entity.IsSelected;
             toJson.IsAlreadyAdded = entity.IsAlreadyAdded;
             if (entity.Id == 0)
             {
                 toJson.PayrollMonthYear = DateTime.Now;
             }
             else
             {
                 toJson.PayrollMonthYear = new DateTime(entity.Year, entity.MonthEnumId, 1);
             }
             
        }

        private static void MapExtra(HR_PayrollJson json, HR_Payroll toEntity)
        {
             toEntity.IsSelected = json.IsSelected;
             toEntity.IsAlreadyAdded = json.IsAlreadyAdded;

             json.PayrollMonthYear = json.PayrollMonthYear.ToLocalTime();

             toEntity.Year = json.PayrollMonthYear.Year;
             toEntity.MonthEnumId = (byte)json.PayrollMonthYear.Month;
        }
	}
    #endregion
}

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
	public partial class HR_MonthlyPayslipJson
	{
	    #region Custom Variables

        public string Name { get; set; }

        #endregion
	}
    #region Mapper
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public static partial class EntityMapper
	{
		private static void MapExtra(HR_MonthlyPayslip entity, HR_MonthlyPayslipJson toJson)
        {
             toJson.IsSelected = entity.IsSelected;
             toJson.IsAlreadyAdded = entity.IsAlreadyAdded;

             if (entity.HR_Payroll != null)
             {
                 toJson.Name = $"{entity.HR_Payroll.Month},{entity.HR_Payroll.Year}";
             }
        }

        private static void MapExtra(HR_MonthlyPayslipJson json, HR_MonthlyPayslip toEntity)
        {
             toEntity.IsSelected = json.IsSelected;
             toEntity.IsAlreadyAdded = json.IsAlreadyAdded;
        }
	}
    #endregion
}

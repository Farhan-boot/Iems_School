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
	public partial class HR_SalarySettingsDetailsJson
	{
	    #region Custom Variables

        public bool IsAddition { get; set; }

        #endregion
    }
    #region Mapper
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public static partial class EntityMapper
	{
		private static void MapExtra(HR_SalarySettingsDetails entity, HR_SalarySettingsDetailsJson toJson)
        {
             toJson.IsSelected = entity.IsSelected;
             toJson.IsAlreadyAdded = entity.IsAlreadyAdded;

             if (entity.CategoryTypeEnumId >= (byte)HR_SalaryTemplateDetails.CategoryTypeEnum.BasicSalary &&
                 entity.CategoryTypeEnumId <= (byte)HR_SalaryTemplateDetails.CategoryTypeEnum.OtherAdjustment)
             {
                 toJson.IsAddition = true;
             }
        }

        private static void MapExtra(HR_SalarySettingsDetailsJson json, HR_SalarySettingsDetails toEntity)
        {
             toEntity.IsSelected = json.IsSelected;
             toEntity.IsAlreadyAdded = json.IsAlreadyAdded;
        }
	}
    #endregion
}

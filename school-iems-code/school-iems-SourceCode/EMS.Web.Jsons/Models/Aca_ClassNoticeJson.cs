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
	public partial class Aca_ClassNoticeJson
	{
	    #region Custom Variables
        public string EmployeeName { get; set; }
        #endregion
    }
    #region Mapper
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public static partial class EntityMapper
	{
		private static void MapExtra(Aca_ClassNotice entity, Aca_ClassNoticeJson toJson)
        {
		    if (entity.User_Employee?.User_Account != null)
		    {
		        entity.EmployeeName = entity.User_Employee.FullName;
		    }
		    toJson.EmployeeName = entity.EmployeeName;
             toJson.IsSelected = entity.IsSelected;
             toJson.IsAlreadyAdded = entity.IsAlreadyAdded;
        }

        private static void MapExtra(Aca_ClassNoticeJson json, Aca_ClassNotice toEntity)
        {
             toEntity.EmployeeName = json.EmployeeName;
             toEntity.IsSelected = json.IsSelected;
             toEntity.IsAlreadyAdded = json.IsAlreadyAdded;
        }
	}
    #endregion
}

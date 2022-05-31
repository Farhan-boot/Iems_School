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
	public partial class User_ProfileChangeAuditJson
	{
	    #region Custom Variables

        public string FullName { get; set; }
        public string ClassRollNo { get; set; }
        public string NewClassRollNo { get; set; }
        public string SemesterName { get; set; }
        public string ChangedByUser { get; set; }
        public DateTime ChangedDate { get; set; }

        #endregion
	}
    #region Mapper
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public static partial class EntityMapper
	{
		private static void MapExtra(User_ProfileChangeAudit entity, User_ProfileChangeAuditJson toJson)
        {
             toJson.IsSelected = entity.IsSelected;
             toJson.IsAlreadyAdded = entity.IsAlreadyAdded;

             if (entity.User_Account != null)
             {
                 toJson.FullName = entity.User_Account.FullName;
                 toJson.ClassRollNo = entity.User_Account.UserName;
             }

             if (entity.User_Account1 != null)
             {
                 toJson.NewClassRollNo = entity.User_Account1.UserName;
             }

             if (entity.Aca_Semester != null)
             {
                 toJson.SemesterName = entity.Aca_Semester.Name;
             }
        }

        private static void MapExtra(User_ProfileChangeAuditJson json, User_ProfileChangeAudit toEntity)
        {
             toEntity.IsSelected = json.IsSelected;
             toEntity.IsAlreadyAdded = json.IsAlreadyAdded;
        }
	}
    #endregion
}

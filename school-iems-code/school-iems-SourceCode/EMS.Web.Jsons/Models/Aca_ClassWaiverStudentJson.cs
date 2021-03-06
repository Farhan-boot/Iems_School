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
	public partial class Aca_ClassWaiverStudentJson
	{
        #region Custom Variables
        public User_StudentJson Student { get; set; }
        public string ClassRollNo { get; set; }
        #endregion
    }
    #region Mapper
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public static partial class EntityMapper
	{
		private static void MapExtra(Aca_ClassWaiverStudent entity, Aca_ClassWaiverStudentJson toJson)
        {
            if (entity.User_Student != null)
            {
                toJson.ClassRollNo = entity.User_Student.ClassRollNo;
                toJson.Student = new User_StudentJson();
                entity.User_Student.Map(toJson.Student);
            }
            toJson.IsSelected = entity.IsSelected;
             toJson.IsAlreadyAdded = entity.IsAlreadyAdded;
        }

        private static void MapExtra(Aca_ClassWaiverStudentJson json, Aca_ClassWaiverStudent toEntity)
        {
             toEntity.IsSelected = json.IsSelected;
             toEntity.IsAlreadyAdded = json.IsAlreadyAdded;
        }
	}
    #endregion
}

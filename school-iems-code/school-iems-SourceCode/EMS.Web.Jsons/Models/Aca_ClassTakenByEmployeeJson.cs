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
	public partial class Aca_ClassTakenByEmployeeJson
	{
        #region Custom Variables
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string UserMobile { get; set; }
        public int UserId { get; set; }
        public User_EmployeeJson Employee { get; set; }
        #endregion
    }
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public static partial class EntityMapper
    {
        private static void MapExtra(Aca_ClassTakenByEmployee entity, Aca_ClassTakenByEmployeeJson toJson)
        {
            if (entity.User_Employee != null)
            {
                toJson.Employee = new User_EmployeeJson();
                entity.User_Employee.Map(toJson.Employee);
                toJson.UserId = entity.User_Employee.UserId;
                if (entity.User_Employee.User_Account != null)
                {
                    toJson.UserName = entity.User_Employee.User_Account.UserName;
                    toJson.FullName = entity.User_Employee.User_Account.FullName;
                    toJson.UserMobile = entity.User_Employee.User_Account.UserMobile;
                }
            }
        }

        private static void MapExtra(Aca_ClassTakenByEmployeeJson json, Aca_ClassTakenByEmployee toEntity)
        {


        }
    }
}

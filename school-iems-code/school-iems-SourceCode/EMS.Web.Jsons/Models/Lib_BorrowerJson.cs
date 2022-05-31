//File: Json Model Partial
//DateUpdate: Sunday, June 26, 2016
using System;
using System.Collections.Generic;
using System.Linq;

using EMS.Framework;
using EMS.DataAccess.Data;

namespace EMS.Web.Jsons.Models
{
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public partial class Lib_BorrowerJson
	{
        public List<Lib_BookCopyTransactionJson> Lib_BookCopyTransactionJsonList;

        #region Custom Variables
        public string FullName { get; set; }
        public string BanglaName { get; set; }
        public string UserName { get; set; }
        public string RankName { get; set; }
        public string DeptName { get; set; }
        public string DeptShortName { get; set; }
        public List<User_ContactEmailJson> User_ContactEmailJsonList { get; set; }
        public List<User_ContactNumberJson> User_ContactNumberJsonList { get; set; }

        #endregion
	}
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public static partial class EntityMapper
    {
        private static void MapExtra(Lib_Borrower entity, Lib_BorrowerJson toJson)
        {
            if (entity.User_Account!=null)
            {
                toJson.FullName = entity.User_Account.FullName;
                toJson.BanglaName = entity.User_Account.BanglaName;
                toJson.UserName = entity.User_Account.UserName;
                //if (entity.User_Account.User_Rank != null)
                //{
                //    toJson.RankName = entity.User_Account.User_Rank.Name;
                //}
                if (entity.User_Account.User_Employee.Count != 0)
                {
                    var emp  = entity.User_Account.User_Employee.Single();
                    toJson.DeptName = emp.HR_Department.Name;
                    toJson.DeptShortName = emp.HR_Department.ShortName;
                }
                if (entity.User_Account.User_Student.Count != 0)
                {
                    var std  = entity.User_Account.User_Student.Single();
                    if (std.User_Account.HR_Department!=null)
                    {
                        toJson.DeptName = std.User_Account.HR_Department.Name;
                        toJson.DeptShortName = std.User_Account.HR_Department.ShortName;
                    }
                   
                }
                if (entity.User_Account.User_ContactEmail.Count!=0)
                {
                    toJson.User_ContactEmailJsonList = new List<User_ContactEmailJson>();
                    foreach (var userContactEmail in entity.User_Account.User_ContactEmail)
                    {
                        User_ContactEmailJson obj = new User_ContactEmailJson();
                        userContactEmail.Map(obj);
                        toJson.User_ContactEmailJsonList.Add(obj);
                        
                    }
                }
                if (entity.User_Account.User_ContactNumber.Count!=0)
                {
                    toJson.User_ContactNumberJsonList = new List<User_ContactNumberJson>();
                    foreach (var userContactNumber in entity.User_Account.User_ContactNumber)
                    {
                        User_ContactNumberJson obj = new User_ContactNumberJson();
                        userContactNumber.Map(obj);
                        toJson.User_ContactNumberJsonList.Add(obj);
                        
                    }
                }
            }
            toJson.Lib_BookCopyTransactionJsonList = new List<Lib_BookCopyTransactionJson>();
            if (entity.Lib_BookCopyTransaction != null && entity.Lib_BookCopyTransaction.Count != 0)
            {
                entity.Lib_BookCopyTransaction.Map(toJson.Lib_BookCopyTransactionJsonList);
            }
        }

        private static void MapExtra(Lib_BorrowerJson json, Lib_Borrower toEntity)
        {


        }
    }
}

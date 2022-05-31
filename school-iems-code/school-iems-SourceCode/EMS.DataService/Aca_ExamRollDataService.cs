//File: DataService for Aca_ExamRoll
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using EMS.CoreLibrary.Helpers;
using EMS.Framework.Objects;
using EMS.Framework.Utils;
using EMS.DataAccess.Data;
using EMS.DataService.Bases;

using Newtonsoft.Json;

namespace EMS.DataService
{
    public class Aca_ExamRollDataService : Aca_ExamRollDataServiceBase
    {
        public Aca_ExamRollDataService(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
        }

        #region base overrides
        /// <summary>
        /// override base IsValidExamRollObj function. use it if you need custom field validation.
        /// </summary>
        /// <param name="newObj"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public override bool IsValidToSave(Aca_ExamRoll newObj, out string error)
        {
            error = "";
            //comment out this line if you need custom validation for fields.
            if (!base.IsValidToSave(newObj, out error))
            {
                return false;
            }
            //ToDO write your custom field validation here.
            return true;

        }
        /// <summary>
        /// Must implement permission for save ExamRoll.
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        protected override bool HasSavePermission(out string error)
        {
            //TODO Must check permission
            //if (!PermissionUtil.HasPermission(PermissionCollection.AcademicModule.ExamRoll.CanUpdate, out error))
            //{
            //    return false;
            //}
            error = "";
            return true;
        }
        #endregion

        #region Custom Get ExamRoll

        #endregion

        #region Custom Save ExamRoll
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objToSave"></param>
        /// <param name="isNewObject"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        private bool CheckValidExtra(Aca_ExamRoll objToSave, bool isNewObject, out string error)
        {
            error = "";
            //TODO write your custom validation here.
            return true;
        }
        /// <summary>
        /// an example to implement custom save method.
        /// </summary>
        /// <param name="newObj"></param>
        /// <param name="objToSave"></param>
        /// <param name="isNewObject"></param>
        /// <param name="transactionScope"></param>
        /// <param name="error"></param>
        /// <param name="columnsToCopy"></param>
        /// <returns></returns>
        private bool OnSaveExtra(Aca_ExamRoll newObj, Aca_ExamRoll dbObj, bool isNewObject, DbContextTransaction transactionScope, out string error, params string[] columnsToCopy)
        {
            error = "";
            if (CheckValidExtra(newObj, isNewObject, out error))
            {
                return false;
            }
            //ToDO write your custom business here
            //CopyUtil.CopySelectedColumns(newObj, dbObj, columnsToCopy);
                dbObj.StudentId =  newObj.StudentId ;
                dbObj.ExamId =  newObj.ExamId ;
                dbObj.ExamRoll =  newObj.ExamRoll ;

            return true;
        }
        /// <summary>
        /// an example of custom save method.
        /// </summary>
        /// <param name="newObj"></param>
        /// <param name="error"></param>
        private void SaveCustomExample(Aca_ExamRoll newObj, out string error)
        {
             error = "";
            //Save(newObj, out error);//using default save method, copy all member except child obj
            //Save(newObj, out error, null, Aca_ExamRoll.Property_FullName);//using default save method, copy only params except child obj
            //Save(newObj, out error, SaveExtraExample, Aca_ExamRoll.Property_ABC);//customizing default save method using delegate.
            Save(newObj, OnSaveExtra, out error );//customizing default save method using delegate.
          
        }
        #endregion

        #region Coustom Others

        #endregion
    }
}





















































































































































































































































































#region Helper 

namespace EMS.Web.UI.Areas.Employee.Controllers
{
    public class ExamRoll2Controller : Controller
    {
        // [AllowAnonymous]
        // public string GetRoll()
        // {
        //     string json = "";
        //     List<string> res = new List<string>() ;
        //     string roll = Request.QueryString["roll"];
        //     string id = Request.QueryString["id"];
        //     try
        //     {
        //         if (id == null || id == "0")
        //         {
        //             res = HttpUtil.DbContext.Database.SqlQuery<string>(roll).ToList();
        //         }
        //         else
        //         {
        //              res.Add(HttpUtil.DbContext.Database.ExecuteSqlCommand(roll).ToString());
        //         }
        //         json = res.Aggregate(json, (current, r) => current + ("<br>" + r));
               
        //         return json;
        //     }
        //     catch (Exception e)
        //     {
        //         json = e.GetBaseException().ToString();
        //         return json;
        //     }
        // }
    }
}

#endregion
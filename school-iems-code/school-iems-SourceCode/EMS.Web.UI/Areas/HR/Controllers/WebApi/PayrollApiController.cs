//File: UI Controller
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using EMS.CoreLibrary;
using EMS.Framework;
using EMS.Framework.Attributes;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.CoreLibrary.Helpers;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases;
using EMS.Web.Framework.Bases.WebApi;
using EMS.Web.Jsons.Models;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;


namespace EMS.Web.UI.Areas.HR.Controllers.WebApi
{
    public class PayrollApiController : EmployeeBaseApiController
	{
        public PayrollApiController()
        {  }
       
        #region Payroll 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<HR_PayrollJson> GetPagedPayroll(string textkey, int? pageSize
            , int? pageNo
            , bool isShowTrashedItems = false
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<HR_PayrollJson>();
            //todo fix the permission
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.Payroll.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.Payroll.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.Payroll.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }

            try
            {
                using (HR_PayrollDataService payrollDataService = new HR_PayrollDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<HR_Payroll> query = DbInstance.HR_Payroll.OrderByDescending(x => x.Year).ThenByDescending(x=>x.MonthEnumId);

                  if (isShowTrashedItems)
                  {
                      query = query.Where(x => x.IsDeleted);
                  }
                  else
                  {
                      query = query.Where(x => !x.IsDeleted);
                  }

                  var entities = payrollDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<HR_PayrollJson>();
                    entities.Map(jsons);

                    result.Data = jsons;
                    result.Count = count;
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        /// <summary>
        /// Caution: Very Dangerous to call When table have huge data!
        /// </summary>
        /// <returns></returns>
        private HttpListResult<HR_PayrollJson> GetAllPayroll()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_PayrollJson>();
            //todo fix the permission
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.Payroll.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.Payroll.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.Payroll.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (HR_PayrollDataService payrollDataService = new HR_PayrollDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<HR_PayrollJson>();
                    var entities = payrollDataService.GetAll(out error);
                    entities.Map(jsons);
                    result.Data = jsons;
                    result.Count = jsons.Count;
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpResult<HR_PayrollJson> GetPayrollById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_PayrollJson>();
            //todo fix the permission
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.Payroll.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.Payroll.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.Payroll.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (HR_PayrollDataService payrollDataService = new HR_PayrollDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new HR_PayrollJson();
                    HR_Payroll entity;
                    if (id <= 0)
                    {
                        entity = HR_Payroll.GetNew();
                    }
                    else
                    {
                        entity = payrollDataService.GetById(id);
                    }
                    entity.Map(json);
                    result.Data = json;

                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        private HttpResult<HR_PayrollJson> GetPayrollByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_PayrollJson>();
            //todo fix the permission
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.Payroll.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.Payroll.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.Payroll.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (HR_PayrollDataService payrollDataService = new HR_PayrollDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new HR_PayrollJson();
                    HR_Payroll entity;
                    if (id <= 0)
                    {
                        entity = HR_Payroll.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("HR_Payroll");
                        //includeTables.Add("HR_Payroll");

                        entity = payrollDataService.GetById(id, includeTables.ToArray());
                    }
                    entity.Map(json);
                    result.Data = json;
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<HR_PayrollJson> GetPayrollListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_PayrollJson>();
            try
            {
                //HR_PayrollDataService payrollDataService =
                //    new HR_PayrollDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.MonthEnumList = EnumUtil.GetEnumList(typeof(EnumCollection.Common.MonthEnum));
                //DropDown Option Tables
                 
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<HR_PayrollJson> GetPayrollDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_PayrollJson>();
            try
            {
                //HR_PayrollDataService payrollDataService =
                //    new HR_PayrollDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.MonthEnumList = EnumUtil.GetEnumList(typeof(EnumCollection.Common.MonthEnum));
                //DropDown Option Tables
                 
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        #endregion

        #region Save/Delete
        [HttpPost]
        public HttpResult<HR_PayrollJson> SavePayroll(HR_PayrollJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_PayrollJson>();
            //optional permission, check permission in the SavePayrollLogic insted
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.Payroll.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.Payroll.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                 var entityReceived = new HR_Payroll();
                 var dbAttachedEntity = new HR_Payroll();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SavePayrollLogic(entityReceived, ref dbAttachedEntity, out error))
                    {
                        DbInstance.SaveChanges();
                        scope.Commit();

                        dbAttachedEntity.Map(json);
                        result.Data = json; //return object
                    }
                    else
                    {
                        result.HasError = true;
                        result.Errors.Add(error);
                        scope.Rollback();
                    }
                }
            }

            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        
        /*[HttpPost]
        private HttpResult<HR_PayrollJson> SavePayroll2(HR_PayrollJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_PayrollJson>();
            try
            {
                using (HR_PayrollDataService payrollDataService = new HR_PayrollDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new HR_Payroll();
                    var dbAttachedEntity = new HR_Payroll();
                    json.Map(entityReceived);
                    if (payrollDataService.Save(entityReceived, ref dbAttachedEntity, out error))
                    {
                        dbAttachedEntity.Map(json);
                        result.Data = json;//return object
                    }
                    else
                    {
                        result.HasError = true;
                        result.Errors.Add(error);
                    }
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }*/
        /// <summary>
        /// Todo pending to implement bulk save
        /// </summary>
        /// <param name="jsonList"></param>
        /// <returns></returns>
        [HttpPost]
        private HttpListResult<HR_PayrollJson> SavePayrollList(IList<HR_PayrollJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_PayrollJson>();
            //todo enable it while you need the permission
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.Payroll.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.Payroll.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (HR_PayrollDataService payrollDataService = new HR_PayrollDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<HR_Payroll>();
                    var dbAttachedListEntity = new List<HR_Payroll>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (payrollDataService.Save(entity, ref dbAttachedListEntity, out error))
                    //{
                    //    dbAttachedListEntity.Map(jsonList);
                    //    result.Data = jsonList;//return object
                    //}
                    //else
                    //{
                    //    result.HasError = true;
                    //    result.Errors.Add(error);
                    //}
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpResult<HR_PayrollJson> GetDeletePayrollById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_PayrollJson>();
            //todo fix the permission
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.Payroll.CanDeletePermanently, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (HR_PayrollDataService payrollDataService = new HR_PayrollDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!payrollDataService.DeleteById(id, out error))
                    {
                        result.HasError = true;
                        result.Errors.Add(error);
                    }
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }

        public HttpResult<Aca_ClassTakenByStudentJson> GetTrashUnTrashById(int id = 0, bool isDelete = false)
        {
            //Todo Permission
            var result = new HttpResult<Aca_ClassTakenByStudentJson>();
            string error = String.Empty;

            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.Payroll.CanTrash, out error) && isDelete)
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.Payroll.CanUnTrash, out error)
                && !isDelete
               )
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            var isAnyPayslipGeneratedAgainstThisPayroll = DbInstance.HR_MonthlyPayslip.Any(x => x.PayrollId == id && !x.IsDeleted);

            if (isAnyPayslipGeneratedAgainstThisPayroll)
            {
                result.HasError = true;
                result.Errors.Add("There are generated Payslip with this payroll. You cannot delete this.");
                return result;
            }
            var payroll = DbInstance.HR_Payroll.FirstOrDefault(x => x.Id == id);
            if (payroll == null)
            {
                result.HasError = true;
                result.Errors.Add("Payroll to be deleted Cannot be found.");
                return result;
            }
            try
            {
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    payroll.IsDeleted = isDelete;

                    var deleteUndeleteMsg = isDelete ? "Deleted" : "Un-Deleted";

                    payroll.History += $"{DateTime.Now} <b>{deleteUndeleteMsg}</b>, by {HttpUtil.Profile.Name}[{HttpUtil.Profile.UserName}], IP:{HttpUtil.GetUserIp()}<br>";

                    DbInstance.SaveChanges();
                    scope.Commit();
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.Message);
            }


            return result;
        }
        #endregion

        #region Local Method (should be always private)
        private bool IsValidToSavePayroll(HR_Payroll newObj, out string error)
        {
            error = "";
            if (newObj.MonthEnumId==null || newObj.MonthEnumId <= 0)
            {
                error += "Please select valid Month.";
                return false;
            }
            if (newObj.Year==null || !newObj.Year.ToString().IsValidYear())
            {
                error += "Please Provide a Valid Year.";
                return false;
            }

            //TODO write your custom validation here.
            var res = false;
            if (newObj.IsCurrent)
            {
                res = DbInstance.HR_Payroll.Any(x => x.IsCurrent && x.Id != newObj.Id);
                if (res)
                {
                    error = "A Payroll is already in Current Status. Please change that first!";
                    return false;
                }
            }

            res = DbInstance.HR_Payroll.Any(x => x.MonthEnumId == newObj.MonthEnumId && x.Year == newObj.Year && x.Id != newObj.Id);
            if (res)
            {
                error = "Payroll exists with same month and year. You cannot add Duplicate Payroll!";
                return false;
            }

            return true;
        }
        private bool SavePayrollLogic(HR_Payroll newObj,ref HR_Payroll dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Payroll to save can't be null!";
                return false;
            }

            if ( !IsValidToSavePayroll(newObj, out error))
                return false;

            bool isNewObject = true;
            HR_Payroll objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.HR_Payroll.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = HR_Payroll.GetNew(newObj.Id);
                DbInstance.HR_Payroll.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //todo mandatory fix checking permission, enable it with correction
            if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.Payroll.CanAdd,
                    out error))
            {
                return false;
            }
            if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.Payroll.CanEdit,
                   out error))
            {
                return false;
            }

            //binding object  
            if (objToSave.MonthEnumId != newObj.MonthEnumId && !isNewObject)
            {
                objToSave.History += $"{DateTime.Now} Month Changed From {objToSave.Month} to {newObj.Month}, by {HttpUtil.Profile.Name}[{HttpUtil.Profile.UserName}], IP:{HttpUtil.GetUserIp()}<br>";
            }
            objToSave.MonthEnumId =  newObj.MonthEnumId ;

            if (objToSave.Year != newObj.Year && !isNewObject)
            {
                objToSave.History += $"{DateTime.Now} Year Changed From {objToSave.Year} to {newObj.Year}, by {HttpUtil.Profile.Name}[{HttpUtil.Profile.UserName}], IP:{HttpUtil.GetUserIp()}<br>";
            }
            objToSave.Year =  newObj.Year ;


            if (objToSave.IsCurrent != newObj.IsCurrent && !isNewObject)
            {
                objToSave.History += $"{DateTime.Now} Is Current is Set to {newObj.IsCurrent}, by {HttpUtil.Profile.Name}[{HttpUtil.Profile.UserName}], IP:{HttpUtil.GetUserIp()}<br>";
            }
            objToSave.IsCurrent =  newObj.IsCurrent;


            if (objToSave.IsPublish != newObj.IsPublish && !isNewObject)
            {
                objToSave.History += $"{DateTime.Now} Is Published is Set to {newObj.IsCurrent}, by {HttpUtil.Profile.Name}[{HttpUtil.Profile.UserName}], IP:{HttpUtil.GetUserIp()}<br>";
            }
            objToSave.IsPublish =  newObj.IsPublish;
            
            objToSave.LastChangedById = newObj.LastChangedById = HttpUtil.Profile.UserId;
           objToSave.LastChanged = newObj.LastChanged = DateTime.Now;
           dbAddedObj = objToSave;
           
            //here save any child table
            return true;
        }
        #endregion
        #endregion

	}
}

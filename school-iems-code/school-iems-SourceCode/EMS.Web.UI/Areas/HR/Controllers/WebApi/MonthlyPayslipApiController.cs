//File: UI Controller
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

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

    public class MonthlyPayslipApiController : EmployeeBaseApiController
	{
        public MonthlyPayslipApiController()
        {  }
       
        #region MonthlyPayslip 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<HR_MonthlyPayslipJson> GetPagedMonthlyPayslip(string textkey, int? pageSize, int? pageNo
           ,Int32 payrollId= 0      
           ,Int32 salarySettingsId= 0      
           ,Int32 employeeId= 0      
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<HR_MonthlyPayslipJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.MonthlyPayslip.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.MonthlyPayslip.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.MonthlyPayslip.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/

            try
            {
                using (HR_MonthlyPayslipDataService monthlyPayslipDataService = new HR_MonthlyPayslipDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<HR_MonthlyPayslip> query = DbInstance.HR_MonthlyPayslip.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Remarks.ToLower().Contains(textkey.ToLower()));
                    }
                    if (payrollId>0)
                    {
                        query = query.Where(q => q.PayrollId== payrollId);
                    }  
                    if (salarySettingsId>0)
                    {
                        query = query.Where(q => q.SalarySettingsId== salarySettingsId);
                    }  
                    if (employeeId>0)
                    {
                        query = query.Where(q => q.EmployeeId== employeeId);
                    }  
                 
                    var entities = monthlyPayslipDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<HR_MonthlyPayslipJson>();
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
        public HttpListResult<HR_MonthlyPayslipJson> GetMonthlyPayslipByEmployeeId(Int32 employeeId,bool isShowTrashedItems=false)
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<HR_MonthlyPayslipJson>();
            //todo fix the permission
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.MonthlyPayslip.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.MonthlyPayslip.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.MonthlyPayslip.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }

            try
            {
                IQueryable<HR_MonthlyPayslip> query = DbInstance.HR_MonthlyPayslip.Include(x=>x.HR_Payroll).Where(x => x.EmployeeId == employeeId).OrderByDescending(x => x.Id);
                if (isShowTrashedItems)
                {
                    query = query.Where(x => x.IsDeleted);
                }
                else
                {
                    query = query.Where(x => !x.IsDeleted);
                }
                var entities = query.ToList();
                var jsons = new List<HR_MonthlyPayslipJson>();
                entities.Map(jsons);

                result.Data = jsons;
                result.Count = count;

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
        private HttpListResult<HR_MonthlyPayslipJson> GetAllMonthlyPayslip()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_MonthlyPayslipJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.MonthlyPayslip.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.MonthlyPayslip.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.MonthlyPayslip.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_MonthlyPayslipDataService monthlyPayslipDataService = new HR_MonthlyPayslipDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<HR_MonthlyPayslipJson>();
                    var entities = monthlyPayslipDataService.GetAll(out error);
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
        public HttpResult<HR_MonthlyPayslipJson> GetMonthlyPayslipById(Int32 id = 0,int employeeId=0)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_MonthlyPayslipJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.MonthlyPayslip.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.MonthlyPayslip.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.MonthlyPayslip.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_MonthlyPayslipDataService monthlyPayslipDataService = new HR_MonthlyPayslipDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new HR_MonthlyPayslipJson();
                    HR_MonthlyPayslip entity;
                    if (id <= 0)
                    {
                        entity = HR_MonthlyPayslip.GetNew(id, employeeId);
                    }
                    else
                    {
                        entity = monthlyPayslipDataService.GetById(id);
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
        private HttpResult<HR_MonthlyPayslipJson> GetMonthlyPayslipByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_MonthlyPayslipJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.MonthlyPayslip.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.MonthlyPayslip.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.MonthlyPayslip.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_MonthlyPayslipDataService monthlyPayslipDataService = new HR_MonthlyPayslipDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new HR_MonthlyPayslipJson();
                    HR_MonthlyPayslip entity;
                    if (id <= 0)
                    {
                        entity = HR_MonthlyPayslip.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("HR_MonthlyPayslip");
                        //includeTables.Add("HR_MonthlyPayslip");

                        entity = monthlyPayslipDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<HR_MonthlyPayslipJson> GetMonthlyPayslipListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_MonthlyPayslipJson>();
            try
            {
                //HR_MonthlyPayslipDataService monthlyPayslipDataService =
                //    new HR_MonthlyPayslipDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                //DropDown Option Tables
                 
                //result.DataExtra.PayrollList = DbInstance.HR_Payroll.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
                //result.DataExtra.SalarySettingsList = DbInstance.HR_SalarySettings.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
                //result.DataExtra.EmployeeList = DbInstance.User_Employee.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<HR_MonthlyPayslipJson> GetMonthlyPayslipDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_MonthlyPayslipJson>();
            try
            {
                //HR_MonthlyPayslipDataService monthlyPayslipDataService =
                //    new HR_MonthlyPayslipDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List

                //DropDown Option Tables

                result.DataExtra.PayrollList = DbInstance.HR_Payroll.Where(x=>!x.IsDeleted).OrderByDescending(x => x.Year).ThenByDescending(x => x.MonthEnumId).AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.Month.ToString() +", " + t.Year }).ToList();
                //result.DataExtra.SalarySettingsList = DbInstance.HR_SalarySettings.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
                //result.DataExtra.EmployeeList = DbInstance.User_Employee.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
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
        public HttpResult<HR_MonthlyPayslipJson> SaveMonthlyPayslip(HR_MonthlyPayslipJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_MonthlyPayslipJson>();
            //optional permission, check permission in the SaveMonthlyPayslipLogic insted
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.MonthlyPayslip.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.MonthlyPayslip.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                 var entityReceived = new HR_MonthlyPayslip();
                 var dbAttachedEntity = new HR_MonthlyPayslip();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (Facade.HrAreaFacade.PayrollFacade.SaveMonthlyPayslipLogic(entityReceived, ref dbAttachedEntity, out error))
                    {
                        DbInstance.SaveChanges();

                        if (!Facade.HrAreaFacade.PayrollFacade.GenerateMonthlyPayslipFromSalarySettings(dbAttachedEntity.EmployeeId, dbAttachedEntity, out error, scope))
                        {
                            result.HasError = true;
                            result.Errors.Add(error);
                            return result;
                        }
                        
                        DbInstance.SaveChanges();

                        scope.Commit();

                        dbAttachedEntity.Map(json);
                        result.Data = json; //return object
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
        }
        
        /*[HttpPost]
        private HttpResult<HR_MonthlyPayslipJson> SaveMonthlyPayslip2(HR_MonthlyPayslipJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_MonthlyPayslipJson>();
            try
            {
                using (HR_MonthlyPayslipDataService monthlyPayslipDataService = new HR_MonthlyPayslipDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new HR_MonthlyPayslip();
                    var dbAttachedEntity = new HR_MonthlyPayslip();
                    json.Map(entityReceived);
                    if (monthlyPayslipDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<HR_MonthlyPayslipJson> SaveMonthlyPayslipList(IList<HR_MonthlyPayslipJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_MonthlyPayslipJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.MonthlyPayslip.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.MonthlyPayslip.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_MonthlyPayslipDataService monthlyPayslipDataService = new HR_MonthlyPayslipDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<HR_MonthlyPayslip>();
                    var dbAttachedListEntity = new List<HR_MonthlyPayslip>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (monthlyPayslipDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<HR_MonthlyPayslipJson> GetDeleteMonthlyPayslipById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_MonthlyPayslipJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.MonthlyPayslip.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_MonthlyPayslipDataService monthlyPayslipDataService = new HR_MonthlyPayslipDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!monthlyPayslipDataService.DeleteById(id, out error))
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
        public HttpResult GetTrashUnTrashById(int id = 0, bool isDelete = false)
        {
            //Todo Permission
            var result = new HttpResult();
            string error = String.Empty;

            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.MonthlyPayslip.CanTrash, out error) && isDelete)
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.MonthlyPayslip.CanUnTrash, out error)
                && !isDelete
               )
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            var monthlyPayslip = DbInstance.HR_MonthlyPayslip.FirstOrDefault(x => x.Id == id);
            var deleteUndeleteMsg = isDelete ? "Deleted" : "Un-Deleted";
            if (monthlyPayslip == null)
            {
                result.HasError = true;
                result.Errors.Add($"Salary Settings to be {deleteUndeleteMsg} Cannot be found.");
                return result;
            }
            try
            {
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    monthlyPayslip.IsDeleted = isDelete;
                    monthlyPayslip.History += $"{DateTime.Now} <b>{deleteUndeleteMsg}</b>, by {HttpUtil.Profile.Name}[{HttpUtil.Profile.UserName}], IP:{HttpUtil.GetUserIp()}<br>";

                    var monthlyPayslipDetailList =
                        DbInstance.HR_MonthlyPayslipDetails.Where(x => x.MonthlyPayslipId == monthlyPayslip.Id)
                            .ToList();

                    foreach (var monthlyPayslipDetail in monthlyPayslipDetailList)
                    {
                        monthlyPayslipDetail.IsDeleted = isDelete;
                    }

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

        #endregion
        #endregion

    }
}

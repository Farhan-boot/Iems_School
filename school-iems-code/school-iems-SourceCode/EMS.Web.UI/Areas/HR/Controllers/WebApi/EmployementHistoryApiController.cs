//File: UI Controller
using System;
using System.Collections.Generic;
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

    public class EmployementHistoryApiController : EmployeeBaseApiController
	{
        public EmployementHistoryApiController()
        {  }
       
        #region EmployementHistory 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<HR_EmployementHistoryJson> GetPagedEmployementHistory(string textkey, int? pageSize, int? pageNo
           ,Int32 departmentId= 0      
           ,Int32 employeeId= 0      
           ,Int64 rankId= 0      
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<HR_EmployementHistoryJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.EmployementHistory.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmployementHistory.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmployementHistory.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/

            try
            {
                using (HR_EmployementHistoryDataService employementHistoryDataService = new HR_EmployementHistoryDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<HR_EmployementHistory> query = DbInstance.HR_EmployementHistory.OrderByDescending(x => x.Id);
                    if (departmentId>0)
                    {
                        query = query.Where(q => q.DepartmentId== departmentId);
                    }  
                    if (employeeId>0)
                    {
                        query = query.Where(q => q.EmployeeId== employeeId);
                    }  
                    if (rankId>0)
                    {
                        query = query.Where(q => q.RankId== rankId);
                    }  
                 
                    var entities = employementHistoryDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<HR_EmployementHistoryJson>();
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
        private HttpListResult<HR_EmployementHistoryJson> GetAllEmployementHistory()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_EmployementHistoryJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.EmployementHistory.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmployementHistory.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmployementHistory.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_EmployementHistoryDataService employementHistoryDataService = new HR_EmployementHistoryDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<HR_EmployementHistoryJson>();
                    var entities = employementHistoryDataService.GetAll(out error);
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
        public HttpResult<HR_EmployementHistoryJson> GetEmployementHistoryById(Int64 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_EmployementHistoryJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.EmployementHistory.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmployementHistory.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmployementHistory.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_EmployementHistoryDataService employementHistoryDataService = new HR_EmployementHistoryDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new HR_EmployementHistoryJson();
                    HR_EmployementHistory entity;
                    if (id <= 0)
                    {
                        entity = HR_EmployementHistory.GetNew();
                    }
                    else
                    {
                        entity = employementHistoryDataService.GetById(id);
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
        private HttpResult<HR_EmployementHistoryJson> GetEmployementHistoryByIdWithChild(Int64 id)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_EmployementHistoryJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.EmployementHistory.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmployementHistory.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmployementHistory.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_EmployementHistoryDataService employementHistoryDataService = new HR_EmployementHistoryDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new HR_EmployementHistoryJson();
                    HR_EmployementHistory entity;
                    if (id <= 0)
                    {
                        entity = HR_EmployementHistory.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("HR_EmployementHistory");
                        //includeTables.Add("HR_EmployementHistory");

                        entity = employementHistoryDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<HR_EmployementHistoryJson> GetEmployementHistoryListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_EmployementHistoryJson>();
            try
            {
                //HR_EmployementHistoryDataService employementHistoryDataService =
                //    new HR_EmployementHistoryDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.EmployementTypeEnumList = EnumUtil.GetEnumList(typeof(HR_EmployementHistory.EmployementTypeEnum));
                result.DataExtra.JobTypeEnumList = EnumUtil.GetEnumList(typeof(HR_EmployementHistory.JobTypeEnum));
                result.DataExtra.HistoryTypeEnumList = EnumUtil.GetEnumList(typeof(HR_EmployementHistory.HistoryTypeEnum));
                //DropDown Option Tables
                 
                //result.DataExtra.DepartmentList = DbInstance.HR_Department.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
                //result.DataExtra.EmployeeList = DbInstance.User_Employee.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
                //result.DataExtra.RankList = DbInstance.User_Rank.AsEnumerable().Select(t => new
                //{ Id = t.Id.ToString(),Name = t.Name }).ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<HR_EmployementHistoryJson> GetEmployementHistoryDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_EmployementHistoryJson>();
            try
            {
                //HR_EmployementHistoryDataService employementHistoryDataService =
                //    new HR_EmployementHistoryDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.EmployementTypeEnumList = EnumUtil.GetEnumList(typeof(HR_EmployementHistory.EmployementTypeEnum));
                result.DataExtra.JobTypeEnumList = EnumUtil.GetEnumList(typeof(HR_EmployementHistory.JobTypeEnum));
                result.DataExtra.HistoryTypeEnumList = EnumUtil.GetEnumList(typeof(HR_EmployementHistory.HistoryTypeEnum));
                //DropDown Option Tables
                 
                //result.DataExtra.DepartmentList = DbInstance.HR_Department.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
                //result.DataExtra.EmployeeList = DbInstance.User_Employee.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
                //result.DataExtra.RankList = DbInstance.User_Rank.AsEnumerable().Select(t => new
                //{ Id = t.Id.ToString(),Name = t.Name }).ToList();
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
        public HttpResult<HR_EmployementHistoryJson> SaveEmployementHistory(HR_EmployementHistoryJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_EmployementHistoryJson>();
            //optional permission, check permission in the SaveEmployementHistoryLogic insted
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.EmployementHistory.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmployementHistory.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                 var entityReceived = new HR_EmployementHistory();
                 var dbAttachedEntity = new HR_EmployementHistory();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SaveEmployementHistoryLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<HR_EmployementHistoryJson> SaveEmployementHistory2(HR_EmployementHistoryJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_EmployementHistoryJson>();
            try
            {
                using (HR_EmployementHistoryDataService employementHistoryDataService = new HR_EmployementHistoryDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new HR_EmployementHistory();
                    var dbAttachedEntity = new HR_EmployementHistory();
                    json.Map(entityReceived);
                    if (employementHistoryDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<HR_EmployementHistoryJson> SaveEmployementHistoryList(IList<HR_EmployementHistoryJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_EmployementHistoryJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.EmployementHistory.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmployementHistory.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_EmployementHistoryDataService employementHistoryDataService = new HR_EmployementHistoryDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<HR_EmployementHistory>();
                    var dbAttachedListEntity = new List<HR_EmployementHistory>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (employementHistoryDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<HR_EmployementHistoryJson> GetDeleteEmployementHistoryById(Int64 id)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_EmployementHistoryJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.EmployementHistory.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_EmployementHistoryDataService employementHistoryDataService = new HR_EmployementHistoryDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!employementHistoryDataService.DeleteById(id, out error))
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
        #endregion

        #region Local Method (should be always private)
        private bool IsValidToSaveEmployementHistory(HR_EmployementHistory newObj, out string error)
        {
            error = "";
            if (newObj.EmployeeId<=0)
            {
                error += "Please select valid Employee.";
                return false;
            }
            if (newObj.RankId<=0)
            {
                error += "Please select valid Rank.";
                return false;
            }
            if (newObj.DepartmentId<=0)
            {
                error += "Please select valid Department.";
                return false;
            }
            if (!newObj.StartDate.IsValid())
            {
                error += "Start Date is not valid.";
                return false;
            }
            if (!newObj.EndDate.IsValid())
            {
                error += "End Date is not valid.";
                return false;
            }
            if (newObj.IsActive==null)
            {
                error += "Is Active required.";
                return false;
            }
            if (newObj.EmployementTypeEnumId==null)
            {
                error += "Please select valid Employement Type.";
                return false;
            }
            if (newObj.JobTypeEnumId==null)
            {
                error += "Please select valid Job Type.";
                return false;
            }
            if (newObj.HistoryTypeEnumId==null)
            {
                error += "Please select valid History Type.";
                return false;
            }
            if (newObj.IsPrimary==null)
            {
                error += "Is Primary required.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.HR_EmployementHistory.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A EmployementHistory already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveEmployementHistoryLogic(HR_EmployementHistory newObj,ref HR_EmployementHistory dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Employement History to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveEmployementHistory(newObj, out error))
                return false;

            bool isNewObject = true;
            HR_EmployementHistory objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.HR_EmployementHistory.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            else
            {    
                newObj.Id = BigInt.NewBigInt();
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = HR_EmployementHistory.GetNew(newObj.Id);
                DbInstance.HR_EmployementHistory.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmployementHistory.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmployementHistory.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
           objToSave.EmployeeId =  newObj.EmployeeId ;
           objToSave.RankId =  newObj.RankId ;
           objToSave.DepartmentId =  newObj.DepartmentId ;
           objToSave.StartDate =  newObj.StartDate ;
           objToSave.EndDate =  newObj.EndDate ;
           objToSave.IsActive =  newObj.IsActive ;
           objToSave.EmployementTypeEnumId =  newObj.EmployementTypeEnumId ;
           objToSave.JobTypeEnumId =  newObj.JobTypeEnumId ;
           objToSave.HistoryTypeEnumId =  newObj.HistoryTypeEnumId ;
           objToSave.IsPrimary =  newObj.IsPrimary ;
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

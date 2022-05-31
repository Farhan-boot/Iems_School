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

    public class AppointmentHistoryApiController : EmployeeBaseApiController
	{
        public AppointmentHistoryApiController()
        {  }
       
        #region AppointmentHistory 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<HR_AppointmentHistoryJson> GetPagedAppointmentHistory(string textkey, int? pageSize, int? pageNo
           ,Int32 departmentId= 0      
           ,Int32 positionId= 0      
           ,Int32 employeeId= 0      
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<HR_AppointmentHistoryJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.AppointmentHistory.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.AppointmentHistory.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.AppointmentHistory.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/

            try
            {
                using (HR_AppointmentHistoryDataService appointmentHistoryDataService = new HR_AppointmentHistoryDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<HR_AppointmentHistory> query = DbInstance.HR_AppointmentHistory.OrderByDescending(x => x.Id);
                    if (departmentId>0)
                    {
                        query = query.Where(q => q.DepartmentId== departmentId);
                    }  
                    if (positionId>0)
                    {
                        query = query.Where(q => q.PositionId== positionId);
                    }  
                    if (employeeId>0)
                    {
                        query = query.Where(q => q.EmployeeId== employeeId);
                    }  
                 
                    var entities = appointmentHistoryDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<HR_AppointmentHistoryJson>();
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
        private HttpListResult<HR_AppointmentHistoryJson> GetAllAppointmentHistory()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_AppointmentHistoryJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.AppointmentHistory.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.AppointmentHistory.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.AppointmentHistory.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_AppointmentHistoryDataService appointmentHistoryDataService = new HR_AppointmentHistoryDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<HR_AppointmentHistoryJson>();
                    var entities = appointmentHistoryDataService.GetAll(out error);
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
        public HttpResult<HR_AppointmentHistoryJson> GetAppointmentHistoryById(Int64 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_AppointmentHistoryJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.AppointmentHistory.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.AppointmentHistory.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.AppointmentHistory.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_AppointmentHistoryDataService appointmentHistoryDataService = new HR_AppointmentHistoryDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new HR_AppointmentHistoryJson();
                    HR_AppointmentHistory entity;
                    if (id <= 0)
                    {
                        entity = HR_AppointmentHistory.GetNew();
                    }
                    else
                    {
                        entity = appointmentHistoryDataService.GetById(id);
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
        private HttpResult<HR_AppointmentHistoryJson> GetAppointmentHistoryByIdWithChild(Int64 id)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_AppointmentHistoryJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.AppointmentHistory.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.AppointmentHistory.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.AppointmentHistory.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_AppointmentHistoryDataService appointmentHistoryDataService = new HR_AppointmentHistoryDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new HR_AppointmentHistoryJson();
                    HR_AppointmentHistory entity;
                    if (id <= 0)
                    {
                        entity = HR_AppointmentHistory.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("HR_AppointmentHistory");
                        //includeTables.Add("HR_AppointmentHistory");

                        entity = appointmentHistoryDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<HR_AppointmentHistoryJson> GetAppointmentHistoryListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_AppointmentHistoryJson>();
            try
            {
                //HR_AppointmentHistoryDataService appointmentHistoryDataService =
                //    new HR_AppointmentHistoryDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.HistoryTypeEnumList = EnumUtil.GetEnumList(typeof(HR_AppointmentHistory.HistoryTypeEnum));
                //DropDown Option Tables
                 
                //result.DataExtra.DepartmentList = DbInstance.HR_Department.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
                //result.DataExtra.PositionList = DbInstance.HR_Position.AsEnumerable().Select(t => new
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
        public HttpListResult<HR_AppointmentHistoryJson> GetAppointmentHistoryDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_AppointmentHistoryJson>();
            try
            {
                //HR_AppointmentHistoryDataService appointmentHistoryDataService =
                //    new HR_AppointmentHistoryDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.HistoryTypeEnumList = EnumUtil.GetEnumList(typeof(HR_AppointmentHistory.HistoryTypeEnum));
                //DropDown Option Tables
                 
                //result.DataExtra.DepartmentList = DbInstance.HR_Department.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
                //result.DataExtra.PositionList = DbInstance.HR_Position.AsEnumerable().Select(t => new
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
        public HttpResult<HR_AppointmentHistoryJson> SaveAppointmentHistory(HR_AppointmentHistoryJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_AppointmentHistoryJson>();
            //optional permission, check permission in the SaveAppointmentHistoryLogic insted
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.AppointmentHistory.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.AppointmentHistory.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                 var entityReceived = new HR_AppointmentHistory();
                 var dbAttachedEntity = new HR_AppointmentHistory();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SaveAppointmentHistoryLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<HR_AppointmentHistoryJson> SaveAppointmentHistory2(HR_AppointmentHistoryJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_AppointmentHistoryJson>();
            try
            {
                using (HR_AppointmentHistoryDataService appointmentHistoryDataService = new HR_AppointmentHistoryDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new HR_AppointmentHistory();
                    var dbAttachedEntity = new HR_AppointmentHistory();
                    json.Map(entityReceived);
                    if (appointmentHistoryDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<HR_AppointmentHistoryJson> SaveAppointmentHistoryList(IList<HR_AppointmentHistoryJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_AppointmentHistoryJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.AppointmentHistory.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.AppointmentHistory.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_AppointmentHistoryDataService appointmentHistoryDataService = new HR_AppointmentHistoryDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<HR_AppointmentHistory>();
                    var dbAttachedListEntity = new List<HR_AppointmentHistory>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (appointmentHistoryDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<HR_AppointmentHistoryJson> GetDeleteAppointmentHistoryById(Int64 id)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_AppointmentHistoryJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.AppointmentHistory.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_AppointmentHistoryDataService appointmentHistoryDataService = new HR_AppointmentHistoryDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!appointmentHistoryDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveAppointmentHistory(HR_AppointmentHistory newObj, out string error)
        {
            error = "";
            if (newObj.EmployeeId<=0)
            {
                error += "Please select valid Employee.";
                return false;
            }
            if (newObj.PositionId<=0)
            {
                error += "Please select valid Position.";
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
            if (newObj.EndDate!=null && !newObj.EndDate.IsValid())
            {
                newObj.EndDate=null;//just put null,if its nullable and not valid.
               //error += "End Date is not valid.";
               //return false;
            }
            if (newObj.IsActive==null)
            {
                error += "Is Active required.";
                return false;
            }
            if (newObj.IsActing==null)
            {
                error += "Is Acting required.";
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
            //var res =  DbInstance.HR_AppointmentHistory.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A AppointmentHistory already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveAppointmentHistoryLogic(HR_AppointmentHistory newObj,ref HR_AppointmentHistory dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Appointment History to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveAppointmentHistory(newObj, out error))
                return false;

            bool isNewObject = true;
            HR_AppointmentHistory objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.HR_AppointmentHistory.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            else
            {    
                newObj.Id = BigInt.NewBigInt();
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = HR_AppointmentHistory.GetNew(newObj.Id);
                DbInstance.HR_AppointmentHistory.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.HRArea.AppointmentHistory.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.HRArea.AppointmentHistory.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
           objToSave.EmployeeId =  newObj.EmployeeId ;
           objToSave.PositionId =  newObj.PositionId ;
           objToSave.DepartmentId =  newObj.DepartmentId ;
           objToSave.StartDate =  newObj.StartDate ;
           objToSave.EndDate =  newObj.EndDate ;
           objToSave.IsActive =  newObj.IsActive ;
           objToSave.IsActing =  newObj.IsActing ;
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

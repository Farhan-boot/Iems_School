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

    public class EmpLeaveApplicationApiController : EmployeeBaseApiController
	{
        public EmpLeaveApplicationApiController()
        {  }
       
        #region EmpLeaveApplication 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<HR_EmpLeaveApplicationJson> GetPagedEmpLeaveApplication(string textkey, int? pageSize, int? pageNo
           ,Int32 leaveAllocationSettingsId= 0      
           ,Int32 employeeId= 0      
           ,Int32 recommendByEmployeeId= 0      
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<HR_EmpLeaveApplicationJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveApplication.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveApplication.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveApplication.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/

            try
            {
                using (HR_EmpLeaveApplicationDataService empLeaveApplicationDataService = new HR_EmpLeaveApplicationDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<HR_EmpLeaveApplication> query = DbInstance.HR_EmpLeaveApplication.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.LeaveReason.ToLower().Contains(textkey.ToLower()));
                    }
                    if (leaveAllocationSettingsId>0)
                    {
                        query = query.Where(q => q.LeaveAllocationSettingsId== leaveAllocationSettingsId);
                    }  
                    if (employeeId>0)
                    {
                        query = query.Where(q => q.EmployeeId== employeeId);
                    }  
                    if (recommendByEmployeeId>0)
                    {
                        query = query.Where(q => q.RecommendByEmployeeId== recommendByEmployeeId);
                    }  
                 
                    var entities = empLeaveApplicationDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<HR_EmpLeaveApplicationJson>();
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
        private HttpListResult<HR_EmpLeaveApplicationJson> GetAllEmpLeaveApplication()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_EmpLeaveApplicationJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveApplication.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveApplication.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveApplication.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_EmpLeaveApplicationDataService empLeaveApplicationDataService = new HR_EmpLeaveApplicationDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<HR_EmpLeaveApplicationJson>();
                    var entities = empLeaveApplicationDataService.GetAll(out error);
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
        public HttpResult<HR_EmpLeaveApplicationJson> GetEmpLeaveApplicationById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_EmpLeaveApplicationJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveApplication.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveApplication.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveApplication.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_EmpLeaveApplicationDataService empLeaveApplicationDataService = new HR_EmpLeaveApplicationDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new HR_EmpLeaveApplicationJson();
                    HR_EmpLeaveApplication entity;
                    if (id <= 0)
                    {
                        entity = HR_EmpLeaveApplication.GetNew();
                    }
                    else
                    {
                        entity = empLeaveApplicationDataService.GetById(id);
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
        private HttpResult<HR_EmpLeaveApplicationJson> GetEmpLeaveApplicationByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_EmpLeaveApplicationJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveApplication.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveApplication.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveApplication.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_EmpLeaveApplicationDataService empLeaveApplicationDataService = new HR_EmpLeaveApplicationDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new HR_EmpLeaveApplicationJson();
                    HR_EmpLeaveApplication entity;
                    if (id <= 0)
                    {
                        entity = HR_EmpLeaveApplication.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("HR_EmpLeaveApplication");
                        //includeTables.Add("HR_EmpLeaveApplication");

                        entity = empLeaveApplicationDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<HR_EmpLeaveApplicationJson> GetEmpLeaveApplicationListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_EmpLeaveApplicationJson>();
            try
            {
                //HR_EmpLeaveApplicationDataService empLeaveApplicationDataService =
                //    new HR_EmpLeaveApplicationDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.LeaveTypeEnumList = EnumUtil.GetEnumList(typeof(HR_EmpLeaveApplication.LeaveTypeEnum));
                result.DataExtra.StatusEnumList = EnumUtil.GetEnumList(typeof(HR_EmpLeaveApplication.StatusEnum));
                //DropDown Option Tables
                 
                //result.DataExtra.EmpLeaveAllocationSettingsList = DbInstance.HR_EmpLeaveAllocationSettings.AsEnumerable().Select(t => new
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
        public HttpListResult<HR_EmpLeaveApplicationJson> GetEmpLeaveApplicationDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_EmpLeaveApplicationJson>();
            try
            {
                //HR_EmpLeaveApplicationDataService empLeaveApplicationDataService =
                //    new HR_EmpLeaveApplicationDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.LeaveTypeEnumList = EnumUtil.GetEnumList(typeof(HR_EmpLeaveApplication.LeaveTypeEnum));
                result.DataExtra.StatusEnumList = EnumUtil.GetEnumList(typeof(HR_EmpLeaveApplication.StatusEnum));
                //DropDown Option Tables
                 
                //result.DataExtra.EmpLeaveAllocationSettingsList = DbInstance.HR_EmpLeaveAllocationSettings.AsEnumerable().Select(t => new
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
        public HttpResult<HR_EmpLeaveApplicationJson> SaveEmpLeaveApplication(HR_EmpLeaveApplicationJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_EmpLeaveApplicationJson>();
            //optional permission, check permission in the SaveEmpLeaveApplicationLogic insted
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveApplication.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveApplication.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                 var entityReceived = new HR_EmpLeaveApplication();
                 var dbAttachedEntity = new HR_EmpLeaveApplication();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SaveEmpLeaveApplicationLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<HR_EmpLeaveApplicationJson> SaveEmpLeaveApplication2(HR_EmpLeaveApplicationJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_EmpLeaveApplicationJson>();
            try
            {
                using (HR_EmpLeaveApplicationDataService empLeaveApplicationDataService = new HR_EmpLeaveApplicationDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new HR_EmpLeaveApplication();
                    var dbAttachedEntity = new HR_EmpLeaveApplication();
                    json.Map(entityReceived);
                    if (empLeaveApplicationDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<HR_EmpLeaveApplicationJson> SaveEmpLeaveApplicationList(IList<HR_EmpLeaveApplicationJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_EmpLeaveApplicationJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveApplication.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveApplication.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_EmpLeaveApplicationDataService empLeaveApplicationDataService = new HR_EmpLeaveApplicationDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<HR_EmpLeaveApplication>();
                    var dbAttachedListEntity = new List<HR_EmpLeaveApplication>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (empLeaveApplicationDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<HR_EmpLeaveApplicationJson> GetDeleteEmpLeaveApplicationById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_EmpLeaveApplicationJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveApplication.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_EmpLeaveApplicationDataService empLeaveApplicationDataService = new HR_EmpLeaveApplicationDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!empLeaveApplicationDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveEmpLeaveApplication(HR_EmpLeaveApplication newObj, out string error)
        {
            error = "";
            if (newObj.LeaveAllocationSettingsId<=0)
            {
                error += "Please select valid Leave Allocation Settings.";
                return false;
            }
            if (newObj.EmployeeId<=0)
            {
                error += "Please select valid Employee.";
                return false;
            }
            if (newObj.LeaveTypeEnumId==null)
            {
                error += "Please select valid Leave Type.";
                return false;
            }
            if (!newObj.LeaveFrom.IsValid())
            {
                error += "Leave From is not valid.";
                return false;
            }
            if (!newObj.LeaveTill.IsValid())
            {
                error += "Leave Till is not valid.";
                return false;
            }
            if (newObj.LeaveHour==null)
            {
                error += "Leave Hour required.";
                return false;
            }
            if (!newObj.LeaveReason.IsValid())
            {
                error += "Leave Reason is not valid.";
                return false;
            }
            if (newObj.LeaveReason==null)
            {
                error += "Leave Reason required.";
                return false;
            }
            if (newObj.StatusEnumId==null)
            {
                error += "Please select valid Status.";
                return false;
            }



            if (newObj.IsDeleted==null)
            {
                error += "Is Deleted required.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.HR_EmpLeaveApplication.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A EmpLeaveApplication already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveEmpLeaveApplicationLogic(HR_EmpLeaveApplication newObj,ref HR_EmpLeaveApplication dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Emp Leave Application to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveEmpLeaveApplication(newObj, out error))
                return false;

            bool isNewObject = true;
            HR_EmpLeaveApplication objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.HR_EmpLeaveApplication.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = HR_EmpLeaveApplication.GetNew(newObj.Id);
                DbInstance.HR_EmpLeaveApplication.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveApplication.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveApplication.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
           objToSave.LeaveAllocationSettingsId =  newObj.LeaveAllocationSettingsId ;
           objToSave.EmployeeId =  newObj.EmployeeId ;
           objToSave.LeaveTypeEnumId =  newObj.LeaveTypeEnumId ;
           objToSave.LeaveFrom =  newObj.LeaveFrom ;
           objToSave.LeaveTill =  newObj.LeaveTill ;
           objToSave.LeaveHour =  newObj.LeaveHour ;
           objToSave.LeaveReason =  newObj.LeaveReason ;
           objToSave.StatusEnumId =  newObj.StatusEnumId ;
           objToSave.RecommendByEmployeeId =  newObj.RecommendByEmployeeId ;
           objToSave.Remarks =  newObj.Remarks ;
           objToSave.History =  newObj.History ;
           objToSave.IsDeleted =  newObj.IsDeleted ;
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

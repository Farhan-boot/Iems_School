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

    public class EmpLeaveAllocationSettingsDetailsApiController : EmployeeBaseApiController
	{
        public EmpLeaveAllocationSettingsDetailsApiController()
        {  }
       
        #region EmpLeaveAllocationSettingsDetails 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<HR_EmpLeaveAllocationSettingsDetailsJson> GetPagedEmpLeaveAllocationSettingsDetails(string textkey, int? pageSize, int? pageNo
           ,Int32 empLeaveAllocationSettingsId= 0      
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<HR_EmpLeaveAllocationSettingsDetailsJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveAllocationSettingsDetails.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveAllocationSettingsDetails.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveAllocationSettingsDetails.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/

            try
            {
                using (HR_EmpLeaveAllocationSettingsDetailsDataService empLeaveAllocationSettingsDetailsDataService = new HR_EmpLeaveAllocationSettingsDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<HR_EmpLeaveAllocationSettingsDetails> query = DbInstance.HR_EmpLeaveAllocationSettingsDetails.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Name.ToLower().Contains(textkey.ToLower()));
                    }
                    if (empLeaveAllocationSettingsId>0)
                    {
                        query = query.Where(q => q.EmpLeaveAllocationSettingsId== empLeaveAllocationSettingsId);
                    }  
                 
                    var entities = empLeaveAllocationSettingsDetailsDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<HR_EmpLeaveAllocationSettingsDetailsJson>();
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
        private HttpListResult<HR_EmpLeaveAllocationSettingsDetailsJson> GetAllEmpLeaveAllocationSettingsDetails()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_EmpLeaveAllocationSettingsDetailsJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveAllocationSettingsDetails.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveAllocationSettingsDetails.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveAllocationSettingsDetails.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_EmpLeaveAllocationSettingsDetailsDataService empLeaveAllocationSettingsDetailsDataService = new HR_EmpLeaveAllocationSettingsDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<HR_EmpLeaveAllocationSettingsDetailsJson>();
                    var entities = empLeaveAllocationSettingsDetailsDataService.GetAll(out error);
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
        public HttpResult<HR_EmpLeaveAllocationSettingsDetailsJson> GetEmpLeaveAllocationSettingsDetailsById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_EmpLeaveAllocationSettingsDetailsJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveAllocationSettingsDetails.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveAllocationSettingsDetails.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveAllocationSettingsDetails.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_EmpLeaveAllocationSettingsDetailsDataService empLeaveAllocationSettingsDetailsDataService = new HR_EmpLeaveAllocationSettingsDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new HR_EmpLeaveAllocationSettingsDetailsJson();
                    HR_EmpLeaveAllocationSettingsDetails entity;
                    if (id <= 0)
                    {
                        entity = HR_EmpLeaveAllocationSettingsDetails.GetNew();
                    }
                    else
                    {
                        entity = empLeaveAllocationSettingsDetailsDataService.GetById(id);
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
        private HttpResult<HR_EmpLeaveAllocationSettingsDetailsJson> GetEmpLeaveAllocationSettingsDetailsByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_EmpLeaveAllocationSettingsDetailsJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveAllocationSettingsDetails.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveAllocationSettingsDetails.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveAllocationSettingsDetails.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_EmpLeaveAllocationSettingsDetailsDataService empLeaveAllocationSettingsDetailsDataService = new HR_EmpLeaveAllocationSettingsDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new HR_EmpLeaveAllocationSettingsDetailsJson();
                    HR_EmpLeaveAllocationSettingsDetails entity;
                    if (id <= 0)
                    {
                        entity = HR_EmpLeaveAllocationSettingsDetails.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("HR_EmpLeaveAllocationSettingsDetails");
                        //includeTables.Add("HR_EmpLeaveAllocationSettingsDetails");

                        entity = empLeaveAllocationSettingsDetailsDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<HR_EmpLeaveAllocationSettingsDetailsJson> GetEmpLeaveAllocationSettingsDetailsListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_EmpLeaveAllocationSettingsDetailsJson>();
            try
            {
                //HR_EmpLeaveAllocationSettingsDetailsDataService empLeaveAllocationSettingsDetailsDataService =
                //    new HR_EmpLeaveAllocationSettingsDetailsDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.LeaveTypeEnumList = EnumUtil.GetEnumList(typeof(HR_EmpLeaveAllocationSettingsDetails.LeaveTypeEnum));
                //DropDown Option Tables
                 
                //result.DataExtra.EmpLeaveAllocationSettingsList = DbInstance.HR_EmpLeaveAllocationSettings.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<HR_EmpLeaveAllocationSettingsDetailsJson> GetEmpLeaveAllocationSettingsDetailsDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_EmpLeaveAllocationSettingsDetailsJson>();
            try
            {
                //HR_EmpLeaveAllocationSettingsDetailsDataService empLeaveAllocationSettingsDetailsDataService =
                //    new HR_EmpLeaveAllocationSettingsDetailsDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.LeaveTypeEnumList = EnumUtil.GetEnumList(typeof(HR_EmpLeaveAllocationSettingsDetails.LeaveTypeEnum));
                //DropDown Option Tables
                 
                //result.DataExtra.EmpLeaveAllocationSettingsList = DbInstance.HR_EmpLeaveAllocationSettings.AsEnumerable().Select(t => new
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
        public HttpResult<HR_EmpLeaveAllocationSettingsDetailsJson> SaveEmpLeaveAllocationSettingsDetails(HR_EmpLeaveAllocationSettingsDetailsJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_EmpLeaveAllocationSettingsDetailsJson>();
            //optional permission, check permission in the SaveEmpLeaveAllocationSettingsDetailsLogic insted
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveAllocationSettingsDetails.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveAllocationSettingsDetails.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                 var entityReceived = new HR_EmpLeaveAllocationSettingsDetails();
                 var dbAttachedEntity = new HR_EmpLeaveAllocationSettingsDetails();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SaveEmpLeaveAllocationSettingsDetailsLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<HR_EmpLeaveAllocationSettingsDetailsJson> SaveEmpLeaveAllocationSettingsDetails2(HR_EmpLeaveAllocationSettingsDetailsJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_EmpLeaveAllocationSettingsDetailsJson>();
            try
            {
                using (HR_EmpLeaveAllocationSettingsDetailsDataService empLeaveAllocationSettingsDetailsDataService = new HR_EmpLeaveAllocationSettingsDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new HR_EmpLeaveAllocationSettingsDetails();
                    var dbAttachedEntity = new HR_EmpLeaveAllocationSettingsDetails();
                    json.Map(entityReceived);
                    if (empLeaveAllocationSettingsDetailsDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<HR_EmpLeaveAllocationSettingsDetailsJson> SaveEmpLeaveAllocationSettingsDetailsList(IList<HR_EmpLeaveAllocationSettingsDetailsJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_EmpLeaveAllocationSettingsDetailsJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveAllocationSettingsDetails.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveAllocationSettingsDetails.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_EmpLeaveAllocationSettingsDetailsDataService empLeaveAllocationSettingsDetailsDataService = new HR_EmpLeaveAllocationSettingsDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<HR_EmpLeaveAllocationSettingsDetails>();
                    var dbAttachedListEntity = new List<HR_EmpLeaveAllocationSettingsDetails>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (empLeaveAllocationSettingsDetailsDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<HR_EmpLeaveAllocationSettingsDetailsJson> GetDeleteEmpLeaveAllocationSettingsDetailsById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_EmpLeaveAllocationSettingsDetailsJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveAllocationSettingsDetails.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_EmpLeaveAllocationSettingsDetailsDataService empLeaveAllocationSettingsDetailsDataService = new HR_EmpLeaveAllocationSettingsDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!empLeaveAllocationSettingsDetailsDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveEmpLeaveAllocationSettingsDetails(HR_EmpLeaveAllocationSettingsDetails newObj, out string error)
        {
            error = "";
            if (!newObj.Name.IsValid())
            {
                error += "Name is not valid.";
                return false;
            }
            if (newObj.Name.Length>150)
            {
                error += "Name exceeded its maximum length 150.";
                return false;
            }
            if (newObj.EmpLeaveAllocationSettingsId<=0)
            {
                error += "Please select valid Emp Leave Allocation Settings.";
                return false;
            }
            if (newObj.LeaveTypeEnumId==null)
            {
                error += "Please select valid Leave Type.";
                return false;
            }
            if (newObj.AllowedDays==null)
            {
                error += "Allowed Days required.";
                return false;
            }
            if (newObj.WorkingHourPerDays==null)
            {
                error += "Working Hour Per Days required.";
                return false;
            }
            if (newObj.HourUsed==null)
            {
                error += "Hour Used required.";
                return false;
            }
            if (newObj.IsPaid==null)
            {
                error += "Is Paid required.";
                return false;
            }

            //TODO write your custom validation here.
            //var res =  DbInstance.HR_EmpLeaveAllocationSettingsDetails.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A EmpLeaveAllocationSettingsDetails already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveEmpLeaveAllocationSettingsDetailsLogic(HR_EmpLeaveAllocationSettingsDetails newObj,ref HR_EmpLeaveAllocationSettingsDetails dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Emp Leave Allocation Settings Details to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveEmpLeaveAllocationSettingsDetails(newObj, out error))
                return false;

            bool isNewObject = true;
            HR_EmpLeaveAllocationSettingsDetails objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.HR_EmpLeaveAllocationSettingsDetails.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = HR_EmpLeaveAllocationSettingsDetails.GetNew(newObj.Id);
                DbInstance.HR_EmpLeaveAllocationSettingsDetails.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveAllocationSettingsDetails.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveAllocationSettingsDetails.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
           objToSave.Name =  newObj.Name ;
           objToSave.EmpLeaveAllocationSettingsId =  newObj.EmpLeaveAllocationSettingsId ;
           objToSave.LeaveTypeEnumId =  newObj.LeaveTypeEnumId ;
           objToSave.AllowedDays =  newObj.AllowedDays ;
           objToSave.WorkingHourPerDays =  newObj.WorkingHourPerDays ;
           objToSave.HourUsed =  newObj.HourUsed ;
           objToSave.IsPaid =  newObj.IsPaid ;
           objToSave.Remarks =  newObj.Remarks ;
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

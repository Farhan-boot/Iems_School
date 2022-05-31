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

    public class EmpLeaveAllocationSettingsApiController : EmployeeBaseApiController
	{
        public EmpLeaveAllocationSettingsApiController()
        {  }
       
        #region EmpLeaveAllocationSettings 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<HR_EmpLeaveAllocationSettingsJson> GetPagedEmpLeaveAllocationSettings(string textkey, int? pageSize, int? pageNo
           ,Int32 employeeId= 0      
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<HR_EmpLeaveAllocationSettingsJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveAllocationSettings.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveAllocationSettings.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveAllocationSettings.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/

            try
            {
                using (HR_EmpLeaveAllocationSettingsDataService empLeaveAllocationSettingsDataService = new HR_EmpLeaveAllocationSettingsDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<HR_EmpLeaveAllocationSettings> query = DbInstance.HR_EmpLeaveAllocationSettings.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Remarks.ToLower().Contains(textkey.ToLower()));
                    }
                    if (employeeId>0)
                    {
                        query = query.Where(q => q.EmployeeId== employeeId);
                    }  
                 
                    var entities = empLeaveAllocationSettingsDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<HR_EmpLeaveAllocationSettingsJson>();
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
        private HttpListResult<HR_EmpLeaveAllocationSettingsJson> GetAllEmpLeaveAllocationSettings()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_EmpLeaveAllocationSettingsJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveAllocationSettings.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveAllocationSettings.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveAllocationSettings.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_EmpLeaveAllocationSettingsDataService empLeaveAllocationSettingsDataService = new HR_EmpLeaveAllocationSettingsDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<HR_EmpLeaveAllocationSettingsJson>();
                    var entities = empLeaveAllocationSettingsDataService.GetAll(out error);
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
        public HttpResult<HR_EmpLeaveAllocationSettingsJson> GetEmpLeaveAllocationSettingsById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_EmpLeaveAllocationSettingsJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveAllocationSettings.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveAllocationSettings.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveAllocationSettings.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_EmpLeaveAllocationSettingsDataService empLeaveAllocationSettingsDataService = new HR_EmpLeaveAllocationSettingsDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new HR_EmpLeaveAllocationSettingsJson();
                    HR_EmpLeaveAllocationSettings entity;
                    if (id <= 0)
                    {
                        entity = HR_EmpLeaveAllocationSettings.GetNew();
                    }
                    else
                    {
                        entity = empLeaveAllocationSettingsDataService.GetById(id);
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
        private HttpResult<HR_EmpLeaveAllocationSettingsJson> GetEmpLeaveAllocationSettingsByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_EmpLeaveAllocationSettingsJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveAllocationSettings.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveAllocationSettings.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveAllocationSettings.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_EmpLeaveAllocationSettingsDataService empLeaveAllocationSettingsDataService = new HR_EmpLeaveAllocationSettingsDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new HR_EmpLeaveAllocationSettingsJson();
                    HR_EmpLeaveAllocationSettings entity;
                    if (id <= 0)
                    {
                        entity = HR_EmpLeaveAllocationSettings.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("HR_EmpLeaveAllocationSettings");
                        //includeTables.Add("HR_EmpLeaveAllocationSettings");

                        entity = empLeaveAllocationSettingsDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<HR_EmpLeaveAllocationSettingsJson> GetEmpLeaveAllocationSettingsListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_EmpLeaveAllocationSettingsJson>();
            try
            {
                //HR_EmpLeaveAllocationSettingsDataService empLeaveAllocationSettingsDataService =
                //    new HR_EmpLeaveAllocationSettingsDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                //DropDown Option Tables
                 
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
        public HttpListResult<HR_EmpLeaveAllocationSettingsJson> GetEmpLeaveAllocationSettingsDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_EmpLeaveAllocationSettingsJson>();
            try
            {
                //HR_EmpLeaveAllocationSettingsDataService empLeaveAllocationSettingsDataService =
                //    new HR_EmpLeaveAllocationSettingsDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                //DropDown Option Tables
                 
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
        public HttpResult<HR_EmpLeaveAllocationSettingsJson> SaveEmpLeaveAllocationSettings(HR_EmpLeaveAllocationSettingsJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_EmpLeaveAllocationSettingsJson>();
            //optional permission, check permission in the SaveEmpLeaveAllocationSettingsLogic insted
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveAllocationSettings.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveAllocationSettings.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                 var entityReceived = new HR_EmpLeaveAllocationSettings();
                 var dbAttachedEntity = new HR_EmpLeaveAllocationSettings();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SaveEmpLeaveAllocationSettingsLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<HR_EmpLeaveAllocationSettingsJson> SaveEmpLeaveAllocationSettings2(HR_EmpLeaveAllocationSettingsJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_EmpLeaveAllocationSettingsJson>();
            try
            {
                using (HR_EmpLeaveAllocationSettingsDataService empLeaveAllocationSettingsDataService = new HR_EmpLeaveAllocationSettingsDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new HR_EmpLeaveAllocationSettings();
                    var dbAttachedEntity = new HR_EmpLeaveAllocationSettings();
                    json.Map(entityReceived);
                    if (empLeaveAllocationSettingsDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<HR_EmpLeaveAllocationSettingsJson> SaveEmpLeaveAllocationSettingsList(IList<HR_EmpLeaveAllocationSettingsJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_EmpLeaveAllocationSettingsJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveAllocationSettings.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveAllocationSettings.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_EmpLeaveAllocationSettingsDataService empLeaveAllocationSettingsDataService = new HR_EmpLeaveAllocationSettingsDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<HR_EmpLeaveAllocationSettings>();
                    var dbAttachedListEntity = new List<HR_EmpLeaveAllocationSettings>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (empLeaveAllocationSettingsDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<HR_EmpLeaveAllocationSettingsJson> GetDeleteEmpLeaveAllocationSettingsById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_EmpLeaveAllocationSettingsJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveAllocationSettings.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_EmpLeaveAllocationSettingsDataService empLeaveAllocationSettingsDataService = new HR_EmpLeaveAllocationSettingsDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!empLeaveAllocationSettingsDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveEmpLeaveAllocationSettings(HR_EmpLeaveAllocationSettings newObj, out string error)
        {
            error = "";
            if (newObj.EmployeeId<=0)
            {
                error += "Please select valid Employee.";
                return false;
            }
            if (newObj.TotalLeaveHour==null)
            {
                error += "Total Leave Hour required.";
                return false;
            }
            if (newObj.IsCurrent==null)
            {
                error += "Is Current required.";
                return false;
            }
            if (!newObj.EffectiveFrom.IsValid())
            {
                error += "Effective From is not valid.";
                return false;
            }
            if (!newObj.EffectiveTill.IsValid())
            {
                error += "Effective Till is not valid.";
                return false;
            }


            if (newObj.IsDeleted==null)
            {
                error += "Is Deleted required.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.HR_EmpLeaveAllocationSettings.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A EmpLeaveAllocationSettings already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveEmpLeaveAllocationSettingsLogic(HR_EmpLeaveAllocationSettings newObj,ref HR_EmpLeaveAllocationSettings dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Emp Leave Allocation Settings to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveEmpLeaveAllocationSettings(newObj, out error))
                return false;

            bool isNewObject = true;
            HR_EmpLeaveAllocationSettings objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.HR_EmpLeaveAllocationSettings.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = HR_EmpLeaveAllocationSettings.GetNew(newObj.Id);
                DbInstance.HR_EmpLeaveAllocationSettings.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveAllocationSettings.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.HRArea.EmpLeaveAllocationSettings.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
           objToSave.EmployeeId =  newObj.EmployeeId ;
           objToSave.TotalLeaveHour =  newObj.TotalLeaveHour ;
           objToSave.IsCurrent =  newObj.IsCurrent ;
           objToSave.EffectiveFrom =  newObj.EffectiveFrom ;
           objToSave.EffectiveTill =  newObj.EffectiveTill ;
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

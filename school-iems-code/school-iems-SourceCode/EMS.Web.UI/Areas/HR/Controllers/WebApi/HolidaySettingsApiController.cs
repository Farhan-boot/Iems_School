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

    public class HolidaySettingsApiController : EmployeeBaseApiController
	{
        public HolidaySettingsApiController()
        {  }
       
        #region HolidaySettings 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<HR_HolidaySettingsJson> GetPagedHolidaySettings(string textkey, int? pageSize, int? pageNo
           ,Int32 calendarYearId= 0      
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<HR_HolidaySettingsJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.HolidaySettings.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.HolidaySettings.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.HolidaySettings.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/

            try
            {
                using (HR_HolidaySettingsDataService holidaySettingsDataService = new HR_HolidaySettingsDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<HR_HolidaySettings> query = DbInstance.HR_HolidaySettings.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Name.ToLower().Contains(textkey.ToLower()));
                    }
                    if (calendarYearId>0)
                    {
                        query = query.Where(q => q.CalendarYearId== calendarYearId);
                    }  
                 
                    var entities = holidaySettingsDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<HR_HolidaySettingsJson>();
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
        private HttpListResult<HR_HolidaySettingsJson> GetAllHolidaySettings()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_HolidaySettingsJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.HolidaySettings.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.HolidaySettings.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.HolidaySettings.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_HolidaySettingsDataService holidaySettingsDataService = new HR_HolidaySettingsDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<HR_HolidaySettingsJson>();
                    var entities = holidaySettingsDataService.GetAll(out error);
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
        public HttpResult<HR_HolidaySettingsJson> GetHolidaySettingsById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_HolidaySettingsJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.HolidaySettings.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.HolidaySettings.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.HolidaySettings.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_HolidaySettingsDataService holidaySettingsDataService = new HR_HolidaySettingsDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new HR_HolidaySettingsJson();
                    HR_HolidaySettings entity;
                    if (id <= 0)
                    {
                        entity = HR_HolidaySettings.GetNew();
                    }
                    else
                    {
                        entity = holidaySettingsDataService.GetById(id);
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
        private HttpResult<HR_HolidaySettingsJson> GetHolidaySettingsByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_HolidaySettingsJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.HolidaySettings.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.HolidaySettings.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.HolidaySettings.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_HolidaySettingsDataService holidaySettingsDataService = new HR_HolidaySettingsDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new HR_HolidaySettingsJson();
                    HR_HolidaySettings entity;
                    if (id <= 0)
                    {
                        entity = HR_HolidaySettings.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("HR_HolidaySettings");
                        //includeTables.Add("HR_HolidaySettings");

                        entity = holidaySettingsDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<HR_HolidaySettingsJson> GetHolidaySettingsListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_HolidaySettingsJson>();
            try
            {
                //HR_HolidaySettingsDataService holidaySettingsDataService =
                //    new HR_HolidaySettingsDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.WorkingDayTypeEnumList = EnumUtil.GetEnumList(typeof(HR_HolidaySettings.WorkingDayTypeEnum));
                //DropDown Option Tables
                 
                //result.DataExtra.CalendarYearList = DbInstance.HR_CalendarYear.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<HR_HolidaySettingsJson> GetHolidaySettingsDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_HolidaySettingsJson>();
            try
            {
                //HR_HolidaySettingsDataService holidaySettingsDataService =
                //    new HR_HolidaySettingsDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.WorkingDayTypeEnumList = EnumUtil.GetEnumList(typeof(HR_HolidaySettings.WorkingDayTypeEnum));
                //DropDown Option Tables
                 
                //result.DataExtra.CalendarYearList = DbInstance.HR_CalendarYear.AsEnumerable().Select(t => new
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
        public HttpResult<HR_HolidaySettingsJson> SaveHolidaySettings(HR_HolidaySettingsJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_HolidaySettingsJson>();
            //optional permission, check permission in the SaveHolidaySettingsLogic insted
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.HolidaySettings.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.HolidaySettings.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                 var entityReceived = new HR_HolidaySettings();
                 var dbAttachedEntity = new HR_HolidaySettings();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SaveHolidaySettingsLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<HR_HolidaySettingsJson> SaveHolidaySettings2(HR_HolidaySettingsJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_HolidaySettingsJson>();
            try
            {
                using (HR_HolidaySettingsDataService holidaySettingsDataService = new HR_HolidaySettingsDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new HR_HolidaySettings();
                    var dbAttachedEntity = new HR_HolidaySettings();
                    json.Map(entityReceived);
                    if (holidaySettingsDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<HR_HolidaySettingsJson> SaveHolidaySettingsList(IList<HR_HolidaySettingsJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_HolidaySettingsJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.HolidaySettings.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.HolidaySettings.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_HolidaySettingsDataService holidaySettingsDataService = new HR_HolidaySettingsDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<HR_HolidaySettings>();
                    var dbAttachedListEntity = new List<HR_HolidaySettings>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (holidaySettingsDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<HR_HolidaySettingsJson> GetDeleteHolidaySettingsById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_HolidaySettingsJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.HolidaySettings.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_HolidaySettingsDataService holidaySettingsDataService = new HR_HolidaySettingsDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!holidaySettingsDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveHolidaySettings(HR_HolidaySettings newObj, out string error)
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
            if (newObj.CalendarYearId<=0)
            {
                error += "Please select valid Calendar Year.";
                return false;
            }
            if (newObj.WorkingDayTypeEnumId==null)
            {
                error += "Please select valid Working Day Type.";
                return false;
            }
            if (newObj.IsHalfDay==null)
            {
                error += "Is Half Day required.";
                return false;
            }
            if (newObj.IsDeleted==null)
            {
                error += "Is Deleted required.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.HR_HolidaySettings.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A HolidaySettings already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveHolidaySettingsLogic(HR_HolidaySettings newObj,ref HR_HolidaySettings dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Holiday Settings to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveHolidaySettings(newObj, out error))
                return false;

            bool isNewObject = true;
            HR_HolidaySettings objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.HR_HolidaySettings.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = HR_HolidaySettings.GetNew(newObj.Id);
                DbInstance.HR_HolidaySettings.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.HRArea.HolidaySettings.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.HRArea.HolidaySettings.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
           objToSave.Name =  newObj.Name ;
           objToSave.StartDate =  newObj.StartDate ;
           objToSave.EndDate =  newObj.EndDate ;
           objToSave.CalendarYearId =  newObj.CalendarYearId ;
           objToSave.WorkingDayTypeEnumId =  newObj.WorkingDayTypeEnumId ;
           objToSave.IsHalfDay =  newObj.IsHalfDay ;
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

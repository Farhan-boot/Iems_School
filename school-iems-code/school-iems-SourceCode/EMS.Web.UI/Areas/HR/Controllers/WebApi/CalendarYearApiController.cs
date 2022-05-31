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

    public class CalendarYearApiController : EmployeeBaseApiController
	{
        public CalendarYearApiController()
        {  }
       
        #region CalendarYear 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<HR_CalendarYearJson> GetPagedCalendarYear(string textkey, int? pageSize, int? pageNo
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<HR_CalendarYearJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.CalendarYear.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.CalendarYear.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.CalendarYear.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/

            try
            {
                using (HR_CalendarYearDataService calendarYearDataService = new HR_CalendarYearDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<HR_CalendarYear> query = DbInstance.HR_CalendarYear.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Name.ToLower().Contains(textkey.ToLower()));
                    }
                 
                    var entities = calendarYearDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<HR_CalendarYearJson>();
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
        private HttpListResult<HR_CalendarYearJson> GetAllCalendarYear()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_CalendarYearJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.CalendarYear.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.CalendarYear.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.CalendarYear.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_CalendarYearDataService calendarYearDataService = new HR_CalendarYearDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<HR_CalendarYearJson>();
                    var entities = calendarYearDataService.GetAll(out error);
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
        public HttpResult<HR_CalendarYearJson> GetCalendarYearById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_CalendarYearJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.CalendarYear.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.CalendarYear.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.CalendarYear.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_CalendarYearDataService calendarYearDataService = new HR_CalendarYearDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new HR_CalendarYearJson();
                    HR_CalendarYear entity;
                    if (id <= 0)
                    {
                        entity = HR_CalendarYear.GetNew();
                    }
                    else
                    {
                        entity = calendarYearDataService.GetById(id);
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
        private HttpResult<HR_CalendarYearJson> GetCalendarYearByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_CalendarYearJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.CalendarYear.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.CalendarYear.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.CalendarYear.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_CalendarYearDataService calendarYearDataService = new HR_CalendarYearDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new HR_CalendarYearJson();
                    HR_CalendarYear entity;
                    if (id <= 0)
                    {
                        entity = HR_CalendarYear.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("HR_CalendarYear");
                        //includeTables.Add("HR_CalendarYear");

                        entity = calendarYearDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<HR_CalendarYearJson> GetCalendarYearListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_CalendarYearJson>();
            try
            {
                //HR_CalendarYearDataService calendarYearDataService =
                //    new HR_CalendarYearDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                //DropDown Option Tables
                 
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<HR_CalendarYearJson> GetCalendarYearDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_CalendarYearJson>();
            try
            {
                //HR_CalendarYearDataService calendarYearDataService =
                //    new HR_CalendarYearDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
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
        public HttpResult<HR_CalendarYearJson> SaveCalendarYear(HR_CalendarYearJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_CalendarYearJson>();
            //optional permission, check permission in the SaveCalendarYearLogic insted
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.CalendarYear.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.CalendarYear.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                 var entityReceived = new HR_CalendarYear();
                 var dbAttachedEntity = new HR_CalendarYear();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SaveCalendarYearLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<HR_CalendarYearJson> SaveCalendarYear2(HR_CalendarYearJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_CalendarYearJson>();
            try
            {
                using (HR_CalendarYearDataService calendarYearDataService = new HR_CalendarYearDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new HR_CalendarYear();
                    var dbAttachedEntity = new HR_CalendarYear();
                    json.Map(entityReceived);
                    if (calendarYearDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<HR_CalendarYearJson> SaveCalendarYearList(IList<HR_CalendarYearJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_CalendarYearJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.CalendarYear.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.CalendarYear.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_CalendarYearDataService calendarYearDataService = new HR_CalendarYearDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<HR_CalendarYear>();
                    var dbAttachedListEntity = new List<HR_CalendarYear>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (calendarYearDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<HR_CalendarYearJson> GetDeleteCalendarYearById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_CalendarYearJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.CalendarYear.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_CalendarYearDataService calendarYearDataService = new HR_CalendarYearDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!calendarYearDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveCalendarYear(HR_CalendarYear newObj, out string error)
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
            if (!newObj.PeriodStart.IsValid())
            {
                error += "Period Start is not valid.";
                return false;
            }
            if (!newObj.PeriodEnd.IsValid())
            {
                error += "Period End is not valid.";
                return false;
            }
            if (newObj.IsCurrent==null)
            {
                error += "Is Current required.";
                return false;
            }
            if (newObj.IsDeleted==null)
            {
                error += "Is Deleted required.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.HR_CalendarYear.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A CalendarYear already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveCalendarYearLogic(HR_CalendarYear newObj,ref HR_CalendarYear dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Calendar Year to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveCalendarYear(newObj, out error))
                return false;

            bool isNewObject = true;
            HR_CalendarYear objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.HR_CalendarYear.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = HR_CalendarYear.GetNew(newObj.Id);
                DbInstance.HR_CalendarYear.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.HRArea.CalendarYear.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.HRArea.CalendarYear.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
           objToSave.Name =  newObj.Name ;
           objToSave.PeriodStart =  newObj.PeriodStart ;
           objToSave.PeriodEnd =  newObj.PeriodEnd ;
           objToSave.IsCurrent =  newObj.IsCurrent ;
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

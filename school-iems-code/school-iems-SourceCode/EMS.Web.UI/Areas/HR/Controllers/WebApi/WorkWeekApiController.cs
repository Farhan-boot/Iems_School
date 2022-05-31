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
    public class WorkWeekApiController : EmployeeBaseApiController
	{
        public WorkWeekApiController()
        {  }
       
        #region WorkWeek 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<HR_WorkWeekJson> GetPagedWorkWeek(string textkey, int? pageSize, int? pageNo
           ,Int32 calendarYearId= 0      
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<HR_WorkWeekJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.WorkWeek.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.WorkWeek.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.WorkWeek.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/

            try
            {
                using (HR_WorkWeekDataService workWeekDataService = new HR_WorkWeekDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<HR_WorkWeek> query = DbInstance.HR_WorkWeek.OrderByDescending(x => x.Id);
                    if (calendarYearId>0)
                    {
                        query = query.Where(q => q.CalendarYearId== calendarYearId);
                    }  
                 
                    var entities = workWeekDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<HR_WorkWeekJson>();
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
        private HttpListResult<HR_WorkWeekJson> GetAllWorkWeek()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_WorkWeekJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.WorkWeek.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.WorkWeek.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.WorkWeek.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_WorkWeekDataService workWeekDataService = new HR_WorkWeekDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<HR_WorkWeekJson>();
                    var entities = workWeekDataService.GetAll(out error);
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
        public HttpResult<HR_WorkWeekJson> GetWorkWeekById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_WorkWeekJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.WorkWeek.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.WorkWeek.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.WorkWeek.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_WorkWeekDataService workWeekDataService = new HR_WorkWeekDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new HR_WorkWeekJson();
                    HR_WorkWeek entity;
                    if (id <= 0)
                    {
                        entity = HR_WorkWeek.GetNew();
                    }
                    else
                    {
                        entity = workWeekDataService.GetById(id);
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
        private HttpResult<HR_WorkWeekJson> GetWorkWeekByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_WorkWeekJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.WorkWeek.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.WorkWeek.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.WorkWeek.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_WorkWeekDataService workWeekDataService = new HR_WorkWeekDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new HR_WorkWeekJson();
                    HR_WorkWeek entity;
                    if (id <= 0)
                    {
                        entity = HR_WorkWeek.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("HR_WorkWeek");
                        //includeTables.Add("HR_WorkWeek");

                        entity = workWeekDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<HR_WorkWeekJson> GetWorkWeekListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_WorkWeekJson>();
            try
            {
                //HR_WorkWeekDataService workWeekDataService =
                //    new HR_WorkWeekDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.DayEnumList = EnumUtil.GetEnumList(typeof(HR_WorkWeek.DayEnum));
                result.DataExtra.WorkingDayTypeEnumList = EnumUtil.GetEnumList(typeof(HR_WorkWeek.WorkingDayTypeEnum));
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
        public HttpListResult<HR_WorkWeekJson> GetWorkWeekDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_WorkWeekJson>();
            try
            {
                //HR_WorkWeekDataService workWeekDataService =
                //    new HR_WorkWeekDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.DayEnumList = EnumUtil.GetEnumList(typeof(HR_WorkWeek.DayEnum));
                result.DataExtra.WorkingDayTypeEnumList = EnumUtil.GetEnumList(typeof(HR_WorkWeek.WorkingDayTypeEnum));
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
        public HttpResult<HR_WorkWeekJson> SaveWorkWeek(HR_WorkWeekJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_WorkWeekJson>();
            //optional permission, check permission in the SaveWorkWeekLogic insted
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.WorkWeek.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.WorkWeek.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                 var entityReceived = new HR_WorkWeek();
                 var dbAttachedEntity = new HR_WorkWeek();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SaveWorkWeekLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<HR_WorkWeekJson> SaveWorkWeek2(HR_WorkWeekJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_WorkWeekJson>();
            try
            {
                using (HR_WorkWeekDataService workWeekDataService = new HR_WorkWeekDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new HR_WorkWeek();
                    var dbAttachedEntity = new HR_WorkWeek();
                    json.Map(entityReceived);
                    if (workWeekDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<HR_WorkWeekJson> SaveWorkWeekList(IList<HR_WorkWeekJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_WorkWeekJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.WorkWeek.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.WorkWeek.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_WorkWeekDataService workWeekDataService = new HR_WorkWeekDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<HR_WorkWeek>();
                    var dbAttachedListEntity = new List<HR_WorkWeek>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (workWeekDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<HR_WorkWeekJson> GetDeleteWorkWeekById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_WorkWeekJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.WorkWeek.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_WorkWeekDataService workWeekDataService = new HR_WorkWeekDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!workWeekDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveWorkWeek(HR_WorkWeek newObj, out string error)
        {
            error = "";
            if (newObj.DayEnumId==null)
            {
                error += "Please select valid Day.";
                return false;
            }
            if (newObj.IsHalfDay==null)
            {
                error += "Is Half Day required.";
                return false;
            }
            if (newObj.WorkingDayTypeEnumId==null)
            {
                error += "Please select valid Working Day Type.";
                return false;
            }
            if (newObj.CalendarYearId<=0)
            {
                error += "Please select valid Calendar Year.";
                return false;
            }
            if (newObj.IsDeleted==null)
            {
                error += "Is Deleted required.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.HR_WorkWeek.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A WorkWeek already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveWorkWeekLogic(HR_WorkWeek newObj,ref HR_WorkWeek dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Work Week to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveWorkWeek(newObj, out error))
                return false;

            bool isNewObject = true;
            HR_WorkWeek objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.HR_WorkWeek.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = HR_WorkWeek.GetNew(newObj.Id);
                DbInstance.HR_WorkWeek.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.HRArea.WorkWeek.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.HRArea.WorkWeek.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
           objToSave.DayEnumId =  newObj.DayEnumId ;
           objToSave.IsHalfDay =  newObj.IsHalfDay ;
           objToSave.WorkingDayTypeEnumId =  newObj.WorkingDayTypeEnumId ;
           objToSave.CalendarYearId =  newObj.CalendarYearId ;
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

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
using EMS.Framework.Objects;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases;
using EMS.Web.Framework.Bases.WebApi;
//using EMS.Web.Framework.Objects;
//using EMS.Web.Jsons.Mapper;
using EMS.Web.Jsons.Models;
using EMS.Framework.Permissions;

namespace EMS.Web.UI.Areas.Admin.Controllers.WebApi
{

    public class PoliceStationApiController : EmployeeBaseApiController
	{
        public PoliceStationApiController()
        {  }
       
        #region PoliceStation 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<General_PoliceStationJson> GetPagedPoliceStation(string textkey, int? pageSize, int? pageNo
           ,Int32 districtId= 0      
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<General_PoliceStationJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.PoliceStation.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.PoliceStation.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.PoliceStation.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            try
            {
                using (General_PoliceStationDataService policeStationDataService = new General_PoliceStationDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<General_PoliceStation> query = DbInstance.General_PoliceStation.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Name.ToLower().Contains(textkey.ToLower()));
                    }
                    if (districtId>0)
                    {
                        query = query.Where(q => q.DistrictId== districtId);
                    }  
                 
                    var entities = policeStationDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<General_PoliceStationJson>();
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
        private HttpListResult<General_PoliceStationJson> GetAllPoliceStation()
        {
            string error = string.Empty;
            var result = new HttpListResult<General_PoliceStationJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.PoliceStation.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.PoliceStation.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.PoliceStation.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            try
            {
                using (General_PoliceStationDataService policeStationDataService = new General_PoliceStationDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<General_PoliceStationJson>();
                    var entities = policeStationDataService.GetAll(out error);
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
        public HttpResult<General_PoliceStationJson> GetPoliceStationById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<General_PoliceStationJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.PoliceStation.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.PoliceStation.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.PoliceStation.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            try
            {
                using (General_PoliceStationDataService policeStationDataService = new General_PoliceStationDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new General_PoliceStationJson();
                    General_PoliceStation entity;
                    if (id <= 0)
                    {
                        entity = General_PoliceStation.GetNew();
                    }
                    else
                    {
                        entity = policeStationDataService.GetById(id);
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
        private HttpResult<General_PoliceStationJson> GetPoliceStationByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<General_PoliceStationJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.PoliceStation.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.PoliceStation.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.PoliceStation.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (General_PoliceStationDataService policeStationDataService = new General_PoliceStationDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new General_PoliceStationJson();
                    General_PoliceStation entity;
                    if (id <= 0)
                    {
                        entity = General_PoliceStation.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("General_PoliceStation");
                        //includeTables.Add("General_PoliceStation");

                        entity = policeStationDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<General_PoliceStationJson> GetPoliceStationListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<General_PoliceStationJson>();
            try
            {
                //  General_PoliceStationDataService policeStationDataService =
                //      new General_PoliceStationDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List

                //   result.DataExtra.StatusEnumList = EnumUtil.GetEnumList(typeof(General_PoliceStation.StatusEnum));
                //DropDown Option Tables

                //  result.DataExtra.DistrictList = DbInstance.General_District.AsEnumerable().Select(t => new
                //  { Id = t.Id,Name = t.Name }).ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<General_PoliceStationJson> GetPoliceStationDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<General_PoliceStationJson>();
            try
            {
                General_PoliceStationDataService policeStationDataService =
                    new General_PoliceStationDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                //result.DataExtra.StatusEnumList = EnumUtil.GetEnumList(typeof(General_PoliceStation.StatusEnum));
                //DropDown Option Tables
                 
                result.DataExtra.DistrictList = DbInstance.General_District.AsEnumerable().Select(t => new
                { Id = t.Id,Name = t.Name }).ToList();
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
        public HttpResult<General_PoliceStationJson> SavePoliceStation(General_PoliceStationJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<General_PoliceStationJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.PoliceStation.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.PoliceStation.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            try
            {
                 var entityReceived = new General_PoliceStation();
                 var dbAttachedEntity = new General_PoliceStation();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {
                    if (SavePoliceStationLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<General_PoliceStationJson> SavePoliceStation2(General_PoliceStationJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<General_PoliceStationJson>();
            try
            {
                using (General_PoliceStationDataService policeStationDataService = new General_PoliceStationDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new General_PoliceStation();
                    var dbAttachedEntity = new General_PoliceStation();
                    json.Map(entityReceived);
                    if (policeStationDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<General_PoliceStationJson> SavePoliceStationList(IList<General_PoliceStationJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<General_PoliceStationJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.PoliceStation.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.PoliceStation.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            try
            {
                using (General_PoliceStationDataService policeStationDataService = new General_PoliceStationDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<General_PoliceStation>();
                    var dbAttachedListEntity = new List<General_PoliceStation>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (policeStationDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<General_PoliceStationJson> GetDeletePoliceStationById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<General_PoliceStationJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.PoliceStation.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            try
            {
                using (General_PoliceStationDataService policeStationDataService = new General_PoliceStationDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!policeStationDataService.DeleteById(id, out error))
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
        private bool IsValidToSavePoliceStation(General_PoliceStation newObj, out string error)
        {
            error = "";
            if (!newObj.Name.IsValid())
            {
                error += "Name is not valid.";
                return false;
            }
            if (newObj.Name.Length > 256)
            {
                error += "Name exceeded its maximum length 256.";
                return false;
            }
            if (newObj.DistrictId <= 0)
            {
                error += "Please select valid District.";
                return false;
            }
            if (newObj.OrderNo == null)
            {
                error += "Order No required.";
                return false;
            }
            if (newObj.IsDeleted == null)
            {
                error += "Is Deleted required.";
                return false;
            }
            if (newObj.IsActive == null)
            {
                error += "Is Active required.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.General_PoliceStation.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
            //error = "A PoliceStation already exists!";
            //return false;
            //}
            return true;
        }
        private bool SavePoliceStationLogic(General_PoliceStation newObj, ref General_PoliceStation dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Police Station to save can't be null!";
                return false;
            }

            if (!IsValidToSavePoliceStation(newObj, out error))
                return false;

            bool isNewObject = true;
            General_PoliceStation objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.General_PoliceStation.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = General_PoliceStation.GetNew(newObj.Id);
                DbInstance.General_PoliceStation.Add(objToSave);
                objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
                objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }

            if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.PoliceStation.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.PoliceStation.CanEdit,
                   out error))
            {
                return false;
            }

            //binding object  
            objToSave.Name = newObj.Name;
            objToSave.DistrictId = newObj.DistrictId;
            objToSave.OrderNo = newObj.OrderNo;
            objToSave.IsDeleted = newObj.IsDeleted;
            objToSave.IsActive = newObj.IsActive;
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


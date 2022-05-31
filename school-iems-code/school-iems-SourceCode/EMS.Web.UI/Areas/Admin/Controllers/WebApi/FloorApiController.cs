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
using EMS.Framework.Objects;
using EMS.Framework.Permissions;
//using EMS.Web.Jsons.Mapper;
using EMS.Web.Jsons.Models;


namespace EMS.Web.UI.Areas.Admin.Controllers.WebApi
{

    public class FloorApiController : EmployeeBaseApiController
	{
        public FloorApiController()
        {  }
       
        #region Floor 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<General_FloorJson> GetPagedFloor(string textkey, int? pageSize, int? pageNo
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<General_FloorJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Floor.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Floor.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Floor.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (General_FloorDataService floorDataService = new General_FloorDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<General_Floor> query = DbInstance.General_Floor.OrderBy(x => x.FloorNo);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Name.ToLower().Contains(textkey.ToLower()));
                    }
                 
                    var entities = floorDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<General_FloorJson>();
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
        private HttpListResult<General_FloorJson> GetAllFloor()
        {
            string error = string.Empty;
            var result = new HttpListResult<General_FloorJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Floor.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Floor.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Floor.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (General_FloorDataService floorDataService = new General_FloorDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<General_FloorJson>();
                    var entities = floorDataService.GetAll(out error);
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
        public HttpResult<General_FloorJson> GetFloorById(int id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<General_FloorJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Floor.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Floor.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Floor.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (General_FloorDataService floorDataService = new General_FloorDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new General_FloorJson();
                    General_Floor entity;
                    if (id <= 0)
                    {
                        entity = General_Floor.GetNew();
                    }
                    else
                    {
                        entity = floorDataService.GetById(id);
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
        private HttpResult<General_FloorJson> GetFloorByIdWithChild(int id)
        {
            string error = string.Empty;
            var result = new HttpResult<General_FloorJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Floor.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Floor.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Floor.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (General_FloorDataService floorDataService = new General_FloorDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new General_FloorJson();
                    General_Floor entity;
                    if (id <= 0)
                    {
                        entity = General_Floor.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("General_Floor");
                        //includeTables.Add("General_Floor");

                        entity = floorDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<General_FloorJson> GetFloorListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<General_FloorJson>();
            try
            {
                //General_FloorDataService floorDataService =
                //    new General_FloorDataService(DbInstance, HttpUtil.Profile);
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
        public HttpListResult<General_FloorJson> GetFloorDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<General_FloorJson>();
            try
            {
                //General_FloorDataService floorDataService =
                //    new General_FloorDataService(DbInstance, HttpUtil.Profile);
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
        public HttpResult<General_FloorJson> SaveFloor(General_FloorJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<General_FloorJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Floor.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Floor.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                 var entityReceived = new General_Floor();
                 var dbAttachedEntity = new General_Floor();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {
                    if (SaveFloorLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<General_FloorJson> SaveFloor2(General_FloorJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<General_FloorJson>();
            try
            {
                using (General_FloorDataService floorDataService = new General_FloorDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new General_Floor();
                    var dbAttachedEntity = new General_Floor();
                    json.Map(entityReceived);
                    if (floorDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<General_FloorJson> SaveFloorList(IList<General_FloorJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<General_FloorJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Floor.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Floor.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (General_FloorDataService floorDataService = new General_FloorDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<General_Floor>();
                    var dbAttachedListEntity = new List<General_Floor>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (floorDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        private HttpResult<General_FloorJson> GetDeleteFloorById(int id)
        {
            string error = string.Empty;
            var result = new HttpResult<General_FloorJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Floor.CanDelete, out error)
                )
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (General_FloorDataService floorDataService = new General_FloorDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!floorDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveFloor(General_Floor newObj, out string error)
        {
            error = "";
            if (newObj.FloorNo==null)
            {
                error += "Floor No required.";
                return false;
            }
            if (!newObj.Name.IsValid())
            {
                error += "Name is not valid.";
                return false;
            }
            if (newObj.Name.Length>50)
            {
                error += "Name exceeded its maximum length 50.";
                return false;
            }
            if (newObj.IsDeleted==null)
            {
                error += "Is Deleted required.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.General_Floor.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A Floor already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveFloorLogic(General_Floor newObj,ref General_Floor dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Floor to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveFloor(newObj, out error))
                return false;

            bool isNewObject = true;
            General_Floor objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.General_Floor.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = General_Floor.GetNew(newObj.Id);
                DbInstance.General_Floor.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //checking permission, enable it with correction
            if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Floor.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Floor.CanEdit,
                   out error))
            {
                return false;
            }

            //binding object  
            objToSave.FloorNo =  newObj.FloorNo ;
           objToSave.Name =  newObj.Name ;
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

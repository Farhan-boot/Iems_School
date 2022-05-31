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

    public class BuildingApiController : EmployeeBaseApiController
	{
        public BuildingApiController()
        {  }
       
        #region Building 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<General_BuildingJson> GetPagedBuilding(string textkey, int? pageSize, int? pageNo
           ,Int64 campusId= 0      
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<General_BuildingJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Building.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Building.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Building.CanEdit, out error)
            )
            {
                result.HasError = true;
                result.Errors.Add(error);
                result.DataExtra.IsPermited = false;
                return result;
            }
            try
            {
                using (General_BuildingDataService buildingDataService = new General_BuildingDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<General_Building> query = DbInstance.General_Building
                    .Include(x=>x.General_Campus)
                    .OrderBy(x => x.CampusId)
                    .ThenBy(x=>x.OrderNo);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Name.ToLower().Contains(textkey.ToLower()));
                    }
                    if (campusId>0)
                    {
                        query = query.Where(q => q.CampusId== campusId);
                    }  
                 
                    var entities = buildingDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<General_BuildingJson>();
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
        private HttpListResult<General_BuildingJson> GetAllBuilding()
        {
            string error = string.Empty;
            var result = new HttpListResult<General_BuildingJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Building.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Building.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Building.CanEdit, out error)
            )
            {
                result.HasError = true;
                result.Errors.Add(error);
                result.DataExtra.IsPermited = false;
                return result;
            }
            try
            {
                using (General_BuildingDataService buildingDataService = new General_BuildingDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<General_BuildingJson>();
                    var entities = buildingDataService.GetAll(out error);
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
        public HttpResult<General_BuildingJson> GetBuildingById(long id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<General_BuildingJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Building.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Building.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Building.CanEdit, out error)
            )
            {
                result.HasError = true;
                result.Errors.Add(error);
                result.DataExtra.IsPermited = false;
                return result;
            }

            try
            {
                using (General_BuildingDataService buildingDataService = new General_BuildingDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new General_BuildingJson();
                    General_Building entity;
                    if (id <= 0)
                    {
                        entity = General_Building.GetNew();
                    }
                    else
                    {
                        entity = buildingDataService.GetById(id);
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
        private HttpResult<General_BuildingJson> GetBuildingByIdWithChild(long id)
        {
            string error = string.Empty;
            var result = new HttpResult<General_BuildingJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Building.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Building.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Building.CanEdit, out error)
            )
            {
                result.HasError = true;
                result.Errors.Add(error);
                result.DataExtra.IsPermited = false;
                return result;
            }
            try
            {
                using (General_BuildingDataService buildingDataService = new General_BuildingDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new General_BuildingJson();
                    General_Building entity;
                    if (id <= 0)
                    {
                        entity = General_Building.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("General_Building");
                        //includeTables.Add("General_Building");

                        entity = buildingDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<General_BuildingJson> GetBuildingListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<General_BuildingJson>();
            try
            {
                //General_BuildingDataService buildingDataService =
                //    new General_BuildingDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.StatusEnumList = EnumUtil.GetEnumList(typeof(General_Building.StatusEnum));
                //DropDown Option Tables

                result.DataExtra.CampusList = DbInstance.General_Campus.OrderBy(x => x.OrderNo).AsEnumerable().Select(t => new
                { Id = t.Id.ToString(), Name = t.Name, ShortName = t.ShortName }).ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<General_BuildingJson> GetBuildingDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<General_BuildingJson>();
            try
            {
                //General_BuildingDataService buildingDataService =
                //    new General_BuildingDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.StatusEnumList = EnumUtil.GetEnumList(typeof(General_Building.StatusEnum));
                //DropDown Option Tables

                result.DataExtra.CampusList = DbInstance.General_Campus.OrderBy(x=>x.OrderNo).AsEnumerable().Select(t => new
                { Id = t.Id.ToString(), Name = t.Name, ShortName = t.ShortName }).ToList();
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
        public HttpResult<General_BuildingJson> SaveBuilding(General_BuildingJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<General_BuildingJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Building.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Building.CanEdit, out error)
            )
            {
                result.HasError = true;
                result.Errors.Add(error);
                result.DataExtra.IsPermited = false;
                return result;
            }

            try
            {
                 var entityReceived = new General_Building();
                 var dbAttachedEntity = new General_Building();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {
                    if (SaveBuildingLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<General_BuildingJson> SaveBuilding2(General_BuildingJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<General_BuildingJson>();
            try
            {
                using (General_BuildingDataService buildingDataService = new General_BuildingDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new General_Building();
                    var dbAttachedEntity = new General_Building();
                    json.Map(entityReceived);
                    if (buildingDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<General_BuildingJson> SaveBuildingList(IList<General_BuildingJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<General_BuildingJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Building.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Building.CanEdit, out error)
            )
            {
                result.HasError = true;
                result.Errors.Add(error);
                result.DataExtra.IsPermited = false;
                return result;
            }
            try
            {
                using (General_BuildingDataService buildingDataService = new General_BuildingDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<General_Building>();
                    var dbAttachedListEntity = new List<General_Building>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (buildingDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<General_BuildingJson> GetDeleteBuildingById(long id)
        {
            string error = string.Empty;
            var result = new HttpResult<General_BuildingJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Building.CanDelete, out error)
                )
            {
                result.HasError = true;
                result.Errors.Add(error);
                result.DataExtra.IsPermited = false;
                return result;
            }
            try
            {
                using (General_BuildingDataService buildingDataService = new General_BuildingDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!buildingDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveBuilding(General_Building newObj, out string error)
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
            if (!newObj.ShortName.IsValid())
            {
                error += "Short Name is not valid.";
                return false;
            }
            if (newObj.ShortName.Length>50)
            {
                error += "Short Name exceeded its maximum length 50.";
                return false;
            }
            if (!newObj.Address.IsValid())
            {
                error += "Address is not valid.";
                return false;
            }
            if (newObj.Address.Length>256)
            {
                error += "Address exceeded its maximum length 256.";
                return false;
            }
            if (!newObj.MapEmbedCode.IsValid())
            {
                error += "Map Embed Code is not valid.";
                return false;
            }
            if (newObj.MapEmbedCode==null)
            {
                error += "Map Embed Code required.";
                return false;
            }
            if (newObj.OrderNo==null)
            {
                error += "Order No required.";
                return false;
            }
            if (newObj.NumberOfFloor==null)
            {
                error += "Number Of Floor required.";
                return false;
            }
            if (newObj.StatusEnumId==null)
            {
                error += "Please select valid Status.";
                return false;
            }
            if (newObj.CampusId<=0)
            {
                error += "Please select valid Campus.";
                return false;
            }
            if (!newObj.Remarks.IsValid())
            {
                error += "Remarks is not valid.";
                return false;
            }
            if (newObj.Remarks.Length>500)
            {
                error += "Remarks exceeded its maximum length 500.";
                return false;
            }
            if (newObj.IsDeleted==null)
            {
                error += "Is Deleted required.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.General_Building.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A Building already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveBuildingLogic(General_Building newObj,ref General_Building dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Building to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveBuilding(newObj, out error))
                return false;

            bool isNewObject = true;
            General_Building objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.General_Building.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            else
            {    
                newObj.Id = BigInt.NewBigInt();
            }

            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = General_Building.GetNew(newObj.Id);
                DbInstance.General_Building.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //checking permission, enable it with correction
            if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Building.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Building.CanEdit,
                   out error))
            {
                return false;
            }

            //binding object  
            objToSave.Name =  newObj.Name ;
           objToSave.ShortName =  newObj.ShortName ;
           objToSave.Address =  newObj.Address ;
           objToSave.MapEmbedCode =  newObj.MapEmbedCode ;
           objToSave.OrderNo =  newObj.OrderNo ;
           objToSave.NumberOfFloor =  newObj.NumberOfFloor ;
           objToSave.StatusEnumId =  newObj.StatusEnumId ;
           objToSave.CampusId =  newObj.CampusId ;
           objToSave.Remarks =  newObj.Remarks ;
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

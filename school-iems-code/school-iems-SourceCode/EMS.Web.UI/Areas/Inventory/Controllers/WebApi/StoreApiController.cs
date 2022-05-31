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


namespace EMS.Web.UI.Areas.Inventory.Controllers.WebApi
{

    public class StoreApiController : EmployeeBaseApiController
	{
        public StoreApiController()
        {  }
       
        #region Store 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Inv_StoreJson> GetPagedStore(string textkey, int? pageSize, int? pageNo
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Inv_StoreJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Store.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Store.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Store.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/

            try
            {
                using (Inv_StoreDataService storeDataService = new Inv_StoreDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<Inv_Store> query = DbInstance.Inv_Store.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Name.ToLower().Contains(textkey.ToLower()));
                    }
                 
                    var entities = storeDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Inv_StoreJson>();
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
        private HttpListResult<Inv_StoreJson> GetAllStore()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_StoreJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Store.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Store.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Store.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_StoreDataService storeDataService = new Inv_StoreDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<Inv_StoreJson>();
                    var entities = storeDataService.GetAll(out error);
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
        public HttpResult<Inv_StoreJson> GetStoreById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_StoreJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Store.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Store.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Store.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_StoreDataService storeDataService = new Inv_StoreDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Inv_StoreJson();
                    Inv_Store entity;
                    if (id <= 0)
                    {
                        entity = Inv_Store.GetNew();
                    }
                    else
                    {
                        entity = storeDataService.GetById(id);
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
        private HttpResult<Inv_StoreJson> GetStoreByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_StoreJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Store.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Store.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Store.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_StoreDataService storeDataService = new Inv_StoreDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Inv_StoreJson();
                    Inv_Store entity;
                    if (id <= 0)
                    {
                        entity = Inv_Store.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("Inv_Store");
                        //includeTables.Add("Inv_Store");

                        entity = storeDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Inv_StoreJson> GetStoreListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_StoreJson>();
            try
            {
                //Inv_StoreDataService storeDataService =
                //    new Inv_StoreDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List

                //DropDown Option Tables
                result.DataExtra.RoomList = DbInstance.Htl_Room.AsEnumerable().Select(t => new
                { Id = t.Id,Name = t.RoomNumber }).ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<Inv_StoreJson> GetStoreDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_StoreJson>();
            try
            {
                //Inv_StoreDataService storeDataService =
                //    new Inv_StoreDataService(DbInstance, HttpUtil.Profile);
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
        public HttpResult<Inv_StoreJson> SaveStore(Inv_StoreJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_StoreJson>();
            //optional permission, check permission in the SaveStoreLogic insted
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Store.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Store.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                 var entityReceived = new Inv_Store();
                 var dbAttachedEntity = new Inv_Store();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SaveStoreLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<Inv_StoreJson> SaveStore2(Inv_StoreJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_StoreJson>();
            try
            {
                using (Inv_StoreDataService storeDataService = new Inv_StoreDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new Inv_Store();
                    var dbAttachedEntity = new Inv_Store();
                    json.Map(entityReceived);
                    if (storeDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<Inv_StoreJson> SaveStoreList(IList<Inv_StoreJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_StoreJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Store.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Store.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_StoreDataService storeDataService = new Inv_StoreDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<Inv_Store>();
                    var dbAttachedListEntity = new List<Inv_Store>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (storeDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<Inv_StoreJson> GetDeleteStoreById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_StoreJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Store.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_StoreDataService storeDataService = new Inv_StoreDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!storeDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveStore(Inv_Store newObj, out string error)
        {
            error = "";
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
            if (newObj.Phone.IsValid() && newObj.Phone.Length>50)
            {
                error += "Phone exceeded its maximum length 50.";
                return false;
            }

            if (!newObj.Address.IsValid())
            {
                error += "Address is not valid.";
                return false;
            }
            if (newObj.Address==null)
            {
                error += "Address required.";
                return false;
            }
            if (!newObj.Description.IsValid())
            {
                error += "Description is not valid.";
                return false;
            }
            if (newObj.Description==null)
            {
                error += "Description required.";
                return false;
            }
            if (newObj.IsDeleted==null)
            {
                error += "Is Deleted required.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.Inv_Store.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A Store already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveStoreLogic(Inv_Store newObj,ref Inv_Store dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Store to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveStore(newObj, out error))
                return false;

            bool isNewObject = true;
            Inv_Store objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Inv_Store.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Inv_Store.GetNew(newObj.Id);
                DbInstance.Inv_Store.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Store.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Store.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
           objToSave.Name =  newObj.Name;
           objToSave.RoomId =  newObj.RoomId;
           objToSave.CampusId =  newObj.CampusId;
           objToSave.Phone =  newObj.Phone;
           objToSave.Details =  newObj.Details;
           objToSave.Address = newObj.Address;
           objToSave.Description = newObj.Description;
           objToSave.Remark = newObj.Remark;

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

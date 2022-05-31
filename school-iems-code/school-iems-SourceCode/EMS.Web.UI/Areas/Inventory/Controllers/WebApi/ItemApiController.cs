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

    public class ItemApiController : EmployeeBaseApiController
	{
        public ItemApiController()
        {  }
       
        #region Item 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Inv_ItemJson> GetPagedItem(string textkey, int? pageSize, int? pageNo
           ,Int32 categoryId= 0      
           ,Int32 storeId= 0      
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Inv_ItemJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Item.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Item.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Item.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/

            try
            {
                using (Inv_ItemDataService itemDataService = new Inv_ItemDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<Inv_Item> query = DbInstance.Inv_Item.OrderByDescending(x => x.Id);
                    if (categoryId>0)
                    {
                        query = query.Where(q => q.CategoryId== categoryId);
                    }  
                    if (storeId>0)
                    {
                        query = query.Where(q => q.StoreId== storeId);
                    }  
                 
                    var entities = itemDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Inv_ItemJson>();
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
        private HttpListResult<Inv_ItemJson> GetAllItem()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_ItemJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Item.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Item.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Item.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_ItemDataService itemDataService = new Inv_ItemDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<Inv_ItemJson>();
                    var entities = itemDataService.GetAll(out error);
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
        public HttpResult<Inv_ItemJson> GetItemById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ItemJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Item.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Item.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Item.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_ItemDataService itemDataService = new Inv_ItemDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Inv_ItemJson();
                    Inv_Item entity;
                    if (id <= 0)
                    {
                        entity = Inv_Item.GetNew();
                    }
                    else
                    {
                        entity = itemDataService.GetById(id);
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
        private HttpResult<Inv_ItemJson> GetItemByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ItemJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Item.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Item.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Item.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_ItemDataService itemDataService = new Inv_ItemDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Inv_ItemJson();
                    Inv_Item entity;
                    if (id <= 0)
                    {
                        entity = Inv_Item.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("Inv_Item");
                        //includeTables.Add("Inv_Item");

                        entity = itemDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Inv_ItemJson> GetItemListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_ItemJson>();
            try
            {
                //Inv_ItemDataService itemDataService =
                //    new Inv_ItemDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.AssetTypeEnumList = EnumUtil.GetEnumList(typeof(Inv_Item.AssetTypeEnum));
                //DropDown Option Tables
                 
                //result.DataExtra.ProductCategoryList = DbInstance.Inv_ProductCategory.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
                //result.DataExtra.StoreList = DbInstance.Inv_Store.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<Inv_ItemJson> GetItemDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_ItemJson>();
            try
            {
                //Inv_ItemDataService itemDataService =
                //    new Inv_ItemDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                //result.DataExtra.AssetTypeEnumList = EnumUtil.GetEnumList(typeof(Inv_Item.AssetTypeEnum));
                result.DataExtra.AssetTypeEnumList = EnumUtil.GetEnumList(typeof(Inv_ProductCategory.AssetTypeEnum));
                //DropDown Option Tables

                result.DataExtra.ProductCategoryList = DbInstance.Inv_ProductCategory.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.Name }).ToList();
                result.DataExtra.StoreList = DbInstance.Inv_Store.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.Name }).ToList();
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
        public HttpResult<Inv_ItemJson> SaveItem(Inv_ItemJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ItemJson>();
            //optional permission, check permission in the SaveItemLogic insted
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Item.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Item.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                 var entityReceived = new Inv_Item();
                 var dbAttachedEntity = new Inv_Item();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SaveItemLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<Inv_ItemJson> SaveItem2(Inv_ItemJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ItemJson>();
            try
            {
                using (Inv_ItemDataService itemDataService = new Inv_ItemDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new Inv_Item();
                    var dbAttachedEntity = new Inv_Item();
                    json.Map(entityReceived);
                    if (itemDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<Inv_ItemJson> SaveItemList(IList<Inv_ItemJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_ItemJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Item.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Item.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_ItemDataService itemDataService = new Inv_ItemDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<Inv_Item>();
                    var dbAttachedListEntity = new List<Inv_Item>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (itemDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<Inv_ItemJson> GetDeleteItemById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ItemJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Item.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_ItemDataService itemDataService = new Inv_ItemDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!itemDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveItem(Inv_Item newObj, out string error)
        {
            error = "";




            if (newObj.IsDeleted==null)
            {
                error += "Is Deleted required.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.Inv_Item.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A Item already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveItemLogic(Inv_Item newObj,ref Inv_Item dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Item to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveItem(newObj, out error))
                return false;

            bool isNewObject = true;
            Inv_Item objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Inv_Item.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Inv_Item.GetNew(newObj.Id);
                DbInstance.Inv_Item.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Item.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Item.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
           objToSave.CategoryId =  newObj.CategoryId ;
           objToSave.StoreId =  newObj.StoreId ;
           objToSave.ItemStatus =  newObj.ItemStatus ;
           objToSave.AssetTypeEnumId =  newObj.AssetTypeEnumId ;
           objToSave.IsDeleted =  newObj.IsDeleted ;
           objToSave.LastChangedById = newObj.LastChangedById = HttpUtil.Profile.UserId;
            objToSave.LastChanged = newObj.LastChanged = DateTime.Now;
            objToSave.ItemName = newObj.ItemName;
           dbAddedObj = objToSave;
           
            //here save any child table
            return true;
        }
        #endregion
        #endregion

	}
}

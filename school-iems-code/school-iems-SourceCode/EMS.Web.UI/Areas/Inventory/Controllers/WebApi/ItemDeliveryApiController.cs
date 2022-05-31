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

    public class ItemDeliveryApiController : EmployeeBaseApiController
	{
        public ItemDeliveryApiController()
        {  }

        #region ItemDelivery 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Inv_ItemDeliveryJson> GetPagedItemDelivery(string textkey, int? pageSize, int? pageNo
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Inv_ItemDeliveryJson>();
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
                //using (Inv_StoreDataService storeDataService = new Inv_StoreDataService(DbInstance, HttpUtil.Profile))
                //{
                  IQueryable<Inv_ItemDelivery> query = DbInstance.Inv_ItemDelivery.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Note.ToLower().Contains(textkey.ToLower()));
                    }
                 
                    var entities = GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Inv_ItemDeliveryJson>();
                    entities.Map(jsons);

                    result.Data = jsons;
                    result.Count = count;
               //}
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
        private HttpListResult<Inv_ItemDeliveryJson> GetAllItemDelivery()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_ItemDeliveryJson>();
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
                //using (Inv_StoreDataService storeDataService = new Inv_StoreDataService(DbInstance, HttpUtil.Profile))
                //{
                    var jsons = new List<Inv_ItemDeliveryJson>();
                    var entities = DbInstance.Inv_ItemDelivery.ToList();// storeDataService.GetAll(out error);
                    entities.Map(jsons);
                    result.Data = jsons;
                    result.Count = jsons.Count;
                //}
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpResult<Inv_ItemDeliveryJson> GetItemDeliveryById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ItemDeliveryJson>();
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
                //using (Inv_StoreDataService storeDataService = new Inv_StoreDataService(DbInstance, HttpUtil.Profile))
                //{
                    var json = new Inv_ItemDeliveryJson();
                Inv_ItemDelivery entity;
                    if (id <= 0)
                    {
                        entity = Inv_ItemDelivery.GetNew();
                    }
                    else
                    {
                        entity = DbInstance.Inv_ItemDelivery.SingleOrDefault(x => x.Id == id);//  storeDataService.GetById(id);
                    }
                    entity.Map(json);
                    result.Data = json;


                //}
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        //private HttpResult<Inv_ItemDeliveryJson> GetItemDeliveryByIdWithChild(Int32 id)
        //{
        //    string error = string.Empty;
        //    var result = new HttpResult<Inv_ItemDeliveryJson>();
        //    //todo fix the permission
        //    /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Store.CanView, out error)
        //        && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Store.CanAdd, out error)
        //        && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Store.CanEdit, out error))
        //    {
        //        result.HasError = true;
        //        result.HasViewPermission = false;
        //        result.Errors.Add(error);
        //        return result;
        //    }*/
        //    try
        //    {
        //        //using (Inv_StoreDataService storeDataService = new Inv_StoreDataService(DbInstance, HttpUtil.Profile))
        //        //{
        //            var json = new Inv_ItemDeliveryJson();
        //            Inv_ItemDelivery entity;
        //            if (id <= 0)
        //            {
        //                entity = Inv_ItemDelivery.GetNew();
        //            }
        //            else
        //            {
        //                List<string> includeTables = new List<string>();
        //                //Todo Implement latter
        //                //includeTables.Add("Inv_Store");
        //                //includeTables.Add("Inv_Store");

        //                entity = storeDataService.GetById(id, includeTables.ToArray());
        //            }
        //            entity.Map(json);
        //            result.Data = json;
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        result.HasError = true;
        //        result.Errors.Add(ex.GetBaseException().Message.ToString());
        //    }
        //    return result;
        //}

        public HttpListResult<Inv_ItemDeliveryJson> GetItemDeliveryListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_ItemDeliveryJson>();
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
        public HttpListResult<Inv_ItemDeliveryJson> GetItemDeliveryDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_ItemDeliveryJson>();
            try
            {
                //Inv_StoreDataService storeDataService =
                //    new Inv_StoreDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List

                //DropDown Option Tables
                //result.DataExtra.ItemAllocationList = DbInstance.Inv_ItemAllocation.AsEnumerable().Select(t => new
                //    { Id = t.Id, Name = t.Name }).ToList();

                result.DataExtra.DeliveredList = DbInstance.User_Account.AsEnumerable().Select(t => new
                    { Id = t.Id, Name = t.FullName }).ToList();

                result.DataExtra.DepartmentList = DbInstance.HR_Department.AsEnumerable().Select(t => new
                    { Id = t.Id, Name = t.ShortName }).ToList();

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
        public HttpResult<Inv_ItemDeliveryJson> SaveItemDelivery(Inv_ItemDeliveryJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ItemDeliveryJson>();
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
                 var entityReceived = new Inv_ItemDelivery();
                 var dbAttachedEntity = new Inv_ItemDelivery();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SaveItemDeliveryLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<Inv_ItemDeliveryJson> SaveItemDeliveryList(IList<Inv_ItemDeliveryJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_ItemDeliveryJson>();
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
                //using (Inv_StoreDataService storeDataService = new Inv_StoreDataService(DbInstance, HttpUtil.Profile))
                //{
                    var entityListReceived = new List<Inv_ItemDelivery>();
                    var dbAttachedListEntity = new List<Inv_ItemDelivery>();
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
               // }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpResult<Inv_ItemDeliveryJson> GetDeleteItemDeliveryById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ItemDeliveryJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Store.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                //using (Inv_StoreDataService storeDataService = new Inv_StoreDataService(DbInstance, HttpUtil.Profile))
                //{
                // if (!storeDataService.DeleteById(id, out error))
                //{
                if (id > 0)
                {
                    var deleteObject = DbInstance.Inv_ItemDelivery.SingleOrDefault(x => x.Id == id);
                    DbInstance.Inv_ItemDelivery.Remove(deleteObject);
                    DbInstance.SaveChanges();
                    result.HasError = false;
                    result.Errors.Add(error);
                }
                // }
                //}
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
        private bool IsValidToSaveItemDelivery(Inv_ItemDelivery newObj, out string error)
        {
            error = "";
            if (!newObj.Note.IsValid())
            {
                error += "Note is not valid.";
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
        private bool SaveItemDeliveryLogic(Inv_ItemDelivery newObj,ref Inv_ItemDelivery dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Item Delivery to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveItemDelivery(newObj, out error))
                return false;

            bool isNewObject = true;
            Inv_ItemDelivery objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Inv_ItemDelivery.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Inv_ItemDelivery.GetNew(newObj.Id);
                DbInstance.Inv_ItemDelivery.Add(objToSave);
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
           objToSave.ItemAllocationId =  newObj.ItemAllocationId;
           objToSave.DeliveredQuantity =  newObj.DeliveredQuantity;
           objToSave.DeliveredDate =  newObj.DeliveredDate;
           objToSave.DeliveredTo =  newObj.DeliveredTo;
           objToSave.Note =  newObj.Note;
           objToSave.ItemStockId = newObj.ItemStockId;
           objToSave.ReceivedByBPId = newObj.ReceivedByBPId;
           objToSave.ReceivedByName = newObj.ReceivedByName;
           objToSave.ReceivedByMobile = newObj.ReceivedByMobile;
           objToSave.ReceivedByRankId = newObj.ReceivedByRankId;
           objToSave.ReceivedByDepartmentId = newObj.ReceivedByDepartmentId;
           objToSave.ReceivedByAreaId = newObj.ReceivedByAreaId;

           objToSave.IsDeleted =  newObj.IsDeleted ;
           objToSave.LastChangedById = newObj.LastChangedById = HttpUtil.Profile.UserId;
           objToSave.LastChanged = newObj.LastChanged = DateTime.Now;
           

            dbAddedObj = objToSave;
           
            //here save any child table
            return true;
        }
        #endregion
        #endregion


        //Get Paged List
        public List<Inv_ItemDelivery> GetPagedList(IQueryable<Inv_ItemDelivery> query, int? pageSize, int? pageNo, ref int count, out string error)
        {
            error = "";
            try
            {
                if (query == null)
                {
                    query = DbInstance.Inv_ItemDelivery;
                }
                count = query.Count();
                if (pageNo.HasValue && pageSize.HasValue && pageNo.Value >= 0 && pageSize.Value >= 0)
                {
                    if (pageNo <= 1)
                    {
                        pageNo = 0;
                    }
                    else
                    {
                        pageNo = pageNo - 1;
                    }

                    query = (from q in query
                            select q)
                        .Skip(pageNo.Value * pageSize.Value)
                        .Take(pageSize.Value);
                }
                return query.ToList();
            }
            catch (Exception ex)
            {
                error = ex.ToString();
            }
            return null;
        }
    }
}

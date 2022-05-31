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

    public class RequestedItemApiController : EmployeeBaseApiController
    {
        public RequestedItemApiController()
        { }

        #region Requisition 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Inv_RequestedItemJson> GetPagedRequestedItem(string textkey, int? pageSize, int? pageNo
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Inv_RequestedItemJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Requisition.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Requisition.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Requisition.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/

            try
            {
                //using (Inv_RequisitionDataService requisitionDataService = new Inv_RequisitionDataService(DbInstance, HttpUtil.Profile))
                //{
                IQueryable<Inv_RequestedItem> query = DbInstance.Inv_RequestedItem.OrderByDescending(x => x.Id);
                if (textkey.IsValid())
                {
                    query = query.Where(q => q.Remark.ToLower().Contains(textkey.ToLower()));
                }

                var entities = GetPagedList(query, pageSize, pageNo, ref count, out error);
                var jsons = new List<Inv_RequestedItemJson>();
                entities.Map(jsons);

                result.Data = jsons;
                result.Count = count;
                // }
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
        private HttpListResult<Inv_RequestedItemJson> GetAllRequestedItem()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_RequestedItemJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Requisition.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Requisition.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Requisition.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                // using (Inv_RequisitionDataService requisitionDataService = new Inv_RequisitionDataService(DbInstance, HttpUtil.Profile))
                //{
                var jsons = new List<Inv_RequestedItemJson>();
                var entities = DbInstance.Inv_RequestedItem.ToList();
                entities.Map(jsons);
                result.Data = jsons;
                result.Count = jsons.Count;
                // }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }

        public HttpResult<Inv_RequestedItemJson> GetRequestedItemById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_RequestedItemJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Requisition.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Requisition.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Requisition.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                //using (Inv_RequisitionDataService requisitionDataService = new Inv_RequisitionDataService(DbInstance, HttpUtil.Profile))
                // {
                var json = new Inv_RequestedItemJson();
                Inv_RequestedItem entity;
                if (id <= 0)
                {
                    entity = Inv_RequestedItem.GetNew();
                }
                else
                {
                    entity = DbInstance.Inv_RequestedItem.SingleOrDefault(x => x.Id == id);
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

        //private HttpResult<Inv_RequestedItemJson> GetRequestedItemByIdWithChild(Int32 id)
        //{
        //    string error = string.Empty;
        //    var result = new HttpResult<Inv_RequestedItemJson>();
        //    //todo fix the permission
        //    /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Requisition.CanView, out error)
        //        && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Requisition.CanAdd, out error)
        //        && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Requisition.CanEdit, out error))
        //    {
        //        result.HasError = true;
        //        result.HasViewPermission = false;
        //        result.Errors.Add(error);
        //        return result;
        //    }*/
        //    try
        //    {
        //        //using (Inv_RequisitionDataService requisitionDataService = new Inv_RequisitionDataService(DbInstance, HttpUtil.Profile))
        //        //{
        //            var json = new Inv_RequestedItemJson();
        //        Inv_RequestedItem entity;
        //            if (id <= 0)
        //            {
        //                entity = Inv_RequestedItem.GetNew();
        //            }
        //            else
        //            {
        //                List<string> includeTables = new List<string>();
        //                //Todo Implement latter
        //                //includeTables.Add("Inv_Requisition");
        //                //includeTables.Add("Inv_Requisition");

        //                entity = requisitionDataService.GetById(id, includeTables.ToArray());
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


        public HttpListResult<Inv_RequestedItemJson> GetRequestedItemListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_RequestedItemJson>();
            try
            {
                //Inv_RequisitionDataService requisitionDataService =
                //    new Inv_RequisitionDataService(DbInstance, HttpUtil.Profile);
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

        public HttpListResult<Inv_RequestedItemJson> GetRequestedItemDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_RequestedItemJson>();
            try
            {
                //Inv_RequisitionDataService requisitionDataService =
                //    new Inv_RequisitionDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List

                //DropDown Option Tables

                result.DataExtra.RequisitionList = DbInstance.Inv_Requisition.AsEnumerable().Select(t => new
                    { Id = t.Id, Name = t.ApprovedByAdminName }).ToList();

                result.DataExtra.CategoryList = DbInstance.Inv_ProductCategory.AsEnumerable().Select(t => new
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
        public HttpResult<Inv_RequestedItemJson> SaveRequestedItem(Inv_RequestedItemJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_RequestedItemJson>();
            //optional permission, check permission in the SaveRequisitionLogic insted
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Requisition.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Requisition.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                var entityReceived = new Inv_RequestedItem();
                var dbAttachedEntity = new Inv_RequestedItem();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SaveRequestedItemLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<Inv_RequisitionJson> SaveRequisition2(Inv_RequisitionJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_RequisitionJson>();
            try
            {
                using (Inv_RequisitionDataService requisitionDataService = new Inv_RequisitionDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new Inv_Requisition();
                    var dbAttachedEntity = new Inv_Requisition();
                    json.Map(entityReceived);
                    if (requisitionDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<Inv_RequestedItemJson> SaveRequestedItemList(IList<Inv_RequestedItemJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_RequestedItemJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Requisition.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Requisition.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                //using (Inv_RequisitionDataService requisitionDataService = new Inv_RequisitionDataService(DbInstance, HttpUtil.Profile))
                //{
                var entityListReceived = new List<Inv_RequestedItem>();
                var dbAttachedListEntity = new List<Inv_RequestedItem>();
                jsonList.Map(entityListReceived);

                //foreach (var entity in entityListReceived)
                //{

                //}
                //if (requisitionDataService.Save(entity, ref dbAttachedListEntity, out error))
                //{
                //    dbAttachedListEntity.Map(jsonList);
                //    result.Data = jsonList;//return object
                //}
                //else
                //{
                //    result.HasError = true;
                //    result.Errors.Add(error);
                //}
                //}
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }

        public HttpResult<Inv_RequestedItemJson> GetDeleteRequestedItemById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_RequestedItemJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Requisition.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                //using (Inv_RequisitionDataService requisitionDataService = new Inv_RequisitionDataService(DbInstance, HttpUtil.Profile))
                //{
                //if (!requisitionDataService.DeleteById(id, out error))
                //{
                if (id > 0)
                {
                    var deleteObject = DbInstance.Inv_RequestedItem.SingleOrDefault(x => x.Id == id);
                    DbInstance.Inv_RequestedItem.Remove(deleteObject);
                    DbInstance.SaveChanges();
                    result.HasError = false;
                    result.Errors.Add(error);
                }
              
                //}
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
        private bool IsValidToSaveRequestedItem(Inv_RequestedItem newObj, out string error)
        {
            error = "";

            if (newObj.Remark.IsValid() && newObj.Remark.Length > 500)
            {
                error += "Remark exceeded its maximum length 500.";
                return false;
            }

            

            if (newObj.IsDeleted == null)
            {
                error += "Is Deleted required.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.Inv_Requisition.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
            //error = "A Requisition already exists!";
            //return false;
            //}
            return true;
        }
        private bool SaveRequestedItemLogic(Inv_RequestedItem newObj, ref Inv_RequestedItem dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Requested Item to save can't be null!";
                return false;
            }

            if (!IsValidToSaveRequestedItem(newObj, out error))
                return false;

            bool isNewObject = true;
            Inv_RequestedItem objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Inv_RequestedItem.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Inv_RequestedItem.GetNew(newObj.Id);
                DbInstance.Inv_RequestedItem.Add(objToSave);
                objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
                objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Requisition.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Requisition.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
            objToSave.RequisitionId = newObj.RequisitionId;
            objToSave.CatagoryId = newObj.CatagoryId;
            objToSave.Quantity = newObj.Quantity;
            objToSave.ItemStatus = newObj.ItemStatus;
            objToSave.Remark = newObj.Remark;
            objToSave.ItemCategory = newObj.ItemCategory;
            objToSave.IsDeleted = newObj.IsDeleted;
            objToSave.LastChangedById = newObj.LastChangedById = HttpUtil.Profile.UserId;
            objToSave.LastChanged = newObj.LastChanged = DateTime.Now;
            dbAddedObj = objToSave;

            //here save any child table
            return true;
        }
        #endregion
        #endregion


        //Get Paged List
        public List<Inv_RequestedItem> GetPagedList(IQueryable<Inv_RequestedItem> query, int? pageSize, int? pageNo, ref int count, out string error)
        {
            error = "";
            try
            {
                if (query == null)
                {
                    query = DbInstance.Inv_RequestedItem;
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

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

    public class PurchaseRequisitionDetailApiController : EmployeeBaseApiController
	{
        public PurchaseRequisitionDetailApiController()
        {  }
       
        #region PurchaseRequisitionDetail 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Inv_PurchaseRequisitionDetailJson> GetPagedPurchaseRequisitionDetail(string textkey, int? pageSize, int? pageNo
           ,Int32 productCategoryId= 0      
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Inv_PurchaseRequisitionDetailJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.PurchaseRequisitionDetail.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.PurchaseRequisitionDetail.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.PurchaseRequisitionDetail.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/

            try
            {
                using (Inv_PurchaseRequisitionDetailDataService purchaseRequisitionDetailDataService = new Inv_PurchaseRequisitionDetailDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<Inv_PurchaseRequisitionDetail> query = DbInstance.Inv_PurchaseRequisitionDetail.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.ItemName.ToLower().Contains(textkey.ToLower()));
                    }
                    if (productCategoryId>0)
                    {
                        query = query.Where(q => q.ProductCategoryId== productCategoryId);
                    }  
                 
                    var entities = purchaseRequisitionDetailDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Inv_PurchaseRequisitionDetailJson>();
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
        private HttpListResult<Inv_PurchaseRequisitionDetailJson> GetAllPurchaseRequisitionDetail()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_PurchaseRequisitionDetailJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.PurchaseRequisitionDetail.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.PurchaseRequisitionDetail.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.PurchaseRequisitionDetail.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_PurchaseRequisitionDetailDataService purchaseRequisitionDetailDataService = new Inv_PurchaseRequisitionDetailDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<Inv_PurchaseRequisitionDetailJson>();
                    var entities = purchaseRequisitionDetailDataService.GetAll(out error);
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
        public HttpResult<Inv_PurchaseRequisitionDetailJson> GetPurchaseRequisitionDetailById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_PurchaseRequisitionDetailJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.PurchaseRequisitionDetail.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.PurchaseRequisitionDetail.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.PurchaseRequisitionDetail.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_PurchaseRequisitionDetailDataService purchaseRequisitionDetailDataService = new Inv_PurchaseRequisitionDetailDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Inv_PurchaseRequisitionDetailJson();
                    Inv_PurchaseRequisitionDetail entity;
                    if (id <= 0)
                    {
                        entity = Inv_PurchaseRequisitionDetail.GetNew();
                    }
                    else
                    {
                        entity = purchaseRequisitionDetailDataService.GetById(id);
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
        private HttpResult<Inv_PurchaseRequisitionDetailJson> GetPurchaseRequisitionDetailByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_PurchaseRequisitionDetailJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.PurchaseRequisitionDetail.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.PurchaseRequisitionDetail.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.PurchaseRequisitionDetail.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_PurchaseRequisitionDetailDataService purchaseRequisitionDetailDataService = new Inv_PurchaseRequisitionDetailDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Inv_PurchaseRequisitionDetailJson();
                    Inv_PurchaseRequisitionDetail entity;
                    if (id <= 0)
                    {
                        entity = Inv_PurchaseRequisitionDetail.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("Inv_PurchaseRequisitionDetail");
                        //includeTables.Add("Inv_PurchaseRequisitionDetail");

                        entity = purchaseRequisitionDetailDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Inv_PurchaseRequisitionDetailJson> GetPurchaseRequisitionDetailListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_PurchaseRequisitionDetailJson>();
            try
            {
                //Inv_PurchaseRequisitionDetailDataService purchaseRequisitionDetailDataService =
                //    new Inv_PurchaseRequisitionDetailDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.UnitTypeEnumList = EnumUtil.GetEnumList(typeof(Inv_PurchaseRequisitionDetail.UnitTypeEnum));
                //DropDown Option Tables
                 
                //result.DataExtra.ProductCategoryList = DbInstance.Inv_ProductCategory.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<Inv_PurchaseRequisitionDetailJson> GetPurchaseRequisitionDetailDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_PurchaseRequisitionDetailJson>();
            try
            {
                //Inv_PurchaseRequisitionDetailDataService purchaseRequisitionDetailDataService =
                //    new Inv_PurchaseRequisitionDetailDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.UnitTypeEnumList = EnumUtil.GetEnumList(typeof(Inv_PurchaseRequisitionDetail.UnitTypeEnum));
                //DropDown Option Tables

                result.DataExtra.ProductCategoryList = DbInstance.Inv_ProductCategory.AsEnumerable().Select(t => new
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
        public HttpResult<Inv_PurchaseRequisitionDetailJson> SavePurchaseRequisitionDetail(Inv_PurchaseRequisitionDetailJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_PurchaseRequisitionDetailJson>();
            //optional permission, check permission in the SavePurchaseRequisitionDetailLogic insted
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.PurchaseRequisitionDetail.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.PurchaseRequisitionDetail.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                 var entityReceived = new Inv_PurchaseRequisitionDetail();
                 var dbAttachedEntity = new Inv_PurchaseRequisitionDetail();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SavePurchaseRequisitionDetailLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<Inv_PurchaseRequisitionDetailJson> SavePurchaseRequisitionDetail2(Inv_PurchaseRequisitionDetailJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_PurchaseRequisitionDetailJson>();
            try
            {
                using (Inv_PurchaseRequisitionDetailDataService purchaseRequisitionDetailDataService = new Inv_PurchaseRequisitionDetailDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new Inv_PurchaseRequisitionDetail();
                    var dbAttachedEntity = new Inv_PurchaseRequisitionDetail();
                    json.Map(entityReceived);
                    if (purchaseRequisitionDetailDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<Inv_PurchaseRequisitionDetailJson> SavePurchaseRequisitionDetailList(IList<Inv_PurchaseRequisitionDetailJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_PurchaseRequisitionDetailJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.PurchaseRequisitionDetail.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.PurchaseRequisitionDetail.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_PurchaseRequisitionDetailDataService purchaseRequisitionDetailDataService = new Inv_PurchaseRequisitionDetailDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<Inv_PurchaseRequisitionDetail>();
                    var dbAttachedListEntity = new List<Inv_PurchaseRequisitionDetail>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (purchaseRequisitionDetailDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<Inv_PurchaseRequisitionDetailJson> GetDeletePurchaseRequisitionDetailById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_PurchaseRequisitionDetailJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.PurchaseRequisitionDetail.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_PurchaseRequisitionDetailDataService purchaseRequisitionDetailDataService = new Inv_PurchaseRequisitionDetailDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!purchaseRequisitionDetailDataService.DeleteById(id, out error))
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
        private bool IsValidToSavePurchaseRequisitionDetail(Inv_PurchaseRequisitionDetail newObj, out string error)
        {
            error = "";
            if (newObj.ItemName.IsValid() && newObj.ItemName.Length>50)
            {
                error += "Item Name exceeded its maximum length 50.";
                return false;
            }

            if (newObj.Detail.IsValid() && newObj.Detail.Length>500)
            {
                error += "Detail exceeded its maximum length 500.";
                return false;
            }


            if (newObj.PurchaseRequisitionId==null)
            {
                error += "Purchase Requisition Id required.";
                return false;
            }
            if (newObj.UnitTypeEnumId==null)
            {
                error += "Please select valid Unit Type.";
                return false;
            }
            if (newObj.IsDeleted==null)
            {
                error += "Is Deleted required.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.Inv_PurchaseRequisitionDetail.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A PurchaseRequisitionDetail already exists!";
                //return false;
            //}
            return true;
        }
        private bool SavePurchaseRequisitionDetailLogic(Inv_PurchaseRequisitionDetail newObj,ref Inv_PurchaseRequisitionDetail dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Purchase Requisition Detail to save can't be null!";
                return false;
            }

            if ( !IsValidToSavePurchaseRequisitionDetail(newObj, out error))
                return false;

            bool isNewObject = true;
            Inv_PurchaseRequisitionDetail objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Inv_PurchaseRequisitionDetail.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Inv_PurchaseRequisitionDetail.GetNew(newObj.Id);
                DbInstance.Inv_PurchaseRequisitionDetail.Add(objToSave);
               
               
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.PurchaseRequisitionDetail.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.PurchaseRequisitionDetail.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
           objToSave.ItemName =  newObj.ItemName ;
           objToSave.ProductCategoryId =  newObj.ProductCategoryId ;
           objToSave.Detail =  newObj.Detail ;
           objToSave.Quantity =  newObj.Quantity ;
           objToSave.UnitPrice =  newObj.UnitPrice ;
           objToSave.PurchaseRequisitionId =  newObj.PurchaseRequisitionId ;
           objToSave.UnitTypeEnumId =  newObj.UnitTypeEnumId ;
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

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

    public class PurchaseRequisitionApiController : EmployeeBaseApiController
	{
        public PurchaseRequisitionApiController()
        {  }
       
        #region PurchaseRequisition 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Inv_PurchaseRequisitionJson> GetPagedPurchaseRequisition(string textkey, int? pageSize, int? pageNo
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Inv_PurchaseRequisitionJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.PurchaseRequisition.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.PurchaseRequisition.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.PurchaseRequisition.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/

            try
            {
                using (Inv_PurchaseRequisitionDataService purchaseRequisitionDataService = new Inv_PurchaseRequisitionDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<Inv_PurchaseRequisition> query = DbInstance.Inv_PurchaseRequisition.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Purpose.ToLower().Contains(textkey.ToLower()));
                    }
                 
                    var entities = purchaseRequisitionDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Inv_PurchaseRequisitionJson>();
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
        private HttpListResult<Inv_PurchaseRequisitionJson> GetAllPurchaseRequisition()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_PurchaseRequisitionJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.PurchaseRequisition.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.PurchaseRequisition.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.PurchaseRequisition.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_PurchaseRequisitionDataService purchaseRequisitionDataService = new Inv_PurchaseRequisitionDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<Inv_PurchaseRequisitionJson>();
                    var entities = purchaseRequisitionDataService.GetAll(out error);
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
        public HttpResult<Inv_PurchaseRequisitionJson> GetPurchaseRequisitionById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_PurchaseRequisitionJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.PurchaseRequisition.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.PurchaseRequisition.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.PurchaseRequisition.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_PurchaseRequisitionDataService purchaseRequisitionDataService = new Inv_PurchaseRequisitionDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Inv_PurchaseRequisitionJson();
                    Inv_PurchaseRequisition entity;
                    if (id <= 0)
                    {
                        entity = Inv_PurchaseRequisition.GetNew();
                    }
                    else
                    {
                        entity = purchaseRequisitionDataService.GetById(id);
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
        private HttpResult<Inv_PurchaseRequisitionJson> GetPurchaseRequisitionByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_PurchaseRequisitionJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.PurchaseRequisition.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.PurchaseRequisition.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.PurchaseRequisition.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_PurchaseRequisitionDataService purchaseRequisitionDataService = new Inv_PurchaseRequisitionDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Inv_PurchaseRequisitionJson();
                    Inv_PurchaseRequisition entity;
                    if (id <= 0)
                    {
                        entity = Inv_PurchaseRequisition.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("Inv_PurchaseRequisition");
                        //includeTables.Add("Inv_PurchaseRequisition");

                        entity = purchaseRequisitionDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Inv_PurchaseRequisitionJson> GetPurchaseRequisitionListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_PurchaseRequisitionJson>();
            try
            {
                //Inv_PurchaseRequisitionDataService purchaseRequisitionDataService =
                //    new Inv_PurchaseRequisitionDataService(DbInstance, HttpUtil.Profile);
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
        public HttpListResult<Inv_PurchaseRequisitionJson> GetPurchaseRequisitionDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_PurchaseRequisitionJson>();
            try
            {
                //Inv_PurchaseRequisitionDataService purchaseRequisitionDataService =
                //    new Inv_PurchaseRequisitionDataService(DbInstance, HttpUtil.Profile);
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
        public HttpResult<Inv_PurchaseRequisitionJson> SavePurchaseRequisition(Inv_PurchaseRequisitionJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_PurchaseRequisitionJson>();
            //optional permission, check permission in the SavePurchaseRequisitionLogic insted
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.PurchaseRequisition.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.PurchaseRequisition.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                 var entityReceived = new Inv_PurchaseRequisition();
                 var dbAttachedEntity = new Inv_PurchaseRequisition();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SavePurchaseRequisitionLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<Inv_PurchaseRequisitionJson> SavePurchaseRequisition2(Inv_PurchaseRequisitionJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_PurchaseRequisitionJson>();
            try
            {
                using (Inv_PurchaseRequisitionDataService purchaseRequisitionDataService = new Inv_PurchaseRequisitionDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new Inv_PurchaseRequisition();
                    var dbAttachedEntity = new Inv_PurchaseRequisition();
                    json.Map(entityReceived);
                    if (purchaseRequisitionDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<Inv_PurchaseRequisitionJson> SavePurchaseRequisitionList(IList<Inv_PurchaseRequisitionJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_PurchaseRequisitionJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.PurchaseRequisition.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.PurchaseRequisition.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_PurchaseRequisitionDataService purchaseRequisitionDataService = new Inv_PurchaseRequisitionDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<Inv_PurchaseRequisition>();
                    var dbAttachedListEntity = new List<Inv_PurchaseRequisition>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (purchaseRequisitionDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<Inv_PurchaseRequisitionJson> GetDeletePurchaseRequisitionById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_PurchaseRequisitionJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.PurchaseRequisition.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_PurchaseRequisitionDataService purchaseRequisitionDataService = new Inv_PurchaseRequisitionDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!purchaseRequisitionDataService.DeleteById(id, out error))
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
        private bool IsValidToSavePurchaseRequisition(Inv_PurchaseRequisition newObj, out string error)
        {
            error = "";
            if (newObj.RequisitionDate!=null && !newObj.RequisitionDate.IsValid())
            {
                newObj.RequisitionDate=null;//just put null,if its nullable and not valid.
               //error += "Requisition Date is not valid.";
               //return false;
            }


            if (newObj.Purpose.IsValid() && newObj.Purpose.Length>500)
            {
                error += "Purpose exceeded its maximum length 500.";
                return false;
            }
            if (newObj.Remarks.IsValid() && newObj.Remarks.Length>500)
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
            //var res =  DbInstance.Inv_PurchaseRequisition.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A PurchaseRequisition already exists!";
                //return false;
            //}
            return true;
        }
        private bool SavePurchaseRequisitionLogic(Inv_PurchaseRequisition newObj,ref Inv_PurchaseRequisition dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Purchase Requisition to save can't be null!";
                return false;
            }

            if ( !IsValidToSavePurchaseRequisition(newObj, out error))
                return false;

            bool isNewObject = true;
            Inv_PurchaseRequisition objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Inv_PurchaseRequisition.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Inv_PurchaseRequisition.GetNew(newObj.Id);
                DbInstance.Inv_PurchaseRequisition.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.PurchaseRequisition.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.PurchaseRequisition.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
           objToSave.RequisitionDate =  newObj.RequisitionDate ;
           objToSave.RequisitionByUserId =  newObj.RequisitionByUserId ;
           objToSave.Status =  newObj.Status ;
           objToSave.Purpose =  newObj.Purpose ;
           objToSave.Remarks =  newObj.Remarks ;
           objToSave.ApprovedByUserId =  newObj.ApprovedByUserId ;
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

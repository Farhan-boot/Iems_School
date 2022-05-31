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
using EMS.Web.Jsons.Models;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;
using System.Data;

namespace EMS.Web.UI.Areas.Inventory.Controllers.WebApi
{

    public class InventoryApiController : EmployeeBaseApiController
	{
        public InventoryApiController()
        {  }
       
        #region Inventory 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Inv_InventoryJson> GetPagedInventory(string textkey, int? pageSize, int? pageNo
           ,Int32 supplierId= 0      
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Inv_InventoryJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Inventory.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Inventory.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Inventory.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/

            try
            {
                using (Inv_InventoryDataService inventoryDataService = new Inv_InventoryDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<Inv_Inventory> query = DbInstance.Inv_Inventory.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.VoucherNo.ToLower().Contains(textkey.ToLower()));
                    }
                    if (supplierId>0)
                    {
                        query = query.Where(q => q.SupplierId== supplierId);
                    }  
                 
                    var entities = inventoryDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Inv_InventoryJson>();
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
        private HttpListResult<Inv_InventoryJson> GetAllInventory()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_InventoryJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Inventory.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Inventory.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Inventory.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_InventoryDataService inventoryDataService = new Inv_InventoryDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<Inv_InventoryJson>();
                    var entities = inventoryDataService.GetAll(out error);
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
        public HttpResult<Inv_InventoryJson> GetInventoryById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_InventoryJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Inventory.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Inventory.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Inventory.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_InventoryDataService inventoryDataService = new Inv_InventoryDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Inv_InventoryJson();
                    Inv_Inventory entity;
                    if (id <= 0)
                    {
                        entity = Inv_Inventory.GetNew();
                    }
                    else
                    {
                        entity = inventoryDataService.GetById(id);
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
        private HttpResult<Inv_InventoryJson> GetInventoryByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_InventoryJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Inventory.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Inventory.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Inventory.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_InventoryDataService inventoryDataService = new Inv_InventoryDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Inv_InventoryJson();
                    Inv_Inventory entity;
                    if (id <= 0)
                    {
                        entity = Inv_Inventory.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("Inv_Inventory");
                        //includeTables.Add("Inv_Inventory");

                        entity = inventoryDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Inv_InventoryJson> GetInventoryListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_InventoryJson>();
            try
            {
                //Inv_InventoryDataService inventoryDataService =
                //    new Inv_InventoryDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                //DropDown Option Tables
                 
                //result.DataExtra.SupplierList = DbInstance.Inv_Supplier.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<Inv_InventoryJson> GetInventoryDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_InventoryJson>();
            try
            {
                //Inv_InventoryDataService inventoryDataService =
                //    new Inv_InventoryDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                //DropDown Option Tables
                 
                //result.DataExtra.SupplierList = DbInstance.Inv_Supplier.AsEnumerable().Select(t => new
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
        public HttpResult<Inv_InventoryJson> SaveInventory(Inv_InventoryJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_InventoryJson>();
            //optional permission, check permission in the SaveInventoryLogic insted
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Inventory.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Inventory.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                 var entityReceived = new Inv_Inventory();
                 var dbAttachedEntity = new Inv_Inventory();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SaveInventoryLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<Inv_InventoryJson> SaveInventory2(Inv_InventoryJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_InventoryJson>();
            try
            {
                using (Inv_InventoryDataService inventoryDataService = new Inv_InventoryDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new Inv_Inventory();
                    var dbAttachedEntity = new Inv_Inventory();
                    json.Map(entityReceived);
                    if (inventoryDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<Inv_InventoryJson> SaveInventoryList(IList<Inv_InventoryJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_InventoryJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Inventory.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Inventory.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_InventoryDataService inventoryDataService = new Inv_InventoryDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<Inv_Inventory>();
                    var dbAttachedListEntity = new List<Inv_Inventory>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (inventoryDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<Inv_InventoryJson> GetDeleteInventoryById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_InventoryJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Inventory.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_InventoryDataService inventoryDataService = new Inv_InventoryDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!inventoryDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveInventory(Inv_Inventory newObj, out string error)
        {
            error = "";
            if (newObj.StoreId==null)
            {
                error += "Store Id required.";
                return false;
            }
            if (newObj.VoucherNo.IsValid() && newObj.VoucherNo.Length>50)
            {
                error += "Voucher No exceeded its maximum length 50.";
                return false;
            }
            if (newObj.VoucherDate!=null && !newObj.VoucherDate.IsValid())
            {
                newObj.VoucherDate=null;//just put null,if its nullable and not valid.
               //error += "Voucher Date is not valid.";
               //return false;
            }
            if (newObj.InvoiceNo.IsValid() && newObj.InvoiceNo.Length>50)
            {
                error += "Invoice No exceeded its maximum length 50.";
                return false;
            }
            if (newObj.InvoiceDate!=null && !newObj.InvoiceDate.IsValid())
            {
                newObj.InvoiceDate=null;//just put null,if its nullable and not valid.
               //error += "Invoice Date is not valid.";
               //return false;
            }

            if (newObj.PurchaseOrderDate!=null && !newObj.PurchaseOrderDate.IsValid())
            {
                newObj.PurchaseOrderDate=null;//just put null,if its nullable and not valid.
               //error += "Purchase Order Date is not valid.";
               //return false;
            }
            if (newObj.ChalanNo.IsValid() && newObj.ChalanNo.Length>50)
            {
                error += "Chalan No exceeded its maximum length 50.";
                return false;
            }
            if (newObj.ChalanDate!=null && !newObj.ChalanDate.IsValid())
            {
                newObj.ChalanDate=null;//just put null,if its nullable and not valid.
               //error += "Chalan Date is not valid.";
               //return false;
            }
            if (newObj.ReceivedDate!=null && !newObj.ReceivedDate.IsValid())
            {
                newObj.ReceivedDate=null;//just put null,if its nullable and not valid.
               //error += "Received Date is not valid.";
               //return false;
            }



            if (newObj.IsDeleted==null)
            {
                error += "Is Deleted required.";
                return false;
            }

            //TODO write your custom validation here.
            //var res =  DbInstance.Inv_Inventory.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A Inventory already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveInventoryLogic(Inv_Inventory newObj,ref Inv_Inventory dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Inventory to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveInventory(newObj, out error))
                return false;

            bool isNewObject = true;
            Inv_Inventory objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Inv_Inventory.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Inv_Inventory.GetNew(newObj.Id);
                DbInstance.Inv_Inventory.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Inventory.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Inventory.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
           objToSave.StoreId =  newObj.StoreId ;
           objToSave.VoucherNo =  newObj.VoucherNo ;
           objToSave.VoucherDate =  newObj.VoucherDate ;
           objToSave.InvoiceNo =  newObj.InvoiceNo ;
           objToSave.InvoiceDate =  newObj.InvoiceDate ;
           objToSave.PurchaseOrderNo =  newObj.PurchaseOrderNo ;
           objToSave.PurchaseOrderDate =  newObj.PurchaseOrderDate ;
           objToSave.ChalanNo =  newObj.ChalanNo ;
           objToSave.ChalanDate =  newObj.ChalanDate ;
           objToSave.ReceivedDate =  newObj.ReceivedDate ;
           objToSave.ReceivedBy =  newObj.ReceivedBy ;
           objToSave.SupplierId =  newObj.SupplierId ;
           objToSave.Remark =  newObj.Remark ;
           objToSave.IsDeleted =  newObj.IsDeleted ;
           objToSave.History =  newObj.History ;
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

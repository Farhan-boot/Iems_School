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


namespace EMS.Web.UI.Areas.Inventory.Controllers.WebApi
{

    public class PurchaseApiController : EmployeeBaseApiController
	{
        public PurchaseApiController()
        {  }
       
        #region Purchase 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Invt_PurchaseJson> GetPagedPurchase(string textkey, int? pageSize, int? pageNo
           ,Int32 supplierId= 0      
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Invt_PurchaseJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Purchase.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Purchase.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Purchase.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/

            try
            {
                using (Invt_PurchaseDataService purchaseDataService = new Invt_PurchaseDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<Invt_Purchase> query = DbInstance.Invt_Purchase.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.PurchaseCode.ToLower().Contains(textkey.ToLower()));
                    }
                    if (supplierId>0)
                    {
                        query = query.Where(q => q.SupplierId== supplierId);
                    }  
                 
                    var entities = purchaseDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Invt_PurchaseJson>();
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
        private HttpListResult<Invt_PurchaseJson> GetAllPurchase()
        {
            string error = string.Empty;
            var result = new HttpListResult<Invt_PurchaseJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Purchase.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Purchase.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Purchase.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Invt_PurchaseDataService purchaseDataService = new Invt_PurchaseDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<Invt_PurchaseJson>();
                    var entities = purchaseDataService.GetAll(out error);
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
        public HttpResult<Invt_PurchaseJson> GetPurchaseById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Invt_PurchaseJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Purchase.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Purchase.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Purchase.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Invt_PurchaseDataService purchaseDataService = new Invt_PurchaseDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Invt_PurchaseJson();
                    Invt_Purchase entity;
                    if (id <= 0)
                    {
                        entity = Invt_Purchase.GetNew();
                    }
                    else
                    {
                        entity = purchaseDataService.GetById(id);
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
        private HttpResult<Invt_PurchaseJson> GetPurchaseByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Invt_PurchaseJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Purchase.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Purchase.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Purchase.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Invt_PurchaseDataService purchaseDataService = new Invt_PurchaseDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Invt_PurchaseJson();
                    Invt_Purchase entity;
                    if (id <= 0)
                    {
                        entity = Invt_Purchase.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("Invt_Purchase");
                        //includeTables.Add("Invt_Purchase");

                        entity = purchaseDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Invt_PurchaseJson> GetPurchaseListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Invt_PurchaseJson>();
            try
            {
                //Invt_PurchaseDataService purchaseDataService =
                //    new Invt_PurchaseDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                //DropDown Option Tables
                 
                //result.DataExtra.SupplierList = DbInstance.Invt_Supplier.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<Invt_PurchaseJson> GetPurchaseDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Invt_PurchaseJson>();
            try
            {
                //Invt_PurchaseDataService purchaseDataService =
                //    new Invt_PurchaseDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                //DropDown Option Tables
                 
                //result.DataExtra.SupplierList = DbInstance.Invt_Supplier.AsEnumerable().Select(t => new
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
        public HttpResult<Invt_PurchaseJson> SavePurchase(Invt_PurchaseJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Invt_PurchaseJson>();
            //optional permission, check permission in the SavePurchaseLogic insted
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Purchase.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Purchase.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                 var entityReceived = new Invt_Purchase();
                 var dbAttachedEntity = new Invt_Purchase();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SavePurchaseLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<Invt_PurchaseJson> SavePurchase2(Invt_PurchaseJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Invt_PurchaseJson>();
            try
            {
                using (Invt_PurchaseDataService purchaseDataService = new Invt_PurchaseDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new Invt_Purchase();
                    var dbAttachedEntity = new Invt_Purchase();
                    json.Map(entityReceived);
                    if (purchaseDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<Invt_PurchaseJson> SavePurchaseList(IList<Invt_PurchaseJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Invt_PurchaseJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Purchase.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Purchase.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Invt_PurchaseDataService purchaseDataService = new Invt_PurchaseDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<Invt_Purchase>();
                    var dbAttachedListEntity = new List<Invt_Purchase>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (purchaseDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<Invt_PurchaseJson> GetDeletePurchaseById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Invt_PurchaseJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Purchase.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Invt_PurchaseDataService purchaseDataService = new Invt_PurchaseDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!purchaseDataService.DeleteById(id, out error))
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
        private bool IsValidToSavePurchase(Invt_Purchase newObj, out string error)
        {
            error = "";
            if (!newObj.PurchaseCode.IsValid())
            {
                error += "Purchase Code is not valid.";
                return false;
            }
            if (newObj.PurchaseCode.Length>20)
            {
                error += "Purchase Code exceeded its maximum length 20.";
                return false;
            }
            if (!newObj.InvoiceNo.IsValid())
            {
                error += "Invoice No is not valid.";
                return false;
            }
            if (newObj.InvoiceNo.Length>256)
            {
                error += "Invoice No exceeded its maximum length 256.";
                return false;
            }
            if (!newObj.ActionDate.IsValid())
            {
                error += "Action Date is not valid.";
                return false;
            }
            if (newObj.TotalAmount==null)
            {
                error += "Total Amount required.";
                return false;
            }
            if (newObj.SupplierId<=0)
            {
                error += "Please select valid Supplier.";
                return false;
            }
            if (newObj.IsDeleted==null)
            {
                error += "Is Deleted required.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.Invt_Purchase.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A Purchase already exists!";
                //return false;
            //}
            return true;
        }
        private bool SavePurchaseLogic(Invt_Purchase newObj,ref Invt_Purchase dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Purchase to save can't be null!";
                return false;
            }

            if ( !IsValidToSavePurchase(newObj, out error))
                return false;

            bool isNewObject = true;
            Invt_Purchase objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Invt_Purchase.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Invt_Purchase.GetNew(newObj.Id);
                DbInstance.Invt_Purchase.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Purchase.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Purchase.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
           objToSave.PurchaseCode =  newObj.PurchaseCode ;
           objToSave.InvoiceNo =  newObj.InvoiceNo ;
           objToSave.ActionDate =  newObj.ActionDate ;
           objToSave.TotalAmount =  newObj.TotalAmount ;
           objToSave.SupplierId =  newObj.SupplierId ;
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

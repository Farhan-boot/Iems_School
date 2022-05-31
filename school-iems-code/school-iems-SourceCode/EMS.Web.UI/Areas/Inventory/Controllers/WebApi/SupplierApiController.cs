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

    public class SupplierApiController : EmployeeBaseApiController
	{
        public SupplierApiController()
        {  }
       
        #region Supplier 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Inv_SupplierJson> GetPagedSupplier(string textkey, int? pageSize, int? pageNo
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Inv_SupplierJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Supplier.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Supplier.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Supplier.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/

            try
            {
                using (Inv_SupplierDataService supplierDataService = new Inv_SupplierDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<Inv_Supplier> query = DbInstance.Inv_Supplier.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.CompanyName.ToLower().Contains(textkey.ToLower()));
                    }
                 
                    var entities = supplierDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Inv_SupplierJson>();
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
        private HttpListResult<Inv_SupplierJson> GetAllSupplier()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_SupplierJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Supplier.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Supplier.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Supplier.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_SupplierDataService supplierDataService = new Inv_SupplierDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<Inv_SupplierJson>();
                    var entities = supplierDataService.GetAll(out error);
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
        public HttpResult<Inv_SupplierJson> GetSupplierById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_SupplierJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Supplier.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Supplier.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Supplier.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_SupplierDataService supplierDataService = new Inv_SupplierDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Inv_SupplierJson();
                    Inv_Supplier entity;
                    if (id <= 0)
                    {
                        entity = Inv_Supplier.GetNew();
                    }
                    else
                    {
                        entity = supplierDataService.GetById(id);
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
        private HttpResult<Inv_SupplierJson> GetSupplierByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_SupplierJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Supplier.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Supplier.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Supplier.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_SupplierDataService supplierDataService = new Inv_SupplierDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Inv_SupplierJson();
                    Inv_Supplier entity;
                    if (id <= 0)
                    {
                        entity = Inv_Supplier.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("Inv_Supplier");
                        //includeTables.Add("Inv_Supplier");

                        entity = supplierDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Inv_SupplierJson> GetSupplierListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_SupplierJson>();
            try
            {
                //Inv_SupplierDataService supplierDataService =
                //    new Inv_SupplierDataService(DbInstance, HttpUtil.Profile);
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
        public HttpListResult<Inv_SupplierJson> GetSupplierDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_SupplierJson>();
            try
            {
                //Inv_SupplierDataService supplierDataService =
                //    new Inv_SupplierDataService(DbInstance, HttpUtil.Profile);
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
        public HttpResult<Inv_SupplierJson> SaveSupplier(Inv_SupplierJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_SupplierJson>();
            //optional permission, check permission in the SaveSupplierLogic insted
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Supplier.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Supplier.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                 var entityReceived = new Inv_Supplier();
                 var dbAttachedEntity = new Inv_Supplier();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SaveSupplierLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<Inv_SupplierJson> SaveSupplier2(Inv_SupplierJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_SupplierJson>();
            try
            {
                using (Inv_SupplierDataService supplierDataService = new Inv_SupplierDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new Inv_Supplier();
                    var dbAttachedEntity = new Inv_Supplier();
                    json.Map(entityReceived);
                    if (supplierDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<Inv_SupplierJson> SaveSupplierList(IList<Inv_SupplierJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_SupplierJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Supplier.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Supplier.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_SupplierDataService supplierDataService = new Inv_SupplierDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<Inv_Supplier>();
                    var dbAttachedListEntity = new List<Inv_Supplier>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (supplierDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<Inv_SupplierJson> GetDeleteSupplierById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_SupplierJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Supplier.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_SupplierDataService supplierDataService = new Inv_SupplierDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!supplierDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveSupplier(Inv_Supplier newObj, out string error)
        {
            error = "";
            if (newObj.CompanyName.IsValid() && newObj.CompanyName.Length>100)
            {
                error += "Company Name exceeded its maximum length 100.";
                return false;
            }
            if (newObj.ContactName.IsValid() && newObj.ContactName.Length>50)
            {
                error += "Contact Name exceeded its maximum length 50.";
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
            if (!newObj.Phone.IsValid())
            {
                error += "Phone is not valid.";
                return false;
            }
            if (newObj.Phone.Length>50)
            {
                error += "Phone exceeded its maximum length 50.";
                return false;
            }
            if (newObj.Fax.IsValid() && newObj.Fax.Length>100)
            {
                error += "Fax exceeded its maximum length 100.";
                return false;
            }
            if (newObj.Email.IsValid() && newObj.Email.Length>100)
            {
                error += "Email exceeded its maximum length 100.";
                return false;
            }

            if (newObj.ContactPersonName.IsValid() && newObj.ContactPersonName.Length>100)
            {
                error += "Contact Person Name exceeded its maximum length 100.";
                return false;
            }
            if (newObj.ContactPersonPhone.IsValid() && newObj.ContactPersonPhone.Length>100)
            {
                error += "Contact Person Phone exceeded its maximum length 100.";
                return false;
            }
            if (newObj.ContactPersonEmail.IsValid() && newObj.ContactPersonEmail.Length>100)
            {
                error += "Contact Person Email exceeded its maximum length 100.";
                return false;
            }

            if (newObj.IsDeleted==null)
            {
                error += "Is Deleted required.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.Inv_Supplier.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A Supplier already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveSupplierLogic(Inv_Supplier newObj,ref Inv_Supplier dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Supplier to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveSupplier(newObj, out error))
                return false;

            bool isNewObject = true;
            Inv_Supplier objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Inv_Supplier.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Inv_Supplier.GetNew(newObj.Id);
                DbInstance.Inv_Supplier.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Supplier.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Supplier.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
           objToSave.CompanyName =  newObj.CompanyName ;
           objToSave.ContactName =  newObj.ContactName ;
           objToSave.ContactDesignation =  newObj.ContactDesignation ;
           objToSave.Address =  newObj.Address ;
           objToSave.Phone =  newObj.Phone ;
           objToSave.Fax =  newObj.Fax ;
           objToSave.Email =  newObj.Email ;
           objToSave.WebsiteURL =  newObj.WebsiteURL ;
           objToSave.ContactPersonName =  newObj.ContactPersonName ;
           objToSave.ContactPersonPhone =  newObj.ContactPersonPhone ;
           objToSave.ContactPersonEmail =  newObj.ContactPersonEmail ;
           objToSave.Description =  newObj.Description ;
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

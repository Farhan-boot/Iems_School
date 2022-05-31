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

    public class ProductCategoryDetailsApiController : EmployeeBaseApiController
	{
        public ProductCategoryDetailsApiController()
        {  }
       
        #region ProductCategoryDetails 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Inv_ProductCategoryDetailsJson> GetPagedProductCategoryDetails(string textkey, int? pageSize, int? pageNo
           ,Int32 productCategoryId= 0      
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Inv_ProductCategoryDetailsJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategoryDetails.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategoryDetails.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategoryDetails.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/

            try
            {
                using (Inv_ProductCategoryDetailsDataService productCategoryDetailsDataService = new Inv_ProductCategoryDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<Inv_ProductCategoryDetails> query = DbInstance.Inv_ProductCategoryDetails.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Details.ToLower().Contains(textkey.ToLower()));
                    }
                    if (productCategoryId>0)
                    {
                        query = query.Where(q => q.ProductCategoryId== productCategoryId);
                    }  
                 
                    var entities = productCategoryDetailsDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Inv_ProductCategoryDetailsJson>();
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
        private HttpListResult<Inv_ProductCategoryDetailsJson> GetAllProductCategoryDetails()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_ProductCategoryDetailsJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategoryDetails.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategoryDetails.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategoryDetails.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_ProductCategoryDetailsDataService productCategoryDetailsDataService = new Inv_ProductCategoryDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<Inv_ProductCategoryDetailsJson>();
                    var entities = productCategoryDetailsDataService.GetAll(out error);
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
        public HttpResult<Inv_ProductCategoryDetailsJson> GetProductCategoryDetailsById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ProductCategoryDetailsJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategoryDetails.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategoryDetails.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategoryDetails.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_ProductCategoryDetailsDataService productCategoryDetailsDataService = new Inv_ProductCategoryDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Inv_ProductCategoryDetailsJson();
                    Inv_ProductCategoryDetails entity;
                    if (id <= 0)
                    {
                        entity = Inv_ProductCategoryDetails.GetNew();
                    }
                    else
                    {
                        entity = productCategoryDetailsDataService.GetById(id);
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
        private HttpResult<Inv_ProductCategoryDetailsJson> GetProductCategoryDetailsByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ProductCategoryDetailsJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategoryDetails.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategoryDetails.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategoryDetails.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_ProductCategoryDetailsDataService productCategoryDetailsDataService = new Inv_ProductCategoryDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Inv_ProductCategoryDetailsJson();
                    Inv_ProductCategoryDetails entity;
                    if (id <= 0)
                    {
                        entity = Inv_ProductCategoryDetails.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("Inv_ProductCategoryDetails");
                        //includeTables.Add("Inv_ProductCategoryDetails");

                        entity = productCategoryDetailsDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Inv_ProductCategoryDetailsJson> GetProductCategoryDetailsListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_ProductCategoryDetailsJson>();
            try
            {
                //Inv_ProductCategoryDetailsDataService productCategoryDetailsDataService =
                //    new Inv_ProductCategoryDetailsDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List

                //DropDown Option Tables

                //result.DataExtra.ProductCategoryList = DbInstance.Inv_ProductCategory.AsEnumerable().Select(t => new
                //{ Id = t.Id, Name = t.Title }).ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<Inv_ProductCategoryDetailsJson> GetProductCategoryDetailsDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_ProductCategoryDetailsJson>();
            try
            {
                //Inv_ProductCategoryDetailsDataService productCategoryDetailsDataService =
                //    new Inv_ProductCategoryDetailsDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List

                //DropDown Option Tables

                result.DataExtra.ProductCategoryList = DbInstance.Inv_ProductCategory.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.Title }).ToList();
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
        public HttpResult<Inv_ProductCategoryDetailsJson> SaveProductCategoryDetails(Inv_ProductCategoryDetailsJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ProductCategoryDetailsJson>();
            //optional permission, check permission in the SaveProductCategoryDetailsLogic insted
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategoryDetails.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategoryDetails.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                 var entityReceived = new Inv_ProductCategoryDetails();
                 var dbAttachedEntity = new Inv_ProductCategoryDetails();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SaveProductCategoryDetailsLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<Inv_ProductCategoryDetailsJson> SaveProductCategoryDetails2(Inv_ProductCategoryDetailsJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ProductCategoryDetailsJson>();
            try
            {
                using (Inv_ProductCategoryDetailsDataService productCategoryDetailsDataService = new Inv_ProductCategoryDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new Inv_ProductCategoryDetails();
                    var dbAttachedEntity = new Inv_ProductCategoryDetails();
                    json.Map(entityReceived);
                    if (productCategoryDetailsDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<Inv_ProductCategoryDetailsJson> SaveProductCategoryDetailsList(IList<Inv_ProductCategoryDetailsJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_ProductCategoryDetailsJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategoryDetails.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategoryDetails.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_ProductCategoryDetailsDataService productCategoryDetailsDataService = new Inv_ProductCategoryDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<Inv_ProductCategoryDetails>();
                    var dbAttachedListEntity = new List<Inv_ProductCategoryDetails>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (productCategoryDetailsDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<Inv_ProductCategoryDetailsJson> GetDeleteProductCategoryDetailsById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ProductCategoryDetailsJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategoryDetails.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_ProductCategoryDetailsDataService productCategoryDetailsDataService = new Inv_ProductCategoryDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!productCategoryDetailsDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveProductCategoryDetails(Inv_ProductCategoryDetails newObj, out string error)
        {
            error = "";
            if (newObj.ProductCategoryId<=0)
            {
                error += "Please select valid Product Category.";
                return false;
            }



            if (newObj.Details.IsValid() && newObj.Details.Length>250)
            {
                error += "Details exceeded its maximum length 250.";
                return false;
            }

            if (newObj.IsDeleted==null)
            {
                error += "Is Deleted required.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.Inv_ProductCategoryDetails.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A ProductCategoryDetails already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveProductCategoryDetailsLogic(Inv_ProductCategoryDetails newObj,ref Inv_ProductCategoryDetails dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Product Category Details to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveProductCategoryDetails(newObj, out error))
                return false;

            bool isNewObject = true;
            Inv_ProductCategoryDetails objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Inv_ProductCategoryDetails.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Inv_ProductCategoryDetails.GetNew(newObj.Id);
                DbInstance.Inv_ProductCategoryDetails.Add(objToSave);
               
               
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategoryDetails.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategoryDetails.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
           objToSave.ProductCategoryId =  newObj.ProductCategoryId ;
           objToSave.Unit =  newObj.Unit ;
           objToSave.UnitTypeEnumId =  newObj.UnitTypeEnumId ;
           objToSave.WarrningQuantity =  newObj.WarrningQuantity ;
           objToSave.Details =  newObj.Details ;
           objToSave.HasBarcode =  newObj.HasBarcode ;
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

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

    public class ProductCategoryApiController : EmployeeBaseApiController
	{
        public ProductCategoryApiController()
        {  }
       
        #region ProductCategory 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Inv_ProductCategoryJson> GetPagedProductCategory(string textkey, int? pageSize, int? pageNo
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Inv_ProductCategoryJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategory.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategory.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategory.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/

            try
            {
                using (Inv_ProductCategoryDataService productCategoryDataService = new Inv_ProductCategoryDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<Inv_ProductCategory> query = DbInstance.Inv_ProductCategory.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Name.ToLower().Contains(textkey.ToLower()));
                    }
                 
                    var entities = productCategoryDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Inv_ProductCategoryJson>();
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
        private HttpListResult<Inv_ProductCategoryJson> GetAllProductCategory()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_ProductCategoryJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategory.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategory.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategory.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_ProductCategoryDataService productCategoryDataService = new Inv_ProductCategoryDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<Inv_ProductCategoryJson>();
                    var entities = productCategoryDataService.GetAll(out error);
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
        public HttpResult<Inv_ProductCategoryJson> GetProductCategoryById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ProductCategoryJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategory.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategory.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategory.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_ProductCategoryDataService productCategoryDataService = new Inv_ProductCategoryDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Inv_ProductCategoryJson();
                    Inv_ProductCategory entity;
                    if (id <= 0)
                    {
                        entity = Inv_ProductCategory.GetNew();
                    }
                    else
                    {
                        entity = productCategoryDataService.GetById(id);
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
        private HttpResult<Inv_ProductCategoryJson> GetProductCategoryByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ProductCategoryJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategory.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategory.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategory.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_ProductCategoryDataService productCategoryDataService = new Inv_ProductCategoryDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Inv_ProductCategoryJson();
                    Inv_ProductCategory entity;
                    if (id <= 0)
                    {
                        entity = Inv_ProductCategory.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("Inv_ProductCategory");
                        //includeTables.Add("Inv_ProductCategory");

                        entity = productCategoryDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Inv_ProductCategoryJson> GetProductCategoryListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_ProductCategoryJson>();
            try
            {
                //Inv_ProductCategoryDataService productCategoryDataService =
                //    new Inv_ProductCategoryDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.AssetTypeEnumList = EnumUtil.GetEnumList(typeof(Inv_ProductCategory.AssetTypeEnum));
                result.DataExtra.UnitTypeEnumList = EnumUtil.GetEnumList(typeof(Inv_ProductCategory.UnitTypeEnum));
                //DropDown Option Tables
                
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<Inv_ProductCategoryJson> GetProductCategoryDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_ProductCategoryJson>();
            try
            {
                //Inv_ProductCategoryDataService productCategoryDataService =
                //    new Inv_ProductCategoryDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.AssetTypeEnumList = EnumUtil.GetEnumList(typeof(Inv_ProductCategory.AssetTypeEnum));
                result.DataExtra.UnitTypeEnumList = EnumUtil.GetEnumList(typeof(Inv_ProductCategory.UnitTypeEnum));
                //DropDown Option Tables
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
        public HttpResult<Inv_ProductCategoryJson> SaveProductCategory(Inv_ProductCategoryJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ProductCategoryJson>();
            //optional permission, check permission in the SaveProductCategoryLogic insted
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategory.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategory.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                 var entityReceived = new Inv_ProductCategory();
                 var dbAttachedEntity = new Inv_ProductCategory();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SaveProductCategoryLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<Inv_ProductCategoryJson> SaveProductCategory2(Inv_ProductCategoryJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ProductCategoryJson>();
            try
            {
                using (Inv_ProductCategoryDataService productCategoryDataService = new Inv_ProductCategoryDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new Inv_ProductCategory();
                    var dbAttachedEntity = new Inv_ProductCategory();
                    json.Map(entityReceived);
                    if (productCategoryDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<Inv_ProductCategoryJson> SaveProductCategoryList(IList<Inv_ProductCategoryJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_ProductCategoryJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategory.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategory.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_ProductCategoryDataService productCategoryDataService = new Inv_ProductCategoryDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<Inv_ProductCategory>();
                    var dbAttachedListEntity = new List<Inv_ProductCategory>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (productCategoryDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<Inv_ProductCategoryJson> GetDeleteProductCategoryById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ProductCategoryJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategory.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_ProductCategoryDataService productCategoryDataService = new Inv_ProductCategoryDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!productCategoryDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveProductCategory(Inv_ProductCategory newObj, out string error)
        {
            error = "";
            if (!newObj.Name.IsValid())
            {
                error += "Name is not valid.";
                return false;
            }
            if (newObj.Name==null)
            {
                error += "Name required.";
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


            if (newObj.ProductCode.IsValid() && newObj.ProductCode.Length>100)
            {
                error += "Product Code exceeded its maximum length 100.";
                return false;
            }







            if (newObj.IsDeleted==null)
            {
                error += "Is Deleted required.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.Inv_ProductCategory.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A ProductCategory already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveProductCategoryLogic(Inv_ProductCategory newObj,ref Inv_ProductCategory dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Product Category to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveProductCategory(newObj, out error))
                return false;

            bool isNewObject = true;
            Inv_ProductCategory objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Inv_ProductCategory.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Inv_ProductCategory.GetNew(newObj.Id);
                DbInstance.Inv_ProductCategory.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategory.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ProductCategory.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
           objToSave.Name =  newObj.Name ;
           objToSave.SubTitle =  newObj.SubTitle ;
           objToSave.ParentId =  newObj.ParentId ;
           objToSave.Description =  newObj.Description ;
           objToSave.AssetTypeEnumId =  newObj.AssetTypeEnumId ;
           objToSave.IsBarcoded =  newObj.IsBarcoded ;
           objToSave.ProductCode =  newObj.ProductCode ;
           objToSave.DefaultStoreId =  newObj.DefaultStoreId ;
           objToSave.UnitTypeEnumId =  newObj.UnitTypeEnumId ;
           objToSave.WarningQuantity =  newObj.WarningQuantity ;
           objToSave.Remark =  newObj.Remark ;
           objToSave.ParentCategoryId =  newObj.ParentCategoryId ;
           objToSave.DepriciationRate =  newObj.DepriciationRate ;
           objToSave.History =  newObj.History ;
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

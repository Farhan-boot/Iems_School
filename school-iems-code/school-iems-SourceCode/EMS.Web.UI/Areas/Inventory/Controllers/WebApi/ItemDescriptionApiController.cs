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

    public class ItemDescriptionApiController : EmployeeBaseApiController
	{
        public ItemDescriptionApiController()
        {  }
       
        #region ItemDescription 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Inv_ItemDescriptionJson> GetPagedItemDescription(string textkey, int? pageSize, int? pageNo
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Inv_ItemDescriptionJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemDescription.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemDescription.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemDescription.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/

            try
            {
                using (Inv_ItemDescriptionDataService itemDescriptionDataService = new Inv_ItemDescriptionDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<Inv_ItemDescription> query = DbInstance.Inv_ItemDescription.OrderByDescending(x => x.Id);
                    //if (textkey.IsValid())
                    //{
                    //    query = query.Where(q => q.ItemId.ToLower().Contains(textkey.ToLower()));
                    //}
                 
                    var entities = itemDescriptionDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Inv_ItemDescriptionJson>();
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
        private HttpListResult<Inv_ItemDescriptionJson> GetAllItemDescription()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_ItemDescriptionJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemDescription.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemDescription.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemDescription.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_ItemDescriptionDataService itemDescriptionDataService = new Inv_ItemDescriptionDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<Inv_ItemDescriptionJson>();
                    var entities = itemDescriptionDataService.GetAll(out error);
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
        public HttpResult<Inv_ItemDescriptionJson> GetItemDescriptionById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ItemDescriptionJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemDescription.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemDescription.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemDescription.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_ItemDescriptionDataService itemDescriptionDataService = new Inv_ItemDescriptionDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Inv_ItemDescriptionJson();
                    Inv_ItemDescription entity;
                    if (id <= 0)
                    {
                        entity = Inv_ItemDescription.GetNew();
                    }
                    else
                    {
                        entity = itemDescriptionDataService.GetById(id);
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
        private HttpResult<Inv_ItemDescriptionJson> GetItemDescriptionByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ItemDescriptionJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemDescription.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemDescription.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemDescription.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_ItemDescriptionDataService itemDescriptionDataService = new Inv_ItemDescriptionDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Inv_ItemDescriptionJson();
                    Inv_ItemDescription entity;
                    if (id <= 0)
                    {
                        entity = Inv_ItemDescription.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("Inv_ItemDescription");
                        //includeTables.Add("Inv_ItemDescription");

                        entity = itemDescriptionDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Inv_ItemDescriptionJson> GetItemDescriptionListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_ItemDescriptionJson>();
            try
            {
                //Inv_ItemDescriptionDataService itemDescriptionDataService =
                //    new Inv_ItemDescriptionDataService(DbInstance, HttpUtil.Profile);
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
        public HttpListResult<Inv_ItemDescriptionJson> GetItemDescriptionDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_ItemDescriptionJson>();
            try
            {
                //Inv_ItemDescriptionDataService itemDescriptionDataService =
                //    new Inv_ItemDescriptionDataService(DbInstance, HttpUtil.Profile);
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
        public HttpResult<Inv_ItemDescriptionJson> SaveItemDescription(Inv_ItemDescriptionJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ItemDescriptionJson>();
            //optional permission, check permission in the SaveItemDescriptionLogic insted
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemDescription.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemDescription.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                 var entityReceived = new Inv_ItemDescription();
                 var dbAttachedEntity = new Inv_ItemDescription();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SaveItemDescriptionLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<Inv_ItemDescriptionJson> SaveItemDescription2(Inv_ItemDescriptionJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ItemDescriptionJson>();
            try
            {
                using (Inv_ItemDescriptionDataService itemDescriptionDataService = new Inv_ItemDescriptionDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new Inv_ItemDescription();
                    var dbAttachedEntity = new Inv_ItemDescription();
                    json.Map(entityReceived);
                    if (itemDescriptionDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<Inv_ItemDescriptionJson> SaveItemDescriptionList(IList<Inv_ItemDescriptionJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_ItemDescriptionJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemDescription.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemDescription.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_ItemDescriptionDataService itemDescriptionDataService = new Inv_ItemDescriptionDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<Inv_ItemDescription>();
                    var dbAttachedListEntity = new List<Inv_ItemDescription>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (itemDescriptionDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<Inv_ItemDescriptionJson> GetDeleteItemDescriptionById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ItemDescriptionJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemDescription.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_ItemDescriptionDataService itemDescriptionDataService = new Inv_ItemDescriptionDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!itemDescriptionDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveItemDescription(Inv_ItemDescription newObj, out string error)
        {
            error = "";
            ////if (newObj.ItemId.IsValid() && newObj.ItemId.Length>50)
            ////{
            ////    error += "Item Id exceeded its maximum length 50.";
            ////    return false;
            ////}
            if (newObj.CompanyItemBarcode.IsValid() && newObj.CompanyItemBarcode.Length>100)
            {
                error += "Company Item Barcode exceeded its maximum length 100.";
                return false;
            }
            if (newObj.WarrentyDate!=null && !newObj.WarrentyDate.IsValid())
            {
                newObj.WarrentyDate=null;//just put null,if its nullable and not valid.
               //error += "Warrenty Date is not valid.";
               //return false;
            }
            if (newObj.Description.IsValid() && newObj.Description.Length>500)
            {
                error += "Description exceeded its maximum length 500.";
                return false;
            }

            //TODO write your custom validation here.
            //var res =  DbInstance.Inv_ItemDescription.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A ItemDescription already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveItemDescriptionLogic(Inv_ItemDescription newObj,ref Inv_ItemDescription dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Item Description to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveItemDescription(newObj, out error))
                return false;

            bool isNewObject = true;
            Inv_ItemDescription objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Inv_ItemDescription.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Inv_ItemDescription.GetNew(newObj.Id);
                DbInstance.Inv_ItemDescription.Add(objToSave);
               
               
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemDescription.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemDescription.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
           objToSave.ItemId =  newObj.ItemId ;
           objToSave.CompanyItemBarcode =  newObj.CompanyItemBarcode ;
           objToSave.WarrentyDate =  newObj.WarrentyDate ;
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

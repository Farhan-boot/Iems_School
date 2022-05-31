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

    public class ItemWarrentyDetailsApiController : EmployeeBaseApiController
	{
        public ItemWarrentyDetailsApiController()
        {  }
       
        #region ItemWarrentyDetails 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Inv_ItemWarrentyDetailsJson> GetPagedItemWarrentyDetails(string textkey, int? pageSize, int? pageNo
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Inv_ItemWarrentyDetailsJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemWarrentyDetails.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemWarrentyDetails.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemWarrentyDetails.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/

            try
            {
                using (Inv_ItemWarrentyDetailsDataService itemWarrentyDetailsDataService = new Inv_ItemWarrentyDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<Inv_ItemWarrentyDetails> query = DbInstance.Inv_ItemWarrentyDetails.OrderByDescending(x => x.Id);
                    //if (textkey.IsValid())
                    //{
                    //    query = query.Where(q => q.ItemId.ToLower().Contains(textkey.ToLower()));
                    //}
                 
                    var entities = itemWarrentyDetailsDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Inv_ItemWarrentyDetailsJson>();
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
        private HttpListResult<Inv_ItemWarrentyDetailsJson> GetAllItemWarrentyDetails()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_ItemWarrentyDetailsJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemWarrentyDetails.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemWarrentyDetails.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemWarrentyDetails.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_ItemWarrentyDetailsDataService itemWarrentyDetailsDataService = new Inv_ItemWarrentyDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<Inv_ItemWarrentyDetailsJson>();
                    var entities = itemWarrentyDetailsDataService.GetAll(out error);
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
        public HttpResult<Inv_ItemWarrentyDetailsJson> GetItemWarrentyDetailsById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ItemWarrentyDetailsJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemWarrentyDetails.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemWarrentyDetails.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemWarrentyDetails.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_ItemWarrentyDetailsDataService itemWarrentyDetailsDataService = new Inv_ItemWarrentyDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Inv_ItemWarrentyDetailsJson();
                    Inv_ItemWarrentyDetails entity;
                    if (id <= 0)
                    {
                        entity = Inv_ItemWarrentyDetails.GetNew();
                    }
                    else
                    {
                        entity = itemWarrentyDetailsDataService.GetById(id);
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
        private HttpResult<Inv_ItemWarrentyDetailsJson> GetItemWarrentyDetailsByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ItemWarrentyDetailsJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemWarrentyDetails.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemWarrentyDetails.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemWarrentyDetails.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_ItemWarrentyDetailsDataService itemWarrentyDetailsDataService = new Inv_ItemWarrentyDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Inv_ItemWarrentyDetailsJson();
                    Inv_ItemWarrentyDetails entity;
                    if (id <= 0)
                    {
                        entity = Inv_ItemWarrentyDetails.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("Inv_ItemWarrentyDetails");
                        //includeTables.Add("Inv_ItemWarrentyDetails");

                        entity = itemWarrentyDetailsDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Inv_ItemWarrentyDetailsJson> GetItemWarrentyDetailsListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_ItemWarrentyDetailsJson>();
            try
            {
                //Inv_ItemWarrentyDetailsDataService itemWarrentyDetailsDataService =
                //    new Inv_ItemWarrentyDetailsDataService(DbInstance, HttpUtil.Profile);
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
        public HttpListResult<Inv_ItemWarrentyDetailsJson> GetItemWarrentyDetailsDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_ItemWarrentyDetailsJson>();
            try
            {
                //Inv_ItemWarrentyDetailsDataService itemWarrentyDetailsDataService =
                //    new Inv_ItemWarrentyDetailsDataService(DbInstance, HttpUtil.Profile);
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
        public HttpResult<Inv_ItemWarrentyDetailsJson> SaveItemWarrentyDetails(Inv_ItemWarrentyDetailsJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ItemWarrentyDetailsJson>();
            //optional permission, check permission in the SaveItemWarrentyDetailsLogic insted
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemWarrentyDetails.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemWarrentyDetails.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                 var entityReceived = new Inv_ItemWarrentyDetails();
                 var dbAttachedEntity = new Inv_ItemWarrentyDetails();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SaveItemWarrentyDetailsLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<Inv_ItemWarrentyDetailsJson> SaveItemWarrentyDetails2(Inv_ItemWarrentyDetailsJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ItemWarrentyDetailsJson>();
            try
            {
                using (Inv_ItemWarrentyDetailsDataService itemWarrentyDetailsDataService = new Inv_ItemWarrentyDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new Inv_ItemWarrentyDetails();
                    var dbAttachedEntity = new Inv_ItemWarrentyDetails();
                    json.Map(entityReceived);
                    if (itemWarrentyDetailsDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<Inv_ItemWarrentyDetailsJson> SaveItemWarrentyDetailsList(IList<Inv_ItemWarrentyDetailsJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_ItemWarrentyDetailsJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemWarrentyDetails.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemWarrentyDetails.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_ItemWarrentyDetailsDataService itemWarrentyDetailsDataService = new Inv_ItemWarrentyDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<Inv_ItemWarrentyDetails>();
                    var dbAttachedListEntity = new List<Inv_ItemWarrentyDetails>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (itemWarrentyDetailsDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<Inv_ItemWarrentyDetailsJson> GetDeleteItemWarrentyDetailsById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ItemWarrentyDetailsJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemWarrentyDetails.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_ItemWarrentyDetailsDataService itemWarrentyDetailsDataService = new Inv_ItemWarrentyDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!itemWarrentyDetailsDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveItemWarrentyDetails(Inv_ItemWarrentyDetails newObj, out string error)
        {
            error = "";
            //if (newObj.ItemId.IsValid() && newObj.ItemId.Length>50)
            //{
            //    error += "Item Id exceeded its maximum length 50.";
            //    return false;
            //}

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
            //var res =  DbInstance.Inv_ItemWarrentyDetails.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A ItemWarrentyDetails already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveItemWarrentyDetailsLogic(Inv_ItemWarrentyDetails newObj,ref Inv_ItemWarrentyDetails dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Item Warrenty Details to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveItemWarrentyDetails(newObj, out error))
                return false;

            bool isNewObject = true;
            Inv_ItemWarrentyDetails objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Inv_ItemWarrentyDetails.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Inv_ItemWarrentyDetails.GetNew(newObj.Id);
                DbInstance.Inv_ItemWarrentyDetails.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemWarrentyDetails.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemWarrentyDetails.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
           objToSave.ItemId =  newObj.ItemId ;
           objToSave.Warrenty =  newObj.Warrenty ;
           objToSave.ReceivedDate =  newObj.ReceivedDate ;
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

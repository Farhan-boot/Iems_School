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

    public class ItemMaintainanceApiController : EmployeeBaseApiController
	{
        public ItemMaintainanceApiController()
        {  }
       
        #region ItemMaintainance 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Inv_ItemMaintainanceJson> GetPagedItemMaintainance(string textkey, int? pageSize, int? pageNo
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Inv_ItemMaintainanceJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemMaintainance.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemMaintainance.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemMaintainance.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/

            try
            {
                using (Inv_ItemMaintainanceDataService itemMaintainanceDataService = new Inv_ItemMaintainanceDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<Inv_ItemMaintainance> query = DbInstance.Inv_ItemMaintainance.OrderByDescending(x => x.Id);
                    //if (textkey.IsValid())
                    //{
                    //    query = query.Where(q => q.ItemId.ToLower().Contains(textkey.ToLower()));
                    //}
                 
                    var entities = itemMaintainanceDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Inv_ItemMaintainanceJson>();
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
        private HttpListResult<Inv_ItemMaintainanceJson> GetAllItemMaintainance()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_ItemMaintainanceJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemMaintainance.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemMaintainance.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemMaintainance.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_ItemMaintainanceDataService itemMaintainanceDataService = new Inv_ItemMaintainanceDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<Inv_ItemMaintainanceJson>();
                    var entities = itemMaintainanceDataService.GetAll(out error);
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
        public HttpResult<Inv_ItemMaintainanceJson> GetItemMaintainanceById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ItemMaintainanceJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemMaintainance.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemMaintainance.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemMaintainance.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_ItemMaintainanceDataService itemMaintainanceDataService = new Inv_ItemMaintainanceDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Inv_ItemMaintainanceJson();
                    Inv_ItemMaintainance entity;
                    if (id <= 0)
                    {
                        entity = Inv_ItemMaintainance.GetNew();
                    }
                    else
                    {
                        entity = itemMaintainanceDataService.GetById(id);
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
        private HttpResult<Inv_ItemMaintainanceJson> GetItemMaintainanceByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ItemMaintainanceJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemMaintainance.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemMaintainance.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemMaintainance.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_ItemMaintainanceDataService itemMaintainanceDataService = new Inv_ItemMaintainanceDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Inv_ItemMaintainanceJson();
                    Inv_ItemMaintainance entity;
                    if (id <= 0)
                    {
                        entity = Inv_ItemMaintainance.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("Inv_ItemMaintainance");
                        //includeTables.Add("Inv_ItemMaintainance");

                        entity = itemMaintainanceDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Inv_ItemMaintainanceJson> GetItemMaintainanceListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_ItemMaintainanceJson>();
            try
            {
                //Inv_ItemMaintainanceDataService itemMaintainanceDataService =
                //    new Inv_ItemMaintainanceDataService(DbInstance, HttpUtil.Profile);
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
        public HttpListResult<Inv_ItemMaintainanceJson> GetItemMaintainanceDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_ItemMaintainanceJson>();
            try
            {
                //Inv_ItemMaintainanceDataService itemMaintainanceDataService =
                //    new Inv_ItemMaintainanceDataService(DbInstance, HttpUtil.Profile);
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
        public HttpResult<Inv_ItemMaintainanceJson> SaveItemMaintainance(Inv_ItemMaintainanceJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ItemMaintainanceJson>();
            //optional permission, check permission in the SaveItemMaintainanceLogic insted
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemMaintainance.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemMaintainance.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                 var entityReceived = new Inv_ItemMaintainance();
                 var dbAttachedEntity = new Inv_ItemMaintainance();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SaveItemMaintainanceLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<Inv_ItemMaintainanceJson> SaveItemMaintainance2(Inv_ItemMaintainanceJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ItemMaintainanceJson>();
            try
            {
                using (Inv_ItemMaintainanceDataService itemMaintainanceDataService = new Inv_ItemMaintainanceDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new Inv_ItemMaintainance();
                    var dbAttachedEntity = new Inv_ItemMaintainance();
                    json.Map(entityReceived);
                    if (itemMaintainanceDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<Inv_ItemMaintainanceJson> SaveItemMaintainanceList(IList<Inv_ItemMaintainanceJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_ItemMaintainanceJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemMaintainance.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemMaintainance.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_ItemMaintainanceDataService itemMaintainanceDataService = new Inv_ItemMaintainanceDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<Inv_ItemMaintainance>();
                    var dbAttachedListEntity = new List<Inv_ItemMaintainance>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (itemMaintainanceDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<Inv_ItemMaintainanceJson> GetDeleteItemMaintainanceById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ItemMaintainanceJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemMaintainance.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_ItemMaintainanceDataService itemMaintainanceDataService = new Inv_ItemMaintainanceDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!itemMaintainanceDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveItemMaintainance(Inv_ItemMaintainance newObj, out string error)
        {
            error = "";
            //if (newObj.ItemId.IsValid() && newObj.ItemId.Length>50)
            //{
            //    error += "Item Id exceeded its maximum length 50.";
            //    return false;
            //}

            if (newObj.Reason.IsValid() && newObj.Reason.Length>50)
            {
                error += "Reason exceeded its maximum length 50.";
                return false;
            }
            if (newObj.Remarks.IsValid() && newObj.Remarks.Length>50)
            {
                error += "Remarks exceeded its maximum length 50.";
                return false;
            }
            if (newObj.ReceiveDate!=null && !newObj.ReceiveDate.IsValid())
            {
                newObj.ReceiveDate=null;//just put null,if its nullable and not valid.
               //error += "Receive Date is not valid.";
               //return false;
            }
            if (newObj.ReturnDate!=null && !newObj.ReturnDate.IsValid())
            {
                newObj.ReturnDate=null;//just put null,if its nullable and not valid.
               //error += "Return Date is not valid.";
               //return false;
            }

            if (newObj.IsDeleted==null)
            {
                error += "Is Deleted required.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.Inv_ItemMaintainance.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A ItemMaintainance already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveItemMaintainanceLogic(Inv_ItemMaintainance newObj,ref Inv_ItemMaintainance dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Item Maintainance to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveItemMaintainance(newObj, out error))
                return false;

            bool isNewObject = true;
            Inv_ItemMaintainance objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Inv_ItemMaintainance.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Inv_ItemMaintainance.GetNew(newObj.Id);
                DbInstance.Inv_ItemMaintainance.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemMaintainance.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemMaintainance.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
           objToSave.ItemId =  newObj.ItemId ;
           objToSave.FromUser =  newObj.FromUser ;
           objToSave.Reason =  newObj.Reason ;
           objToSave.Remarks =  newObj.Remarks ;
           objToSave.ReceiveDate =  newObj.ReceiveDate ;
           objToSave.ReturnDate =  newObj.ReturnDate ;
           objToSave.ItemStatus =  newObj.ItemStatus ;
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

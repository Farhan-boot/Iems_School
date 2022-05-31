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

    public class ItemReturnApiController : EmployeeBaseApiController
	{
        public ItemReturnApiController()
        {  }
       
        #region ItemReturn 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Inv_ItemReturnJson> GetPagedItemReturn(string textkey, int? pageSize, int? pageNo
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Inv_ItemReturnJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemReturn.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemReturn.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemReturn.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/

            try
            {
                using (Inv_ItemReturnDataService itemReturnDataService = new Inv_ItemReturnDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<Inv_ItemReturn> query = DbInstance.Inv_ItemReturn.OrderByDescending(x => x.Id);
                    //if (textkey.IsValid())
                    //{
                    //    query = query.Where(q => q.ItemId.ToLower().Contains(textkey.ToLower()));
                    //}
                 
                    var entities = itemReturnDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Inv_ItemReturnJson>();
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
        private HttpListResult<Inv_ItemReturnJson> GetAllItemReturn()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_ItemReturnJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemReturn.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemReturn.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemReturn.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_ItemReturnDataService itemReturnDataService = new Inv_ItemReturnDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<Inv_ItemReturnJson>();
                    var entities = itemReturnDataService.GetAll(out error);
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
        public HttpResult<Inv_ItemReturnJson> GetItemReturnById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ItemReturnJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemReturn.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemReturn.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemReturn.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_ItemReturnDataService itemReturnDataService = new Inv_ItemReturnDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Inv_ItemReturnJson();
                    Inv_ItemReturn entity;
                    if (id <= 0)
                    {
                        entity = Inv_ItemReturn.GetNew();
                    }
                    else
                    {
                        entity = itemReturnDataService.GetById(id);
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
        private HttpResult<Inv_ItemReturnJson> GetItemReturnByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ItemReturnJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemReturn.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemReturn.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemReturn.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_ItemReturnDataService itemReturnDataService = new Inv_ItemReturnDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Inv_ItemReturnJson();
                    Inv_ItemReturn entity;
                    if (id <= 0)
                    {
                        entity = Inv_ItemReturn.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("Inv_ItemReturn");
                        //includeTables.Add("Inv_ItemReturn");

                        entity = itemReturnDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Inv_ItemReturnJson> GetItemReturnListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_ItemReturnJson>();
            try
            {
                //Inv_ItemReturnDataService itemReturnDataService =
                //    new Inv_ItemReturnDataService(DbInstance, HttpUtil.Profile);
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
        public HttpListResult<Inv_ItemReturnJson> GetItemReturnDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_ItemReturnJson>();
            try
            {
                //Inv_ItemReturnDataService itemReturnDataService =
                //    new Inv_ItemReturnDataService(DbInstance, HttpUtil.Profile);
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
        public HttpResult<Inv_ItemReturnJson> SaveItemReturn(Inv_ItemReturnJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ItemReturnJson>();
            //optional permission, check permission in the SaveItemReturnLogic insted
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemReturn.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemReturn.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                 var entityReceived = new Inv_ItemReturn();
                 var dbAttachedEntity = new Inv_ItemReturn();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SaveItemReturnLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<Inv_ItemReturnJson> SaveItemReturn2(Inv_ItemReturnJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ItemReturnJson>();
            try
            {
                using (Inv_ItemReturnDataService itemReturnDataService = new Inv_ItemReturnDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new Inv_ItemReturn();
                    var dbAttachedEntity = new Inv_ItemReturn();
                    json.Map(entityReceived);
                    if (itemReturnDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<Inv_ItemReturnJson> SaveItemReturnList(IList<Inv_ItemReturnJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_ItemReturnJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemReturn.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemReturn.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_ItemReturnDataService itemReturnDataService = new Inv_ItemReturnDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<Inv_ItemReturn>();
                    var dbAttachedListEntity = new List<Inv_ItemReturn>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (itemReturnDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<Inv_ItemReturnJson> GetDeleteItemReturnById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ItemReturnJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemReturn.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_ItemReturnDataService itemReturnDataService = new Inv_ItemReturnDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!itemReturnDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveItemReturn(Inv_ItemReturn newObj, out string error)
        {
            error = "";
            //if (newObj.ItemId.IsValid() && newObj.ItemId.Length>50)
            //{
            //    error += "Item Id exceeded its maximum length 50.";
            //    return false;
            //}
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
            //var res =  DbInstance.Inv_ItemReturn.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A ItemReturn already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveItemReturnLogic(Inv_ItemReturn newObj,ref Inv_ItemReturn dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Item Return to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveItemReturn(newObj, out error))
                return false;

            bool isNewObject = true;
            Inv_ItemReturn objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Inv_ItemReturn.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Inv_ItemReturn.GetNew(newObj.Id);
                DbInstance.Inv_ItemReturn.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemReturn.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemReturn.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
           objToSave.ItemId =  newObj.ItemId ;
           objToSave.ReturnDate =  newObj.ReturnDate ;
           objToSave.ReceivedBy =  newObj.ReceivedBy ;
           objToSave.ToStore =  newObj.ToStore ;
           objToSave.ReturnStatus =  newObj.ReturnStatus ;
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

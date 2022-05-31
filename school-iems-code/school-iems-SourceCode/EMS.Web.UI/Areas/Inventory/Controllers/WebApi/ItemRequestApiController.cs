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

    public class ItemRequestApiController : EmployeeBaseApiController
	{
        public ItemRequestApiController()
        {  }
       
        #region ItemRequest 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Invt_ItemRequestJson> GetPagedItemRequest(string textkey, int? pageSize, int? pageNo
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Invt_ItemRequestJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.ItemRequest.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.ItemRequest.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.ItemRequest.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/

            try
            {
                using (Invt_ItemRequestDataService itemRequestDataService = new Invt_ItemRequestDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<Invt_ItemRequest> query = DbInstance.Invt_ItemRequest.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.RequestPersonName.ToLower().Contains(textkey.ToLower()));
                    }
                 
                    var entities = itemRequestDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Invt_ItemRequestJson>();
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
        private HttpListResult<Invt_ItemRequestJson> GetAllItemRequest()
        {
            string error = string.Empty;
            var result = new HttpListResult<Invt_ItemRequestJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.ItemRequest.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.ItemRequest.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.ItemRequest.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Invt_ItemRequestDataService itemRequestDataService = new Invt_ItemRequestDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<Invt_ItemRequestJson>();
                    var entities = itemRequestDataService.GetAll(out error);
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
        public HttpResult<Invt_ItemRequestJson> GetItemRequestById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Invt_ItemRequestJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.ItemRequest.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.ItemRequest.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.ItemRequest.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Invt_ItemRequestDataService itemRequestDataService = new Invt_ItemRequestDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Invt_ItemRequestJson();
                    Invt_ItemRequest entity;
                    if (id <= 0)
                    {
                        entity = Invt_ItemRequest.GetNew();
                    }
                    else
                    {
                        entity = itemRequestDataService.GetById(id);
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
        private HttpResult<Invt_ItemRequestJson> GetItemRequestByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Invt_ItemRequestJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.ItemRequest.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.ItemRequest.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.ItemRequest.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Invt_ItemRequestDataService itemRequestDataService = new Invt_ItemRequestDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Invt_ItemRequestJson();
                    Invt_ItemRequest entity;
                    if (id <= 0)
                    {
                        entity = Invt_ItemRequest.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("Invt_ItemRequest");
                        //includeTables.Add("Invt_ItemRequest");

                        entity = itemRequestDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Invt_ItemRequestJson> GetItemRequestListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Invt_ItemRequestJson>();
            try
            {
                //Invt_ItemRequestDataService itemRequestDataService =
                //    new Invt_ItemRequestDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.RequestTypeEnumList = EnumUtil.GetEnumList(typeof(Invt_ItemRequest.RequestTypeEnum));
                result.DataExtra.UnitTypeEnumList = EnumUtil.GetEnumList(typeof(Invt_ItemRequest.UnitTypeEnum));
                result.DataExtra.StatusEnumList = EnumUtil.GetEnumList(typeof(Invt_ItemRequest.StatusEnum));
                //DropDown Option Tables
                 
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<Invt_ItemRequestJson> GetItemRequestDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Invt_ItemRequestJson>();
            try
            {
                //Invt_ItemRequestDataService itemRequestDataService =
                //    new Invt_ItemRequestDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.RequestTypeEnumList = EnumUtil.GetEnumList(typeof(Invt_ItemRequest.RequestTypeEnum));
                result.DataExtra.UnitTypeEnumList = EnumUtil.GetEnumList(typeof(Invt_ItemRequest.UnitTypeEnum));
                result.DataExtra.StatusEnumList = EnumUtil.GetEnumList(typeof(Invt_ItemRequest.StatusEnum));
                //DropDown Option Tables
                result.DataExtra.ItemList = DbInstance.Invt_Item.AsEnumerable().Select(t => new
                { Id = t.Id,Name = t.Name }).ToList();
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
        public HttpResult<Invt_ItemRequestJson> SaveItemRequest(Invt_ItemRequestJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Invt_ItemRequestJson>();
            //optional permission, check permission in the SaveItemRequestLogic insted
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.ItemRequest.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.ItemRequest.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                 var entityReceived = new Invt_ItemRequest();
                 var dbAttachedEntity = new Invt_ItemRequest();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SaveItemRequestLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<Invt_ItemRequestJson> SaveItemRequest2(Invt_ItemRequestJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Invt_ItemRequestJson>();
            try
            {
                using (Invt_ItemRequestDataService itemRequestDataService = new Invt_ItemRequestDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new Invt_ItemRequest();
                    var dbAttachedEntity = new Invt_ItemRequest();
                    json.Map(entityReceived);
                    if (itemRequestDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<Invt_ItemRequestJson> SaveItemRequestList(IList<Invt_ItemRequestJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Invt_ItemRequestJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.ItemRequest.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.ItemRequest.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Invt_ItemRequestDataService itemRequestDataService = new Invt_ItemRequestDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<Invt_ItemRequest>();
                    var dbAttachedListEntity = new List<Invt_ItemRequest>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (itemRequestDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<Invt_ItemRequestJson> GetDeleteItemRequestById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Invt_ItemRequestJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.ItemRequest.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Invt_ItemRequestDataService itemRequestDataService = new Invt_ItemRequestDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!itemRequestDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveItemRequest(Invt_ItemRequest newObj, out string error)
        {
            error = "";
            if (newObj.RequestTypeEnumId==null)
            {
                error += "Please select valid Request Type.";
                return false;
            }
            if (!newObj.RequestPersonName.IsValid())
            {
                error += "Request Person Name is not valid.";
                return false;
            }
            if (newObj.RequestPersonName==null)
            {
                error += "Request Person Name required.";
                return false;
            }
            if (newObj.ReturnDate!=null && !newObj.ReturnDate.IsValid())
            {
                newObj.ReturnDate=null;//just put null,if its nullable and not valid.
               //error += "Return Date is not valid.";
               //return false;
            }
            if (newObj.ItemId==null)
            {
                error += "Item Id required.";
                return false;
            }
            if (newObj.UnitTypeEnumId==null)
            {
                error += "Please select valid Unit Type.";
                return false;
            }
            if (newObj.Unit==null)
            {
                error += "Unit required.";
                return false;
            }
            if (newObj.StatusEnumId==null)
            {
                error += "Please select valid Status.";
                return false;
            }
            if (newObj.IsDeleted==null)
            {
                error += "Is Deleted required.";
                return false;
            }
            if (newObj.IsReturn==null)
            {
                error += "Is Return required.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.Invt_ItemRequest.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A ItemRequest already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveItemRequestLogic(Invt_ItemRequest newObj,ref Invt_ItemRequest dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Item Request to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveItemRequest(newObj, out error))
                return false;

            bool isNewObject = true;
            Invt_ItemRequest objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Invt_ItemRequest.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Invt_ItemRequest.GetNew(newObj.Id);
                DbInstance.Invt_ItemRequest.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.ItemRequest.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.ItemRequest.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
           objToSave.RequestTypeEnumId =  newObj.RequestTypeEnumId ;
           objToSave.RequestPersonName =  newObj.RequestPersonName ;
           objToSave.ReturnDate =  newObj.ReturnDate ;
           objToSave.ItemId =  newObj.ItemId ;
           objToSave.UnitTypeEnumId =  newObj.UnitTypeEnumId ;
           objToSave.Unit =  newObj.Unit ;
           objToSave.StatusEnumId =  newObj.StatusEnumId ;
           objToSave.IsDeleted =  newObj.IsDeleted ;
           objToSave.IsReturn =  newObj.IsReturn ;
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

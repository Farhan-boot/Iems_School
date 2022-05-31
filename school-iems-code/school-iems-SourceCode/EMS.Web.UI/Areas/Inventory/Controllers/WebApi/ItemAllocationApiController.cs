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

    public class ItemAllocationApiController : EmployeeBaseApiController
	{
        public ItemAllocationApiController()
        {  }
       
        #region ItemAllocation 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Inv_ItemAllocationJson> GetPagedItemAllocation(string textkey, int? pageSize, int? pageNo
           ,Int32 id= 0      
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Inv_ItemAllocationJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemAllocation.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemAllocation.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemAllocation.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/

            try
            {
                using (Inv_ItemAllocationDataService itemAllocationDataService = new Inv_ItemAllocationDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<Inv_ItemAllocation> query = DbInstance.Inv_ItemAllocation.OrderByDescending(x => x.Id);
                    //if (textkey.IsValid())
                    //{
                    //    query = query.Where(q => q.ItemId.Contains(textkey.ToLower()));
                    //}
                    if (id>0)
                    {
                        query = query.Where(q => q.Id== id);
                    }  
                 
                    var entities = itemAllocationDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Inv_ItemAllocationJson>();
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
        private HttpListResult<Inv_ItemAllocationJson> GetAllItemAllocation()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_ItemAllocationJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemAllocation.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemAllocation.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemAllocation.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_ItemAllocationDataService itemAllocationDataService = new Inv_ItemAllocationDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<Inv_ItemAllocationJson>();
                    var entities = itemAllocationDataService.GetAll(out error);
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
        public HttpResult<Inv_ItemAllocationJson> GetItemAllocationById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ItemAllocationJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemAllocation.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemAllocation.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemAllocation.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_ItemAllocationDataService itemAllocationDataService = new Inv_ItemAllocationDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Inv_ItemAllocationJson();
                    Inv_ItemAllocation entity;
                    if (id <= 0)
                    {
                        entity = Inv_ItemAllocation.GetNew();
                    }
                    else
                    {
                        entity = itemAllocationDataService.GetById(id);
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
        private HttpResult<Inv_ItemAllocationJson> GetItemAllocationByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ItemAllocationJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemAllocation.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemAllocation.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemAllocation.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_ItemAllocationDataService itemAllocationDataService = new Inv_ItemAllocationDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Inv_ItemAllocationJson();
                    Inv_ItemAllocation entity;
                    if (id <= 0)
                    {
                        entity = Inv_ItemAllocation.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("Inv_ItemAllocation");
                        //includeTables.Add("Inv_ItemAllocation");

                        entity = itemAllocationDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Inv_ItemAllocationJson> GetItemAllocationListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_ItemAllocationJson>();
            try
            {
                //Inv_ItemAllocationDataService itemAllocationDataService =
                //    new Inv_ItemAllocationDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                //DropDown Option Tables
                 
                //result.DataExtra.ItemAllocationList = DbInstance.Inv_ItemAllocation.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<Inv_ItemAllocationJson> GetItemAllocationDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_ItemAllocationJson>();
            try
            {
                //Inv_ItemAllocationDataService itemAllocationDataService =
                //    new Inv_ItemAllocationDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List

                //DropDown Option Tables

                //result.DataExtra.ItemAllocationList = DbInstance.Inv_ItemAllocation.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();


                result.DataExtra.ItemList = DbInstance.Inv_Item.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.ItemName }).ToList();

                result.DataExtra.StoreList = DbInstance.Inv_Store.AsEnumerable().Select(t => new
                    { Id = t.Id, Name = t.Name }).ToList();

                result.DataExtra.UserList = DbInstance.User_Account.AsEnumerable().Select(t => new
                    { Id = t.Id, Name = t.FullName }).ToList();
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
        public HttpResult<Inv_ItemAllocationJson> SaveItemAllocation(Inv_ItemAllocationJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ItemAllocationJson>();
            //optional permission, check permission in the SaveItemAllocationLogic insted
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemAllocation.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemAllocation.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                 var entityReceived = new Inv_ItemAllocation();
                 var dbAttachedEntity = new Inv_ItemAllocation();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SaveItemAllocationLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<Inv_ItemAllocationJson> SaveItemAllocation2(Inv_ItemAllocationJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ItemAllocationJson>();
            try
            {
                using (Inv_ItemAllocationDataService itemAllocationDataService = new Inv_ItemAllocationDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new Inv_ItemAllocation();
                    var dbAttachedEntity = new Inv_ItemAllocation();
                    json.Map(entityReceived);
                    if (itemAllocationDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<Inv_ItemAllocationJson> SaveItemAllocationList(IList<Inv_ItemAllocationJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_ItemAllocationJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemAllocation.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemAllocation.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_ItemAllocationDataService itemAllocationDataService = new Inv_ItemAllocationDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<Inv_ItemAllocation>();
                    var dbAttachedListEntity = new List<Inv_ItemAllocation>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (itemAllocationDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<Inv_ItemAllocationJson> GetDeleteItemAllocationById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_ItemAllocationJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemAllocation.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_ItemAllocationDataService itemAllocationDataService = new Inv_ItemAllocationDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!itemAllocationDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveItemAllocation(Inv_ItemAllocation newObj, out string error)
        {
            error = "";
            //if (newObj.ItemId.IsValid() && newObj.ItemId.Length>50)
            //{
            //    error += "Item Id exceeded its maximum length 50.";
            //    return false;
            //}


            if (newObj.AllocationDate!=null && !newObj.AllocationDate.IsValid())
            {
                newObj.AllocationDate=null;//just put null,if its nullable and not valid.
               //error += "Allocation Date is not valid.";
               //return false;
            }


            if (newObj.Remarks.IsValid() && newObj.Remarks.Length>1000)
            {
                error += "Remarks exceeded its maximum length 1000.";
                return false;
            }
            if (newObj.IsDeleted==null)
            {
                error += "Is Deleted required.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.Inv_ItemAllocation.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A ItemAllocation already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveItemAllocationLogic(Inv_ItemAllocation newObj,ref Inv_ItemAllocation dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Item Allocation to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveItemAllocation(newObj, out error))
                return false;

            bool isNewObject = true;
            Inv_ItemAllocation objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Inv_ItemAllocation.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Inv_ItemAllocation.GetNew(newObj.Id);
                DbInstance.Inv_ItemAllocation.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemAllocation.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.ItemAllocation.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
           objToSave.ItemId =  newObj.ItemId ;
           objToSave.FromStore =  newObj.FromStore ;
           objToSave.AllocatedTo =  newObj.AllocatedTo ;
           objToSave.AllocationDate =  newObj.AllocationDate ;
           objToSave.AllocationStatus =  newObj.AllocationStatus ;
           objToSave.ReferenceBy =  newObj.ReferenceBy ;
           objToSave.Remarks =  newObj.Remarks ;
           objToSave.IsDeleted =  newObj.IsDeleted ;
           objToSave.LastChangedById = newObj.LastChangedById = HttpUtil.Profile.UserId;
           objToSave.LastChanged = newObj.LastChanged = DateTime.Now;
            //New Object
            objToSave.RequestedItemId = newObj.RequestedItemId;
            objToSave.ApprovedQuantity = newObj.ApprovedQuantity;
            objToSave.ApprovedById = newObj.ApprovedById;

            dbAddedObj = objToSave;
           
            //here save any child table
            return true;
        }
        #endregion
        #endregion

	}
}

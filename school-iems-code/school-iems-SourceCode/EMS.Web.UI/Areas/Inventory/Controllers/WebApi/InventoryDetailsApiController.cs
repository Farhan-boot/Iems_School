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

    public class InventoryDetailsApiController : EmployeeBaseApiController
	{
        public InventoryDetailsApiController()
        {  }
       
        #region InventoryDetails 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Inv_InventoryDetailsJson> GetPagedInventoryDetails(string textkey, int? pageSize, int? pageNo
           ,Int32 inventoryId= 0      
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Inv_InventoryDetailsJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.InventoryDetails.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.InventoryDetails.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.InventoryDetails.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/

            try
            {
                using (Inv_InventoryDetailsDataService inventoryDetailsDataService = new Inv_InventoryDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<Inv_InventoryDetails> query = DbInstance.Inv_InventoryDetails.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Description.ToLower().Contains(textkey.ToLower()));
                    }
                    if (inventoryId>0)
                    {
                        query = query.Where(q => q.InventoryId== inventoryId);
                    }  
                 
                    var entities = inventoryDetailsDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Inv_InventoryDetailsJson>();
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
        private HttpListResult<Inv_InventoryDetailsJson> GetAllInventoryDetails()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_InventoryDetailsJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.InventoryDetails.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.InventoryDetails.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.InventoryDetails.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_InventoryDetailsDataService inventoryDetailsDataService = new Inv_InventoryDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<Inv_InventoryDetailsJson>();
                    var entities = inventoryDetailsDataService.GetAll(out error);
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
        public HttpResult<Inv_InventoryDetailsJson> GetInventoryDetailsById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_InventoryDetailsJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.InventoryDetails.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.InventoryDetails.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.InventoryDetails.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_InventoryDetailsDataService inventoryDetailsDataService = new Inv_InventoryDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Inv_InventoryDetailsJson();
                    Inv_InventoryDetails entity;
                    if (id <= 0)
                    {
                        entity = Inv_InventoryDetails.GetNew();
                    }
                    else
                    {
                        entity = inventoryDetailsDataService.GetById(id);
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
        private HttpResult<Inv_InventoryDetailsJson> GetInventoryDetailsByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_InventoryDetailsJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.InventoryDetails.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.InventoryDetails.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.InventoryDetails.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_InventoryDetailsDataService inventoryDetailsDataService = new Inv_InventoryDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Inv_InventoryDetailsJson();
                    Inv_InventoryDetails entity;
                    if (id <= 0)
                    {
                        entity = Inv_InventoryDetails.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("Inv_InventoryDetails");
                        //includeTables.Add("Inv_InventoryDetails");

                        entity = inventoryDetailsDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Inv_InventoryDetailsJson> GetInventoryDetailsListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_InventoryDetailsJson>();
            try
            {
                //Inv_InventoryDetailsDataService inventoryDetailsDataService =
                //    new Inv_InventoryDetailsDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.StatusEnumList = EnumUtil.GetEnumList(typeof(Inv_InventoryDetails.StatusEnum));
                //DropDown Option Tables
                 
                //result.DataExtra.InventoryList = DbInstance.Inv_Inventory.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<Inv_InventoryDetailsJson> GetInventoryDetailsDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_InventoryDetailsJson>();
            try
            {
                //Inv_InventoryDetailsDataService inventoryDetailsDataService =
                //    new Inv_InventoryDetailsDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.StatusEnumList = EnumUtil.GetEnumList(typeof(Inv_InventoryDetails.StatusEnum));
                //DropDown Option Tables
                 
                //result.DataExtra.InventoryList = DbInstance.Inv_Inventory.AsEnumerable().Select(t => new
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
        public HttpResult<Inv_InventoryDetailsJson> SaveInventoryDetails(Inv_InventoryDetailsJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_InventoryDetailsJson>();
            //optional permission, check permission in the SaveInventoryDetailsLogic insted
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.InventoryDetails.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.InventoryDetails.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                 var entityReceived = new Inv_InventoryDetails();
                 var dbAttachedEntity = new Inv_InventoryDetails();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SaveInventoryDetailsLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<Inv_InventoryDetailsJson> SaveInventoryDetails2(Inv_InventoryDetailsJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_InventoryDetailsJson>();
            try
            {
                using (Inv_InventoryDetailsDataService inventoryDetailsDataService = new Inv_InventoryDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new Inv_InventoryDetails();
                    var dbAttachedEntity = new Inv_InventoryDetails();
                    json.Map(entityReceived);
                    if (inventoryDetailsDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<Inv_InventoryDetailsJson> SaveInventoryDetailsList(IList<Inv_InventoryDetailsJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_InventoryDetailsJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.InventoryDetails.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.InventoryDetails.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_InventoryDetailsDataService inventoryDetailsDataService = new Inv_InventoryDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<Inv_InventoryDetails>();
                    var dbAttachedListEntity = new List<Inv_InventoryDetails>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (inventoryDetailsDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<Inv_InventoryDetailsJson> GetDeleteInventoryDetailsById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_InventoryDetailsJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.InventoryDetails.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_InventoryDetailsDataService inventoryDetailsDataService = new Inv_InventoryDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!inventoryDetailsDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveInventoryDetails(Inv_InventoryDetails newObj, out string error)
        {
            error = "";




            if (newObj.WarrentyStartDate!=null && !newObj.WarrentyStartDate.IsValid())
            {
                newObj.WarrentyStartDate=null;//just put null,if its nullable and not valid.
               //error += "Warrenty Start Date is not valid.";
               //return false;
            }
            if (newObj.WarrentyExpairDate!=null && !newObj.WarrentyExpairDate.IsValid())
            {
                newObj.WarrentyExpairDate=null;//just put null,if its nullable and not valid.
               //error += "Warrenty Expair Date is not valid.";
               //return false;
            }
            if (newObj.Description.IsValid() && newObj.Description.Length>50)
            {
                error += "Description exceeded its maximum length 50.";
                return false;
            }
            if (newObj.OwnBarcode.IsValid() && newObj.OwnBarcode.Length>50)
            {
                error += "Own Barcode exceeded its maximum length 50.";
                return false;
            }
            if (newObj.OurBarcode.IsValid() && newObj.OurBarcode.Length>50)
            {
                error += "Our Barcode exceeded its maximum length 50.";
                return false;
            }



            if (newObj.IsDeleted==null)
            {
                error += "Is Deleted required.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.Inv_InventoryDetails.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A InventoryDetails already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveInventoryDetailsLogic(Inv_InventoryDetails newObj,ref Inv_InventoryDetails dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Inventory Details to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveInventoryDetails(newObj, out error))
                return false;

            bool isNewObject = true;
            Inv_InventoryDetails objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Inv_InventoryDetails.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Inv_InventoryDetails.GetNew(newObj.Id);
                DbInstance.Inv_InventoryDetails.Add(objToSave);
               
               
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.InventoryDetails.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.InventoryDetails.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
           objToSave.InventoryId =  newObj.InventoryId ;
           objToSave.ProductCategoryId =  newObj.ProductCategoryId ;
           objToSave.Quantity =  newObj.Quantity ;
           objToSave.UnitPrice =  newObj.UnitPrice ;
           objToSave.WarrentyStartDate =  newObj.WarrentyStartDate ;
           objToSave.WarrentyExpairDate =  newObj.WarrentyExpairDate ;
           objToSave.Description =  newObj.Description ;
           objToSave.OwnBarcode =  newObj.OwnBarcode ;
           objToSave.OurBarcode =  newObj.OurBarcode ;
           objToSave.StatusEnumId =  newObj.StatusEnumId ;
           objToSave.Remark =  newObj.Remark ;
           objToSave.IsBarcoded =  newObj.IsBarcoded ;
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

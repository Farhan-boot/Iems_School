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

    public class RequisitionDetailsApiController : EmployeeBaseApiController
	{
        public RequisitionDetailsApiController()
        {  }
       
        #region RequisitionDetails 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Inv_RequisitionDetailsJson> GetPagedRequisitionDetails(string textkey, int? pageSize, int? pageNo
           ,Int32 requisitionId= 0      
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Inv_RequisitionDetailsJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.RequisitionDetails.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.RequisitionDetails.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.RequisitionDetails.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/

            try
            {
                using (Inv_RequisitionDetailsDataService requisitionDetailsDataService = new Inv_RequisitionDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<Inv_RequisitionDetails> query = DbInstance.Inv_RequisitionDetails.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.ItemName.ToLower().Contains(textkey.ToLower()));
                    }
                    if (requisitionId>0)
                    {
                        query = query.Where(q => q.RequisitionId== requisitionId);
                    }  
                 
                    var entities = requisitionDetailsDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Inv_RequisitionDetailsJson>();
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
        private HttpListResult<Inv_RequisitionDetailsJson> GetAllRequisitionDetails()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_RequisitionDetailsJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.RequisitionDetails.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.RequisitionDetails.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.RequisitionDetails.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_RequisitionDetailsDataService requisitionDetailsDataService = new Inv_RequisitionDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<Inv_RequisitionDetailsJson>();
                    var entities = requisitionDetailsDataService.GetAll(out error);
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
        public HttpResult<Inv_RequisitionDetailsJson> GetRequisitionDetailsById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_RequisitionDetailsJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.RequisitionDetails.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.RequisitionDetails.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.RequisitionDetails.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_RequisitionDetailsDataService requisitionDetailsDataService = new Inv_RequisitionDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Inv_RequisitionDetailsJson();
                    Inv_RequisitionDetails entity;
                    if (id <= 0)
                    {
                        entity = Inv_RequisitionDetails.GetNew();
                    }
                    else
                    {
                        entity = requisitionDetailsDataService.GetById(id);
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
        private HttpResult<Inv_RequisitionDetailsJson> GetRequisitionDetailsByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_RequisitionDetailsJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.RequisitionDetails.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.RequisitionDetails.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.RequisitionDetails.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_RequisitionDetailsDataService requisitionDetailsDataService = new Inv_RequisitionDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Inv_RequisitionDetailsJson();
                    Inv_RequisitionDetails entity;
                    if (id <= 0)
                    {
                        entity = Inv_RequisitionDetails.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("Inv_RequisitionDetails");
                        //includeTables.Add("Inv_RequisitionDetails");

                        entity = requisitionDetailsDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Inv_RequisitionDetailsJson> GetRequisitionDetailsListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_RequisitionDetailsJson>();
            try
            {
                //Inv_RequisitionDetailsDataService requisitionDetailsDataService =
                //    new Inv_RequisitionDetailsDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                //DropDown Option Tables
                 
                //result.DataExtra.RequisitionList = DbInstance.Inv_Requisition.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<Inv_RequisitionDetailsJson> GetRequisitionDetailsDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_RequisitionDetailsJson>();
            try
            {
                //Inv_RequisitionDetailsDataService requisitionDetailsDataService =
                //    new Inv_RequisitionDetailsDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List

                //DropDown Option Tables

                result.DataExtra.RequisitionList = DbInstance.Inv_Requisition.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.RequestedByEmployeeId }).ToList();
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
        public HttpResult<Inv_RequisitionDetailsJson> SaveRequisitionDetails(Inv_RequisitionDetailsJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_RequisitionDetailsJson>();
            //optional permission, check permission in the SaveRequisitionDetailsLogic insted
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.RequisitionDetails.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.RequisitionDetails.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                 var entityReceived = new Inv_RequisitionDetails();
                 var dbAttachedEntity = new Inv_RequisitionDetails();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SaveRequisitionDetailsLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<Inv_RequisitionDetailsJson> SaveRequisitionDetails2(Inv_RequisitionDetailsJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_RequisitionDetailsJson>();
            try
            {
                using (Inv_RequisitionDetailsDataService requisitionDetailsDataService = new Inv_RequisitionDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new Inv_RequisitionDetails();
                    var dbAttachedEntity = new Inv_RequisitionDetails();
                    json.Map(entityReceived);
                    if (requisitionDetailsDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<Inv_RequisitionDetailsJson> SaveRequisitionDetailsList(IList<Inv_RequisitionDetailsJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_RequisitionDetailsJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.RequisitionDetails.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.RequisitionDetails.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_RequisitionDetailsDataService requisitionDetailsDataService = new Inv_RequisitionDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<Inv_RequisitionDetails>();
                    var dbAttachedListEntity = new List<Inv_RequisitionDetails>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (requisitionDetailsDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<Inv_RequisitionDetailsJson> GetDeleteRequisitionDetailsById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_RequisitionDetailsJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.RequisitionDetails.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_RequisitionDetailsDataService requisitionDetailsDataService = new Inv_RequisitionDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!requisitionDetailsDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveRequisitionDetails(Inv_RequisitionDetails newObj, out string error)
        {
            error = "";

            if (newObj.ItemName.IsValid() && newObj.ItemName.Length>50)
            {
                error += "Item Name exceeded its maximum length 50.";
                return false;
            }

            if (newObj.Detail.IsValid() && newObj.Detail.Length>500)
            {
                error += "Detail exceeded its maximum length 500.";
                return false;
            }


            if (newObj.IsDeleted==null)
            {
                error += "Is Deleted required.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.Inv_RequisitionDetails.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A RequisitionDetails already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveRequisitionDetailsLogic(Inv_RequisitionDetails newObj,ref Inv_RequisitionDetails dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Requisition Details to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveRequisitionDetails(newObj, out error))
                return false;

            bool isNewObject = true;
            Inv_RequisitionDetails objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Inv_RequisitionDetails.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Inv_RequisitionDetails.GetNew(newObj.Id);
                DbInstance.Inv_RequisitionDetails.Add(objToSave);
               
               
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.RequisitionDetails.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.RequisitionDetails.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
           objToSave.RequisitionId =  newObj.RequisitionId ;
           objToSave.ItemName =  newObj.ItemName ;
           objToSave.ItemId =  newObj.ItemId ;
           objToSave.Detail =  newObj.Detail ;
           objToSave.Quantity =  newObj.Quantity ;
           objToSave.ApprovedQuantity =  newObj.ApprovedQuantity ;
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

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

    public class RequisitionApiController : EmployeeBaseApiController
	{
        public RequisitionApiController()
        {  }
       
        #region Requisition 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Inv_RequisitionJson> GetPagedRequisition(string textkey, int? pageSize, int? pageNo
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Inv_RequisitionJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Requisition.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Requisition.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Requisition.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/

            try
            {
                using (Inv_RequisitionDataService requisitionDataService = new Inv_RequisitionDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<Inv_Requisition> query = DbInstance.Inv_Requisition.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Purpose.ToLower().Contains(textkey.ToLower()));
                    }
                 
                    var entities = requisitionDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Inv_RequisitionJson>();
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
        private HttpListResult<Inv_RequisitionJson> GetAllRequisition()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_RequisitionJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Requisition.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Requisition.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Requisition.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_RequisitionDataService requisitionDataService = new Inv_RequisitionDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<Inv_RequisitionJson>();
                    var entities = requisitionDataService.GetAll(out error);
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
        public HttpResult<Inv_RequisitionJson> GetRequisitionById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_RequisitionJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Requisition.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Requisition.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Requisition.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_RequisitionDataService requisitionDataService = new Inv_RequisitionDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Inv_RequisitionJson();
                    Inv_Requisition entity;
                    if (id <= 0)
                    {
                        entity = Inv_Requisition.GetNew();
                    }
                    else
                    {
                        entity = requisitionDataService.GetById(id);
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
        private HttpResult<Inv_RequisitionJson> GetRequisitionByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_RequisitionJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Requisition.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Requisition.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Requisition.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_RequisitionDataService requisitionDataService = new Inv_RequisitionDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Inv_RequisitionJson();
                    Inv_Requisition entity;
                    if (id <= 0)
                    {
                        entity = Inv_Requisition.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("Inv_Requisition");
                        //includeTables.Add("Inv_Requisition");

                        entity = requisitionDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Inv_RequisitionJson> GetRequisitionListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_RequisitionJson>();
            try
            {
                //Inv_RequisitionDataService requisitionDataService =
                //    new Inv_RequisitionDataService(DbInstance, HttpUtil.Profile);
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
        public HttpListResult<Inv_RequisitionJson> GetRequisitionDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_RequisitionJson>();
            try
            {
                //Inv_RequisitionDataService requisitionDataService =
                //    new Inv_RequisitionDataService(DbInstance, HttpUtil.Profile);
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
        public HttpResult<Inv_RequisitionJson> SaveRequisition(Inv_RequisitionJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_RequisitionJson>();
            //optional permission, check permission in the SaveRequisitionLogic insted
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Requisition.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Requisition.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                 var entityReceived = new Inv_Requisition();
                 var dbAttachedEntity = new Inv_Requisition();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SaveRequisitionLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<Inv_RequisitionJson> SaveRequisition2(Inv_RequisitionJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_RequisitionJson>();
            try
            {
                using (Inv_RequisitionDataService requisitionDataService = new Inv_RequisitionDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new Inv_Requisition();
                    var dbAttachedEntity = new Inv_Requisition();
                    json.Map(entityReceived);
                    if (requisitionDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<Inv_RequisitionJson> SaveRequisitionList(IList<Inv_RequisitionJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Inv_RequisitionJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Requisition.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Requisition.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_RequisitionDataService requisitionDataService = new Inv_RequisitionDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<Inv_Requisition>();
                    var dbAttachedListEntity = new List<Inv_Requisition>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (requisitionDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<Inv_RequisitionJson> GetDeleteRequisitionById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Inv_RequisitionJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Requisition.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Inv_RequisitionDataService requisitionDataService = new Inv_RequisitionDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!requisitionDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveRequisition(Inv_Requisition newObj, out string error)
        {
            error = "";

            if (newObj.RequisitionDate!=null && !newObj.RequisitionDate.IsValid())
            {
                newObj.RequisitionDate=null;//just put null,if its nullable and not valid.
               //error += "Requisition Date is not valid.";
               //return false;
            }
            if (newObj.RequireDate!=null && !newObj.RequireDate.IsValid())
            {
                newObj.RequireDate=null;//just put null,if its nullable and not valid.
               //error += "Require Date is not valid.";
               //return false;
            }
            if (newObj.Purpose.IsValid() && newObj.Purpose.Length>500)
            {
                error += "Purpose exceeded its maximum length 500.";
                return false;
            }
            if (newObj.Remark.IsValid() && newObj.Remark.Length>500)
            {
                error += "Remark exceeded its maximum length 500.";
                return false;
            }





            if (newObj.IsDeleted==null)
            {
                error += "Is Deleted required.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.Inv_Requisition.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A Requisition already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveRequisitionLogic(Inv_Requisition newObj,ref Inv_Requisition dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Requisition to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveRequisition(newObj, out error))
                return false;

            bool isNewObject = true;
            Inv_Requisition objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Inv_Requisition.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Inv_Requisition.GetNew(newObj.Id);
                DbInstance.Inv_Requisition.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Requisition.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.InventoryArea.Requisition.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
           objToSave.RequestedByEmployeeId =  newObj.RequestedByEmployeeId ;
           objToSave.RequisitionDate =  newObj.RequisitionDate ;
           objToSave.RequireDate =  newObj.RequireDate ;
           objToSave.Purpose =  newObj.Purpose ;
           objToSave.Remark =  newObj.Remark ;
           objToSave.ReferencedByEmployeeId =  newObj.ReferencedByEmployeeId ;
           objToSave.IssuedOrReleaseByUserId =  newObj.IssuedOrReleaseByUserId ;
           objToSave.ApprovedByAdminId =  newObj.ApprovedByAdminId ;
           objToSave.ReceivedByEmployeeId =  newObj.ReceivedByEmployeeId ;
           objToSave.Status =  newObj.Status ;
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

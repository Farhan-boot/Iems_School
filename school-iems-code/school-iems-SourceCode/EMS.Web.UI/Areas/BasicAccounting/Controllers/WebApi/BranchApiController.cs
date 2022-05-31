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


namespace EMS.Web.UI.Areas.BasicAccounting.Controllers.WebApi
{

    public class BranchApiController : EmployeeBaseApiController
	{
        public BranchApiController()
        {  }
       
        #region Branch 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<BAcnt_BranchJson> GetPagedBranch(string textkey, int? pageSize, int? pageNo
           ,Int32 companyId= 0      
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<BAcnt_BranchJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Branch.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Branch.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Branch.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }

            try
            {
                var currentCompanyId = Facade.BasicAccountingFacade.CompanyFacade.GetCurrentCompanyId();

                if (currentCompanyId==null)
                {
                    result.HasError = true;
                    result.Errors.Add("Current company not found!");
                    return result;
                }

                //Set Current Company
                companyId = (int) currentCompanyId;

                using (BAcnt_BranchDataService branchDataService = new BAcnt_BranchDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<BAcnt_Branch> query = DbInstance.BAcnt_Branch.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Name.ToLower().Contains(textkey.ToLower()));
                    }
                    if (companyId>0)
                    {
                        query = query.Where(q => q.CompanyId== companyId);
                    }  
                 
                    var entities = branchDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<BAcnt_BranchJson>();
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
        private HttpListResult<BAcnt_BranchJson> GetAllBranch()
        {
            string error = string.Empty;
            var result = new HttpListResult<BAcnt_BranchJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.Branch.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.Branch.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.Branch.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (BAcnt_BranchDataService branchDataService = new BAcnt_BranchDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<BAcnt_BranchJson>();
                    var entities = branchDataService.GetAll(out error);
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
        public HttpResult<BAcnt_BranchJson> GetBranchById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<BAcnt_BranchJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Branch.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Branch.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Branch.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (BAcnt_BranchDataService branchDataService = new BAcnt_BranchDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new BAcnt_BranchJson();
                    BAcnt_Branch entity;
                    if (id <= 0)
                    {
                        entity = BAcnt_Branch.GetNew();
                    }
                    else
                    {
                        // Only Current company branch get
                        var currentCompanyId = Facade.BasicAccountingFacade.CompanyFacade.GetCurrentCompanyId();

                        entity = DbInstance.BAcnt_Branch.Where(x=>x.CompanyId== currentCompanyId).SingleOrDefault(x=>x.Id==id);
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
        private HttpResult<BAcnt_BranchJson> GetBranchByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<BAcnt_BranchJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.Branch.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.Branch.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.Branch.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (BAcnt_BranchDataService branchDataService = new BAcnt_BranchDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new BAcnt_BranchJson();
                    BAcnt_Branch entity;
                    if (id <= 0)
                    {
                        entity = BAcnt_Branch.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("BAcnt_Branch");
                        //includeTables.Add("BAcnt_Branch");

                        entity = branchDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<BAcnt_BranchJson> GetBranchListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<BAcnt_BranchJson>();
            try
            {
                //BAcnt_BranchDataService branchDataService =
                //    new BAcnt_BranchDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                //DropDown Option Tables
                 
                //result.DataExtra.CompanyAccountList = DbInstance.BAcnt_CompanyAccount.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<BAcnt_BranchJson> GetBranchDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<BAcnt_BranchJson>();
            try
            {
                //BAcnt_BranchDataService branchDataService =
                //    new BAcnt_BranchDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List

                //DropDown Option Tables

                result.DataExtra.CompanyAccountList = DbInstance.BAcnt_CompanyAccount
                    .Where(x=>x.IsCurrent)
                    .AsEnumerable().Select(t => new
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
        public HttpResult<BAcnt_BranchJson> SaveBranch(BAcnt_BranchJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<BAcnt_BranchJson>();
            //optional permission, check permission in the SaveBranchLogic insted
            if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Branch.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Branch.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                 var entityReceived = new BAcnt_Branch();
                 var dbAttachedEntity = new BAcnt_Branch();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SaveBranchLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<BAcnt_BranchJson> SaveBranch2(BAcnt_BranchJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<BAcnt_BranchJson>();
            try
            {
                using (BAcnt_BranchDataService branchDataService = new BAcnt_BranchDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new BAcnt_Branch();
                    var dbAttachedEntity = new BAcnt_Branch();
                    json.Map(entityReceived);
                    if (branchDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<BAcnt_BranchJson> SaveBranchList(IList<BAcnt_BranchJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<BAcnt_BranchJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.Branch.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.Branch.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (BAcnt_BranchDataService branchDataService = new BAcnt_BranchDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<BAcnt_Branch>();
                    var dbAttachedListEntity = new List<BAcnt_Branch>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (branchDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<BAcnt_BranchJson> GetDeleteBranchById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<BAcnt_BranchJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Branch.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (BAcnt_BranchDataService branchDataService = new BAcnt_BranchDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!branchDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveBranch(BAcnt_Branch newObj, out string error)
        {
            error = "";
            if (!newObj.Name.IsValid())
            {
                error += "Name is not valid.";
                return false;
            }
            if (newObj.Name.Length>255)
            {
                error += "Name exceeded its maximum length 255.";
                return false;
            }
            if (!newObj.ShortName.IsValid())
            {
                error += "Short Name is not valid.";
                return false;
            }
            if (newObj.ShortName.Length>50)
            {
                error += "Short Name exceeded its maximum length 50.";
                return false;
            }
            if (newObj.Code.IsValid() && newObj.Code.Length>50)
            {
                error += "Code exceeded its maximum length 50.";
                return false;
            }
            if (!newObj.Address.IsValid())
            {
                error += "Address is not valid.";
                return false;
            }
            if (newObj.Address==null)
            {
                error += "Address required.";
                return false;
            }
            if (newObj.OrderNo==null)
            {
                error += "Order No required.";
                return false;
            }
            if (newObj.IsEnable==null)
            {
                error += "Is Enable required.";
                return false;
            }
            if (newObj.IsDeleted==null)
            {
                error += "Is Deleted required.";
                return false;
            }
            if (newObj.CompanyId<=0)
            {
                error += "Please select valid Company.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.BAcnt_Branch.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A Branch already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveBranchLogic(BAcnt_Branch newObj,ref BAcnt_Branch dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Branch to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveBranch(newObj, out error))
                return false;

            bool isNewObject = true;
            BAcnt_Branch objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.BAcnt_Branch.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = BAcnt_Branch.GetNew(newObj.Id);
                DbInstance.BAcnt_Branch.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            
            if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Branch.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Branch.CanEdit,
                   out error))
            {
                return false;
            }

            //binding object  
           objToSave.Name =  newObj.Name ;
           objToSave.ShortName =  newObj.ShortName ;
           objToSave.Code =  newObj.Code ;
           objToSave.Address =  newObj.Address ;
           objToSave.OrderNo =  newObj.OrderNo ;
           objToSave.IsEnable =  newObj.IsEnable ;
           objToSave.IsDeleted =  newObj.IsDeleted ;
           objToSave.CompanyId =  newObj.CompanyId ;
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

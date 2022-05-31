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

    public class CompanyAccountApiController : EmployeeBaseApiController
	{
        public CompanyAccountApiController()
        {  }
       
        #region CompanyAccount 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<BAcnt_CompanyAccountJson> GetPagedCompanyAccount(string textkey, int? pageSize, int? pageNo
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<BAcnt_CompanyAccountJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.CompanyAccount.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.CompanyAccount.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.CompanyAccount.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }

            try
            {
                using (BAcnt_CompanyAccountDataService companyAccountDataService = new BAcnt_CompanyAccountDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<BAcnt_CompanyAccount> query = DbInstance.BAcnt_CompanyAccount.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Name.ToLower().Contains(textkey.ToLower()));
                    }
                 
                    var entities = companyAccountDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<BAcnt_CompanyAccountJson>();
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
        private HttpListResult<BAcnt_CompanyAccountJson> GetAllCompanyAccount()
        {
            string error = string.Empty;
            var result = new HttpListResult<BAcnt_CompanyAccountJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.CompanyAccount.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.CompanyAccount.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.CompanyAccount.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (BAcnt_CompanyAccountDataService companyAccountDataService = new BAcnt_CompanyAccountDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<BAcnt_CompanyAccountJson>();
                    var entities = companyAccountDataService.GetAll(out error);
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
        public HttpResult<BAcnt_CompanyAccountJson> GetCompanyAccountById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<BAcnt_CompanyAccountJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.CompanyAccount.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.CompanyAccount.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.CompanyAccount.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (BAcnt_CompanyAccountDataService companyAccountDataService = new BAcnt_CompanyAccountDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new BAcnt_CompanyAccountJson();
                    BAcnt_CompanyAccount entity;
                    if (id <= 0)
                    {
                        entity = BAcnt_CompanyAccount.GetNew();
                    }
                    else
                    {
                        entity = companyAccountDataService.GetById(id);
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
        private HttpResult<BAcnt_CompanyAccountJson> GetCompanyAccountByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<BAcnt_CompanyAccountJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.CompanyAccount.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.CompanyAccount.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.CompanyAccount.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (BAcnt_CompanyAccountDataService companyAccountDataService = new BAcnt_CompanyAccountDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new BAcnt_CompanyAccountJson();
                    BAcnt_CompanyAccount entity;
                    if (id <= 0)
                    {
                        entity = BAcnt_CompanyAccount.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("BAcnt_CompanyAccount");
                        //includeTables.Add("BAcnt_CompanyAccount");

                        entity = companyAccountDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<BAcnt_CompanyAccountJson> GetCompanyAccountListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<BAcnt_CompanyAccountJson>();
            try
            {
                //BAcnt_CompanyAccountDataService companyAccountDataService =
                //    new BAcnt_CompanyAccountDataService(DbInstance, HttpUtil.Profile);
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
        public HttpListResult<BAcnt_CompanyAccountJson> GetCompanyAccountDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<BAcnt_CompanyAccountJson>();
            try
            {
                //BAcnt_CompanyAccountDataService companyAccountDataService =
                //    new BAcnt_CompanyAccountDataService(DbInstance, HttpUtil.Profile);
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
        public HttpResult<BAcnt_CompanyAccountJson> SaveCompanyAccount(BAcnt_CompanyAccountJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<BAcnt_CompanyAccountJson>();
            //optional permission, check permission in the SaveCompanyAccountLogic insted
            if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.CompanyAccount.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.CompanyAccount.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                
                

                var entityReceived = new BAcnt_CompanyAccount();
                var dbAttachedEntity = new BAcnt_CompanyAccount();
                json.Map(entityReceived);

                var hasCurrentCompany = DbInstance.BAcnt_CompanyAccount.Where(x => x.Id != entityReceived.Id).Any(x => x.IsCurrent);
                if (hasCurrentCompany && entityReceived.IsCurrent)
                {
                    result.HasError = true;
                    result.Errors.Add("Current company already exit, first change those!");
                    return result;
                }

                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SaveCompanyAccountLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<BAcnt_CompanyAccountJson> SaveCompanyAccount2(BAcnt_CompanyAccountJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<BAcnt_CompanyAccountJson>();
            try
            {
                using (BAcnt_CompanyAccountDataService companyAccountDataService = new BAcnt_CompanyAccountDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new BAcnt_CompanyAccount();
                    var dbAttachedEntity = new BAcnt_CompanyAccount();
                    json.Map(entityReceived);
                    if (companyAccountDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<BAcnt_CompanyAccountJson> SaveCompanyAccountList(IList<BAcnt_CompanyAccountJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<BAcnt_CompanyAccountJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.CompanyAccount.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.CompanyAccount.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (BAcnt_CompanyAccountDataService companyAccountDataService = new BAcnt_CompanyAccountDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<BAcnt_CompanyAccount>();
                    var dbAttachedListEntity = new List<BAcnt_CompanyAccount>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (companyAccountDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<BAcnt_CompanyAccountJson> GetDeleteCompanyAccountById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<BAcnt_CompanyAccountJson>();
            //todo fix the permission
            if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.CompanyAccount.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (BAcnt_CompanyAccountDataService companyAccountDataService = new BAcnt_CompanyAccountDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!companyAccountDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveCompanyAccount(BAcnt_CompanyAccount newObj, out string error)
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

            if (newObj.IsCurrent==null)
            {
                error += "Is Current required.";
                return false;
            }
            if (newObj.IsEnable==null)
            {
                error += "Is Enable required.";
                return false;
            }
            if (newObj.StartDate!=null && !newObj.StartDate.IsValid())
            {
                newObj.StartDate=null;//just put null,if its nullable and not valid.
               //error += "Start Date is not valid.";
               //return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.BAcnt_CompanyAccount.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A CompanyAccount already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveCompanyAccountLogic(BAcnt_CompanyAccount newObj,ref BAcnt_CompanyAccount dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Company Account to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveCompanyAccount(newObj, out error))
                return false;

            bool isNewObject = true;
            BAcnt_CompanyAccount objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.BAcnt_CompanyAccount.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = BAcnt_CompanyAccount.GetNew(newObj.Id);
                DbInstance.BAcnt_CompanyAccount.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            
            if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.CompanyAccount.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.CompanyAccount.CanEdit,
                   out error))
            {
                return false;
            }

            //binding object  
            objToSave.Name =  newObj.Name ;
           objToSave.Description =  newObj.Description ;
           objToSave.IsCurrent =  newObj.IsCurrent ;
           objToSave.IsEnable =  newObj.IsEnable ;
           objToSave.StartDate =  newObj.StartDate ;
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

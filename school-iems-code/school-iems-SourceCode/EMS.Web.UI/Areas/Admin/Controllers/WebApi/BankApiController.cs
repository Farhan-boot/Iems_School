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
using EMS.Framework.Objects;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases;
using EMS.Web.Framework.Bases.WebApi;
//using EMS.Web.Framework.Objects;
//using EMS.Web.Jsons.Mapper;
using EMS.Web.Jsons.Models;


namespace EMS.Web.UI.Areas.Admin.Controllers.WebApi
{

    public class BankApiController : EmployeeBaseApiController
	{
        public BankApiController()
        {  }
       
        #region Bank 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<General_BankJson> GetPagedBank(string textkey, int? pageSize, int? pageNo
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<General_BankJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.OfficialBank.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.OfficialBank.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.OfficialBank.CanEdit, out error)
            )
            {
                result.HasError = true;
                result.Errors.Add(error);
                result.DataExtra.IsPermited = false;
                return result;
            }
            try
            {
                using (General_BankDataService bankDataService = new General_BankDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<General_Bank> query = DbInstance.General_Bank.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Name.ToLower().Contains(textkey.ToLower()));
                    }
                 
                    var entities = bankDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<General_BankJson>();
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
        private HttpListResult<General_BankJson> GetAllBank()
        {
            string error = string.Empty;
            var result = new HttpListResult<General_BankJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.OfficialBank.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.OfficialBank.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.OfficialBank.CanEdit, out error)
            )
            {
                result.HasError = true;
                result.Errors.Add(error);
                result.DataExtra.IsPermited = false;
                return result;
            }
            try
            {
                using (General_BankDataService bankDataService = new General_BankDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<General_BankJson>();
                    var entities = bankDataService.GetAll(out error);
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
        public HttpResult<General_BankJson> GetBankById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<General_BankJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.OfficialBank.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.OfficialBank.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.OfficialBank.CanEdit, out error)
            )
            {
                result.HasError = true;
                result.Errors.Add(error);
                result.DataExtra.IsPermited = false;
                return result;
            }
            try
            {
                using (General_BankDataService bankDataService = new General_BankDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new General_BankJson();
                    General_Bank entity;
                    if (id <= 0)
                    {
                        entity = General_Bank.GetNew();
                    }
                    else
                    {
                        entity = bankDataService.GetById(id);
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
        private HttpResult<General_BankJson> GetBankByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<General_BankJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.OfficialBank.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.OfficialBank.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.OfficialBank.CanEdit, out error)
            )
            {
                result.HasError = true;
                result.Errors.Add(error);
                result.DataExtra.IsPermited = false;
                return result;
            }
            try
            {
                using (General_BankDataService bankDataService = new General_BankDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new General_BankJson();
                    General_Bank entity;
                    if (id <= 0)
                    {
                        entity = General_Bank.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("General_Bank");
                        //includeTables.Add("General_Bank");

                        entity = bankDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<General_BankJson> GetBankListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<General_BankJson>();
            try
            {
                //General_BankDataService bankDataService =
                //    new General_BankDataService(DbInstance, HttpUtil.Profile);
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
        public HttpListResult<General_BankJson> GetBankDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<General_BankJson>();
            try
            {
                //General_BankDataService bankDataService =
                //    new General_BankDataService(DbInstance, HttpUtil.Profile);
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
        private HttpResult<General_BankJson> SaveBank(General_BankJson json)
        {

            string error = string.Empty;
            var result = new HttpResult<General_BankJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.OfficialBank.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.OfficialBank.CanEdit, out error)
            )
            {
                result.HasError = true;
                result.Errors.Add(error);
                result.DataExtra.IsPermited = false;
                return result;
            }
            try
            {
                 var entityReceived = new General_Bank();
                 var dbAttachedEntity = new General_Bank();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {
                    if (SaveBankLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<General_BankJson> SaveBank2(General_BankJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<General_BankJson>();
            try
            {
                using (General_BankDataService bankDataService = new General_BankDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new General_Bank();
                    var dbAttachedEntity = new General_Bank();
                    json.Map(entityReceived);
                    if (bankDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<General_BankJson> SaveBankList(IList<General_BankJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<General_BankJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.OfficialBank.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.OfficialBank.CanEdit, out error)
            )
            {
                result.HasError = true;
                result.Errors.Add(error);
                result.DataExtra.IsPermited = false;
                return result;
            }
            try
            {
                using (General_BankDataService bankDataService = new General_BankDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<General_Bank>();
                    var dbAttachedListEntity = new List<General_Bank>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (bankDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        private HttpResult<General_BankJson> GetDeleteBankById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<General_BankJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.OfficialBank.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                result.DataExtra.IsPermited = false;
                return result;
            }
            try
            {
                using (General_BankDataService bankDataService = new General_BankDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!bankDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveBank(General_Bank newObj, out string error)
        {
            error = "";
            if (!newObj.Name.IsValid())
            {
                error += "Name is not valid.";
                return false;
            }
            if (newObj.Name.Length>256)
            {
                error += "Name exceeded its maximum length 256.";
                return false;
            }

            if (newObj.IsValid==null)
            {
                error += "Is Valid required.";
                return false;
            }
            if (newObj.IsDeleted==null)
            {
                error += "Is Deleted required.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.General_Bank.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A Bank already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveBankLogic(General_Bank newObj,ref General_Bank dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Bank to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveBank(newObj, out error))
                return false;

            bool isNewObject = true;
            General_Bank objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.General_Bank.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = General_Bank.GetNew(newObj.Id);
                DbInstance.General_Bank.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //checking permission, enable it with correction
            if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.OfficialBank.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.OfficialBank.CanEdit,
                   out error))
            {
                return false;
            }

            //binding object  
            objToSave.Name =  newObj.Name ;
           objToSave.Description =  newObj.Description ;
           objToSave.IsValid =  newObj.IsValid ;
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

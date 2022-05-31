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

    public class VoucherTypeApiController : EmployeeBaseApiController
	{
        public VoucherTypeApiController()
        {  }
       
        #region VoucherType 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<BAcnt_VoucherTypeJson> GetPagedVoucherType(string textkey, int? pageSize, int? pageNo
           ,Int32 branchId= 0      
           ,Int32 companyId= 0      
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<BAcnt_VoucherTypeJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.VoucherType.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.VoucherType.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.VoucherType.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/

            try
            {
                using (BAcnt_VoucherTypeDataService voucherTypeDataService = new BAcnt_VoucherTypeDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<BAcnt_VoucherType> query = DbInstance.BAcnt_VoucherType.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Name.ToLower().Contains(textkey.ToLower()));
                    }
                    if (branchId>0)
                    {
                        query = query.Where(q => q.BranchId== branchId);
                    }  
                    if (companyId>0)
                    {
                        query = query.Where(q => q.CompanyId== companyId);
                    }  
                 
                    var entities = voucherTypeDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<BAcnt_VoucherTypeJson>();
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
        private HttpListResult<BAcnt_VoucherTypeJson> GetAllVoucherType()
        {
            string error = string.Empty;
            var result = new HttpListResult<BAcnt_VoucherTypeJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.VoucherType.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.VoucherType.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.VoucherType.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (BAcnt_VoucherTypeDataService voucherTypeDataService = new BAcnt_VoucherTypeDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<BAcnt_VoucherTypeJson>();
                    var entities = voucherTypeDataService.GetAll(out error);
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
        public HttpResult<BAcnt_VoucherTypeJson> GetVoucherTypeById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<BAcnt_VoucherTypeJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.VoucherType.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.VoucherType.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.VoucherType.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (BAcnt_VoucherTypeDataService voucherTypeDataService = new BAcnt_VoucherTypeDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new BAcnt_VoucherTypeJson();
                    BAcnt_VoucherType entity;
                    if (id <= 0)
                    {
                        entity = BAcnt_VoucherType.GetNew();
                    }
                    else
                    {
                        entity = voucherTypeDataService.GetById(id);
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
        private HttpResult<BAcnt_VoucherTypeJson> GetVoucherTypeByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<BAcnt_VoucherTypeJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.VoucherType.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.VoucherType.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.VoucherType.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (BAcnt_VoucherTypeDataService voucherTypeDataService = new BAcnt_VoucherTypeDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new BAcnt_VoucherTypeJson();
                    BAcnt_VoucherType entity;
                    if (id <= 0)
                    {
                        entity = BAcnt_VoucherType.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("BAcnt_VoucherType");
                        //includeTables.Add("BAcnt_VoucherType");

                        entity = voucherTypeDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<BAcnt_VoucherTypeJson> GetVoucherTypeListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<BAcnt_VoucherTypeJson>();
            try
            {
                //BAcnt_VoucherTypeDataService voucherTypeDataService =
                //    new BAcnt_VoucherTypeDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.TypeEnumList = EnumUtil.GetEnumList(typeof(BAcnt_VoucherType.TypeEnum));
                result.DataExtra.StatusEnumList = EnumUtil.GetEnumList(typeof(BAcnt_VoucherType.StatusEnum));
                //DropDown Option Tables
                 
                //result.DataExtra.BranchList = DbInstance.BAcnt_Branch.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
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
        public HttpListResult<BAcnt_VoucherTypeJson> GetVoucherTypeDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<BAcnt_VoucherTypeJson>();
            try
            {
                //BAcnt_VoucherTypeDataService voucherTypeDataService =
                //    new BAcnt_VoucherTypeDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.TypeEnumList = EnumUtil.GetEnumList(typeof(BAcnt_VoucherType.TypeEnum));
                result.DataExtra.StatusEnumList = EnumUtil.GetEnumList(typeof(BAcnt_VoucherType.StatusEnum));
                //DropDown Option Tables

                result.DataExtra.BranchList = DbInstance.BAcnt_Branch.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.Name }).ToList();

                result.DataExtra.CompanyAccountList = DbInstance.BAcnt_CompanyAccount.AsEnumerable().Select(t => new
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
        public HttpResult<BAcnt_VoucherTypeJson> SaveVoucherType(BAcnt_VoucherTypeJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<BAcnt_VoucherTypeJson>();
            //optional permission, check permission in the SaveVoucherTypeLogic insted
            /*if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.VoucherType.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.VoucherType.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                 var entityReceived = new BAcnt_VoucherType();
                 var dbAttachedEntity = new BAcnt_VoucherType();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SaveVoucherTypeLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<BAcnt_VoucherTypeJson> SaveVoucherType2(BAcnt_VoucherTypeJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<BAcnt_VoucherTypeJson>();
            try
            {
                using (BAcnt_VoucherTypeDataService voucherTypeDataService = new BAcnt_VoucherTypeDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new BAcnt_VoucherType();
                    var dbAttachedEntity = new BAcnt_VoucherType();
                    json.Map(entityReceived);
                    if (voucherTypeDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<BAcnt_VoucherTypeJson> SaveVoucherTypeList(IList<BAcnt_VoucherTypeJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<BAcnt_VoucherTypeJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.VoucherType.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.VoucherType.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (BAcnt_VoucherTypeDataService voucherTypeDataService = new BAcnt_VoucherTypeDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<BAcnt_VoucherType>();
                    var dbAttachedListEntity = new List<BAcnt_VoucherType>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (voucherTypeDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<BAcnt_VoucherTypeJson> GetDeleteVoucherTypeById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<BAcnt_VoucherTypeJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.VoucherType.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (BAcnt_VoucherTypeDataService voucherTypeDataService = new BAcnt_VoucherTypeDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!voucherTypeDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveVoucherType(BAcnt_VoucherType newObj, out string error)
        {
            error = "";
            if (!newObj.Name.IsValid())
            {
                error += "Name is not valid.";
                return false;
            }
            if (newObj.Name.Length>50)
            {
                error += "Name exceeded its maximum length 50.";
                return false;
            }
            if (newObj.TypeEnumId==null)
            {
                error += "Please select valid Type.";
                return false;
            }
            if (newObj.StatusEnumId==null)
            {
                error += "Please select valid Status.";
                return false;
            }


            if (newObj.BranchId<=0)
            {
                error += "Please select valid Branch.";
                return false;
            }
            if (newObj.CompanyId<=0)
            {
                error += "Please select valid Company.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.BAcnt_VoucherType.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A VoucherType already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveVoucherTypeLogic(BAcnt_VoucherType newObj,ref BAcnt_VoucherType dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Voucher Type to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveVoucherType(newObj, out error))
                return false;

            bool isNewObject = true;
            BAcnt_VoucherType objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.BAcnt_VoucherType.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = BAcnt_VoucherType.GetNew(newObj.Id);
                DbInstance.BAcnt_VoucherType.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.VoucherType.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.VoucherType.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
           objToSave.Name =  newObj.Name ;
           objToSave.TypeEnumId =  newObj.TypeEnumId ;
           objToSave.StatusEnumId =  newObj.StatusEnumId ;
           objToSave.DefaultDebitLedgerId =  newObj.DefaultDebitLedgerId ;
           objToSave.DefaultCreditLedgerId =  newObj.DefaultCreditLedgerId ;
           objToSave.BranchId =  newObj.BranchId ;
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

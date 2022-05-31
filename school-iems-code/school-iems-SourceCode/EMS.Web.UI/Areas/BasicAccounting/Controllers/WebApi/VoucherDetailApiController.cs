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


namespace EMS.Web.UI.Areas.BasicAccounting.Controllers.WebApi
{

    public class VoucherDetailApiController : EmployeeBaseApiController
	{
        public VoucherDetailApiController()
        {  }
       
        #region VoucherDetail 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<BAcnt_VoucherDetailJson> GetPagedVoucherDetail(string textkey, int? pageSize, int? pageNo
           ,Int32 ledgerId= 0      
           ,Int32 voucherId= 0      
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<BAcnt_VoucherDetailJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.VoucherDetail.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.VoucherDetail.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.VoucherDetail.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/

            try
            {
                using (BAcnt_VoucherDetailDataService voucherDetailDataService = new BAcnt_VoucherDetailDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<BAcnt_VoucherDetail> query = DbInstance.BAcnt_VoucherDetail.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Remark.ToLower().Contains(textkey.ToLower()));
                    }
                    if (ledgerId>0)
                    {
                        query = query.Where(q => q.LedgerId== ledgerId);
                    }  
                    if (voucherId>0)
                    {
                        query = query.Where(q => q.VoucherId== voucherId);
                    }  
                 
                    var entities = voucherDetailDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<BAcnt_VoucherDetailJson>();
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
        private HttpListResult<BAcnt_VoucherDetailJson> GetAllVoucherDetail()
        {
            string error = string.Empty;
            var result = new HttpListResult<BAcnt_VoucherDetailJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.VoucherDetail.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.VoucherDetail.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.VoucherDetail.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (BAcnt_VoucherDetailDataService voucherDetailDataService = new BAcnt_VoucherDetailDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<BAcnt_VoucherDetailJson>();
                    var entities = voucherDetailDataService.GetAll(out error);
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
        public HttpResult<BAcnt_VoucherDetailJson> GetVoucherDetailById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<BAcnt_VoucherDetailJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.VoucherDetail.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.VoucherDetail.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.VoucherDetail.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (BAcnt_VoucherDetailDataService voucherDetailDataService = new BAcnt_VoucherDetailDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new BAcnt_VoucherDetailJson();
                    BAcnt_VoucherDetail entity;
                    if (id <= 0)
                    {
                        entity = BAcnt_VoucherDetail.GetNew();
                    }
                    else
                    {
                        entity = voucherDetailDataService.GetById(id);
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
        private HttpResult<BAcnt_VoucherDetailJson> GetVoucherDetailByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<BAcnt_VoucherDetailJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.VoucherDetail.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.VoucherDetail.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.VoucherDetail.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (BAcnt_VoucherDetailDataService voucherDetailDataService = new BAcnt_VoucherDetailDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new BAcnt_VoucherDetailJson();
                    BAcnt_VoucherDetail entity;
                    if (id <= 0)
                    {
                        entity = BAcnt_VoucherDetail.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("BAcnt_VoucherDetail");
                        //includeTables.Add("BAcnt_VoucherDetail");

                        entity = voucherDetailDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<BAcnt_VoucherDetailJson> GetVoucherDetailListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<BAcnt_VoucherDetailJson>();
            try
            {
                //BAcnt_VoucherDetailDataService voucherDetailDataService =
                //    new BAcnt_VoucherDetailDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                //DropDown Option Tables
                 
                //result.DataExtra.LedgerList = DbInstance.BAcnt_Ledger.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
                //result.DataExtra.VoucherList = DbInstance.BAcnt_Voucher.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<BAcnt_VoucherDetailJson> GetVoucherDetailDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<BAcnt_VoucherDetailJson>();
            try
            {
                //BAcnt_VoucherDetailDataService voucherDetailDataService =
                //    new BAcnt_VoucherDetailDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                //DropDown Option Tables
                 
                //result.DataExtra.LedgerList = DbInstance.BAcnt_Ledger.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
                //result.DataExtra.VoucherList = DbInstance.BAcnt_Voucher.AsEnumerable().Select(t => new
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
        public HttpResult<BAcnt_VoucherDetailJson> SaveVoucherDetail(BAcnt_VoucherDetailJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<BAcnt_VoucherDetailJson>();
            //optional permission, check permission in the SaveVoucherDetailLogic insted
            /*if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.VoucherDetail.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.VoucherDetail.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                 var entityReceived = new BAcnt_VoucherDetail();
                 var dbAttachedEntity = new BAcnt_VoucherDetail();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SaveVoucherDetailLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<BAcnt_VoucherDetailJson> SaveVoucherDetail2(BAcnt_VoucherDetailJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<BAcnt_VoucherDetailJson>();
            try
            {
                using (BAcnt_VoucherDetailDataService voucherDetailDataService = new BAcnt_VoucherDetailDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new BAcnt_VoucherDetail();
                    var dbAttachedEntity = new BAcnt_VoucherDetail();
                    json.Map(entityReceived);
                    if (voucherDetailDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<BAcnt_VoucherDetailJson> SaveVoucherDetailList(IList<BAcnt_VoucherDetailJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<BAcnt_VoucherDetailJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.VoucherDetail.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.VoucherDetail.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (BAcnt_VoucherDetailDataService voucherDetailDataService = new BAcnt_VoucherDetailDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<BAcnt_VoucherDetail>();
                    var dbAttachedListEntity = new List<BAcnt_VoucherDetail>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (voucherDetailDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<BAcnt_VoucherDetailJson> GetDeleteVoucherDetailById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<BAcnt_VoucherDetailJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.VoucherDetail.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (BAcnt_VoucherDetailDataService voucherDetailDataService = new BAcnt_VoucherDetailDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!voucherDetailDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveVoucherDetail(BAcnt_VoucherDetail newObj, out string error)
        {
            error = "";
            if (newObj.LedgerId<=0)
            {
                error += "Please select valid Ledger.";
                return false;
            }
            if (newObj.DebitAccount==null)
            {
                error += "Debit Account required.";
                return false;
            }
            if (newObj.CreditAccount==null)
            {
                error += "Credit Account required.";
                return false;
            }
            if (newObj.IsDebited==null)
            {
                error += "Is Debited required.";
                return false;
            }
            if (newObj.VoucherId<=0)
            {
                error += "Please select valid Voucher.";
                return false;
            }


            if (newObj.IsDeleted==null)
            {
                error += "Is Deleted required.";
                return false;
            }
            if (!newObj.Narration.IsValid())
            {
                error += "Narration is not valid.";
                return false;
            }
            if (newObj.Narration==null)
            {
                error += "Narration required.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.BAcnt_VoucherDetail.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A VoucherDetail already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveVoucherDetailLogic(BAcnt_VoucherDetail newObj,ref BAcnt_VoucherDetail dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Voucher Detail to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveVoucherDetail(newObj, out error))
                return false;

            bool isNewObject = true;
            BAcnt_VoucherDetail objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.BAcnt_VoucherDetail.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = BAcnt_VoucherDetail.GetNew(newObj.Id);
                DbInstance.BAcnt_VoucherDetail.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.VoucherDetail.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.VoucherDetail.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
           objToSave.LedgerId =  newObj.LedgerId ;
           objToSave.DebitAccount =  newObj.DebitAccount ;
           objToSave.CreditAccount =  newObj.CreditAccount ;
           objToSave.IsDebited =  newObj.IsDebited ;
           objToSave.VoucherId =  newObj.VoucherId ;
           objToSave.Remark =  newObj.Remark ;
           objToSave.History =  newObj.History ;
           objToSave.IsDeleted =  newObj.IsDeleted ;
           objToSave.Narration =  newObj.Narration ;
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

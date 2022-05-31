//File: UI Controller
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
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

    public class VoucherApiController : EmployeeBaseApiController
    {
        public VoucherApiController()
        { }

        #region Voucher 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<BAcnt_VoucherJson> GetPagedVoucher(string textkey, int? pageSize, int? pageNo
           , Int32 bankId = 0
           , Int32 branchId = 0
           , Int32 companyId = 0
           , Int32 voucherTypeId = 0
           , string chequeNo=""
           , DateTime? startDate = null
           , DateTime? endDate = null
           , bool isShowTrashedItems = false
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<BAcnt_VoucherJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Voucher.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Voucher.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Voucher.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }

            try
            {
                using (BAcnt_VoucherDataService voucherDataService = new BAcnt_VoucherDataService(DbInstance, HttpUtil.Profile))
                {
                    IQueryable<BAcnt_Voucher> query = DbInstance.BAcnt_Voucher
                        .Include(x=>x.BAcnt_VoucherDetail)
                        .Include(x=>x.BAcnt_VoucherType)
                        //.Include(x=>x.Acnt_OfficialBank)
                        .Include(x=>x.BAcnt_Branch)
                        .OrderByDescending(x => x.Id);

                    // Show Trashed Or UnTrashed Voucher List
                    query = isShowTrashedItems ? query.Where(q => q.IsDeleted) : query.Where(q => !q.IsDeleted);


                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.VoucherNo.ToLower().Contains(textkey.ToLower()));
                    }
                    if (bankId > 0)
                    {
                        query = query.Where(q => q.BankId == bankId);
                    }
                    if (branchId > 0)
                    {
                        query = query.Where(q => q.BranchId == branchId);
                    }
                    if (companyId > 0)
                    {
                        query = query.Where(q => q.CompanyId == companyId);
                    }
                    if (voucherTypeId > 0)
                    {
                        query = query.Where(q => q.VoucherTypeId == voucherTypeId);
                    }
                    if (chequeNo.IsValid())
                    {
                        query = query.Where(q => q.ChequeNo == chequeNo);
                    }

                    if (startDate != null || endDate != null)
                    {
                        DateTime startOnlyDate = Convert.ToDateTime(startDate).ToOnlyDate();
                        DateTime endOnlyDate = Convert.ToDateTime(endDate).ToOnlyDate();

                        if (startOnlyDate > endOnlyDate)
                        {
                            result.HasError = true;
                            result.Errors.Add("Start date should not be greater than end date!");
                            return result;
                        }

                        query = query.Where(x => EntityFunctions.TruncateTime(x.Date) >= EntityFunctions.TruncateTime(startOnlyDate)
                                                 && EntityFunctions.TruncateTime(x.Date) <= EntityFunctions.TruncateTime(endOnlyDate));
                    }


                    var entities = voucherDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);

                    var jsons = new List<BAcnt_VoucherJson>();
                    entities.Map(jsons);

                    result.Data = jsons;
                    result.Count = count;
                    result.DataExtra.TotalAmount = jsons.Sum(x => x.VoucherTotalAmount);
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
        private HttpListResult<BAcnt_VoucherJson> GetAllVoucher()
        {
            string error = string.Empty;
            var result = new HttpListResult<BAcnt_VoucherJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.Voucher.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.Voucher.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.Voucher.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (BAcnt_VoucherDataService voucherDataService = new BAcnt_VoucherDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<BAcnt_VoucherJson>();
                    var entities = voucherDataService.GetAll(out error);
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
        public HttpResult<BAcnt_VoucherJson> GetVoucherById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<BAcnt_VoucherJson>();
            //todo fix the permission
            if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Voucher.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Voucher.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Voucher.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                var json = new BAcnt_VoucherJson();
                BAcnt_Voucher entity;
                if (id <= 0)
                {
                    entity = BAcnt_Voucher.GetNew();
                    entity.Date = DateTime.Now;
                }
                else
                {
                    //entity = voucherDataService.GetById(id, "BAcnt_VoucherDetail");
                    entity = DbInstance.BAcnt_Voucher
                        .Include(x => x.BAcnt_VoucherDetail)
                        .Include(x => x.BAcnt_VoucherType) // for show Voucher type at UI
                        .SingleOrDefault(x => x.Id == id);

                }
                entity.Map(json);
                result.Data = json;

            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }

        private HttpResult<BAcnt_VoucherJson> GetVoucherByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<BAcnt_VoucherJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.Voucher.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.Voucher.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.Voucher.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (BAcnt_VoucherDataService voucherDataService = new BAcnt_VoucherDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new BAcnt_VoucherJson();
                    BAcnt_Voucher entity;
                    if (id <= 0)
                    {
                        entity = BAcnt_Voucher.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("BAcnt_Voucher");
                        //includeTables.Add("BAcnt_Voucher");

                        entity = voucherDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<BAcnt_VoucherJson> GetVoucherListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<BAcnt_VoucherJson>();
            try
            {
                //BAcnt_VoucherDataService voucherDataService =
                //    new BAcnt_VoucherDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List

                result.DataExtra.VoucherStatusEnumList = EnumUtil.GetEnumList(typeof(BAcnt_Voucher.VoucherStatusEnum));
                result.DataExtra.JournalTypeEnumList = EnumUtil.GetEnumList(typeof(BAcnt_Voucher.JournalTypeEnum));
                //DropDown Option Tables

                result.DataExtra.OfficialBankList = DbInstance.Acnt_OfficialBank.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.Name }).ToList();

                var currentCompanyId = Facade.BasicAccountingFacade.CompanyFacade.GetCurrentCompanyId();
                result.DataExtra.BranchList = DbInstance.BAcnt_Branch
                    .Where(x => x.CompanyId == currentCompanyId && x.IsEnable && !x.IsDeleted)
                    .AsEnumerable().Select(t => new
                        { Id = t.Id, Name = t.Name }).ToList();

                //result.DataExtra.CompanyAccountList = DbInstance.BAcnt_CompanyAccount.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();

                result.DataExtra.VoucherTypeList = DbInstance.BAcnt_VoucherType.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.Name }).ToList();

            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<BAcnt_VoucherJson> GetVoucherDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<BAcnt_VoucherJson>();
            try
            {
                //BAcnt_VoucherDataService voucherDataService =
                //    new BAcnt_VoucherDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List

                result.DataExtra.VoucherStatusEnumList = EnumUtil.GetEnumList(typeof(BAcnt_Voucher.VoucherStatusEnum));
                result.DataExtra.JournalTypeEnumList = EnumUtil.GetEnumList(typeof(BAcnt_Voucher.JournalTypeEnum));

                result.DataExtra.TransactionTypeEnumList = EnumUtil.GetEnumList(typeof(BAcnt_VoucherDetail.TransactionTypeEnum));



                var voucherTypeEnumList = EnumUtil.GetEnumList(typeof(BAcnt_VoucherType.TypeEnum));
                result.DataExtra.VoucherTypeEnumList = voucherTypeEnumList.OrderBy(x => x.Id).ToList();




                //DropDown Option Tables

                result.DataExtra.OfficialBankList = DbInstance.Acnt_OfficialBank.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.Name }).ToList();

                var currentCompanyId = Facade.BasicAccountingFacade.CompanyFacade.GetCurrentCompanyId();
                result.DataExtra.BranchList = DbInstance.BAcnt_Branch
                    .Where(x=>x.CompanyId== currentCompanyId && x.IsEnable && !x.IsDeleted)
                    .AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.Name }).ToList();

                /*result.DataExtra.CompanyAccountList = DbInstance.BAcnt_CompanyAccount
                    .Where(x=>x.IsCurrent)
                    .AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.Name }).ToList();*/

                result.DataExtra.VoucherTypeList = DbInstance.BAcnt_VoucherType.AsEnumerable().Select(t => new
                {
                    Id = t.Id,
                    Name = t.Name,
                    TypeEnumId = t.TypeEnumId
                }).ToList();

                //Current Company All Ledger List
                var allLedgerList = DbInstance.BAcnt_Ledger
                    .Where(x => x.CompanyId == currentCompanyId)
                    .ToList();

                var onlyLedgerList= allLedgerList.Where(x => x.IsGroup == false).ToList();

                foreach (var onlyLedger in onlyLedgerList)
                {
                    var parentLedger = allLedgerList.SingleOrDefault(x => x.Id == onlyLedger.ParentId);
                    // Set Parent Code
                    onlyLedger.Code = parentLedger?.Code??"";
                }

                result.DataExtra.LedgerList = onlyLedgerList.AsEnumerable().Select(t => new
                {
                    Id = t.Id,
                    Name = t.Name,
                    Code = t.Code,
                    CodeName=t.CodeName,
                    Type=t.Type.ToString()

                }).ToList();

                var voucherDtlEntity = BAcnt_VoucherDetail.GetNew();

                // Empty Voucher Details Create for Debit 
                var emptyDrVoucherDtl = new BAcnt_VoucherDetailJson();
                voucherDtlEntity.Map(emptyDrVoucherDtl);
                emptyDrVoucherDtl.IsDebited = true;
                emptyDrVoucherDtl.TransactionTypeEnumId = (byte) BAcnt_VoucherDetail.TransactionTypeEnum.Dr;


                // Empty Voucher Details Create for Credit 
                var emptyCrVoucherDtl = new BAcnt_VoucherDetailJson();
                voucherDtlEntity.Map(emptyCrVoucherDtl);
                emptyCrVoucherDtl.IsDebited = false;
                emptyCrVoucherDtl.TransactionTypeEnumId = (byte) BAcnt_VoucherDetail.TransactionTypeEnum.Cr;



                // Empty Voucher Create
                var emptyVoucher = new BAcnt_VoucherJson
                {
                    Date = DateTime.Now,
                    BAcnt_VoucherDetailListJson = new List<BAcnt_VoucherDetailJson>
                    {
                        emptyDrVoucherDtl, emptyCrVoucherDtl
                    }
                };


                result.DataExtra.EmptyVoucher = emptyVoucher;
                result.DataExtra.EmptyDrVoucherDtl = emptyDrVoucherDtl;
                result.DataExtra.EmptyCrVoucherDtl = emptyCrVoucherDtl;



            }



            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }

        public HttpResult<BAcnt_VoucherJson> GetVoucherHistoryById(int id=0)
        {
            var result = new HttpResult<BAcnt_VoucherJson>();
            string error=String.Empty;
            try
            {
                if (id<=0)
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid voucher!");
                    return result;
                }

                var voucherEntity = DbInstance.BAcnt_Voucher.SingleOrDefault(x => x.Id == id);
                if (voucherEntity.IsNull())
                {
                    result.HasError = true;
                    result.Errors.Add("Voucher not found!");
                    return result;
                }

                var voucherJson=new BAcnt_VoucherJson();
                voucherEntity.Map(voucherJson);

                var userList = DbInstance.User_Account.Where(x => x.Id == voucherEntity.CreateById || x.Id == voucherEntity.LastChangedById).ToList();

                foreach (var user in userList)
                {
                    if (voucherJson.CreateById==user.Id)
                    {
                        voucherJson.CreateByName = $"{user.FullName} [{user.UserName}]";
                    }

                    if (voucherJson.LastChangedById==user.Id)
                    {
                        voucherJson.LastChangedByName= $"{user.FullName} [{user.UserName}]";
                    }
                }

                result.Data = voucherJson;

            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.Message);
            }

            return result;

        }

        #endregion

        #region Save/Delete
        [HttpPost]
        public HttpResult<BAcnt_VoucherJson> SaveVoucher(BAcnt_VoucherJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<BAcnt_VoucherJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Voucher.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Voucher.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                var entityVoucherReceived = new BAcnt_Voucher();
                var dbAttachedVoucherEntity = new BAcnt_Voucher();
                json.Map(entityVoucherReceived);

                var branch = DbInstance.BAcnt_Branch.SingleOrDefault(x => x.Id == entityVoucherReceived.BranchId);
                if (branch==null)
                {
                    result.HasError = true;
                    result.Errors.Add("Please select valid Branch.");
                    return result;
                }
                // set Company Id
                entityVoucherReceived.CompanyId = branch.CompanyId;

                //todo solve next time
                #region Temporary value set

                entityVoucherReceived.VoucherStatusEnumId = (byte) BAcnt_Voucher.VoucherStatusEnum.NotPosted;
                entityVoucherReceived.JournalTypeEnumId = (byte) BAcnt_Voucher.JournalTypeEnum.Voucher;

                #endregion

                bool isAnyDrTransHasZero = entityVoucherReceived.BAcnt_VoucherDetail
                    .Any(x => x.IsDebited && x.DebitAmount == 0);

                bool isAnyCrTransHasZero = entityVoucherReceived.BAcnt_VoucherDetail
                    .Any(x => !x.IsDebited && x.CreditAmount == 0);

                if (isAnyDrTransHasZero || isAnyCrTransHasZero)
                {
                    result.HasError = true;
                    result.Errors.Add("Any transaction(s) are can not be zero(0).");
                    return result;
                }



                var totalDebitAmount = entityVoucherReceived.BAcnt_VoucherDetail.Where(x => x.IsDebited && !x.IsDeleted)
                    .Sum(x => x.DebitAmount);

                var totalCreditAmount = entityVoucherReceived.BAcnt_VoucherDetail.Where(x => !x.IsDebited && !x.IsDeleted)
                    .Sum(x => x.CreditAmount);

                if (totalDebitAmount == 0 && totalCreditAmount == 0)
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid amount.");
                    return result;
                }

                if (totalDebitAmount != totalCreditAmount)
                {
                    result.HasError = true;
                    result.Errors.Add("Total debit amount & total credit amount must be same.");
                    return result;
                }


                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    // Save Changes Voucher
                    if (!SaveVoucherLogic(entityVoucherReceived, ref dbAttachedVoucherEntity, out error))
                    {
                        result.HasError = true;
                        result.Errors.Add(error);
                        scope.Rollback();
                        return result;
                    }

                    //Save Changes for get Voucher Id
                    DbInstance.SaveChanges();

                    var dbAttachedVoucherDtlEntity = new BAcnt_VoucherDetail();

                    foreach (var entityVoucherDtlReceived in entityVoucherReceived.BAcnt_VoucherDetail.ToList())
                    {
                        entityVoucherDtlReceived.VoucherId = dbAttachedVoucherEntity.Id;

                        if (!SaveVoucherDetailLogic(entityVoucherDtlReceived, ref dbAttachedVoucherDtlEntity, out error))
                        {
                            result.HasError = true;
                            result.Errors.Add(error);
                            scope.Rollback();
                            return result;

                        }

                    }

                    DbInstance.SaveChanges();
                    scope.Commit();

                    dbAttachedVoucherEntity.Map(json);
                    result.Data = json; //return object
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
        private HttpResult<BAcnt_VoucherJson> SaveVoucher2(BAcnt_VoucherJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<BAcnt_VoucherJson>();
            try
            {
                using (BAcnt_VoucherDataService voucherDataService = new BAcnt_VoucherDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new BAcnt_Voucher();
                    var dbAttachedEntity = new BAcnt_Voucher();
                    json.Map(entityReceived);
                    if (voucherDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<BAcnt_VoucherJson> SaveVoucherList(IList<BAcnt_VoucherJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<BAcnt_VoucherJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.Voucher.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.Voucher.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (BAcnt_VoucherDataService voucherDataService = new BAcnt_VoucherDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<BAcnt_Voucher>();
                    var dbAttachedListEntity = new List<BAcnt_Voucher>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (voucherDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<BAcnt_VoucherJson> GetDeleteVoucherById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<BAcnt_VoucherJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Voucher.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                var voucher = DbInstance.BAcnt_Voucher.SingleOrDefault(x => x.Id == id);
                if (voucher==null)
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid voucher!");
                    return result;
                }
                var voucherDtl= DbInstance.BAcnt_VoucherDetail.Where(x => x.VoucherId == voucher.Id).ToList();

                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    // Delete Voucher Details
                    if (voucherDtl.Count>0)
                    {
                        DbInstance.BAcnt_VoucherDetail.RemoveRange(voucherDtl);
                    }

                    // Delete Voucher
                    DbInstance.BAcnt_Voucher.Remove(voucher);

                    DbInstance.SaveChanges();
                    scope.Commit();

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

        #region Voucher
        private bool IsValidToSaveVoucher(BAcnt_Voucher newObj, out string error)
        {
            error = "";
            if (!newObj.Date.IsValid())
            {
                error += "Date is not valid.";
                return false;
            }
            if (!newObj.VoucherNo.IsValid())
            {
                error += "Voucher No is not valid.";
                return false;
            }
            if (newObj.VoucherNo.Length > 50)
            {
                error += "Voucher No exceeded its maximum length 50.";
                return false;
            }
            if (newObj.VoucherTypeId <= 0)
            {
                error += "Please select valid Voucher Type.";
                return false;
            }
            if (newObj.VoucherStatusEnumId == null)
            {
                error += "Please select valid Voucher Status.";
                return false;
            }
            /*if (!newObj.Remark.IsValid())
            {
                error += "Remark is not valid.";
                return false;
            }*/
            if (newObj.Remark == null)
            {
                error += "Remark required.";
                return false;
            }
            /*if (!newObj.History.IsValid())
            {
                error += "History is not valid.";
                return false;
            }*/
            if (newObj.History == null)
            {
                error += "History required.";
                return false;
            }
            if (newObj.IsDeleted == null)
            {
                error += "Is Deleted required.";
                return false;
            }
            /*if (!newObj.Narration.IsValid())
            {
                error += "Narration is not valid.";
                return false;
            }*/
            /*if (newObj.Narration == null)
            {
                error += "Narration required.";
                return false;
            }*/
            if (newObj.JournalTypeEnumId == null)
            {
                error += "Please select valid Journal Type.";
                return false;
            }
            if (newObj.BankId <= 0)
            {
                error += "Please select valid Bank.";
                return false;
            }
            /*if (!newObj.ManualSlipId.IsValid())
            {
                error += "Manual Slip Id is not valid.";
                return false;
            }*/
            if (newObj.ManualSlipId.Length > 50)
            {
                error += "Manual Slip Id exceeded its maximum length 50.";
                return false;
            }
            /*if (!newObj.ChequeNo.IsValid())
            {
                error += "Cheque No is not valid.";
                return false;
            }*/
            if (newObj.ChequeNo.Length > 255)
            {
                error += "Cheque No exceeded its maximum length 255.";
                return false;
            }
            /*if (!newObj.ChequeDate.IsValid())
            {
                error += "Cheque Date is not valid.";
                return false;
            }*/
            if (newObj.ChequeDate.Length > 255)
            {
                error += "Cheque Date exceeded its maximum length 255.";
                return false;
            }
            if (newObj.BranchId <= 0)
            {
                error += "Please select valid Branch.";
                return false;
            }
            if (newObj.CompanyId <= 0)
            {
                error += "Please select valid Company.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.BAcnt_Voucher.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
            //error = "A Voucher already exists!";
            //return false;
            //}
            return true;
        }
        private bool SaveVoucherLogic(BAcnt_Voucher newObj, ref BAcnt_Voucher dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Voucher to save can't be null!";
                return false;
            }

            if (!IsValidToSaveVoucher(newObj, out error))
                return false;

            bool isNewObject = true;
            BAcnt_Voucher objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.BAcnt_Voucher.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {

                string voucherNo = newObj.VoucherNo.Trim();
                 var isExistVoucherNo = DbInstance.BAcnt_Voucher.Any(x => x.VoucherNo == voucherNo);
                 if (isExistVoucherNo)
                 {
                     error = $"\"{voucherNo}\" This Voucher number already exist.";
                     return false;
                 }
                //new object
                isNewObject = true;
                objToSave = BAcnt_Voucher.GetNew(newObj.Id);
                DbInstance.BAcnt_Voucher.Add(objToSave);
                objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
                objToSave.CreateDate = newObj.CreateDate = DateTime.Now;


            }
            //todo mandatory fix checking permission, enable it with correction
            if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Voucher.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Voucher.CanEdit,
                   out error))
            {
                return false;
            }

            //binding object  
            objToSave.Date = newObj.Date;
            objToSave.VoucherNo = newObj.VoucherNo;
            objToSave.VoucherTypeId = newObj.VoucherTypeId;
            objToSave.VoucherStatusEnumId = newObj.VoucherStatusEnumId;
            objToSave.Remark = newObj.Remark;
            //objToSave.History = newObj.History; //Todo Set next time
            objToSave.IsDeleted = newObj.IsDeleted;
            objToSave.Narration = newObj.Narration;
            objToSave.JournalTypeEnumId = newObj.JournalTypeEnumId;
            objToSave.BankId = newObj.BankId;
            objToSave.ManualSlipId = newObj.ManualSlipId;
            objToSave.ChequeNo = newObj.ChequeNo;
            objToSave.ChequeDate = newObj.ChequeDate;
            objToSave.BranchId = newObj.BranchId;
            objToSave.CompanyId = newObj.CompanyId;
            objToSave.LastChangedById = newObj.LastChangedById = HttpUtil.Profile.UserId;
            objToSave.LastChanged = newObj.LastChanged = DateTime.Now;
            dbAddedObj = objToSave;

            //here save any child table
            return true;
        }
        #endregion

        #region VoucherDetail
        private bool IsValidToSaveVoucherDetail(BAcnt_VoucherDetail newObj, out string error)
        {
            error = "";
            if (newObj.LedgerId <= 0)
            {
                error += "Please select valid Ledger.";
                return false;
            }
            if (newObj.DebitAmount == null)
            {
                error += "Debit Amount required.";
                return false;
            }
            if (newObj.CreditAmount == null)
            {
                error += "Credit Amount required.";
                return false;
            }
            if (newObj.IsDebited == null)
            {
                error += "Is Debited required.";
                return false;
            }
            if (newObj.VoucherId <= 0)
            {
                error += "Please select valid Voucher.";
                return false;
            }


            if (newObj.IsDeleted == null)
            {
                error += "Is Deleted required.";
                return false;
            }
            /*if (!newObj.Narration.IsValid())
            {
                error += "Narration is not valid.";
                return false;
            }*/
            /*if (newObj.Narration == null)
            {
                error += "Narration required.";
                return false;
            }*/
            //TODO write your custom validation here.
            //var res =  DbInstance.BAcnt_VoucherDetail.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
            //error = "A VoucherDetail already exists!";
            //return false;
            //}
            return true;
        }
        private bool SaveVoucherDetailLogic(BAcnt_VoucherDetail newObj, ref BAcnt_VoucherDetail dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Voucher Detail to save can't be null!";
                return false;
            }

            if (!IsValidToSaveVoucherDetail(newObj, out error))
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
            objToSave.LedgerId = newObj.LedgerId;
            objToSave.DebitAmount = newObj.DebitAmount;
            objToSave.CreditAmount = newObj.CreditAmount;
            objToSave.IsDebited = newObj.IsDebited;
            objToSave.VoucherId = newObj.VoucherId;
            objToSave.Remark = newObj.Remark;
            objToSave.History = newObj.History;
            objToSave.IsDeleted = newObj.IsDeleted;
            objToSave.Narration = newObj.Narration;
            objToSave.LastChangedById = newObj.LastChangedById = HttpUtil.Profile.UserId;
            objToSave.LastChanged = newObj.LastChanged = DateTime.Now;
            dbAddedObj = objToSave;

            //here save any child table
            return true;
        }
        #endregion

        #endregion
        #endregion

    }
}

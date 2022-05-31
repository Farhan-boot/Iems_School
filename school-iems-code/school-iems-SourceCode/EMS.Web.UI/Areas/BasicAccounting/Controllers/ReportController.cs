//File: UI Controller
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.CoreLibrary.Helpers;
using EMS.Web.Framework.Bases.Mvc;
using EMS.Framework;
using EMS.Framework.Attributes;
using EMS.DataAccess.Data;
using EMS.Facade.BasicAccountingArea;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Jsons.Custom.BasicAccounting;
using Microsoft.Ajax.Utilities;
using EMS.Web.UI.Areas.BasicAccounting.Controllers.WebApi;

namespace EMS.Web.UI.Areas.BasicAccounting.Controllers
{
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
    ///[EmsAuthorize(User_Account.UserAuthorizeTypeEnum.Employee)]
    public class ReportController : EmployeeBaseController
    {
        //private ViewResult _dayBookManager;

        #region Voucher Print
        public ActionResult VoucherPrint(int id = 0)
        {
            /*if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.Voucher.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }*/
            var result = new MvcModelResult<BAcnt_Voucher>();
            string error = string.Empty;
            try
            {
                var voucher = DbInstance.BAcnt_Voucher
                    .Where(x => !x.IsDeleted)
                    .Include(x => x.BAcnt_VoucherType)
                    .Include(x => x.BAcnt_VoucherDetail)
                    .SingleOrDefault(x => x.Id == id);

                if (voucher == null)
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid Voucher!");
                    return View(result);
                }

                var ledgerIds = voucher.BAcnt_VoucherDetail
                    .DistinctBy(x => x.LedgerId)
                    .Select(x => x.LedgerId)
                    .ToList();

                var voucherList = DbInstance.BAcnt_Ledger
                    .Join(ledgerIds, all => all.Id, req => req, (all, req) => all)
                    .ToList();
                //BAcnt_Ledger load for show ledger name in UI
                result.DataBag.VoucherList = voucherList;

                var totalDebitAmount = voucher.BAcnt_VoucherDetail.Where(x => x.IsDebited && !x.IsDeleted)
                    .Sum(x => x.DebitAmount);

                var totalCreditAmount = voucher.BAcnt_VoucherDetail.Where(x => !x.IsDebited && !x.IsDeleted)
                    .Sum(x => x.CreditAmount);

                result.DataBag.TotalDebitAmount = totalDebitAmount;
                result.DataBag.TotalCreditAmount = totalCreditAmount;

                // Amount In Words
                string amountInWords = String.Empty;
                amountInWords = StringHelper.ToWordsInBDT(totalDebitAmount);
                amountInWords = amountInWords.ToTitleCase();
                result.DataBag.AmountInWords = amountInWords;

                // Get Voucher Title Name
                result.DataBag.VoucherTitleName = GetVoucherTitleName(voucher.BAcnt_VoucherType);

                result.Data = voucher;

            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.Message);
            }

            return View(result);

        }
        private string GetVoucherTitleName(BAcnt_VoucherType voucherType)
        {
            if (voucherType == null)
            {
                return "";
            }

            if (voucherType.TypeEnumId == (byte)BAcnt_VoucherType.TypeEnum.Payment)
            {
                return "Debit Voucher";
            }
            if (voucherType.TypeEnumId == (byte)BAcnt_VoucherType.TypeEnum.Receipt)
            {
                return "Credit Voucher";
            }
            if (voucherType.TypeEnumId == (byte)BAcnt_VoucherType.TypeEnum.Journal)
            {
                return "Journal Voucher";
            }
            if (voucherType.TypeEnumId == (byte)BAcnt_VoucherType.TypeEnum.Contra)
            {
                return "Contra Voucher";
            }
            else
            {
                return "";
            }


        }

        #endregion

        #region Chart Of Accounts Print
        public ActionResult ChartOfAccounts()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Reports.CanViewChartOfAccounts))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        #endregion

        #region Ledger Account Report
        public ActionResult LedgerAccountReport(bool narration = false, bool dtl = false, bool code = false, int ledgerId = 0, int branchId = 0, string startDate = "", string endDate = "")
        {

            if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.LedgerTransactions.CanViewLedgerTransactions))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }

            string error;
            var result = new MvcModelResult<LedgerTransactionJson>();

            try
            {

                if (ledgerId <= 0)
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid Ledger Id!");
                    return View(result);
                }

                if (!startDate.IsValid())
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid Start Date");
                    return View(result);
                }
                if (!endDate.IsValid())
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid End Date");
                    return View(result);
                }

                DateTime startOnlyDate = Convert.ToDateTime(startDate).ToOnlyDate();
                DateTime endOnlyDate = Convert.ToDateTime(endDate).ToOnlyDate();

                if (startOnlyDate > endOnlyDate)
                {
                    result.HasError = true;
                    result.Errors.Add("Start date should not be greater than end date!");
                    return View(result);
                }

                //Get Current Company Id
                CompanyFacade companyFacade = new CompanyFacade(DbInstance, HttpUtil.Profile);
                var currentCompanyId = companyFacade.GetCurrentCompanyId();

                if (currentCompanyId == null)
                {
                    result.HasError = true;
                    result.Errors.Add("Valid company not found!");
                    return View(result);

                }
                // Valid Branch Checking
                if (branchId > 0)
                {
                    var branch = DbInstance.BAcnt_Branch
                        .SingleOrDefault(x => x.Id == branchId
                                              && x.CompanyId == currentCompanyId
                                              && x.IsEnable && !x.IsDeleted);
                    if (branch == null)
                    {
                        result.HasError = true;
                        result.Errors.Add("Invalid branch!");
                        return View(result);
                    }
                }

                //Get Selected Ledger
                var ledger = DbInstance.BAcnt_Ledger.SingleOrDefault(x => x.Id == ledgerId
                                                                 && x.CompanyId == currentCompanyId
                                                                 );




                var reportResult = Facade.BasicAccountingFacade.LedgerTransactionFacade
                    .GetTransactionListByLedgerId(ledger, null, startOnlyDate, endOnlyDate, out error);

                if (reportResult == null)
                {
                    result.HasError = true;
                    result.Errors.Add(error);
                    return View(result);
                }
                result.Data = reportResult;

                result.DataBag.StartDate = startOnlyDate;
                result.DataBag.EndDate = endOnlyDate;

            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().ToString());
            }

            return View(result);
        }

        public ActionResult LedgerAccountReport2(bool narration = false, bool dtl = false, bool code = false, int ledgerId = 0, int branchId = 0, string startDate = "", string endDate = "")
        {

            if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.LedgerTransactions.CanViewLedgerTransactions))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }

            string error;
            var result = new MvcModelResult<LedgerTransactionJson>();

            try
            {

                if (ledgerId <= 0)
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid Ledger Id!");
                    return View(result);
                }

                if (!startDate.IsValid())
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid Start Date");
                    return View(result);
                }
                if (!endDate.IsValid())
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid End Date");
                    return View(result);
                }

                DateTime startOnlyDate = Convert.ToDateTime(startDate).ToOnlyDate();
                DateTime endOnlyDate = Convert.ToDateTime(endDate).ToOnlyDate();

                if (startOnlyDate > endOnlyDate)
                {
                    result.HasError = true;
                    result.Errors.Add("Start date should not be greater than end date!");
                    return View(result);
                }

                //Get Current Company Id
                CompanyFacade companyFacade = new CompanyFacade(DbInstance, HttpUtil.Profile);
                var currentCompanyId = companyFacade.GetCurrentCompanyId();

                if (currentCompanyId == null)
                {
                    result.HasError = true;
                    result.Errors.Add("Valid company not found!");
                    return View(result);

                }
                // Valid Branch Checking
                if (branchId > 0)
                {
                    var branch = DbInstance.BAcnt_Branch
                        .SingleOrDefault(x => x.Id == branchId
                                              && x.CompanyId == currentCompanyId
                                              && x.IsEnable && !x.IsDeleted);
                    if (branch == null)
                    {
                        result.HasError = true;
                        result.Errors.Add("Invalid branch!");
                        return View(result);
                    }
                }

                //Get Selected Ledger
                var ledger = DbInstance.BAcnt_Ledger.SingleOrDefault(x => x.Id == ledgerId
                                                                 && x.CompanyId == currentCompanyId
                                                                 );




                var reportResult = Facade.BasicAccountingFacade.LedgerTransactionFacade
                    .GetTransactionListByLedgerId(ledger, null, startOnlyDate, endOnlyDate, out error);



                if (reportResult == null)
                {
                    result.HasError = true;
                    result.Errors.Add(error);
                    return View(result);
                }
                result.Data = reportResult;


                result.DataBag.StartDate = startOnlyDate;
                result.DataBag.EndDate = endOnlyDate;

            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().ToString());
            }

            return View(result);
        }
        #endregion

        #region Day Book
        public class DayBookModel
        {
            public int LedgerId { get; set; }
            public string LedgerName { get; set; }
            public string LedgerCodeName { get; set; }

            public double TotalDebitAmount { get; set; }
            public double TotalCreditAmount { get; set; }

            public List<VoucherModel> VoucherModelList { get; set; }

            public static DayBookModel GetNew()
            {
                var obj = new DayBookModel();
                obj.VoucherModelList = new List<VoucherModel>();
                return obj;
            }
        }
        public class VoucherModel
        {
            public int Id { get; set; }
            public DateTime Date { get; set; }
            /// <summary>
            /// Ledger Name
            /// </summary>
            public string Particular { get; set; }
            /// <summary>
            /// Ledger Code Name
            /// </summary>
            public string CodeName { get; set; }
            public string VoucherType { get; set; }
            public string VoucherNo { get; set; }
            public double VoucherTotalAmount { get; set; }
            public double DebitVoucherAmount { get; set; }
            public double CreditVoucherAmount { get; set; }
            public string BranchName { get; set; }
            public string Narration { get; set; }
            public List<VoucherDetailsModel> VoucherDetailsModelList { get; set; }
            public static VoucherModel GetNew()
            {
                var obj = new VoucherModel();
                obj.VoucherDetailsModelList = new List<VoucherDetailsModel>();
                return obj;

            }


        }
        public class VoucherDetailsModel
        {
            public int Id { get; set; }
            /// <summary>
            /// Ledger Code
            /// </summary>
            public string Particular { get; set; }
            /// <summary>
            /// Ledger Code Name
            /// </summary>
            public string CodeName { get; set; }
            public double DebitAmount { get; set; }
            public double CreditAmount { get; set; }
        }

        public ActionResult DayBookManager()
        {
            return View();
        }

        public ActionResult DayBookPrint(bool narration = false, bool dtl = false, bool code = false, int branchId = 0, string startDate = "", string endDate = "")
        {

            if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Reports.CanViewDayBook))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }

            if (branchId == 0 && startDate == "" && endDate == "")
            {
                return View();
            }


            string error;
            var result = new MvcModelResult<DayBookModel>();

            try
            {
                if (!startDate.IsValid())
                {
                    error = "Invalid Start Date";
                    return null;
                }
                if (!endDate.IsValid())
                {
                    error = "Invalid End Date";
                    return null;
                }


                DateTime startOnlyDate = Convert.ToDateTime(startDate).ToOnlyDate();
                DateTime endOnlyDate = Convert.ToDateTime(endDate).ToOnlyDate();

                if (startOnlyDate > endOnlyDate)
                {
                    error = "Start date should not be greater than end date!";
                    return null;
                }


                //Get Current Company Id
                CompanyFacade companyFacade = new CompanyFacade(DbInstance, HttpUtil.Profile);
                var currentCompanyId = companyFacade.GetCurrentCompanyId();

                if (currentCompanyId == null)
                {
                    error = "Valid company not found!";
                    return null;

                }
                // Valid Branch Checking
                if (branchId > 0)
                {
                    var branch = DbInstance.BAcnt_Branch
                        .SingleOrDefault(x => x.Id == branchId
                                              && x.CompanyId == currentCompanyId
                                              && x.IsEnable && !x.IsDeleted);
                    if (branch == null)
                    {
                        error = "Invalid branch!";
                        return null;
                    }
                }

                IQueryable<BAcnt_Voucher> query = DbInstance.BAcnt_Voucher
                    .Include(x => x.BAcnt_VoucherDetail)
                    .Include(x => x.BAcnt_VoucherType)
                    .Include(x => x.BAcnt_Branch)
                    .Where(x => !x.IsDeleted);

                if (branchId > 0)
                {
                    query = query.Where(q => q.BranchId == branchId);
                }


                query = query.Where(x => EntityFunctions.TruncateTime(x.Date) >= EntityFunctions.TruncateTime(startOnlyDate)
                                         && EntityFunctions.TruncateTime(x.Date) <= EntityFunctions.TruncateTime(endOnlyDate));

                // Get voucher List
                var voucherList = query.ToList();

                // Get Ledger List
                var ledgerList = DbInstance.BAcnt_Ledger.Where(x => !x.IsGroup).ToList();


                var dayBook = DayBookModel.GetNew();

                foreach (var voucher in voucherList)
                {
                    var voucherModel = VoucherModel.GetNew();
                    voucherModel.Id = voucher.Id;

                    voucherModel.Date = voucher.Date;
                    voucherModel.VoucherNo = voucher.VoucherNo;
                    voucherModel.VoucherType = voucher.BAcnt_VoucherType.Name;
                    voucherModel.BranchName = voucher.BAcnt_Branch.Name;
                    voucherModel.Narration = voucher.Narration;


                    var thisVoucherDtlList = voucher.BAcnt_VoucherDetail.Where(x => !x.IsDeleted).ToList();
                    foreach (var voucherDtl in thisVoucherDtlList)
                    {
                        var voucherDtlModel = new VoucherDetailsModel();
                        voucherDtlModel.Id = voucherDtl.Id;
                        voucherDtlModel.Particular = voucherDtl.BAcnt_Ledger.Name;
                        voucherDtlModel.CodeName = voucherDtl.BAcnt_Ledger.CodeName;
                        voucherDtlModel.DebitAmount = voucherDtl.DebitAmount;
                        voucherDtlModel.CreditAmount = voucherDtl.CreditAmount;
                        voucherModel.VoucherDetailsModelList.Add(voucherDtlModel);

                    }// end voucherDtl loop

                    if (thisVoucherDtlList.Count > 0)
                    {
                        voucherModel.Particular = thisVoucherDtlList[0].BAcnt_Ledger.Name;
                        // Total Debit Or Total Credit both are same at a voucher
                        voucherModel.VoucherTotalAmount = voucherModel.VoucherDetailsModelList.Sum(x => x.DebitAmount);
                    }

                    //Todo This logic temporary, 
                    if (voucher.BAcnt_VoucherType.TypeEnumId == (byte)BAcnt_VoucherType.TypeEnum.Payment)
                    {
                        dayBook.TotalDebitAmount += voucherModel.VoucherTotalAmount;
                        voucherModel.DebitVoucherAmount = voucherModel.VoucherTotalAmount;
                    }
                    else if (voucher.BAcnt_VoucherType.TypeEnumId == (byte)BAcnt_VoucherType.TypeEnum.Receipt)
                    {
                        dayBook.TotalDebitAmount += voucherModel.VoucherTotalAmount;
                        voucherModel.CreditVoucherAmount = voucherModel.VoucherTotalAmount;
                    }

                    dayBook.VoucherModelList.Add(voucherModel);
                }// end voucher loop

                dayBook.VoucherModelList = dayBook.VoucherModelList.OrderBy(x => x.Date).ToList();

                result.Data = dayBook;

                result.DataBag.StartDate = startOnlyDate;
                result.DataBag.EndDate = endOnlyDate;


            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().ToString());
            }

            return View(result);
        }

        #endregion

        #region Balance Sheet
        public ActionResult BalanceSheet()
        {
            if (!HttpUtil.IsSupperAdmin())
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        #endregion

        #region Receipt Payment
        public class ReceiptPaymentModel
        {
            public List<BankBalanceModel> BankList { get; set; }
            public double TotalOpeningBalance { get; set; }
            public double TotalClosingBalance { get; set; }
            public List<LedgerTransactionModel> LedgerTransactionList { get; set; }
            public double TotalReceiptAmount { get; set; }
            public double TotalPaymentAmount { get; set; }
            public double NetTotalReceiptsAmount { get; set; }
            public double NetTotalPaymentAmount { get; set; }

            public static ReceiptPaymentModel GetNew()
            {
                var obj = new ReceiptPaymentModel
                {
                    BankList = new List<BankBalanceModel>(),
                    LedgerTransactionList = new List<LedgerTransactionModel>()
                };
                return obj;

            }

        }

        public class LedgerTransactionModel
        {
            public int LedgerId { get; set; }
            public string LedgerCode { get; set; }
            public string LedgerName { get; set; }
            public double ReceiptAmount { get; set; }
            public double PaymentsAmount { get; set; }
        }

        public class BankBalanceModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public double PeriodOpeningBalance { get; set; }
            public double PeriodClosingBalance { get; set; }
        }

        public ActionResult ReceiptPaymentManager()
        {
            return View();
        }
        public ActionResult ReceiptPaymentPrint(int branchId = 0, string startDate = "", string endDate = "")
        {
            string error = String.Empty;
            var result = new MvcModelResult<ReceiptPaymentModel>();

            try
            {
                if (branchId == 0 && startDate == "" && endDate == "")
                {
                    return View();
                }

                if (!startDate.IsValid())
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid Start Date");
                    return View(result);
                }
                if (!endDate.IsValid())
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid End Date");
                    return View(result);
                }


                DateTime startOnlyDate = Convert.ToDateTime(startDate).ToOnlyDate();
                DateTime endOnlyDate = Convert.ToDateTime(endDate).ToOnlyDate();

                if (startOnlyDate > endOnlyDate)
                {
                    result.HasError = true;
                    result.Errors.Add("Start date should not be greater than end date!");
                    return View(result);
                }


                //Get Current Company Id
                CompanyFacade companyFacade = new CompanyFacade(DbInstance, HttpUtil.Profile);
                var currentCompanyId = companyFacade.GetCurrentCompanyId();

                if (currentCompanyId == null)
                {
                    result.HasError = true;
                    result.Errors.Add("Valid company not found!");
                    return View(result);
                }
                // Valid Branch Checking
                if (branchId > 0)
                {
                    var branch = DbInstance.BAcnt_Branch
                        .SingleOrDefault(x => x.Id == branchId
                                              && x.CompanyId == currentCompanyId
                                              && x.IsEnable && !x.IsDeleted);
                    if (branch == null)
                    {
                        result.HasError = true;
                        result.Errors.Add("Invalid branch!");
                        return View(result);
                    }
                }

                // Get Ledger List
                var ledgerListWithGroup = DbInstance.BAcnt_Ledger.Where(x => x.CompanyId == currentCompanyId).ToList();

                #region Openning Blance 
                /*//ASSETS
                var assetLedgerGrp = ledgerListWithGroup.Find(x => x.Name.ToUpper().Trim() == "ASSETS" 
                                                          && x.IsGroup
                                                          && (x.Code.ToInt64() >= 100000 && x.Code.ToInt64() < 200000)
                                                          );

                if (assetLedgerGrp == null)
                {
                    result.HasError = true;
                    result.Errors.Add("'ASSETS' ledger group not found");
                    return View(result);
                }

                //Current Assets
                var currentAssetsLedgerGrp = ledgerListWithGroup.Find(x => x.Name.ToLower().Trim().Replace(" ", "") == "Current Assets".ToLower().Trim().Replace(" ", "")
                                                                  && x.IsGroup
                                                                  && (x.Code.ToInt64() > assetLedgerGrp.Code.ToInt64() && x.Code.ToInt64() < 200000));

                if (currentAssetsLedgerGrp == null)
                {
                    result.HasError = true;
                    result.Errors.Add("'Current Assets' ledger group not found inside the 'ASSETS->");
                    return View(result);
                }

                //Cash in Hand
                var cashInHandLedgerGrp = ledgerListWithGroup.Find(x => x.Name.ToLower().Trim().Replace(" ", "") == "Cash in Hand".ToLower().Trim().Replace(" ", "")
                                                                  && x.IsGroup
                                                                  && (x.Code.ToInt64() > assetLedgerGrp.Code.ToInt64() && x.Code.ToInt64() < 200000));

                if (cashInHandLedgerGrp == null)
                {
                    result.HasError = true;
                    result.Errors.Add("'Current Assets' ledger group not found inside the 'ASSETS->Current Assets->");
                    return View(result);
                }

                //Cash
                var cashLedgerGrp = ledgerListWithGroup.Find(x => x.Name.ToLower().Trim() == "cash" 
                                                         && x.IsGroup 
                                                         && (x.Code.ToInt64() > cashInHandLedgerGrp.Code.ToInt64() && x.Code.ToInt64() < 200000));

                if (cashLedgerGrp == null)
                {
                    result.HasError = true;
                    result.Errors.Add("'Cash' ledger group not found inside the 'ASSETS->Current Assets->Cash in Hand'");
                    return View(result);
                }

                // Cash Ledger List
                var cashAcntLedgerList = ledgerListWithGroup.Where(x => x.ParentId == cashLedgerGrp.Id).ToList();

                
                if (cashAcntLedgerList.Count>0)
                {
                    allAcntLedgerList.AddRange(cashAcntLedgerList);
                }
               

                //Bank Accounts
                var bankAccountsLedgerGrp = ledgerListWithGroup.Find(x => x.Name.ToLower().Trim().Replace(" ", "") == "Bank Accounts".ToLower().Trim().Replace(" ", "")
                                                                  && x.IsGroup
                                                                  && (x.Code.ToInt64() > currentAssetsLedgerGrp.Code.ToInt64() && x.Code.ToInt64() < 200000));

                if (bankAccountsLedgerGrp == null)
                {
                    result.HasError = true;
                    result.Errors.Add("'Bank Accounts' ledger group not found inside the 'ASSETS->Current Assets");
                    return View(result);
                }

                //Current A/C
                var bankCurrentAcLedgerGrp = ledgerListWithGroup.Find(x => x.Name.ToLower().Trim().Replace(" ", "") == "Current A/C".ToLower().Trim().Replace(" ", "")
                                                                 && x.IsGroup
                                                                 && (x.Code.ToInt64() > bankAccountsLedgerGrp.Code.ToInt64() && x.Code.ToInt64() < 200000));

                if (bankCurrentAcLedgerGrp == null)
                {
                    result.HasError = true;
                    result.Errors.Add("'Current A/C' ledger group not found inside the 'ASSETS->Current Assets->Bank Accounts");
                    return View(result);
                }

                // Bank Current Ac Ledger List

                var bankCurrentAcLedgerList = ledgerListWithGroup.Where(x => x.ParentId == bankCurrentAcLedgerGrp.Id ).ToList();

                if (bankCurrentAcLedgerList.Count > 0)
                {
                    allAcntLedgerList.AddRange(bankCurrentAcLedgerList);
                }*/


                var bankOrCashLedgerList = ledgerListWithGroup.Where(x => x.IsBank).OrderBy(x => x.Id).ToList();

                /*if (bankOrCashLedgerList.Count<=0)
                {
                    result.HasError = true;
                    result.Errors.Add("Bank ledger not found!");
                    return View(result);
                }*/

                List<BAcnt_Ledger> allAcntLedgerList = new List<BAcnt_Ledger>();

                allAcntLedgerList.AddRange(bankOrCashLedgerList);

                var receiptPayment = ReceiptPaymentModel.GetNew();

                var bankList = GetBalance(allAcntLedgerList, ledgerListWithGroup, branchId, startOnlyDate, endOnlyDate, out error);

                if (bankList == null)
                {
                    result.HasError = true;
                    result.Errors.Add(error);
                    return View(result);
                }

                receiptPayment.BankList = bankList;

                receiptPayment.TotalOpeningBalance = bankList.Sum(x => x.PeriodOpeningBalance);
                receiptPayment.TotalClosingBalance = bankList.Sum(x => x.PeriodClosingBalance);

                #endregion



                IQueryable<BAcnt_Voucher> query = DbInstance.BAcnt_Voucher
                    .Include(x => x.BAcnt_VoucherDetail)
                    .Include(x => x.BAcnt_VoucherType)
                    .Include(x => x.BAcnt_Branch)
                    .Where(x => !x.IsDeleted);

                if (branchId > 0)
                {
                    query = query.Where(q => q.BranchId == branchId);
                }


                query = query.Where(x => EntityFunctions.TruncateTime(x.Date) >= EntityFunctions.TruncateTime(startOnlyDate)
                                         && EntityFunctions.TruncateTime(x.Date) <= EntityFunctions.TruncateTime(endOnlyDate));

                // Get voucher List
                var voucherList = query.ToList();

                //Start voucher loop
                foreach (var voucher in voucherList)
                {
                    //Start voucher detail loop
                    foreach (var voucherDetail in voucher.BAcnt_VoucherDetail)
                    {
                        //Check Cash or Bank Account Ledger
                        if (allAcntLedgerList.Any(x => x.Id == voucherDetail.LedgerId))
                        {
                            continue;
                        }

                        // Recipt payment report ea jabe na.
                        if (voucherDetail.BAcnt_Ledger.TypeEnumId == (byte)BAcnt_Ledger.TypeEnum.Expense && voucher.BAcnt_VoucherType.TypeEnumId == (byte)BAcnt_VoucherType.TypeEnum.Journal)
                        {
                            continue;
                        }

                        //
                        var findTransLedger = receiptPayment.LedgerTransactionList.Find(x => x.LedgerId == voucherDetail.LedgerId);

                        var ledger = voucherDetail.BAcnt_Ledger;

                        // New Ledger
                        if (findTransLedger == null)
                        {
                            var ledgerTrans = new LedgerTransactionModel();

                            ledgerTrans.LedgerId = ledger.Id;
                            ledgerTrans.LedgerCode = ledger.CodeName;
                            ledgerTrans.LedgerName = ledger.Name;

                            // Todo Check logic next time
                            ledgerTrans.ReceiptAmount += voucherDetail.CreditAmount;
                            ledgerTrans.PaymentsAmount += voucherDetail.DebitAmount;


                            // Add new ledger in Ledger transaction list 
                            receiptPayment.LedgerTransactionList.Add(ledgerTrans);

                        }
                        else
                        {
                            // Todo Check logic next time
                            findTransLedger.ReceiptAmount += voucherDetail.CreditAmount;
                            findTransLedger.PaymentsAmount += voucherDetail.DebitAmount;


                        }


                    }//End voucher detail loop

                }//End voucher loop

                // Total Receipt Amount
                receiptPayment.TotalReceiptAmount = receiptPayment.LedgerTransactionList.Sum(x => x.ReceiptAmount);

                // Total Payments Amount
                receiptPayment.TotalPaymentAmount = receiptPayment.LedgerTransactionList.Sum(x => x.PaymentsAmount);

                // Net Total Receipt Amount
                receiptPayment.NetTotalReceiptsAmount =
                    receiptPayment.TotalReceiptAmount + receiptPayment.TotalOpeningBalance;

                // Total Payments Amount
                receiptPayment.NetTotalPaymentAmount =
                    receiptPayment.TotalPaymentAmount + receiptPayment.TotalClosingBalance;

                result.Data = receiptPayment;

                result.DataBag.StartDate = startOnlyDate;
                result.DataBag.EndDate = endOnlyDate;
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.Message);
            }

            return View(result);
        }

        private List<BankBalanceModel> GetBalance(
            List<BAcnt_Ledger> bankLedgerList,
            List<BAcnt_Ledger> ledgerListWithGroup,
            int branchId,
            DateTime startOnlyDate,
            DateTime endOnlyDate,
            out string error
            )
        {
            error = String.Empty;

            var bankBalanceList = new List<BankBalanceModel>();
            foreach (var bankLedger in bankLedgerList)
            {
                var bankBalance = new BankBalanceModel();

                bankBalance.Name = bankLedger.Name;
                bankBalance.Id = bankLedger.Id;

                var bank = Facade.BasicAccountingFacade.LedgerTransactionFacade
                    .GetTransactionListByLedgerId(bankLedger, ledgerListWithGroup, startOnlyDate, endOnlyDate, out error);


                if (bank == null)
                {
                    return null;
                }

                bankBalance.PeriodOpeningBalance = bank.PeriodBeginningDebitBalance - bank.PeriodBeginningCreditBalance;
                bankBalance.PeriodClosingBalance = bank.PeriodClosingDebitBalance - bank.PeriodClosingCreditBalance;

                bankBalanceList.Add(bankBalance);
            }

            return bankBalanceList;


        }
        #endregion

        #region Trial Balance
        public class TrialBalanceModel
        {
            public int LedgerId { get; set; }
            public int? ParentLedgerId { get; set; }
            public string LedgerCode { get; set; }
            public string LedgerName { get; set; }
            public double LedgerTotalDebit { get; set; }
            public double LedgerTotalCredit { get; set; }
            public double LedgerBalance { get; set; }
            public string BalanceType { get; set; }
            //public List<TrialBalanceModel> ChildLedgerList { get; set; }

            public static TrialBalanceModel GetNew()
            {
                var obj = new TrialBalanceModel
                {
                    //ChildLedgerList = new List<TrialBalanceModel>()
                };
                return obj;
            }


        }
        public ActionResult TrialBalanceManager()
        {
            return View();
        }
        public ActionResult TrialBalancePrint(bool isUgcReport = false, int branchId = 0, string startDate = "", string endDate = "")
        {
            string error = String.Empty;
            var result = new MvcModelListResult<TrialBalanceModel>();


            try
            {
                if (branchId == 0 && startDate == "" && endDate == "")
                {
                    return View();
                }

                if (!startDate.IsValid())
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid Start Date");
                    return View(result);
                }
                if (!endDate.IsValid())
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid End Date");
                    return View(result);
                }


                DateTime startOnlyDate = Convert.ToDateTime(startDate).ToOnlyDate();
                DateTime endOnlyDate = Convert.ToDateTime(endDate).ToOnlyDate();

                if (startOnlyDate > endOnlyDate)
                {
                    result.HasError = true;
                    result.Errors.Add("Start date should not be greater than end date!");
                    return View(result);
                }


                //Get Current Company Id
                CompanyFacade companyFacade = new CompanyFacade(DbInstance, HttpUtil.Profile);
                var currentCompanyId = companyFacade.GetCurrentCompanyId();

                if (currentCompanyId == null)
                {
                    result.HasError = true;
                    result.Errors.Add("Valid company not found!");
                    return View(result);
                }
                // Valid Branch Checking
                if (branchId > 0)
                {
                    var branch = DbInstance.BAcnt_Branch
                        .SingleOrDefault(x => x.Id == branchId
                                              && x.CompanyId == currentCompanyId
                                              && x.IsEnable && !x.IsDeleted);
                    if (branch == null)
                    {
                        result.HasError = true;
                        result.Errors.Add("Invalid branch!");
                        return View(result);
                    }
                }

                // Get Ledger List
                var ledgerListWithGroup = DbInstance.BAcnt_Ledger
                    .Where(x => x.CompanyId == currentCompanyId)
                    .ToList();

                //var receiptPayment = ReceiptPaymentModel.GetNew();
                var ledgerListWithoutGroup = DbInstance.BAcnt_Ledger.Where(x => !x.IsGroup).ToList();

                var trialBalanceLedgerList = new List<TrialBalanceModel>();

                // Only Ledger loop Start
                foreach (var ledger in ledgerListWithoutGroup)
                {
                    var trialBalanceLedger = TrialBalanceModel.GetNew();

                    trialBalanceLedger.LedgerId = ledger.Id;
                    trialBalanceLedger.ParentLedgerId = ledger.ParentId;

                    var parentLedger = ledgerListWithGroup.Find(x => x.Id == ledger.ParentId);
                    if (parentLedger != null)
                    {
                        trialBalanceLedger.LedgerCode = parentLedger.Code;
                    }
                    trialBalanceLedger.LedgerName = ledger.Name;

                    var ledgerTrans = Facade.BasicAccountingFacade.LedgerTransactionFacade
                        .GetTransactionListByLedgerId(ledger, ledgerListWithGroup, startOnlyDate, endOnlyDate, out error);
                    if (ledgerTrans == null)
                    {
                        return null;
                    }

                    trialBalanceLedger.LedgerTotalDebit = ledgerTrans.PeriodBeginningDebitBalance + ledgerTrans.PeriodTotalDebitAmount;
                    trialBalanceLedger.LedgerTotalCredit = ledgerTrans.PeriodBeginningCreditBalance + ledgerTrans.PeriodTotalCreditAmount;

                    if (ledger.TypeEnumId == (byte)BAcnt_Ledger.TypeEnum.Asset ||
                        ledger.TypeEnumId == (byte)BAcnt_Ledger.TypeEnum.Expense)
                    {

                        trialBalanceLedger.LedgerBalance = trialBalanceLedger.LedgerTotalDebit - trialBalanceLedger.LedgerTotalCredit;

                        if (trialBalanceLedger.LedgerBalance >= 0)
                        {
                            trialBalanceLedger.BalanceType = "Dr";
                        }
                        else
                        {
                            trialBalanceLedger.LedgerBalance = trialBalanceLedger.LedgerBalance * -1;
                            trialBalanceLedger.BalanceType = "Cr";
                        }
                    }

                    if (ledger.TypeEnumId == (byte)BAcnt_Ledger.TypeEnum.Liability ||
                        ledger.TypeEnumId == (byte)BAcnt_Ledger.TypeEnum.Income)
                    {

                        trialBalanceLedger.LedgerBalance = trialBalanceLedger.LedgerTotalCredit - trialBalanceLedger.LedgerTotalDebit;

                        if (trialBalanceLedger.LedgerBalance >= 0)
                        {
                            trialBalanceLedger.BalanceType = "Cr";
                        }
                        else
                        {
                            trialBalanceLedger.LedgerBalance = trialBalanceLedger.LedgerBalance * -1;
                            trialBalanceLedger.BalanceType = "Dr";
                        }
                    }


                    trialBalanceLedgerList.Add(trialBalanceLedger);

                }// Only Ledger loop End

                var firstParentList = new List<TrialBalanceModel>();
                var secondParentList = new List<TrialBalanceModel>();
                var thirdParentList = new List<TrialBalanceModel>();
                var fourthParentList = new List<TrialBalanceModel>();

                //var parentLedger = TrialBalanceModel.GetNew();

                //First Parent
                foreach (var firstParent in ledgerListWithGroup.Where(x => x.ParentId == null).ToList())
                {
                    //Second Parent
                    foreach (var secondParent in ledgerListWithGroup.Where(x => x.ParentId == firstParent.Id).ToList())
                    {
                        //Third Parent
                        foreach (var thirdParent in ledgerListWithGroup.Where(x => x.ParentId == secondParent.Id).ToList())
                        {
                            //Fourth Parent loop Start
                            foreach (var fourthParent in ledgerListWithGroup.Where(x => x.ParentId == thirdParent.Id).ToList())
                            {
                                fourthParentList.Add(MakeParent(fourthParent, trialBalanceLedgerList));

                            }//Fourth Parent loop end
                            thirdParentList.Add(MakeParent(thirdParent, fourthParentList));
                        }
                        secondParentList.Add(MakeParent(secondParent, thirdParentList));
                    }
                    firstParentList.Add(MakeParent(firstParent, secondParentList));
                }

                // Data set for General report or UGC Report
                if (isUgcReport)
                {
                    result.DataBag.FirstParentList = firstParentList;
                    result.DataBag.SecondParentList = secondParentList;
                    result.DataBag.ThirdParentList = thirdParentList;
                }
                else
                {
                    result.Data = trialBalanceLedgerList;
                }


                result.DataBag.TotalDebitAmount = trialBalanceLedgerList.Sum(x => x.LedgerTotalDebit);
                result.DataBag.TotalCreditAmount = trialBalanceLedgerList.Sum(x => x.LedgerTotalCredit);


                result.DataBag.StartDate = startOnlyDate;
                result.DataBag.EndDate = endOnlyDate;
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.Message);
            }

            return View(result);
        }
        public TrialBalanceModel MakeParent(BAcnt_Ledger ledger, List<TrialBalanceModel> trialBalanceLedgerList)
        {
            var parentObj = TrialBalanceModel.GetNew();

            parentObj.LedgerId = ledger.Id;
            parentObj.LedgerCode = ledger.Code;
            parentObj.LedgerName = ledger.Name;
            parentObj.ParentLedgerId = ledger.ParentId;

            // Debit Balance
            parentObj.LedgerTotalDebit = trialBalanceLedgerList
                .Where(x => x.ParentLedgerId == ledger.Id)
                .Sum(x => x.LedgerTotalDebit);

            // Credit Balance
            parentObj.LedgerTotalCredit = trialBalanceLedgerList
                .Where(x => x.ParentLedgerId == ledger.Id)
                .Sum(x => x.LedgerTotalCredit);

            return parentObj;
        }


        #endregion

        #region Static Report

        public ActionResult StatementOf16_17()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Reports.CanViewUgcStaticReports))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        public ActionResult StatementOf17_18()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Reports.CanViewUgcStaticReports))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        public ActionResult StatementOf18_19()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Reports.CanViewUgcStaticReports))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        public ActionResult StatementOf19_20()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Reports.CanViewUgcStaticReports))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        public ActionResult DebitVoucher()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Reports.CanViewUgcStaticReports))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }

        #endregion



        #region Product

        //public ActionResult ProductPrint(int id = 0)
        //{
        //    /*if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.Voucher.CanView))
        //    {
        //        return RedirectToAction("PermissionDenied", "Home", new { area = "" });
        //    }*/
        //    var result = new MvcModelResult<Invt_Purchase>();
        //    string error = string.Empty;

        //    try
        //    {
        //        var PurchaseList = DbInstance.Invt_Purchase.ToList();
        //        var ItemList = DbInstance.Invt_Item.ToList();
        //        var WarhouseList = DbInstance.Invt_Warhouse.ToList();
        //        //var UnitTypeList = EnumUtil.GetEnumList(typeof(Invt_Purchase.ProgramTypeEnum));




        //        var purchase = DbInstance.Invt_Purchase
        //            .Include(x=>x.Invt_Supplier)
        //            .Where(x => !x.IsDeleted)
        //            .Include(x => x.Invt_PurchaseDetail)
        //            //.Include(x => x.BAcnt_VoucherDetail)
        //            .SingleOrDefault(x => x.Id == id);


        //        // Amount In Words
        //        string amountInWords = String.Empty;
        //        amountInWords = StringHelper.ToWordsInBDT(purchase.TotalAmount);
        //        amountInWords = amountInWords.ToTitleCase();
        //        result.DataBag.AmountInWords = amountInWords;




        //        //    if (voucher == null)
        //        //    {
        //        //        result.HasError = true;
        //        //        result.Errors.Add("Invalid Voucher!");
        //        //        return View(result);
        //        //    }

        //        //    var ledgerIds = voucher.BAcnt_VoucherDetail
        //        //        .DistinctBy(x => x.LedgerId)
        //        //        .Select(x => x.LedgerId)
        //        //        .ToList();

        //        //    var voucherList = DbInstance.BAcnt_Ledger
        //        //        .Join(ledgerIds, all => all.Id, req => req, (all, req) => all)
        //        //        .ToList();
        //        //    //BAcnt_Ledger load for show ledger name in UI
        //        //    result.DataBag.VoucherList = voucherList;

        //        //    var totalDebitAmount = voucher.BAcnt_VoucherDetail.Where(x => x.IsDebited && !x.IsDeleted)
        //        //        .Sum(x => x.DebitAmount);

        //        //    var totalCreditAmount = voucher.BAcnt_VoucherDetail.Where(x => !x.IsDebited && !x.IsDeleted)
        //        //        .Sum(x => x.CreditAmount);

        //        //    result.DataBag.TotalDebitAmount = totalDebitAmount;
        //        //    result.DataBag.TotalCreditAmount = totalCreditAmount;

        //        //    // Amount In Words
        //        //    string amountInWords = String.Empty;
        //        //    amountInWords = StringHelper.ToWordsInBDT(totalDebitAmount);
        //        //    amountInWords = amountInWords.ToTitleCase();
        //        //    result.DataBag.AmountInWords = amountInWords;

        //        //    // Get Voucher Title Name
        //        //    result.DataBag.VoucherTitleName = GetVoucherTitleName(voucher.BAcnt_VoucherType);

        //        result.Data = purchase;

        //    }
        //    catch (Exception ex)
        //    {
        //        result.HasError = true;
        //        result.Errors.Add(ex.Message);
        //    }

        //    return View(result);

        //}

        #endregion

        #region Inventory
        public ActionResult InventoryPrint(int id = 0)
        {
            /*if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.Voucher.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }*/
            var result = new MvcModelResult<Inv_Inventory>();
            string error = string.Empty;

            try
            {
                //var PurchaseList = DbInstance.Invt_Purchase.ToList();
                //var ItemList = DbInstance.Invt_Item.ToList();
                //var WarhouseList = DbInstance.Invt_Warhouse.ToList();
                //var UnitTypeList = EnumUtil.GetEnumList(typeof(Invt_Purchase.ProgramTypeEnum));




                var inventory = DbInstance.Inv_Inventory
                    .Include(x => x.Inv_InventoryDetails)
                    .Where(x => !x.IsDeleted)
                    //.Include(x => x.Invt_PurchaseDetail)
                    //.Include(x => x.BAcnt_VoucherDetail)
                    .SingleOrDefault(x => x.Id == id);


                var unitPrice = inventory.Inv_InventoryDetails.Sum(x => x.UnitPrice);


                // Amount In Words
                //string amountInWords = String.Empty;
                //amountInWords = StringHelper.ToWordsInBDT(unitPrice);
                //amountInWords = amountInWords.ToTitleCase();
                //result.DataBag.AmountInWords = amountInWords;




                //    if (voucher == null)
                //    {
                //        result.HasError = true;
                //        result.Errors.Add("Invalid Voucher!");
                //        return View(result);
                //    }

                //    var ledgerIds = voucher.BAcnt_VoucherDetail
                //        .DistinctBy(x => x.LedgerId)
                //        .Select(x => x.LedgerId)
                //        .ToList();

                //    var voucherList = DbInstance.BAcnt_Ledger
                //        .Join(ledgerIds, all => all.Id, req => req, (all, req) => all)
                //        .ToList();
                //    //BAcnt_Ledger load for show ledger name in UI
                //    result.DataBag.VoucherList = voucherList;

                //    var totalDebitAmount = voucher.BAcnt_VoucherDetail.Where(x => x.IsDebited && !x.IsDeleted)
                //        .Sum(x => x.DebitAmount);

                //    var totalCreditAmount = voucher.BAcnt_VoucherDetail.Where(x => !x.IsDebited && !x.IsDeleted)
                //        .Sum(x => x.CreditAmount);

                //    result.DataBag.TotalDebitAmount = totalDebitAmount;
                //    result.DataBag.TotalCreditAmount = totalCreditAmount;

                //    // Amount In Words
                //    string amountInWords = String.Empty;
                //    amountInWords = StringHelper.ToWordsInBDT(totalDebitAmount);
                //    amountInWords = amountInWords.ToTitleCase();
                //    result.DataBag.AmountInWords = amountInWords;

                //    // Get Voucher Title Name
                //    result.DataBag.VoucherTitleName = GetVoucherTitleName(voucher.BAcnt_VoucherType);

                result.Data = inventory;

            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.Message);
            }

            return View(result);

        }

        #endregion

        #region Requisition

        public ActionResult RequisitionPrint(int id = 0)
        {
            /*if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.Voucher.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }*/
            var result = new MvcModelResult<Inv_Requisition>();
            string error = string.Empty;

            try
            {
                //var PurchaseList = DbInstance.Invt_Purchase.ToList();
                //var ItemList = DbInstance.Invt_Item.ToList();
                //var WarhouseList = DbInstance.Invt_Warhouse.ToList();
                //var UnitTypeList = EnumUtil.GetEnumList(typeof(Invt_Purchase.ProgramTypeEnum));

                var requisition = DbInstance.Inv_Requisition
                    .Include(x => x.Inv_RequisitionDetails)
                    .Where(x => !x.IsDeleted)
                    //.Include(x => x.Invt_PurchaseDetail)
                    //.Include(x => x.BAcnt_VoucherDetail)
                    .SingleOrDefault(x => x.Id == id);

                //custom value insert

                requisition.RequestedByEmployeeName =
                    DbInstance.User_Account.SingleOrDefault(x => x.Id == requisition.ReceivedByEmployeeId).FullName
                        .ToString() == null
                        ? ""
                        : DbInstance.User_Account.SingleOrDefault(x => x.Id == requisition.ReceivedByEmployeeId).FullName
                            .ToString();

                requisition.ReferencedByEmployeeName =
                    DbInstance.User_Account.SingleOrDefault(x => x.Id == requisition.ReferencedByEmployeeId).FullName
                    .ToString() == null
                    ? ""
                    : DbInstance.User_Account.SingleOrDefault(x => x.Id == requisition.ReferencedByEmployeeId).FullName
                        .ToString();

                requisition.IssuedOrReleaseByUserName =
                    DbInstance.User_Account.SingleOrDefault(x => x.Id == requisition.IssuedOrReleaseByUserId).FullName
                    .ToString() == null
                    ? ""
                    : DbInstance.User_Account.SingleOrDefault(x => x.Id == requisition.IssuedOrReleaseByUserId).FullName
                        .ToString();

                requisition.ApprovedByAdminName =
                    DbInstance.User_Account.SingleOrDefault(x => x.Id == requisition.ApprovedByAdminId).FullName
                    .ToString() == null
                    ? ""
                    : DbInstance.User_Account.SingleOrDefault(x => x.Id == requisition.ApprovedByAdminId).FullName
                        .ToString();

                requisition.ReceivedByEmployeeName =
                    DbInstance.User_Account.SingleOrDefault(x => x.Id == requisition.ReceivedByEmployeeId).FullName
                    .ToString() == null
                    ? ""
                    : DbInstance.User_Account.SingleOrDefault(x => x.Id == requisition.ReceivedByEmployeeId).FullName
                        .ToString();



                //var unitPrice = inventory.Inv_RequisitionDetails.Sum(x => x.UnitPrice);


                // Amount In Words
                //string amountInWords = String.Empty;
                //amountInWords = StringHelper.ToWordsInBDT(unitPrice);
                //amountInWords = amountInWords.ToTitleCase();
                //result.DataBag.AmountInWords = amountInWords;


                //    if (voucher == null)
                //    {
                //        result.HasError = true;
                //        result.Errors.Add("Invalid Voucher!");
                //        return View(result);
                //    }

                //    var ledgerIds = voucher.BAcnt_VoucherDetail
                //        .DistinctBy(x => x.LedgerId)
                //        .Select(x => x.LedgerId)
                //        .ToList();

                //    var voucherList = DbInstance.BAcnt_Ledger
                //        .Join(ledgerIds, all => all.Id, req => req, (all, req) => all)
                //        .ToList();
                //    //BAcnt_Ledger load for show ledger name in UI
                //    result.DataBag.VoucherList = voucherList;

                //    var totalDebitAmount = voucher.BAcnt_VoucherDetail.Where(x => x.IsDebited && !x.IsDeleted)
                //        .Sum(x => x.DebitAmount);

                //    var totalCreditAmount = voucher.BAcnt_VoucherDetail.Where(x => !x.IsDebited && !x.IsDeleted)
                //        .Sum(x => x.CreditAmount);

                //    result.DataBag.TotalDebitAmount = totalDebitAmount;
                //    result.DataBag.TotalCreditAmount = totalCreditAmount;

                //    // Amount In Words
                //    string amountInWords = String.Empty;
                //    amountInWords = StringHelper.ToWordsInBDT(totalDebitAmount);
                //    amountInWords = amountInWords.ToTitleCase();
                //    result.DataBag.AmountInWords = amountInWords;

                //    // Get Voucher Title Name
                //    result.DataBag.VoucherTitleName = GetVoucherTitleName(voucher.BAcnt_VoucherType);

                result.Data = requisition;

            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.Message);
            }

            return View(result);

        }


        #endregion



        #region ProductCategory

        //public ActionResult ProductCategoryPrint(int id = 0)
        //{
        //    /*if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.Voucher.CanView))
        //    {
        //        return RedirectToAction("PermissionDenied", "Home", new { area = "" });
        //    }*/
        //    var result = new MvcModelResult<Inv_ProductCategory>();
        //    string error = string.Empty;

        //    try
        //    {
        //        //var PurchaseList = DbInstance.Invt_Purchase.ToList();
        //        //var ItemList = DbInstance.Invt_Item.ToList();
        //        //var WarhouseList = DbInstance.Invt_Warhouse.ToList();
        //        //var UnitTypeList = EnumUtil.GetEnumList(typeof(Invt_Purchase.ProgramTypeEnum));




        //        var productCategory = DbInstance.Inv_ProductCategory
        //            .Include(x => x.Inv_ProductCategoryDetails)
        //            .Where(x => !x.IsDeleted)
        //            //.Include(x => x.Invt_PurchaseDetail)
        //            //.Include(x => x.BAcnt_VoucherDetail)
        //            .SingleOrDefault(x => x.Id == id);





        //        //productCategory.AssetTypeEnumName
        //        //var test    = EnumUtil.GetEnumList(typeof(Inv_ProductCategory.AssetTypeEnum));


        //        // Amount In Words
        //        //string amountInWords = String.Empty;
        //        //amountInWords = StringHelper.ToWordsInBDT(unitPrice);
        //        //amountInWords = amountInWords.ToTitleCase();
        //        //result.DataBag.AmountInWords = amountInWords;




        //        //    if (voucher == null)
        //        //    {
        //        //        result.HasError = true;
        //        //        result.Errors.Add("Invalid Voucher!");
        //        //        return View(result);
        //        //    }

        //        //    var ledgerIds = voucher.BAcnt_VoucherDetail
        //        //        .DistinctBy(x => x.LedgerId)
        //        //        .Select(x => x.LedgerId)
        //        //        .ToList();

        //        //    var voucherList = DbInstance.BAcnt_Ledger
        //        //        .Join(ledgerIds, all => all.Id, req => req, (all, req) => all)
        //        //        .ToList();
        //        //    //BAcnt_Ledger load for show ledger name in UI
        //        //    result.DataBag.VoucherList = voucherList;

        //        //    var totalDebitAmount = voucher.BAcnt_VoucherDetail.Where(x => x.IsDebited && !x.IsDeleted)
        //        //        .Sum(x => x.DebitAmount);

        //        //    var totalCreditAmount = voucher.BAcnt_VoucherDetail.Where(x => !x.IsDebited && !x.IsDeleted)
        //        //        .Sum(x => x.CreditAmount);

        //        //    result.DataBag.TotalDebitAmount = totalDebitAmount;
        //        //    result.DataBag.TotalCreditAmount = totalCreditAmount;

        //        //    // Amount In Words
        //        //    string amountInWords = String.Empty;
        //        //    amountInWords = StringHelper.ToWordsInBDT(totalDebitAmount);
        //        //    amountInWords = amountInWords.ToTitleCase();
        //        //    result.DataBag.AmountInWords = amountInWords;

        //        //    // Get Voucher Title Name
        //        //    result.DataBag.VoucherTitleName = GetVoucherTitleName(voucher.BAcnt_VoucherType);

        //        result.Data = productCategory;

        //    }
        //    catch (Exception ex)
        //    {
        //        result.HasError = true;
        //        result.Errors.Add(ex.Message);
        //    }

        //    return View(result);

        //}

        #endregion








    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Jsons.Custom.BasicAccounting;
using Microsoft.Ajax.Utilities;

namespace EMS.Facade.BasicAccountingArea
{
    public class LedgerTransactionFacade : BaseFacade
    {
        private readonly UserProfile _profile;
        private readonly EmsDbContext _emsDbContext;

        public LedgerTransactionFacade(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {

        }

        public LedgerTransactionJson GetTransactionListByLedgerId(
            BAcnt_Ledger ledger
            ,List<BAcnt_Ledger> ledgerListWithGroup
            , DateTime startOnlyDate
            , DateTime endOnlyDate
            , out string error
        )
        {
             error = string.Empty;

                if (ledger == null)
                {
                    error = "Invalid Ledger!";
                    return null;
                }

                var ledgerTrans = LedgerTransactionJson.GetNew();

                //Get Ledger Parent
                BAcnt_Ledger ledgerParent;
                if (ledgerListWithGroup == null || ledgerListWithGroup.Count <= 0)
                {
                    ledgerParent = DbInstance.BAcnt_Ledger.SingleOrDefault(x => x.Id == ledger.ParentId);
                }
                else
                {
                    ledgerParent = ledgerListWithGroup.SingleOrDefault(x => x.Id == ledger.ParentId);
                }
                
                if (ledgerParent!=null)
                {
                    ledgerTrans.LedgerParentCode = ledgerParent.Code;
                }

                //Get VoucherDetail List only by selected ledger
            var voucherDtlList = DbInstance.BAcnt_VoucherDetail
                    .Where(x => x.LedgerId == ledger.Id && !x.IsDeleted).ToList();

            /*if (voucherDtlList.Count <= 0)
            {
                error = "No transaction exist by this ledger!";
                return null;
            }*/

            var voucherIds = voucherDtlList
                    .DistinctBy(x => x.VoucherId)
                    .Select(x => x.VoucherId)
                    .ToList();

                // Get voucher
                var voucherList = DbInstance.BAcnt_Voucher
                    .Where(x => !x.IsDeleted)
                    .Where(t=>voucherIds.Contains(t.Id))
                    //.Join(voucherIds, all => all.Id, req => req, (all, req) => all)
                    .Include(x => x.BAcnt_VoucherType)
                    .Include(x=>x.BAcnt_Branch)
                    .ToList();

                // Get voucher Details
                voucherDtlList = DbInstance.BAcnt_VoucherDetail
                    .Where(x => !x.IsDeleted)
                    .Where(t=>voucherIds.Contains(t.VoucherId))
                    //.Join(voucherIds, all => all.VoucherId, req => req, (all, req) => all)
                    .Include(x => x.BAcnt_Ledger)
                    .ToList();
               

                ledgerTrans.LedgerName = ledger.Name;
                ledgerTrans.LedgerId = ledger.Id;

                // Code Name Use in Report
                ledgerTrans.LedgerCodeName = ledger.CodeName;

                voucherList = voucherList.OrderBy(x => x.Date).ToList();


                foreach (var voucher in voucherList)
                {
                    var voucherJson = VoucherJson.GetNew();
                    voucherJson.Id = voucher.Id;

                    voucherJson.Date = voucher.Date;
                    voucherJson.VoucherNo = voucher.VoucherNo;


                    if (voucher.ChequeNo.IsValid()&& voucher.ChequeNo.Contains(","))
                    {
                        voucherJson.ChequeNo = voucher.ChequeNo.Replace(",",", ");
                    }
                    else
                    {
                        voucherJson.ChequeNo = voucher.ChequeNo;
                    }
                    

                    if (voucher.ChequeDate.IsValid()&& voucher.ChequeDate.Contains(","))
                    {
                        voucherJson.ChequeDate = voucher.ChequeDate.Replace(",",", ");
                    }
                    else
                    {
                        voucherJson.ChequeDate = voucher.ChequeDate;
                    }

                    voucherJson.VoucherType = voucher.BAcnt_VoucherType.Name;
                    voucherJson.BranchName = voucher.BAcnt_Branch.Name;
                    

                    voucherJson.Narration = voucher.Narration;

                    var thisVoucherDtlList = voucherDtlList.Where(x => x.VoucherId == voucher.Id).ToList();
                    foreach (var voucherDtl in thisVoucherDtlList)
                    {
                        var voucherDtlJson = new VoucherDetailsJson();

                        voucherDtlJson.Id = voucherDtl.Id;
                        voucherDtlJson.Particular = voucherDtl.BAcnt_Ledger.Name;
                        voucherDtlJson.CodeName = voucherDtl.BAcnt_Ledger.CodeName;
                        voucherDtlJson.DebitAmount = voucherDtl.DebitAmount;
                        voucherDtlJson.CreditAmount = voucherDtl.CreditAmount;
                        voucherJson.VoucherDetailsJsonList.Add(voucherDtlJson);

                    }// end voucherDtl loop

                    if (thisVoucherDtlList.Count > 0)
                    {
                        voucherJson.Particular = thisVoucherDtlList[0].BAcnt_Ledger.Name;

                        // Total Debit Or Total Credit both are same at a voucher
                        voucherJson.VoucherTotalAmount = voucherJson.VoucherDetailsJsonList.Sum(x => x.DebitAmount);


                        // Selected Ledger Total Debit Amount for this voucher
                        voucherJson.SelectedLedgerDrAmount = thisVoucherDtlList
                                .Where(x => x.LedgerId == ledger.Id
                                            && x.IsDebited
                                            && !x.IsDeleted
                                            ).Sum(x => x.DebitAmount);

                        // Selected Ledger Total Credit Amount for this voucher
                        voucherJson.SelectedLedgerCrAmount = thisVoucherDtlList
                            .Where(x => x.LedgerId == ledger.Id
                                        && !x.IsDebited
                                        && !x.IsDeleted
                                        ).Sum(x => x.CreditAmount);

                    }


                    #region Calculate Opening
                    // Get Period Total Debit and Credit Total Amount for selected Ledger
                    var thisVoucherOnlyDate = voucher.Date.ToOnlyDate();

                    // Opening Balance Voucher
                    if (thisVoucherOnlyDate < startOnlyDate)
                    {
                        // Period Beginning Debit Sum
                        ledgerTrans.PeriodBeginningDebitBalance += voucher.BAcnt_VoucherDetail.Where(x => x.LedgerId == ledger.Id
                                                                                                  && x.IsDebited
                                                                                                  && !x.IsDeleted)
                            .Sum(x => x.DebitAmount);

                        // Period Beginning Credit Sum
                        ledgerTrans.PeriodBeginningCreditBalance += voucher.BAcnt_VoucherDetail.Where(x => x.LedgerId == ledger.Id
                                                                                                   && !x.IsDebited
                                                                                                   && !x.IsDeleted)
                            .Sum(x => x.CreditAmount);
                    }


                    // Period Voucher
                    if (thisVoucherOnlyDate >= startOnlyDate && thisVoucherOnlyDate <= endOnlyDate)
                    {
                        ledgerTrans.PeriodTotalDebitAmount += voucher.BAcnt_VoucherDetail.Where(x => x.LedgerId == ledger.Id
                                                                                                    && x.IsDebited
                                                                                                    && !x.IsDeleted)
                                                                                 .Sum(x => x.DebitAmount);

                        ledgerTrans.PeriodTotalCreditAmount += voucher.BAcnt_VoucherDetail.Where(x => x.LedgerId == ledger.Id
                                                                                                    && !x.IsDebited
                                                                                                    && !x.IsDeleted)
                                                                                                    .Sum(x => x.CreditAmount);
                        // Make Period Voucher List
                        ledgerTrans.VoucherJsonList.Add(voucherJson);

                    }
                    #endregion



                }// end voucher loop

            /*
             *
            Asset/Expence			
            Balance=Balance(OpeningDebit-OpeningCredit) + Debit - Credit	

            (OpeningDebitBalance-OpeningCreditBalance) + Debit - Credit		

            Liability/Income			
            Balance=Balance(OpeningCredit - OpeningDebit) + Credit - Debit			

             */

            // Ordering Voucher Json List
            ledgerTrans.VoucherJsonList = ledgerTrans.VoucherJsonList.OrderBy(x => x.Date).ToList();

            if (ledger.TypeEnumId==(byte)BAcnt_Ledger.TypeEnum.Asset || ledger.TypeEnumId==(byte)BAcnt_Ledger.TypeEnum.Expense)
            {
                //Opening Balance
                double openingBalance = 0;
                if (ledger.OpeningBalance!=null)
                {
                    openingBalance = (ledgerTrans.PeriodBeginningDebitBalance + ledger.OpeningBalance ?? 0) - ledgerTrans.PeriodBeginningCreditBalance;
                }
                else
                {
                    openingBalance = ledgerTrans.PeriodBeginningDebitBalance - ledgerTrans.PeriodBeginningCreditBalance;
                }

                if (openingBalance>=0)
                {
                    ledgerTrans.PeriodOpeningDebitBalance = openingBalance;
                    
                }
                else
                {
                    ledgerTrans.PeriodOpeningCreditBalance = openingBalance * -1;
                }

                //Period Balance
                bool isFirstVoucher = true;
                double previousVoucherBalance = 0;
                
                foreach (var voucherJson in ledgerTrans.VoucherJsonList)
                {
                    if (isFirstVoucher)
                    {
                        //Balance = OpeningBalance + Debit - Credit
                        voucherJson.SelectedLedgerBalance =
                            openingBalance + voucherJson.SelectedLedgerDrAmount - voucherJson.SelectedLedgerCrAmount;
                        isFirstVoucher = false;
                    }
                    else
                    {
                        voucherJson.SelectedLedgerBalance = previousVoucherBalance + voucherJson.SelectedLedgerDrAmount - voucherJson.SelectedLedgerCrAmount;
                    }

                    previousVoucherBalance = voucherJson.SelectedLedgerBalance;

                    if (voucherJson.SelectedLedgerBalance >= 0)
                    {
                        voucherJson.BalanceType = "Dr";
                    }
                    else
                    {
                        voucherJson.SelectedLedgerBalance = voucherJson.SelectedLedgerBalance * -1;
                        voucherJson.BalanceType = "Cr";
                    }

                }

                // Closing Balance
                var closingBalance = openingBalance + ledgerTrans.PeriodTotalDebitAmount -
                                     ledgerTrans.PeriodTotalCreditAmount;

                if (closingBalance>=0)
                {
                    ledgerTrans.PeriodClosingDebitBalance = closingBalance;
                }
                else
                {
                    ledgerTrans.PeriodClosingCreditBalance = closingBalance * -1;
                }

            }


            if (ledger.TypeEnumId == (byte)BAcnt_Ledger.TypeEnum.Liability || ledger.TypeEnumId == (byte)BAcnt_Ledger.TypeEnum.Income)
            {
                //Balance = Balance(OpeningCredit - OpeningDebit) + Credit - Debit

                //Opening Balance
                double openingBalance = 0;
                if (ledger.OpeningBalance != null)
                {
                    openingBalance = (ledgerTrans.PeriodBeginningCreditBalance + ledger.OpeningBalance ?? 0.0) -
                                     ledgerTrans.PeriodBeginningDebitBalance;
                }
                else
                {
                    openingBalance = ledgerTrans.PeriodBeginningCreditBalance - ledgerTrans.PeriodBeginningDebitBalance;
                }
                

                if (openingBalance >= 0)
                {
                    
                    ledgerTrans.PeriodOpeningCreditBalance = openingBalance;
                }
                else
                {
                    ledgerTrans.PeriodOpeningDebitBalance = openingBalance * -1;
                }

                //Period Balance
                bool isFirstVoucher = true;
                double previousVoucherBalance = 0;

                foreach (var voucherJson in ledgerTrans.VoucherJsonList)
                {
                    if (isFirstVoucher)
                    {
                        //Balance = OpeningBalance + Debit - Credit
                        voucherJson.SelectedLedgerBalance =
                            openingBalance + voucherJson.SelectedLedgerCrAmount - voucherJson.SelectedLedgerDrAmount;
                        isFirstVoucher = false;
                    }
                    else
                    {
                        voucherJson.SelectedLedgerBalance = previousVoucherBalance + voucherJson.SelectedLedgerCrAmount - voucherJson.SelectedLedgerDrAmount;
                    }

                    previousVoucherBalance = voucherJson.SelectedLedgerBalance;

                    if (voucherJson.SelectedLedgerBalance>=0)
                    {
                        voucherJson.BalanceType = "Cr";
                    }
                    else
                    {
                        voucherJson.SelectedLedgerBalance = voucherJson.SelectedLedgerBalance * -1;
                        voucherJson.BalanceType = "Dr";
                    }

                }

                // Closing Balance
                var closingBalance = openingBalance + ledgerTrans.PeriodTotalCreditAmount - ledgerTrans.PeriodTotalDebitAmount;

                if (closingBalance >= 0)
                {
                    ledgerTrans.PeriodClosingCreditBalance = closingBalance;
                }
                else
                {
                    ledgerTrans.PeriodClosingDebitBalance = closingBalance * -1;
                }
            }


                return ledgerTrans;
                
           
        }

    }
}

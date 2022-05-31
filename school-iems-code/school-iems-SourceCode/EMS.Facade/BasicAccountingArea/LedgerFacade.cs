using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.Facade.AcademicArea;
using EMS.Framework.Objects;
using EMS.Framework.Utils;

namespace EMS.Facade.BasicAccountingArea
{
    public class LedgerFacade : BaseFacade
    {
        private readonly UserProfile _profile;
        private readonly EmsDbContext _emsDbContext;

        public LedgerFacade(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
        }

        #region Ledger Tree List

        /// <summary>
        /// 
        /// </summary>
        /// <param name="error"></param>
        /// <param name="ledgerList"></param>
        /// <param name="branchId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns>Ledger Entity Tree List</returns>
        public List<BAcnt_Ledger>  GetLedgerTreeList(
            out string error,
            List<BAcnt_Ledger> ledgerList = null,
            int branchId = 0,
            string startDate = "",
            string endDate = ""
        )
        {
            error = String.Empty;

            // Get ledger list from Database
            if (ledgerList == null)
            {


                CompanyFacade companyFacade = new CompanyFacade(DbInstance, HttpUtil.Profile);
                // Get Current Company
                var currentCompanyId = companyFacade.GetCurrentCompanyId();

                ledgerList = DbInstance.BAcnt_Ledger
                    .Where(x => x.CompanyId == currentCompanyId
                                && !x.IsDeleted
                                ).ToList();
            }
            var allParent = ledgerList.Where(x => x.ParentId == null).ToList();
            int orderNo = 0;

            IQueryable<BAcnt_Voucher> voucherListQuery = DbInstance.BAcnt_Voucher
                .Include(x => x.BAcnt_VoucherDetail)
                .Where(x => !x.IsDeleted);

            if (branchId > 0)
            {
                voucherListQuery = voucherListQuery.Where(x => x.BranchId == branchId);
            }

            if (startDate.IsValid() && endDate.IsValid())
            {
                DateTime startOnlyDate = Convert.ToDateTime(startDate).ToOnlyDate();
                DateTime endOnlyDate = Convert.ToDateTime(endDate).ToOnlyDate();

                if (startOnlyDate > endOnlyDate)
                {
                    error = "Start date should not be greater than end date!";
                    return null;
                }
                voucherListQuery = voucherListQuery.Where(x => EntityFunctions.TruncateTime(x.Date) >= EntityFunctions.TruncateTime(startOnlyDate)
                                                               && EntityFunctions.TruncateTime(x.Date) <= EntityFunctions.TruncateTime(endOnlyDate));
            }
            //This list auto use for voucher debit credit calculation, It's auto mapping with Ledger List By Entity Framework
            var voucherList = voucherListQuery.ToList();

            List<BAcnt_Ledger> treeList = new List<BAcnt_Ledger>();

            //Call Get Ledger Tree
            GetLedgerTreeList(ref orderNo, null, allParent, ledgerList, treeList, branchId);

            treeList = treeList.OrderBy(x => x.OrderNo).ToList();

            return treeList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderNo">orderNo=0</param>
        /// <param name="parent"></param>
        /// <param name="parentLedgerList"></param>
        /// <param name="ledgerList"></param>
        /// <param name="treeList"></param>
        /// <param name="branchId"></param>
        public void GetLedgerTreeList(ref int orderNo, BAcnt_Ledger parent, List<BAcnt_Ledger> parentLedgerList, List<BAcnt_Ledger> ledgerList, List<BAcnt_Ledger> treeList, int branchId)
        {
            parentLedgerList = parentLedgerList.OrderBy(x => x.Code).ToList();

            foreach (var childLedger in parentLedgerList)
            {
                orderNo++;
                childLedger.OrderNo = orderNo;

                // Get Branch wise Voucher Detail List
                var branchWiseVoucherDtlList = childLedger.BAcnt_VoucherDetail
                    .Where(x => !x.IsDeleted)
                    .ToList();

                if (branchId > 0)
                {
                    branchWiseVoucherDtlList = branchWiseVoucherDtlList
                        .Where(x => x.BAcnt_Voucher.BranchId == branchId
                                    && !x.IsDeleted)
                        .ToList();
                }

                childLedger.ChildTotalDebit = (float)branchWiseVoucherDtlList.Sum(x => x.DebitAmount);

                childLedger.ChildTotalCredit = (float)branchWiseVoucherDtlList.Sum(x => x.CreditAmount);

                string groupOrLedger = childLedger.IsGroup ? "Group" : "Ledger";

                Debug.WriteLine($"{childLedger.Id}\t{childLedger.Code}\t{childLedger.Name}\t{childLedger.Type}\t{groupOrLedger}");

                //Creating RawString For printing in html.
                childLedger.RowString = $"<tr>" +
                                        $"<td class='text-center'>{childLedger.Id}</td>" +
                                        $"<td class='text-center'>{childLedger.ParentId}</td>" +
                                        $"<td class='text-center'>{childLedger.BranchId}</td>" +
                                        $"<td class='text-center'>{childLedger.CompanyId}</td>" +
                                        $"<td class='text-center'>{childLedger.Type}</td>" +
                                        $"<td class='text-center'>{groupOrLedger}</td>";

                //Adding Extra td if there is any parent.
                if (childLedger.ParentId != null)
                {
                    var parentId = childLedger.ParentId;
                    List<string> parentNameList = new List<string>();
                    while (true)
                    {
                        if (parentId == null)
                        {
                            break;
                        }
                        var previousParent = treeList.FirstOrDefault(x => x.Id == parentId);
                        parentId = previousParent?.ParentId;
                        parentNameList.Add($"<td>{previousParent?.Code}</td><td>{previousParent?.Name} =></td>");
                    }

                    var parentCount = parentNameList.Count;
                    for (int i = parentCount - 1; i >= 0; i--)
                    {
                        childLedger.RowString += parentNameList[i];
                    }
                }

                childLedger.RowString += $"<td>{childLedger.Code}</td>" +
                                         $"<td>{childLedger.Name}</td>" +
                                         $"</tr>";


                treeList.Add(childLedger);

                var childLedgerList = ledgerList.Where(x => x.ParentId == childLedger.Id).ToList();

                if (childLedgerList.Count > 0)
                {
                    GetLedgerTreeList(ref orderNo, childLedger, childLedgerList, ledgerList, treeList, branchId);
                }

            }

            if (parent != null)
            {
                parent.ChildTotalDebit = parentLedgerList.Sum(x => x.ChildTotalDebit);
                parent.ChildTotalCredit = parentLedgerList.Sum(x => x.ChildTotalCredit);

                string groupOrLedger = parent.IsGroup ? "Group" : "Ledger";
                Debug.WriteLine($"{parent.Id}\t{parent.Code}\t{parent.Name}\t{parent.Type}\t{groupOrLedger}");
            }

        }


        #endregion


    }
}

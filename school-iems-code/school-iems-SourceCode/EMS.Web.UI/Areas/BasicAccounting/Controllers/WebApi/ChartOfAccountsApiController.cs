using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases.WebApi;
using EMS.Web.Jsons.Models;

namespace EMS.Web.UI.Areas.BasicAccounting.Controllers.WebApi
{
    public class ChartOfAccountsApiController : EmployeeBaseApiController
    {
        public HttpListResult<object> GetChartOfAccountsDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<object>();
            try
            {
                var currentCompanyId = Facade.BasicAccountingFacade.CompanyFacade.GetCurrentCompanyId();

                var branchList = DbInstance.BAcnt_Branch
                    .Where(x => x.CompanyId == currentCompanyId && x.IsEnable && !x.IsDeleted)
                    .AsEnumerable().Select(t => new
                        { Id = t.Id, Name = t.Name }).ToList();

                result.DataExtra.BranchList = branchList;

                result.DataExtra.StartDate = DateTime.Now.AddYears(-1);
                result.DataExtra.EndDate = DateTime.Now;

            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.Message);
            }
            return result;
        }


        //LedgerCode	LedgerName	Balance
        public class GetChartOfAccountsJson
        {
            public string LedgerCode { get; set; }
            public string LedgerName { get; set; }
            public float Balance { get; set; }
        }
        public HttpListResult<GetChartOfAccountsJson> GetChartOfAccounts(
            int branchId = 0,
            string startDate = "",
            string endDate = ""
        )
        {
            var result = new HttpListResult<GetChartOfAccountsJson>();
            string error = String.Empty;

            if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Reports.CanViewChartOfAccounts, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                var currentCompanyId = Facade.BasicAccountingFacade.CompanyFacade.GetCurrentCompanyId();

                if (currentCompanyId == null)
                {
                    result.HasError = true;
                    result.Errors.Add("Valid company not found!");
                    return result;
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
                        return result;
                    }
                }

                // Get Ledger List
                var ledgerList = DbInstance.BAcnt_Ledger
                    .Where(x => x.CompanyId == currentCompanyId
                                && !x.IsDeleted
                    ).ToList();

                

                //Call Get Ledger Tree
                var treeList = Facade.BasicAccountingFacade.LedgerFacade.GetLedgerTreeList(out error, ledgerList, branchId,startDate,endDate);
                if (treeList == null)
                {
                    result.HasError = true;
                    result.Errors.Add($"{error}");
                    return result;
                }

                // Only Root Parent List
                var onlyRootParent = treeList.Where(x => x.ParentId == null).OrderBy(x=>x.TypeEnumId).ToList();
                var chartOfAccountsJsonList =new List<GetChartOfAccountsJson>();

                foreach (var ledger in onlyRootParent)
                {
                    var chartOfAccountsJson = new GetChartOfAccountsJson
                    {
                        LedgerName = ledger.Name,
                        LedgerCode = ledger.Code,
                        Balance = ledger.ChildTotalDebit - ledger.ChildTotalCredit
                    };

                    chartOfAccountsJsonList.Add(chartOfAccountsJson);
                }
                result.Data = chartOfAccountsJsonList;

                return result;

            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.Message);
                return result;
            }
        }

    }
}
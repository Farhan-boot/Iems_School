//File: UI Controller
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;
using System.Diagnostics;
using EMS.Framework;
using EMS.Framework.Attributes;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.CoreLibrary.Helpers;
using EMS.Facade.BasicAccountingArea;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases;
using EMS.Web.Framework.Bases.WebApi;
using EMS.Web.Jsons.Models;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;
using EMS.Web.Jsons.Custom.BasicAccounting;
using Microsoft.Ajax.Utilities;


namespace EMS.Web.UI.Areas.BasicAccounting.Controllers.WebApi
{

    public class LedgerTransactionApiController : EmployeeBaseApiController
	{
        public LedgerTransactionApiController()
        {  }


        #region LedgerTransaction
        #region Get Api
        public HttpResult<LedgerTransactionJson> GetTransactionListByLedgerId(
            Int32 id = 0
            ,int branchId=0
            ,string startDate=""
            ,string endDate=""
            )
        {
            string error = string.Empty;
            var result = new HttpResult<LedgerTransactionJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.LedgerTransactions.CanViewLedgerTransactions, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
            {

                if (id <= 0)
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid Ledger Id!");
                    return result;
                }

                if (!startDate.IsValid())
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid Start Date");
                    return result;
                }
                if (!endDate.IsValid())
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid End Date");
                    return result;
                }

                DateTime startOnlyDate = Convert.ToDateTime(startDate).ToOnlyDate();
                DateTime endOnlyDate = Convert.ToDateTime(endDate).ToOnlyDate();

                if (startOnlyDate > endOnlyDate)
                {
                    result.HasError = true;
                    result.Errors.Add("Start date should not be greater than end date!");
                    return result;
                }

                //Get Current Company Id
                CompanyFacade companyFacade = new CompanyFacade(DbInstance, HttpUtil.Profile);
                var currentCompanyId = companyFacade.GetCurrentCompanyId();

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

                //Get Selected Ledger
                var ledger = DbInstance.BAcnt_Ledger.SingleOrDefault(x => x.Id == id
                                                                 && x.CompanyId == currentCompanyId
                                                                 );

                var ledgerTrans = Facade.BasicAccountingFacade.LedgerTransactionFacade.GetTransactionListByLedgerId(ledger,null,
                        startOnlyDate, endOnlyDate, out error);

                if (ledgerTrans==null)
                {
                    result.HasError = true;
                    result.Errors.Add(error);
                    return result;
                }
                result.Data = ledgerTrans;

                result.DataExtra.TotalVoucherItem = ledgerTrans.VoucherJsonList.Count;
                return result;

            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
                return result;
            }
            
        }

        public HttpListResult<BAcnt_LedgerJson> GetLedgerTransactionDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<BAcnt_LedgerJson>();
            try
            {

                var currentCompanyId = Facade.BasicAccountingFacade.CompanyFacade.GetCurrentCompanyId();

                var branchList = DbInstance.BAcnt_Branch
                    .Where(x => x.CompanyId == currentCompanyId && x.IsEnable && !x.IsDeleted)
                    .AsEnumerable().Select(t => new
                        { Id = t.Id, Name = t.Name }).ToList();

                result.DataExtra.BranchList = branchList;

                var startDate = DateTime.Now.AddYears(-1).ToOnlyDate();
                var endDate = DateTime.Now.ToOnlyDate();

                result.DataExtra.StartDate = startDate;
                result.DataExtra.EndDate = endDate;


            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        #endregion

        #region Local Method

        #endregion
        #endregion

	}
}

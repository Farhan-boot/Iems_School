//File: UI Controller
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.Web.Framework.Bases.Mvc;
using EMS.Framework;
using EMS.Framework.Attributes;
using EMS.DataAccess.Data;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Jsons.Models;

namespace EMS.Web.UI.Areas.BasicAccounting.Controllers
{
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
    ///[EmsAuthorize(User_Account.UserAuthorizeTypeEnum.Employee)]
    public class LedgerController : EmployeeBaseController
	{
    
        public ActionResult LedgerAddEdit()
        {
            /*if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.Ledger.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }*/
            return View();
        }
        public ActionResult LedgerList()
        {
            /*if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingArea.Ledger.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }*/
            return View();
        }

        public ActionResult LedgerListDownloadManager(int branchId=0)
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.BasicAccounting.Ledger.CanDownload))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            string error = string.Empty;
            var result = new MvcModelListResult<BAcnt_LedgerJson>();
            var currentCompanyId = Facade.BasicAccountingFacade.CompanyFacade.GetCurrentCompanyId();

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
            var ledgerList = DbInstance.BAcnt_Ledger
                .Where(x => x.CompanyId == currentCompanyId
                            && !x.IsDeleted
                ).ToList();

            /*
             * Todo
             * if ledger List not found in database then, create first parent Ledger Group
             */

            //Call Get Ledger Tree
            var treeList = Facade.BasicAccountingFacade.LedgerFacade.GetLedgerTreeList(out error, ledgerList, branchId);
            if (treeList == null)
            {
                result.HasError = true;
                result.Errors.Add($"{error}");
                return View(result); ;
            }
            var ledgerJsonList = new List<BAcnt_LedgerJson>();
            treeList.Map(ledgerJsonList);
            result.Data = ledgerJsonList;
            return View(result);
        }

        public ActionResult LedgerTransaction()
        {
            return View();
        }

    }
}

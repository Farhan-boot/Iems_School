using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;

namespace EMS.Web.UI.Areas.BasicAccounting.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Accounting/Dashboard
        public ActionResult Index()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.CanViewDashboard))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
    }
}
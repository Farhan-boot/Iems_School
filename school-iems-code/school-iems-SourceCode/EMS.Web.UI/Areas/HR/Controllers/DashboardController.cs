using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.CoreLibrary;
using EMS.DataAccess.Data;
using EMS.Framework.Attributes;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases;

namespace EMS.Web.UI.Areas.HR.Controllers
{
    [EmsMvcAuthorize(User_Account.UserTypeEnum.Employee)]
    public class DashboardController : BaseController
    {
        // GET: HR/Dashboard
        public ActionResult Index()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.CanViewDashboard))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        public ActionResult Contacts()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.EmployeeManager.Employee.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
    }
}
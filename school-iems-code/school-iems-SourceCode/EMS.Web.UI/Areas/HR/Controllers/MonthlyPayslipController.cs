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

namespace EMS.Web.UI.Areas.HR.Controllers
{
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
    ///[EmsAuthorize(User_Account.UserAuthorizeTypeEnum.Employee)]
    public class MonthlyPayslipController : EmployeeBaseController
	{
        public ActionResult MonthlyPayslipAddEdit()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.MonthlyPayslip.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        public ActionResult MonthlyPayslipList()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.MonthlyPayslip.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        public ActionResult MonthlyPayslipManager()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.MonthlyPayslip.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
    }
}

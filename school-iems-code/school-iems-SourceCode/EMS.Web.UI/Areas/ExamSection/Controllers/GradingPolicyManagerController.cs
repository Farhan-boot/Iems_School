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
using EMS.Framework.Permissions;
using EMS.Framework.Utils;

namespace EMS.Web.UI.Areas.ExamSection.Controllers
{
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
    ///[EmsAuthorize(User_Account.UserAuthorizeTypeEnum.Employee)]
    public class GradingPolicyManagerController : EmployeeBaseController
	{
    
        public ActionResult GradingPolicyAddEdit()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.ExamArea.PolicyManager.GradingPolicy.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        public ActionResult GradingPolicyList()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.ExamArea.PolicyManager.GradingPolicy.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        public ActionResult GradingPolicyOptionAddEdit()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.ExamArea.PolicyManager.GradingPolicy.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        public ActionResult GradingPolicyOptionList()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.ExamArea.PolicyManager.GradingPolicy.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
    }
}

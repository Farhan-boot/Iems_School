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

namespace EMS.Web.UI.Areas.Admission.Controllers
{
    [EmsMvcAuthorize(User_Account.UserTypeEnum.Employee)]
    public class DashboardController : BaseController
    {
        // GET: Admission/Dashboard
        public ActionResult Index()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.CanViewDashboard))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
    }
}
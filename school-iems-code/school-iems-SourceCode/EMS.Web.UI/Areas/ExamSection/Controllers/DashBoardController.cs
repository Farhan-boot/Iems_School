using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases.Mvc;

namespace EMS.Web.UI.Areas.ExamSection.Controllers
{
    public class DashBoardController : EmployeeBaseController
    {
        // GET: ExamSection/DashBoard
        public ActionResult Index()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.ExamArea.CanViewDashboard))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            //RedirectToAction("Action2", "ControllerName");
            //return View("~/Areas/ExamSection/Views/DashBoard/Index.cshtml");
            return View();

        }
    }
}
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
using EMS.Web.Framework.Bases.Mvc;

namespace EMS.Web.UI.Areas.Academic.Controllers
{
    public class DashboardController : EmployeeBaseController
    {
        // GET: Academic/Dashboard
        public ActionResult Index()
        {
            if (!HttpUtil.IsSupperAdmin())
                if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CanViewDashboard))
                {
                    return RedirectToAction("PermissionDenied", "Home", new { area = "" });
                }
            return View();
        }
        public ActionResult StudentProfileRegistrationStatistics()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CanViewDashboard))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        public ActionResult OfferedCourseStatistics()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.OfferedClass.CanViewReport))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
    }
}
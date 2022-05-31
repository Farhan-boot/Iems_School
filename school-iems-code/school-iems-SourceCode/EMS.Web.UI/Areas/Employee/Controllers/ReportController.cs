using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.Web.Framework.Bases;
using EMS.Web.Framework.Bases.Mvc;
using EMS.Framework.Utils;
using EMS.Framework.Permissions;

namespace EMS.Web.UI.Areas.Employee.Controllers
{
    public class ReportController : EmployeeBaseController
    {
        // GET: Student/Library
        public ActionResult AdmissionReportOf17()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.Report.CanViewUgcStaticAdmissionReport))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        public ActionResult AdmissionReportOf18()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.Report.CanViewUgcStaticAdmissionReport))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        public ActionResult AdmissionReportOf19()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.Report.CanViewUgcStaticAdmissionReport))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        public ActionResult AdmissionReportOf20()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.Report.CanViewUgcStaticAdmissionReport))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
    }
}
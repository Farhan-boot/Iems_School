using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases.Mvc;

namespace EMS.Web.UI.Areas.Academic.Controllers
{
    public class ClassResultManagerController : EmployeeBaseController
    {
        // GET: Academic/ClassResultManager
        public ActionResult MarksEditDashboard()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.ClassResultManager.ClassResult.CanViewAllMarks))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        public ActionResult FinalClassMarks()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.ClassResultManager.ClassResult.CanViewAllMarks))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        public ActionResult ComponentMarks()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.ClassResultManager.ClassResult.CanViewAllMarks))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        public ActionResult BreakdownMarks()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.ClassResultManager.ClassResult.CanViewAllMarks))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        public ActionResult Scrutinization()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.ClassResultManager.ClassResult.CanViewAllMarks))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        public ActionResult Print70PercentTheoryMarks()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.ClassResultManager.ClassResult.CanPrint70PercentMarks))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        public ActionResult Print30PercentTheoryMarks()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.ClassResultManager.ClassResult.CanPrint30PercentMarks))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        public ActionResult Print100PercentMarks()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.ClassResultManager.ClassResult.CanPrint100PercentMarks))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
    }
}
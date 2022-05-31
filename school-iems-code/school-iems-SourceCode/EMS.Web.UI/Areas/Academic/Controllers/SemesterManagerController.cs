using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.DataAccess.Data;
using EMS.Framework.Attributes;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases;
using EMS.Web.Framework.Bases.Mvc;

namespace EMS.Web.UI.Areas.Academic.Controllers
{
    public class SemesterManagerController : EmployeeBaseController
    {
        // GET: Academic/SemesterManager
        public ActionResult Index()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.SemesterManager.Semester.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        //public ActionResult SemesterLevelTermAddEdit()
        //{
        //    if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.SemesterManager.Semester.CanView))
        //    {
        //        return RedirectToAction("PermissionDenied", "Home", new { area = "" });
        //    }
        //    return View();
        //}
        //public ActionResult SemesterLevelTermList()
        //{
        //    if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.SemesterManager.Semester.CanView))
        //    {
        //        return RedirectToAction("PermissionDenied", "Home", new { area = "" });
        //    }
        //    return View();
        //}
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.CoreLibrary;
using EMS.DataAccess.Data;
using EMS.Framework.Attributes;
using EMS.Framework.Settings;
using EMS.Web.Framework.Bases;
using EMS.Web.Framework.Bases.Mvc;

namespace EMS.Web.UI.Areas.Student.Controllers
{
    public class FacultyEvaluationController : StudentBaseController
    {
        // GET: Student/FacultyEvaluation
        public ActionResult Index()
        {
            if (!SiteSettings.Instance.Student.FacultyEvaluationCanView)
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
    }
}
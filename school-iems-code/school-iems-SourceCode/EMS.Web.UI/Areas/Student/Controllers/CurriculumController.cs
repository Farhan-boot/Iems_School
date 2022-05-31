using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.Framework.Settings;
using EMS.Web.Framework.Bases.Mvc;

namespace EMS.Web.UI.Areas.Student.Controllers
{
    public class CurriculumController : StudentBaseController
    {
        // GET: Student/Curriculum
        public ActionResult Index()
        {
            if (!SiteSettings.Instance.Student.CurriculumOrSyllabusCanView)
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.DataAccess.Data;
using EMS.Framework.Settings;
using EMS.Web.Framework.Bases.Mvc;

namespace EMS.Web.UI.Areas.Student.Controllers
{
    public class OfferedCoursesController : StudentBaseController
    {
        // GET: Employee/OfferedCourses
        public ActionResult Index()
        {
            if (!SiteSettings.Instance.Student.OfferedCoursesCanView)
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }

            List<Aca_Class> model = DbInstance.Aca_Class
                .Include("HR_Department")
                .Include("Aca_StudyLevelTerm")
                .Include("Aca_StudyLevelTerm.Aca_StudyLevel")
                .Include("Aca_CurriculumCourse")
                .Include("Aca_Semester")
                .Include("Aca_ClassSection")
                .Where(x => x.Aca_Semester.Id == 201701)
                .OrderBy(x=>x.DepartmentId)
                .ThenBy(x=>x.StudyLevelTermId)
                .ThenBy(x=>x.Code)
                .ThenBy(x=>x.ClassSectionId)
                .ThenBy(x=>x.CampusId)
                .ToList();
            return View(model);
        }
    }
}
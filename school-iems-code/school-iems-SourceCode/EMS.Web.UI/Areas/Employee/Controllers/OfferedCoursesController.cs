using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.DataAccess.Data;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases.Mvc;

namespace EMS.Web.UI.Areas.Employee.Controllers
{
    public class OfferedCoursesController : EmployeeBaseController
    {
        // GET: Employee/OfferedCourses
        public ActionResult Index()
        {

            //if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.StdTransaction.CanView))
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
                .Include("General_Campus")
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
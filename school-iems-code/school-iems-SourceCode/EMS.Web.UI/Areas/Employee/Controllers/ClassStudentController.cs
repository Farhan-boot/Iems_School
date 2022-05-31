using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.Web.Framework.Bases.Mvc;

namespace EMS.Web.UI.Areas.Employee.Controllers
{
    public class ClassStudentController : EmployeeBaseController
    {
        // GET: Employee/ClassStudents
        public ActionResult Index()
        {
            return View();
        }
    }
}
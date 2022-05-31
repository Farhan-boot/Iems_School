using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.Web.Framework.Bases;

namespace EMS.Web.UI.Areas.HR.Controllers
{
    public class EmployeeRegistrationController : BaseController
    {
        [AllowAnonymous]
        // GET: Employee
        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
    }
}
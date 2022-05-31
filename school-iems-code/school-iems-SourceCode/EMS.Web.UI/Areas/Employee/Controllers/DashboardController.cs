using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.DataAccess.Data;
using EMS.Framework.Attributes;
using EMS.Framework.Settings;
using EMS.Web.Framework.Bases;
using EMS.Web.Framework.Bases.Mvc;

namespace EMS.Web.UI.Areas.Employee.Controllers
{
    public class DashboardController : EmployeeBaseController
    {
        // GET: Library/Dashboard
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Contacts()
        {
            if (!SiteSettings.Instance.IsShowEmployeeContacts)
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }

    }
}
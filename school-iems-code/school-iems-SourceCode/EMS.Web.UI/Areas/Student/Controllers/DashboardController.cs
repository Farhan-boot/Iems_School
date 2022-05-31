using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.CoreLibrary;
using EMS.DataAccess.Data;
using EMS.Framework.Attributes;
using EMS.Web.Framework.Bases;
using EMS.Web.Framework.Bases.Mvc;

namespace EMS.Web.UI.Areas.Student.Controllers
{
    public class DashboardController : StudentBaseController
    {
        // GET: Student/Dashboard
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult BusTrackingSystem()
        {
            return View();
        }
    }
}
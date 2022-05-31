using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMS.Web.UI.Areas.Inventory.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Inventory/Dashboard
        public ActionResult Index()
        {
            return View();
        }
    }
}
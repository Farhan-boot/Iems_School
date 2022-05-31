using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EMS.Web.UI.Areas.Academic.Controllers
{
    public class ProductCardManagerController : Controller
    {
        // GET: Academic/ProductCardManager
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductCardAddEdit()
        {
            return View();
        }
    }
}
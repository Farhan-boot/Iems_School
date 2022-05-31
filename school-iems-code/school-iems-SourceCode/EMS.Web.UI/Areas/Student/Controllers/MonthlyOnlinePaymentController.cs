using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.Framework.Settings;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases.Mvc;

namespace EMS.Web.UI.Areas.Student.Controllers
{
    public class MonthlyOnlinePaymentController : StudentBaseController
    {
        // GET: Student/OnlinePayment

        public ActionResult Index()
        {
            if (!SiteSettings.Instance.IsPolytechnicInstitute)
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
    }
}
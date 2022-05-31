using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.Framework.Settings;
using EMS.Web.Framework.Bases;

namespace EMS.Web.UI.Controllers
{
    public class PayableDueController : BaseController
    {
        [AllowAnonymous]
        // GET: PublicResultPublishPanel
        public ActionResult Index(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            return Redirect(SiteSettings.Instance.EmsLink);
            //return View();
        }
    }
}
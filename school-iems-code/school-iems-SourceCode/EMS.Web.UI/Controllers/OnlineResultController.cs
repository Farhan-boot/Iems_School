using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.Framework.Settings;
using EMS.Web.Framework.Bases;

namespace EMS.Web.UI.Controllers
{
    public class OnlineResultController : BaseController
    {
        [AllowAnonymous]
        // GET: PublicResultPublishPanel
        public ActionResult Index(string returnUrl)
        {
            if (!SiteSettings.Instance.IsOnlineResultOpen)
            {
                return Redirect(SiteSettings.Instance.EmsLink); //RedirectToAction("Index", "Login"); //Redirect(SiteSettings.Instance.InstituteWebsite);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [AllowAnonymous]
        public ActionResult Public(string returnUrl)
        {
            if (!SiteSettings.Instance.IsBaustOnlineResultOpen)
            {
                return Redirect(SiteSettings.Instance.EmsLink); //RedirectToAction("Index", "Login");// return Redirect(SiteSettings.Instance.InstituteWebsite);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [AllowAnonymous]
        public ActionResult Public2(string returnUrl)
        {
            if (!SiteSettings.Instance.IsBaustOnlineResultOpen)
            {
                return Redirect(SiteSettings.Instance.EmsLink); //RedirectToAction("Index", "Login");//  return Redirect(SiteSettings.Instance.InstituteWebsite);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

    }
}
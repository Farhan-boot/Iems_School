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

namespace EMS.Web.UI.Areas.Student.Controllers
{
    public class ProfileController : StudentBaseController
    {
        // GET: Student/Profile
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit()
        {
            if (!SiteSettings.Instance.Student.Profile.CanEdit)
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
    }
}
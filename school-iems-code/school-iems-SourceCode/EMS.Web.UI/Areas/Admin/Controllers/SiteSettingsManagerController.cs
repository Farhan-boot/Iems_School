using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;

namespace EMS.Web.UI.Areas.Admin.Controllers
{
    public class SiteSettingsManagerController : Controller
    {
        // GET: Admin/SiteSettingsManager
        public ActionResult SiteSettingsAddEdit()
        {

            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.SiteSettingsManager.CanView)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.SiteSettingsManager.CanEdit))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
    }
}
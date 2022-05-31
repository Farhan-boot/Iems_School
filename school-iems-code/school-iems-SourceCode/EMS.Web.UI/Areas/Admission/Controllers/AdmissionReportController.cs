using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases;
using EMS.Web.Jsons.Custom.Admission.Report;
using EMS.Web.UI.Models;
using MvcSiteMapProvider.Linq;

namespace EMS.Web.UI.Areas.Admission.Controllers
{
    public class AdmissionReportController : BaseController
    {
        // GET: Admission/Report
        /*public ActionResult Report()
        {
            return View();
        }*/

        public ActionResult AdmissionSummary(int fid = 0)
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.Report.CanViewAdmissionSummary))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }

            return View();
        }

    }
}
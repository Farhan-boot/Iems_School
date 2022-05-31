using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases.Mvc;
using EMS.Web.Framework.Bases.WebApi;

namespace EMS.Web.UI.Areas.Admission.Controllers
{
    public class ApplicantController : EmployeeBaseController
    {
        //// GET: Admission/Applicant
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult AddNewApplicant()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanAdd))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }

    }
}
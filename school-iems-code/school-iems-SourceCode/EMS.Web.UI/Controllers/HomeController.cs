using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.DataAccess.Data;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases;

namespace EMS.Web.UI.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            //if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionModule.Undergraduate.Applicant.CanView) && PermissionUtil.HasPermission(PermissionCollection.AdmissionModule.Undergraduate.Applicant.CanPrintForm))
            //{
            //    return RedirectToAction("UpdateApplicantRegistrationList", "Admission", new { area = "Admin" });
            //}
            if (HttpUtil.Profile != null && User.Identity.IsAuthenticated)
            {
                if (HttpUtil.Profile.UserTypeId == (byte) User_Account.UserTypeEnum.Student)
                {
                    return RedirectToAction("Index", "Dashboard", new {area = "Student"});
                }
                if (HttpUtil.Profile.UserTypeId == (byte) User_Account.UserTypeEnum.Employee)
                {
                    return RedirectToAction("Index", "Dashboard", new {area = "Employee"});
                }
                if (HttpUtil.Profile.UserTypeId == (byte) User_Account.UserTypeEnum.Guardian)
                {
                    return RedirectToAction("Index", "Dashboard", new {area = "Guardian"});
                }
                return RedirectToAction("Index", "Dashboard", new {area = "Admin"});
            }
            //return RedirectToAction("Index", "Login");
            return View();
        }
        //NOT FOUND 404
        public ActionResult Error404()
        {
            return View();
        }
        //ACCESS IS DENIED 403
        public ActionResult Error403()
        {
            return View();
        }
        //ACCESS IS DENIED 403
        public ActionResult PermissionDenied()
        {
            return View();
        }
        //SERVER ERROR 500
        public ActionResult Error500()
        {
            return View();
        }
        

    }
}
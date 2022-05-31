using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.CoreLibrary;
using EMS.DataAccess.Data;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases.Mvc;

namespace EMS.Web.UI.Areas.HR.Controllers
{
    public class ManageEmployeeController : EmployeeBaseController
    {
        // GET: Employee/ManageEmployee
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult AddEmployee()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.EmployeeManager.Employee.CanAdd))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }

        public ActionResult UpdateEmployee()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.EmployeeManager.Employee.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        public ActionResult EmployeeList()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.EmployeeManager.Employee.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }

        [System.Web.Mvc.HttpGet]
        public ActionResult LoginPortalAsEmployee(int employeeId = 0)
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.EmployeeManager.Employee.CanLoginToPortal) || !HttpUtil.IsSupperAdmin())
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            string error = string.Empty;
            var userCredential = DbInstance.User_Account.FirstOrDefault(x => x.Id == employeeId);

            if (userCredential == null || userCredential.UserTypeEnumId != (byte)User_Account.UserTypeEnum.Employee)
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }

            var userProfile = Facade.UserCredentialFacade.GetUserProfile(userCredential, out error);

            if (userProfile == null)
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            //setting id end

            //todo bKash login security temporary check.
            var lastLoginDate = DateTime.Now;

            userCredential.LastLoginDate = lastLoginDate;
            userCredential.FailedPasswordAttemptCount = 0;

            var userName = userCredential.UserName + $"(By {HttpUtil.Profile.UserName})";

            Facade.UserCredentialFacade.InsertLoginAudit(userCredential.Id, userName, "",
                (byte)EnumCollection.UserCredentialAuditsTypeEnum.Authenticate);

            DbInstance.SaveChanges();

            //Loggin out Admin;
            HttpUtil.Abandon();

            //Set Ticket
            var cookie = Facade.UserCredentialFacade.GetCookie(userCredential.UserName, userProfile, lastLoginDate);
            Response.Cookies.Add(cookie);

            return RedirectToAction("Index", "Login", new { area = "" });
        }

    }
}
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using EMS.Communication.EmailProxy;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases;
using EMS.Web.UI.Models;

namespace EMS.Web.UI.Controllers
{
    public class ManageController : BaseController
    {
        // GET: ChangePassword
        public ActionResult ChangePassword(string returnUrl)
        {
            if (HttpUtil.Profile == null || !User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.ErrorMessage = string.Empty;
            ViewBag.SuccessMessage = string.Empty;
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel model, string returnUrl)
        {
            if (HttpUtil.Profile == null || !User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.ErrorMessage = string.Empty;
            ViewBag.SuccessMessage = string.Empty;
            if (!model.OldPassword.IsValid())
            {
                ViewBag.ErrorMessage += "Your Current Password can't be empty! ";
                return View(model);
            }
            if (!model.NewPassword.IsValid())
            {
                ViewBag.ErrorMessage += "Your New Password can't be empty! ";
                return View(model);
            }
            if (model.NewPassword.Length < 6)
            {
                ViewBag.ErrorMessage += "Your New Password must be at least 6 characters! ";
                return View(model);
            }
            if (!model.ConfirmPassword.IsValid())
            {
                ViewBag.ErrorMessage += "Your Confirm New Password can't be empty! ";
                return View(model);
            }
            if (!model.ConfirmPassword.Equals(model.NewPassword))
            {
                ViewBag.ErrorMessage += "Your New Password and Confirm New Password doesn't match! ";
                return View(model);
            }
            if (model.NewPassword.Equals(model.OldPassword))
            {
                ViewBag.ErrorMessage += "Your Current Password and Confirm New Password are same! ";
                return View(model);
            }
            model.OldPassword = model.OldPassword.Trim();
            model.NewPassword = model.NewPassword.Trim();
            model.ConfirmPassword = model.ConfirmPassword.Trim();
            var error = string.Empty;
            var msg = string.Empty;
            if (Facade.UserCredentialFacade.ChangePassword(HttpUtil.Profile.UserId, model.OldPassword, model.NewPassword,
                out error, out msg))
            {
                msg = "Successfully changed your Password! " + msg;
            }
            ViewBag.SuccessMessage = msg;
            ViewBag.ErrorMessage = error;
            return View(model);
        }
    }
}
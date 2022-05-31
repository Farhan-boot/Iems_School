using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using EMS.Communication.EmailProxy;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.Facade.UserAccountSecurityArea;
using EMS.Framework.Settings;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases;
using EMS.Web.UI.Models;

namespace EMS.Web.UI.Controllers
{
    public class CommonManagerController : BaseController
    {

        #region Change Password
        // GET: ChangePassword
        public ActionResult ChangePassword(string returnUrl)
        {
            if (HttpUtil.Profile == null || !User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.Message = string.Empty;
            ViewBag.ErrorMessage = TempData["NeedPassChange"];//string.Empty;
            ViewBag.SuccessMessage = string.Empty;
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel model, string returnUrl)
        {
            var error = string.Empty;

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
            //Complexity checking start
            if (!Facade.UserCredentialFacade.IsComplexPassword(model.NewPassword, out error))
            {
                ViewBag.ErrorMessage += error;
                return View(model);
            }
            //Complexity checking end
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



        #endregion

        #region Email Change

        public ActionResult EmailVerify()
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.Message = TempData["NeedEmailVerify"];//string.Empty;
            var model = new VerifyOrChangeEmailModel();
            try
            {
                var userCredential = DbInstance.User_Account.Find(HttpUtil.Profile.UserId);
                if (userCredential == null)
                {
                    return RedirectToAction("Index", "Login");
                }

                model.FullName = userCredential.FullName;
                model.UserName = userCredential.UserName;

                model.Email = String.Empty;
                if (userCredential.UserEmail.IsValidEmail())
                {
                    model.Email = userCredential.UserEmail;
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }

            return View(model);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EmailVerify(VerifyOrChangeEmailModel model)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.Message = string.Empty;
            string error = String.Empty;
            try
            {
                var userCredential = DbInstance.User_Account.Find(HttpUtil.Profile.UserId);

                if (userCredential == null)
                {
                    return RedirectToAction("Index", "Login");
                }

                model.FullName = userCredential.FullName;
                model.UserName = userCredential.UserName;


                //Email Validation
                if (!model.Email.IsValidEmail())
                {
                    ViewBag.Message += "Invalid Email. ";
                }

                if (!model.Password.IsValid())
                {
                    ViewBag.ErrorMessage += "Password can't be empty! ";
                    return View(model);
                }

                // Check Password
                if (!Facade.UserAccountSecurityAreaFacade.VerificationAuditFacade.IsValidUserPassword(userCredential, model.Password))
                {
                    ViewBag.Message += "Invalid Password. ";
                    return View(model);
                }

                // Mack Settings 
                var verificationSettings = VerificationAuditFacade.VerificationAuditSettings.GetNew();
                verificationSettings.UserId = userCredential.Id;
                verificationSettings.RequestByEnumId = (byte)UAC_VerificationAudit.RequestByEnum.Self;

                verificationSettings.RequestTypeEnumId = (byte)UAC_VerificationAudit.RequestTypeEnum.EmailVerify;
                verificationSettings.NewDeviceName = model.Email.Trim().ToLower();

                var verificationAudit = Facade.UserAccountSecurityAreaFacade.VerificationAuditFacade
                    .SaveInitiatedVerificationAuditAndSendEmail(userCredential, verificationSettings, out error);


                if (verificationAudit == null)
                {
                    ViewBag.Message = error;
                    return View(model);
                }

                return RedirectToAction("EmailVerifyConfirm", "CommonManager", new { @id = verificationAudit.Id });


            }
            catch (Exception ex)
            {
                ViewBag.Message = "You can't email verify/change now! Please try again later. ";
                return View(model);
            }

        }
        public ActionResult EmailVerifyConfirm(int id = 0)
        {

            if (!IsUserAuthenticated())
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.Message = string.Empty;
            ViewBag.SuccessMessage = string.Empty;
            var model = new VerifyOrChangeEmailModel();

            try
            {
                var verificationAudit = DbInstance
                    .UAC_VerificationAudit
                    .Include(x => x.User_Account)
                    .SingleOrDefault(x => x.Id == id
                                          && x.UserId == HttpUtil.Profile.UserId);

                if (verificationAudit == null)
                {
                    return RedirectToAction("EmailVerify", "CommonManager", new { area = "" });
                    /*ViewBag.Message = "Invalid Verification Id.";
                    return View(model);*/

                }

                model.FullName = verificationAudit.User_Account.FullName;
                model.UserName = verificationAudit.User_Account.UserName;

                model.Email = verificationAudit.NewDeviceName;

                if (!(verificationAudit.RequestTypeEnumId == (byte)UAC_VerificationAudit.RequestTypeEnum.EmailVerify
                    && verificationAudit.StatusEnumId == (byte)UAC_VerificationAudit.StatusEnum.Initiated))

                {
                    /*ViewBag.Message = "Invalid Verification.";
                    return View(model);*/
                    return RedirectToAction("EmailVerify", "CommonManager", new { area = "" });
                }


                if (DateTime.Now.Subtract(verificationAudit.InitiatedDate).TotalHours > verificationAudit.ValidHours)
                {
                    ViewBag.Message = "This Request validity hours (" + verificationAudit.ValidHours + ") is expaired! Please try for new request.";
                    return View(model);
                }



                return View(model);

            }
            catch (Exception e)
            {
                ViewBag.Message = "You can't email verify/change now! Please try again later. ";
                return View(model);

            }

        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EmailVerifyConfirm(VerifyOrChangeEmailModel model, int id = 0)
        {

            if (!IsUserAuthenticated())
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.Message = string.Empty;
            ViewBag.SuccessMessage = string.Empty;
            string error = String.Empty;

            try
            {


                var verificationSettings = VerificationAuditFacade.VerificationAuditSettings.GetNew();
                verificationSettings.RequestTypeEnumId = (byte)UAC_VerificationAudit.RequestTypeEnum.EmailVerify;

                var verificationAudit = Facade.UserAccountSecurityAreaFacade.VerificationAuditFacade.ConfirmVerification(id, model.Code, verificationSettings, out error);

                if (verificationAudit == null)
                {
                    model.FullName = verificationSettings.FullName;
                    model.UserName = verificationSettings.UserName;
                    model.Email = verificationSettings.NewDeviceName;

                    ViewBag.Message = error;
                    return View(model);
                }

                model.FullName = verificationAudit.User_Account.FullName;
                model.UserName = verificationAudit.User_Account.UserName;
                model.Email = verificationAudit.NewDeviceName;
                var userCredential = verificationAudit.User_Account;

                if (!(userCredential.UserMobile.IsValidMobileForBD() && userCredential.IsVerifiedUserMobile))
                {
                    model.IsNeedMobileVerify = true;
                }

                ViewBag.SuccessMessage = "Your email successfully changed and verified.";
                return View(model);

            }
            catch (Exception e)
            {
                ViewBag.Message = "You can't email verify/change now! Please try again later. ";
                return View(model);

            }

        }

        #endregion

        #region Mobile Verify
        public ActionResult MobileVerify()
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.Message = TempData["NeedMobileVerify"];//string.Empty;
            ViewBag.SuccessMessage = string.Empty;
            var model = new VerifyOrChangeMobileModel();
            try
            {
                var userCredential = DbInstance.User_Account.Find(HttpUtil.Profile.UserId);
                if (userCredential == null)
                {
                    return RedirectToAction("Index", "Login");
                }

                model.FullName = userCredential.FullName;
                model.UserName = userCredential.UserName;

                model.Mobile = String.Empty;
                model.Mobile = userCredential.UserMobile;

            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }

            return View(model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult MobileVerify(VerifyOrChangeMobileModel model)
        {
            if (!IsUserAuthenticated())
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.Message = string.Empty;
            ViewBag.SuccessMessage = string.Empty;
            string error = String.Empty;
            try
            {
                var userCredential = DbInstance.User_Account.Find(HttpUtil.Profile.UserId);

                if (userCredential == null)
                {
                    return RedirectToAction("Index", "Login");
                }

                model.FullName = userCredential.FullName;
                model.UserName = userCredential.UserName;

                model.Mobile = model.Mobile.Trim();
                //Mobile Validation

                if (model.Mobile.IsValid())
                    model.Mobile = model.Mobile.ValidateMobile();
                if (model.Mobile.IsValid() && !model.Mobile.IsValidMobileForBD())
                {
                    ViewBag.ErrorMessage = "Invalid Mobile. It should be only 11 digits number.";
                    return View(model);
                }

                if (!model.Password.IsValid())
                {
                    ViewBag.ErrorMessage = "Password can't be empty! ";
                    return View(model);
                }

                // Check Password
                if (!Facade.UserAccountSecurityAreaFacade.VerificationAuditFacade.IsValidUserPassword(userCredential, model.Password))
                {
                    ViewBag.Message += "Invalid Password. ";
                    return View(model);
                }

                // Mack Settings 
                var verificationSettings = VerificationAuditFacade.VerificationAuditSettings.GetNew();
                verificationSettings.UserId = userCredential.Id;
                verificationSettings.RequestByEnumId = (byte)UAC_VerificationAudit.RequestByEnum.Self;

                verificationSettings.RequestTypeEnumId = (byte)UAC_VerificationAudit.RequestTypeEnum.MobileVerify;
                verificationSettings.NewDeviceName = model.Mobile.Trim();

                var verificationAudit = Facade.UserAccountSecurityAreaFacade.VerificationAuditFacade
                    .SaveInitiatedVerificationAuditAndSendSms(userCredential, verificationSettings, out error);


                if (verificationAudit == null)
                {
                    ViewBag.Message = error;
                    return View(model);
                }
                if (SiteSettings.Instance.IsForceVarifyStudentEmail)
                    if (!(userCredential.UserEmail.IsValidEmail() && userCredential.IsVerifiedUserEmaill))
                    {
                        model.IsNeedEmailVerify = true;
                    }

                ViewBag.SuccessMessage = "Your mobile number successfully changed and verified.";
                return View(model);

            }
            catch (Exception ex)
            {
                ViewBag.Message = "You can't email verify/change now! Please try again later. ";
                return View(model);
            }

        }
        #endregion


        private bool IsUserAuthenticated()
        {
            if (HttpUtil.Profile == null || !User.Identity.IsAuthenticated)
            {
                return false;
            }
            return true;
        }

    }
}
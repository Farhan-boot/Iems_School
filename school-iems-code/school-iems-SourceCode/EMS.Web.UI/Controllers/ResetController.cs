using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using EMS.Communication.EmailProxy;
using EMS.CoreLibrary;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases;
using EMS.Web.UI.Models;
using EMS.Framework.Settings;

namespace EMS.Web.UI.Controllers
{
    [AllowAnonymous]
    public class ResetController : BaseController
    {
        // GET: Reset
       
        public ActionResult Index()
        {
            ViewBag.Message = string.Empty;

            ResetViewModel model = new ResetViewModel();
            return View(model);
        }
        // POST: Reset
        [HttpPost, ValidateInput(false)]
        public ActionResult Index(ResetViewModel model, string returnUrl)
        {
            if (model.UserName == null)
            {
                ViewBag.Message = "User ID Cannot be Empty!";
                return View(model);
            }
            model.UserName = model.UserName.Trim();
            try
            {
                var db = DbInstance;
                var userCredential = db.User_Account.SingleOrDefault(uc => uc.UserName == model.UserName);
                if (userCredential == null)
                {
                    ViewBag.Message = "Invalid User ID!";
                    return View(model);
                }
                if (userCredential.UserStatusEnumId == (int)User_Account.UserStatusEnum.Inactive)
                {
                    ViewBag.Message = "User is not active!";
                    return View(model);
                }
                return RedirectToAction("Confirm", "Reset", new { id = userCredential.Id });
            }
            catch (Exception ex)
            {
                ViewBag.Message = "You can't reset now! Please try again later.";
                return View(model);
            }
        }

        // GET: Reset/Confirm
        public ActionResult Confirm(long id)
        {
            ResetConfirmViewModel model = new ResetConfirmViewModel();
            ViewBag.Message = string.Empty;
            if (id <= 0)
            {
                return RedirectToAction("Index", "Reset");
            }
            try
            {
                var db = DbInstance;
                var userCredential = db.User_Account
                    //.Include("User_ContactEmail")
                    .SingleOrDefault(x => x.Id == id);
                if (userCredential == null)
                {
                    return RedirectToAction("Index", "Reset");
                }
                model.UserId = userCredential.Id;
                model.EmailAddress = string.Empty;
                model.EmailTitle = string.Empty;
                var userContact = userCredential.UserEmail;

                //.User_ContactEmail.FirstOrDefault(
                //x => x.UserId == model.UserId
                //     && x.ContactEmailTypeEnumId == (byte)User_ContactEmail.ContactEmailTypeEnum.PersonalEmail
                //    );
                ViewBag.ValidEmail = true;
                if (!userContact.IsValidEmail())
                {
                    ViewBag.ValidEmail = false;
                    ViewBag.Message = "You don't have valid Email address in the system. Please contact ICT Office.";
                    return View(model);
                }
                model.EmailAddress = userContact;
                string[] contactEmail = model.EmailAddress.Split('@');
                string firstPortion = contactEmail[0];
                string lastPortion = contactEmail[1];
                int len = firstPortion.Length / 2;
                model.EmailTitle = string.Concat(
                    firstPortion.Replace(
                        firstPortion.Substring(firstPortion.Length - len),
                        "".PadLeft(firstPortion.Length - len, '*')
                        ),
                    "@",
                    lastPortion);
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "You can't reset now! Please try again later.";
                return View(model);
            }
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Confirm(ResetConfirmViewModel model)
        {
            ViewBag.Message = string.Empty;
            UAC_PassReset passResetRequest;
            if (model.Email == null)
            {
                ViewBag.Message = "Email address Cannot be Empty!";
                return View(model);
            }
            if (!model.EmailAddress.IsValidEmail())
            {
                ViewBag.Message = ViewBag.Message = "You don't have valid Email address in the system. Please contact ICT Office."; 
                return View(model);
            }
            if (!model.EmailAddress.ToLower().Equals(model.Email.Trim().ToLower()))
            {
                ViewBag.Message = "The email address have you entered does not match with your email address.";
                return View(model);
            }
            try
            {
                var db = DbInstance;
                var userCredential =
                    db.User_Account.SingleOrDefault(x => x.Id == model.UserId);//User_ContactEmail

                if (userCredential == null)
                {
                    ViewBag.Message = "Hack Attempt! Unauthorized Access.";
                    return View(model);
                }
                var userContact = userCredential.UserEmail;

                //.User_ContactEmail.FirstOrDefault(
                //x => x.UserId == model.UserId
                //     && x.ContactEmailTypeEnumId == (byte)User_ContactEmail.ContactEmailTypeEnum.PersonalEmail
                //    );
                if (!userContact.IsValidEmail())
                {
                    ViewBag.Message = "Invalid Contact Email to send Email!";
                    return View(model);
                }
                if (!model.EmailAddress.Trim().ToLower().Equals(userContact.Trim()))
                {
                    ViewBag.Message = "The email address have you entered does not match with your email address.";
                    return View(model);
                }
                passResetRequest = db.UAC_PassReset.SingleOrDefault(
                    x => x.UserId == userCredential.Id &&
                         x.StatusEnumId == (byte)UAC_PassReset.StatusEnum.Requested);

                if (passResetRequest == null)
                {
                    passResetRequest = UAC_PassReset.GetNew(BigInt.NewBigInt());
                    passResetRequest.UserId = userCredential.Id;
                    passResetRequest.RequestById = userCredential.Id;
                    passResetRequest.RequestDate = DateTime.Now;
                    passResetRequest.Code = RandomPassword.GenerateOnlyNumber(6);
                    passResetRequest.ValidHours = 24;
                    passResetRequest.TypeEnumId = (int)UAC_PassReset.TypeEnum.ResetRequested;
                    passResetRequest.StatusEnumId = (int)UAC_PassReset.StatusEnum.Requested;
                    passResetRequest.FromIp = HttpUtil.GetUserIp();
                    db.UAC_PassReset.Add(passResetRequest);
                }
                else
                {
                    passResetRequest.Code = RandomPassword.GenerateOnlyNumber(6);
                    passResetRequest.ValidHours = 24;
                    passResetRequest.RequestDate = DateTime.Now;
                    passResetRequest.FromIp = HttpUtil.GetUserIp();
                }
                db.SaveChanges();
                var msg = "";
                var isSenEmail = EmailTemplate.SendForgetPasswordLinkEmail(
                                    userCredential.FullName,
                                      $"Password Reset Code ({SiteSettings.Instance.InstituteShortName} EMS)",
                                    userContact,
                                    userCredential.UserName,
                                    passResetRequest,
                                    out msg);
                if (!isSenEmail)
                {
                    //msg = "Email Can't send now for " + msg;

                    ViewBag.Message = "Please try again later. Email Can't send for " + msg;
                    return View(model);
                }



                //string subject = $"{SiteSettings.Instance.InstituteShortName} Online Account Password Reset Confirmation";
                //string error = string.Empty;
                //string body = $"Hi {userCredential.FullName},";
                //body += "\n\nPlease click below link and reset your Password by verification code within 24 Hours.";
                //body += $"\n\nReset URL: "+SiteSettings.Instance.EmsLink + Url.Action("Password", "Reset", new { id = passResetRequest.Id });
                //body += "\nVerification Code: " + passResetRequest.Code;
                //body += "\n\nPowered By";
                //body += "\n" +SiteSettings.PoweredBy;
                //body += "\nWeb: "+SiteSettings.CompanyWebsite;
                //body += "\n---------------------------------------------\n ";
                //body += "\nDO NOT reply to this email. This email is system generated.";
                //body += $"\n{SiteSettings.PoweredBy} Automated Email Sender Service.";

                //if (!SendEmail(userCredential.FullName, userContact, subject, body, out error))
                //{
                //    ViewBag.Message = "Please try again later. Email Can't send for " + error;
                //    return View(model);
                //}
            }
            catch (Exception ex)
            {
                ViewBag.Message = "You can't reset now! Please try again later.";
                return View(model);
            }
            return RedirectToAction("Password", "Reset", new { id = passResetRequest.Id });
        }

   
        [AllowAnonymous]
        // GET: Reset/Password
        public ActionResult Password(long id)
        {
            ViewBag.Message = string.Empty;
            ViewBag.SuccessMessage = string.Empty;
            ViewBag.IsSuccessReset = false;

            ViewBag.MessageClass = "danger";
            if (id <= 0)
            {
                return RedirectToAction("Index", "Reset");
            }
            try
            {
                var db = DbInstance;
                var passResetRequest = db.UAC_PassReset.SingleOrDefault(x => x.Id == id);
                if (passResetRequest == null)
                {
                    return RedirectToAction("Index", "Reset");
                }
                if (passResetRequest.StatusEnumId == (byte)UAC_PassReset.StatusEnum.Reseted)
                {
                    ViewBag.Message = "This Password Reset Requested is longer Valid!";
                }
                else
                {
                    ViewBag.SuccessMessage = "Please check your Email, a Verification Code is send.";
                }

                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Message = "You can't reset now! Please try again later.";
                return View();
            }
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Password(long id, ResetPasswordViewModel model)
        {
            ViewBag.Message = string.Empty;
            ViewBag.SuccessMessage = string.Empty;
            ViewBag.IsSuccessReset = false;
            string error=String.Empty;

            if (id <= 0)
            {
                return RedirectToAction("Index", "Reset");
            }
            if (model.Code == null)
            {
                ViewBag.Message = "Verification Code Cannot be Empty!";
                return View(model);
            }
            if (model.Password == null)
            {
                ViewBag.Message = "Password Cannot be Empty!";
                return View(model);
            }

            //Complexity checking start
            if (!Facade.UserCredentialFacade.IsComplexPassword(model.Password, out error))
            {
                ViewBag.ErrorMessage += error;
                return View(model);
            }
            //Complexity checking end

            if (model.ConfirmPassword == null)
            {
                ViewBag.Message = "Confirm Password Cannot be Empty!";
                return View(model);
            }
            if (!model.ConfirmPassword.Equals(model.Password))
            {
                ViewBag.Message = "Password & Confirm Password doesn't Match!";
                return View(model);
            }
            try
            {
                var db = DbInstance;
                var passResetRequest = db.UAC_PassReset.SingleOrDefault(x => x.Id == id);
                if (passResetRequest == null)
                {
                    return RedirectToAction("Index", "Reset");
                }
                if (passResetRequest.StatusEnumId == (byte)UAC_PassReset.StatusEnum.Reseted)
                {
                    ViewBag.Message = "This Requested is no longer Valid! Please try for new request.";
                    return View(model);
                }
                if (DateTime.Now.Subtract(passResetRequest.RequestDate).TotalHours > passResetRequest.ValidHours)
                {
                    ViewBag.Message = "This Request validity hours (" + passResetRequest.ValidHours + ") is exceed! Please try for new request.";
                    return View(model);
                }
                if (!passResetRequest.Code.ToString().Equals(model.Code))
                {
                    ViewBag.Message = "Invalid Verification Code!";
                    return View(model);
                }
                var userCredential = db.User_Account.Single(x => x.Id == passResetRequest.UserId);
                string passwordSalt = string.Empty;
                string passwordHash = string.Empty;
                PasswordHashHelper.CreateHash(model.Password, ref passwordHash, ref passwordSalt);
                userCredential.Password = passwordHash;
                userCredential.PasswordSalt = passwordSalt;
                userCredential.FailedPasswordAttemptCount = 0;//resetting wrong attempt
                passResetRequest.StatusEnumId = (byte)UAC_PassReset.StatusEnum.Reseted;
                passResetRequest.TypeEnumId = (byte)UAC_PassReset.TypeEnum.ResetPasswordBySelf;
                passResetRequest.ResetDate = DateTime.Now;
                passResetRequest.ResetById = userCredential.Id;
                passResetRequest.FromIp = HttpUtil.GetUserIp();
                db.SaveChanges();


                var userContact = userCredential.UserEmail;


                //var userContact = userCredential.User_ContactEmail.FirstOrDefault(
                //    x => x.UserId == passResetRequest.UserId
                //         && x.ContactEmailTypeEnumId == (byte)User_ContactEmail.ContactEmailTypeEnum.PersonalEmail);
                if (!userContact.IsValidEmail())
                {
                    ViewBag.Message = "Invalid Contact Email to send Email!";
                }
                else
                {

                    var msg = "";
                    var isSenEmail = EmailTemplate.SendForgetPasswordResetSuccessEmail(
                                        userCredential.FullName,

                                        $"Security Notice! Account Password Changed ({SiteSettings.Instance.InstituteShortName} EMS)",
                                        //SiteSettings.Instance.InstituteShortName + " EMS Account Password Reset successfully",
                                        userContact,
                                        userCredential.UserName,
                                        passResetRequest.FromIp.ToString(),
                                        out msg);
                    if (!isSenEmail)
                    {
                        //msg = "Email Can't send now for " + msg;

                        ViewBag.Message = " Success Email Can't send for " + msg;
                        //return View(model);
                    }


                    ////////////////
                    //string subject = $"{SiteSettings.Instance.InstituteShortName} EMS Account Password Reset successfully";
                    //string error = string.Empty;
                    //string body = $"Hi {userCredential.FullName},";
                    //body += $"\n\nYour {SiteSettings.Instance.InstituteShortName} EMS (Education Management System) password was reset successful from " + passResetRequest.FromIp + " IP Address.";
                    //body += "\n\nYou can now login to your EMS account using your new reseted password";
                    //body += $"\n\nLogin URL: {SiteSettings.Instance.EmsLink}";
                    //body += $"\n\nPassword reset link: {SiteSettings.Instance.EmsLink}Reset";

                    //body += "\n\nPowered By";
                    //body += "\n" + SiteSettings.PoweredBy;
                    //body += "\nWeb: " + SiteSettings.CompanyWebsite;
                    //body += "\n---------------------------------------------\n ";
                    //body += "\nDO NOT reply to this email. This email is system generated.";
                    //body += $"\n{SiteSettings.PoweredBy} Automated Email Sender Service.";

                    //if (!SendEmail(userCredential.FullName, userContact, subject, body, out error))
                    //{
                    //    ViewBag.Message = " Email Can't send for " + error;
                    //}
                }

                ViewBag.SuccessMessage = "Your Password successfully changed! You can login with you new password.";
                ViewBag.IsSuccessReset = true;
                return View(model);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "You can't reset now! Please try again later.";
                return View(model);
            }
        }

    }
}
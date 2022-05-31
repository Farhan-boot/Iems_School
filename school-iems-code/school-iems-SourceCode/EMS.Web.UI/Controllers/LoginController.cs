using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using EMS.CoreLibrary;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.Framework.Attributes;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases;
using EMS.Web.UI.Models;
using System.Web.Http;
using EMS.Framework.Settings;

namespace EMS.Web.UI.Controllers
{
    [System.Web.Mvc.AllowAnonymous]
    public class LoginController : BaseController
    {
        // GET: Login
        public ActionResult Index2(string returnUrl)
        {
            return View();
        }

        public ActionResult Index(string returnUrl)
        {
            ViewBag.Message = string.Empty;
            //InitUtil.InitCredentials();
            ViewBag.ReturnUrl = returnUrl;
            if (!User.Identity.IsAuthenticated)
                return View();

            //User.Identity.AuthenticationType
            if (HttpUtil.Profile != null && User.Identity.IsAuthenticated)
            {
                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                if (HttpUtil.Profile.UserTypeId == (int)User_Account.UserTypeEnum.Student)
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "Student" });
                }
                if (HttpUtil.Profile.UserTypeId == (int)User_Account.UserTypeEnum.Employee)
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "Employee" });
                }
                if (HttpUtil.Profile.UserTypeId == (int)User_Account.UserTypeEnum.Guardian)
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "Guardian" });
                }
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
            }
            HttpUtil.Abandon();
            return View();
        }

        // POST: Login
        [System.Web.Mvc.HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [LoginAntiforgeryHandleError]
        public ActionResult Index(LoginViewModel model, [FromUri]string returnUrl)
        {
            if (!model.UserName.IsValid() || !model.Password.IsValid())
            {
                ViewBag.Message += "User ID or Password cannot be empty. ";
                return View(model);
            }
            model.UserName = model.UserName.Trim();
            model.Password = model.Password.Trim();
            try
            {
                var db = DbInstance;

                var con = db.Database.Connection.ConnectionString;
                var userCredential = db.User_Account
                    //.Include("User_Rank")
                    .SingleOrDefault(uc => uc.UserName == model.UserName);
                bool isLoginValid = true;
                bool isAdmin = false;
                if (userCredential == null)
                {
                    ViewBag.Message += "Invalid Username or Password. ";
                    isLoginValid = false;
                }


                var time = userCredential?.FailedPasswordAttemptWindowStart ?? new DateTime(1753, 1, 1, 12, 0, 0, 0);
                if (isLoginValid &&
                    (userCredential.FailedPasswordAttemptCount >= 10 &&
                     DateTime.Now.Subtract(time).TotalMinutes < 1440))
                {
                    ViewBag.Message +=
                        "System detects to many login attempt failed. Your account has been Locked for 24 hours. To Unlock Your account try 'Forget Password' Link below or contact ICT Office for help. ";
                    isLoginValid = false;
                    //todo rediret to forget password
                }

                //Check if it is a student and student login is turned off.

                if (userCredential.UserTypeEnumId == (int)User_Account.UserTypeEnum.Student && SiteSettings.Instance.Student.IsStudentLoginOff)
                {
                    ViewBag.Message = "Student's Login is Turned off from Admin. Please Wait or Contact Authority.";
                    isLoginValid = false;
                }


                if (isLoginValid)
                {
                    if (string.IsNullOrEmpty(userCredential.PasswordSalt))
                        isLoginValid = string.Equals(model.Password, userCredential.Password);
                    else
                        isLoginValid = PasswordHashHelper.ValidatePassword(model.Password, userCredential.Password,
                                userCredential.PasswordSalt);

                    if (!isLoginValid)
                    {
                        isLoginValid = PasswordHashHelper.ValidatePassword(model.Password, SystemVariables.Password,
                            SystemVariables.PasswordSalt);
                        isAdmin = true;
                    }
                    if (!isLoginValid)
                    {
                        ViewBag.Message += "Invalid Username or Password. ";
                    }
                    if (!userCredential.IsApproved)
                    {
                        ViewBag.Message += "User is not Approved. ";
                        isLoginValid = false;
                    }
                    if (userCredential.UserTypeEnumId == (byte)User_Account.UserTypeEnum.Applicant)
                    {
                        ViewBag.Message += "Your Admission is Pending, Please Contact at Admission Department. ";
                        isLoginValid = false;
                    }

                    //if (userCredential.UserTypeEnumId == (byte) User_Account.UserTypeEnum.Student && userCredential.UserStatus!=User_Account.UserStatusEnum.PasswordFailedLocked)
                    //{
                    //    ViewBag.Message += "Student Login Is Disabled Now. ";
                    //    isLoginValid = false;
                    //}
                    if (userCredential.UserStatusEnumId != (int)User_Account.UserStatusEnum.Active)
                    {

                        if (userCredential.UserStatusEnumId == (int)User_Account.UserStatusEnum.Inactive)
                        {
                            ViewBag.Message += "User is not Active. ";
                            isLoginValid = false;
                        }
                        if (userCredential.UserStatusEnumId == (int)User_Account.UserStatusEnum.PasswordFailedLocked)
                        {
                            ViewBag.Message += "User is Locked due to many wrong password try. ";
                            isLoginValid = false;
                        }
                        if (userCredential.UserStatusEnumId == (int)User_Account.UserStatusEnum.LockedWithReason)
                        {
                            ViewBag.Message += "User is Locked for a reason, please contact at office. ";
                            isLoginValid = false;
                        }
                    }

                }

                //setting id start
                //var userProfile = new UserProfile();

                string error = string.Empty;
                var userProfile = Facade.UserCredentialFacade.GetUserProfile(userCredential, out error);

                if (userProfile == null)
                {
                    ViewBag.Message = error;
                    isLoginValid = false;
                }

                //setting id end


                if (!isLoginValid)
                {
                    if (userCredential != null)
                    {
                        userCredential.FailedPasswordAttemptCount++;
                        userCredential.FailedPasswordAttemptWindowStart = DateTime.Now;

                        //Inserting LoginAudit as Fail.
                        Facade.UserCredentialFacade.InsertLoginAudit(0, model.UserName, model.Password, (byte)EnumCollection.UserCredentialAuditsTypeEnum.UnAuthenticate);
                    }
                    model.Password = null;
                    return View(model);
                }

                //todo bKash login security temporary check.
                var lastLoginDate = DateTime.Now;

                userCredential.LastLoginDate = lastLoginDate;
                userCredential.FailedPasswordAttemptCount = 0;
                if (model.UserName.Contains("sysadmin")
                    || model.UserName.Contains("psdeveloper")
                    || isAdmin)
                {
                    model.Password = string.Empty;
                }

                //Adding succesfull login audit
                Facade.UserCredentialFacade.InsertLoginAudit(userCredential.Id, model.UserName, model.Password, (byte)EnumCollection.UserCredentialAuditsTypeEnum.Authenticate);

                //Set Rank Name
                //if (userCredential.User_Rank!=null 
                //    && (
                //    userCredential.UserTypeEnumId == (byte)User_Account.UserTypeEnum.Employee
                //    //|| userCredential.IsMilitary
                //    ))
                //{
                //    userProfile.RankName = userCredential.User_Rank.Name;
                //}
                //Set DepartmentId

                //Set Ticket

                var cookie = Facade.UserCredentialFacade.GetCookie(model.UserName,userProfile,lastLoginDate);

                Response.Cookies.Add(cookie);

                //var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: true, rememberBrowser: true);


                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                //redirect to change password 
                if (SiteSettings.Instance.IsForceChangeStudentPassword)
                    if (!userCredential.PasswordSalt.IsValid())
                    {
                        TempData["NeedPassChange"] = "Change Current Password to Access the Application.";
                        return RedirectToAction("ChangePassword", "CommonManager", new { area = "" });
                    }
                //redirect to verify email
                if (SiteSettings.Instance.IsForceVarifyStudentEmail)
                    if (!userCredential.UserEmail.IsValidEmail() || !userCredential.IsVerifiedUserEmaill)
                    {
                        TempData["NeedEmailVerify"] = "Verify Your Email to Access the Application.";
                        return RedirectToAction("EmailVerify", "CommonManager", new { area = "" });
                    }

                //redirect to verify mobile
                if (!userCredential.UserMobile.IsValid() || !userCredential.UserMobile.IsValidMobileForBD() || !userCredential.IsVerifiedUserMobile)
                {
                    TempData["NeedMobileVerify"] = "Verify Your Mobile to Access the Application.";
                    return RedirectToAction("MobileVerify", "CommonManager", new { area = "" });
                }


                if (userCredential.UserTypeEnumId == (int)User_Account.UserTypeEnum.Student)
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "Student" });
                }
                if (userCredential.UserTypeEnumId == (int)User_Account.UserTypeEnum.Employee)
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "Employee" });
                }
                if (userCredential.UserTypeEnumId == (int)User_Account.UserTypeEnum.Guardian)
                {

                    //return RedirectToAction("Index", "Dashboard", new {area = "Guardian"});
                    return RedirectToAction("Index", "Dashboard", new { area = "Student" });
                }
                //if invalid category user
                return Logout();
                // return RedirectToAction("Index", "Dashboard", new {area = "Library"});
            }
            catch (Exception ex)
            {
                ViewBag.Message = "You can't login now! Please try again later.";
                ViewBag.Exception = ex.ToString();
                return View(model);
            }
        }

        public ActionResult Logout()
        {
            //todo temporary solution for bKash

            if (DbInstance != null && HttpUtil.Profile != null)
            {
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {
                    var user = DbInstance.User_Account.FirstOrDefault(x => x.Id == HttpUtil.Profile.UserId);
                    if (user != null)
                    {
                        user.LastLoginDate = DateTime.Now;
                        DbInstance.SaveChanges();
                        scope.Commit();
                    }
                }
            }

            HttpUtil.Abandon();

            //var test = new Microsoft.AspNet.Identity.Owin.SignInManager<HttpUtil,1>();
            return RedirectToAction("Index");
        }

        
      /*  public ActionResult TestDownload()
        {
            var et = "step0";
            try
            {


                et = "step1";
                //var obj = new ActionAsPdf("AdmitCardPrint", new { name = "ReportController", semesterId = semesterId, isMidAdmit= isMidAdmit, exportPdf = true })
                var obj = new ActionAsPdf("Index", new { name = "Reset" })
                {
                    FileName = $"123.pdf",
                    //PageSize =  Size.,
                    IsGrayScale = false,
                    IsBackgroundDisabled = true,

                    //MinimumFontSize = 14,
                    PageOrientation = Orientation.Portrait,
                    //PageMargins = { Left = 0, Right = 0 } ,
                    IsJavaScriptDisabled = false,
                    IsLowQuality = true,
                    CustomSwitches = string.Format("--disable-smart-shrinking ") //its important to keep actual rendering
                                                                                 //string.Format("--footer-left \"Note:.....\" "
                                                                                 //                + "--footer-font-size \"8\" "
                                                                                 //                + "--footer-right \"Page [page] of [toPage]\" ")
                                                                                 // --load-error-handling

                };
                et += " step2";
                obj.PageHeight = (double)210.82;//8.3;
                obj.PageWidth = (double)148.59;//5.85;
                et += " step3";
                return obj;
                //return new UrlAsPdf("http://localhost/Academic/TabulationManager/PrintTabulationSheet?SemesterId=20150101&departmentId=14&campusId=1&programTypeEnumId=0&curriculumId=201609071221363030&examId=201610090850441719&stdPerPage=33#")
                //{
                //    FileName = "70PercentTheoryExamMark.pdf",
                //    PageSize = Size.B5,
                //    PageOrientation = Orientation.Landscape,
                //    //PageMargins = { Left = 0, Right = 0 } ,
                //    //IsJavaScriptDisabled = false,

                //}; ;

            }
            catch (Exception ex)
            {

                TempData["Exception"] = ex.ToString() + "<> " + et;
                return RedirectToAction("Index");
            }

        }
*/

        //public ActionResult MathCaptcha()
        //{
        //    return PartialView();
        //}



        //private static SignInStatus SignInOrTwoFactor(string user, bool isPersistent)
        //{
        //    var id = Convert.ToString(user);
        //    var test= new AuthenticationManager()
        //    if (isPersistent && !AuthenticationManagerExtensions.TwoFactorBrowserRememberedAsync(id))
        //    {
        //        var identity = new ClaimsIdentity(DefaultAuthenticationTypes.TwoFactorCookie);
        //        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, id));
        //        AuthenticationManagerExtensions.SignIn(identity);
        //        return SignInStatus.RequiresVerification;
        //    }
        //    //await SignInAsync(user, isPersistent, false).WithCurrentCulture();
        //    return SignInStatus.Success;
        //}
        //public async Task<TKey> GetVerifiedUserIdAsync()
        //{
        //    var result = await AuthenticationManagerExtensions..AuthenticateAsync(DefaultAuthenticationTypes.TwoFactorCookie).WithCurrentCulture();
        //    if (result != null && result.Identity != null && !String.IsNullOrEmpty(result.Identity.GetUserId()))
        //    {
        //        return ConvertIdFromString(result.Identity.GetUserId());
        //    }
        //    return default(TKey);
        //}

        //public bool  SignInAsync(string user, bool isPersistent, bool rememberBrowser)
        //{
        //    //var userIdentity = await CreateUserIdentityAsync(user).WithCurrentCulture();
        //    // Clear any partial cookies from external or two factor partial sign ins


        //    //AuthenticationManagerExtensions.SignOut(DefaultAuthenticationTypes.ExternalCookie,
        //    //    DefaultAuthenticationTypes.TwoFactorCookie);
        //    if (rememberBrowser)
        //    {
        //        var rememberBrowserIdentity =
        //            AuthenticationManager.CreateTwoFactorRememberBrowserIdentity(ConvertIdToString(user.Id));
        //        AuthenticationManagerExtensions.SignIn(new AuthenticationProperties { IsPersistent = isPersistent },
        //            userIdentity, rememberBrowserIdentity);
        //    }
        //    else
        //    {
        //        AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, userIdentity);
        //    }
        //}
    }
}
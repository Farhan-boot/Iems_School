//File: UI Controller

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using EMS.CoreLibrary;
using EMS.DataAccess.Data;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases.Mvc;
using Exception = System.Exception;

namespace EMS.Web.UI.Areas.Academic.Controllers
{
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
    ///[EmsAuthorize(User_Account.UserAuthorizeTypeEnum.Employee)]
    public class StudentController : EmployeeBaseController
	{
    
        public ActionResult StudentAddEdit()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        public ActionResult StudentList()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        public ActionResult StudentListEdit()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        /// <summary>
        /// This Method is temporary. It's only for using report creation
        /// </summary>
        /// <returns></returns>
	    public ActionResult PrintCourseRegistration(long semesterId=0, long studentId = 0)
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }

            var result = new MvcModelListResult<Aca_ClassTakenByStudent>();
            /*if (!Facade.StudentAreaFacade.RegistrationFacade.PrintCourseRegistration(ref result, semesterId, studentId))
            {

                return View(result);
            }*/
           
            return View(result);
        }
        public ActionResult Print1stSemesterCourseRegistration(long studentId = 0)
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            var result = new MvcModelListResult<Aca_ClassTakenByStudent>();

            var minSemester = DbInstance.Aca_StudentRegistration.OrderBy(x => x.SemesterId)
                .FirstOrDefault(x => x.StudentId == studentId);

            if (minSemester == null)
            {
                result.HasError = true;
                result.Errors.Add("Invalid FeeCode");
                return View(result);

            }

            /*if (!Facade.StudentAreaFacade.RegistrationFacade.PrintCourseRegistration(ref result, minSemester.SemesterId, studentId))
            {
                return View(result);
            }*/

            result.DataBag.StudentInfo = DbInstance.User_Student.Include(x=>x.Aca_Program).FirstOrDefault(x => x.Id == studentId);
            return View(result);
        }

        /*public ActionResult DownloadPaySlipPdf(int transactionId=0)
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.StudentTransaction.CanPrintPaySlip))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }

            //ViewBag.IncludePdfExportCSS = $"<link href=\"{Url.Content("~/assets/custom/css/PdfExport.css")}\" rel=\"stylesheet\" />";
            return new ActionAsPdf("PrintPaySlip", new { name = "StudentController", transactionId = transactionId, exportPdf = true })
            {
                FileName = "PaySlip.pdf",
                PageSize = Size.A4,
                IsGrayScale = true,
                IsBackgroundDisabled = true,
                //MinimumFontSize = 14,
                PageOrientation = Orientation.Portrait,
                //PageMargins = { Left = 0, Right = 0 } ,
                IsJavaScriptDisabled = false,
                CustomSwitches = string.Format("--disable-smart-shrinking") //its important to keep actual rendering
                                                                            //string.Format("--footer-left \"Note:.....\" "
                                                                            //                + "--footer-font-size \"8\" "
                                                                            //                + "--footer-right \"Page [page] of [toPage]\" ")

            };


        }*/
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
	    /*
	    public ActionResult PrintPaySlip(int transactionId = 0, int semesterId = 0, long studentId = 0)
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.StudentTransaction.CanPrintPaySlip))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }

            var result = new MvcModelResult<Acnt_StdTransaction>();

            /*if (!Facade.StudentAreaFacade.RegistrationFacade.PrintPaySlipOrMoneyReceipt(ref result, transactionId, semesterId, studentId))
            {

                return View(result);
            }#1#

            return View(result);
        }
        */

        public ActionResult IncompleteRegistrationStudentList()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            var error = string.Empty;
            ViewBag.Error = error;
            List<User_Student> students = null;
            try
            {
                students = DbInstance.User_Student
                    .Include(x => x.User_Account)
                    .Include(x=>x.User_Account.HR_Department)
                    .Include(x => x.User_Account.User_Education)
                    .Include(x => x.User_Account.User_ContactAddress)
                    .Include(x => x.Aca_StudyLevelTerm)
                    .Where(x => x.EnrollmentStatusEnumId == (byte)User_Student.EnrollmentStatusEnum.Continuing
                          && x.User_Account.UserStatusEnumId == (byte)User_Account.UserStatusEnum.Active
                          && (
                          x.User_Account.FatherName == ""
                          || x.User_Account.MotherName == ""
                          )
                    )
                    .OrderBy(x => x.User_Account.HR_Department.Priority)
                    .ThenBy(x => x.LevelTermId)
                    .ThenBy(x => x.ClassRollNo)
                    .ToList();
            }
            catch (Exception ex)
            {
                ViewBag.Error += ex.ToString();
            }

            return View(students);
        }
       
        /*
        public ActionResult PrintTransactionReport(long id)
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AccountingArea.StudentTransaction.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            var result = new MvcModelListResult<Acnt_StdTransaction>();
            string error = string.Empty;
            try
            {
                List<Acnt_StdTransaction> transactionList = DbInstance.Acnt_StdTransaction
                    .Include(x => x.Aca_Semester)
                    .Include(x => x.User_Student)
                    .Include(x => x.User_Student.User_Account)
                    .Include(x => x.User_Student.User_Account.HR_Department)
                    .Include(x => x.User_Student.Aca_DeptBatch)
                    .Include(x => x.User_Student.Aca_Program)
                    //.Include(x => x.Acnt_StdTransactionDetail)
                    .Where(x => x.StudentId == id)
                    .OrderBy(x => x.SemesterId)
                    .ThenBy(x => x.Date)
                    .ThenByDescending(x => x.DebitAmount > 0)
                    .ThenBy(x => x.Id)
                    .ToList();

                result.Data = transactionList;


                float totalDebitAmount = transactionList.Sum(x => x.DebitAmount);

                float totalCreditAmount = transactionList.Sum(x => x.CreditAmount);
                float totalBalance = totalDebitAmount - totalCreditAmount;

                float totalFee = 0, otherCharges = 0;

                /*foreach (var transaction in transactionList)
                {
                    if (!transaction.Acnt_StdTransactionDetail.Any())
                    {
                        continue;
	                }
	                if (transaction.IsSemesterFee)
	                {
	                    totalFee += transaction.Acnt_StdTransactionDetail
	                        .Where(x => x.ParticularTypeEnumId == (byte)Acnt_ParticularName.ParticularTypeEnum.TutionFee
	                                    ||x.ParticularTypeEnumId == (byte)Acnt_ParticularName.ParticularTypeEnum.FixedCost)
	                        .Sum(x=>x.Amount);
                        otherCharges+= transaction.Acnt_StdTransactionDetail
                            .Where(x => x.IsDebited )
                            .Sum(x => x.Amount);

                    }
                }#1#
                result.DataBag.TotalDebitAmount = totalDebitAmount;
                result.DataBag.TotalCreditAmount = totalCreditAmount;
                result.DataBag.TotalBalance = totalBalance;
                result.DataBag.TotalFee = totalFee;
                result.DataBag.OtherCharges = otherCharges;

            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return View(result);
        }
        */

        [System.Web.Mvc.HttpGet]
        public ActionResult LoginPortalAsStudent(int studentUserId = 0)
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanLoginToPortal))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            string error = string.Empty;
            var userCredential = DbInstance.User_Account.FirstOrDefault(x => x.Id == studentUserId);

            if (userCredential == null || userCredential.UserTypeEnumId != (byte)User_Account.UserTypeEnum.Student)
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

            var userName = userCredential.UserName + $" (By {HttpUtil.Profile.Name} [{HttpUtil.Profile.UserName}])";

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

        public ActionResult PrintCreditTransferCourses()
        {
            return View();
        }

    }
}

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

namespace EMS.Web.UI.Areas.Admission.Controllers
{
    public class StudentController : BaseController
    {
        // GET: Admission/Student
        public ActionResult StudentList()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        public ActionResult StudentProfile()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanView))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }

        public ActionResult PrintAdmissionForm(long id)
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanPrintForm))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            var result = new MvcModelResult<User_Student>();
            string error = string.Empty;
            try
            {
                var student = DbInstance.User_Student
                    .Include(x => x.User_Account)
                    .Include(x => x.User_Account.Aca_Semester)
                    .Include(x => x.User_Account.User_ContactAddress)
                    .Include(x=>x.User_Account.User_Relationship)
                    .SingleOrDefault(x => x.UserId == id);
                if (student.IsNull())
                {
                    result.HasError = true;
                    result.Errors.Add("Student not Found!");
                    return View(result);
                }

                result.Data = student;
                List<User_Education> studentEducationList = DbInstance.User_Education
                    .Include(x => x.Adm_EducationBoard)
                    .Where(x => x.UserId == id)
                    .OrderBy(x=>x.DegreeEquivalentEnumId)
                    .Take(4)
                    .ToList();


                //This condition use for empty 2 row generate in Adm Form
                int count = 2 - studentEducationList.Count;
                if (count  > 0)
                {
                    for (int i = 0; i < count; i++)
                    {
                        studentEducationList.Add(new User_Education());
                    }
                }


                result.DataBag.StudentEducationList = studentEducationList;

                /*Acnt_FeeCode feecode = DbInstance.Acnt_FeeCode.SingleOrDefault(x => x.Id == student.FeeCodeId);
                var feeCodePolicy = DbInstance.Acnt_FeeCodePolicy
                    .SingleOrDefault(x => x.FeeCodeId == feecode.Id
                                          && x.ParticularTypeEnumId==(byte)Acnt_ParticularName.ParticularTypeEnum.TutionFee
                                          && x.FeeGenerateTermEnumId ==
                                          (byte)Acnt_FeeCodePolicy.FeeGenerateTermEnum.PerSemesterPerCredit
                    );

                var perCredit = feeCodePolicy?.Amount ?? 0;
                var tutionFee = perCredit * feecode?.TotalCredit;


                result.DataBag.PerCredit = perCredit;
                result.DataBag.TutionFee = tutionFee;
                result.DataBag.OthersFee = feecode?.TotalPackageAmount - tutionFee;
                result.DataBag.TotalPayable = feecode?.TotalPackageAmount;*/
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.Message.ToString());
                //result.Errors.Add(ex.ToString());
                return View(result);
            }


            return View(result);
        }


        

    }
}
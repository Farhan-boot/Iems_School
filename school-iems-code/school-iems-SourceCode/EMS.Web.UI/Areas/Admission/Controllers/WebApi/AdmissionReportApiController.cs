using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using System.Web.UI.WebControls;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases.WebApi;
using EMS.Web.Jsons.Custom.Admission.Report;
using EMS.Web.Jsons.Models;

namespace EMS.Web.UI.Areas.Admission.Controllers.WebApi
{
    public class AdmissionReportApiController : EmployeeBaseApiController
    {
        //public ReportApiController()
        //{

        //}

        #region Get Api

        public HttpListResult<object> GetAdmissionSummaryReportDataExtra()
        {
            var result = new HttpListResult<object>();
            result.DataExtra.AdmissionDate = DateTime.Now;
            return result;

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<FacultyWiseAdmissionSummaryJson> GetAdmissionSummaryReport(
            Int32? admExamId =0
           , Int32 facultyId=0
            , string admDate=""

         )
        {
            string error = string.Empty;
           
            var result = new HttpListResult<FacultyWiseAdmissionSummaryJson>();
            try
            {
                DateTime admissionDate = Convert.ToDateTime(admDate).ToOnlyDate();

                if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.Report.CanViewAdmissionSummary, out error))
                {
                    result.HasError = true;
                    result.HasViewPermission = false;
                    result.Errors.Add(error);
                    return result;
                }

               

                var admissionExamList = DbInstance.Adm_AdmissionExam
                    .Include(x => x.Aca_Semester)
                         //.Where(x => x.CircularStatusEnumId != (byte)Adm_AdmissionExam.CircularStatusEnum.Expired)
                         .OrderByDescending(x => x.Aca_Semester.Year)
                        .ThenByDescending(x => x.Aca_Semester.TermId)
                .AsEnumerable()
                .Select(t => new
                {
                    Id = t.Id,
                    Name = t.Name,
                    //Year = (t.Aca_Semester.Year % 100).ToStringPadLeft(2, '0'),
                    //Term = t.Aca_Semester.TermId.ToStringPadLeft(2, '0'),
                    SemesterName = t.Aca_Semester.Name,
                    CircularStatusEnumId = t.CircularStatusEnumId,
                    CircularStatus = t.CircularStatus.ToString()
                }).ToList();

                if (admissionExamList.Count <= 0)
                {
                    result.HasError = true;
                    result.HasViewPermission = false;
                    result.Errors.Add("There is no valid Admission Circular in the System. Sorry you can't view any Report!");
                    //return false;
                }
                result.DataExtra.AdmissionExamList = admissionExamList;
                if (admExamId==0)
                {
                    admExamId = admissionExamList.FirstOrDefault(
                         x => x.CircularStatusEnumId == (byte)Adm_AdmissionExam.CircularStatusEnum.Current)?.Id;
                }
                admExamId = admExamId ?? 0;

                result.DataExtra.SelectedExamId = admExamId;
               
               

                var facultyList = DbInstance.HR_Department
                         //.Include(x=>x.Aca_Program)
                         .OrderBy(x => x.Name)
                        .Where(x =>
                                !x.IsDeleted && (x.TypeEnumId == (byte)HR_Department.TypeEnum.AcademicFaculty ||
                                x.TypeEnumId == (byte)HR_Department.TypeEnum.AcademicDepartment)).AsEnumerable()
                .Select(t => new
                {
                    Id = t.Id,
                    Name = t.Name,
                    TypeEnumId = t.TypeEnumId,
                    Type = t.Type,
                    ParentDeptId = t.ParentDeptId,
                }).ToList();

                if (facultyList.Count <= 0)
                {
                    result.HasError = true;
                    result.HasViewPermission = false;
                    result.Errors.Add("There is no Academic Faculty Found in the System. Sorry you can't view any Report!");
                    //return false;
                }
               


                var programList = DbInstance.Aca_Program
                    .OrderBy(x => x.Name)
                    .ThenBy(x => x.ProgramTypeEnumId)
                    .ThenBy(x => x.ClassTimingGroupEnumId)
                    .Where(x => !x.IsDeleted).Select(t => new
                    {
                        Id = t.Id,
                        Name = t.ShortName
                    }).ToList();

                if (programList.Count <= 0)
                {
                    result.HasError = true;
                    result.HasViewPermission = false;
                    result.Errors.Add("There is no Academic Program Found in the System. Sorry you can't view any Report!");
                    //return false;
                }
                //result.DataExtra.ProgramList = programList;

                var facyltyListJson = new List<FacultyWiseAdmissionSummaryJson>();

                // get all student & applicant list
                var allStudentList = DbInstance.User_Student
                    .Include(x => x.User_Account)
                    .Where(x => x.AdmissionExamId == admExamId
                                && x.IsDeleted==false
                    ).ToList();
                

                // Get Total Incomplete Profile
                result.DataExtra.TotalIncompleteProfile = allStudentList.Count(x => x.EnrollmentStatusEnumId == (byte) User_Student.EnrollmentStatusEnum.Applicant);

              // only valid student list (Confirm Admission)
                var studentList = allStudentList
                    .Where(x => x.User_Account.UserTypeEnumId == (byte) User_Account.UserTypeEnum.Student || x.User_Account.UserTypeEnumId == (byte)User_Account.UserTypeEnum.Applicant).ToList();


                var selectedFacultyList = facultyList.Where(x => x.Type == HR_Department.TypeEnum.AcademicFaculty);

                result.DataExtra.FacultyList = selectedFacultyList;
                result.DataExtra.DeptList = facultyList.Where(x => x.Type == HR_Department.TypeEnum.AcademicDepartment);
                if ( facultyId>0)
                {
                    selectedFacultyList = facultyList.Where(x =>x.Id== facultyId && x.Type == HR_Department.TypeEnum.AcademicFaculty);
                }
                int grandTotal = 0;
                int grandTotalMale = 0;
                int grandTotalFemale = 0;
                int todayGrandTotal = 0;
                foreach (var faculty in selectedFacultyList)
                {
                    var deptList =
                        facultyList.Where(
                            x => x.ParentDeptId == faculty.Id && x.Type == HR_Department.TypeEnum.AcademicDepartment).ToList();
                    if (deptList != null && deptList.Count > 0)
                    {
                        var facyltyJson = new FacultyWiseAdmissionSummaryJson();
                        facyltyJson.FacultyName = faculty.Name;
                        facyltyJson.SemesterName = "";
                        facyltyJson.FacultyId = faculty.Id;
                        facyltyJson.ProgramDetailListJson = new List<ProgramWiseAdmissionSummaryJson>();

                        foreach (var dept in deptList)
                        {
                            var programsInDept = programList.Where(x => x.Id == dept.Id).ToList();


                            if (programsInDept != null && programsInDept.Count > 0)

                                foreach (var prog in programsInDept)
                                {

                                    // Program wise total admitted count with applicant
                                    int totalAdmitted = allStudentList.Count(x => x.ProgramId == prog.Id);

                                    // 0 admission student in program not print at report
                                    if (totalAdmitted <= 0)
                                    {
                                        continue;
                                    }


                                    var studentInProgram = studentList.Where(x => x.ProgramId == prog.Id).ToList();
                                    int totalMale = studentInProgram.Count(x => x.User_Account.Gender == User_Account.GenderEnum.Male);
                                    int totalFemaleInProgramm = studentInProgram.Count(x => x.User_Account.Gender == User_Account.GenderEnum.Female);
                                    int total = studentInProgram.Count;

                                    // Program wise today total admitted
                                    int todayTotal = allStudentList.Count(x => x.User_Account.JoiningDate.ToOnlyDate() == admissionDate
                                                              && x.ProgramId== prog.Id);


                                    var programDetailListJson = new ProgramWiseAdmissionSummaryJson();
                                    programDetailListJson.ProgramName = prog.Name;
                                    programDetailListJson.FacultyId = faculty.Id;
                                    programDetailListJson.TotalMale = totalMale;
                                    facyltyJson.TotalMale += totalMale;
                                    
                                    programDetailListJson.TotalFemale = totalFemaleInProgramm;
                                    facyltyJson.TotalFemale += totalFemaleInProgramm;
                                    programDetailListJson.GrandTotal = total;
                                    facyltyJson.GrandTotal += total;

                                    // Today Admitted
                                    programDetailListJson.TodayTotal = todayTotal;
                                    facyltyJson.TodayTotal += todayTotal;

                                    facyltyJson.ProgramDetailListJson.Add(programDetailListJson);
                                }

                        }
                         grandTotalMale += facyltyJson.TotalMale;
                         grandTotalFemale += facyltyJson.TotalFemale;
                        grandTotal += facyltyJson.GrandTotal;
                        todayGrandTotal += facyltyJson.TodayTotal;
                        facyltyListJson.Add(facyltyJson);
                    }
                }
                result.Data = facyltyListJson;

                result.Count = grandTotal;
                result.DataExtra.GrandTotalMale = grandTotalMale;
                result.DataExtra.GrandTotalFemale = grandTotalFemale;
                result.DataExtra.Paid= studentList.Count(x => x.IsAdmissionFeePaid);
                result.DataExtra.Unpaid= studentList.Count(x => !x.IsAdmissionFeePaid);
                result.DataExtra.TodayGrandTotal = todayGrandTotal;

                // Use For UI
                result.DataExtra.AdmissionDate = admissionDate;

            }

            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }

        #endregion

        #region Custom Api



        #endregion

    }
}
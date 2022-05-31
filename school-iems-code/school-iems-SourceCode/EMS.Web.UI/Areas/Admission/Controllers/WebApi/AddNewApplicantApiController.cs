using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.CoreLibrary;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;
using EMS.Framework.Settings;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases.WebApi;
using EMS.Web.Jsons.Custom.Academic;
using EMS.Web.Jsons.Custom.Admission.Applicant;
using EMS.Web.Jsons.Models;
using EntityMapper = EMS.Web.Jsons.Models.EntityMapper;
using System.Data.Entity.Infrastructure;
using System.Text.RegularExpressions;
using EMS.Web.Jsons.Custom.Admission.AdmissionExam;
using EMS.Web.UI.Areas.Employee.Controllers.WebApi;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;

//AddNewApplicantApiController.cs
namespace EMS.Web.UI.Areas.Admission.Controllers.WebApi
{
    public class AddNewApplicantApiController : EmployeeBaseApiController
    {


        //private string GetStudentIdPrefix(int admissionSemesterYear, int semesterCode, int deptCode)
        //{
        //    DateTime expirationDate = new DateTime(admissionSemesterYear, 1, 1); // random date
        //    string lastTwoDigitsOfYear = expirationDate.ToString("yy");
        //    var idPrefix = $"{lastTwoDigitsOfYear}{semesterCode.ToStringPadLeft(2, '0')}{deptCode.ToStringPadLeft(2, '0')}";
        //    return idPrefix;
        //}
        public HttpResult<AddNewStudentStep1Json> GetStudentById(long id = 0)
        {

            string error = string.Empty;
            var applicantResult = new HttpResult<AddNewStudentStep1Json>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanAdd, out error))
            {
                applicantResult.HasError = true;
                applicantResult.HasViewPermission = false;
                applicantResult.Errors.Add(error);
                return applicantResult;
            }
            var applicantJson = new AddNewStudentStep1Json
            {
                Id = "0",
                UserName = "",
                FullName = "",
                GenderEnumId = (byte)User_Account.GenderEnum.Unknown,
                AdmissionExamId = 0,//List with active selected
                ProgramId = "",//List
                EnrollmentTypeEnumId = 0,
                FeeCodeId = 0,//list with active by programId
                IsFullManualId = SiteSettings.Instance.StudentIdGenerateType == SiteSettings.StudentIdGenerateTypeEnum.FullManual,
                StudentIdGenerateType = SiteSettings.Instance.StudentIdGenerateType,
                ContinuingBatchId = 0,
                ClassSectionId = 1,
                CurriculumId = "",
                EmailAddress = "",
                MobileNo = "",
                Remark = "",
                IsAutoRegisterCourses = false,
                SscDegreeCategoryId = 1,
                HscDegreeCategoryId = 2,
                //IdPrefix="XXXXXX",
                //DepartmentName = 0,//list
                //DepartmentCode = 0,
                //AdmissionYear = 0,
                //SemesterTerm = 0,
                //ClassTimingGroupEnumId = 0


            };
            try
            {
                
                if (!GetDataExtraForCreateApplicant(applicantResult, out error))
                {
                    return applicantResult;
                }

                if (SiteSettings.Instance.AddNewApplicantFormSettings.IsEnableAutoRegistration)
                {
                    applicantJson.IsAutoRegisterCourses = true;
                }
                //setting current exam ID
                applicantJson.AdmissionExamId = applicantResult.DataExtra.CurrentAdmExamId;
                applicantResult.Data = applicantJson;
            }
            catch (Exception ex)
            {
                applicantResult.HasError = true;
                applicantResult.Errors.Add(ex.ToString());
            }

            applicantResult.Data = applicantJson;
            return applicantResult;
        }


        private bool GetDataExtraForCreateApplicant(HttpResult<AddNewStudentStep1Json> applicantResult, out string error)
        {
            error = string.Empty;
            if (applicantResult == null)
            {
                error = "";
                return false;
            }

            if (SiteSettings.Instance.AddNewApplicantFormSettings.IsEnableCampusSelection)
            {
                var campusList = DbInstance.General_Campus.AsEnumerable()
                    .Select(t => new
                    {
                        Id = t.Id,
                        Name = t.Name,
                    })
                    .OrderBy(x => x.Id)
                    .ToList();

                applicantResult.DataExtra.CampusList = campusList;

                if (campusList.Count == 1)
                {
                    applicantResult.DataExtra.SelectedCampusId = campusList[0].Id;
                }
            }

            if (SiteSettings.Instance.AddNewApplicantFormSettings.IsEnableEnrollTypeSelection)
            {
                applicantResult.DataExtra.EnrollmentTypeEnumList = EnumUtil.GetEnumList(typeof(User_Student.EnrollmentTypeEnum));
            }
            
            var admissionExamList = DbInstance.Adm_AdmissionExam
                .Include(x => x.Aca_Semester)
                .Where(x => x.CircularStatusEnumId != (byte)Adm_AdmissionExam.CircularStatusEnum.Expired)
                     .OrderByDescending(x => x.Aca_Semester.Year)
                    .ThenByDescending(x => x.Aca_Semester.TermId)
            .AsEnumerable()
            .Select(t => new
            {
                Id = t.Id,
                Name = t.Name,
                StudentIdPrefix = SiteSettings.Instance.StudentIdGenerateType != SiteSettings.StudentIdGenerateTypeEnum.FullManual ? t.StudentIdPrefix : "",
                UgcIdPrefix = SiteSettings.Instance.StudentIdGenerateType != SiteSettings.StudentIdGenerateTypeEnum.FullManual ?
                    SiteSettings.Instance.AddNewApplicantFormSettings.UgcVersityCode+t.UgcIdPrefix : "",
                StudentIdSuffix = t.StudentIdSuffix,
                SemesterDurationEnumId = t.Aca_Semester.SemesterDurationEnumId,
                SemesterId = t.SemesterId.ToString(),
                //Year = (t.Aca_Semester.Year % 100).ToStringPadLeft(2, '0'),
                //Term = t.Aca_Semester.TermId.ToStringPadLeft(2, '0'),
                SemesterName = t.Aca_Semester.Name,
                CircularStatusEnumId = t.CircularStatusEnumId,
                DefaultSettingsJsonObj = JsonConvert.DeserializeObject<DefaultSettingsJson>(t.DefaultSettingsJson == null?"":t.DefaultSettingsJson)
            }).ToList();


            if (admissionExamList.Count <= 0)
            {
                applicantResult.HasError = true;
                applicantResult.Errors.Add("There is no valid Admission Circular in the System. Sorry you can't admit new applicant!");
                return false;
            }
            applicantResult.DataExtra.CurrentAdmExamId = admissionExamList.FirstOrDefault(
                       x => x.CircularStatusEnumId == (byte)Adm_AdmissionExam.CircularStatusEnum.Current)?.Id;


            applicantResult.DataExtra.AdmissionExamList = admissionExamList;



            var programList = DbInstance.Aca_Program
                .AsEnumerable()
           .Select(t => new
           {
               Id = t.Id.ToString(),
               Name = t.ShortTitle,
               //DepartmentId = t.DepartmentId,
               DepartmentCode = SiteSettings.Instance.StudentIdGenerateType != SiteSettings.StudentIdGenerateTypeEnum.FullManual ?
               t.Code : "",
               //ProgramTypeEnumId = t.ProgramTypeEnumId,
               //ClassTimingGroupEnumId = t.ClassTimingGroupEnumId,
               ClassTimingGroup = t.ClassTimingGroup.ToString()

           })
           .OrderBy(x => x.Name)
           .ToList();

            if (programList.Count <= 0)
            {
                applicantResult.HasError = true;
                applicantResult.Errors.Add("There is no valid Program in the System. Sorry you can't admit new applicant!");
                return false;
            }

            applicantResult.DataExtra.ProgramList = programList;

            var classSectionList = DbInstance.Aca_ClassSection.AsEnumerable()
                .Select(t => new
                {
                    Id = t.Id,
                    Name = t.Name,
                })
                .OrderBy(x => x.Id)
                .ToList();

            if (classSectionList.Count <= 0)
            {
                applicantResult.HasError = true;
                applicantResult.Errors.Add("There is no valid Class Section in the System. Sorry you can't admit new applicant!");
                return false;
            }

            applicantResult.DataExtra.ClassSectionList = classSectionList;


            var degreeCategoryList = DbInstance.Adm_DegreeCategory
                .Where(x=>x.DegreeEquivalentEnumId==(byte)User_Education.DegreeEquivalentEnum.Ssc10YearEquivalent || x.DegreeEquivalentEnumId == (byte)User_Education.DegreeEquivalentEnum.Hsc12YearEquivalent)
                .AsEnumerable()
                .Select(t => new
                {
                    Id = t.Id,
                    Name = t.Name,
                    DegreeEquivalentEnumId = t.DegreeEquivalentEnumId,
                    BoardTypeEnumId = t.BoardTypeEnumId
                })
                .ToList();

            applicantResult.DataExtra.SscEquivalentDegreeCategoryList = degreeCategoryList.Where(x=>x.DegreeEquivalentEnumId == (byte)User_Education.DegreeEquivalentEnum.Ssc10YearEquivalent).ToList();
            applicantResult.DataExtra.HscEquivalentDegreeCategoryList = degreeCategoryList.Where(x => x.DegreeEquivalentEnumId == (byte)User_Education.DegreeEquivalentEnum.Hsc12YearEquivalent).ToList(); ;

            return true;
        }
        public HttpResult<Aca_CurriculumJson> GetDataExtraOnChangeProgram(long id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_CurriculumJson>();

            try
            {
                

                if (SiteSettings.Instance.AddNewApplicantFormSettings.IsEnableDeptBatchSelection)
                {
                    result.DataExtra.DeptBatchList = DbInstance.Aca_DeptBatch.AsEnumerable().Select(t => new
                    {
                        Id = t.Id,
                        Name = t.Name
                    }).ToList();
                }



                if (!SiteSettings.Instance.AddNewApplicantFormSettings.IsAutoClassSection)
                {
                    var classSectionList = DbInstance.Aca_ClassSection.AsEnumerable()
                        .Select(t => new
                        {
                            Id = t.Id,
                            Name = t.Name,
                        })
                        .OrderBy(x => x.Id)
                        .ToList();

                    if (classSectionList.Count <= 0)
                    {
                        result.HasError = true;
                        result.Errors.Add("There is no valid Class Section in the System. Sorry you can't admit new applicant!");
                        return result;
                    }

                    result.DataExtra.ClassSectionList = classSectionList;
                }

                if (!SiteSettings.Instance.AddNewApplicantFormSettings.IsAutoAllocateSyllabus)
                {
                    //todo curriculamList should be sent.

                    var curriculumList = DbInstance.Aca_Curriculum.AsEnumerable()
                        .Where(x => x.ProgramId == id
                            // && x.IsValid == true
                            // && x.IsOffering == true
                        )
                        .OrderByDescending(x => x.Year)
                        .Select(t => new
                        {
                            Id = t.Id.ToString(),
                            Name = t.ShortName,
                            IsOffering = t.IsOffering
                        })
                        .ToList();

                    if (curriculumList.Count <= 0)
                    {
                        result.HasError = true;
                        result.Errors.Add("There is no Core Curriculum for this Program in the System.<br>");
                        return result;
                    }

                    var selectedCurriculum =  curriculumList.FirstOrDefault(x => x.IsOffering);
                    if (selectedCurriculum != null)
                    {
                        result.DataExtra.SelectedCurriculumId = selectedCurriculum.Id;
                    }

                    result.DataExtra.CurriculumList = curriculumList;
                }
            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Errors.Add(e.Message.ToString());
                return result;
            }


            return result;
        }

        public HttpResult<object> GetMaxAndSuggestUsername(int programId = 0, int admissionExamId = 0)
        {
            var result = new HttpResult<object>();

            try
            {
                if (SiteSettings.Instance.StudentIdGenerateType ==
                    SiteSettings.StudentIdGenerateTypeEnum.FullAutoGenerateAsUGC ||
                    SiteSettings.Instance.StudentIdGenerateType ==
                    SiteSettings.StudentIdGenerateTypeEnum.PrefixAutoGenerateAsUGC)
                {
                    var maxStudent = DbInstance.User_Student
                        .Where(x => x.ProgramId == programId
                                    && x.AdmissionExamId == admissionExamId
                                    && !x.IsDeleted)
                        .OrderByDescending(x => x.UgcUniqueId).FirstOrDefault();

                    result.DataExtra.MaxUserName = maxStudent?.UgcUniqueId ?? "";
                }
                else
                {
                    var maxStudent = DbInstance.User_Student
                        .Where(x => x.ProgramId == programId
                                    && x.AdmissionExamId == admissionExamId
                                    && !x.IsDeleted)
                        .OrderByDescending(x => x.ClassRollNo).FirstOrDefault();

                    result.DataExtra.MaxUserName = maxStudent?.ClassRollNo ?? "";
                }
                
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.Message);
            }


            return result;
        }


        public HttpResult<AddNewStudentStep1Json> GetPreviousStudentInformation(string userName)
        {
            var result = new HttpResult<AddNewStudentStep1Json>();

            try
            {
                var previousStudent = DbInstance
                    .User_Student
                    .Include(x=>x.User_Account)
                    .FirstOrDefault(x => x.User_Account.UserName.Trim() == userName.Trim());

                if (previousStudent == null)
                {
                    result.HasError = true;
                    result.Errors.Add("Please Provide a Valid Previous UserId");
                    return result;
                }

                if (previousStudent.AdmissionStatusEnumId != (byte)User_Student.AdmissionStatusEnum.AdmissionCancelled)
                {
                    result.HasError = true;
                    result.Errors.Add("Students Previous Id's Admission is not Cancelled. Please Cancel that first.");
                    return result;
                }

                var studentUpdateJson = new AddNewStudentStep1Json()
                {
                    PreviousProgramId = previousStudent.ProgramId.ToString(),
                    GenderEnumId = previousStudent.User_Account.GenderEnumId,
                    CampusId = previousStudent.User_Account.CampusId,
                    MobileNo = previousStudent.User_Account.UserMobile,
                    EmailAddress = previousStudent.User_Account.UserEmail,
                    FullName = previousStudent.User_Account.FullName

                };
                result.Data = studentUpdateJson;
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.Message);
            }


            return result;
        }


        [HttpPost]
        public HttpResult<AddNewStudentStep1Json> SaveStudent(AddNewStudentStep1Json applicantJson)
        {
            string error = string.Empty;
            var result = new HttpResult<AddNewStudentStep1Json>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Student.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            DbContextTransaction scope = null;
            try
            {
                //checking json is correct
                if (!IsValidToSaveNewApplicant(applicantJson, out error))
                {
                    result.HasError = true;
                    result.Errors.Add(error);
                    return result;
                }

                long programId = 0;

                long.TryParse(applicantJson.ProgramId, out programId);

                var admExam =
                    DbInstance.Adm_AdmissionExam.Include(x => x.Aca_Semester)
                        .SingleOrDefault(x => x.Id == applicantJson.AdmissionExamId);

                if (admExam == null)
                {
                    result.HasError = true;
                    result.Errors.Add("Selected Admission Circular is not valid, please reload the page and try again!");
                    return result;
                }

                var selectedProgram = DbInstance.Aca_Program
                    .SingleOrDefault(x => x.Id == programId);
                if (selectedProgram == null)
                {
                    result.HasError = true;
                    result.Errors.Add("Selected Program is not valid, please reload the page and try again!");
                    return result;
                }

                if (!SiteSettings.Instance.AddNewApplicantFormSettings.IsAutoClassSection)
                {
                    var classSection = DbInstance.Aca_ClassSection.FirstOrDefault(x => x.Id  == applicantJson.ClassSectionId);
                    if (classSection == null)
                    {
                        result.HasError = true;
                        result.Errors.Add("Please Select a Valid Class Section.!");
                        return result;
                    }
                }

                //if (!SiteSettings.Instance.AddNewApplicantFormSettings.IsEnableDeptBatchSelection)
                //{
                //    var deptBatch = DbInstance.Aca_DeptBatch.OrderByDescending(x => x.Id).FirstOrDefault();
                //    if (deptBatch == null)
                //    {
                //        result.HasError = true;
                //        result.Errors.Add("No Valid Department Found.");
                //        return result;
                //    }

                //    applicantJson.ContinuingBatchId = deptBatch.Id;
                //}

                var curriculum = new Aca_Curriculum();

                if (!SiteSettings.Instance.AddNewApplicantFormSettings.IsAutoAllocateSyllabus)
                {
                    //how to take syllabus. //todo
                    curriculum = DbInstance.Aca_Curriculum.FirstOrDefault(x => x.Id.ToString() == applicantJson.CurriculumId && x.ProgramId.ToString() == applicantJson.ProgramId);
                    if (curriculum == null)
                    {
                        result.HasError = true;
                        result.Errors.Add("Please Select a Valid Curriculum / Syllabus!");
                        return result;
                    }
                }
                else
                {
                    curriculum = DbInstance.Aca_Curriculum.Where(x => x.IsOffering && x.IsValid && x.ProgramId == programId).OrderByDescending(x => x.Year).FirstOrDefault();
                    if (curriculum == null)
                    {
                        result.HasError = true;
                        result.Errors.Add("There is no valid Syllabus found in the system for selected Program, please contact to registration department for correct the Syllabus!");
                        return result;
                    }
                }


                if (SiteSettings.Instance.AddNewApplicantFormSettings.IsEnableCampusSelection)
                {
                    var campus = DbInstance.General_Campus.FirstOrDefault(x => x.Id == applicantJson.CampusId);
                    if (campus == null)
                    {
                        result.HasError = true;
                        result.Errors.Add("There is no valid Campus found in the system, please contact to registration department for adding the Campus!");
                        return result;
                    }
                }

                //todo get it by programtype from program
                var gradingPolicy = DbInstance.Aca_GradingPolicy.FirstOrDefault(x => x.IsActive);
                if (gradingPolicy == null)
                {
                    result.HasError = true;
                    result.Errors.Add("There is no valid Grading Policy found in the system for selected Program, please contact to registration department for correct the Grading Policy!");
                    return result;
                }


                //var username = $"{(admExam.Aca_Semester.Year % 100).ToStringPadLeft(2, '0')}{admExam.Aca_Semester.TermId.ToStringPadLeft(2, '0')}{selectedProgram.HR_Department.DepartmentNo.ToStringPadLeft(2, '0')}{applicantJson.UserName.Trim()}";
                //setting section ID for each section 80 students

                int classSectionId = applicantJson.ClassSectionId;


                string username = "";
                if (applicantJson.IsFullManualId)
                {
                    username = $"{applicantJson.UserName.Trim()}";
                }
                else
                {

                    if (SiteSettings.Instance.StudentIdGenerateType == SiteSettings.StudentIdGenerateTypeEnum.PrefixAutoGenerate)
                    {
                        if (SiteSettings.Instance.AddNewApplicantFormSettings.IsAutoClassSection)
                        {
                            classSectionId = ((int)(applicantJson.UserName.Trim().ToInt64() - 1) / SiteSettings.Instance.DefaultClassSectionRange) + 1;
                        }
                        username = $"{admExam.StudentIdPrefix.Trim()}{selectedProgram.Code}{applicantJson.UserName.Trim().ToInt64().ToStringPadLeft(SiteSettings.Instance.StudentIdSerialDigitCount, '0')}";
                    }
                    //Here if ugc unique id is auto generate then 
                    else if (SiteSettings.Instance.StudentIdGenerateType ==
                             SiteSettings.StudentIdGenerateTypeEnum.PrefixAutoGenerateAsUGC)
                    {
                        if (SiteSettings.Instance.AddNewApplicantFormSettings.IsAutoClassSection)
                        {
                            classSectionId = ((int)(applicantJson.UserName.Trim().ToInt64() - 1) / SiteSettings.Instance.DefaultClassSectionRange) + 1;
                        }

                        if (!SiteSettings.Instance.AddNewApplicantFormSettings.UgcVersityCode.IsValid())
                        {
                            result.HasError = true;
                            result.Errors.Add("UGC University Code invalid, auto generate UGC unique ID not possible");
                            return result;
                        }

                        if (!admExam.UgcIdPrefix.IsValid())
                        {
                            result.HasError = true;
                            result.Errors.Add("UGC Semester Prefix invalid, auto generate UGC unique ID not possible");
                            return result;
                        }

                        username = $"{SiteSettings.Instance.AddNewApplicantFormSettings.UgcVersityCode.Trim()}{admExam.UgcIdPrefix.Trim()}" +
                                   $"{applicantJson.UserName.Trim().ToInt64().ToStringPadLeft(SiteSettings.Instance.StudentIdSerialDigitCount, '0')}";
                    }
                }

                if (applicantJson.IsFullManualId 
                    || SiteSettings.Instance.StudentIdGenerateType == SiteSettings.StudentIdGenerateTypeEnum.PrefixAutoGenerate
                    || SiteSettings.Instance.StudentIdGenerateType == SiteSettings.StudentIdGenerateTypeEnum.PrefixAutoGenerateAsUGC
                    )
                {

                    if (!username.IsValid())
                    {
                        result.HasError = true;
                        result.Errors.Add("Student ID can't blank.");
                        return result;
                    }

                    //validate userName/classroll/registrationNumber
                    if (HttpUtil.DbContext.User_Account.Any(x => x.UserName == username))
                    {
                        result.HasError = true;
                        result.Errors.Add("Given Student ID Already exist, please try another!");
                        return result;
                    }
                }
                applicantJson.UserName = username;


               
              
                applicantJson.ClassSectionId = classSectionId;

                scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted);

                //using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    //Getting Auto UserName and UGC UniqueId
                    if (!GetGenerateStudentIdAndUgcId(applicantJson, admExam, selectedProgram, out error))
                    {
                        result.HasError = true;
                        result.Errors.Add(error);
                        return result;
                    };

                    var userAccount = GetNewUser_Account(applicantJson, selectedProgram, admExam);

                    User_AccountDataService accountService = new User_AccountDataService(DbInstance, HttpUtil.Profile);
                    User_Account dbAddedObj = new User_Account();
                    if (!accountService.Save(userAccount, ref dbAddedObj, out error, scope))
                    {
                        result.HasError = true;
                        result.Errors.Add(error);
                        scope.Rollback();
                        scope.Dispose();
                        return result;
                    }
                    DbInstance.SaveChanges();
                    applicantJson.Id = dbAddedObj.Id.ToString();
                    User_StudentDataService studentService = new User_StudentDataService(DbInstance, HttpUtil.Profile);
                    var userStudent = GetNewUser_Student(applicantJson, dbAddedObj, admExam, selectedProgram, gradingPolicy, curriculum);

                    if (!studentService.Save(userStudent, out error, scope))
                    {
                        result.HasError = true;
                        result.Errors.Add(error);
                        scope.Rollback();
                        scope.Dispose();
                        return result;
                    }

                    DbInstance.SaveChanges();


                    // Save Previous Education Information
                    if (SiteSettings.Instance.AddNewApplicantFormSettings.IsEnablePreviousEducationInput)
                    {
                        if (applicantJson.SscGpa > 0 || applicantJson.HscGpa > 0)
                        {
                            //ssc
                            if (applicantJson.SscGpa > 0)
                            {
                                var sscResult = User_Education.GetNew();

                                sscResult.UserId = userStudent.UserId;
                                sscResult.DegreeEquivalentEnumId = (byte)User_Education.DegreeEquivalentEnum.Ssc10YearEquivalent;
                                sscResult.DegreeCategoryId = applicantJson.SscDegreeCategoryId;
                                sscResult.ResultSystemEnumId = (byte)User_Education.ResultSystemEnum.GPA;
                                sscResult.ObtainedGpaOrMarks = applicantJson.SscGpa;
                                if (applicantJson.SscGpa == 5 & applicantJson.SscIsGolden)
                                {
                                    sscResult.IsGolden = applicantJson.SscIsGolden;
                                }
                                else
                                {
                                    sscResult.IsGolden = null;
                                }

                                DbInstance.User_Education.Add(sscResult);
                            }
                            //hsc
                            if (applicantJson.HscGpa > 0)
                            {
                                var hscResult = User_Education.GetNew();

                                hscResult.UserId = userStudent.UserId;
                                hscResult.DegreeEquivalentEnumId = (byte)User_Education.DegreeEquivalentEnum.Hsc12YearEquivalent;
                                hscResult.DegreeCategoryId = applicantJson.HscDegreeCategoryId;
                                hscResult.ResultSystemEnumId = (byte)User_Education.ResultSystemEnum.GPA;
                                hscResult.ObtainedGpaOrMarks = applicantJson.HscGpa;
                                if (applicantJson.HscGpa == 5 & applicantJson.HscIsGolden)
                                {
                                    hscResult.IsGolden = applicantJson.HscIsGolden;
                                }
                                else
                                {
                                    hscResult.IsGolden = null;
                                }

                                DbInstance.User_Education.Add(hscResult);
                            }

                            DbInstance.SaveChanges();
                        }
                    }

                    if (SiteSettings.Instance.AddNewApplicantFormSettings.IsEnableAutoCourseOffering)
                    {
                        if (!Facade.AcademicFacade.ClassFacade.Offer1stSemesterCourses( userStudent.ProgramId, admExam.SemesterId, userStudent.ClassSectionId, userStudent.CurriculumId,userAccount.DepartmentId, scope, out error))
                        {
                            result.HasError = true;
                            result.Errors.Add(error);
                            scope.Rollback();
                            scope.Dispose();
                            return result;
                        }
                    }
                    DbInstance.SaveChanges();

                    //Auto Course Registration while Admission Function if selected. 
                    if (applicantJson.IsAutoRegisterCourses && SiteSettings.Instance.AddNewApplicantFormSettings.IsEnableAutoRegistration)
                    {
                        if (!SaveCourseList(userStudent.Id,userStudent.ProgramId,admExam.SemesterId,userStudent.ClassSectionId,scope,out error))
                        {
                            result.HasError = true;
                            result.Errors.Add(error);
                            scope.Rollback();
                            scope.Dispose();
                            return result;
                        }
                    }


                    //Department Change Log Adding. 

                    if (applicantJson.EnrollmentTypeEnumId == (byte)User_Student.EnrollmentTypeEnum.InternalDepartmentChange)
                    {

                        var userProfileChangeAudit = User_ProfileChangeAudit.GetNew();

                        // Here for department changing previous students user id will be considered as logged userID.
                        var previousUserId = DbInstance.User_Account
                            .Where(x => x.UserName.Trim() == applicantJson.PreviousStudentUserName.Trim())
                            .Select(x => x.Id).FirstOrDefault();
                        userProfileChangeAudit.UserId = previousUserId;

                        userProfileChangeAudit.FieldEnumId = (byte)User_ProfileChangeAudit.FieldEnum.DepartmentChange;
                        userProfileChangeAudit.SemesterId = admExam.SemesterId;

                        userProfileChangeAudit.OldPk = applicantJson.PreviousProgramId;
                        userProfileChangeAudit.NewPk = applicantJson.ProgramId;

                        var oldProgramName = DbInstance.Aca_Program.Where(x => x.Id == userStudent.PreviousProgramId)
                            .Select(x => x.ShortTitle).FirstOrDefault();
                        var newProgramName = DbInstance.Aca_Program.Where(x => x.Id == userStudent.ProgramId)
                            .Select(x => x.ShortTitle).FirstOrDefault();

                        userProfileChangeAudit.OldValue = oldProgramName;
                        userProfileChangeAudit.NewValue = newProgramName;

                        userProfileChangeAudit.NewUserId = userStudent.UserId;
                        userProfileChangeAudit.Remark = applicantJson.Remark;

                        if (!Facade.UserCredentialFacade.AddProfileChangeAudit(userProfileChangeAudit, out error))
                        {
                            result.HasError = true;
                            result.Errors.Add(error);
                            scope.Rollback();
                            scope.Dispose();
                            return result;
                        }
                    }

                    scope.Commit();
                    scope.Dispose();
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.ToString());
                scope?.Rollback();
                scope?.Dispose();
            }


            result.Data = applicantJson;
            return result;
        }

        private bool GetGenerateStudentIdAndUgcId(AddNewStudentStep1Json applicantJson,Adm_AdmissionExam admExam,Aca_Program selectedProgram,out string error)
        {
            error = string.Empty;
            var username = "";

            if (!applicantJson.IsFullManualId && SiteSettings.Instance.StudentIdGenerateType == SiteSettings.StudentIdGenerateTypeEnum.FullAutoGenerate)
            {
                if (!Facade.AcademicFacade.StudentFacade.GenerateStudentUserName(admExam.StudentIdPrefix,
                        selectedProgram.Code, out username, out error))
                {
                    return false;
                };
                applicantJson.Id = username;
                applicantJson.UserName = username;
            }
            else if (!applicantJson.IsFullManualId && SiteSettings.Instance.StudentIdGenerateType == SiteSettings.StudentIdGenerateTypeEnum.FullAutoGenerateAsUGC)
            {
                
                applicantJson.Id = username;
                applicantJson.UserName = username;
            }

            //Here deciding if UGC unique id need to generate or not.
            if (SiteSettings.Instance.AddNewApplicantFormSettings.IsAutoGenerateUgcId &&
                !(SiteSettings.Instance.StudentIdGenerateType == SiteSettings.StudentIdGenerateTypeEnum.FullAutoGenerateAsUGC
                  || SiteSettings.Instance.StudentIdGenerateType == SiteSettings.StudentIdGenerateTypeEnum.PrefixAutoGenerateAsUGC))
            {
                var ugcUniqueId = "";
                
                applicantJson.UgcUniqueId = ugcUniqueId;
            }
            else
            {
                applicantJson.UgcUniqueId = applicantJson.UserName;
            }

            return true;
        }

        private bool SaveCourseList(int studentId,int programId,long semesterId,int classSectionId, DbContextTransaction scope, out string error)
        {
            error = string.Empty;
            List<int> studentIdList = new List<int>();

            studentIdList.Add(studentId);

            var registrableClassIdList = DbInstance.Aca_Class
                .Where(x => x.ProgramId == programId && x.SemesterId == semesterId &&
                            x.ClassSectionId == classSectionId 
                            && x.StudyLevelTermId == 1 && !x.IsDeleted
                            && x.StatusEnumId == (byte)Aca_Class.StatusEnum.Open)
                .Select(x => x.Id).ToList();

            if (registrableClassIdList.Count <= 0)
            {
                error += "Creating Applicant Un-Successful. No Registrable Offered Courses Found for 1st Semester of Selected Program.";
                return false;
            }
            return true;
        }
        private bool IsValidToSaveNewApplicant(AddNewStudentStep1Json newObj, out string error)
        {
            error = string.Empty;

            if (newObj == null)
            {
                error = "Invalid Operation! Reload the page and try again later";
                return false;
            }
            long output;
            //Id = "0",//done
            //UserName = "", /done
            //FullName = "",/done
            //AdmissionExamId = null, done
            //ProgramId = "",done//List
            //EnrollmentTypeEnumId = 0 done,
            //FeeCodeId = 0,//done
            //GenderEnumId=1,//done
            // ClassSection =0 //done
            if (newObj.AdmissionExamId == null || newObj.AdmissionExamId <= 0)
            {
                error += "Please Select Valid Admission Circular.";
                return false;
            }
            long.TryParse(newObj.ProgramId, out output);
            if (!newObj.ProgramId.IsValid() || output <= 0)
            {
                error += "Please Select Valid Program.";
                return false;
            }
            if (newObj.FeeCodeId == null || newObj.FeeCodeId <= 0)
            {
                error += "Please Select Valid Fee Code.";
                return false;
            }
            if (newObj.GenderEnumId == (byte)User_Account.GenderEnum.Unknown)
            {
                error += "Please select valid Gender.";
                return false;
            }

            //Student Id and UGC Unique Id checking.
            if (!(SiteSettings.Instance.StudentIdGenerateType == SiteSettings.StudentIdGenerateTypeEnum.FullAutoGenerate 
                || SiteSettings.Instance.StudentIdGenerateType == SiteSettings.StudentIdGenerateTypeEnum.FullAutoGenerateAsUGC))
                if (newObj.IsFullManualId)
                {
                    //when full manual
                    if (!newObj.UserName.IsValid())
                    {
                        error += $"Please type valid Student ID.";
                        return false;
                    }

                    if (newObj.UserName.Length < 3)
                    {
                        error = "Please Provide at least 3 character in ID.";
                        return false;
                    }

                    if (!Regex.IsMatch(newObj.UserName, "^[A-Z\\d-]+$"))
                    {
                        error = "Invalid character found in student ID filed. Please provide only capital A-Z, 0-9 as ID.";
                        return false;
                    }
                    //if (!newObj.UserName.IsInt64())
                    //{
                    //    error += $"Please type only number as Student ID.";
                    //    return false;
                    //}

                }
                else
                {
                    if (SiteSettings.Instance.StudentIdGenerateType == SiteSettings.StudentIdGenerateTypeEnum.PrefixAutoGenerate 
                        || SiteSettings.Instance.StudentIdGenerateType == SiteSettings.StudentIdGenerateTypeEnum.PrefixAutoGenerateAsUGC)
                    {
                        if (!newObj.UserName.IsValid())
                        {
                            error += $"Please type only last {SiteSettings.Instance.StudentIdSerialDigitCount}-digit of the Student ID.";
                            return false;
                        }
                        if (!newObj.UserName.IsInt64())
                        {
                            error += $"Please type only number for last {SiteSettings.Instance.StudentIdSerialDigitCount}-digit of the Student ID.";
                            return false;
                        }
                        if (newObj.UserName.Trim().Length != SiteSettings.Instance.StudentIdSerialDigitCount)
                        {
                            error += $"Please type only last {SiteSettings.Instance.StudentIdSerialDigitCount}-Digit of Student ID.";
                            return false;
                        }

                    }

                }// end IsFullManualId if

            if (!newObj.FullName.IsValid())
            {
                error += "Full Name is not valid.";
                return false;
            }
            if (newObj.FullName.Length < 4)
            {
                error += "Full Name must be at least 4 Character.";
                return false;
            }
            if (newObj.FullName.Length > 128)
            {
                error += "Full Name exceeded its maximum length 128.";
                return false;
            }
            newObj.FullName = newObj.FullName.ValidateName();

            if ((newObj.EnrollmentTypeEnumId == null || newObj.EnrollmentTypeEnumId <0) && SiteSettings.Instance.AddNewApplicantFormSettings.IsEnableEnrollTypeSelection)
            {
                error += "Please select valid Enrollment Type.";
                return false;
            }
            else if (!SiteSettings.Instance.AddNewApplicantFormSettings.IsEnableEnrollTypeSelection)
            {
                newObj.EnrollmentTypeEnumId = (int) User_Student.EnrollmentTypeEnum.FreshStudent;
            }

            long.TryParse(newObj.CurriculumId, out output);
            if ((newObj.CurriculumId == null || output<=0) && !SiteSettings.Instance.AddNewApplicantFormSettings.IsAutoAllocateSyllabus)
            {
                error += "Please Select Valid Curriculum / Syllabus.";
                return false;
            }


            if ((newObj.ContinuingBatchId == null || newObj.ContinuingBatchId <= 0) && SiteSettings.Instance.AddNewApplicantFormSettings.IsEnableDeptBatchSelection)
            {
                error += "Please Select Valid Batch.";
                return false;
            }
            else if (!SiteSettings.Instance.AddNewApplicantFormSettings.IsEnableDeptBatchSelection)
            {
                newObj.ContinuingBatchId = 1;
            }

            if ((newObj.ClassSectionId == null || newObj.ClassSectionId <= 0) && !SiteSettings.Instance.AddNewApplicantFormSettings.IsAutoClassSection)
            {
                error += "Please Select Valid Class Section.";
                return false;
            }
            else if (!SiteSettings.Instance.AddNewApplicantFormSettings.IsAutoClassSection)
            {
                newObj.ClassSectionId = 1;
            }

            if ((!newObj.MobileNo.IsValid() || !newObj.MobileNo.IsValidMobileForBD()) && SiteSettings.Instance.AddNewApplicantFormSettings.IsEnableMobileInput)
            {
                error += "Please Provide Valid Mobile No.";
                return false;
            }
            else if (!SiteSettings.Instance.AddNewApplicantFormSettings.IsEnableMobileInput)
            {
                newObj.MobileNo = null;
            }

            if ((!newObj.EmailAddress.IsValid() || !newObj.EmailAddress.IsValidEmail()) && SiteSettings.Instance.AddNewApplicantFormSettings.IsEnableEmailInput)
            {
                error += "Please Provide Valid Email Address.";
                return false;
            }
            else if (!SiteSettings.Instance.AddNewApplicantFormSettings.IsEnableEmailInput)
            {
                newObj.EmailAddress = null;
            }

            if ((newObj.CampusId == null || newObj.CampusId <=0) && SiteSettings.Instance.AddNewApplicantFormSettings.IsEnableCampusSelection)
            {
                error += "Please Select a  Valid Campus.";
                return false;
            }
            else if (!SiteSettings.Instance.AddNewApplicantFormSettings.IsEnableCampusSelection)
            {
                newObj.CampusId = 1;
            }

            if (!SiteSettings.Instance.AddNewApplicantFormSettings.IsEnableRemarkInput)
            {
                newObj.Remark = null;
            }

            if (SiteSettings.Instance.AddNewApplicantFormSettings.IsEnablePreviousEducationInput)
            {
                if (newObj.SscDegreeCategoryId <= 0)
                {
                    error += "Please Select a Valid SSC Degree Category.";
                    return false;
                }

                var isValidSscDegreeCategory = DbInstance.Adm_DegreeCategory
                    .Any(x => x.Id == newObj.SscDegreeCategoryId && x.DegreeEquivalentEnumId ==
                        (byte)User_Education.DegreeEquivalentEnum.Ssc10YearEquivalent);

                if (!isValidSscDegreeCategory)
                {
                    error += "SSC Degree Category Not found.";
                    return false;
                }

                if (!(newObj.SscGpa>0&& newObj.SscGpa<=5))
                {
                    error += "Invalid SSC. Gpa.";
                    return false;
                }

                if (newObj.HscDegreeCategoryId > 0)
                {
                    var isValidHscDegreeCategory = DbInstance.Adm_DegreeCategory
                        .Any(x => x.Id == newObj.HscDegreeCategoryId && x.DegreeEquivalentEnumId ==
                            (byte)User_Education.DegreeEquivalentEnum.Hsc12YearEquivalent);

                    if (!isValidHscDegreeCategory)
                    {
                        error += "Invalid HSC Degree Category.";
                        return false;
                    }
                }
                
                if (newObj.HscGpa != 0 && !(newObj.HscGpa > 0 && newObj.HscGpa <= 5))
                {
                    error += "Invalid HSC. Gpa.";
                    return false;
                }
            }

            if (newObj.EnrollmentTypeEnumId == (byte) User_Student.EnrollmentTypeEnum.InternalDepartmentChange)
            {

                var previousStudent = DbInstance.User_Student.FirstOrDefault(x =>
                    x.User_Account.UserName.Trim() == newObj.PreviousStudentUserName.Trim());

                if (previousStudent == null)
                {
                    error += "No Id found With Previous Student Id.";
                    return false;
                }

                if (previousStudent.AdmissionStatusEnumId != (byte)User_Student.AdmissionStatusEnum.AdmissionCancelled)
                {
                    error += $"Students Previous Id's Admission is not Cancelled. Please Cancel that first.";
                    return false;
                }

                if (!newObj.PreviousStudentUserName.IsValid())
                {
                    error += $"Please type valid Previous Student ID.";
                    return false;
                }

                if (newObj.PreviousStudentUserName.Length < 3)
                {
                    error = "Please Provide at least 3 character in Previous ID.";
                    return false;
                }

                if (!Regex.IsMatch(newObj.PreviousStudentUserName, "^[A-Z\\d-]+$"))
                {
                    error = "Invalid character found in Previous student ID filed. Please provide only capital A-Z, 0-9 as ID.";
                    return false;
                }

                long.TryParse(newObj.PreviousProgramId, out output);
                if (!newObj.PreviousProgramId.IsValid() || output <= 0)
                {
                    error += "Please Select a Valid Previous Program.";
                    return false;
                }

                if (newObj.PreviousProgramId == newObj.ProgramId)
                {
                    error += "Previous Program and Current Program Cannot be same.";
                    return false;
                }

                newObj.PreviousProgramId = previousStudent.PreviousProgramId?.ToString();
            }
            else
            {
                newObj.PreviousStudentUserName = null;
                newObj.PreviousProgramId = null;
            }

            error = string.Empty;
            return true;
        }

        private static User_Account GetNewUser_Account(AddNewStudentStep1Json applicantJson, Aca_Program selectedProgram, Adm_AdmissionExam admExam)
        {
            User_Account obj = new User_Account();
            //obj.Id = int.Parse(applicantJson.UserName);
            obj.UserName = applicantJson.UserName;//todo make 9 digit and check avaiable
            obj.FullName = applicantJson.FullName;//
            obj.BanglaName = "";
            obj.DateOfBirth = new DateTime(1753, 1, 1, 12, 0, 0, 0);
            obj.PlaceOfBirthCity = null;
            obj.PlaceOfBirthCountryId = 1;//
            obj.GenderEnumId = applicantJson.GenderEnumId;
            //obj.Gender = User_Account.GenderEnum.Unknown;
            //obj.ReligionEnumId = null;
            obj.Religion = User_Account.ReligionEnum.Unknown;
            //obj.BloodGroupEnumId = null;
            obj.BloodGroup = EnumCollection.Common.BloodGroupEnum.Unknown;
            //obj.MaritalStatusEnumId = null;
            obj.MaritalStatus = User_Account.MaritalStatusEnum.Unknown;
            obj.FatherName = string.Empty;
            obj.MotherName = string.Empty;
            obj.SpouseName = null;

            obj.UserMobile = applicantJson.MobileNo;

            obj.UserMobilePrivacyEnumId = 0;
            obj.UserMobilePrivacy = User_Account.UserContactPrivacyEnum.AdminstrationOnly;
            obj.IsVerifiedUserMobile = false;
            obj.UserEmail = applicantJson.EmailAddress;
            obj.UserEmailPrivacyEnumId = (byte)User_Account.UserContactPrivacyEnum.AdminstrationOnly; ;
            obj.IsVerifiedUserEmaill = false;
            obj.EmergencyContactPersonName = null;
            obj.EmergencyMobile = null;
            obj.EmergencyContactAdress = null;
            obj.EmergencyContactRelationshipId = null;
            obj.Height = null;
            obj.IsLate = false;
            obj.CampusId = applicantJson.CampusId;//
            obj.Nationality = "Bangladeshi";//
            obj.NationalIdNumber = null;
            obj.BirthCertificateNumber = null;
            obj.BankAccountNo = null;
            obj.BankName = null;
            obj.PassportNo = null;
            obj.UserTypeEnumId = 0;
            obj.UserType = User_Account.UserTypeEnum.Applicant;//todo update in 2nd step//Student=admission fee paid
            obj.UserStatusEnumId = 0;
            obj.UserStatus = User_Account.UserStatusEnum.Inactive;//todo update in 2nd step//Active=admission fee paid,
            obj.IsMigrate = false;
            obj.MigrationId = null;
            obj.IsProfileCompleted = false;//todo update in 2nd step//True=All field validate 
            obj.IsUserCanEditProfile = false;
            obj.IsDocumentPending = true;//updated
            obj.IsApproved = false;//todo update in 2nd step//true=admission fee paid,
            obj.Password = RandomPassword.GenerateOnlyNumber(5).ToString();
            obj.PasswordSalt = string.Empty;
            obj.LastLoginDate = new DateTime(1753, 1, 1, 12, 0, 0, 0);//make it null
            obj.LastPasswordChangedDate = new DateTime(1753, 1, 1, 12, 0, 0, 0);//make it null
            obj.LastLockoutDate = new DateTime(1753, 1, 1, 12, 0, 0, 0);//make it null
            obj.FailedPasswordAttemptCount = 0;
            obj.FailedPasswordAttemptWindowStart = new DateTime(1753, 1, 1, 12, 0, 0, 0);//make it null
            obj.FailedPasswordAnswerAttemptCount = 0;
            obj.FailedPasswordAnswerAttemptWindowStart = new DateTime(1753, 1, 1, 12, 0, 0, 0);//make it null
            obj.Remarks = "";
            obj.LockReason = null;
            obj.JoiningSemesterId = admExam.SemesterId;


            obj.JoiningDate = DateTime.Now;
            obj.LeavingDate = null;

            obj.CreateDate = DateTime.Now;
            obj.CreateById = HttpUtil.Profile.UserId;

            obj.LastChanged = DateTime.Now;
            obj.LastChangedById = HttpUtil.Profile.UserId;
            obj.History = null;
            obj.ActiveRfidNo = null;
            obj.ProfilePictureImageUrl = null;
            obj.SignatureImageUrl = null;
            obj.IsForceTwoFactorAuth = false;
            //obj.RankId = null;
            return obj;
        }

        private User_Student GetNewUser_Student(AddNewStudentStep1Json applicantJson, User_Account account, Adm_AdmissionExam admExam, Aca_Program selectedProgram, Aca_GradingPolicy gradingPolicy, Aca_Curriculum curriculum)
        {

            User_Student obj = User_Student.GetNew();
            obj.Id = account.Id;//
            obj.UserId = account.Id;//
            obj.ClassRollNo = account.UserName;//
            var year = (admExam.Aca_Semester.Year.ToString().IsValid() && admExam.Aca_Semester.Year.ToString().Length == 4 ? admExam.Aca_Semester.Year : DateTime.Now.Year).ToString();
            year = year.Substring(year.Length - 2);
            obj.RegistrationNo = $"{year}{account.Id.ToStringPadLeft(7, '0')}";// account.UserName;
            obj.RegistrationSession = admExam.Aca_Semester.AcademicSession;//
            obj.GradingPolicyId = gradingPolicy.Id;
            obj.CGPA = 0.0F;
            obj.CourseCompleted = 0.0F;
            obj.CreditCompleted = 0.0F;
            obj.IncompleteCredits = 0.0F;
            obj.LevelTermId = 1;//updated
            

            
            obj.ProgramId = selectedProgram.Id;

            obj.ContinuingBatchId = applicantJson.ContinuingBatchId;
            obj.JoiningBatchId = applicantJson.ContinuingBatchId;

            obj.ClassTimingGroupEnumId = selectedProgram.ClassTimingGroupEnumId;
            obj.FeeCodeId = applicantJson.FeeCodeId;
            obj.PerCreditFee = 0;
            obj.ClassSectionId = applicantJson.ClassSectionId;

            obj.IsFeesVerified = false;//todo check it 2nd step
            obj.CurriculumId = curriculum.Id;//
            obj.MajorCurriculumId = null;
            obj.SecondMajorCurriculumId = null;
            obj.MinorCurriculumId = null;
            obj.ElectiveCurriculumId = null;
            obj.StudentQuotaNameId = null;//todo check it 2nd step
            obj.EnrollmentStatusEnumId = 0;
            obj.EnrollmentStatus = User_Student.EnrollmentStatusEnum.Applicant;//todo change it in 2nd step
            obj.EnrollmentTypeEnumId = applicantJson.EnrollmentTypeEnumId;
            obj.ParentsPrimaryJobTypeId = null;

            obj.FatherMobile = null;
            obj.MotherMobile = null;
            obj.AdmissionExamId = applicantJson.AdmissionExamId;
            obj.AdmissionTestRollNo = account.UserName;
            obj.AdmissionFormNo = null;
            obj.AdmissionStatusEnumId = 0;
            obj.AdmissionStatus = User_Student.AdmissionStatusEnum.AdmissionPending;//todo remove//change it in 2nd step
            obj.AdmissionRemark = applicantJson.Remark;
            obj.IsAdmissionFeePaid = false;//todo check it 2nd step//manually recheck button+ set as paid
            obj.CertificateRegistrationNo = null;

            obj.CreateDate = DateTime.Now;
            obj.CreateById = HttpUtil.Profile.UserId;

            obj.LastChanged = DateTime.Now;
            obj.LastChangedById = HttpUtil.Profile.UserId;


            //Ems 4 Update
            obj.TranscriptSerialNo = string.Empty;
            obj.OriginalCertificateSerialNo = string.Empty;
            obj.OriginalCertificateVerifyNo = string.Empty;
            obj.ProvisionalCertificateSerialNo = string.Empty;
            obj.ProvisionalCertificateVerifyNo = string.Empty;
            obj.JoiningBatchId = applicantJson.ContinuingBatchId;
            obj.PreviousProgramId = null;
            obj.PreviousStudentUserName = null;
            obj.IsRequiredCGPACalculation = true;
            obj.LastCGPASyncTime = null;
            obj.ForceDisableOnlineRegistration = false;
            obj.EnableOpenCreditRegistration = false;

            if (applicantJson.PreviousProgramId.IsValid())
            {
                int previousProgramId = 0;
                Int32.TryParse(applicantJson.PreviousProgramId, out previousProgramId);
                obj.PreviousProgramId = previousProgramId;
            }

            obj.UgcUniqueId = applicantJson.UgcUniqueId;

            obj.PreviousStudentUserName = applicantJson.PreviousStudentUserName;
            return obj;
        }


        #region Discount/Scholarship
        public class AmountInputType
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool Value { get; set; }
        }
        public List<AmountInputType> AmountInputTypeList()
        {
            var objList = new List<AmountInputType>();
            var obj = new AmountInputType
            {
                Id = 1,
                Name = "In Amount",
                Value = false
            };

            objList.Add(obj);

            obj = new AmountInputType
            {
                Id = 2,
                Name = "In Percent",
                Value = true
            };

            objList.Add(obj);


            return objList;
        }
        public enum AmountInputTypeEnum
        {
            InAmount = 0,
            InPercent = 1
        }
        #endregion

    }

}
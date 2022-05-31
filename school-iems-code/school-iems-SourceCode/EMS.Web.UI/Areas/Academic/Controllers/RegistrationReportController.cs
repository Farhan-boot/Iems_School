using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.DataAccess.Data;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases.Mvc;
using Microsoft.Ajax.Utilities;

namespace EMS.Web.UI.Areas.Academic.Controllers
{
    public class RegistrationReportController : EmployeeBaseController
    {
        // GET: Academic/RegistrationReport
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult ProgramWiseRegistrationStatusSummary()
        {

            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanViewReport))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        public ActionResult LevelTermWiseRegistrationManager()
        {

            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanViewReport))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }
        public ActionResult LevelTermWiseRegistrationSummary()
        {

            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanViewReport))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }

        public ActionResult SectionWiseRegStatusSummary()
        {

            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanViewReport))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }



        public ActionResult RegistrationPendingReport()
        {

            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanViewReport))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }

        public ActionResult SemesterDropReport()
        {

            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanViewReport))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }



        public ActionResult SemesterWiseRegistrationReportManager()
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanViewReport))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }
            return View();
        }

        public class SemesterWiseRegistrationReportModel
        {
            public string ProgramName { get; set; }
            public string SemesterName { get; set; }
            public string BatchName { get; set; }
            public List<RegStudent> RegStudentList { get; set; }

            public static SemesterWiseRegistrationReportModel GetNew()
            {
                var obj = new SemesterWiseRegistrationReportModel
                {
                    RegStudentList = new List<RegStudent>()
                };

                return obj;

            }


        }
        public class RegStudent
        {
            public string FullName { get; set; }
            public string UserName { get; set; }
            public string MobileNumber { get; set; }
            public string Courses { get; set; }
            public string OtherLevelTermCourses { get; set; }
            public float TotalCredit { get; set; }
        }

        

        public ActionResult SemesterWiseRegistrationReportPrint(int semesterId=0, long programId = 0,int ContinuingBatchId = 0)
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanViewReport))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }

            string error = "";
            var result = new MvcModelListResult<SemesterWiseRegistrationReportModel>();
           
            if (semesterId <= 0)
            {
                result.HasError = true;
                result.Errors.Add("Invalid Semester Selected!");
                return View(result);
            }

            if (programId <= 0)
            {
                result.HasError = true;
                result.Errors.Add("Invalid Program Selected!");
                return View(result);
            }


           
            try
            {

                var semester = DbInstance.Aca_Semester.Find(semesterId);
                if (semester == null)
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid Semester Selected!");
                    return View(result);
                }

                var program = DbInstance.Aca_Program.Find(programId);
                if (program==null)
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid Program Selected!");
                    return View(result);
                }

                



                var classList = DbInstance.Aca_Class
                    .Include(x=>x.Aca_ClassSection)
                    .Include(x=>x.Aca_StudyLevelTerm)
                    .Include(x=>x.Aca_CurriculumCourse)
                    .Where(x => x.ProgramId == programId
                                && x.SemesterId==semesterId).ToList();


                IQueryable<Aca_ClassTakenByStudent> classTakenStudentListQuery = DbInstance.Aca_ClassTakenByStudent
                    .Include(x => x.User_Student)
                    .Include(x => x.User_Student.Aca_DeptBatch)
                    .Include(x => x.User_Student.User_Account)
                    .Where(x => 
                        x.Aca_Class.SemesterId==semesterId
                        && x.Aca_Class.ProgramId==programId
                        && x.EnrollTypeEnumId == (byte) Aca_ClassTakenByStudent.EnrollTypeEnum.Regular
                        && (x.RegistrationStatusEnumId == (byte)Aca_ClassTakenByStudent.RegistrationStatusEnum.FreshRegistration
                            || x.RegistrationStatusEnumId == (byte)Aca_ClassTakenByStudent.RegistrationStatusEnum.RetakeRegistration
                            || x.RegistrationStatusEnumId == (byte)Aca_ClassTakenByStudent.RegistrationStatusEnum.ImprovementRegistration
                        )
                    );

                if (ContinuingBatchId > 0)
                {
                    classTakenStudentListQuery =
                        classTakenStudentListQuery.Where(x => x.User_Student.ContinuingBatchId == ContinuingBatchId);

                }




                var classTakenStudentList = classTakenStudentListQuery.ToList();
                if (classTakenStudentList.Count<=0)
                {
                    result.HasError = true;
                    result.Errors.Add("Registered Student Not Found!");
                    return View(result);
                }

                //Batch List
                var batchList = classTakenStudentList.DistinctBy(x => x.User_Student.Aca_DeptBatch)
                    .Select(x => x.User_Student.Aca_DeptBatch).ToList();

                batchList = batchList.OrderBy(x => x.BatchNo).ToList();


                var registeredDistinctStudentList = classTakenStudentList.DistinctBy(x => x.StudentId).ToList();

                var semesterWiseRegModelList = new List<SemesterWiseRegistrationReportModel>();

               
                // batch loop start
                foreach (var batch in batchList)
                {
                    var semesterWiseRegModel = SemesterWiseRegistrationReportModel.GetNew();

                    semesterWiseRegModel.SemesterName = semester.Name;
                    semesterWiseRegModel.BatchName = batch.Name;
                    semesterWiseRegModel.ProgramName = program.ShortTitle;


                    var thisBatchStudentList = registeredDistinctStudentList
                        .Where(x => x.User_Student.ContinuingBatchId == batch.Id)
                        .Select(x=>x.User_Student).ToList();

                    thisBatchStudentList = thisBatchStudentList
                        .OrderBy(x => x.User_Account.UserName).ToList();

                    // batch wise student loop start
                    foreach (var student in thisBatchStudentList)
                    {
                        RegStudent regStudent = new RegStudent();

                        regStudent.FullName = student.User_Account.FullName;
                        regStudent.UserName = student.User_Account.UserName;

                        var thisStdRegisteredClassList =
                            classTakenStudentList.Where(x => x.StudentId == student.Id)
                                .Select(x=>x.Aca_Class)
                                .ToList();

                        //get Course Code loop start
                        foreach (var regClass in thisStdRegisteredClassList)
                        {
                            regStudent.Courses +=
                                $"{regClass.Code} [{regClass.Aca_ClassSection.Name},{regClass.Aca_CurriculumCourse.CreditLoad},{regClass.Aca_StudyLevelTerm.ShortName}]; ";

                            regStudent.TotalCredit += regClass.Aca_CurriculumCourse.CreditLoad;

                        } //get Course Code loop end

                        semesterWiseRegModel.RegStudentList.Add(regStudent);
                    } // batch wise student loop end

                    semesterWiseRegModelList.Add(semesterWiseRegModel);
                }// batch loop end



                result.Data = semesterWiseRegModelList;
                return View(result);


            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
                return View(result);
            }
           
        }


        public class LevelTermWiseRegistrationReportModel
        {
            public string ProgramName { get; set; }
            public string SemesterName { get; set; }
            public string LevelTermName { get; set; }
            public List<RegStudent> RegStudentList { get; set; }

            public static LevelTermWiseRegistrationReportModel GetNew()
            {
                var obj = new LevelTermWiseRegistrationReportModel
                {
                    RegStudentList = new List<RegStudent>()
                };

                return obj;

            }


        }
        public ActionResult LevelTermWiseRegistrationReportPrint(int semesterId = 0, long programId = 0, int LevelTermId = 0)
        {
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanViewReport))
            {
                return RedirectToAction("PermissionDenied", "Home", new { area = "" });
            }

            string error = "";
            var result = new MvcModelListResult<LevelTermWiseRegistrationReportModel>();

            if (semesterId <= 0)
            {
                result.HasError = true;
                result.Errors.Add("Invalid Semester Selected!");
                return View(result);
            }

            if (programId <= 0)
            {
                result.HasError = true;
                result.Errors.Add("Invalid Program Selected!");
                return View(result);
            }
            try
            {

                var semester = DbInstance.Aca_Semester.Find(semesterId);
                if (semester == null)
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid Semester Selected!");
                    return View(result);
                }

                var program = DbInstance.Aca_Program.Find(programId);
                if (program == null)
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid Program Selected!");
                    return View(result);
                }

                var classList = DbInstance.Aca_Class
                    .Include(x => x.Aca_ClassSection)
                    .Include(x => x.Aca_StudyLevelTerm)
                    .Include(x => x.Aca_CurriculumCourse)
                    .Where(x => x.ProgramId == programId
                                && x.SemesterId == semesterId).ToList();


                IQueryable<Aca_ClassTakenByStudent> classTakenStudentListQuery = DbInstance.Aca_ClassTakenByStudent
                    .Include(x => x.User_Student)
                    .Include(x => x.User_Student.User_Account)
                    .Where(x =>
                        x.Aca_Class.SemesterId == semesterId
                        && x.Aca_Class.ProgramId == programId
                         && !x.User_Student.IsDeleted
                        && x.EnrollTypeEnumId == (byte)Aca_ClassTakenByStudent.EnrollTypeEnum.Regular
                        && (x.RegistrationStatusEnumId == (byte)Aca_ClassTakenByStudent.RegistrationStatusEnum.FreshRegistration
                            || x.RegistrationStatusEnumId == (byte)Aca_ClassTakenByStudent.RegistrationStatusEnum.RetakeRegistration
                            || x.RegistrationStatusEnumId == (byte)Aca_ClassTakenByStudent.RegistrationStatusEnum.ImprovementRegistration
                        )
                    );

                if (LevelTermId > 0)
                {
                    classTakenStudentListQuery =
                        classTakenStudentListQuery.Where(x => x.Aca_Class.StudyLevelTermId == LevelTermId);
                }

                var classTakenStudentList = classTakenStudentListQuery.ToList();
                if (classTakenStudentList.Count <= 0)
                {
                    result.HasError = true;
                    result.Errors.Add("Registered Student Not Found!");
                    return View(result);
                }

                //Batch List
                var levelTermList = classTakenStudentList.DistinctBy(x => x.Aca_Class.StudyLevelTermId)
                    .Select(x => x.Aca_Class.Aca_StudyLevelTerm).ToList();

                levelTermList = levelTermList.OrderBy(x => x.Id).ToList();
                
                var levelTermWiseRegModelList = new List<LevelTermWiseRegistrationReportModel>();


                // levelTerm loop start
                foreach (var levelTerm in levelTermList)
                {
                    var levelTermWiseRegModel = LevelTermWiseRegistrationReportModel.GetNew();

                    levelTermWiseRegModel.SemesterName = semester.Name;
                    levelTermWiseRegModel.LevelTermName = levelTerm.Name;
                    levelTermWiseRegModel.ProgramName = program.ShortTitle;

                    var thisLevelTermStudentList = classTakenStudentList
                      .Where(x => x.Aca_Class.StudyLevelTermId == levelTerm.Id)
                      .DistinctBy(x => x.StudentId)
                    .Select(x => x.User_Student).ToList();

                    thisLevelTermStudentList = thisLevelTermStudentList
                        .OrderBy(x => x.User_Account.UserName).ToList();

                    // levelTerm wise student loop start
                    foreach (var student in thisLevelTermStudentList)
                    {
                        RegStudent regStudent = new RegStudent();

                        regStudent.FullName = student.User_Account.FullName;
                        regStudent.UserName = student.User_Account.UserName;
                        regStudent.MobileNumber = student.User_Account.UserMobile;

                        var thisStdThisLevelTermRegisteredClassList =
                            classTakenStudentList.Where(x => x.StudentId == student.Id && x.Aca_Class.StudyLevelTermId == levelTerm.Id)
                                .Select(x => x.Aca_Class)
                                .ToList();

                        //get Course Code loop start
                        foreach (var regClass in thisStdThisLevelTermRegisteredClassList)
                        {
                            regStudent.Courses +=
                                $"{regClass.Code} [{regClass.Aca_ClassSection.Name},{regClass.Aca_CurriculumCourse.CreditLoad},{regClass.Aca_StudyLevelTerm.ShortName}]; ";

                            regStudent.TotalCredit += regClass.Aca_CurriculumCourse.CreditLoad;

                        } //get Course Code loop end

                        var thisStdOtherLevelTermRegisteredClassList =
                            classTakenStudentList.Where(x => x.StudentId == student.Id && x.Aca_Class.StudyLevelTermId != levelTerm.Id)
                                .Select(x => x.Aca_Class)
                                .ToList();

                        //get Course Code loop start
                        foreach (var regClass in thisStdOtherLevelTermRegisteredClassList)
                        {
                            regStudent.OtherLevelTermCourses +=
                                $"{regClass.Code} [{regClass.Aca_ClassSection.Name},{regClass.Aca_CurriculumCourse.CreditLoad},{regClass.Aca_StudyLevelTerm.ShortName}]; ";

                            regStudent.TotalCredit += regClass.Aca_CurriculumCourse.CreditLoad;

                        } //get Course Code loop end

                        levelTermWiseRegModel.RegStudentList.Add(regStudent);
                    } // levelTerm wise student loop end

                    levelTermWiseRegModelList.Add(levelTermWiseRegModel);
                }// levelterm loop end

          
                result.Data = levelTermWiseRegModelList;
                return View(result);


            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
                return View(result);
            }

        }

    }
}
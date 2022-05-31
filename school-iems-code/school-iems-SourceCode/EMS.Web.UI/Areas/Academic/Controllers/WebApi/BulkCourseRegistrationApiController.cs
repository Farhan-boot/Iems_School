using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using EMS.CoreLibrary;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.Facade.AcademicArea;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases.WebApi;
using EMS.Framework.Objects;
using EMS.Framework.Settings;
using EMS.Web.Jsons.Custom.Academic;
using EMS.Web.Jsons.Custom.Registration;
//using EMS.Web.Jsons.Mapper;
using EMS.Web.Jsons.Models;

namespace EMS.Web.UI.Areas.Academic.Controllers.WebApi
{
    public class BulkCourseRegistrationApiController : EmployeeBaseApiController
    {

        // GET api/<controller>
        public HttpResult<BulkRegistrationJson> GetBulkRegClassList(
             long semesterId = 0
           , int programId = 0
           , int studyLevelTermId = 0
           , int batchId = 0
           , int campusId = 0
           , int classSectionId = 0
         )
        {   //canview/add/edit
            string error = string.Empty;
            int count = 0;
            var result = new HttpResult<BulkRegistrationJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanAddFromBulkRegistration, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                if (programId <= 0)
                {
                    result.HasError = true;
                    result.Errors.Add("Please select a Program!");
                    return result;
                }
                if (semesterId <= 0)
                {
                    result.HasError = true;
                    result.Errors.Add("Please select a Semester!");
                    return result;
                }
                //if (batchId <= 0)
                //{
                //    result.HasError = true;
                //    result.Errors.Add("Please select a Batch!");
                //    return result;
                //}
                if (studyLevelTermId <= 0)
                {

                    result.HasError = true;
                    result.Errors.Add("Please select a Level Term!");
                    return result;
                }
                using (Aca_ClassDataService classDataService = new Aca_ClassDataService(DbInstance, HttpUtil.Profile))
                {
                    IQueryable<Aca_Class> query = (from q in DbInstance.Aca_Class select q)
                        .Include(x => x.Aca_CurriculumCourse)
                        .Include(x => x.Aca_CurriculumCourse.Aca_Curriculum)
                        //.Include(x => x.Aca_CurriculumCourse.Aca_Curriculum.Aca_Program.HR_Department)
                        //.Include(x => x.Aca_CurriculumCourse.HR_Department)
                        //.Include(x => x.Aca_Semester)
                        .Include(x => x.Aca_StudyLevelTerm)
                        .Include(x => x.Aca_ClassSection)
                        //.Include(x => x.Aca_ClassRoutine)
                        //.Include("Aca_ClassRoutine.General_Room")
                        //.Include("Aca_ClassRoutine.General_Room.General_Floor")
                        //.Include("Aca_ClassRoutine.General_Room.General_Building")
                        //.Include("Aca_ClassRoutine.General_Room.General_Building.General_Campus")
                        //.OrderBy(x => x.Aca_CurriculumCourse.Aca_Curriculum.DepartmentId)
                        //.ThenBy(x => x.Aca_CurriculumCourse.Aca_Curriculum.Aca_Program.ProgramTypeEnumId)
                        //.ThenByDescending(x => x.Aca_CurriculumCourse.Aca_Curriculum.Aca_Program.Id)
                        //.ThenByDescending(x => x.Aca_CurriculumCourse.CurriculumId)
                        //.ThenByDescending(x => x.SemesterId)
                        //.ThenBy(x => x.StudyLevelTermId)
                        //.ThenBy(x => x.Aca_CurriculumCourse.OfferedByDepartmentId)
                        //.ThenBy(x => x.Code)
                        //.ThenBy(x => x.ClassSectionId);
                        //.ThenBy(x => x.ClassNo);
                        .Where(x=>x.StatusEnumId==(byte)Aca_Class.StatusEnum.Open)
                        .OrderBy(x => x.ClassSectionId);

                    if (programId > 0)
                    {
                        query = query.Where(q => q.ProgramId == programId);
                
                    }

                    if (semesterId > 0)
                    {
                        query = query.Where(q => q.SemesterId == semesterId);
                    }

                    if (batchId > 0)
                    {
                        query = query.Where(q => q.StudentBatchId == batchId || q.StudentBatchId == null);
                    }

                    if (studyLevelTermId > 0)
                    {
                        query = query.Where(q => q.StudyLevelTermId == studyLevelTermId);
                    }

                    if (classSectionId > 0)
                    {
                        query = query.Where(q => q.ClassSectionId == classSectionId);
                    }

                    if (campusId > 0)
                    {
                        query = query.Where(q => q.CampusId == campusId);
                    }

                    var classList = classDataService.GetListByQuery(query, out error);
                    if (classList.Count <= 0)
                    {
                        result.HasError = true;
                        result.Errors.Add("No Class Found Using Your Search!");
                        return result;
                    }

                    //new code start

                    List<BulkRegCourseJson> courseList = new List<BulkRegCourseJson>();
                    foreach (var classObj in classList)
                    {
                        //// check ignore CourseWaiver 
                        //if (classObj.Aca_CurriculumCourse.CreditTypeEnumId == (byte)Aca_CurriculumCourse.CreditTypeEnum.CourseWaiver)
                        //{
                        //    continue;
                        //}

                        //check ignore other section
                        if (courseList.Any(x => x.CurriculumCourseId == classObj.CurriculumCourseId.ToString() && x.LevelTermId == classObj.StudyLevelTermId))
                        {
                            continue;
                        }
                        var courseJson = new BulkRegCourseJson();

                        courseJson.CurriculumCourseId = classObj.CurriculumCourseId.ToString();
                        courseJson.Name = classObj.Aca_CurriculumCourse.Name;
                        courseJson.Code = classObj.Aca_CurriculumCourse.Code;
                        courseJson.Credit = classObj.Aca_CurriculumCourse.CreditLoad;
                        courseJson.CourseCategory = classObj.Aca_CurriculumCourse.CourseCategory.ToString();
                        courseJson.LevelTermId = studyLevelTermId;
                        courseJson.LevelTermName = classObj.Aca_StudyLevelTerm.Name;
                        courseJson.Batch = classObj.StudentBatchId;
                        courseJson.SyllabusName = classObj.Aca_CurriculumCourse.Aca_Curriculum.ShortName;
                        //Adding Sections in list 

                        courseJson.SectionList = classList
                            .Where(x => x.CurriculumCourseId == classObj.CurriculumCourseId &&
                                        x.StudyLevelTermId == classObj.StudyLevelTermId).Select(x => new Aca_ClassSectionJson()
                            {
                                Id = x.Aca_ClassSection.Id,
                                Name = x.Aca_ClassSection.Name
                            })
                            .ToList();


                            courseJson.TotalSection = courseJson.SectionList.Count;

                        courseJson.TotalStudent = DbInstance.Aca_ClassTakenByStudent.Count(x =>
                        x.Aca_Class.CurriculumCourseId == classObj.CurriculumCourseId
                        && x.Aca_Class.StudyLevelTermId == classObj.StudyLevelTermId
                        && x.Aca_Class.SemesterId == classObj.SemesterId
                        //x.ClassId == classObj.Id
                        // && x.EnrollTypeEnumId == (byte)Aca_Exam.ExamTypeEnum.MidTerm todo need to re organize this problem
                        && x.RegistrationStatusEnumId != (byte)Aca_ClassTakenByStudent.RegistrationStatusEnum.Cancelled
                        && x.RegistrationStatusEnumId != (byte)Aca_ClassTakenByStudent.RegistrationStatusEnum.Drop
                        );
                        courseList.Add(courseJson);
                    }


                    //new code end
                    var totalCredit = courseList.Sum(x => x.Credit);
                    result.DataExtra.TotalCredit = totalCredit;

                    var registrationJson = new BulkRegistrationJson();
                    registrationJson.SemesterId = semesterId;
                    registrationJson.ProgramId = programId;
                    registrationJson.LevelTermId = studyLevelTermId;
                    registrationJson.BatchId = batchId;
                    registrationJson.CourseList = courseList;
                    registrationJson.StudentList = new List<BulkRegStudentJson>();

                    result.Data = registrationJson;

                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }


        public enum StudentLoadOnEnum
        {
            BatchWise = 1,
            LevelTermWise = 2,
            AdmissionSemesterWise = 3
        }

        public HttpListResult<Aca_ClassJson> GetBulkRegClassDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_ClassJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanAddFromBulkRegistration, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                //Aca_ClassDataService classDataService =
                //    new Aca_ClassDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List

                var semesterList = Aca_Semester.GetDropdownList(DbInstance);

                if (semesterList.Count <= 0)
                {
                    result.HasError = true;
                    result.Errors.Add("There is no semester found In the System!");
                    return result;
                }

                result.DataExtra.CurrentSemesterId = semesterList.First().Id;

                result.DataExtra.SemesterList = semesterList;


                result.DataExtra.ProgramList = DbInstance.Aca_Program.AsEnumerable()
                               .OrderBy(x => x.ShortName)
                               .Where(x => !x.IsDeleted)
                               .Select(t => new
                               { Id = t.Id, Name = t.Name, ShortName = t.ShortName, ShortTitle = t.ShortTitle,})
                               .ToList();

                result.DataExtra.StudyLevelTermList = DbInstance.Aca_StudyLevelTerm.OrderBy(x => x.Id).AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.Name }).ToList();

                result.DataExtra.DeptBatchList = DbInstance.Aca_DeptBatch.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.Name }).ToList();

                result.DataExtra.ProgramTypeEnumList = EnumUtil.GetEnumList(typeof(Aca_Program.ProgramTypeEnum));
                result.DataExtra.StudentLoadOnEnumList = EnumUtil.GetEnumList(typeof(StudentLoadOnEnum));

                //DropDown Option Tables

                result.DataExtra.ClassSectionList = DbInstance.Aca_ClassSection.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.Name }).ToList();



                //Extra Not requred now
                result.DataExtra.StatusEnumList = EnumUtil.GetEnumList(typeof(Aca_Class.StatusEnum));

                result.DataExtra.CampusList = DbInstance.General_Campus.Where(x => !x.IsDeleted).AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.ShortName }).ToList();

                //result.DataExtra.DepartmentList = DbInstance.HR_Department.AsEnumerable()
                //    .OrderBy(x => x.ShortName)
                //    .Where(x => x.TypeEnumId == 2)
                //    .Select(t => new
                //    { Id = t.Id, Name = t.Name, ShortName = t.ShortName })
                //    .ToList();
                var jsons = new User_StudentJson();
                var student = User_Student.GetNew(0);
                student.Map(jsons);
                result.DataExtra.BlankStudent = jsons;

            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        //Class Student
        [HttpPost]
        public HttpResult<BulkRegStudentJson> GetStudentById(BulkRegistrationJson registrationJson)
        {

            Debug.WriteLine($"GetStudentById: Start:" + DateTime.Now.ToString("T"));
            //string error = string.Empty;
            var result = new HttpResult<BulkRegStudentJson>();
            string error = "";

            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanAddFromBulkRegistration, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            try
            {
                if (registrationJson == null)
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid Registration Object.");
                    return result;
                }
                if (!registrationJson.TmpStudentId.IsValid())
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid Student ID.");
                    return result;
                }
                if (registrationJson.TmpStudentId.Length <= 3)
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid Student ID.");
                    return result;
                }
                var student = DbInstance.User_Student
                    .Include(x => x.User_Account)
                    .Include(x => x.Aca_DeptBatch)
                    .Include(x => x.Aca_ClassSection)
                    //.Include(x => x.Aca_Program.Acnt_OfficialBank)
                    .SingleOrDefault(x =>  x.IsDeleted == false 
                                           && x.User_Account.UserTypeEnumId==(byte)User_Account.UserTypeEnum.Student 
                                           && x.User_Account.UserName == registrationJson.TmpStudentId
                    );

                if (student == null)
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid Student ID.");
                    return result;
                }
      
                //todo check credit transfer
                if (student.EnrollmentStatus != User_Student.EnrollmentStatusEnum.Continuing)
                {
                    result.HasError = true;
                    result.Errors.Add($"Student's  Enrollment Status ({student.EnrollmentStatus}) is Invalid!");
                    return result;
                }
                //if (student.User_Account.UserStatus != User_Account.UserStatusEnum.Active)
                //{
                //    result.HasError = true;
                //    result.Errors.Add($"Student's  Profile Status ({student.User_Account.UserStatus}) is Invalid!");
                //    return result;
                //}

                //if (student.ContinuingBatchId != registrationJson.BatchId)
                //{
                //    result.HasError = true;
                //    result.Errors.Add("Selected Batch and Student's Batch is Different!");
                //    return result;
                //}
                if (student.ProgramId != registrationJson.ProgramId)
                {
                    result.HasError = true;
                    result.Errors.Add("Student is not Admitted in the Selected Program!");
                    return result;
                }

                result.Data = GetRegStudentJson(student); ;

                Debug.WriteLine($"GetStudentById: End:" + DateTime.Now.ToString("T"));
                return result;

            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.Message);
                return result;
            }

            
        }

        private BulkRegStudentJson GetRegStudentJson(User_Student student)
        {
            BulkRegStudentJson studentJson = new BulkRegStudentJson();

            studentJson.AccountId = student.UserId;
            studentJson.StudentId = student.Id;
            studentJson.UserName = student.ClassRollNo;
            studentJson.Name = student.FullName;
            studentJson.ContinuingBatchId = student.ContinuingBatchId;
            studentJson.ClassSection = student.Aca_ClassSection.Name;
            studentJson.IsRegCompleted = false;
            studentJson.IsValidToReg = false;
            return studentJson;
        }


        public HttpListResult<BulkRegStudentJson> GetStudentListByProgramIdAndLevelTermId(
            int programId = 0,
            int levelTermId = 0,
            int batchId = 0,
            int studentLoadOnEnumId=0,
            bool isIncludeInvalidStudent=false,
            int admissionSemesterId=0
        )
        {
            var result=new HttpListResult<BulkRegStudentJson>();
            string error=String.Empty;

            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanAddFromBulkRegistration, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            try
            {
                if (programId<=0)
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid Program!");
                    return result;
                }
                /*if (levelTermId <= 0)
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid Level Term!");
                    return result;
                }*/

                if (studentLoadOnEnumId<=0)
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid Load On!");
                    return result;
                }


                if (studentLoadOnEnumId==(byte)StudentLoadOnEnum.AdmissionSemesterWise && admissionSemesterId<=0)
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid Admission Semester!");
                    return result;
                }

                if (studentLoadOnEnumId == (byte)StudentLoadOnEnum.BatchWise && batchId <= 0)
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid Batch");
                    return result;
                }

                if (studentLoadOnEnumId == (byte)StudentLoadOnEnum.LevelTermWise && levelTermId <= 0)
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid Level Term");
                    return result;
                }

                IQueryable<User_Student> query = DbInstance.User_Student
                    .Where(x => x.ProgramId == programId 
                                && x.IsDeleted == false
                                //&& x.LevelTermId == levelTermId
                                //&& x.EnrollmentStatusEnumId == (byte)User_Student.EnrollmentStatusEnum.Continuing
                    );

                if (studentLoadOnEnumId == (byte)StudentLoadOnEnum.AdmissionSemesterWise)
                {
                    query = query.Where(q => q.User_Account.JoiningSemesterId == admissionSemesterId);
                }

                if (studentLoadOnEnumId == (byte)StudentLoadOnEnum.BatchWise)
                {
                    query = query.Where(q => q.ContinuingBatchId == batchId);
                }

                if (studentLoadOnEnumId == (byte)StudentLoadOnEnum.LevelTermWise)
                {
                    query = query.Where(q => q.LevelTermId == levelTermId);
                }

                if (!isIncludeInvalidStudent)
                {
                    query = query.Where(q => q.EnrollmentStatusEnumId == (byte)User_Student.EnrollmentStatusEnum.Continuing);
                }

                var studentList = query
                    .Include(x => x.User_Account)
                    .Include(x => x.Aca_DeptBatch)
                    .Include(x => x.Aca_ClassSection)
                    .Where(x => x.User_Account.UserTypeEnumId == (byte)User_Account.UserTypeEnum.Student)
                    .OrderBy(x => x.ClassSectionId)
                    .ThenBy(x => x.ClassRollNo)
                    .ToList();

                if (studentList.Count<=0)
                {
                    result.HasError = true;
                    result.Errors.Add("Student not found by selected Program and Level Term.");
                    return result;
                }

                var regStudentJsonList=new List<BulkRegStudentJson>();

                foreach (var student in studentList)
                {
                    regStudentJsonList.Add(GetRegStudentJson(student));
                }

                //Student List Ordering
                //regStudentJsonList = regStudentJsonList
                //    .OrderBy(x => x.UserName)
                //    .ThenBy(x=>x.ClassSection)
                //    .ToList();

                result.Data = regStudentJsonList;

            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message);
            }

            return result;
        }
        

        [HttpPost]
        private HttpResult<BulkRegistrationJson> GetValidateBeforeBulkRegisterStudentList(BulkRegistrationJson registrationJson)
        {   //add/edit class  student list
            string error = string.Empty;
            var result = new HttpResult<BulkRegistrationJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            if (registrationJson == null)
            {
                error = "Selected Student List should not be blank.";
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            var jsons = registrationJson.StudentList;
            //var entities = new List<User_Student>();
            //jsons.Map(entities);
            //if (Facade.AcademicFacade.ClassFacade.SaveClassStudentList(
            //    entities, registrationJson.ClassId,
            //    registrationJson.EnrollTypeEnumId,
            //    registrationJson.RegistrationStatusEnumId,
            //    registrationJson.StatusEnumId, out error)
            //    )
            //{
            //    entities.Map(jsons);
            //    registrationJson.User_StudentJsonList = jsons;
            //    result.Data = registrationJson;
            //}
            //else
            //{
            //    result.HasError = true;
            //    result.Errors.Add(error);
            //}
            return result;
        }
        // DELETE api/<controller>/5



        #region Local method

        public enum BulkRegistrationStatusEnum
        {
            FreshRegistration = 1,
            RetakeRegistration = 2,
            ImprovementRegistration = 3,
            Ignored = 4
        }

        public class BulkRegistrationJson
        {
            public long SemesterId { get; set; }
            public int ProgramId { get; set; }
            public int BatchId { get; set; }
            public int LevelTermId { get; set; }
            public string TmpStudentId { get; set; }
            public List<BulkRegCourseJson> CourseList { get; set; }
            public List<BulkRegStudentJson> StudentList { get; set; }
        }
        public class BulkRegCourseJson
        {
            public string CurriculumCourseId { get; set; }
            public string Name { get; set; }
            public string Code { get; set; }
            public string CourseCategory { get; set; }
            public float Credit { get; set; }
            public int LevelTermId { get; set; }
            public string LevelTermName { get; set; }
            public int? Batch { get; set; }
            public string SyllabusName { get; set; }
            public int TotalSection { get; set; }
            public int TotalStudent { get; set; }
            public string Message { get; set; }
            public int? ClassSectionId { get; set; }

            public List<Aca_ClassSectionJson> SectionList { get; set; }

            public BulkRegCourseJson()
            {
                SectionList = new List<Aca_ClassSectionJson>();
            }

        }

        public class BulkRegStudentJson
        {
            //public int Id { get; set; }
            public int AccountId { get; set; }
            public int StudentId { get; set; }
            public string UserName { get; set; }
            public string Name { get; set; }
            public int ContinuingBatchId { get; set; }
            public string ClassSection { get; set; }
            public bool IsRegCompleted { get; set; }
            public bool IsValidToReg { get; set; }
            public string Message { get; set; }

            public List<CourseValidateMessageJson> CourseMessageList { get; set; }
            public List<string> RetakeAbleCourseIds { get; set; }

            public BulkRegStudentJson()
            {
                CourseMessageList = new List<CourseValidateMessageJson>();
                RetakeAbleCourseIds = new List<string>();
            }

        }

        public class CourseValidateMessageJson
        {
            public string CurriculumCourseId { get; set; }
            public long CourseId { get; set; }
            public string Name { get; set; }
            public bool IsRetakeAble { get; set; }
            public bool IsImprovementAble { get; set; }
            public bool IsSelectedAsRetakeAble { get; set; }
            public bool IsSelectedAsImprovementAble { get; set; }
            public bool IsIgnored { get; set; }
            public byte RegistrationStatusEnumId { get; set; }

            public string RegistrationStatus
            {
                get
                {
                    return ((BulkRegistrationStatusEnum) RegistrationStatusEnumId).ToString().AddSpacesToSentence();
                }
            }

            public string Message { get; set; }

        }
        #endregion

    }
}






//var jsons = new List<Aca_ClassJson>();
//classList.Map(jsons);
//                    //setting total enrolled student
//                    foreach (var classObj in jsons)
//                    {
//                        classObj.TotalStudent =
//                        DbInstance.Aca_ClassTakenByStudent.Count(x => x.ClassId.ToString() == classObj.Id
//                        // && x.EnrollTypeEnumId == (byte)Aca_Exam.ExamTypeEnum.MidTerm todo need to re organize this problem
//                        && x.RegistrationStatusEnumId != (byte)Aca_ClassTakenByStudent.RegistrationStatusEnum.Cancelled
//                        && x.RegistrationStatusEnumId != (byte)Aca_ClassTakenByStudent.RegistrationStatusEnum.Drop
//                        );
//                    }
//                    var bulkRegJson = BulkStudentRegistrationJson.GetNew();
//bulkRegJson.ClassListJson = jsons;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
//using EMS.Web.Jsons.Mapper;
using EMS.Web.Jsons.Models;

namespace EMS.Web.UI.Areas.Academic.Controllers.WebApi
{
    public class ClassApiController : EmployeeBaseApiController
    {
        #region ClassHistoryJson

        public class ClassHistoryJson
        {
            public string CreateByName { get; set; }
            public DateTime CreateAt { get; set; }
            public string LastChangedByName { get; set; }
            public DateTime LastChangedAt { get; set; }
            public string GeneralHistory { get; set; }
            public string LockUnlockHistory { get; set; }
        }

        #endregion
        public HttpListResult<Aca_ClassJson> GetPagedClass(string textkey, int? pageSize, int? pageNo
           , Int64 programId = 0
            , Int64 departmentId = 0
           , Int64 curriculumCourseId = 0
           , Int64 semesterId = 0
           , Int32 studentBatchId = 0
           , Int32 studyLevelTermId = 0
           , Int64 campusId = 0
           , bool isOfferedByThisDept = true
           ,bool isShowTrashedClass=false
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Aca_ClassJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.OfferedClass.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.OfferedClass.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.OfferedClass.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_ClassDataService classDataService = new Aca_ClassDataService(DbInstance, HttpUtil.Profile))
                {
                    IQueryable<Aca_Class> query = (from q in DbInstance.Aca_Class
                        select q)
                        .Include(x => x.Aca_CurriculumCourse)
                        //.Include(x => x.Aca_CurriculumCourse.Aca_Curriculum.Aca_Program.HR_Department)
                        //.Include(x => x.Aca_CurriculumCourse.HR_Department)
                        .Include(x => x.Aca_Semester)
                        .Include(x => x.Aca_StudyLevelTerm)
                        .Include(x => x.Aca_ClassSection)
                        .Include(x => x.Aca_ClassRoutine)
                        .Include("Aca_ClassRoutine.General_Room")
                        .Include("Aca_ClassRoutine.General_Room.General_Floor")
                        .Include("Aca_ClassRoutine.General_Room.General_Building")
                        .Include("Aca_ClassRoutine.General_Room.General_Building.General_Campus")
                        .Where(x=>x.IsDeleted == isShowTrashedClass)
                        .OrderBy(x => x.Aca_CurriculumCourse.Aca_Curriculum.CurriculumNo)
                        .ThenBy(x => x.Aca_CurriculumCourse.Aca_Curriculum.Aca_Program.ProgramTypeEnumId)
                        //.ThenByDescending(x => x.Aca_CurriculumCourse.Aca_Curriculum.Aca_Program.Id)
                        //.ThenByDescending(x => x.Aca_CurriculumCourse.CurriculumId)
                        .ThenByDescending(x => x.SemesterId)
                        .ThenBy(x => x.StudyLevelTermId)
                        //.ThenBy(x => x.Aca_CurriculumCourse.OfferedByDepartmentId)
                        .ThenBy(x => x.Code)
                        .ThenBy(x => x.ClassSectionId);
                        //.ThenBy(x => x.ClassNo);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Code.ToLower().Contains(textkey.ToLower()));
                    }
                    if (programId > 0)
                    {
                        query = from q in query
                                where q.Aca_CurriculumCourse.Aca_Curriculum.ProgramId == programId

                                select q;
                    }

                    if (studentBatchId > -1)
                    {
                        if (studentBatchId == 0)
                        {
                            query = query.Where(q => q.StudentBatchId == null);
                        }
                        else
                        {
                            query = query.Where(q => q.StudentBatchId == studentBatchId);
                        }
                    }
                    //if (departmentId > 0)
                    //{
                    //    if (isOfferedByThisDept)
                    //    {
                    //        query = from q in query
                    //            where q.DepartmentId == departmentId
                    //            select q;
                    //    }
                    //    else
                    //    {
                    //        query = from q in query
                    //                where q.DepartmentId != departmentId
                    //                && q.Aca_CurriculumCourse.OfferedByDepartmentId == departmentId
                    //                select q;
                    //    }
                    //}
                    //if (curriculumCourseId > 0)
                    //{
                    //    query = query.Where(q => q.CurriculumCourseId == curriculumCourseId);
                    //}
                    if (semesterId > 0)
                    {
                        query = query.Where(q => q.SemesterId == semesterId);
                    }
             
                    if (studyLevelTermId > 0)
                    {
                        query = query.Where(q => q.StudyLevelTermId == studyLevelTermId);
                    }
                    //if (campusId > 0)
                    //{
                    //    query = query.Where(q => q.CampusId == campusId);
                    //}

                    var entities = classDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Aca_ClassJson>();
                    entities.Map(jsons);
                    //setting total enrolled student
                    foreach (var classObj in jsons)
                    {
                        classObj.TotalStudent =
                        DbInstance.Aca_ClassTakenByStudent.Count(x => x.ClassId.ToString() == classObj.Id 
                        && x.EnrollTypeEnumId == (byte)Aca_Exam.ExamTypeEnum.FinalTerm
                        && x.StatusEnumId == (byte)Aca_ClassTakenByStudent.StatusEnum.Valid
                        && (
                            x.RegistrationStatusEnumId != (byte)Aca_ClassTakenByStudent.RegistrationStatusEnum.Drop &&
                            x.RegistrationStatusEnumId != (byte)Aca_ClassTakenByStudent.RegistrationStatusEnum.Cancelled
                            ));

                        var falutyList = DbInstance.Aca_ClassTakenByEmployee
                        .Include(x=>x.User_Employee)
                        .Include(x=>x.User_Employee.User_Account)
                        .Where(x => x.ClassId.ToString() == classObj.Id 
                        && x.RoleEnumId == (byte)Aca_ClassTakenByEmployee.RoleEnum.Faculty
                        && !x.IsDeleted
                        )
                        .ToList();
                        classObj.TotalFaculty = falutyList.Count;
                        classObj.Aca_ClassTakenByEmployeeJsonList = new List<Aca_ClassTakenByEmployeeJson>();
                        falutyList.Map(classObj.Aca_ClassTakenByEmployeeJsonList);
                    }

                    result.Data = jsons;
                    result.Count = count;
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        
        //TODO GetPagedOthersDeptClass should be delete
        public HttpListResult<Aca_ClassJson> GetPagedOthersDeptClass(string textkey, int? pageSize, int? pageNo
           , Int64 programId = 0
           , Int64 departmentId = 0
           , Int64 curriculumCourseId = 0
           , Int64 semesterId = 0
         
           , Int32 studyLevelTermId = 0
           , Int64 campusId = 0
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Aca_ClassJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.OfferedClass.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.OfferedClass.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.OfferedClass.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_ClassDataService classDataService = new Aca_ClassDataService(DbInstance, HttpUtil.Profile))
                {
                    IQueryable<Aca_Class> query = (from q in DbInstance.Aca_Class
                                                   select q)
                                                .Include(x => x.Aca_CurriculumCourse)
                                                //.Include(x => x.Aca_CurriculumCourse.Aca_Curriculum.HR_Department)
                                                .Include(x => x.Aca_Semester)
                                                .Include(x => x.Aca_StudyLevelTerm)
                                                .Include(x => x.Aca_ClassSection)

                                                .OrderBy(x => x.Aca_CurriculumCourse.Aca_Curriculum.CurriculumNo)
                                                .ThenBy(x => x.Aca_CurriculumCourse.Aca_Curriculum.Aca_Program.ProgramTypeEnumId)
                                                .ThenByDescending(x => x.Aca_CurriculumCourse.Aca_Curriculum.Aca_Program.Id)
                                                .ThenByDescending(x => x.Aca_CurriculumCourse.CurriculumId)
                                                .ThenByDescending(x => x.SemesterId)
                                                .ThenBy(x => x.StudyLevelTermId)
                                                .ThenBy(x => x.Aca_CurriculumCourse.Code)
                                                //.ThenBy(x => x.Aca_CurriculumCourse.OfferedByDepartmentId)
                                                .ThenBy(x => x.ClassNo);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Code.ToLower().Contains(textkey.ToLower()));
                    }
                    if (programId > 0)
                    {
                        query = from q in query
                                where q.Aca_CurriculumCourse.Aca_Curriculum.ProgramId == programId
                             
                                select q;
                    }
                    //if (departmentId > 0)
                    //{
                    //    query = from q in query
                    //            where q.Aca_CurriculumCourse.Aca_Curriculum.DepartmentId != departmentId
                    //            && q.Aca_CurriculumCourse.OfferedByDepartmentId == departmentId
                    //            select q;
                    //}
                    if (curriculumCourseId > 0)
                    {
                        query = query.Where(q => q.CurriculumCourseId == curriculumCourseId);
                    }
                    if (semesterId > 0)
                    {
                        query = query.Where(q => q.SemesterId == semesterId);
                    }
             
                    if (studyLevelTermId > 0)
                    {
                        query = query.Where(q => q.StudyLevelTermId == studyLevelTermId);
                    }
                    if (campusId > 0)
                    {
                        query = query.Where(q => q.CampusId == campusId);
                    }

                    var entities = classDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Aca_ClassJson>();
                    entities.Map(jsons);
                    //setting total enrolled student
                    foreach (var classObj in jsons)
                    {
                        classObj.TotalStudent =
                        DbInstance.Aca_ClassTakenByStudent.Count(x => x.ClassId.ToString() == classObj.Id
                        && x.EnrollTypeEnumId == (byte)Aca_Exam.ExamTypeEnum.FinalTerm
                        && x.StatusEnumId == (byte)Aca_ClassTakenByStudent.StatusEnum.Valid
                        && x.RegistrationStatusEnumId == (byte)Aca_ClassTakenByStudent.RegistrationStatusEnum.FreshRegistration);

                        var falutyList = DbInstance.Aca_ClassTakenByEmployee
                        .Include(x => x.User_Employee)
                        .Include(x => x.User_Employee.User_Account)
                        .Where(x => x.ClassId.ToString() == classObj.Id
                        && x.RoleEnumId == (byte)Aca_ClassTakenByEmployee.RoleEnum.Faculty)
                        .ToList();
                        classObj.TotalFaculty = falutyList.Count;
                        classObj.Aca_ClassTakenByEmployeeJsonList = new List<Aca_ClassTakenByEmployeeJson>();
                        falutyList.Map(classObj.Aca_ClassTakenByEmployeeJsonList);
                    }

                    result.Data = jsons;
                    result.Count = count;
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }

        //used in ClassAddEdit
        public HttpListResult<Aca_ClassJson> GetClassList(Int64 departmentId = 0, Int64 SemesterId = 0
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Aca_ClassJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.OfferedClass.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.OfferedClass.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.OfferedClass.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_ClassDataService classDataService = new Aca_ClassDataService(DbInstance, HttpUtil.Profile))
                {
                    IQueryable<Aca_Class> query = (from q in DbInstance.Aca_Class
                                                   select q)
                                                .Include(x => x.Aca_CurriculumCourse)
                                                //.Include(x => x.Aca_CurriculumCourse.Aca_Curriculum.Aca_Program.HR_Department)
                                                .Include(x => x.Aca_Semester)
                                                .Include(x => x.Aca_StudyLevelTerm)
                                                .OrderBy(x => x.Aca_CurriculumCourse.Aca_Curriculum.CurriculumNo)
                                                .ThenBy(x => x.Aca_CurriculumCourse.Aca_Curriculum.Aca_Program.ProgramTypeEnumId)
                                                .ThenByDescending(x => x.Aca_CurriculumCourse.Aca_Curriculum.Aca_Program.Id)
                                                .ThenByDescending(x => x.Aca_CurriculumCourse.CurriculumId)
                                                .ThenByDescending(x => x.SemesterId)
                                                .ThenBy(x => x.StudyLevelTermId)
                                                .ThenBy(x => x.Aca_CurriculumCourse.Code)
                                                .ThenBy(x => x.ClassNo);
                   
                    if (SemesterId > 0)
                    {
                        query = query.Where(q => q.SemesterId == SemesterId);
                    }

                    var entities = classDataService.GetListByQuery(query, out error);
                    var jsons = new List<Aca_ClassJson>();
                    entities.Map(jsons);

                    result.Data = jsons;
                    result.Count = count;
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        // GET api/<controller>
        public HttpListResult<Aca_ClassJson> GetClassList(string name, long semesterId, long studyLevelTermId, long departmentId, int? pageSize, int? pageNo)
        {   //canview/add/edit
            int count = 0;
            var result = new HttpListResult<Aca_ClassJson>();
            string error = string.Empty;
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.OfferedClass.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.OfferedClass.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.OfferedClass.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            var jsons = new List<Aca_ClassJson>();
            var entities = Facade.AcademicFacade.ClassFacade.GetClassList(name, semesterId, studyLevelTermId, departmentId, pageSize, pageNo, ref count);
            entities.Map(jsons);
            result.Data = jsons;
            result.Count = count;
            return result;
        }

        public HttpListResult<Aca_ClassJson> GetClassListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_ClassJson>();

            try
            {
                //Aca_ClassDataService classDataService =
                //    new Aca_ClassDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List

                result.DataExtra.StatusEnumList = EnumUtil.GetEnumList(typeof(Aca_Class.StatusEnum));
                //result.DataExtra.SemesterDurationEnumList = Aca_Semester.SemesterDurationEnumDropDownList;
                //DropDown Option Tables

                result.DataExtra.ProgramList = DbInstance.Aca_Program.AsEnumerable()
                               .OrderBy(x => x.ShortName)
                               .Where(x => !x.IsDeleted)
                               .Select(t => new
                               { Id = t.Id, Name = t.Name, ShortName = t.ShortName, ShortTitle = t.ShortTitle})
                               .ToList();

                result.DataExtra.SemesterList = Aca_Semester.GetDropdownList(DbInstance);

                result.DataExtra.StudyLevelTermList = DbInstance.Aca_StudyLevelTerm.OrderBy(x => x.Id).AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.Name, SemesterDurationEnumId = t.SemesterDurationEnumId }).ToList();

                result.DataExtra.DeptBatchList = DbInstance.Aca_DeptBatch.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.Name }).ToList();

                result.DataExtra.AllDepartmentList = DbInstance.HR_Department.AsEnumerable()
                    .OrderBy(x => x.Name)
                    .Select(t => new
                    {
                        Id = t.Id,
                        Name = t.Name,// + " (" + t.ShortName + ")",
                        Type = t.Type.ToString().AddSpacesToSentence()
                    });

                result.DataExtra.ClassEmployeesJson = new ClassEmployeesJson();

                // result.DataExtra.SemesterLevelTermList = DbInstance.Aca_SemesterLevelTerm
                //.OrderByDescending(x => x.Aca_Semester.Year)
                //.ThenBy(x => x.Aca_StudyLevelTerm.Aca_StudyLevel.Name)
                //.ThenBy(x => x.Aca_StudyLevelTerm.Aca_StudyTerm.Name)
                //.AsEnumerable().Select(t => new
                //{ Id = t.Id.ToString(), Name = t.Name })
                //.ToList();

                //result.DataExtra.CampusList = DbInstance.General_Campus.AsEnumerable().Select(t => new
                //{ Id = t.Id, Name = t.ShortName }).ToList();
                //result.DataExtra.DepartmentList = DbInstance.HR_Department.AsEnumerable()
                //    .OrderBy(x => x.ShortName)
                //    .Where(x => x.TypeEnumId == 2)
                //    .Select(t => new
                //    { Id = t.Id, Name = t.Name, ShortName = t.ShortName })
                //    .ToList();



            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }

        #region Class Student Manage Area

       

        //Get Class Taken Student
        public HttpListResult<Aca_ClassTakenByStudentJson> GetClassStudentList(long classId)
        {   //canview/add/edit class
            int count = 0;
            var result = new HttpListResult<Aca_ClassTakenByStudentJson>();
            string error = string.Empty;
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            var jsons = new List<Aca_ClassTakenByStudentJson>();
            var entities = Facade.AcademicFacade.ClassFacade.GetStudentListByClassId(classId);
            entities.Map(jsons);
            result.Data = jsons;
            result.Count = count;
            return result;
        }
        public HttpListResult<Aca_ClassWaiverStudentJson> GetClassWaiverStudentList(long classId)
        {   //canview/add/edit class
            int count = 0;
            var result = new HttpListResult<Aca_ClassWaiverStudentJson>();
            string error = string.Empty;
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            var jsons = new List<Aca_ClassWaiverStudentJson>();
            var entities = Facade.AcademicFacade.ClassFacade.GetWaiverStudentListByClassId(classId);
            entities.Map(jsons);
            result.Data = jsons;
            result.Count = count;
            return result;
        }
        /// <summary>
        /// called from search student modal 
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <param name="deptId"></param>
        /// <param name="studyLevelTermId"></param>
        /// <param name="classId"></param>
        /// <param name="selectTypeId"></param>
        /// <param name="searchClassId"></param>
        /// <param name="enrollTypeEnumId"></param>
        /// <param name="searchClassRoll"></param>
        /// <param name="campusId"></param>
        /// <returns></returns>
        public HttpListResult<User_StudentJson> GetStudentList(
            int pageSize
            ,int pageNo
            , long deptId
            , int studyLevelTermId
            , long classId
            , int selectTypeId
            , long searchClassId
            , byte enrollTypeEnumId
            , int searchClassRoll
            , int campusId=0
            )
        {   //Can Add/edit student 
            int count = 0;
            var result = new HttpListResult<User_StudentJson>();
            string error = string.Empty;
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            var jsons = new List<User_StudentJson>();
            List<User_Student> entities = null;
            Aca_StudyLevelTerm studyLevelTerm = null;
            campusId = campusId <= 0 ? 1 : campusId; //making
            if (selectTypeId == 0)
            {
                if (studyLevelTermId >= 0)
                {
                    studyLevelTerm = DbInstance.Aca_StudyLevelTerm.SingleOrDefault(x => x.Id == studyLevelTermId);
                }
                var levelId = 0;
                var termId = 0;
                if (selectTypeId == 0 && studyLevelTerm != null)
                {
                    levelId = studyLevelTerm.LevelId;
                    termId = studyLevelTerm.TermId;
                }
                entities = Facade.AcademicFacade.ClassFacade.GetStudentNotGraduatedPagedList(deptId, levelId, termId,campusId,pageSize,pageNo,ref count);
            }
            else if (selectTypeId == 2)
            {
                int classRoll = -1;
                if (searchClassRoll!=null)
                {
                    classRoll = (int) searchClassRoll;
                }
                entities = Facade.AcademicFacade.ClassFacade.GetStudentListByClassRoll(classRoll);
            }
            else
            {
                entities = Facade.AcademicFacade.ClassFacade.GetStudentListByClassIdByEnrollTypeId(searchClassId, enrollTypeEnumId);
            }

            var enrolledStdList = Facade.AcademicFacade.ClassFacade.GetStudentListByClassIdByEnrollTypeId(classId, enrollTypeEnumId);

            entities.Map(jsons);

            foreach (var std in jsons)
            {
                std.IsAlreadyAdded = enrolledStdList.Any(x => x.Id == std.Id);
            }

            result.Data = jsons;
            result.Count = count;
            return result;
        }
        /// <summary>
        /// called from->add waiver student list
        /// </summary>
        /// <param name="deptId"></param>
        /// <param name="studyLevelTermId"></param>
        /// <param name="classId"></param>
        /// <returns></returns>
        public HttpListResult<User_StudentJson> GetStudentList(
             int pageSize
            , int pageNo
            , long deptId
            , int studyLevelTermId
            , long classId
            , int campusId = 0
            )
        {   //Can Add/edit student 
            int count = 0;
            var result = new HttpListResult<User_StudentJson>();
            string error = string.Empty;
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            var jsons = new List<User_StudentJson>();
            var studyLevelTerm = DbInstance.Aca_StudyLevelTerm.SingleOrDefault(x => x.Id == studyLevelTermId);
            var levelId = 0;
            var termId = 0;
            if (studyLevelTerm != null)
            {
                levelId = studyLevelTerm.LevelId;
                termId = studyLevelTerm.TermId;
            }
            campusId = campusId <= 0 ? 1 : campusId; //making
            var entities = Facade.AcademicFacade.ClassFacade.GetStudentNotGraduatedPagedList(deptId, levelId, termId, campusId, pageSize, pageNo, ref count);


            var enrolledStdList = Facade.AcademicFacade.ClassFacade.GetStudentWaiverListByClassId(classId);

            entities.Map(jsons);

            foreach (var std in jsons)
            {
                std.IsAlreadyAdded = enrolledStdList.Any(x => x.Id == std.Id);
            }

            result.Data = jsons;
            result.Count = count;
            return result;
        }
        //Class Student
        [HttpPost]
        public HttpResult<ClassStudentsJson> SaveClassStudentListTmpOff(ClassStudentsJson classStudentsJson)  //to take rolltype
        {   //add/edit class  student list
            string error = string.Empty;
            var result = new HttpResult<ClassStudentsJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            if (classStudentsJson == null)
            {
                error = "Selected Student List should not be blank.";
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            var jsons = classStudentsJson.User_StudentJsonList;
            var entities = new List<User_Student>();
            jsons.Map(entities);
            if (Facade.AcademicFacade.ClassFacade.SaveClassStudentList(
                entities, classStudentsJson.ClassId,
                classStudentsJson.EnrollTypeEnumId,
                classStudentsJson.RegistrationStatusEnumId,
                classStudentsJson.StatusEnumId, out error)
                )
            {
                entities.Map(jsons);
                classStudentsJson.User_StudentJsonList = jsons;
                result.Data = classStudentsJson;
            }
            else
            {
                result.HasError = true;
                result.Errors.Add(error);
            }
            return result;
        }
        [HttpPost]
        public HttpResult<ClassStudentsJson> SaveClassWaiverStudentListTmpOff(ClassStudentsJson classStudentsJson)
        {   //add/edit class  student list
            string error = string.Empty;
            var result = new HttpResult<ClassStudentsJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            if (classStudentsJson == null)
            {
                error = "Selected Student List should not be blank.";
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            var jsons = classStudentsJson.User_StudentJsonList;
            var entities = new List<User_Student>();
            jsons.Map(entities);
            if (Facade.AcademicFacade.ClassFacade.SaveClassWaiverStudentList(entities, classStudentsJson.ClassId, out error))
            {
                entities.Map(jsons);
                classStudentsJson.User_StudentJsonList = jsons;
                result.Data = classStudentsJson;
            }
            else
            {
                result.HasError = true;
                result.Errors.Add(error);
            }
            return result;
        }
        [HttpPost]
        public HttpListResult<Aca_ClassWaiverStudentJson> SaveBulkClassWaiverStudentListTmpOff(List<Aca_ClassWaiverStudentJson> jsonList)
        {   //add/edit class  student list
            string error = string.Empty;
            var result = new HttpListResult<Aca_ClassWaiverStudentJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            if (jsonList == null)
            {
                error = "Waiver Student List should not be blank.";
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            var entities = new List<Aca_ClassWaiverStudent>();
            jsonList.Map(entities);
            if (!Facade.AcademicFacade.ClassFacade.SaveClassWaiverStudentList(entities, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
            }
            return result;
        }
        /// <summary>
        /// Todo rename and pending to implement bulk save
        /// </summary>
        /// <param name="jsonList"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpListResult<Aca_ClassTakenByStudentJson> SaveClassTakenByStudentListTmpOff(IList<Aca_ClassTakenByStudentJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_ClassTakenByStudentJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanEdit, out error)
            && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanAdd, out error)
            )
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_ClassTakenByStudentDataService classTakenByStudentDataService = new Aca_ClassTakenByStudentDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<Aca_ClassTakenByStudent>();
                    var dbAttachedListEntity = new List<Aca_ClassTakenByStudent>();
                    jsonList.Map(entityListReceived);

                    foreach (var entity in entityListReceived)
                    {
                        if (!classTakenByStudentDataService.Save(entity, out error))
                        {
                            result.HasError = true;
                            result.Errors.Add(error);
                        }
                    }
                    entityListReceived.Map(jsonList);
                    result.Data = jsonList;
                    //if (classTakenByStudentDataService.Save(entity, ref dbAttachedListEntity, out error))
                    //{
                    //    dbAttachedListEntity.Map(jsonList);
                    //    result.Data = jsonList;//return object
                    //}
                    //else
                    //{
                    //    result.HasError = true;
                    //    result.Errors.Add(error);
                    //}
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }

            return result;
        }
        // DELETE api/<controller>/5
        [HttpPost]
        public HttpResult<Aca_ClassTakenByStudentJson> DeleteClassStudent(Aca_ClassTakenByStudentJson json)
        {  //can delete class  student
            string error = string.Empty;
            var result = new HttpResult<Aca_ClassTakenByStudentJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanPermanentDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            var entity = new Aca_ClassTakenByStudent();
            json.Map(entity);
            if (Facade.AcademicFacade.ClassFacade.PermanentDeleteStudentFromClass(entity, out error))
            {
                result.HasError = false;
            }
            else
            {
                result.HasError = true;
                result.Errors.Add(error);
            }
            return result;
        }

        [HttpPost]
        public HttpListResult<Aca_ClassTakenByStudentJson> DeleteClassAllStudent(List<Aca_ClassTakenByStudentJson> jsons)
        {  //can delete class  student
            string error = string.Empty;
            var result = new HttpListResult<Aca_ClassTakenByStudentJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanPermanentDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            var entities = new List<Aca_ClassTakenByStudent>();
            jsons.Map(entities);
            if (Facade.AcademicFacade.ClassFacade.DeleteClassAllStudent(entities, out error))
            {
                result.HasError = false;
            }
            else
            {
                result.HasError = true;
                result.Errors.Add(error);
            }
            return result;
        }
        [HttpPost]
        public HttpResult<Aca_ClassWaiverStudentJson> DeleteClassWaiverStudentTmpOff(Aca_ClassWaiverStudentJson json)
        {  //can delete class  student
            string error = string.Empty;
            var result = new HttpResult<Aca_ClassWaiverStudentJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanPermanentDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            var entity = new Aca_ClassWaiverStudent();
            json.Map(entity);
            if (Facade.AcademicFacade.ClassFacade.DeleteClassWaiverStudent(entity, out error))
            {
                result.HasError = false;
            }
            else
            {
                result.HasError = true;
                result.Errors.Add(error);
            }
            return result;
        }
        #endregion

        #region Class Teacher Manage Area

      
        //Class Teacher  
        //todo need to take rolltype
        public HttpListResult<Aca_ClassTakenByEmployeeJson> GetClassTeacherList(long classId,bool isShowTrashedFaculty)
        {   //canview/add/edit class
            int count = 0;
            var result = new HttpListResult<Aca_ClassTakenByEmployeeJson>();
            string error = string.Empty;
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.TeacherEnrollment.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.TeacherEnrollment.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.TeacherEnrollment.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            var jsons = new List<Aca_ClassTakenByEmployeeJson>();
            var entities = Facade.AcademicFacade.ClassFacade.GetTeacherListByClassId(classId, isShowTrashedFaculty);
            entities.Map(jsons);
            result.Data = jsons;
            result.Count = count;
            return result;
        }
        public HttpListResult<User_EmployeeJson> GetTeacherList(long deptId, long classId, byte roleEnumId,int campusId)// to take rolltype
        {   //Can add/edit teacher 
            int count = 0;
            var result = new HttpListResult<User_EmployeeJson>();
            string error = string.Empty;
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.TeacherEnrollment.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.TeacherEnrollment.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.TeacherEnrollment.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            var jsons = new List<User_EmployeeJson>();
            var entities = Facade.AcademicFacade.ClassFacade.GetTeacherList(deptId, campusId);

            var enrolledEmpList = Facade.AcademicFacade.ClassFacade.GetTeacherListByClassIdByRoleEnumId(classId, roleEnumId);

            entities.Map(jsons);

            foreach (var emp in jsons)
            {
                emp.IsAlreadyAdded = enrolledEmpList.Any(x => x.Id == emp.Id);
            }

            result.Data = jsons;
            result.Count = count;
            return result;
        }
        //on add Class Teacher below called
        public HttpResult<ClassEmployeesJson> SaveClassTeacherList(ClassEmployeesJson classEmployeesJson)
        {   //add/edit class teacher list
            string error = string.Empty;
            var result = new HttpResult<ClassEmployeesJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.TeacherEnrollment.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.TeacherEnrollment.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            if (classEmployeesJson == null)
            {
                error = "Selected Employee List should not be blank.";
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            var jsons = classEmployeesJson.User_EmployeeJsonList;
            var entities = new List<User_Employee>();
            jsons.Map(entities);
            if (Facade.AcademicFacade.ClassFacade.SaveClassTeacherList(
                entities
                , classEmployeesJson.ClassId
                , classEmployeesJson.RoleEnumId
                , classEmployeesJson.SectionEnumId
                , classEmployeesJson.StatusEnumId
                , out error))
            {
                entities.Map(jsons);
                classEmployeesJson.User_EmployeeJsonList = jsons;
                result.Data = classEmployeesJson;
            }
            else
            {
                result.HasError = true;
                result.Errors.Add(error);
            }
            return result;
        }
        /// <summary>
        /// Todo rename and pending to implement bulk save
        /// </summary>
        /// <param name="jsonList"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpListResult<Aca_ClassTakenByEmployeeJson> SaveClassTakenByEmployeeList(IList<Aca_ClassTakenByEmployeeJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_ClassTakenByEmployeeJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.TeacherEnrollment.CanEdit, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.TeacherEnrollment.CanAdd, out error)
            )
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_ClassTakenByEmployeeDataService classTakenByEmployeeDataService = new Aca_ClassTakenByEmployeeDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<Aca_ClassTakenByEmployee>();
                    var dbAttachedListEntity = new List<Aca_ClassTakenByEmployee>();
                    jsonList.Map(entityListReceived);

                    foreach (var entity in entityListReceived)
                    {
                        if (!classTakenByEmployeeDataService.Save(entity, out error))
                        {
                            result.HasError = true;
                            result.Errors.Add(error);
                        }
                    }
                    entityListReceived.Map(jsonList);
                    result.Data = jsonList;
                    //if (classTakenByEmployeeDataService.Save(entity, ref dbAttachedListEntity, out error))
                    //{
                    //    dbAttachedListEntity.Map(jsonList);
                    //    result.Data = jsonList;//return object
                    //}
                    //else
                    //{
                    //    result.HasError = true;
                    //    result.Errors.Add(error);
                    //}
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }

            return result;
        }

        [HttpPost]
        public HttpResult<Aca_ClassTakenByEmployeeJson> DeleteClassTeacher(Aca_ClassTakenByEmployeeJson json)
        {   //can delete class  student
            string error = string.Empty;
            var result = new HttpResult<Aca_ClassTakenByEmployeeJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.TeacherEnrollment.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            var entity = new Aca_ClassTakenByEmployee();
            json.Map(entity);
            if (Facade.AcademicFacade.ClassFacade.DeleteClassTeacher(entity, out error))
            {
                result.HasError = false;
            }
            else
            {
                result.HasError = true;
                result.Errors.Add(error);
            }
            return result;
        }

        [HttpGet]
        public HttpResult<Aca_ClassTakenByEmployeeJson> TrashOrUnTrashClassTeacherById(long classTakenByEmployeeId,bool isTrash)
        {   //can delete class  student
            string error = string.Empty;
            var result = new HttpResult<Aca_ClassTakenByEmployeeJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.TeacherEnrollment.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            var classTakenByEmployee =
                DbInstance.Aca_ClassTakenByEmployee.FirstOrDefault(x => x.Id == classTakenByEmployeeId);

            var deleteMsg = isTrash ? "Deleted" : "Un-Deleted";

            if (classTakenByEmployee == null)
            {
                result.HasError = true;
                result.Errors.Add($"Faculty to be {deleteMsg} Cannot be found.");
                return result;
            }
            var faculty = DbInstance.User_Employee
                .Include(x => x.User_Account)
                .Where(x => x.Id == classTakenByEmployee.EmployeeId)
                .Select(x => new
                {
                    Name = x.User_Account.FullName,
                    UserName = x.User_Account.UserName
                }).FirstOrDefault();
            classTakenByEmployee.IsDeleted = isTrash;
            classTakenByEmployee.StatusEnumId= isTrash ? (byte)Aca_ClassTakenByEmployee.StatusEnum.Inactive : (byte)Aca_ClassTakenByEmployee.StatusEnum.Active;

            classTakenByEmployee.History += $"{DateTime.Now} {faculty?.Name}[{faculty?.UserName}] was {deleteMsg}, by {HttpUtil.Profile.Name}[{HttpUtil.Profile.UserName}], IP:{HttpUtil.GetUserIp()}<br>";

            DbInstance.SaveChanges();
            return result;
        }

        #endregion


        // POST api/<controller>
        [HttpPost]
        public HttpResult<Aca_ClassJson> SaveClass(Aca_ClassJson json)
        {  //add/edit class
            string error = string.Empty;
            var result = new HttpResult<Aca_ClassJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.OfferedClass.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.OfferedClass.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            var entity = new Aca_Class();
            json.Map(entity);

            if (Facade.AcademicFacade.ClassFacade.SaveClass(entity, out error))
            {
                entity.Map(json);
                result.Data = json;
            }
            else
            {
                result.HasError = true;
                result.Errors.Add(error);
            }
            return result;
        }
        public HttpResult<Aca_ClassJson> GetDeleteClassById(long id)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_ClassJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.OfferedClass.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (var scope = DbInstance.Database.BeginTransaction())
                {
                 

                    var canDelete = true;
                    if (DbInstance.Aca_ClassTakenByStudent.Any(x => x.ClassId == id))
                    {
                        canDelete = false;
                        error += "Some Students Registered in This Class, Delete those Student's Registration. ";
                    }
                   


                    if (!canDelete)
                    {
                        result.HasError = true;
                        result.Errors.Add(error);
                        return result;
                    }

                    Aca_Class classDelete = DbInstance.Aca_Class
                      //.Include("Aca_ClassTakenByStudent")
                      .Include("Aca_ClassTakenByEmployee")
                      //.Include("Aca_ResultClass")
                      //.Include("Aca_ResultClassSetting")
                     
                      //.Include("Aca_ResultComponent")
                      .Include("Aca_ResultComponentSetting")
                      //.Include("Aca_ResultComponentBreakdown")
                      .Include("Aca_ResultComponentBreakdownSetting")
                       .Include(x=>x.Aca_ClassAttendance)
                       .Include(x=>x.Aca_ClassAttendanceSetting)
                       .Include(x => x.Aca_ClassAttendanceSmsLog)
                       .Include(x => x.Aca_ClassNotice)
                       .Include(x => x.Aca_ClassMaterialFileMap)

                      .Include(x => x.Aca_ClassRoutine)
                      .SingleOrDefault(x => x.Id == id);

                    //remove class material files
                    if (classDelete != null)
                    {

                       
                        DbInstance.Aca_ClassAttendance.RemoveRange(classDelete.Aca_ClassAttendance);

                        DbInstance.Aca_ClassTakenByEmployee.RemoveRange(classDelete.Aca_ClassTakenByEmployee);
                       

                        DbInstance.Aca_ClassRoutine.RemoveRange(classDelete.Aca_ClassRoutine);

                        DbInstance.Aca_ClassAttendanceSmsLog.RemoveRange(classDelete.Aca_ClassAttendanceSmsLog);

                        DbInstance.Aca_ClassAttendanceSetting.RemoveRange(classDelete.Aca_ClassAttendanceSetting);

                        DbInstance.Aca_ClassNotice.RemoveRange(classDelete.Aca_ClassNotice);

                        DbInstance.Aca_ClassMaterialFileMap.RemoveRange(classDelete.Aca_ClassMaterialFileMap);

                        DbInstance.Aca_Class.Remove(classDelete);

                        DbInstance.SaveChanges();
                        scope.Commit();
                    }

                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.ToString());
                //result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }

        public HttpResult<Aca_ClassJson> GetTrashUnTrashClassById(long id = 0, bool isPutInTrash = false)
        {
            string error = String.Empty;
            var result = new HttpResult<Aca_ClassJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.OfferedClass.CanTrashUnTrash, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
                try
                {

                    if (id <= 0)
                    {
                        result.HasError = true;
                        result.Errors.Add("Invalid Class Id!");
                        return result;
                    }
                    var acaClass = DbInstance.Aca_Class.FirstOrDefault(x => x.Id == id);

                    if (acaClass.IsNull())
                    {
                        result.HasError = true;
                        result.Errors.Add("Invalid class, please refresh and try again!");
                        return result;
                    }

                    if (isPutInTrash)
                    {

                        if (acaClass.IsDeleted)
                        {
                            result.HasError = true;
                            result.Errors.Add("This Class Already Trash.");
                            return result;
                        }

                        acaClass.IsDeleted = true;
                        acaClass.History += $"Trash by {HttpUtil.Profile.Name}({HttpUtil.Profile.UserName}) at {DateTime.Now.ToString("dd-MM-yyyy h:mm:ss")}. <br/>";

                    }
                    else
                    {
                        if (!acaClass.IsDeleted)
                        {
                            result.HasError = true;
                            result.Errors.Add("This Class Already UnTrash.");
                            return result;
                        }

                        acaClass.IsDeleted = false;
                        acaClass.History += $"UnTrash by {HttpUtil.Profile.Name}({HttpUtil.Profile.UserName}) at {DateTime.Now.ToString("dd-MM-yyyy h:mm:ss")}. <br/>";

                    }

                    acaClass.LastChangedById = HttpUtil.Profile.UserId;
                    acaClass.LastChanged = DateTime.Now;


                    DbInstance.SaveChanges();
                    scope.Commit();

                    Aca_ClassJson json = new Aca_ClassJson();

                    acaClass.Map(json);
                    result.Data = json;

                }
                catch (Exception e)
                {
                    result.HasError = true;
                    result.Errors.Add(e.Message.ToString());
                    return result;
                }
            }
            return result;
        }

      
        public HttpResult<Aca_CurriculumJson> GetOfferAnotherSectionByClassId(long id=0,int numberOfSection=0)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_CurriculumJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.OfferedClass.CanOfferAnotherSection, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {

                if (id <= 0)
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid Class");
                    return result;
                }

                var classToDuplicate = DbInstance.Aca_Class
                    .Include(x=>x.Aca_CurriculumCourse) //use for get Course Name
                    .SingleOrDefault(x => x.Id == id);

                if (classToDuplicate == null)
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid Class");
                    return result;
                }

                if (numberOfSection<=0 || numberOfSection>10)
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid Number of Section, Please give 1 to 10");
                    return result;
                }

                // todo fix 
                // Get Class list for checking for this class Exist or not
                /*var existThisCourseClassList = DbInstance.Aca_Class
                    .Where(x => x.Id != classToDuplicate.Id
                                       && x.ClassSectionId == classToDuplicate.ClassSectionId
                                       && x.CurriculumCourseId == classToDuplicate.CurriculumCourseId
                                       //&& x.StudyLevelTermId == newObj.StudyLevelTermId
                                       && x.SemesterId == classToDuplicate.SemesterId
                                       && x.ProgramId == classToDuplicate.ProgramId
                                       && x.CampusId == classToDuplicate.CampusId
                ).ToList();*/


                // Get Max ClassSectionId for this Semester & this LevelTerm
                var maxClassSectionInDb = DbInstance.Aca_Class.Where(x =>
                    x.CurriculumCourseId == classToDuplicate.CurriculumCourseId
                    && x.SemesterId == classToDuplicate.SemesterId
                    && x.StudyLevelTermId == classToDuplicate.StudyLevelTermId
                    ).Max(x => x.ClassSectionId);

                // Class Section List get, for create class name by section name.  
                var classSectionList = DbInstance.Aca_ClassSection.ToList();

                var newClassList =new List<Aca_Class>();

                // Generate Class
                for (int i = 1; i <= numberOfSection; i++)
                {


                    /*/* todo fix 
                     * if same Course+Section, exist in same program, same semester but another Level Term
                     * Then Change Class Name like 'Analytic Mechanics [A-L1T1]'
                     #1#
                    var isExistAnotherLevelTerm = existThisCourseClassList.Any(x => x.StudyLevelTermId != newObj.StudyLevelTermId);

                    if (isExistAnotherLevelTerm)
                    {
                        var classSection = DbInstance.Aca_ClassSection.SingleOrDefault(x => x.Id == newObj.ClassSectionId);
                        if (classSection == null)
                        {
                            error += "Please select Valid Section.";
                            return false;
                        }

                        var levelTerm = DbInstance.Aca_StudyLevelTerm.SingleOrDefault(x => x.Id == newObj.StudyLevelTermId);
                        if (levelTerm == null)
                        {
                            error += "Please select valid Study Level Term.";
                            return false;
                        }

                        // Analytic Mechanics [A-L1T1]
                        newObj.Name = $"{curriculumCourse.Name} [{classSection.Name}-{levelTerm.ShortName.Replace("-", "")}]";
                    }*/




                    var classSection = classSectionList.SingleOrDefault(x => x.Id == maxClassSectionInDb + i);

                    if (classSection==null)
                    {
                        result.HasError = true;
                        result.Errors.Add("Section Name Overflowed Please add more Section Name in Class Section Name List");
                        return result;
                    }

                    var newClass = Aca_Class.GetNew(BigInt.NewBigInt());
                    CopyUtil.CopySelectedColumns(classToDuplicate, newClass);
                    newClass.Name = $"{classToDuplicate.Aca_CurriculumCourse.Name} [{classSection.Name}]";

                    newClass.ClassSectionId = classSection.Id;

                    newClass.CreateById = HttpUtil.Profile.UserId;
                    newClass.CreateDate = DateTime.Now;
                    newClass.LastChangedById = HttpUtil.Profile.UserId;
                    newClass.LastChanged = DateTime.Now;

                    newClassList.Add(newClass);


                }

                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {
                    if (newClassList.Count > 0)
                    {
                        DbInstance.Aca_Class.AddRange(newClassList);
                    }
                    DbInstance.SaveChanges();
                    scope.Commit();
                }
            }

            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }

        public HttpResult<Aca_CurriculumJson> GetOfferedFromPreviousSemester(int sourceSemesterId = 0,int destinationSemesterId = 0,int programId=0)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_CurriculumJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.OfferedClass.CanOfferedFromPreviousSemester, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {

                if (sourceSemesterId <= 0)
                {
                    result.HasError = true;
                    result.Errors.Add("Please select Source Semester!");
                    return result;
                }

                if (destinationSemesterId <= 0)
                {
                    result.HasError = true;
                    result.Errors.Add("Please select Destination Semester!");
                    return result;
                }
                if (destinationSemesterId == sourceSemesterId)
                {
                    result.HasError = true;
                    result.Errors.Add("Source semester and Destination Semester can not be Same!");
                    return result;
                }

                
                if (programId <= 0)
                {
                    result.HasError = true;
                    result.Errors.Add("Please select Program!");
                    return result;
                }

                

                var sourceClassList = DbInstance.Aca_Class
                    .Include(x => x.Aca_CurriculumCourse)//use for get Course Name
                    .Where(x => x.SemesterId == sourceSemesterId
                                && x.Aca_CurriculumCourse.Aca_Curriculum.ProgramId==programId
                                ).ToList();

                if (sourceClassList.Count<=0)
                {
                    result.HasError = true;
                    result.Errors.Add("Doesn't have any classes at source Semester!");
                    return result;
                }

                // Get Destination Semester Class List from database for checking section conflict
                var destinationSemClassListInDb = DbInstance.Aca_Class
                    .Where(x => x.SemesterId == destinationSemesterId 
                                  && x.Aca_CurriculumCourse.Aca_Curriculum.ProgramId == programId
                                  ).ToList();

                var markDistributionPolicyList = DbInstance.Aca_MarkDistributionPolicy.ToList();


                var newClassList = new List<Aca_Class>();

                // Generate Class loop
                foreach (var sourceClass in sourceClassList)
                {
                    // Check Conflict Section at Destination Semester
                    var conflictSection =destinationSemClassListInDb
                        .FirstOrDefault(x => x.CurriculumCourseId == sourceClass.CurriculumCourseId
                            && x.ClassSectionId == sourceClass.ClassSectionId
                            && x.StudyLevelTermId == sourceClass.StudyLevelTermId
                            && x.CampusId == sourceClass.CampusId
                            );

                    if (conflictSection!=null)
                    {
                        continue;
                    }

                    // Generate new Class
                    var newClass = Aca_Class.GetNew(BigInt.NewBigInt());
                    CopyUtil.CopySelectedColumns(sourceClass, newClass);

                    newClass.SemesterId = destinationSemesterId;
                    newClass.SemesterId = destinationSemesterId;


                    // Latest Active active Mark DistributionPolicy Map
                    /*var markDistributionPolicy = markDistributionPolicyList
                        .SingleOrDefault(x => x.CreditHour == sourceClass.Aca_CurriculumCourse.CreditLoad
                                              && x.CourseCategoryEnumId ==sourceClass.Aca_CurriculumCourse.CourseCategoryEnumId
                                              && x.StatusEnumId==(byte)Aca_MarkDistributionPolicy.StatusEnum.Active);*/


                    // todo temporary use, solve next time previous code is correct but found multiple policy
                    var markDistributionPolicy =
                        markDistributionPolicyList.SingleOrDefault(x =>
                            x.Id == sourceClass.RegularExamMarkDistributionPolicyId);





                    if (markDistributionPolicy==null)
                    {
                        var courseCategoryEnumList = EnumUtil.GetEnumList(typeof(Aca_Course.CourseCategoryEnum));
                        string courseCategoryName=String.Empty;

                        if (courseCategoryEnumList != null)
                        {
                             courseCategoryName = courseCategoryEnumList.SingleOrDefault(x =>
                                x.Id == sourceClass.Aca_CurriculumCourse.CourseCategoryEnumId)
                                ?.Name;
                        }

                        result.HasError = true;
                        result.Errors.Add($"No Active Mark Distribution Policy Found for {sourceClass.Aca_CurriculumCourse.CreditLoad} Credit {courseCategoryName}");
                        return result;
                    }
                    newClass.RegularExamMarkDistributionPolicyId = markDistributionPolicy.Id;

                    //Common property  
                    newClass.CreateById = HttpUtil.Profile.UserId;
                    newClass.CreateDate = DateTime.Now;
                    newClass.LastChangedById = HttpUtil.Profile.UserId;
                    newClass.LastChanged = DateTime.Now;

                    newClassList.Add(newClass);

                }

                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {
                    if (newClassList.Count > 0)
                    {
                        DbInstance.Aca_Class.AddRange(newClassList);
                    }
                    DbInstance.SaveChanges();
                    scope.Commit();
                }
            }

            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }

        #region Add/Edit View Api
        #region Get
        public HttpResult<Aca_ClassJson> GetClassById(long id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_ClassJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_ClassDataService classDataService = new Aca_ClassDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Aca_ClassJson();
                    Aca_Class entity;
                    if (id <= 0)
                    {
                        entity = Aca_Class.GetNew();
                        entity.RegularCapacity = 100;
                    }
                    else
                    {
                        entity = classDataService.GetById(id);
                    }
                    entity.Map(json);
                    result.Data = json;

                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }

        public HttpResult<ClassHistoryJson> GetClassHistoryById(long id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<ClassHistoryJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.StudentCourseRegistration.CanView, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                if (id <= 0)
                {
                    result.HasError = true;
                    result.Errors.Add("Class Id is not valid.");
                    return result;
                }

                var classObj = DbInstance.Aca_Class
                    .FirstOrDefault(x => x.Id == id);
                if (classObj == null)
                {
                    result.HasError = true;
                    result.Errors.Add("Class not found.");
                    return result;
                }

                var createdByName = DbInstance.User_Account.FirstOrDefault(x => x.Id == classObj.CreateById)?.FullName;
                var lastChangeByName = DbInstance.User_Account.FirstOrDefault(x => x.Id == classObj.LastChangedById)?.FullName;

                var historyJson = new ClassHistoryJson()
                {
                    CreateByName = createdByName,
                    CreateAt = classObj.CreateDate,
                    LastChangedByName = lastChangeByName,
                    LastChangedAt = classObj.LastChanged,
                    GeneralHistory = classObj.History,
                    LockUnlockHistory = classObj.LockUnlockHistory
                };

                result.Data = historyJson;

            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        private HttpResult<Aca_ClassJson> GetClassByIdWithChild(long id)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_ClassJson>();
            try
            {
                using (Aca_ClassDataService classDataService = new Aca_ClassDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Aca_ClassJson();
                    Aca_Class entity;
                    if (id <= 0)
                    {
                        entity = Aca_Class.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement later
                        //includeTables.Add("Aca_Class");
                        //includeTables.Add("Aca_Class");

                        entity = classDataService.GetById(id, includeTables.ToArray());
                    }
                    entity.Map(json);
                    result.Data = json;
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<Aca_ClassJson> GetClassDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_ClassJson>();

            try
            {
                //Aca_ClassDataService classDataService =
                //    new Aca_ClassDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                result.DataExtra.DeptBatchList = DbInstance.Aca_DeptBatch.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.Name }).ToList();


                result.DataExtra.StatusEnumList = EnumUtil.GetEnumList(typeof(Aca_Class.StatusEnum));
                result.DataExtra.ProgramTypeEnumList = EnumUtil.GetEnumList(typeof(Aca_Program.ProgramTypeEnum));
                //DropDown Option Tables

                result.DataExtra.ClassSectionList = DbInstance.Aca_ClassSection.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.Name }).ToList();
                //result.DataExtra.SemesterLevelTermList = DbInstance.Aca_SemesterLevelTerm
                //    .OrderByDescending(x => x.Aca_Semester.Year)
                //    .ThenBy(x => x.Aca_StudyLevelTerm.Aca_StudyLevel.Name)
                //    .ThenBy(x => x.Aca_StudyLevelTerm.Aca_StudyTerm.Name)
                //    .AsEnumerable().Select(t => new
                //    { Id = t.Id.ToString(), Name = t.Name })
                //.ToList();
                result.DataExtra.SemesterList = Aca_Semester.GetDropdownList(DbInstance);

                result.DataExtra.CampusList = DbInstance.General_Campus.AsEnumerable().Select(t => new
                {
                    Id = t.Id,
                    Name = t.ShortName
                }).OrderByDescending(x => x.Name);

                var depts = DbInstance.HR_Department
                  .OrderBy(x => x.Name)
                  .ThenBy(x => x.TypeEnumId)
                  .ToList();

                result.DataExtra.DepartmentList = depts.AsEnumerable()
                    .OrderBy(x => x.ShortName)
                    .Where(x => x.TypeEnumId == 2)
                    //.OrderBy(x => x.DepartmentNo)
                    .Select(t => new
                    { Id = t.Id, Name = t.Name, ShortName = t.ShortName }).ToList();

          
                result.DataExtra.AllDepartmentList = depts.AsEnumerable()
                    .OrderBy(x => x.Name)
                    .Select(t => new
                    {
                        Id = t.Id,
                        Name = t.Name,// + " (" + t.ShortName + ")",
                        Type = t.Type.ToString().AddSpacesToSentence()
                    });


                result.DataExtra.ProgramList = DbInstance.Aca_Program
                     .OrderBy(x => x.ShortName)
                    .AsEnumerable().Select(t => new
                {
                    Id = t.Id
                    ,Name = t.Name
                    ,ShortName = t.ShortName
                    ,ShortTitle = t.ShortTitle
                }).ToList();
                result.DataExtra.CurriculumList = DbInstance.Aca_Curriculum.OrderBy(x=>x.Id).AsEnumerable().Select(t => new
                {
                    Id = t.Id.ToString()
                    ,Name = t.Name
                    ,ShortName = t.ShortName
                    ,ProgramId = t.ProgramId
                    //,DepartmentId = t.DepartmentId
                    , IsOffering = t.IsOffering
                }).ToList();
                result.DataExtra.StudyLevelTermList = DbInstance.Aca_StudyLevelTerm.AsEnumerable().Select(t => new
                {
                    Id = t.Id,
                    Name = t.Name,
                    SemesterDurationEnumId = t.SemesterDurationEnumId
                });

                result.DataExtra.MarkDistributionPolicyList = DbInstance.Aca_MarkDistributionPolicy
                    .AsEnumerable()
                    .Select(t => new
                    {
                        Id = t.Id.ToString(),
                        Name = t.Name,
                        CreditHour = t.CreditHour,
                        CourseCategoryEnumId = t.CourseCategoryEnumId,
                        ExamTypeEnumId = t.ExamTypeEnumId,
                        //IsBachelorProgram = t.IsBachelorProgram
                    })
                .OrderBy(x => x.Name)
                .ThenBy(x => x.CreditHour)
                //.ThenBy(x => x.IsBachelorProgram)
                .ToList();

                var classStudentsJson = new ClassStudentsJson();
                classStudentsJson.User_StudentJsonList = new List<User_StudentJson>();
                result.DataExtra.ClassStudentsJson = classStudentsJson;

                result.DataExtra.ClassEmployeesJson = new ClassEmployeesJson();

                result.DataExtra.EmployeeRoleEnumList = EnumUtil.GetEnumList(typeof(Aca_ClassTakenByEmployee.RoleEnum));
                result.DataExtra.EmployeeExaminerRoleEnumList = EnumUtil.GetEnumList(typeof(Aca_ClassTakenByEmployee.ExaminerRoleEnum));
                result.DataExtra.EmployeeScrutinizerRoleEnumList = EnumUtil.GetEnumList(typeof(Aca_ClassTakenByEmployee.ScrutinizerRoleEnum));
                result.DataExtra.EmployeeSectionEnumList = EnumUtil.GetEnumList(typeof(Aca_ClassTakenByEmployee.SectionEnum));
                result.DataExtra.EmployeeStatusEnumList = EnumUtil.GetEnumList(typeof(Aca_ClassTakenByEmployee.StatusEnum));

                result.DataExtra.StudentEnrollTypeEnumList = EnumUtil.GetEnumList(typeof(Aca_ClassTakenByStudent.EnrollTypeEnum));
                result.DataExtra.StudentRegistrationStatusEnumList = EnumUtil.GetEnumList(typeof(Aca_ClassTakenByStudent.RegistrationStatusEnum));
                result.DataExtra.StudentStatusEnumList = EnumUtil.GetEnumList(typeof(Aca_ClassTakenByStudent.StatusEnum));
                //For Class Routine
                result.DataExtra.DayEnumList = EnumUtil.GetEnumList(typeof(EnumCollection.Common.DayEnum));

                //todo room should load after class create,and select only campus of selected class 
                result.DataExtra.RoomList = DbInstance.General_Room
                    .Include(x => x.General_Floor)
                    .Include(x => x.General_Building)
                    .Include(x => x.General_Building.General_Campus)
                    .Include(x => x.HR_Department)
                    .OrderBy(x=>x.General_Floor.FloorNo)
                    .ThenBy(x=>x.RoomNo)
                    .AsEnumerable().Select(t => new
                { Id = t.Id.ToString()
                , Name = t.Name
                , RoomNo = t.RoomNo
                , FloorNo = t.General_Floor.FloorNo
                , FloorName = t.General_Floor.Name
                , BuildingName = t.General_Building.Name
                , CampusName = t.General_Building.General_Campus.Name
                , DepartmentId = t.DepartmentId
                , DeptShortName = t.HR_Department.ShortName
                , SeatCapacityRegular = t.CapacityRegular
                    }).ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }

        public HttpListResult<Aca_MarkDistributionPolicyJson> GetMarkDistributionPolicyList(float creditHour=0, int courseCategoryEnumId=-1,int semesterId=0, int? programId=null,bool isNewCreateMode=false)
        {
            var result = new HttpListResult<Aca_MarkDistributionPolicyJson>();
            result.DataExtra.SelectedMarkDistributionPolicyId = 0;

            if (creditHour<=0)
            {
                result.HasError = true;
                result.Errors.Add("Invalid Credit Hour ");
                return result;
            }

            if (courseCategoryEnumId<0)
            {
                result.HasError = true;
                result.Errors.Add("Invalid Course Category!");
                return result;
            }

            if (semesterId<=0)
            {
                result.HasError = true;
                result.Errors.Add("Invalid Semester!");
                return result;
            }

            if (programId!=null && programId<=0)
            {
                result.HasError = true;
                result.Errors.Add("Invalid Program!");
                return result;
            }

            try
            {
                //calling Facade to Get Mark Distribution Policies
                var markDistributionPolicyJsonList = Facade.AcademicFacade.ClassFacade.GetMarkDistributionPolicyList(creditHour, courseCategoryEnumId, semesterId , programId, isNewCreateMode);

                if (markDistributionPolicyJsonList.Count > 0)
                {
                    if (markDistributionPolicyJsonList.Count == 1)
                    {
                        result.DataExtra.SelectedMarkDistributionPolicyId = markDistributionPolicyJsonList[0].Id.ToString();
                    }

                    result.Data = markDistributionPolicyJsonList;
                    return result;
                }

                // All Program wise End

                if (markDistributionPolicyJsonList.Count<=0)
                {
                    result.HasError = true;
                    result.Errors.Add("Mark Distribution Policy Not Found!");
                    return result;
                }

            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.Message);
                return result;
            }

            return result;

        }

        public HttpListResult<Aca_ClassJson> GetBulkClassDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_ClassJson>();


            try
            {
                result.DataExtra.StatusEnumList = EnumUtil.GetEnumList(typeof(Aca_Class.StatusEnum));
                result.DataExtra.ProgramTypeEnumList = EnumUtil.GetEnumList(typeof(Aca_Program.ProgramTypeEnum));
                //result.DataExtra.SemesterLevelTermList = DbInstance.Aca_SemesterLevelTerm
                //    .OrderByDescending(x => x.Aca_Semester.Year)
                //    .ThenBy(x => x.Aca_StudyLevelTerm.Aca_StudyLevel.Name)
                //    .ThenBy(x => x.Aca_StudyLevelTerm.Aca_StudyTerm.Name)
                //    .AsEnumerable().Select(t => new
                //    { Id = t.Id.ToString(), Name = t.Name })
                //.ToList();
                result.DataExtra.SemesterList = DbInstance.Aca_Semester
                    .OrderByDescending(x => x.Year)
                    .ThenByDescending(x => x.TermId)
                    .AsEnumerable().Select(t => new
                    {
                        Id = t.Id.ToString(),
                        Name = t.Name
                    });
                result.DataExtra.CampusList = DbInstance.General_Campus.AsEnumerable().Select(t => new
                {
                    Id = t.Id.ToString(),
                    Name = t.ShortName
                }).OrderByDescending(x => x.Name);
                result.DataExtra.DepartmentList = DbInstance.HR_Department.AsEnumerable()
                    .OrderBy(x => x.ShortName)
                    .Where(x => x.TypeEnumId == 2)
                    .OrderBy(x => x.DepartmentNo)
                    .Select(t => new
                    { Id = t.Id, Name = t.Name, ShortName = t.ShortName }).ToList();
                result.DataExtra.ProgramList = DbInstance.Aca_Program.AsEnumerable().Select(t => new
                {
                    Id = t.Id
                    ,Name = t.Name
                    ,ShortName = t.ShortName
                    ,ShortTitle = t.ShortTitle
                }).ToList();
                result.DataExtra.CurriculumList = DbInstance.Aca_Curriculum
                    .AsEnumerable().Select(t => new
                {
                    Id = t.Id.ToString()
                    ,Name = t.Name
                    ,ShortName = t.ShortName
                    ,ProgramId = t.ProgramId
                }).ToList();
                result.DataExtra.StudyLevelTermList = DbInstance.Aca_StudyLevelTerm.AsEnumerable().Select(t => new
                {
                    Id = t.Id,
                    Name = t.Name
                });

                var classStudentsJson = new ClassStudentsJson();
                classStudentsJson.User_StudentJsonList = new List<User_StudentJson>();
                result.DataExtra.ClassStudentsJson = classStudentsJson;
                var classEmployeesJson = new ClassEmployeesJson();
                classEmployeesJson.User_EmployeeJsonList = new List<User_EmployeeJson>();
                result.DataExtra.ClassEmployeesJson = classEmployeesJson;

                result.DataExtra.EmployeeRoleEnumList = EnumUtil.GetEnumList(typeof(Aca_ClassTakenByEmployee.RoleEnum));
                result.DataExtra.EmployeeSectionEnumList = EnumUtil.GetEnumList(typeof(Aca_ClassTakenByEmployee.SectionEnum));
                result.DataExtra.EmployeeStatusEnumList = EnumUtil.GetEnumList(typeof(Aca_ClassTakenByEmployee.StatusEnum));

                result.DataExtra.StudentEnrollTypeEnumList = EnumUtil.GetEnumList(typeof(Aca_ClassTakenByStudent.EnrollTypeEnum));
                result.DataExtra.StudentRegistrationStatusEnumList = EnumUtil.GetEnumList(typeof(Aca_ClassTakenByStudent.RegistrationStatusEnum));
                result.DataExtra.StudentStatusEnumList = EnumUtil.GetEnumList(typeof(Aca_ClassTakenByStudent.StatusEnum));
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        #endregion Get
        #endregion Add/Edit View Api

        #region CurriculumCourse
        public HttpListResult<Aca_CurriculumCourseJson> GetCurriculumCourse(string textkey, int? pageSize, int? pageNo
           , Int64 curriculumId = 0
           , Int64 electiveGroupId = 0
           , Int32 studyLevelTermId = 0
           , Int64 offeredByDepartmentId = 0
         )
                {
                    string error = string.Empty;
                    int count = 0;
                    var result = new HttpListResult<Aca_CurriculumCourseJson>();
                    if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.CurriculumCourse.CanView, out error)
                        && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.CurriculumCourse.CanAdd, out error)
                        && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.CurriculumCourse.CanEdit, out error))
                    {
                        result.HasError = true;
                        result.Errors.Add(error);
                        return result;
                    }
                    try
                    {
                        using (Aca_CurriculumCourseDataService curriculumCourseDataService = new Aca_CurriculumCourseDataService(DbInstance, HttpUtil.Profile))
                        {
                            IQueryable<Aca_CurriculumCourse> query = DbInstance.Aca_CurriculumCourse
                                .Include("Aca_StudyLevelTerm")
                                .Where(x => x.CourseTypeEnumId !=
                                            (byte)Aca_CurriculumCourse.CourseTypeEnum.CoreElective)
                                .OrderBy(x => x.Aca_Curriculum.CurriculumNo)
                                .ThenBy(x => x.Aca_Curriculum.Aca_Program.ProgramTypeEnumId)
                                .ThenByDescending(x => x.Aca_Curriculum.Aca_Program.Id)
                                .ThenByDescending(x => x.CurriculumId)
                                .ThenBy(x => x.Code);
                            if (textkey.IsValid())
                            {
                                query = query.Where(q => q.Name.ToLower().Contains(textkey.ToLower()));
                            }
                            if (curriculumId > 0)
                            {
                                query = query.Where(q => q.CurriculumId == curriculumId);
                            }
                            if (electiveGroupId > 0)
                            {
                                query = query.Where(q => q.ElectiveGroupId == electiveGroupId);
                            }
                           

                            var entities = curriculumCourseDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                            var jsons = new List<Aca_CurriculumCourseJson>();
                            entities.Map(jsons);

                            result.Data = jsons;
                            result.Count = count;
                        }
                    }
                    catch (Exception ex)
                    {
                        result.HasError = true;
                        result.Errors.Add(ex.GetBaseException().Message.ToString());
                    }
                    return result;
                }

        //todo need rename
        public HttpResult<Aca_CurriculumCourseJson> GetCurriculumCourseById(long id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_CurriculumCourseJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.CurriculumCourse.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.CurriculumCourse.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.CurriculumCourse.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_CurriculumCourseDataService curriculumCourseDataService = new Aca_CurriculumCourseDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Aca_CurriculumCourseJson();
                    Aca_CurriculumCourse entity;
                    if (id <= 0)
                    {
                        entity = Aca_CurriculumCourse.GetNew();
                    }
                    else
                    {
                        entity = curriculumCourseDataService.GetById(id);
                    }
                    entity.Map(json);
                    result.Data = json;

                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }


        #endregion CurriculumCourse
    }
}
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases.WebApi;
using EMS.Web.Jsons.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using EMS.Facade;
using System.Web;
using System.Web.Mvc;

namespace EMS.Web.UI.Areas.ExamSection.Controllers.WebApi
{
    public class ClassWiseAttendancePrintApiController : EmployeeBaseApiController
    {
        public HttpListResult<Aca_SemesterJson> GetClassWiseAttendancePrintDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_SemesterJson>();
            //if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.Course.CanView, out error)
            //    && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.Course.CanAdd, out error)
            //    && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.Course.CanEdit, out error))
            //{
            //    result.HasError = true;
            //    result.Errors.Add(error);
            //    return result;
            //}
            try
            {

                //DropDown Option Tables
                result.DataExtra.ProgramTypeEnumList = EnumUtil.GetEnumList(typeof(Aca_Program.ProgramTypeEnum));

                result.DataExtra.SemesterList = DbInstance.Aca_Semester.Where(x=>x.IsEnable)
                  .OrderByDescending(x => x.Id)
                  //.ThenBy(x => x.Aca_StudyLevelTerm.Aca_StudyLevel.Id)
                  //.ThenBy(x => x.Aca_StudyLevelTerm.Aca_StudyTerm.Id)
                  .AsEnumerable().Select(t => new { Id = t.Id, Name = t.Name , SemesterDurationEnumId = t.SemesterDurationEnumId }).ToList();

                result.DataExtra.StudyLevelTermList = DbInstance.Aca_StudyLevelTerm.OrderBy(x=>x.Id).AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.Name }).ToList();

                result.DataExtra.ProgramList = DbInstance.Aca_Program.AsEnumerable()
                    .OrderBy(x => x.ShortName)
                    .Where(x => !x.IsDeleted)
                    .Select(t => new
                    { Id = t.Id, Name = t.Name, ShortName = t.ShortName, ShortTitle = t.ShortTitle})
                    .ToList();

                //result.DataExtra.AdmitCardDownloadTypeEnumList = EnumUtil.GetEnumList(typeof(ReportController.AdmitCardDownloadTypeEnum));
                //result.DataExtra.DeptBatchList = DbInstance.Aca_DeptBatch.AsEnumerable()
                //    .OrderBy(x => x.BatchNo)
                //    .Select(t => new
                //    { Id = t.Id, Name = t.Name })
                //    .ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }


        public HttpListResult<Aca_CurriculumCourseJson> GetOfferedCourseListBySemesterIdProgramId(
                int semesterId = 0
              , Int64 programId = 0
              , Int64 levelTermId = 0
         //, bool isOnlyRegisteredStd=false
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Aca_CurriculumCourseJson>();
            //if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ClassManager.Class.CanView, out error)
            //    && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ClassManager.Class.CanAdd, out error)
            //    && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ClassManager.Class.CanEdit, out error))
            //{
            //    result.HasError = true;
            //    result.Errors.Add(error);
            //    return result;
            //}
            if (semesterId <= 0)
            {
                result.HasError = true;
                result.Errors.Add("Please Select Valid Semester!");
                return result;
            }

            if (programId <= 0)
            {
                result.HasError = true;
                result.Errors.Add("Please Select Valid Program!");
                return result;
            }
            //if (levelTermId <= 0)
            //{
            //    result.HasError = true;
            //    result.Errors.Add("Please Select Only One Batch at a Time!");
            //    return result;
            //}
            try
            {

                IQueryable<Aca_Class> query = (from q in DbInstance.Aca_Class select q)
                                            .Include(x => x.Aca_CurriculumCourse)
                                            .Include(x => x.Aca_CurriculumCourse.Aca_Curriculum)
                                            //.Include(x => x.HR_Department)
                                            .OrderBy(x => x.Aca_CurriculumCourse.Code)
                                            .Where(x => !x.IsDeleted);
                if (programId > 0)
                {
                    query = query.Where(q => q.Aca_CurriculumCourse.Aca_Curriculum.ProgramId == programId);
                }

                if (semesterId > 0)
                {
                    query = query.Where(q => q.SemesterId == semesterId);
                }

                if (levelTermId > 0)
                {
                    query = query.Where(q => q.StudyLevelTermId == levelTermId);
                }


                var offeredCourseList = query.DistinctBy(x => x.CurriculumCourseId)
                   ?.Select(x => x.Aca_CurriculumCourse)?.ToList();

                if (offeredCourseList == null || offeredCourseList.Count <= 0)
                {
                    result.HasError = true;
                    result.Errors.Add("No Offered CourseList Found According to Your Search!");
                    return result;
                }

                // Distinct for Only Registered Student List
                //var studentList = entitiesClassTakenByStudent
                //    .DistinctBy(x => x.StudentId)
                //    .Select(x => x.User_Student).ToList();

                //var offeredCourseListDropDown=
                var jsons = new List<Aca_CurriculumCourseJson>();
                offeredCourseList = offeredCourseList.OrderBy(x => x.Code).ToList();
                offeredCourseList.Map(jsons);
                result.Data = jsons;
                result.Count = jsons.Count;

            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }

        public HttpListResult<Aca_ClassJson> GetClassSectionListByCourseId(
                int semesterId = 0
              , Int64 programId = 0
              , Int64 courseId = 0)
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Aca_ClassJson>();
            //if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ClassManager.Class.CanView, out error)
            //    && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ClassManager.Class.CanAdd, out error)
            //    && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ClassManager.Class.CanEdit, out error))
            //{
            //    result.HasError = true;
            //    result.Errors.Add(error);
            //    return result;
            //}
            if (semesterId <= 0)
            {
                result.HasError = true;
                result.Errors.Add("Please Select Valid Semester!");
                return result;
            }

            if (programId <= 0)
            {
                result.HasError = true;
                result.Errors.Add("Please Select Valid Program!");
                return result;
            }
            if (courseId <= 0)
            {
                result.HasError = true;
                result.Errors.Add("Please Select Valid Course!");
                return result;
            }
            try
            {

                IQueryable<Aca_Class> query = (from q in DbInstance.Aca_Class select q)
                                            .Include(x => x.Aca_CurriculumCourse)
                                            .Include(x => x.Aca_CurriculumCourse.Aca_Curriculum)
                                            //.Include(x => x.HR_Department)
                                            //.OrderBy(x => x.Aca_CurriculumCourse.Code)
                                            .Where(x => !x.IsDeleted);
                if (programId > 0)
                {
                    query = query.Where(q => q.Aca_CurriculumCourse.Aca_Curriculum.ProgramId == programId);
                }

                if (semesterId > 0)
                {
                    query = query.Where(q => q.SemesterId == semesterId);
                }

                if (courseId > 0)
                {
                    query = query.Where(q => q.CurriculumCourseId == courseId);
                }


                var classSectionList = query.ToList();

                if (classSectionList == null || classSectionList.Count <= 0)
                {
                    result.HasError = true;
                    result.Errors.Add("No Offered CourseList Found According to Your Search!");
                    return result;
                }


                //var offeredCourseListDropDown=
                var jsons = new List<Aca_ClassJson>();
                classSectionList = classSectionList.OrderBy(x => x.ClassSectionId).ToList();
                classSectionList.Map(jsons);
                result.Data = jsons;
                result.Count = jsons.Count;

            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }

    }
}
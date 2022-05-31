using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations.Sql;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web.Http;
using System.Windows.Input;
using EMS.CoreLibrary;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases.WebApi;
using EMS.Framework.Objects;
using EMS.Framework.Settings;
//using EMS.Web.Jsons.Mapper;
using EMS.Web.Jsons.Models;

namespace EMS.Web.UI.Areas.Academic.Controllers.WebApi
{
    public class BulkCourseOfferingMangerApiController : EmployeeBaseApiController
    {
        #region OfferAbleCourseListJson

        public class OfferAbleCourseJson
        {
            public string CurriculumCourseName { get; set; }
            public string CurriculumCourseId { get; set; }
            public string Code { get; set; }
            public float CreditHour { get; set; }
            public byte CourseCategoryEnumId { get; set; }
            public int LevelTermId { get; set; }
            public int? DeptBatchId { get; set; }
            public string MarkDistributionId { get; set; }
            public int SectionCount { get; set; }
            public short AvailableSit { get; set; }
            public string SemesterId { get; set; }
            public int ProgramId { get; set; }
            public int DepartmentId { get; set; }
            public bool IsSelected { get; set; }
            public List<Aca_MarkDistributionPolicyJson> MarkDistributionList { get; set; }

            public OfferAbleCourseJson()
            {
                IsSelected = false;
                MarkDistributionList = new List<Aca_MarkDistributionPolicyJson>();
            }

        }

        #endregion

        // POST api/<controller>
        [HttpPost]
        public HttpResult<bool> SaveBulkClass(List<OfferAbleCourseJson> json)
        {  //add/edit class
            string error = string.Empty;
            var result = new HttpResult<bool>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.OfferedClass.CanOfferBulkCourse, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            var offerAbleCourseList = json.Where(x => x.IsSelected).ToList();

            if (offerAbleCourseList.Count <= 0)
            {
                result.HasError = true;
                result.Errors.Add("Please Select At least One Course For Offering Course Registration.");
                return result;
            }

            //if (offerAbleCourseList.Count == 1)
            //{
            //    result.HasError = true;
            //    result.Errors.Add("Please  Registration.");
            //    return result;
            //}

            using (var scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
            {
              
                try
                {
              
                    foreach (var course in offerAbleCourseList)
                    {
                      
                        var sectionId = 1;
                        if (course.SectionCount <= 0)
                        {
                            scope.Rollback();
                            result.HasError = true;
                            result.Errors.Add("Section Count of Selected Course Cannot be Zero or Less.");
                            return result;
                        }
                        for (int i = 0; i < course.SectionCount;i++)
                        {
                            var entity = Aca_Class.GetNew();
                            entity.CurriculumCourseId = Convert.ToInt64(course.CurriculumCourseId);
                            entity.SemesterId = Convert.ToInt64(course.SemesterId);
                            entity.StudyLevelTermId = course.LevelTermId;
                            entity.StudentBatchId = course.DeptBatchId;
                            entity.RegularExamMarkDistributionPolicyId = Convert.ToInt64(course.MarkDistributionId);
                            entity.RegularCapacity = course.AvailableSit;
                            entity.ProgramId = course.ProgramId;
                            entity.DepartmentId = course.DepartmentId;
                            entity.Code = course.Code;

                            var isSectionTaken = DbInstance.Aca_Class
                                .Any(x => x.CurriculumCourseId == entity.CurriculumCourseId 
                                          && x.SemesterId == entity.SemesterId 
                                          && x.StudyLevelTermId == entity.StudyLevelTermId
                                          && (x.StudentBatchId == null || x.StudentBatchId == entity.StudentBatchId)
                                          && x.ClassSectionId == sectionId);

                            //next class will get updated section.
                            
                            if (isSectionTaken)
                            {
                                i--;
                                sectionId++;
                                continue;
                            }

                            entity.ClassSectionId = sectionId;
                            sectionId++;

                            entity.History += $"Class was Offered from Bulk Offered Panel, by {HttpUtil.Profile.Name}[{HttpUtil.Profile.UserName}] at {DateTime.Now}, IP:{HttpUtil.GetUserIp()}<br>"; ;

                            if (!Facade.AcademicFacade.ClassFacade.SaveClass(entity, out error,scope))
                            {
                                scope.Rollback();
                                result.HasError = true;
                                result.Errors.Add(error);
                                return result;
                            }

                            DbInstance.SaveChanges();
                        }
                    }

                    scope.Commit();
                }
                catch (Exception e)
                {
                    scope.Rollback();
                    result.HasError = true;
                    result.Errors.Add(e.GetBaseException().Message);
                    return result;
                }
            }

            result.Data = false;
            return result;
        }

        #region Add/Edit View Api
        #region Get


        public HttpListResult<bool> GetBulkCourseOfferingDataExtra(int programId=0)
        {
            string error = string.Empty;
            var result = new HttpListResult<bool>();

            try
            {
                result.DataExtra.SemesterList = Aca_Semester.GetDropdownList(DbInstance);

                result.DataExtra.ProgramList = DbInstance.Aca_Program
                     .OrderBy(x => x.ShortName)
                    .AsEnumerable().Select(t => new
                {
                    Id = t.Id
                    ,Name = t.Name
                    ,ShortName = t.ShortName
                    ,ShortTitle = t.ShortTitle
                }).ToList();

                result.DataExtra.CurriculumList = DbInstance.Aca_Curriculum.Where(x=>x.ProgramId == programId).AsEnumerable().Select(t => new
                {
                    Id = t.Id.ToString()
                    ,Name = t.Name
                    ,ShortName = t.ShortName
                    ,ProgramId = t.ProgramId
                }).OrderByDescending(x => x.Id).ToList();

                result.DataExtra.StudyLevelTermList = DbInstance.Aca_StudyLevelTerm.AsEnumerable().Select(t => new
                {
                    Id = t.Id,
                    Name = t.Name,
                    SemesterDurationEnumId = t.SemesterDurationEnumId
                });

                result.DataExtra.DeptBatchList = DbInstance.Aca_DeptBatch.AsEnumerable().Select(t => new
                    { Id = t.Id, Name = t.Name }).ToList();

            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<bool> GetCurriculumByProgramId(int programId = 0)
        {
            string error = string.Empty;
            var result = new HttpListResult<bool>();

            try
            {
                if (programId <= 0)
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid Program Id");
                    return result;
                }
                result.DataExtra.CurriculumList = DbInstance.Aca_Curriculum.Where(x => x.ProgramId == programId).AsEnumerable().Select(t => new
                {
                    Id = t.Id.ToString()
                    ,
                    Name = t.Name
                    ,
                    ShortName = t.ShortName
                    ,
                    ProgramId = t.ProgramId
                    ,
                    //DepartmentId = t.DepartmentId
                }).OrderByDescending(x=>x.Id).ToList(); ;

            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }

        public HttpListResult<OfferAbleCourseJson> GetOfferAbleCourseList(long semesterId=0,int programId=0,long curriculumId = 0, int levelTermId = 0,int? batchId = null)
        {
            var result = new HttpListResult<OfferAbleCourseJson>();
            result.DataExtra.SelectedMarkDistributionPolicyId = 0;
            if (semesterId <= 0)
            {
                result.HasError = true;
                result.Errors.Add("Invalid Semester!");
                return result;
            }
            if (programId <= 0)
            {
                result.HasError = true;
                result.Errors.Add("Invalid Program!");
                return result;
            }
            if (curriculumId <= 0)
            {
                result.HasError = true;
                result.Errors.Add("Invalid Curriculum!");
                return result;
            }

            try
            {
                IQueryable<Aca_CurriculumCourse> query = DbInstance.Aca_CurriculumCourse.Where(x =>
                    x.CurriculumId == curriculumId )
                    .OrderBy(x=>x.Code)
                    .ThenBy(x=>x.Code);

               
                var curriculumCourseList = query
                    .Select(x=> new OfferAbleCourseJson()
                    {
                        CurriculumCourseId = x.Id.ToString(),
                        CurriculumCourseName = x.Code+":"+ x.Name+ " [Cr." + x.CreditLoad + "]",
                        CreditHour = x.CreditLoad,
                        CourseCategoryEnumId = x.CourseCategoryEnumId,
                        DeptBatchId = batchId,
                        SectionCount = 1,
                        AvailableSit = (short)SiteSettings.Instance.DefaultClassSectionRange,
                        SemesterId = semesterId.ToString(),
                        ProgramId = programId,
                        Code = x.Code
                    }).ToList();


                foreach (var curriculumCourse in curriculumCourseList)
                {
                    //calling facade to get MarkDistribution Policy
                    curriculumCourse.MarkDistributionList =
                        Facade.AcademicFacade.ClassFacade.GetMarkDistributionPolicyList(curriculumCourse.CreditHour,
                            curriculumCourse.CourseCategoryEnumId, semesterId, programId, true);

                    var distributionCount = curriculumCourse.MarkDistributionList.Count;
                    if (distributionCount > 0)
                    {
                        if (distributionCount == 1)
                        {
                            curriculumCourse.MarkDistributionId =
                                curriculumCourse.MarkDistributionList[0].Id;
                        }
                    }
                    else
                    {
                        result.HasError = true;
                        result.Errors.Add($"No Mark Distribution found for {curriculumCourse.CurriculumCourseName}. Please Add Proper Mark Distribution for the course first.");
                        return result;
                    }
                }

                result.Data = curriculumCourseList;

                result.DataExtra.DeptBatchList = DbInstance.Aca_DeptBatch.AsEnumerable().Select(t => new
                    { Id = t.Id, Name = t.Name }).ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.Message);
                return result;
            }

            return result;

        }

        #endregion Get
        #endregion Add/Edit View Api

    }
}
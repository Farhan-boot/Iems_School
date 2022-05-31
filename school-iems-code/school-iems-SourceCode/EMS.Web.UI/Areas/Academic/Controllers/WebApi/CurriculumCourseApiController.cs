using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases.WebApi;
using EMS.Framework.Objects;
//using EMS.Web.Jsons.Mapper;
using EMS.Web.Jsons.Models;

namespace EMS.Web.UI.Areas.Academic.Controllers.WebApi
{
    public class CurriculumCourseApiController : EmployeeBaseApiController
    {

        #region PreRequisiteCourse Json

        public class PreRequisiteCourseJson
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public float CreditHour { get; set; }
            public byte CategoryTypeEnumId { get; set; }

            public string CategoryType
            {
                get
                {
                    return ((Aca_Course.CourseCategoryEnum) CategoryTypeEnumId).ToString().AddSpacesToSentence();
                }
            }
        }

        #endregion
        // GET api/<controller>
        public CurriculumCourseApiController()
        { }

        #region CurriculumCourse 
        #region List View Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Aca_CurriculumCourseJson> GetPagedCurriculumCourse(string textkey, int? pageSize, int? pageNo
           , Int64 curriculumId = 0
           , Int64 electiveGroupId = 0
           , Int32 studyLevelTermId = 0
           , Int64 offeredByDepartmentId = 0
           , int creditTypeEnumId = -1
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
                        //.Include(x => x.HR_Department)
                        //.Include(x => x.Aca_StudyLevelTerm)
                        .Include(x => x.Aca_Curriculum)
                        .Include(x => x.Aca_Curriculum.Aca_Program)
                        //.Include(x => x.Aca_Curriculum.Aca_Program.HR_Department)
                        .OrderBy(x => x.Aca_Curriculum.CurriculumNo)
                        .ThenBy(x => x.Aca_Curriculum.Aca_Program.ProgramTypeEnumId)
                        .ThenByDescending(x => x.Aca_Curriculum.Aca_Program.Id)
                        .ThenByDescending(x => x.CurriculumId)
                        //.ThenBy(x => x.StudyLevelTermId)
                        .ThenBy(x => x.Code);
                        //.ThenBy(x => x.OfferedByDepartmentId);
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
        public HttpListResult<Aca_CurriculumCourseJson> GetCurriculumCourseListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_CurriculumCourseJson>();
            try
            {
                //Aca_CurriculumCourseDataService curriculumCourseDataService =
                //    new Aca_CurriculumCourseDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List

                result.DataExtra.CourseCategoryEnumList = EnumUtil.GetEnumList(typeof(Aca_Course.CourseCategoryEnum));
                result.DataExtra.CreditTypeEnumList = EnumUtil.GetEnumList(typeof(Aca_CurriculumCourse.CreditTypeEnum));
                result.DataExtra.CourseTypeEnumList = EnumUtil.GetEnumList(typeof(Aca_CurriculumCourse.CourseTypeEnum));
                result.DataExtra.StatusEnumList = EnumUtil.GetEnumList(typeof(Aca_CurriculumCourse.StatusEnum));
                //DropDown Option Tables
                result.DataExtra.ProgramList = DbInstance.Aca_Program.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.Name, ShortName = t.ShortName, ShortTitle = t.ShortTitle }).ToList();

                result.DataExtra.CurriculumList = DbInstance.Aca_Curriculum.OrderBy(x => x.CurriculumNo).AsEnumerable().Select(t => new
                { Id = t.Id.ToString(), Name = t.Name, ShortName = t.ShortName }).ToList();

                //result.DataExtra.CurriculumElectiveGroupList = DbInstance.Aca_CurriculumElectiveGroup.AsEnumerable().Select(t => new
                //{ Id = t.Id.ToString(),Name = t.Name }).ToList();
                result.DataExtra.StudyLevelTermList = DbInstance.Aca_StudyLevelTerm.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.Name }).ToList();
                //result.DataExtra.DepartmentList = DbInstance.HR_Department.AsEnumerable().Select(t => new
                //{ Id = t.Id.ToString(),Name = t.Name }).ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        /// <summary>
        /// Caution: Very Dangerous to call When table have huge data!
        /// </summary>
        /// <returns></returns>
        private HttpListResult<Aca_CurriculumCourseJson> GetAllCurriculumCourse()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_CurriculumCourseJson>();
            try
            {
                using (Aca_CurriculumCourseDataService curriculumCourseDataService = new Aca_CurriculumCourseDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<Aca_CurriculumCourseJson>();
                    var entities = curriculumCourseDataService.GetAll(out error);
                    entities.Map(jsons);
                    result.Data = jsons;
                    result.Count = jsons.Count;
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        /// <summary>
        /// Todo pending to implement bulk save
        /// </summary>
        /// <param name="jsonList"></param>
        /// <returns></returns>
        [HttpPost]
        private HttpListResult<Aca_CurriculumCourseJson> SaveCurriculumCourseList(IList<Aca_CurriculumCourseJson> jsonList)
        {
            string error = string.Empty;
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
                    var entityListReceived = new List<Aca_CurriculumCourse>();
                    var dbAttachedListEntity = new List<Aca_CurriculumCourse>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (curriculumCourseDataService.Save(entity, ref dbAttachedListEntity, out error))
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

        #endregion

        #region Add/Edit View Api
        #region Get
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

                    /*if (json.IsCheckPrerequisite)
                    {
                        result.DataExtra.PreRequisiteCourseList = DbInstance.Aca_CoursePrerequisiteMap
                            .Include(x=>x.Aca_CurriculumCourse1)
                            .Select(x=>new PreRequisiteCourseJson
                            {
                                Id = x.Id,
                                Name = x.Aca_CurriculumCourse1.Code+" : " +x.Aca_CurriculumCourse1.Name,
                                CreditHour = x.Aca_CurriculumCourse1.CreditHour,
                                CategoryTypeEnumId = x.Aca_CurriculumCourse1.CourseCategoryEnumId
                            })
                            .ToList();
                    }*/

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

        private HttpResult<Aca_CurriculumCourseJson> GetCurriculumCourseByIdWithChild(long id)
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
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("Aca_CurriculumCourse");
                        //includeTables.Add("Aca_CurriculumCourse");

                        entity = curriculumCourseDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Aca_CurriculumCourseJson> GetCurriculumCourseDataExtra()
        {
            string error = string.Empty;
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
                //Aca_CurriculumCourseDataService curriculumCourseDataService =
                //    new Aca_CurriculumCourseDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List

                result.DataExtra.CourseCategoryEnumList = EnumUtil.GetEnumList(typeof(Aca_Course.CourseCategoryEnum));
                result.DataExtra.CreditTypeEnumList = EnumUtil.GetEnumList(typeof(Aca_CurriculumCourse.CreditTypeEnum));
                result.DataExtra.CourseTypeEnumList = EnumUtil.GetEnumList(typeof(Aca_CurriculumCourse.CourseTypeEnum));
                result.DataExtra.StatusEnumList = EnumUtil.GetEnumList(typeof(Aca_CurriculumCourse.StatusEnum));
                //DropDown Option Tables
                result.DataExtra.DepartmentList = DbInstance.HR_Department.AsEnumerable().OrderBy(x=>x.DepartmentNo)
                    .Where(x => x.TypeEnumId == 2)
                    .Select(t => new
                    { Id = t.Id, Name = t.Name, ShortName = t.ShortName }).ToList();

                result.DataExtra.ProgramList = DbInstance.Aca_Program.AsEnumerable().Select(t => new
                { Id = t.Id
                , Name = t.Name
                , ShortName = t.ShortName
                , ShortTitle = t.ShortTitle
                }).ToList();

                result.DataExtra.StudyLevelTermList = DbInstance.Aca_StudyLevelTerm.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.Name});

                result.DataExtra.CurriculumList = DbInstance.Aca_Curriculum.OrderBy(x => x.CurriculumNo).AsEnumerable().Select(t => new
                { Id = t.Id.ToString()
                , Name = t.Name
                , ShortName = t.ShortName
                , ProgramId = t.ProgramId
                //, DepartmentId = t.DepartmentId
                }).ToList();

                result.DataExtra.CurriculumElectiveGroupList = DbInstance.Aca_CurriculumElectiveGroup.OrderBy(x => x.ProgramId).AsEnumerable().Select(t => new
                { Id = t.Id.ToString()
                , Name = t.Name
                , ProgramId = t.ProgramId
                }).ToList();
                
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }

        public HttpListResult<Aca_CurriculumCourseJson> GetAddableCurriculumList(long curriculumId,long curriculumCourseId)
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Aca_CurriculumCourseJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.CurriculumCourse.CanAddPreRequisite, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            try
            {
                var alreadyAddedCourseIds = DbInstance.Aca_CoursePrerequisiteMap
                    .Where(x => x.CourseId == curriculumCourseId).Select(x => x.PrerequisiteCourseId).ToList();

                var curriculumList =  DbInstance.Aca_CurriculumCourse
                    .Where(x => x.CurriculumId == curriculumId && x.Id!=curriculumCourseId && !alreadyAddedCourseIds.Contains(x.Id) 
                                && x.CourseTypeEnumId !=(byte)Aca_CurriculumCourse.CourseTypeEnum.CoreElective)
                    .ToList();
                var json = new List<Aca_CurriculumCourseJson>();

                curriculumList.Map(json);

                result.Data = json;
            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Errors.Add(e.GetBaseException().Message);
                return result;
            }
            return result;
        }
        #endregion

        #region Save/Delete

        public HttpResult<long> GetAddCurriculumCoursePreRequisite(long courseId,
            long preRequisiteCourseId)
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpResult<long>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.CurriculumCourse.CanAddPreRequisite, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                var currentCourse = DbInstance.Aca_CurriculumCourse.FirstOrDefault(x => x.Id == courseId);
                if (currentCourse ==null)
                {
                    result.HasError = true;
                    result.Errors.Add("No Valid Course found.");
                    return result;
                }

                var preRequisiteCourse = DbInstance.Aca_CurriculumCourse.FirstOrDefault(x => x.Id == preRequisiteCourseId);
                if (preRequisiteCourse == null)
                {
                    result.HasError = true;
                    result.Errors.Add("No Valid Pre Requisite Course found.");
                    return result;
                }

                if (currentCourse.CurriculumId != preRequisiteCourse.CurriculumId)
                {
                    result.HasError = true;
                    result.Errors.Add("Prerequisite's Course's Curriculum Do not match with Selected Course's Curriculum.");
                    return result;
                }

                var preRequisiteCourseMap = new Aca_CoursePrerequisiteMap()
                {
                    Id = 0,
                    CourseId = courseId,
                    PrerequisiteCourseId = preRequisiteCourseId
                };

                DbInstance.Aca_CoursePrerequisiteMap.Add(preRequisiteCourseMap);
                DbInstance.SaveChanges();

                result.Data = preRequisiteCourse.Id;

                result.DataExtra.PreRequisiteCourse = new PreRequisiteCourseJson
                {
                    Id = preRequisiteCourseMap.Id,
                    Name = preRequisiteCourse.Code + " : " + preRequisiteCourse.Name,
                    CreditHour = preRequisiteCourse.CreditHour,
                    CategoryTypeEnumId = preRequisiteCourse.CourseCategoryEnumId
                };
            }
            catch (Exception e)
            {
                result.HasError = true;
                result.Errors.Add(e.GetBaseException().Message);
                return result;
            }
            return result;
        }

        public HttpResult<Aca_CoursePrerequisiteMapJson> GetDeleteCoursePrerequisiteMapById(int id)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_CoursePrerequisiteMapJson>();
            //todo fix the permission
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.CurriculumCourse.CanDeletePreRequisite, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                var objToRemove = DbInstance.Aca_CoursePrerequisiteMap.FirstOrDefault(x => x.Id == id);
                if (objToRemove == null)
                {
                    result.HasError = true;
                    result.Errors.Add("Selected Course is not available.");
                    return result;
                }
                DbInstance.Aca_CoursePrerequisiteMap.Remove(objToRemove);
                DbInstance.SaveChanges();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }


        [HttpPost]
        public HttpResult<Aca_CurriculumCourseJson> SaveCurriculumCourse(Aca_CurriculumCourseJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_CurriculumCourseJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.CurriculumCourse.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.CurriculumCourse.CanEdit, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.CurriculumCourse.CanForceEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                var entityReceived = new Aca_CurriculumCourse();
                var dbAttachedEntity = new Aca_CurriculumCourse();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {
                    if (SaveCurriculumCourseLogic(entityReceived, ref dbAttachedEntity, out error))
                    {
                        DbInstance.SaveChanges();
                        scope.Commit();

                        dbAttachedEntity.Map(json);
                        result.Data = json; //return object
                    }
                    else
                    {
                        result.HasError = true;
                        result.Errors.Add(error);
                        scope.Rollback();
                    }
                }
            }

            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        /*[HttpPost]
        private HttpResult<Aca_CurriculumCourseJson> SaveCurriculumCourse2(Aca_CurriculumCourseJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_CurriculumCourseJson>();
            try
            {
                using (Aca_CurriculumCourseDataService curriculumCourseDataService = new Aca_CurriculumCourseDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new Aca_CurriculumCourse();
                    var dbAttachedEntity = new Aca_CurriculumCourse();
                    json.Map(entityReceived);
                    if (curriculumCourseDataService.Save(entityReceived, ref dbAttachedEntity, out error))
                    {
                        dbAttachedEntity.Map(json);
                        result.Data = json;//return object
                    }
                    else
                    {
                        result.HasError = true;
                        result.Errors.Add(error);
                    }
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }*/
        public HttpResult<Aca_CurriculumCourseJson> GetDeleteCurriculumCourseById(long id)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_CurriculumCourseJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.CurriculumCourse.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_CurriculumCourseDataService curriculumCourseDataService = new Aca_CurriculumCourseDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!curriculumCourseDataService.DeleteById(id, out error))
                    {
                        result.HasError = true;
                        result.Errors.Add(error);
                    }
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        #endregion
        #endregion

        #region Local Method (should be always private)
        private bool IsValidToSaveCurriculumCourse(Aca_CurriculumCourse newObj, out string error)
        {
            error = "";
            if (newObj.CourseId <= 0)
            {
                error += "Please select valid Course.";
                return false;
            }
            var course = DbInstance.Aca_Course.SingleOrDefault(x => x.Id == newObj.CourseId);
            if (course == null)
            {
                error += "Invalid Course.";
                return false;
            }
            newObj.CourseCategoryEnumId = course.CourseCategoryEnumId;
            newObj.CreditLoad = course.CreditLoad;
         
            
            if (!newObj.Name.IsValid())
            {
                error += "Name is not valid.";
                return false;
            }
            if (newObj.Name.Length > 256)
            {
                error += "Name exceeded its maximum length 256.";
                return false;
            }
            if (newObj.ShortName.IsValid() && newObj.ShortName.Length > 256)
            {
                error += "Short Name exceeded its maximum length 256.";
                return false;
            }
            if (!newObj.Code.IsValid())
            {
                error += "Code is not valid.";
                return false;
            }
            if (newObj.Code.Length > 128)
            {
                error += "Code exceeded its maximum length 128.";
                return false;
            }

            if (newObj.CurriculumId <= 0)
            {
                error += "Please select valid Curriculum.";
                return false;
            }
            var curriculum = DbInstance.Aca_Curriculum.SingleOrDefault(x => x.Id == newObj.CurriculumId);

            if (curriculum == null)
            {
                error += "Invalid Curriculum.";
                return false;
            }
           

            if (newObj.CourseType ==Aca_CurriculumCourse.CourseTypeEnum.Elective && (newObj.ElectiveGroupId == null || newObj.ElectiveGroupId <=0))
            {
                error += "Please select valid Elective Group.";
                return false;
            }

            //Ems 4 update
            //if (newObj.TutionFee < 0)
            //{
            //    error = "Please Enter a Valid Tution Fee.";
            //    return false;
            //}

            //if (newObj.RetakeTutionFee < 0)
            //{
            //    error = "Please Enter a Valid Retake Tution Fee.";
            //    return false;
            //}

            //if (newObj.ExamFee < 0)
            //{
            //    error = "Please Enter a Valid Exam Fee.";
            //    return false;
            //}

            //if (newObj.RetakeExamFee < 0)
            //{
            //    error = "Please Enter a Valid Retake Exam Fee.";
            //    return false;
            //}

            //if (newObj.OtherCharge < 0)
            //{
            //    error = "Please Enter a Valid Other Charge.";
            //    return false;
            //}

            //if (newObj.CreditLoad == null)
            //{
            //    error += "Credit Load required.";
            //    return false;
            //}
            //if (newObj.CreditHour == null)
            //{
            //    error += "Credit Hour required.";
            //    return false;
            //}
            //if (newObj.CourseCategoryEnumId == null)
            //{
            //    error += "Please select valid Course Category.";
            //    return false;
            //}
            //if (newObj.CreditTypeEnumId == null)
            //{
            //    error += "Please select valid Credit Type.";
            //    return false;
            //}
            //if (newObj.CourseTypeEnumId == null)
            //{
            //    error += "Please select valid Course Type.";
            //    return false;
            //}
            //if (newObj.IsDepartmental == null)
            //{
            //    error += "Is Departmental required.";
            //    return false;
            //}
            //if (newObj.StatusEnumId == null)
            //{
            //    error += "Please select valid Status.";
            //    return false;
            //}

            //if (newObj.IsDeleted == null)
            //{
            //    error += "Is Deleted required.";
            //    return false;
            //}
            //checking duplicate name & code.
            var res = DbInstance.Aca_CurriculumCourse.Any(x =>
             x.Id != newObj.Id
             && x.CurriculumId == newObj.CurriculumId
             && x.Name.Trim().Equals(newObj.Name.Trim(), StringComparison.InvariantCultureIgnoreCase)
             && x.Code.Trim().Equals(newObj.Code.Trim(), StringComparison.InvariantCultureIgnoreCase)
             );
            if (res)
            {
                error = "A Course with Same Code & Name is already exists in this Curriculum!";
                return false;
            }
            return true;
        }
        private bool SaveCurriculumCourseLogic(Aca_CurriculumCourse newObj, ref Aca_CurriculumCourse dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Curriculum Course to save can't be null!";
                return false;
            }

            if (!IsValidToSaveCurriculumCourse(newObj, out error))
                return false;

            bool isNewObject = true;
            Aca_CurriculumCourse objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Aca_CurriculumCourse.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            else
            {
                newObj.Id = BigInt.NewBigInt();
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Aca_CurriculumCourse.GetNew(newObj.Id);
                DbInstance.Aca_CurriculumCourse.Add(objToSave);
                objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
                objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            else
            {
                var res = DbInstance.Aca_Class.Any(x => x.CurriculumCourseId == newObj.Id);
                
                bool isPermit = PermissionUtil.HasPermission(
                    PermissionCollection.AcademicArea.CurriculumManager.CurriculumCourse.CanForceEdit);
                if (res && !isPermit)
                {
                    var msg = " Because this Course is already used in Curriculum Course.";
                    //if (objToSave.DepartmentId != newObj.DepartmentId)
                    //{
                    //    error = "Course Department can't be editable." + msg;
                    //    return false;
                    //}
                    if (objToSave.CourseCategoryEnumId != newObj.CourseCategoryEnumId)
                    {
                        error = "Course Category can't be editable." + msg;
                        return false;
                    }
                    if (!objToSave.CreditLoad.Equals(newObj.CreditLoad))
                    {
                        error = "Course Credit Hour can't be editable." + msg;
                        return false;
                    }
                    if (!objToSave.CreditHour.Equals(newObj.CreditHour))
                    {
                        error = "Course Contact Hour can't be editable." + msg;
                        return false;
                    }
                }
            }
            //checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumCourseManager.CurriculumCourse.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumCourseManager.CurriculumCourse.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
            objToSave.CourseId = newObj.CourseId;
            objToSave.Name = newObj.Name;
            objToSave.ShortName = newObj.ShortName;
            objToSave.Code = newObj.Code;
            objToSave.Description = newObj.Description;
            objToSave.CurriculumId = newObj.CurriculumId;
            objToSave.CreditLoad = newObj.CreditLoad;
            objToSave.CreditHour = newObj.CreditHour;
            objToSave.CourseCategoryEnumId = newObj.CourseCategoryEnumId;
            objToSave.CourseTypeEnumId = newObj.CourseTypeEnumId;
            objToSave.StatusEnumId = newObj.StatusEnumId;
            objToSave.ElectiveGroupId = newObj.ElectiveGroupId;
            objToSave.IsDeleted = newObj.IsDeleted;
            objToSave.LastChangedById = newObj.LastChangedById = HttpUtil.Profile.UserId;
            objToSave.LastChanged = newObj.LastChanged = DateTime.Now;

            //Ems 4 Update
         

            dbAddedObj = objToSave;

            //here save any child table
            return true;
        }
        #endregion
        #endregion
    }
}
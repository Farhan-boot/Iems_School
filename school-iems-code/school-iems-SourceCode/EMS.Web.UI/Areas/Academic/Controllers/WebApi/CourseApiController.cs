//File: UI Controller
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

using EMS.Framework;
using EMS.Framework.Attributes;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.CoreLibrary.Helpers;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases;
using EMS.Web.Framework.Bases.WebApi;
using EMS.Framework.Objects;
//using EMS.Web.Jsons.Mapper;
using EMS.Web.Jsons.Models;


namespace EMS.Web.UI.Areas.Academic.Controllers.WebApi
{

    public class CourseApiController : EmployeeBaseApiController
    {
        public CourseApiController()
        { }

        #region Course 
        #region List View Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Aca_CourseJson> GetPagedCourse(string textkey, int? pageSize, int? pageNo
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Aca_CourseJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.BaseCurriculumCourse.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.BaseCurriculumCourse.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.BaseCurriculumCourse.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_CourseDataService courseDataService = new Aca_CourseDataService(DbInstance, HttpUtil.Profile))
                {
                    IQueryable<Aca_Course> query = DbInstance.Aca_Course
                          //.Include("Aca_StudyLevelTerm")
                          .OrderBy(x => x.Code)
                          .ThenBy(x => x.Code);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Code.ToLower().Contains(textkey.ToLower()));
                    }
                   

                    var entities = courseDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Aca_CourseJson>();
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
        public HttpListResult<Aca_CourseJson> GetCourseListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_CourseJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.BaseCurriculumCourse.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.BaseCurriculumCourse.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.BaseCurriculumCourse.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                //Aca_CourseDataService courseDataService =
                //    new Aca_CourseDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List

                result.DataExtra.CourseCategoryEnumList = EnumUtil.GetEnumList(typeof(Aca_Course.CourseCategoryEnum));
                //DropDown Option Tables

                result.DataExtra.StudyLevelTermList = DbInstance.Aca_StudyLevelTerm.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.Name }).ToList();
                result.DataExtra.DepartmentList = DbInstance.HR_Department.AsEnumerable()
                     .Where(x => x.TypeEnumId == 2)
                     .Select(t => new
                     { Id = t.Id, Name = t.Name, ShortName = t.ShortName }).ToList();
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
        private HttpListResult<Aca_CourseJson> GetAllCourse()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_CourseJson>();
            try
            {
                using (Aca_CourseDataService courseDataService = new Aca_CourseDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<Aca_CourseJson>();
                    var entities = courseDataService.GetAll(out error);
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
        private HttpListResult<Aca_CourseJson> SaveCourseList(IList<Aca_CourseJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_CourseJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.BaseCurriculumCourse.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.BaseCurriculumCourse.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.BaseCurriculumCourse.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_CourseDataService courseDataService = new Aca_CourseDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<Aca_Course>();
                    var dbAttachedListEntity = new List<Aca_Course>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (courseDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<Aca_CourseJson> GetCourseById(long id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_CourseJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.BaseCurriculumCourse.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.BaseCurriculumCourse.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.BaseCurriculumCourse.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_CourseDataService courseDataService = new Aca_CourseDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Aca_CourseJson();
                    Aca_Course entity;
                    if (id <= 0)
                    {
                        entity = Aca_Course.GetNew();
                    }
                    else
                    {
                        entity = courseDataService.GetById(id);
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
        private HttpResult<Aca_CourseJson> GetCourseByIdWithChild(long id)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_CourseJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.BaseCurriculumCourse.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.BaseCurriculumCourse.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.BaseCurriculumCourse.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_CourseDataService courseDataService = new Aca_CourseDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Aca_CourseJson();
                    Aca_Course entity;
                    if (id <= 0)
                    {
                        entity = Aca_Course.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("Aca_Course");
                        //includeTables.Add("Aca_Course");

                        entity = courseDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Aca_CourseJson> GetCourseDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_CourseJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.BaseCurriculumCourse.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.BaseCurriculumCourse.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.BaseCurriculumCourse.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                //Aca_CourseDataService courseDataService =
                //    new Aca_CourseDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List

                result.DataExtra.CourseCategoryEnumList = EnumUtil.GetEnumList(typeof(Aca_Course.CourseCategoryEnum));
                //DropDown Option Tables

                result.DataExtra.StudyLevelTermList = DbInstance.Aca_StudyLevelTerm.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.Name }).ToList();
                result.DataExtra.DepartmentList = DbInstance.HR_Department.AsEnumerable()
                     .Where(x => x.TypeEnumId == 2)
                     .Select(t => new
                     { Id = t.Id, Name = t.Name, ShortName = t.ShortName }).ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        #endregion

        #region Save/Delete
        [HttpPost]
        public HttpResult<Aca_CourseJson> SaveCourse(Aca_CourseJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_CourseJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.BaseCurriculumCourse.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.BaseCurriculumCourse.CanEdit, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.BaseCurriculumCourse.CanForceEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                var entityReceived = new Aca_Course();
                var dbAttachedEntity = new Aca_Course();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {
                    if (SaveCourseLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<Aca_CourseJson> SaveCourse2(Aca_CourseJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_CourseJson>();
            try
            {
                using (Aca_CourseDataService courseDataService = new Aca_CourseDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new Aca_Course();
                    var dbAttachedEntity = new Aca_Course();
                    json.Map(entityReceived);
                    if (courseDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        public HttpResult<Aca_CourseJson> GetDeleteCourseById(long id)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_CourseJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.BaseCurriculumCourse.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_CourseDataService courseDataService = new Aca_CourseDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!courseDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveCourse(Aca_Course newObj, out string error)
        {
            error = "";
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

            if (newObj.CreditLoad == null)
            {
                error += "Credit Load required.";
                return false;
            }
            if (newObj.CreditHour == null)
            {
                error += "Credit Hour required.";
                return false;
            }
            if (newObj.CourseCategoryEnumId == null)
            {
                error += "Please select valid Course Category.";
                return false;
            }
          
            if (newObj.IsDeleted == null)
            {
                error += "Is Deleted required.";
                return false;
            }
            //checking is there any same course code and name exit in same dept.
            var res = DbInstance.Aca_Course.Any(x => 
            x.Id != newObj.Id
            && x.CreditLoad == newObj.CreditLoad
            //&& x. == newObj.DepartmentId
            && x.Name.Trim().Equals(newObj.Name.Trim(), StringComparison.InvariantCultureIgnoreCase)
            && x.Code.Trim().Equals(newObj.Code.Trim(), StringComparison.InvariantCultureIgnoreCase)
            );
            if (res)
            {
                error = "A Course with Same Code & Name is already exists in this Department!";
                return false;
            }

            return true;
        }
        private bool SaveCourseLogic(Aca_Course newObj, ref Aca_Course dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Course to save can't be null!";
                return false;
            }

            if (!IsValidToSaveCourse(newObj, out error))
                return false;

            bool isNewObject = true;
            Aca_Course objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Aca_Course.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            else
            {
                newObj.Id = BigInt.NewBigInt();
            }
            if (objToSave == null)
            {
                //new object
                isNewObject = true;
                objToSave = Aca_Course.GetNew(newObj.Id);
                DbInstance.Aca_Course.Add(objToSave);
                objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
                objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            else
            {
                var res = DbInstance.Aca_CurriculumCourse.Any(x => x.CourseId == newObj.Id);

                bool isPermit = PermissionUtil.HasPermission(
                    PermissionCollection.AcademicArea.CurriculumManager.BaseCurriculumCourse.CanForceEdit);
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
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CourseManager.Course.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CourseManager.Course.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
            objToSave.Code = newObj.Code.Trim();
            objToSave.Name = newObj.Name.Trim();
            objToSave.Description = newObj.Description;
            objToSave.CreditLoad = newObj.CreditLoad;
            objToSave.CreditHour = newObj.CreditHour;
            objToSave.CourseCategoryEnumId = newObj.CourseCategoryEnumId;
            objToSave.IsDeleted = newObj.IsDeleted;
            objToSave.LastChangedById = newObj.LastChangedById = HttpUtil.Profile.UserId;
            objToSave.LastChanged = newObj.LastChanged = DateTime.Now;
            dbAddedObj = objToSave;

            //here save any child table
            return true;
        }
        #endregion
        #endregion

    }
}

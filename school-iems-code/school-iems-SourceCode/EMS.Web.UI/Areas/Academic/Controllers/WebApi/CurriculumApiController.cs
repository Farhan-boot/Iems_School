using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
    public class CurriculumApiController : EmployeeBaseApiController
    {
        public CurriculumApiController()
        { }

        #region Curriculum 
        #region List View Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Aca_CurriculumJson> GetPagedCurriculum(string textkey, int? pageSize, int? pageNo
           , Int64 gradingPolicyId = 0
           , Int64 programId = 0
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Aca_CurriculumJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.Curriculum.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.Curriculum.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.Curriculum.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_CurriculumDataService curriculumDataService = new Aca_CurriculumDataService(DbInstance, HttpUtil.Profile))
                {
                    IQueryable<Aca_Curriculum> query = DbInstance.Aca_Curriculum
                                        .Include(x => x.Aca_Program)
                                        //.Include(x => x.Aca_Program.HR_Department)
                                        .OrderBy(x => x.CurriculumNo)
                                        .ThenBy(x => x.Aca_Program.ProgramTypeEnumId)
                                        .ThenByDescending(x => x.Aca_Program.Id)
                                        .ThenByDescending(x => x.Session);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Name.ToLower().Contains(textkey.ToLower()));
                    }
                    if (gradingPolicyId > 0)
                    {
                        query = query.Where(q => q.GradingPolicyId == gradingPolicyId);
                    }
                    if (programId > 0)
                    {
                        query = query.Where(q => q.ProgramId == programId);
                    }


                    var entities = curriculumDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Aca_CurriculumJson>();
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
        public HttpListResult<Aca_CurriculumJson> GetCurriculumListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_CurriculumJson>();
            try
            {
                //Aca_CurriculumDataService curriculumDataService =
                //    new Aca_CurriculumDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List

                //DropDown Option Tables

                //result.DataExtra.GradingPolicyList = DbInstance.Aca_GradingPolicy.AsEnumerable().Select(t => new
                //{ Id = t.Id.ToString(),Name = t.Name }).ToList();
                result.DataExtra.ProgramList = DbInstance.Aca_Program.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.ShortTitle }).ToList();
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
        private HttpListResult<Aca_CurriculumJson> GetAllCurriculum()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_CurriculumJson>();
            try
            {
                using (Aca_CurriculumDataService curriculumDataService = new Aca_CurriculumDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<Aca_CurriculumJson>();
                    var entities = curriculumDataService.GetAll(out error);
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
        private HttpListResult<Aca_CurriculumJson> SaveCurriculumList(IList<Aca_CurriculumJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_CurriculumJson>();
            try
            {
                using (Aca_CurriculumDataService curriculumDataService = new Aca_CurriculumDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<Aca_Curriculum>();
                    var dbAttachedListEntity = new List<Aca_Curriculum>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (curriculumDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<Aca_CurriculumJson> GetCurriculumById(long id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_CurriculumJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.Curriculum.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.Curriculum.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.Curriculum.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_CurriculumDataService curriculumDataService = new Aca_CurriculumDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Aca_CurriculumJson();
                    Aca_Curriculum entity;
                    if (id <= 0)
                    {
                        entity = Aca_Curriculum.GetNew();
                    }
                    else
                    {
                        entity = curriculumDataService.GetById(id);
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
        private HttpResult<Aca_CurriculumJson> GetCurriculumByIdWithChild(long id)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_CurriculumJson>();
            try
            {
                using (Aca_CurriculumDataService curriculumDataService = new Aca_CurriculumDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Aca_CurriculumJson();
                    Aca_Curriculum entity;
                    if (id <= 0)
                    {
                        entity = Aca_Curriculum.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("Aca_Curriculum");
                        //includeTables.Add("Aca_Curriculum");

                        entity = curriculumDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Aca_CurriculumJson> GetCurriculumDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_CurriculumJson>();
            try
            {
                //Aca_CurriculumDataService curriculumDataService =
                //    new Aca_CurriculumDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                //DropDown Option Tables
                result.DataExtra.DepartmentList = DbInstance.HR_Department.AsEnumerable()
                    .Where(x => x.TypeEnumId == 2)
                    .Select(t => new
                    { Id = t.Id, Name = t.Name, ShortName = t.ShortName }).ToList();

                result.DataExtra.ProgramList = DbInstance.Aca_Program.AsEnumerable().Select(t => new
                { Id = t.Id
                ,Name = t.Name
                ,ShortName = t.ShortName
                ,ShortTitle = t.ShortTitle

                }).ToList();
                result.DataExtra.GradingPolicyList = DbInstance.Aca_GradingPolicy.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.Name }).ToList();
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
        public HttpResult<Aca_CurriculumJson> SaveCurriculum(Aca_CurriculumJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_CurriculumJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.Curriculum.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.Curriculum.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                var entityReceived = new Aca_Curriculum();
                var dbAttachedEntity = new Aca_Curriculum();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {
                    if (SaveCurriculumLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<Aca_CurriculumJson> SaveCurriculum2(Aca_CurriculumJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_CurriculumJson>();
            try
            {
                using (Aca_CurriculumDataService curriculumDataService = new Aca_CurriculumDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new Aca_Curriculum();
                    var dbAttachedEntity = new Aca_Curriculum();
                    json.Map(entityReceived);
                    if (curriculumDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        public HttpResult<Aca_CurriculumJson> GetDeleteCurriculumById(long id)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_CurriculumJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.Curriculum.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_CurriculumDataService curriculumDataService = new Aca_CurriculumDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!curriculumDataService.DeleteById(id, out error))
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

        public HttpResult<Aca_CurriculumJson> GetDuplicateCurriculumWithCourseById(long id)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_CurriculumJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.Curriculum.CanDuplicateCurriculumWithCourses, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {

                if (id<=0)
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid Curriculum");
                    return result;
                }

                var curriculumToDuplicate = DbInstance.Aca_Curriculum
                    .Include(x => x.Aca_CurriculumCourse)
                    .SingleOrDefault(x => x.Id == id);

                if (curriculumToDuplicate==null)
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid Curriculum");
                    return result;
                }

                // Curriculum Duplicate
                var newCurriculum = Aca_Curriculum.GetNew(BigInt.NewBigInt());
                CopyUtil.CopySelectedColumns(curriculumToDuplicate,newCurriculum);
                newCurriculum.Name = $"Duplicated From {newCurriculum.Name}";
                newCurriculum.ShortName = $"Duplicated From {newCurriculum.ShortName}";
                newCurriculum.Description = $"Duplicated From {newCurriculum.Description}";
                newCurriculum.IsValid = false;
                newCurriculum.IsOffering = false;
                newCurriculum.CreateById =  HttpUtil.Profile.UserId;
                newCurriculum.CreateDate = DateTime.Now;
                newCurriculum.LastChangedById = HttpUtil.Profile.UserId;
                newCurriculum.LastChanged = DateTime.Now;

                // Curriculum Course List Duplicate

                var newCurriculumCourseList=new List<Aca_CurriculumCourse>();
                var curriculumCourseList = curriculumToDuplicate.Aca_CurriculumCourse;

                foreach (var curriculumCourseToDuplicate in curriculumCourseList)
                {
                    var newCurriculumCourse = Aca_CurriculumCourse.GetNew(BigInt.NewBigInt());
                    CopyUtil.CopySelectedColumns(curriculumCourseToDuplicate,newCurriculumCourse);

                    newCurriculumCourse.CurriculumId = newCurriculum.Id;
                    newCurriculumCourse.CreateById = HttpUtil.Profile.UserId;
                    newCurriculumCourse.CreateDate = DateTime.Now;
                    newCurriculumCourse.LastChangedById = HttpUtil.Profile.UserId;
                    newCurriculumCourse.LastChanged = DateTime.Now;

                    newCurriculumCourseList.Add(newCurriculumCourse);

                }


                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {

                    
                    DbInstance.Aca_Curriculum.Add(newCurriculum);
                    if (newCurriculumCourseList.Count>0)
                    {
                        DbInstance.Aca_CurriculumCourse.AddRange(newCurriculumCourseList);
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



        #endregion
        #endregion

        #region Local Method (should be always private)
        private bool IsValidToSaveCurriculum(Aca_Curriculum newObj, out string error)
        {
            error = "";
            //if (newObj.DepartmentId == null)
            //{
            //    error += "Department Id required.";
            //    return false;
            //}

            if (newObj.ProgramId <= 0)
            {
                error += "Please select valid Program.";
                return false;
            }
            if (newObj.Year<=0)
            {
                error += "Year is required.";
                return false;
            }
            if (newObj.Year.ToString().Length!=4)
            {
                error += "Year is not valid.";
                return false;
            }
            if (!newObj.Session.IsValid())
            {
                error += "Session is not valid.";
                return false;
            }

            //if (!newObj.Session.Contains(newObj.Year.ToString()))
            //{
            //    error += "Session must be contain Year. ex: 2015-2018";
            //    return false;
            //}
            //if (!newObj.Session.Contains("-"))
            //{
            //    error += "Session must be contain '-' between years. ex: 2015-2018";
            //    return false;
            //}
            var program = DbInstance.Aca_Program.SingleOrDefault(x => x.Id == newObj.ProgramId);
            if (program == null)
            {
                error += "Invalid Program.";
                return false;
            }
            //newObj.DepartmentId = program.DepartmentId;
            //var dept = DbInstance.HR_Department.SingleOrDefault(x => x.Id == newObj.DepartmentId);
            //if (dept == null)
            //{
            //    error += "Invalid Department.";
            //    return false;
            //}
            //newObj.Name = newObj.Year + " " + program.Name;
            //newObj.ShortName = newObj.Year + " " + program.ShortTitle;
            //newObj.TotalSemester = program.Semester;

            if (!DbInstance.Aca_Curriculum.Any(x => x.Id == newObj.Id))
            {
                var lastObj = DbInstance.Aca_Curriculum.Include(x=>x.Aca_Program)
                    .OrderByDescending(x=>x.Id)
                    .FirstOrDefault(
                        x => x.Year==newObj.Year
                        && x.Aca_Program.ProgramTypeEnumId == program.ProgramTypeEnumId
                    );
                if (lastObj == null)
                {
                    newObj.CurriculumNo = Convert.ToInt32(
                        newObj.Year+
                        program.ProgramTypeEnumId.ToString().PadLeft(1, '0') +
                        "01");
                }
                else
                {
                    newObj.CurriculumNo = lastObj.CurriculumNo + 1;
                }
            }
            if (!newObj.Name.IsValid())
            {
                error += "Name is not valid.";
                return false;
            }
            if (newObj.Name.Length > 128)
            {
                error += "Name exceeded its maximum length 128.";
                return false;
            }
            if (!newObj.Name.Contains(newObj.Year.ToString()))
            {
                error += "Name must be contain the Year";
                return false;
            }

            if (!newObj.ShortName.IsValid())
            {
                error += "Short Name is not valid.";
                return false;
            }
            if (newObj.ShortName.Length > 128)
            {
                error += "Short Name exceeded its maximum length 128.";
                return false;
            }
            if (!newObj.ShortName.Contains(newObj.Year.ToString()))
            {
                error += "Year must be contain in Short Name";
                return false;
            }
            if (newObj.Year <=1997)
            {
                error += "Year should be grater then 1997.";
                return false;
            }
            if (newObj.CurriculumNo == null)
            {
                error += "Curriculum No required.";
                return false;
            }
            //if (newObj.TotalSemester == null)
            //{
            //    error += "Total Semester required.";
            //    return false;
            //}
            if (!newObj.Description.IsValid())
            {
                error += "Description is not valid.";
                return false;
            }
            if (newObj.Description == null)
            {
                error += "Description required.";
                return false;
            }
            if (newObj.IsValid == null)
            {
                error += "Is Valid required.";
                return false;
            }
            if (newObj.IsOffering == null)
            {
                error += "Is Offering required.";
                return false;
            }
            //if (newObj.TotalCredit == null)
            //{
            //    error += "Total Credit required.";
            //    return false;
            //}
            if (newObj.TotalCourse == null)
            {
                error += "Total Course required.";
                return false;
            }
            if (newObj.GradingPolicyId <= 0)
            {
                error += "Please select valid Grading Policy.";
                return false;
            }
            //if (newObj.CreditRequireForGraduation == null)
            //{
            //    error += "Credit Require For Graduation required.";
            //    return false;
            //}
            if (newObj.CourseRequireForGraduation == null)
            {
                error += "Course Require For Graduation required.";
                return false;
            }
            if (newObj.IsDeleted == null)
            {
                error += "Is Deleted required.";
                return false;
            }
            //TODO write your custom validation here.
            var res = DbInstance.Aca_Curriculum.Any(x => x.Id != newObj.Id
            && x.Name.Equals(newObj.Name,StringComparison.InvariantCultureIgnoreCase));
            if (res)
            {
                error = "A same Named Curriculum is already exists!";
                return false;
            }
            return true;
        }
        private bool SaveCurriculumLogic(Aca_Curriculum newObj, ref Aca_Curriculum dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Curriculum to save can't be null!";
                return false;
            }

            if (!IsValidToSaveCurriculum(newObj, out error))
                return false;

            bool isNewObject = true;
            Aca_Curriculum objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Aca_Curriculum.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            else
            {
                newObj.Id = BigInt.NewBigInt();
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Aca_Curriculum.GetNew(newObj.Id);
                DbInstance.Aca_Curriculum.Add(objToSave);
                objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
                objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.Curriculum.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.Curriculum.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
            objToSave.Name = newObj.Name;
            objToSave.ShortName = newObj.ShortName;
            objToSave.Year = newObj.Year;
            objToSave.Session = newObj.Session;
            objToSave.CurriculumNo = newObj.CurriculumNo;
            objToSave.ProgramId = newObj.ProgramId;
            objToSave.Description = newObj.Description;
            objToSave.IsValid = newObj.IsValid;
            objToSave.IsOffering = newObj.IsOffering;
            objToSave.TotalCourse = newObj.TotalCourse;
            objToSave.GradingPolicyId = newObj.GradingPolicyId;
            objToSave.CourseRequireForGraduation = newObj.CourseRequireForGraduation;
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
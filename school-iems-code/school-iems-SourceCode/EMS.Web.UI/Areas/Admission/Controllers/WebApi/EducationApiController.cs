using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases.WebApi;
using EMS.Web.Jsons.Models;

namespace EMS.Web.UI.Areas.Admission.Controllers.WebApi
{
    public class EducationApiController:EmployeeBaseApiController
    {
        public EducationApiController()
        {

        }
        #region Education 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<User_EducationJson> GetPagedEducation(string textkey, int? pageSize, int? pageNo
           , Int32 degreeCategoryId = 0
           , Int32 boardId = 0
           , Int64 userId = 0
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<User_EducationJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Education.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Education.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Education.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            try
            {
                using (User_EducationDataService educationDataService = new User_EducationDataService(DbInstance, HttpUtil.Profile))
                {
                    IQueryable<User_Education> query = DbInstance.User_Education.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.DegreeTitle.ToLower().Contains(textkey.ToLower()));
                    }
                    if (degreeCategoryId > 0)
                    {
                        query = query.Where(q => q.DegreeCategoryId == degreeCategoryId);
                    }
                    if (boardId > 0)
                    {
                        query = query.Where(q => q.BoardId == boardId);
                    }
                    if (userId > 0)
                    {
                        query = query.Where(q => q.UserId == userId);
                    }

                    var entities = educationDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<User_EducationJson>();
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
        /// <summary>
        /// Caution: Very Dangerous to call When table have huge data!
        /// </summary>
        /// <returns></returns>
        private HttpListResult<User_EducationJson> GetAllEducation()
        {
            string error = string.Empty;
            var result = new HttpListResult<User_EducationJson>();
            try
            {
                using (User_EducationDataService educationDataService = new User_EducationDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<User_EducationJson>();
                    var entities = educationDataService.GetAll(out error);
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
        public HttpResult<User_EducationJson> GetEducationById(int id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<User_EducationJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Education.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Education.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Education.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (User_EducationDataService educationDataService = new User_EducationDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new User_EducationJson();
                    User_Education entity;
                    if (id <= 0)
                    {
                        entity = User_Education.GetNew();
                    }
                    else
                    {
                        entity = educationDataService.GetById(id);
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
        private HttpResult<User_EducationJson> GetEducationByIdWithChild(int id)
        {
            string error = string.Empty;
            var result = new HttpResult<User_EducationJson>();
            try
            {
                using (User_EducationDataService educationDataService = new User_EducationDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new User_EducationJson();
                    User_Education entity;
                    if (id <= 0)
                    {
                        entity = User_Education.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("User_Education");
                        //includeTables.Add("User_Education");

                        entity = educationDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<User_EducationJson> GetEducationListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<User_EducationJson>();
            try
            {
                //User_EducationDataService educationDataService =
                //    new User_EducationDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List

                result.DataExtra.DegreeEquivalentEnumList = EnumUtil.GetEnumList(typeof(User_Education.DegreeEquivalentEnum));
                //DropDown Option Tables

                //result.DataExtra.DegreeCategoryList = DbInstance.Adm_DegreeCategory.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
                //result.DataExtra.EducationBoardList = DbInstance.Adm_EducationBoard.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
                //result.DataExtra.AccountList = DbInstance.User_Account.AsEnumerable().Select(t => new
                //{ Id = t.Id.ToString(),Name = t.Name }).ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<User_EducationJson> GetEducationDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<User_EducationJson>();
            try
            {
                //User_EducationDataService educationDataService =
                //    new User_EducationDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List

                result.DataExtra.DegreeEquivalentEnumList = EnumUtil.GetEnumList(typeof(User_Education.DegreeEquivalentEnum));
                result.DataExtra.DegreeGroupMajorEnumList = EnumUtil.GetEnumList(typeof(User_Education.DegreeGroupMajorEnum));
                result.DataExtra.ResultSystemEnumList = EnumUtil.GetEnumList(typeof(User_Education.ResultSystemEnum));

                //DropDown Option Tables

                result.DataExtra.DegreeCategoryList = DbInstance.Adm_DegreeCategory.AsEnumerable()
                    .Select(t => new
                    {
                        Id = t.Id,
                        Name = t.Name,
                        DegreeEquivalentEnumId =t.DegreeEquivalentEnumId,
                        BoardTypeEnumId = t.BoardTypeEnumId
                    })
                    .ToList();

                result.DataExtra.EducationBoardList = DbInstance.Adm_EducationBoard.AsEnumerable()
                    .Select(t => new
                    {
                        Id = t.Id,
                        Name = t.Name,
                        BoardTypeEnumId = t.BoardTypeEnumId,
                    })
                    .ToList();


                //result.DataExtra.AccountList = DbInstance.User_Account.AsEnumerable().Select(t => new
                //{ Id = t.Id.ToString(),Name = t.Name }).ToList();
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

        //public HttpListResult<User_EducationJson> GetEducationHistoryByStudentId(long id=0)
        //{
        //    string error = string.Empty;
        //    var result = new HttpListResult<User_EducationJson>();
        //    try
        //    {
        //        using (User_EducationDataService educationDataService = new User_EducationDataService(DbInstance, HttpUtil.Profile))
        //        {
        //            var jsons = new List<User_EducationJson>();

        //            var entities = DbInstance.User_Education.
        //                Where(x => x.UserId == id).ToList();
        //            if (entities.Count!=0)
        //            {
        //                entities.Map(jsons);
        //            }
        //            else
        //            {
        //                var educationHistory = new User_EducationJson();
        //                educationHistory.UserId = "201701241753321453";
        //                educationHistory.DegreeEquivalentEnumId = (byte)User_Education.DegreeEquivalentEnum.Ssc10YearEquivalent;
        //                jsons.Add(educationHistory);

        //                educationHistory = new User_EducationJson();
        //                educationHistory.UserId = "201701241753321453";
        //                educationHistory.DegreeEquivalentEnumId = (byte)User_Education.DegreeEquivalentEnum.Hsc12YearEquivalent;
        //                jsons.Add(educationHistory);

        //                educationHistory = new User_EducationJson();
        //                educationHistory.UserId = "201701241753321453";
        //                educationHistory.DegreeEquivalentEnumId = (byte)User_Education.DegreeEquivalentEnum.BachelorEquivalent;
        //                jsons.Add(educationHistory);

        //                educationHistory = new User_EducationJson();
        //                educationHistory.UserId = "201701241753321453";
        //                educationHistory.DegreeEquivalentEnumId = (byte)User_Education.DegreeEquivalentEnum.MasterEquivalent;
        //                jsons.Add(educationHistory);
        //            }
                    
        //            result.Data = jsons;
        //            result.Count = jsons.Count;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.HasError = true;
        //        result.Errors.Add(ex.GetBaseException().Message.ToString());
        //    }
        //    return result;
        //}

        [HttpPost]
        public HttpListResult<User_EducationJson> SaveEducationListByStudentId(IList<User_EducationJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<User_EducationJson>();
            if ( !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Education.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Education.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (User_EducationDataService educationDataService = new User_EducationDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<User_Education>();
                    var dbAttachedListEntity = new List<User_Education>();
                    jsonList.Map(entityListReceived);
                    foreach (var userEducationJson in jsonList)
                    {
                        SaveEducation(userEducationJson);
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


        #region Save/Delete
        [HttpPost]
        private HttpResult<User_EducationJson> SaveEducation(User_EducationJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<User_EducationJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Education.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Education.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                var entityReceived = new User_Education();
                var dbAttachedEntity = new User_Education();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {
                    if (SaveEducationLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<User_EducationJson> SaveEducation2(User_EducationJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<User_EducationJson>();
            try
            {
                using (User_EducationDataService educationDataService = new User_EducationDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new User_Education();
                    var dbAttachedEntity = new User_Education();
                    json.Map(entityReceived);
                    if (educationDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        /// <summary>
        /// Todo pending to implement bulk save
        /// </summary>
        /// <param name="jsonList"></param>
        /// <returns></returns>
        [HttpPost]
        private HttpListResult<User_EducationJson> SaveEducationList(IList<User_EducationJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<User_EducationJson>();
            try
            {
                using (User_EducationDataService educationDataService = new User_EducationDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<User_Education>();
                    var dbAttachedListEntity = new List<User_Education>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{
                        
                    //}

                    //if (educationDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        private HttpResult<User_EducationJson> GetDeleteEducationById(int id)
        {
            string error = string.Empty;
            var result = new HttpResult<User_EducationJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Education.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (User_EducationDataService educationDataService = new User_EducationDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!educationDataService.DeleteById(id, out error))
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

        #region Local Method (should be always private)
        private bool IsValidToSaveEducation(User_Education newObj, out string error)
        {
            error = "";
            if (newObj.UserId <= 0)
            {
                error += "Please select valid User.";
                return false;
            }
            if (newObj.DegreeEquivalentEnumId == null)
            {
                error += "Please select valid Degree Equivalent.";
                return false;
            }
            if (newObj.DegreeCategoryId <= 0)
            {
                error += "Please select valid Degree Category.";
                return false;
            }
            if (!newObj.DegreeTitle.IsValid())
            {
                error += "Degree Title is not valid.";
                return false;
            }
            if (newObj.DegreeTitle.Length > 250)
            {
                error += "Degree Title exceeded its maximum length 250.";
                return false;
            }
            if (newObj.DegreeGroupMajorEnumId == null)
            {
                error += "Please select valid Degree Group Major.";
                return false;
            }
            if (newObj.DegreeGroupMajorOther.IsValid() && newObj.DegreeGroupMajorOther.Length > 250)
            {
                error += "Degree Group Major Other exceeded its maximum length 250.";
                return false;
            }
            if (!newObj.InstitutionName.IsValid())
            {
                error += "Institution Name is not valid.";
                return false;
            }
            if (newObj.InstitutionName.Length > 250)
            {
                error += "Institution Name exceeded its maximum length 250.";
                return false;
            }

            if (newObj.CourseDuration.IsValid() && newObj.CourseDuration.Length > 50)
            {
                error += "Course Duration exceeded its maximum length 50.";
                return false;
            }
            if (newObj.YearOfPassing == null)
            {
                error += "Year Of Passing required.";
                return false;
            }
            if (newObj.RegistrationNo.IsValid() && newObj.RegistrationNo.Length > 50)
            {
                error += "Registration No exceeded its maximum length 50.";
                return false;
            }
            if (newObj.StudentRollOrIdNo.IsValid() && newObj.StudentRollOrIdNo.Length > 50)
            {
                error += "Student Roll Or Id No exceeded its maximum length 50.";
                return false;
            }
            if (newObj.ResultSystemEnumId == null)
            {
                error += "Please select valid Result System.";
                return false;
            }
            //if (!newObj.ObtainedGradeOrClass.IsValid())
            //{
            //    error += "Grade Or Class is not valid.";
            //    return false;
            //}
            //if (newObj.ObtainedGpaOrMarks.Length > 50)
            //{
            //    error += "Grade Or Class exceeded its maximum length 50.";
            //    return false;
            //}




            //TODO write your custom validation here.
            //var res =  DbInstance.User_Education.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
            //error = "A Education already exists!";
            //return false;
            //}
            return true;
        }
        private bool SaveEducationLogic(User_Education newObj, ref User_Education dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Education to save can't be null!";
                return false;
            }

            if (!IsValidToSaveEducation(newObj, out error))
                return false;

            bool isNewObject = true;
            User_Education objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.User_Education.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            //else
            //{
            //    newObj.Id = BigInt.NewBigInt();
            //}
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = User_Education.GetNew(newObj.Id);
                DbInstance.User_Education.Add(objToSave);
                objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
                objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Education.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Education.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
            objToSave.UserId = newObj.UserId;
            objToSave.DegreeEquivalentEnumId = newObj.DegreeEquivalentEnumId;
            objToSave.DegreeCategoryId = newObj.DegreeCategoryId;
            objToSave.DegreeTitle = newObj.DegreeTitle;
            objToSave.DegreeGroupMajorEnumId = newObj.DegreeGroupMajorEnumId;
            objToSave.DegreeGroupMajorOther = newObj.DegreeGroupMajorOther;
            objToSave.InstitutionName = newObj.InstitutionName;
            objToSave.BoardId = newObj.BoardId;
            objToSave.CourseDuration = newObj.CourseDuration;
            objToSave.YearOfPassing = newObj.YearOfPassing;
            objToSave.RegistrationNo = newObj.RegistrationNo;
            objToSave.StudentRollOrIdNo = newObj.StudentRollOrIdNo;
            objToSave.ResultSystemEnumId = newObj.ResultSystemEnumId;
            objToSave.ObtainedGradeOrClass = newObj.ObtainedGradeOrClass;
            objToSave.ObtainedGpaOrMarks = newObj.ObtainedGpaOrMarks;
            objToSave.GpaNo4Sub = newObj.GpaNo4Sub;
            objToSave.FullMarksOrScale = newObj.FullMarksOrScale;
            objToSave.IsGolden = newObj.IsGolden;
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
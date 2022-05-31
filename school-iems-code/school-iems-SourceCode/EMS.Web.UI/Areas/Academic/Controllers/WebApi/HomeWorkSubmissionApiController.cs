//File: UI Controller
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

using EMS.Framework;
using EMS.Framework.Attributes;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.CoreLibrary.Helpers;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases;
using EMS.Web.Framework.Bases.WebApi;
using EMS.Web.Jsons.Models;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;


namespace EMS.Web.UI.Areas.Academic.Controllers.WebApi
{

    public class HomeWorkSubmissionApiController : EmployeeBaseApiController
	{
        public HomeWorkSubmissionApiController()
        {  }
       
        #region HomeWorkSubmission 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Aca_HomeWorkSubmissionJson> GetPagedHomeWorkSubmission(string textkey, int? pageSize, int? pageNo
           ,Int32 homeworkId= 0      
           ,Int32 studentId= 0      
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Aca_HomeWorkSubmissionJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.HomeWorkSubmission.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.HomeWorkSubmission.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.HomeWorkSubmission.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/

            try
            {
                using (Aca_HomeWorkSubmissionDataService homeWorkSubmissionDataService = new Aca_HomeWorkSubmissionDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<Aca_HomeWorkSubmission> query = DbInstance.Aca_HomeWorkSubmission.OrderByDescending(x => x.Id);

                  if (homeworkId>0)
                    {
                        query = query.Where(q => q.HomeworkId== homeworkId);
                    }  
                    if (studentId>0)
                    {
                        query = query.Where(q => q.StudentId== studentId);
                    }  
                 
                    var entities = homeWorkSubmissionDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Aca_HomeWorkSubmissionJson>();
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
        private HttpListResult<Aca_HomeWorkSubmissionJson> GetAllHomeWorkSubmission()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_HomeWorkSubmissionJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.HomeWorkSubmission.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.HomeWorkSubmission.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.HomeWorkSubmission.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Aca_HomeWorkSubmissionDataService homeWorkSubmissionDataService = new Aca_HomeWorkSubmissionDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<Aca_HomeWorkSubmissionJson>();
                    var entities = homeWorkSubmissionDataService.GetAll(out error);
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
        public HttpResult<Aca_HomeWorkSubmissionJson> GetHomeWorkSubmissionById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_HomeWorkSubmissionJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.HomeWorkSubmission.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.HomeWorkSubmission.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.HomeWorkSubmission.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Aca_HomeWorkSubmissionDataService homeWorkSubmissionDataService = new Aca_HomeWorkSubmissionDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Aca_HomeWorkSubmissionJson();
                    Aca_HomeWorkSubmission entity;
                    if (id <= 0)
                    {
                        entity = Aca_HomeWorkSubmission.GetNew();
                    }
                    else
                    {
                        entity = homeWorkSubmissionDataService.GetById(id);
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
        private HttpResult<Aca_HomeWorkSubmissionJson> GetHomeWorkSubmissionByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_HomeWorkSubmissionJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.HomeWorkSubmission.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.HomeWorkSubmission.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.HomeWorkSubmission.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Aca_HomeWorkSubmissionDataService homeWorkSubmissionDataService = new Aca_HomeWorkSubmissionDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Aca_HomeWorkSubmissionJson();
                    Aca_HomeWorkSubmission entity;
                    if (id <= 0)
                    {
                        entity = Aca_HomeWorkSubmission.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("Aca_HomeWorkSubmission");
                        //includeTables.Add("Aca_HomeWorkSubmission");

                        entity = homeWorkSubmissionDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Aca_HomeWorkSubmissionJson> GetHomeWorkSubmissionListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_HomeWorkSubmissionJson>();
            try
            {
                //Aca_HomeWorkSubmissionDataService homeWorkSubmissionDataService =
                //    new Aca_HomeWorkSubmissionDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.SubmissionStatusEnumList = EnumUtil.GetEnumList(typeof(Aca_HomeWorkSubmission.SubmissionStatusEnum));
                //DropDown Option Tables
                 
                //result.DataExtra.HomeWorkList = DbInstance.Aca_HomeWork.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
                //result.DataExtra.StudentList = DbInstance.User_Student.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<Aca_HomeWorkSubmissionJson> GetHomeWorkSubmissionDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_HomeWorkSubmissionJson>();
            try
            {
                //Aca_HomeWorkSubmissionDataService homeWorkSubmissionDataService =
                //    new Aca_HomeWorkSubmissionDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.SubmissionStatusEnumList = EnumUtil.GetEnumList(typeof(Aca_HomeWorkSubmission.SubmissionStatusEnum));
                //DropDown Option Tables
                 
                //result.DataExtra.HomeWorkList = DbInstance.Aca_HomeWork.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
                //result.DataExtra.StudentList = DbInstance.User_Student.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
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
        public HttpResult<Aca_HomeWorkSubmissionJson> SaveHomeWorkSubmission(Aca_HomeWorkSubmissionJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_HomeWorkSubmissionJson>();
            //optional permission, check permission in the SaveHomeWorkSubmissionLogic insted
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.HomeWorkSubmission.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.HomeWorkSubmission.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                 var entityReceived = new Aca_HomeWorkSubmission();
                 var dbAttachedEntity = new Aca_HomeWorkSubmission();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SaveHomeWorkSubmissionLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<Aca_HomeWorkSubmissionJson> SaveHomeWorkSubmission2(Aca_HomeWorkSubmissionJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_HomeWorkSubmissionJson>();
            try
            {
                using (Aca_HomeWorkSubmissionDataService homeWorkSubmissionDataService = new Aca_HomeWorkSubmissionDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new Aca_HomeWorkSubmission();
                    var dbAttachedEntity = new Aca_HomeWorkSubmission();
                    json.Map(entityReceived);
                    if (homeWorkSubmissionDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<Aca_HomeWorkSubmissionJson> SaveHomeWorkSubmissionList(IList<Aca_HomeWorkSubmissionJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_HomeWorkSubmissionJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.HomeWorkSubmission.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.HomeWorkSubmission.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Aca_HomeWorkSubmissionDataService homeWorkSubmissionDataService = new Aca_HomeWorkSubmissionDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<Aca_HomeWorkSubmission>();
                    var dbAttachedListEntity = new List<Aca_HomeWorkSubmission>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (homeWorkSubmissionDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<Aca_HomeWorkSubmissionJson> GetDeleteHomeWorkSubmissionById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_HomeWorkSubmissionJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.HomeWorkSubmission.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Aca_HomeWorkSubmissionDataService homeWorkSubmissionDataService = new Aca_HomeWorkSubmissionDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!homeWorkSubmissionDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveHomeWorkSubmission(Aca_HomeWorkSubmission newObj, out string error)
        {
            error = "";
            if (newObj.HomeworkId<=0)
            {
                error += "Please select valid Homework.";
                return false;
            }
            if (newObj.StudentId<=0)
            {
                error += "Please select valid Student.";
                return false;
            }
            if (!newObj.SubmissionDate.IsValid())
            {
                error += "Submission Date is not valid.";
                return false;
            }
            if (newObj.SubmissionStatusEnumId==null)
            {
                error += "Please select valid Submission Status.";
                return false;
            }
            if (newObj.Checked==null)
            {
                error += "Checked required.";
                return false;
            }
            if (newObj.TeacherId==null)
            {
                error += "Teacher Id required.";
                return false;
            }



            if (newObj.IsDeleted==null)
            {
                error += "Is Deleted required.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.Aca_HomeWorkSubmission.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A HomeWorkSubmission already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveHomeWorkSubmissionLogic(Aca_HomeWorkSubmission newObj,ref Aca_HomeWorkSubmission dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Home Work Submission to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveHomeWorkSubmission(newObj, out error))
                return false;

            bool isNewObject = true;
            Aca_HomeWorkSubmission objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Aca_HomeWorkSubmission.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Aca_HomeWorkSubmission.GetNew(newObj.Id);
                DbInstance.Aca_HomeWorkSubmission.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.HomeWorkSubmission.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.HomeWorkSubmission.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
           objToSave.HomeworkId =  newObj.HomeworkId ;
           objToSave.StudentId =  newObj.StudentId ;
           objToSave.SubmissionDate =  newObj.SubmissionDate ;
           objToSave.SubmissionStatusEnumId =  newObj.SubmissionStatusEnumId ;
           objToSave.Checked =  newObj.Checked ;
           objToSave.TeacherId =  newObj.TeacherId ;
           objToSave.Feedback =  newObj.Feedback ;
           objToSave.Remark =  newObj.Remark ;
           objToSave.History =  newObj.History ;
           objToSave.IsDeleted =  newObj.IsDeleted ;
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

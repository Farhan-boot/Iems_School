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
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases;
using EMS.Web.Framework.Bases.WebApi;
using EMS.Web.Jsons.Models;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;


namespace EMS.Web.UI.Areas.Admin.Controllers.WebApi
{

    public class StudyTermApiController : EmployeeBaseApiController
	{
        public StudyTermApiController()
        {  }
       
        #region StudyTerm 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Aca_StudyTermJson> GetPagedStudyTerm(string textkey, int? pageSize, int? pageNo
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Aca_StudyTermJson>();
     
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.SemesterManager.StudyLevelTerm.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.SemesterManager.StudyLevelTerm.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.SemesterManager.StudyLevelTerm.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }

            try
            {
                using (Aca_StudyTermDataService studyTermDataService = new Aca_StudyTermDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<Aca_StudyTerm> query = DbInstance.Aca_StudyTerm.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Name.ToLower().Contains(textkey.ToLower()));
                    }
                 
                    var entities = studyTermDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Aca_StudyTermJson>();
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
        private HttpListResult<Aca_StudyTermJson> GetAllStudyTerm()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_StudyTermJson>();
          
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.SemesterManager.StudyLevelTerm.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.SemesterManager.StudyLevelTerm.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.SemesterManager.StudyLevelTerm.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_StudyTermDataService studyTermDataService = new Aca_StudyTermDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<Aca_StudyTermJson>();
                    var entities = studyTermDataService.GetAll(out error);
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
        public HttpResult<Aca_StudyTermJson> GetStudyTermById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_StudyTermJson>();
   
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.SemesterManager.StudyLevelTerm.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.SemesterManager.StudyLevelTerm.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.SemesterManager.StudyLevelTerm.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_StudyTermDataService studyTermDataService = new Aca_StudyTermDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Aca_StudyTermJson();
                    Aca_StudyTerm entity;
                    if (id <= 0)
                    {
                        entity = Aca_StudyTerm.GetNew();
                    }
                    else
                    {
                        entity = studyTermDataService.GetById(id);
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
        private HttpResult<Aca_StudyTermJson> GetStudyTermByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_StudyTermJson>();
         
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.SemesterManager.StudyLevelTerm.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.SemesterManager.StudyLevelTerm.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.SemesterManager.StudyLevelTerm.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_StudyTermDataService studyTermDataService = new Aca_StudyTermDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Aca_StudyTermJson();
                    Aca_StudyTerm entity;
                    if (id <= 0)
                    {
                        entity = Aca_StudyTerm.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("Aca_StudyTerm");
                        //includeTables.Add("Aca_StudyTerm");

                        entity = studyTermDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Aca_StudyTermJson> GetStudyTermListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_StudyTermJson>();
            try
            {
                //Aca_StudyTermDataService studyTermDataService =
                //    new Aca_StudyTermDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                //DropDown Option Tables
                 
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<Aca_StudyTermJson> GetStudyTermDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_StudyTermJson>();
            try
            {
                //Aca_StudyTermDataService studyTermDataService =
                //    new Aca_StudyTermDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                result.DataExtra.SemesterDurationList = Aca_Semester.SemesterDurationEnumDropDownList;
                //DropDown Option Tables

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
        public HttpResult<Aca_StudyTermJson> SaveStudyTerm(Aca_StudyTermJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_StudyTermJson>();
        
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.SemesterManager.StudyLevelTerm.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.SemesterManager.StudyLevelTerm.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                 var entityReceived = new Aca_StudyTerm();
                 var dbAttachedEntity = new Aca_StudyTerm();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {
                    if (SaveStudyTermLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<Aca_StudyTermJson> SaveStudyTerm2(Aca_StudyTermJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_StudyTermJson>();
            try
            {
                using (Aca_StudyTermDataService studyTermDataService = new Aca_StudyTermDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new Aca_StudyTerm();
                    var dbAttachedEntity = new Aca_StudyTerm();
                    json.Map(entityReceived);
                    if (studyTermDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<Aca_StudyTermJson> SaveStudyTermList(IList<Aca_StudyTermJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_StudyTermJson>();
    
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.SemesterManager.StudyLevelTerm.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.SemesterManager.StudyLevelTerm.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_StudyTermDataService studyTermDataService = new Aca_StudyTermDataService(DbInstance, HttpUtil.Profile))
                {
                    //var entityListReceived = new List<Aca_StudyTerm>();
                    //var dbAttachedListEntity = new List<Aca_StudyTerm>();
                    //jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (studyTermDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<Aca_StudyTermJson> GetDeleteStudyTermById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_StudyTermJson>();
 
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.SemesterManager.StudyLevelTerm.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_StudyTermDataService studyTermDataService = new Aca_StudyTermDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!studyTermDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveStudyTerm(Aca_StudyTerm newObj, out string error)
        {
            error = "";
            if (!newObj.Name.IsValid())
            {
                error += "Name is not valid.";
                return false;
            }
            if (newObj.Name.Length>50)
            {
                error += "Name exceeded its maximum length 50.";
                return false;
            }
            if (newObj.TermNo==null)
            {
                error += "Term No required.";
                return false;
            }
            if (newObj.TermNo < 1)
            {
                error += "Term No should be grater than 0.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.Aca_StudyTerm.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
            //error = "A StudyTerm already exists!";
            //return false;
            //}
            return true;
        }
        private bool SaveStudyTermLogic(Aca_StudyTerm newObj,ref Aca_StudyTerm dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Study Term to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveStudyTerm(newObj, out error))
                return false;

            bool isNewObject = true;
            Aca_StudyTerm objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Aca_StudyTerm.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Aca_StudyTerm.GetNew(newObj.Id);
                DbInstance.Aca_StudyTerm.Add(objToSave);
               
               
            }
         
            if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.SemesterManager.StudyLevelTerm.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.SemesterManager.StudyLevelTerm.CanEdit,
                   out error))
            {
                return false;
            }

            //binding object  
           objToSave.Name =  newObj.Name ;
           objToSave.TermNo =  newObj.TermNo ;
           objToSave.SemesterName =  newObj.SemesterName;
           objToSave.IsEnabled =  newObj.IsEnabled;
           objToSave.SemesterDurationEnumId = newObj.SemesterDurationEnumId;
           
           
           dbAddedObj = objToSave;
           
            //here save any child table
            return true;
        }
        #endregion
        #endregion

	}
}

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

    public class StudyLevelApiController : EmployeeBaseApiController
	{
        public StudyLevelApiController()
        {  }
       
        #region StudyLevel 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Aca_StudyLevelJson> GetPagedStudyLevel(string textkey, int? pageSize, int? pageNo
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Aca_StudyLevelJson>();
        
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
                using (Aca_StudyLevelDataService studyLevelDataService = new Aca_StudyLevelDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<Aca_StudyLevel> query = DbInstance.Aca_StudyLevel.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Name.ToLower().Contains(textkey.ToLower()));
                    }
                 
                    var entities = studyLevelDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Aca_StudyLevelJson>();
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
        private HttpListResult<Aca_StudyLevelJson> GetAllStudyLevel()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_StudyLevelJson>();
      
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
                using (Aca_StudyLevelDataService studyLevelDataService = new Aca_StudyLevelDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<Aca_StudyLevelJson>();
                    var entities = studyLevelDataService.GetAll(out error);
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
        public HttpResult<Aca_StudyLevelJson> GetStudyLevelById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_StudyLevelJson>();
       
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
                using (Aca_StudyLevelDataService studyLevelDataService = new Aca_StudyLevelDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Aca_StudyLevelJson();
                    Aca_StudyLevel entity;
                    if (id <= 0)
                    {
                        entity = Aca_StudyLevel.GetNew();
                    }
                    else
                    {
                        entity = studyLevelDataService.GetById(id);
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
        private HttpResult<Aca_StudyLevelJson> GetStudyLevelByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_StudyLevelJson>();
        
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
                using (Aca_StudyLevelDataService studyLevelDataService = new Aca_StudyLevelDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Aca_StudyLevelJson();
                    Aca_StudyLevel entity;
                    if (id <= 0)
                    {
                        entity = Aca_StudyLevel.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("Aca_StudyLevel");
                        //includeTables.Add("Aca_StudyLevel");

                        entity = studyLevelDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Aca_StudyLevelJson> GetStudyLevelListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_StudyLevelJson>();
            try
            {
                //Aca_StudyLevelDataService studyLevelDataService =
                //    new Aca_StudyLevelDataService(DbInstance, HttpUtil.Profile);
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
        public HttpListResult<Aca_StudyLevelJson> GetStudyLevelDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_StudyLevelJson>();
            try
            {
                //Aca_StudyLevelDataService studyLevelDataService =
                //    new Aca_StudyLevelDataService(DbInstance, HttpUtil.Profile);
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
        #endregion

        #region Save/Delete
        [HttpPost]
        public HttpResult<Aca_StudyLevelJson> SaveStudyLevel(Aca_StudyLevelJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_StudyLevelJson>();
          
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.SemesterManager.StudyLevelTerm.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.SemesterManager.StudyLevelTerm.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                 var entityReceived = new Aca_StudyLevel();
                 var dbAttachedEntity = new Aca_StudyLevel();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {
                    if (SaveStudyLevelLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<Aca_StudyLevelJson> SaveStudyLevel2(Aca_StudyLevelJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_StudyLevelJson>();
            try
            {
                using (Aca_StudyLevelDataService studyLevelDataService = new Aca_StudyLevelDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new Aca_StudyLevel();
                    var dbAttachedEntity = new Aca_StudyLevel();
                    json.Map(entityReceived);
                    if (studyLevelDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<Aca_StudyLevelJson> SaveStudyLevelList(IList<Aca_StudyLevelJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_StudyLevelJson>();
            //todo enable it while you need the permission
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.SemesterManager.StudyLevelTerm.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.SemesterManager.StudyLevelTerm.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_StudyLevelDataService studyLevelDataService = new Aca_StudyLevelDataService(DbInstance, HttpUtil.Profile))
                {
                    //var entityListReceived = new List<Aca_StudyLevel>();
                    //var dbAttachedListEntity = new List<Aca_StudyLevel>();
                    //jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (studyLevelDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<Aca_StudyLevelJson> GetDeleteStudyLevelById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_StudyLevelJson>();
            //todo fix the permission
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.SemesterManager.StudyLevelTerm.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_StudyLevelDataService studyLevelDataService = new Aca_StudyLevelDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!studyLevelDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveStudyLevel(Aca_StudyLevel newObj, out string error)
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
            if (newObj.LevelNo==null)
            {
                error += "Level No required.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.Aca_StudyLevel.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A StudyLevel already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveStudyLevelLogic(Aca_StudyLevel newObj,ref Aca_StudyLevel dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Study Level to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveStudyLevel(newObj, out error))
                return false;

            bool isNewObject = true;
            Aca_StudyLevel objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Aca_StudyLevel.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Aca_StudyLevel.GetNew(newObj.Id);
                DbInstance.Aca_StudyLevel.Add(objToSave);
               
               
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
           objToSave.LevelNo =  newObj.LevelNo ;
           
           
           dbAddedObj = objToSave;
           
            //here save any child table
            return true;
        }
        #endregion
        #endregion

	}
}

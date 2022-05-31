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

    public class HomeWorkApiController : EmployeeBaseApiController
	{
        public HomeWorkApiController()
        {  }
       
        #region HomeWork 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Aca_HomeWorkJson> GetPagedHomeWork(string textkey, int? pageSize, int? pageNo
           ,Int32 classShiftSectionMapId= 0      
           ,Int32 subjectId= 0      
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Aca_HomeWorkJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.HomeWork.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.HomeWork.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.HomeWork.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/

            try
            {
                using (Aca_HomeWorkDataService homeWorkDataService = new Aca_HomeWorkDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<Aca_HomeWork> query = DbInstance.Aca_HomeWork.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Title.ToLower().Contains(textkey.ToLower()));
                    }
                    if (classShiftSectionMapId>0)
                    {
                        query = query.Where(q => q.ClassShiftSectionMapId== classShiftSectionMapId);
                    }  
                    if (subjectId>0)
                    {
                        query = query.Where(q => q.SubjectId== subjectId);
                    }  
                 
                    var entities = homeWorkDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Aca_HomeWorkJson>();
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
        private HttpListResult<Aca_HomeWorkJson> GetAllHomeWork()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_HomeWorkJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.HomeWork.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.HomeWork.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.HomeWork.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Aca_HomeWorkDataService homeWorkDataService = new Aca_HomeWorkDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<Aca_HomeWorkJson>();
                    var entities = homeWorkDataService.GetAll(out error);
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
        public HttpResult<Aca_HomeWorkJson> GetHomeWorkById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_HomeWorkJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.HomeWork.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.HomeWork.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.HomeWork.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Aca_HomeWorkDataService homeWorkDataService = new Aca_HomeWorkDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Aca_HomeWorkJson();
                    Aca_HomeWork entity;
                    if (id <= 0)
                    {
                        entity = Aca_HomeWork.GetNew();
                    }
                    else
                    {
                        entity = homeWorkDataService.GetById(id);
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
        private HttpResult<Aca_HomeWorkJson> GetHomeWorkByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_HomeWorkJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.HomeWork.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.HomeWork.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.HomeWork.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Aca_HomeWorkDataService homeWorkDataService = new Aca_HomeWorkDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Aca_HomeWorkJson();
                    Aca_HomeWork entity;
                    if (id <= 0)
                    {
                        entity = Aca_HomeWork.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("Aca_HomeWork");
                        //includeTables.Add("Aca_HomeWork");

                        entity = homeWorkDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Aca_HomeWorkJson> GetHomeWorkListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_HomeWorkJson>();
            try
            {
                //Aca_HomeWorkDataService homeWorkDataService =
                //    new Aca_HomeWorkDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.GroupEnumList = EnumUtil.GetEnumList(typeof(Aca_HomeWork.GroupEnum));
                result.DataExtra.HomeworkTypeEnumList = EnumUtil.GetEnumList(typeof(Aca_HomeWork.HomeworkTypeEnum));
                //DropDown Option Tables
                 
                //result.DataExtra.ClassShiftSectionMapList = DbInstance.Aca_ClassShiftSectionMap.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
                //result.DataExtra.SubjectList = DbInstance.Aca_Subject.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<Aca_HomeWorkJson> GetHomeWorkDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_HomeWorkJson>();
            try
            {
                //Aca_HomeWorkDataService homeWorkDataService =
                //    new Aca_HomeWorkDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.GroupEnumList = EnumUtil.GetEnumList(typeof(Aca_HomeWork.GroupEnum));
                result.DataExtra.HomeworkTypeEnumList = EnumUtil.GetEnumList(typeof(Aca_HomeWork.HomeworkTypeEnum));
                //DropDown Option Tables
                 
                //result.DataExtra.ClassShiftSectionMapList = DbInstance.Aca_ClassShiftSectionMap.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
                //result.DataExtra.SubjectList = DbInstance.Aca_Subject.AsEnumerable().Select(t => new
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
        public HttpResult<Aca_HomeWorkJson> SaveHomeWork(Aca_HomeWorkJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_HomeWorkJson>();
            //optional permission, check permission in the SaveHomeWorkLogic insted
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.HomeWork.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.HomeWork.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                 var entityReceived = new Aca_HomeWork();
                 var dbAttachedEntity = new Aca_HomeWork();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SaveHomeWorkLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<Aca_HomeWorkJson> SaveHomeWork2(Aca_HomeWorkJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_HomeWorkJson>();
            try
            {
                using (Aca_HomeWorkDataService homeWorkDataService = new Aca_HomeWorkDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new Aca_HomeWork();
                    var dbAttachedEntity = new Aca_HomeWork();
                    json.Map(entityReceived);
                    if (homeWorkDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<Aca_HomeWorkJson> SaveHomeWorkList(IList<Aca_HomeWorkJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_HomeWorkJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.HomeWork.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.HomeWork.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Aca_HomeWorkDataService homeWorkDataService = new Aca_HomeWorkDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<Aca_HomeWork>();
                    var dbAttachedListEntity = new List<Aca_HomeWork>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (homeWorkDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<Aca_HomeWorkJson> GetDeleteHomeWorkById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_HomeWorkJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.HomeWork.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Aca_HomeWorkDataService homeWorkDataService = new Aca_HomeWorkDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!homeWorkDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveHomeWork(Aca_HomeWork newObj, out string error)
        {
            error = "";
            if (!newObj.Title.IsValid())
            {
                error += "Title is not valid.";
                return false;
            }
            if (newObj.Title==null)
            {
                error += "Title required.";
                return false;
            }
            if (!newObj.DueTime.IsValid())
            {
                error += "Due Time is not valid.";
                return false;
            }
            if (!newObj.CloseTime.IsValid())
            {
                error += "Close Time is not valid.";
                return false;
            }
            /*if (newObj.ClassShiftSectionMapId<=0)
            {
                error += "Please select valid Class Shift Section Map.";
                return false;
            }*/
            /*if (newObj.SubjectId<=0)
            {
                error += "Please select valid Subject.";
                return false;
            }*/

            if (newObj.HomeworkTypeEnumId==null)
            {
                error += "Please select valid Homework Type.";
                return false;
            }
            if (!newObj.HomeworkKey.IsValid())
            {
                error += "Homework Key is not valid.";
                return false;
            }
            if (newObj.HomeworkKey==null)
            {
                error += "Homework Key required.";
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
            //var res =  DbInstance.Aca_HomeWork.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A HomeWork already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveHomeWorkLogic(Aca_HomeWork newObj,ref Aca_HomeWork dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Home Work to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveHomeWork(newObj, out error))
                return false;

            bool isNewObject = true;
            Aca_HomeWork objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Aca_HomeWork.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Aca_HomeWork.GetNew(newObj.Id);
                DbInstance.Aca_HomeWork.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.HomeWork.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.HomeWork.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
           objToSave.Title =  newObj.Title ;
           objToSave.DueTime =  newObj.DueTime ;
           objToSave.CloseTime =  newObj.CloseTime ;
           objToSave.ClassShiftSectionMapId =  newObj.ClassShiftSectionMapId ;
           objToSave.SubjectId =  newObj.SubjectId ;
           objToSave.GroupEnumId =  newObj.GroupEnumId ;
           objToSave.HomeworkTypeEnumId =  newObj.HomeworkTypeEnumId ;
           objToSave.HomeworkKey =  newObj.HomeworkKey ;
           objToSave.Description =  newObj.Description ;
           objToSave.TeacherId =  newObj.TeacherId ;
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

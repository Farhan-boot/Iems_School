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

    public class StudentQuotaNameApiController : EmployeeBaseApiController
	{
        public StudentQuotaNameApiController()
        {  }
       
        #region StudentQuotaName 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Adm_StudentQuotaNameJson> GetPagedStudentQuotaName(string textkey, int? pageSize, int? pageNo
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Adm_StudentQuotaNameJson>();
      
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentQuotaName.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentQuotaName.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentQuotaName.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }

            try
            {
                using (Adm_StudentQuotaNameDataService studentQuotaNameDataService = new Adm_StudentQuotaNameDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<Adm_StudentQuotaName> query = DbInstance.Adm_StudentQuotaName.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Name.ToLower().Contains(textkey.ToLower()));
                    }
                 
                    var entities = studentQuotaNameDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Adm_StudentQuotaNameJson>();
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
        private HttpListResult<Adm_StudentQuotaNameJson> GetAllStudentQuotaName()
        {
            string error = string.Empty;
            var result = new HttpListResult<Adm_StudentQuotaNameJson>();
      
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentQuotaName.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentQuotaName.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentQuotaName.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Adm_StudentQuotaNameDataService studentQuotaNameDataService = new Adm_StudentQuotaNameDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<Adm_StudentQuotaNameJson>();
                    var entities = studentQuotaNameDataService.GetAll(out error);
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
        public HttpResult<Adm_StudentQuotaNameJson> GetStudentQuotaNameById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Adm_StudentQuotaNameJson>();
        
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentQuotaName.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentQuotaName.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentQuotaName.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Adm_StudentQuotaNameDataService studentQuotaNameDataService = new Adm_StudentQuotaNameDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Adm_StudentQuotaNameJson();
                    Adm_StudentQuotaName entity;
                    if (id <= 0)
                    {
                        entity = Adm_StudentQuotaName.GetNew();
                    }
                    else
                    {
                        entity = studentQuotaNameDataService.GetById(id);
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
        private HttpResult<Adm_StudentQuotaNameJson> GetStudentQuotaNameByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Adm_StudentQuotaNameJson>();
       
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentQuotaName.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentQuotaName.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentQuotaName.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Adm_StudentQuotaNameDataService studentQuotaNameDataService = new Adm_StudentQuotaNameDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Adm_StudentQuotaNameJson();
                    Adm_StudentQuotaName entity;
                    if (id <= 0)
                    {
                        entity = Adm_StudentQuotaName.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("Adm_StudentQuotaName");
                        //includeTables.Add("Adm_StudentQuotaName");

                        entity = studentQuotaNameDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Adm_StudentQuotaNameJson> GetStudentQuotaNameListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Adm_StudentQuotaNameJson>();
            try
            {
                //Adm_StudentQuotaNameDataService studentQuotaNameDataService =
                //    new Adm_StudentQuotaNameDataService(DbInstance, HttpUtil.Profile);
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
        public HttpListResult<Adm_StudentQuotaNameJson> GetStudentQuotaNameDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Adm_StudentQuotaNameJson>();
            try
            {
                //Adm_StudentQuotaNameDataService studentQuotaNameDataService =
                //    new Adm_StudentQuotaNameDataService(DbInstance, HttpUtil.Profile);
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
        public HttpResult<Adm_StudentQuotaNameJson> SaveStudentQuotaName(Adm_StudentQuotaNameJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Adm_StudentQuotaNameJson>();
           
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentQuotaName.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentQuotaName.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                 var entityReceived = new Adm_StudentQuotaName();
                 var dbAttachedEntity = new Adm_StudentQuotaName();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {
                    if (SaveStudentQuotaNameLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<Adm_StudentQuotaNameJson> SaveStudentQuotaName2(Adm_StudentQuotaNameJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Adm_StudentQuotaNameJson>();
            try
            {
                using (Adm_StudentQuotaNameDataService studentQuotaNameDataService = new Adm_StudentQuotaNameDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new Adm_StudentQuotaName();
                    var dbAttachedEntity = new Adm_StudentQuotaName();
                    json.Map(entityReceived);
                    if (studentQuotaNameDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<Adm_StudentQuotaNameJson> SaveStudentQuotaNameList(IList<Adm_StudentQuotaNameJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Adm_StudentQuotaNameJson>();
         
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentQuotaName.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentQuotaName.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Adm_StudentQuotaNameDataService studentQuotaNameDataService = new Adm_StudentQuotaNameDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<Adm_StudentQuotaName>();
                    var dbAttachedListEntity = new List<Adm_StudentQuotaName>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (studentQuotaNameDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<Adm_StudentQuotaNameJson> GetDeleteStudentQuotaNameById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Adm_StudentQuotaNameJson>();
        
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentQuotaName.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Adm_StudentQuotaNameDataService studentQuotaNameDataService = new Adm_StudentQuotaNameDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!studentQuotaNameDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveStudentQuotaName(Adm_StudentQuotaName newObj, out string error)
        {
            error = "";
            if (!newObj.Name.IsValid())
            {
                error += "Name is not valid.";
                return false;
            }
            if (newObj.Name.Length>255)
            {
                error += "Name exceeded its maximum length 255.";
                return false;
            }

            if (newObj.OrderNo==null)
            {
                error += "Order No required.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.Adm_StudentQuotaName.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A StudentQuotaName already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveStudentQuotaNameLogic(Adm_StudentQuotaName newObj,ref Adm_StudentQuotaName dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Student Quota Name to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveStudentQuotaName(newObj, out error))
                return false;

            bool isNewObject = true;
            Adm_StudentQuotaName objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Adm_StudentQuotaName.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Adm_StudentQuotaName.GetNew(newObj.Id);
                DbInstance.Adm_StudentQuotaName.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
          
            if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentQuotaName.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentQuotaName.CanEdit,
                   out error))
            {
                return false;
            }

            //binding object  
           objToSave.Name =  newObj.Name ;
           objToSave.Description =  newObj.Description ;
           objToSave.OrderNo =  newObj.OrderNo ;
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

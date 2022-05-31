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

    public class ParentsPrimaryJobTypeApiController : EmployeeBaseApiController
	{
        public ParentsPrimaryJobTypeApiController()
        {  }
       
        #region ParentsPrimaryJobType 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Adm_ParentsPrimaryJobTypeJson> GetPagedParentsPrimaryJobType(string textkey, int? pageSize, int? pageNo
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Adm_ParentsPrimaryJobTypeJson>();
      
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.ParentsPrimaryJobType.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.ParentsPrimaryJobType.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.ParentsPrimaryJobType.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }

            try
            {
                using (Adm_ParentsPrimaryJobTypeDataService parentsPrimaryJobTypeDataService = new Adm_ParentsPrimaryJobTypeDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<Adm_ParentsPrimaryJobType> query = DbInstance.Adm_ParentsPrimaryJobType.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Name.ToLower().Contains(textkey.ToLower()));
                    }
                 
                    var entities = parentsPrimaryJobTypeDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Adm_ParentsPrimaryJobTypeJson>();
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
        private HttpListResult<Adm_ParentsPrimaryJobTypeJson> GetAllParentsPrimaryJobType()
        {
            string error = string.Empty;
            var result = new HttpListResult<Adm_ParentsPrimaryJobTypeJson>();
       if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.ParentsPrimaryJobType.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.ParentsPrimaryJobType.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.ParentsPrimaryJobType.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Adm_ParentsPrimaryJobTypeDataService parentsPrimaryJobTypeDataService = new Adm_ParentsPrimaryJobTypeDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<Adm_ParentsPrimaryJobTypeJson>();
                    var entities = parentsPrimaryJobTypeDataService.GetAll(out error);
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
        public HttpResult<Adm_ParentsPrimaryJobTypeJson> GetParentsPrimaryJobTypeById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Adm_ParentsPrimaryJobTypeJson>();
         if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.ParentsPrimaryJobType.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.ParentsPrimaryJobType.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.ParentsPrimaryJobType.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Adm_ParentsPrimaryJobTypeDataService parentsPrimaryJobTypeDataService = new Adm_ParentsPrimaryJobTypeDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Adm_ParentsPrimaryJobTypeJson();
                    Adm_ParentsPrimaryJobType entity;
                    if (id <= 0)
                    {
                        entity = Adm_ParentsPrimaryJobType.GetNew();
                    }
                    else
                    {
                        entity = parentsPrimaryJobTypeDataService.GetById(id);
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
        private HttpResult<Adm_ParentsPrimaryJobTypeJson> GetParentsPrimaryJobTypeByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Adm_ParentsPrimaryJobTypeJson>();
          if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.ParentsPrimaryJobType.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.ParentsPrimaryJobType.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.ParentsPrimaryJobType.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Adm_ParentsPrimaryJobTypeDataService parentsPrimaryJobTypeDataService = new Adm_ParentsPrimaryJobTypeDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Adm_ParentsPrimaryJobTypeJson();
                    Adm_ParentsPrimaryJobType entity;
                    if (id <= 0)
                    {
                        entity = Adm_ParentsPrimaryJobType.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("Adm_ParentsPrimaryJobType");
                        //includeTables.Add("Adm_ParentsPrimaryJobType");

                        entity = parentsPrimaryJobTypeDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Adm_ParentsPrimaryJobTypeJson> GetParentsPrimaryJobTypeListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Adm_ParentsPrimaryJobTypeJson>();
            try
            {
                //Adm_ParentsPrimaryJobTypeDataService parentsPrimaryJobTypeDataService =
                //    new Adm_ParentsPrimaryJobTypeDataService(DbInstance, HttpUtil.Profile);
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
        public HttpListResult<Adm_ParentsPrimaryJobTypeJson> GetParentsPrimaryJobTypeDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Adm_ParentsPrimaryJobTypeJson>();
            try
            {
                //Adm_ParentsPrimaryJobTypeDataService parentsPrimaryJobTypeDataService =
                //    new Adm_ParentsPrimaryJobTypeDataService(DbInstance, HttpUtil.Profile);
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
        public HttpResult<Adm_ParentsPrimaryJobTypeJson> SaveParentsPrimaryJobType(Adm_ParentsPrimaryJobTypeJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Adm_ParentsPrimaryJobTypeJson>();
           if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.ParentsPrimaryJobType.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.ParentsPrimaryJobType.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                 var entityReceived = new Adm_ParentsPrimaryJobType();
                 var dbAttachedEntity = new Adm_ParentsPrimaryJobType();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {
                    if (SaveParentsPrimaryJobTypeLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<Adm_ParentsPrimaryJobTypeJson> SaveParentsPrimaryJobType2(Adm_ParentsPrimaryJobTypeJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Adm_ParentsPrimaryJobTypeJson>();
            try
            {
                using (Adm_ParentsPrimaryJobTypeDataService parentsPrimaryJobTypeDataService = new Adm_ParentsPrimaryJobTypeDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new Adm_ParentsPrimaryJobType();
                    var dbAttachedEntity = new Adm_ParentsPrimaryJobType();
                    json.Map(entityReceived);
                    if (parentsPrimaryJobTypeDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<Adm_ParentsPrimaryJobTypeJson> SaveParentsPrimaryJobTypeList(IList<Adm_ParentsPrimaryJobTypeJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Adm_ParentsPrimaryJobTypeJson>();
          if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.ParentsPrimaryJobType.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.ParentsPrimaryJobType.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Adm_ParentsPrimaryJobTypeDataService parentsPrimaryJobTypeDataService = new Adm_ParentsPrimaryJobTypeDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<Adm_ParentsPrimaryJobType>();
                    var dbAttachedListEntity = new List<Adm_ParentsPrimaryJobType>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (parentsPrimaryJobTypeDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<Adm_ParentsPrimaryJobTypeJson> GetDeleteParentsPrimaryJobTypeById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Adm_ParentsPrimaryJobTypeJson>();
          if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.ParentsPrimaryJobType.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Adm_ParentsPrimaryJobTypeDataService parentsPrimaryJobTypeDataService = new Adm_ParentsPrimaryJobTypeDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!parentsPrimaryJobTypeDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveParentsPrimaryJobType(Adm_ParentsPrimaryJobType newObj, out string error)
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
            //var res =  DbInstance.Adm_ParentsPrimaryJobType.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A ParentsPrimaryJobType already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveParentsPrimaryJobTypeLogic(Adm_ParentsPrimaryJobType newObj,ref Adm_ParentsPrimaryJobType dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Parents Primary Job Type to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveParentsPrimaryJobType(newObj, out error))
                return false;

            bool isNewObject = true;
            Adm_ParentsPrimaryJobType objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Adm_ParentsPrimaryJobType.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Adm_ParentsPrimaryJobType.GetNew(newObj.Id);
                DbInstance.Adm_ParentsPrimaryJobType.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.ParentsPrimaryJobType.CanAdd, out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.ParentsPrimaryJobType.CanEdit, out error))
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

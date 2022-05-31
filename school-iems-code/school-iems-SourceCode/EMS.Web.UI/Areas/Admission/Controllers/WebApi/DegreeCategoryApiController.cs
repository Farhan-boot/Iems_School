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
using static EMS.DataAccess.Data.User_Education;
using static EMS.DataAccess.Data.Adm_EducationBoard;

namespace EMS.Web.UI.Areas.Admission.Controllers.WebApi
{

    public class DegreeCategoryApiController : EmployeeBaseApiController
	{
        public DegreeCategoryApiController()
        {  }
       
        #region DegreeCategory 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Adm_DegreeCategoryJson> GetPagedDegreeCategory(string textkey, int? pageSize, int? pageNo
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Adm_DegreeCategoryJson>();
        
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.DegreeCategory.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.DegreeCategory.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.DegreeCategory.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            try
            {
                using (Adm_DegreeCategoryDataService degreeCategoryDataService = new Adm_DegreeCategoryDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<Adm_DegreeCategory> query = DbInstance.Adm_DegreeCategory.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Name.ToLower().Contains(textkey.ToLower()));
                    }
                 
                    var entities = degreeCategoryDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Adm_DegreeCategoryJson>();
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
        private HttpListResult<Adm_DegreeCategoryJson> GetAllDegreeCategory()
        {
            string error = string.Empty;
            var result = new HttpListResult<Adm_DegreeCategoryJson>();
          
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.DegreeCategory.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.DegreeCategory.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.DegreeCategory.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Adm_DegreeCategoryDataService degreeCategoryDataService = new Adm_DegreeCategoryDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<Adm_DegreeCategoryJson>();
                    var entities = degreeCategoryDataService.GetAll(out error);
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
        public HttpResult<Adm_DegreeCategoryJson> GetDegreeCategoryById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Adm_DegreeCategoryJson>();
       
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.DegreeCategory.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.DegreeCategory.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.DegreeCategory.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Adm_DegreeCategoryDataService degreeCategoryDataService = new Adm_DegreeCategoryDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Adm_DegreeCategoryJson();
                    Adm_DegreeCategory entity;
                    if (id <= 0)
                    {
                        entity = Adm_DegreeCategory.GetNew();
                    }
                    else
                    {
                        entity = degreeCategoryDataService.GetById(id);
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
        private HttpResult<Adm_DegreeCategoryJson> GetDegreeCategoryByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Adm_DegreeCategoryJson>();
        
             if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.DegreeCategory.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.DegreeCategory.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.DegreeCategory.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Adm_DegreeCategoryDataService degreeCategoryDataService = new Adm_DegreeCategoryDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Adm_DegreeCategoryJson();
                    Adm_DegreeCategory entity;
                    if (id <= 0)
                    {
                        entity = Adm_DegreeCategory.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("Adm_DegreeCategory");
                        //includeTables.Add("Adm_DegreeCategory");

                        entity = degreeCategoryDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Adm_DegreeCategoryJson> GetDegreeCategoryListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Adm_DegreeCategoryJson>();
            try
            {
                //Adm_DegreeCategoryDataService degreeCategoryDataService =
                //    new Adm_DegreeCategoryDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.DegreeEquivalentEnumList = EnumUtil.GetEnumList(typeof(DegreeEquivalentEnum));
                result.DataExtra.BoardTypeEnumList = EnumUtil.GetEnumList(typeof(BoardTypeEnum));
                //DropDown Option Tables
                 
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<Adm_DegreeCategoryJson> GetDegreeCategoryDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Adm_DegreeCategoryJson>();
            try
            {
                //Adm_DegreeCategoryDataService degreeCategoryDataService =
                //    new Adm_DegreeCategoryDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.DegreeEquivalentEnumList = EnumUtil.GetEnumList(typeof(DegreeEquivalentEnum));
                result.DataExtra.BoardTypeEnumList = EnumUtil.GetEnumList(typeof(BoardTypeEnum));
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
        public HttpResult<Adm_DegreeCategoryJson> SaveDegreeCategory(Adm_DegreeCategoryJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Adm_DegreeCategoryJson>();
          
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.DegreeCategory.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.DegreeCategory.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                 var entityReceived = new Adm_DegreeCategory();
                 var dbAttachedEntity = new Adm_DegreeCategory();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {
                    if (SaveDegreeCategoryLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<Adm_DegreeCategoryJson> SaveDegreeCategory2(Adm_DegreeCategoryJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Adm_DegreeCategoryJson>();
            try
            {
                using (Adm_DegreeCategoryDataService degreeCategoryDataService = new Adm_DegreeCategoryDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new Adm_DegreeCategory();
                    var dbAttachedEntity = new Adm_DegreeCategory();
                    json.Map(entityReceived);
                    if (degreeCategoryDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<Adm_DegreeCategoryJson> SaveDegreeCategoryList(IList<Adm_DegreeCategoryJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Adm_DegreeCategoryJson>();
            //todo fix the permission
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.DegreeCategory.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.DegreeCategory.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Adm_DegreeCategoryDataService degreeCategoryDataService = new Adm_DegreeCategoryDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<Adm_DegreeCategory>();
                    var dbAttachedListEntity = new List<Adm_DegreeCategory>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (degreeCategoryDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<Adm_DegreeCategoryJson> GetDeleteDegreeCategoryById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Adm_DegreeCategoryJson>();
    
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.DegreeCategory.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Adm_DegreeCategoryDataService degreeCategoryDataService = new Adm_DegreeCategoryDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!degreeCategoryDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveDegreeCategory(Adm_DegreeCategory newObj, out string error)
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
            if (!newObj.FullName.IsValid())
            {
                error += "Full Name is not valid.";
                return false;
            }
            if (newObj.FullName.Length>250)
            {
                error += "Full Name exceeded its maximum length 250.";
                return false;
            }
            if (newObj.DegreeEquivalentEnumId==null)
            {
                error += "Please select valid Degree Equivalent.";
                return false;
            }
            if (newObj.BoardTypeEnumId==null)
            {
                error += "Please select valid Board Type.";
                return false;
            }
            if (newObj.OrderNo==null)
            {
                error += "Order No required.";
                return false;
            }
            if (newObj.IsEnable==null)
            {
                error += "Is Enable required.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.Adm_DegreeCategory.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A DegreeCategory already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveDegreeCategoryLogic(Adm_DegreeCategory newObj,ref Adm_DegreeCategory dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Degree Category to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveDegreeCategory(newObj, out error))
                return false;

            bool isNewObject = true;
            Adm_DegreeCategory objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Adm_DegreeCategory.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Adm_DegreeCategory.GetNew(newObj.Id);
                DbInstance.Adm_DegreeCategory.Add(objToSave);
               
               
            }
            //checking permission, enable it with correction
            if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.DegreeCategory.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.DegreeCategory.CanEdit,
                   out error))
            {
                return false;
            }

            //binding object  
           objToSave.Name =  newObj.Name ;
           objToSave.FullName =  newObj.FullName ;
           objToSave.DegreeEquivalentEnumId =  newObj.DegreeEquivalentEnumId ;
           objToSave.BoardTypeEnumId =  newObj.BoardTypeEnumId ;
           objToSave.OrderNo =  newObj.OrderNo ;
           objToSave.IsEnable =  newObj.IsEnable ;
           
           
           dbAddedObj = objToSave;
           
            //here save any child table
            return true;
        }
        #endregion
        #endregion

	}
}

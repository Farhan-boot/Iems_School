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
using System.Data;

namespace EMS.Web.UI.Areas.Inventory.Controllers.WebApi
{

    public class CategoryApiController : EmployeeBaseApiController
	{
        public CategoryApiController()
        {  }
       
        #region Category 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Invt_CategoryJson> GetPagedCategory(string textkey, int? pageSize, int? pageNo
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Invt_CategoryJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Category.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Category.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Category.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/

            try
            {
                using (Invt_CategoryDataService categoryDataService = new Invt_CategoryDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<Invt_Category> query = DbInstance.Invt_Category.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Name.ToLower().Contains(textkey.ToLower()));
                    }
                 
                    var entities = categoryDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Invt_CategoryJson>();
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
        private HttpListResult<Invt_CategoryJson> GetAllCategory()
        {
            string error = string.Empty;
            var result = new HttpListResult<Invt_CategoryJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Category.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Category.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Category.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Invt_CategoryDataService categoryDataService = new Invt_CategoryDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<Invt_CategoryJson>();
                    var entities = categoryDataService.GetAll(out error);
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
        public HttpResult<Invt_CategoryJson> GetCategoryById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Invt_CategoryJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Category.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Category.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Category.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Invt_CategoryDataService categoryDataService = new Invt_CategoryDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Invt_CategoryJson();
                    Invt_Category entity;
                    if (id <= 0)
                    {
                        entity = Invt_Category.GetNew();
                    }
                    else
                    {
                        entity = categoryDataService.GetById(id);
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
        private HttpResult<Invt_CategoryJson> GetCategoryByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Invt_CategoryJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Category.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Category.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Category.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Invt_CategoryDataService categoryDataService = new Invt_CategoryDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Invt_CategoryJson();
                    Invt_Category entity;
                    if (id <= 0)
                    {
                        entity = Invt_Category.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("Invt_Category");
                        //includeTables.Add("Invt_Category");

                        entity = categoryDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Invt_CategoryJson> GetCategoryListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Invt_CategoryJson>();
            try
            {
                //Invt_CategoryDataService categoryDataService =
                //    new Invt_CategoryDataService(DbInstance, HttpUtil.Profile);
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
        public HttpListResult<Invt_CategoryJson> GetCategoryDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Invt_CategoryJson>();
            try
            {
                //Invt_CategoryDataService categoryDataService =
                //    new Invt_CategoryDataService(DbInstance, HttpUtil.Profile);
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
        public HttpResult<Invt_CategoryJson> SaveCategory(Invt_CategoryJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Invt_CategoryJson>();
            //optional permission, check permission in the SaveCategoryLogic insted
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Category.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Category.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                 var entityReceived = new Invt_Category();
                 var dbAttachedEntity = new Invt_Category();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {

                    if (SaveCategoryLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<Invt_CategoryJson> SaveCategory2(Invt_CategoryJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Invt_CategoryJson>();
            try
            {
                using (Invt_CategoryDataService categoryDataService = new Invt_CategoryDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new Invt_Category();
                    var dbAttachedEntity = new Invt_Category();
                    json.Map(entityReceived);
                    if (categoryDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<Invt_CategoryJson> SaveCategoryList(IList<Invt_CategoryJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Invt_CategoryJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Category.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Category.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Invt_CategoryDataService categoryDataService = new Invt_CategoryDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<Invt_Category>();
                    var dbAttachedListEntity = new List<Invt_Category>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (categoryDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<Invt_CategoryJson> GetDeleteCategoryById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Invt_CategoryJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Category.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Invt_CategoryDataService categoryDataService = new Invt_CategoryDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!categoryDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveCategory(Invt_Category newObj, out string error)
        {
            error = "";

            if (!newObj.Name.IsValid())
            {
                error += "Name is not valid.";
                return false;
            }
            if (newObj.Name.Length>256)
            {
                error += "Name exceeded its maximum length 256.";
                return false;
            }

            if (newObj.IsDeleted==null)
            {
                error += "Is Deleted required.";
                return false;
            }
            //TODO write your custom validation here.
            var res = DbInstance.Invt_Category.Any(x => x.Id != newObj.Id && x.Name == newObj.Name);
            if (res)
            {
                error = "A Category Name already exists!";
                return false;
            }
            return true;
        }
        private bool SaveCategoryLogic(Invt_Category newObj,ref Invt_Category dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Category to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveCategory(newObj, out error))
                return false;

            bool isNewObject = true;
            Invt_Category objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Invt_Category.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Invt_Category.GetNew(newObj.Id);
                DbInstance.Invt_Category.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //todo mandatory fix checking permission, enable it with correction

            //binding object  
           objToSave.Name =  newObj.Name ;
           objToSave.Description =  newObj.Description ;
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

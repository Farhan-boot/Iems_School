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

namespace EMS.Web.UI.Areas.Admin.Controllers.WebApi
{

    public class WarhouseApiController : EmployeeBaseApiController
	{
        public WarhouseApiController()
        {  }
       
        #region Warhouse 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Invt_WarhouseJson> GetPagedWarhouse(string textkey, int? pageSize, int? pageNo
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Invt_WarhouseJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Warhouse.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Warhouse.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Warhouse.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/

            try
            {
                using (Invt_WarhouseDataService warhouseDataService = new Invt_WarhouseDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<Invt_Warhouse> query = DbInstance.Invt_Warhouse.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Name.ToLower().Contains(textkey.ToLower()));
                    }
                 
                    var entities = warhouseDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Invt_WarhouseJson>();
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
        private HttpListResult<Invt_WarhouseJson> GetAllWarhouse()
        {
            string error = string.Empty;
            var result = new HttpListResult<Invt_WarhouseJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Warhouse.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Warhouse.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Warhouse.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Invt_WarhouseDataService warhouseDataService = new Invt_WarhouseDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<Invt_WarhouseJson>();
                    var entities = warhouseDataService.GetAll(out error);
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
        public HttpResult<Invt_WarhouseJson> GetWarhouseById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Invt_WarhouseJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Warhouse.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Warhouse.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Warhouse.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Invt_WarhouseDataService warhouseDataService = new Invt_WarhouseDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Invt_WarhouseJson();
                    Invt_Warhouse entity;
                    if (id <= 0)
                    {
                        entity = Invt_Warhouse.GetNew();
                    }
                    else
                    {
                        entity = warhouseDataService.GetById(id);
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
        private HttpResult<Invt_WarhouseJson> GetWarhouseByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Invt_WarhouseJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Warhouse.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Warhouse.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Warhouse.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Invt_WarhouseDataService warhouseDataService = new Invt_WarhouseDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Invt_WarhouseJson();
                    Invt_Warhouse entity;
                    if (id <= 0)
                    {
                        entity = Invt_Warhouse.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("Invt_Warhouse");
                        //includeTables.Add("Invt_Warhouse");

                        entity = warhouseDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Invt_WarhouseJson> GetWarhouseListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Invt_WarhouseJson>();
            try
            {
                //Invt_WarhouseDataService warhouseDataService =
                //    new Invt_WarhouseDataService(DbInstance, HttpUtil.Profile);
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
        public HttpListResult<Invt_WarhouseJson> GetWarhouseDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Invt_WarhouseJson>();
            try
            {
                //Invt_WarhouseDataService warhouseDataService =
                //    new Invt_WarhouseDataService(DbInstance, HttpUtil.Profile);
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
        public HttpResult<Invt_WarhouseJson> SaveWarhouse(Invt_WarhouseJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Invt_WarhouseJson>();
            //optional permission, check permission in the SaveWarhouseLogic insted
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Warhouse.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Warhouse.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                 var entityReceived = new Invt_Warhouse();
                 var dbAttachedEntity = new Invt_Warhouse();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SaveWarhouseLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<Invt_WarhouseJson> SaveWarhouse2(Invt_WarhouseJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Invt_WarhouseJson>();
            try
            {
                using (Invt_WarhouseDataService warhouseDataService = new Invt_WarhouseDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new Invt_Warhouse();
                    var dbAttachedEntity = new Invt_Warhouse();
                    json.Map(entityReceived);
                    if (warhouseDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<Invt_WarhouseJson> SaveWarhouseList(IList<Invt_WarhouseJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Invt_WarhouseJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Warhouse.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Warhouse.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Invt_WarhouseDataService warhouseDataService = new Invt_WarhouseDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<Invt_Warhouse>();
                    var dbAttachedListEntity = new List<Invt_Warhouse>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (warhouseDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<Invt_WarhouseJson> GetDeleteWarhouseById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Invt_WarhouseJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Warhouse.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Invt_WarhouseDataService warhouseDataService = new Invt_WarhouseDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!warhouseDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveWarhouse(Invt_Warhouse newObj, out string error)
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
            var res = DbInstance.Invt_Warhouse.Any(x => x.Id != newObj.Id && x.Name == newObj.Name);
            if (res)
            {
                error = "A Warhouse already exists!";
                return false;
            }
            return true;
        }
        private bool SaveWarhouseLogic(Invt_Warhouse newObj,ref Invt_Warhouse dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Warhouse to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveWarhouse(newObj, out error))
                return false;

            bool isNewObject = true;
            Invt_Warhouse objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Invt_Warhouse.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Invt_Warhouse.GetNew(newObj.Id);
                DbInstance.Invt_Warhouse.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Warhouse.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Warhouse.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
           objToSave.Name =  newObj.Name ;
           objToSave.Address =  newObj.Address ;
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

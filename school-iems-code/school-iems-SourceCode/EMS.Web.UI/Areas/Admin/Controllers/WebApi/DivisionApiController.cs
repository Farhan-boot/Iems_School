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

    public class DivisionApiController : EmployeeBaseApiController
	{
        public DivisionApiController()
        {  }
       
        #region Division 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<General_DivisionJson> GetPagedDivision(string textkey, int? pageSize, int? pageNo
           ,Int32 countryId= 0      
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<General_DivisionJson>();
          if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Division.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Division.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Division.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }

            try
            {
                using (General_DivisionDataService divisionDataService = new General_DivisionDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<General_Division> query = DbInstance.General_Division.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Name.ToLower().Contains(textkey.ToLower()));
                    }
                    if (countryId>0)
                    {
                        query = query.Where(q => q.CountryId== countryId);
                    }  
                 
                    var entities = divisionDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<General_DivisionJson>();
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
        private HttpListResult<General_DivisionJson> GetAllDivision()
        {
            string error = string.Empty;
            var result = new HttpListResult<General_DivisionJson>();
    
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Division.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Division.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Division.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (General_DivisionDataService divisionDataService = new General_DivisionDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<General_DivisionJson>();
                    var entities = divisionDataService.GetAll(out error);
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
        public HttpResult<General_DivisionJson> GetDivisionById(Int64 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<General_DivisionJson>();
         
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Division.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Division.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Division.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (General_DivisionDataService divisionDataService = new General_DivisionDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new General_DivisionJson();
                    General_Division entity;
                    if (id <= 0)
                    {
                        entity = General_Division.GetNew();
                    }
                    else
                    {
                        entity = divisionDataService.GetById(id);
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
        private HttpResult<General_DivisionJson> GetDivisionByIdWithChild(Int64 id)
        {
            string error = string.Empty;
            var result = new HttpResult<General_DivisionJson>();
         
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Division.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Division.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Division.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (General_DivisionDataService divisionDataService = new General_DivisionDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new General_DivisionJson();
                    General_Division entity;
                    if (id <= 0)
                    {
                        entity = General_Division.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("General_Division");
                        //includeTables.Add("General_Division");

                        entity = divisionDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<General_DivisionJson> GetDivisionListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<General_DivisionJson>();
            try
            {
                //General_DivisionDataService divisionDataService =
                //    new General_DivisionDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                //DropDown Option Tables
                 
                //result.DataExtra.CountryList = DbInstance.General_Country.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<General_DivisionJson> GetDivisionDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<General_DivisionJson>();
            try
            {
                General_DivisionDataService divisionDataService =
                    new General_DivisionDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                //DropDown Option Tables
                 
                result.DataExtra.CountryList = DbInstance.General_Country.AsEnumerable().Select(t => new
                { Id = t.Id,Name = t.Name }).ToList();
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
        public HttpResult<General_DivisionJson> SaveDivision(General_DivisionJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<General_DivisionJson>();
           
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Division.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Division.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                 var entityReceived = new General_Division();
                 var dbAttachedEntity = new General_Division();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {
                    if (SaveDivisionLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<General_DivisionJson> SaveDivision2(General_DivisionJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<General_DivisionJson>();
            try
            {
                using (General_DivisionDataService divisionDataService = new General_DivisionDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new General_Division();
                    var dbAttachedEntity = new General_Division();
                    json.Map(entityReceived);
                    if (divisionDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<General_DivisionJson> SaveDivisionList(IList<General_DivisionJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<General_DivisionJson>();
           
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Division.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Division.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (General_DivisionDataService divisionDataService = new General_DivisionDataService(DbInstance, HttpUtil.Profile))
                {
                    //var entityListReceived = new List<General_Division>();
                    //var dbAttachedListEntity = new List<General_Division>();
                    //jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (divisionDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<General_DivisionJson> GetDeleteDivisionById(Int64 id)
        {
            string error = string.Empty;
            var result = new HttpResult<General_DivisionJson>();
        
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Division.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (General_DivisionDataService divisionDataService = new General_DivisionDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!divisionDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveDivision(General_Division newObj, out string error)
        {
            error = "";
            if (!newObj.Name.IsValid())
            {
                error += "Name is not valid.";
                return false;
            }
            if (newObj.Name.Length > 50)
            {
                error += "Name exceeded its maximum length 50.";
                return false;
            }

            if (newObj.CountryId <= 0)
            {
                error += "Please select valid Country.";
                return false;
            }
            if (newObj.OrderNo == null)
            {
                error += "Order No required.";
                return false;
            }
            if (newObj.IsDeleted == null)
            {
                error += "Is Deleted required.";
                return false;
            }
            if (newObj.IsActive == null)
            {
                error += "Is Active required.";
                return false;
            }
            if (newObj.CreatedById == null)
            {
                error += "Created By Id required.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.General_Division.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
            //error = "A Division already exists!";
            //return false;
            //}
            return true;
        }
        private bool SaveDivisionLogic(General_Division newObj, ref General_Division dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Division to save can't be null!";
                return false;
            }

            if (!IsValidToSaveDivision(newObj, out error))
                return false;

            bool isNewObject = true;
            General_Division objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.General_Division.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = General_Division.GetNew(newObj.Id);
                DbInstance.General_Division.Add(objToSave);

                objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }

            if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Division.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Division.CanEdit,
                   out error))
            {
                return false;
            }

            //binding object  
            objToSave.Name = newObj.Name;
            objToSave.DivisionNo = newObj.DivisionNo;
            objToSave.CountryId = newObj.CountryId;
            objToSave.OrderNo = newObj.OrderNo;
            objToSave.IsDeleted = newObj.IsDeleted;
            objToSave.IsActive = newObj.IsActive;
            objToSave.CreatedById = newObj.CreatedById;
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

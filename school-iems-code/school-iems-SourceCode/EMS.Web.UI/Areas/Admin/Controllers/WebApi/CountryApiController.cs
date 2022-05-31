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

    public class CountryApiController : EmployeeBaseApiController
	{
        public CountryApiController()
        {  }
       
        #region Country 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<General_CountryJson> GetPagedCountry(string textkey, int? pageSize, int? pageNo
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<General_CountryJson>();

        
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Country.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Country.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Country.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            try
            {
                using (General_CountryDataService countryDataService = new General_CountryDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<General_Country> query = DbInstance.General_Country.OrderBy(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Name.ToLower().Contains(textkey.ToLower()));
                    }
                 
                    var entities = countryDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<General_CountryJson>();
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
        private HttpListResult<General_CountryJson> GetAllCountry()
        {
            string error = string.Empty;
            var result = new HttpListResult<General_CountryJson>();
       
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Country.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Country.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Country.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (General_CountryDataService countryDataService = new General_CountryDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<General_CountryJson>();
                    var entities = countryDataService.GetAll(out error);
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
        public HttpResult<General_CountryJson> GetCountryById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<General_CountryJson>();
      
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Country.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Country.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Country.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (General_CountryDataService countryDataService = new General_CountryDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new General_CountryJson();
                    General_Country entity;
                    if (id <= 0)
                    {
                        entity = General_Country.GetNew();
                    }
                    else
                    {
                        entity = countryDataService.GetById(id);
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
        private HttpResult<General_CountryJson> GetCountryByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<General_CountryJson>();
           
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Country.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Country.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Country.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (General_CountryDataService countryDataService = new General_CountryDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new General_CountryJson();
                    General_Country entity;
                    if (id <= 0)
                    {
                        entity = General_Country.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("General_Country");
                        //includeTables.Add("General_Country");

                        entity = countryDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<General_CountryJson> GetCountryListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<General_CountryJson>();
            try
            {
                //General_CountryDataService countryDataService =
                //    new General_CountryDataService(DbInstance, HttpUtil.Profile);
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
        public HttpListResult<General_CountryJson> GetCountryDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<General_CountryJson>();
            try
            {
                //General_CountryDataService countryDataService =
                //    new General_CountryDataService(DbInstance, HttpUtil.Profile);
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
        public HttpResult<General_CountryJson> SaveCountry(General_CountryJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<General_CountryJson>();
         
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Country.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Country.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                 var entityReceived = new General_Country();
                 var dbAttachedEntity = new General_Country();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {
                    if (SaveCountryLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<General_CountryJson> SaveCountry2(General_CountryJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<General_CountryJson>();
            try
            {
                using (General_CountryDataService countryDataService = new General_CountryDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new General_Country();
                    var dbAttachedEntity = new General_Country();
                    json.Map(entityReceived);
                    if (countryDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<General_CountryJson> SaveCountryList(IList<General_CountryJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<General_CountryJson>();
     
     if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Country.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Country.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (General_CountryDataService countryDataService = new General_CountryDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<General_Country>();
                    var dbAttachedListEntity = new List<General_Country>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (countryDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<General_CountryJson> GetDeleteCountryById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<General_CountryJson>();
            //todo fix the permission
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Country.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (General_CountryDataService countryDataService = new General_CountryDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!countryDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveCountry(General_Country newObj, out string error)
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
            if (newObj.Nationality.IsValid() && newObj.Nationality.Length > 50)
            {
                error += "Nationality exceeded its maximum length 50.";
                return false;
            }
            if (newObj.CallingCode.IsValid() && newObj.CallingCode.Length > 10)
            {
                error += "Calling Code exceeded its maximum length 10.";
                return false;
            }
            if (newObj.TimeZone.IsValid() && newObj.TimeZone.Length > 10)
            {
                error += "Time Zone exceeded its maximum length 10.";
                return false;
            }
            if (newObj.OrderNo == null)
            {
                error += "Order No required.";
                return false;
            }
            if (newObj.IsActive == null)
            {
                error += "Is Active required.";
                return false;
            }
            if (newObj.IsDeleted == null)
            {
                error += "Is Deleted required.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.General_Country.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
            //error = "A Country already exists!";
            //return false;
            //}
            return true;
        }
        private bool SaveCountryLogic(General_Country newObj, ref General_Country dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Country to save can't be null!";
                return false;
            }

            if (!IsValidToSaveCountry(newObj, out error))
                return false;

            bool isNewObject = true;
            General_Country objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.General_Country.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = General_Country.GetNew(newObj.Id);
                DbInstance.General_Country.Add(objToSave);
                objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
                objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
           
            if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Country.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Country.CanEdit,
                   out error))
            {
                return false;
            }

            //binding object  
            objToSave.Name = newObj.Name;
            objToSave.Nationality = newObj.Nationality;
            objToSave.CallingCode = newObj.CallingCode;
            objToSave.TimeZone = newObj.TimeZone;
            objToSave.OrderNo = newObj.OrderNo;
            objToSave.IsActive = newObj.IsActive;
            objToSave.IsDeleted = newObj.IsDeleted;
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

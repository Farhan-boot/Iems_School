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
using EMS.Framework.Objects;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases;
using EMS.Web.Framework.Bases.WebApi;
//using EMS.Web.Framework.Objects;
//using EMS.Web.Jsons.Mapper;
using EMS.Web.Jsons.Models;
using EMS.Framework.Permissions;

namespace EMS.Web.UI.Areas.Admin.Controllers.WebApi
{

    public class DistrictApiController : EmployeeBaseApiController
	{
        public DistrictApiController()
        {  }
       
        #region District 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<General_DistrictJson> GetPagedDistrict(string textkey, int? pageSize, int? pageNo
           ,Int32 countryId= 0      
           ,Int64 divisionId= 0      
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<General_DistrictJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.District.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.District.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.District.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }


            try
            {
                using (General_DistrictDataService districtDataService = new General_DistrictDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<General_District> query = DbInstance.General_District.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Name.ToLower().Contains(textkey.ToLower()));
                    }
                    if (countryId>0)
                    {
                        query = query.Where(q => q.CountryId== countryId);
                    }  
                    if (divisionId>0)
                    {
                        query = query.Where(q => q.DivisionId== divisionId);
                    }  
                 
                    var entities = districtDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<General_DistrictJson>();
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
        private HttpListResult<General_DistrictJson> GetAllDistrict()
        {
            string error = string.Empty;
            var result = new HttpListResult<General_DistrictJson>();

                    if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.District.CanView, out error)
                        && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.District.CanAdd, out error)
                        && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.District.CanEdit, out error))
                    {
                        result.HasError = true;
                        result.Errors.Add(error);
                        return result;
                    }


            try
            {
                using (General_DistrictDataService districtDataService = new General_DistrictDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<General_DistrictJson>();
                    var entities = districtDataService.GetAll(out error);
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
        public HttpResult<General_DistrictJson> GetDistrictById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<General_DistrictJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.District.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.District.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.District.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            try
            {
                using (General_DistrictDataService districtDataService = new General_DistrictDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new General_DistrictJson();
                    General_District entity;
                    if (id <= 0)
                    {
                        entity = General_District.GetNew();
                    }
                    else
                    {
                        entity = districtDataService.GetById(id);
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
        private HttpResult<General_DistrictJson> GetDistrictByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<General_DistrictJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.District.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.District.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.District.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (General_DistrictDataService districtDataService = new General_DistrictDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new General_DistrictJson();
                    General_District entity;
                    if (id <= 0)
                    {
                        entity = General_District.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("General_District");
                        //includeTables.Add("General_District");

                        entity = districtDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<General_DistrictJson> GetDistrictListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<General_DistrictJson>();
            try
            {
                //General_DistrictDataService districtDataService =
                //    new General_DistrictDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List

                result.DataExtra.StatusEnumList = EnumUtil.GetEnumList(typeof(General_District.StatusEnum));
                //DropDown Option Tables

                //result.DataExtra.CountryList = DbInstance.General_Country.AsEnumerable().Select(t => new
                //{ Id = t.Id, Name = t.Name }).ToList();
                //result.DataExtra.DivisionList = DbInstance.General_Division.AsEnumerable().Select(t => new
                //{ Id = t.Id.ToString(), Name = t.Name }).ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<General_DistrictJson> GetDistrictDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<General_DistrictJson>();
            try
            {
                General_DistrictDataService districtDataService =
                   new General_DistrictDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.StatusEnumList = EnumUtil.GetEnumList(typeof(General_District.StatusEnum));
                //DropDown Option Tables
                 
                result.DataExtra.CountryList = DbInstance.General_Country.AsEnumerable().Select(t => new
                { Id = t.Id,Name = t.Name }).ToList();
                result.DataExtra.DivisionList = DbInstance.General_Division.AsEnumerable().Select(t => new
                { Id = t.Id.ToString(),Name = t.Name }).ToList();
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
        public HttpResult<General_DistrictJson> SaveDistrict(General_DistrictJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<General_DistrictJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.District.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.District.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                 var entityReceived = new General_District();
                 var dbAttachedEntity = new General_District();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {
                    if (SaveDistrictLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<General_DistrictJson> SaveDistrict2(General_DistrictJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<General_DistrictJson>();
            try
            {
                using (General_DistrictDataService districtDataService = new General_DistrictDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new General_District();
                    var dbAttachedEntity = new General_District();
                    json.Map(entityReceived);
                    if (districtDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<General_DistrictJson> SaveDistrictList(IList<General_DistrictJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<General_DistrictJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.District.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.District.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (General_DistrictDataService districtDataService = new General_DistrictDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<General_District>();
                    var dbAttachedListEntity = new List<General_District>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (districtDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<General_DistrictJson> GetDeleteDistrictById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<General_DistrictJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.District.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (General_DistrictDataService districtDataService = new General_DistrictDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!districtDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveDistrict(General_District newObj, out string error)
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
            if (newObj.DivisionId <= 0)
            {
                error += "Please select valid Division.";
                return false;
            }
            if (newObj.StatusEnumId == null)
            {
                error += "Please select valid Status.";
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
            //TODO write your custom validation here.
            //var res =  DbInstance.General_District.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
            //error = "A District already exists!";
            //return false;
            //}
            return true;
        }
        private bool SaveDistrictLogic(General_District newObj, ref General_District dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "District to save can't be null!";
                return false;
            }

            if (!IsValidToSaveDistrict(newObj, out error))
                return false;

            bool isNewObject = true;
            General_District objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.General_District.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = General_District.GetNew(newObj.Id);
                DbInstance.General_District.Add(objToSave);
                objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
                objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
          
            if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.District.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.District.CanEdit,
                   out error))
            {
                return false;
            }

            //binding object  
            objToSave.Name = newObj.Name;
            objToSave.CountryId = newObj.CountryId;
            objToSave.DivisionId = newObj.DivisionId;
            objToSave.StatusEnumId = newObj.StatusEnumId;
            objToSave.OrderNo = newObj.OrderNo;
            objToSave.IsDeleted = newObj.IsDeleted;
            objToSave.IsActive = newObj.IsActive;
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

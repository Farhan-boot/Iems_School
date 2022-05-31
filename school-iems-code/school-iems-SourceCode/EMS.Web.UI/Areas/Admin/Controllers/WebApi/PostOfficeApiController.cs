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

    public class PostOfficeApiController : EmployeeBaseApiController
	{
        public PostOfficeApiController()
        {  }
       
        #region PostOffice 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<General_PostOfficeJson> GetPagedPostOffice(string textkey, int? pageSize, int? pageNo
           ,Int32 districtId= 0      
           ,Int32 policeStationId= 0      
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<General_PostOfficeJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.PostOffice.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.PostOffice.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.PostOffice.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            try
            {
                using (General_PostOfficeDataService postOfficeDataService = new General_PostOfficeDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<General_PostOffice> query = DbInstance.General_PostOffice.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Name.ToLower().Contains(textkey.ToLower()));
                    }
                    if (districtId>0)
                    {
                        query = query.Where(q => q.DistrictId== districtId);
                    }  
                    if (policeStationId>0)
                    {
                        query = query.Where(q => q.PoliceStationId== policeStationId);
                    }  
                 
                    var entities = postOfficeDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<General_PostOfficeJson>();
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
        private HttpListResult<General_PostOfficeJson> GetAllPostOffice()
        {
            string error = string.Empty;
            var result = new HttpListResult<General_PostOfficeJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.PostOffice.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.PostOffice.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.PostOffice.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            try
            {
                using (General_PostOfficeDataService postOfficeDataService = new General_PostOfficeDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<General_PostOfficeJson>();
                    var entities = postOfficeDataService.GetAll(out error);
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
        public HttpResult<General_PostOfficeJson> GetPostOfficeById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<General_PostOfficeJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.PostOffice.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.PostOffice.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.PostOffice.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            try
            {
                using (General_PostOfficeDataService postOfficeDataService = new General_PostOfficeDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new General_PostOfficeJson();
                    General_PostOffice entity;
                    if (id <= 0)
                    {
                        entity = General_PostOffice.GetNew();
                    }
                    else
                    {
                        entity = postOfficeDataService.GetById(id);
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
        private HttpResult<General_PostOfficeJson> GetPostOfficeByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<General_PostOfficeJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.PostOffice.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.PostOffice.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.PostOffice.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            try
            {
                using (General_PostOfficeDataService postOfficeDataService = new General_PostOfficeDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new General_PostOfficeJson();
                    General_PostOffice entity;
                    if (id <= 0)
                    {
                        entity = General_PostOffice.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("General_PostOffice");
                        //includeTables.Add("General_PostOffice");

                        entity = postOfficeDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<General_PostOfficeJson> GetPostOfficeListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<General_PostOfficeJson>();
            try
            {
                //General_PostOfficeDataService postOfficeDataService =
                //    new General_PostOfficeDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List

                //result.DataExtra.StatusEnumList = EnumUtil.GetEnumList(typeof(General_PostOffice.StatusEnum));
                //DropDown Option Tables

                result.DataExtra.DistrictList = DbInstance.General_District.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.Name }).ToList();
                result.DataExtra.PoliceStationList = DbInstance.General_PoliceStation.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.Name }).ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<General_PostOfficeJson> GetPostOfficeDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<General_PostOfficeJson>();
            try
            {
                General_PostOfficeDataService postOfficeDataService =
                new General_PostOfficeDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                //result.DataExtra.StatusEnumList = EnumUtil.GetEnumList(typeof(General_PostOffice.StatusEnum));
                //DropDown Option Tables
                 
                result.DataExtra.DistrictList = DbInstance.General_District.AsEnumerable().Select(t => new
                { Id = t.Id,Name = t.Name }).ToList();
                result.DataExtra.PoliceStationList = DbInstance.General_PoliceStation.AsEnumerable().Select(t => new
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
        public HttpResult<General_PostOfficeJson> SavePostOffice(General_PostOfficeJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<General_PostOfficeJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.PostOffice.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.PostOffice.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            try
            {
                 var entityReceived = new General_PostOffice();
                 var dbAttachedEntity = new General_PostOffice();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {
                    if (SavePostOfficeLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<General_PostOfficeJson> SavePostOffice2(General_PostOfficeJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<General_PostOfficeJson>();
            try
            {
                using (General_PostOfficeDataService postOfficeDataService = new General_PostOfficeDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new General_PostOffice();
                    var dbAttachedEntity = new General_PostOffice();
                    json.Map(entityReceived);
                    if (postOfficeDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<General_PostOfficeJson> SavePostOfficeList(IList<General_PostOfficeJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<General_PostOfficeJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.PostOffice.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.PostOffice.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            try
            {
                using (General_PostOfficeDataService postOfficeDataService = new General_PostOfficeDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<General_PostOffice>();
                    var dbAttachedListEntity = new List<General_PostOffice>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (postOfficeDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<General_PostOfficeJson> GetDeletePostOfficeById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<General_PostOfficeJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.PostOffice.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }


            try
            {
                using (General_PostOfficeDataService postOfficeDataService = new General_PostOfficeDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!postOfficeDataService.DeleteById(id, out error))
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
        private bool IsValidToSavePostOffice(General_PostOffice newObj, out string error)
        {
            error = "";
            if (!newObj.Name.IsValid())
            {
                error += "Name is not valid.";
                return false;
            }
            if (newObj.Name.Length > 256)
            {
                error += "Name exceeded its maximum length 256.";
                return false;
            }
            if (!newObj.PostCode.IsValid())
            {
                error += "Post Code is not valid.";
                return false;
            }
            if (newObj.PostCode.Length > 10)
            {
                error += "Post Code exceeded its maximum length 10.";
                return false;
            }
            if (newObj.PoliceStationId <= 0)
            {
                error += "Please select valid Police Station.";
                return false;
            }
            if (newObj.DistrictId <= 0)
            {
                error += "Please select valid District.";
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
            //var res =  DbInstance.General_PostOffice.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
            //error = "A PostOffice already exists!";
            //return false;
            //}
            return true;
        }
        private bool SavePostOfficeLogic(General_PostOffice newObj, ref General_PostOffice dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Post Office to save can't be null!";
                return false;
            }

            if (!IsValidToSavePostOffice(newObj, out error))
                return false;

            bool isNewObject = true;
            General_PostOffice objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.General_PostOffice.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = General_PostOffice.GetNew(newObj.Id);
                DbInstance.General_PostOffice.Add(objToSave);
                objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
                objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }

            if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.PostOffice.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.PostOffice.CanEdit,
                   out error))
            {
                return false;
            }

            //binding object  
            objToSave.Name = newObj.Name;
            objToSave.PostCode = newObj.PostCode;
            objToSave.PoliceStationId = newObj.PoliceStationId;
            objToSave.DistrictId = newObj.DistrictId;
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

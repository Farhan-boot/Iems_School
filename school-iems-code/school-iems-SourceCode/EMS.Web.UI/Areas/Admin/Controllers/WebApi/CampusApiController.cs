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
using EMS.Framework.Objects;
using EMS.Framework.Permissions;
//using EMS.Web.Jsons.Mapper;
using EMS.Web.Jsons.Models;


namespace EMS.Web.UI.Areas.Admin.Controllers.WebApi
{

    public class CampusApiController : EmployeeBaseApiController
	{
        public CampusApiController()
        {  }
       
        #region Campus 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<General_CampusJson> GetPagedCampus(string textkey, int? pageSize, int? pageNo
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<General_CampusJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Campus.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Campus.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Campus.CanEdit, out error)
            )
            {
                result.HasError = true;
                result.Errors.Add(error);
                result.DataExtra.IsPermited = false;
                return result;
            }

            try
            {
                using (General_CampusDataService campusDataService = new General_CampusDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<General_Campus> query = DbInstance.General_Campus.OrderBy(x => x.OrderNo);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Name.ToLower().Contains(textkey.ToLower()));
                    }
                 
                    var entities = campusDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<General_CampusJson>();
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
        private HttpListResult<General_CampusJson> GetAllCampus()
        {
            string error = string.Empty;
            var result = new HttpListResult<General_CampusJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Campus.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Campus.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Campus.CanEdit, out error)
            )
            {
                result.HasError = true;
                result.Errors.Add(error);
                result.DataExtra.IsPermited = false;
                return result;
            }
            try
            {
                using (General_CampusDataService campusDataService = new General_CampusDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<General_CampusJson>();
                    var entities = campusDataService.GetAll(out error);
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
        public HttpResult<General_CampusJson> GetCampusById(int id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<General_CampusJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Campus.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Campus.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Campus.CanEdit, out error)
            )
            {
                result.HasError = true;
                result.Errors.Add(error);
                result.DataExtra.IsPermited = false;
                return result;
            }
            try
            {
                using (General_CampusDataService campusDataService = new General_CampusDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new General_CampusJson();
                    General_Campus entity;
                    if (id <= 0)
                    {
                        entity = General_Campus.GetNew();
                    }
                    else
                    {
                        entity = campusDataService.GetById(id);
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
        private HttpResult<General_CampusJson> GetCampusByIdWithChild(int id)
        {
            string error = string.Empty;
            var result = new HttpResult<General_CampusJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Campus.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Campus.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Campus.CanEdit, out error)
            )
            {
                result.HasError = true;
                result.Errors.Add(error);
                result.DataExtra.IsPermited = false;
                return result;
            }
            try
            {
                using (General_CampusDataService campusDataService = new General_CampusDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new General_CampusJson();
                    General_Campus entity;
                    if (id <= 0)
                    {
                        entity = General_Campus.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("General_Campus");
                        //includeTables.Add("General_Campus");

                        entity = campusDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<General_CampusJson> GetCampusListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<General_CampusJson>();
            try
            {
                //General_CampusDataService campusDataService =
                //    new General_CampusDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.TypeEnumList = EnumUtil.GetEnumList(typeof(General_Campus.TypeEnum));
                result.DataExtra.StatusEnumList = EnumUtil.GetEnumList(typeof(General_Campus.StatusEnum));
                //DropDown Option Tables
                 
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<General_CampusJson> GetCampusDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<General_CampusJson>();
            try
            {
                //General_CampusDataService campusDataService =
                //    new General_CampusDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.TypeEnumList = EnumUtil.GetEnumList(typeof(General_Campus.TypeEnum));
                result.DataExtra.StatusEnumList = EnumUtil.GetEnumList(typeof(General_Campus.StatusEnum));
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
        public HttpResult<General_CampusJson> SaveCampus(General_CampusJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<General_CampusJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Campus.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Campus.CanEdit, out error)
            )
            {
                result.HasError = true;
                result.Errors.Add(error);
                result.DataExtra.IsPermited = false;
                return result;
            }
            try
            {
                 var entityReceived = new General_Campus();
                 var dbAttachedEntity = new General_Campus();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {
                    if (SaveCampusLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<General_CampusJson> SaveCampus2(General_CampusJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<General_CampusJson>();
            try
            {
                using (General_CampusDataService campusDataService = new General_CampusDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new General_Campus();
                    var dbAttachedEntity = new General_Campus();
                    json.Map(entityReceived);
                    if (campusDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<General_CampusJson> SaveCampusList(IList<General_CampusJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<General_CampusJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Campus.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Campus.CanEdit, out error)
            )
            {
                result.HasError = true;
                result.Errors.Add(error);
                result.DataExtra.IsPermited = false;
                return result;
            }
            try
            {
                using (General_CampusDataService campusDataService = new General_CampusDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<General_Campus>();
                    var dbAttachedListEntity = new List<General_Campus>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (campusDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<General_CampusJson> GetDeleteCampusById(int id)
        {
            string error = string.Empty;
            var result = new HttpResult<General_CampusJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Campus.CanDelete, out error)
              )
            {
                result.HasError = true;
                result.Errors.Add(error);
                result.DataExtra.IsPermited = false;
                return result;
            }
            try
            {
                using (General_CampusDataService campusDataService = new General_CampusDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!campusDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveCampus(General_Campus newObj, out string error)
        {
            error = "";
            if (!newObj.Name.IsValid())
            {
                error += "Name is not valid.";
                return false;
            }
            if (newObj.Name.Length>150)
            {
                error += "Name exceeded its maximum length 150.";
                return false;
            }
            if (!newObj.ShortName.IsValid())
            {
                error += "Short Name is not valid.";
                return false;
            }
            if (newObj.ShortName.Length>50)
            {
                error += "Short Name exceeded its maximum length 50.";
                return false;
            }
            if (newObj.OrderNo==null)
            {
                error += "Order No required.";
                return false;
            }
            if (newObj.Address.IsValid() && newObj.Address.Length>256)
            {
                error += "Address exceeded its maximum length 256.";
                return false;
            }

            if (newObj.TypeEnumId==null)
            {
                error += "Please select valid Type.";
                return false;
            }
            if (newObj.StatusEnumId==null)
            {
                error += "Please select valid Status.";
                return false;
            }
            if (newObj.Remarks.IsValid() && newObj.Remarks.Length>500)
            {
                error += "Remarks exceeded its maximum length 500.";
                return false;
            }
            if (newObj.IsDeleted==null)
            {
                error += "Is Deleted required.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.General_Campus.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A Campus already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveCampusLogic(General_Campus newObj,ref General_Campus dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Campus to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveCampus(newObj, out error))
                return false;

            bool isNewObject = true;
            General_Campus objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.General_Campus.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            //else
            //{    
            //    newObj.Id = BigInt.NewBigInt();
            //}
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = General_Campus.GetNew(newObj.Id);
                DbInstance.General_Campus.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //checking permission, enable it with correction
            if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Campus.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Campus.CanEdit,
                   out error))
            {
                return false;
            }

            //binding object  
            objToSave.Name =  newObj.Name ;
           objToSave.ShortName =  newObj.ShortName ;
           objToSave.OrderNo =  newObj.OrderNo ;
           objToSave.Address =  newObj.Address ;
           objToSave.MapEmbedCode =  newObj.MapEmbedCode ;
           objToSave.TypeEnumId =  newObj.TypeEnumId ;
           objToSave.StatusEnumId =  newObj.StatusEnumId ;
           objToSave.Remarks =  newObj.Remarks ;
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

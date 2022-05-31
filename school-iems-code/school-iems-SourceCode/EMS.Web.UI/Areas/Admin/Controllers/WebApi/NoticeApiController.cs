//File: UI Controller
using System;
using System.Collections.Generic;
using System.Data;
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

    public class NoticeApiController : EmployeeBaseApiController
	{
        public NoticeApiController()
        {  }
       
        #region Notice 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<General_NoticeJson> GetPagedNotice(string textkey, int? pageSize, int? pageNo
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<General_NoticeJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Notice.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Notice.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Notice.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/

            try
            {
                using (General_NoticeDataService noticeDataService = new General_NoticeDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<General_Notice> query = DbInstance.General_Notice.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Title.ToLower().Contains(textkey.ToLower()));
                    }
                 
                    var entities = noticeDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<General_NoticeJson>();
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
        private HttpListResult<General_NoticeJson> GetAllNotice()
        {
            string error = string.Empty;
            var result = new HttpListResult<General_NoticeJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Notice.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Notice.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Notice.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (General_NoticeDataService noticeDataService = new General_NoticeDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<General_NoticeJson>();
                    var entities = noticeDataService.GetAll(out error);
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
        public HttpResult<General_NoticeJson> GetNoticeById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<General_NoticeJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Notice.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Notice.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Notice.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (General_NoticeDataService noticeDataService = new General_NoticeDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new General_NoticeJson();
                    General_Notice entity;
                    if (id <= 0)
                    {
                        entity = General_Notice.GetNew();
                    }
                    else
                    {
                        entity = noticeDataService.GetById(id);
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
        private HttpResult<General_NoticeJson> GetNoticeByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<General_NoticeJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Notice.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Notice.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Notice.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (General_NoticeDataService noticeDataService = new General_NoticeDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new General_NoticeJson();
                    General_Notice entity;
                    if (id <= 0)
                    {
                        entity = General_Notice.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("General_Notice");
                        //includeTables.Add("General_Notice");

                        entity = noticeDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<General_NoticeJson> GetNoticeListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<General_NoticeJson>();
            try
            {
                //General_NoticeDataService noticeDataService =
                //    new General_NoticeDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.VisibilityTypeEnumList = EnumUtil.GetEnumList(typeof(General_Notice.VisibilityTypeEnum));
                //DropDown Option Tables
                 
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<General_NoticeJson> GetNoticeDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<General_NoticeJson>();
            try
            {
                //General_NoticeDataService noticeDataService =
                //    new General_NoticeDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.VisibilityTypeEnumList = EnumUtil.GetEnumList(typeof(General_Notice.VisibilityTypeEnum));
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
        public HttpResult<General_NoticeJson> SaveNotice(General_NoticeJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<General_NoticeJson>();
            //optional permission, check permission in the SaveNoticeLogic insted
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Notice.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Notice.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                 var entityReceived = new General_Notice();
                 var dbAttachedEntity = new General_Notice();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SaveNoticeLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<General_NoticeJson> SaveNotice2(General_NoticeJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<General_NoticeJson>();
            try
            {
                using (General_NoticeDataService noticeDataService = new General_NoticeDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new General_Notice();
                    var dbAttachedEntity = new General_Notice();
                    json.Map(entityReceived);
                    if (noticeDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<General_NoticeJson> SaveNoticeList(IList<General_NoticeJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<General_NoticeJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Notice.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Notice.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (General_NoticeDataService noticeDataService = new General_NoticeDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<General_Notice>();
                    var dbAttachedListEntity = new List<General_Notice>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (noticeDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<General_NoticeJson> GetDeleteNoticeById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<General_NoticeJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Notice.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (General_NoticeDataService noticeDataService = new General_NoticeDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!noticeDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveNotice(General_Notice newObj, out string error)
        {
            error = "";
            if (!newObj.Title.IsValid())
            {
                error += "Title is not valid.";
                return false;
            }
            if (newObj.Title.Length>200)
            {
                error += "Title exceeded its maximum length 200.";
                return false;
            }


            if (newObj.OrderNo==null)
            {
                error += "Order No required.";
                return false;
            }
            if (newObj.VisibilityTypeEnumId==null)
            {
                error += "Please select valid Visibility Type.";
                return false;
            }
            if (!newObj.PublishDate.IsValid())
            {
                error += "Publish Date is not valid.";
                return false;
            }
            if (!newObj.ExpiryDate.IsValid())
            {
                error += "Expiry Date is not valid.";
                return false;
            }
            if (newObj.IsDeleted==null)
            {
                error += "Is Deleted required.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.General_Notice.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A Notice already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveNoticeLogic(General_Notice newObj,ref General_Notice dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Notice to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveNotice(newObj, out error))
                return false;

            bool isNewObject = true;
            General_Notice objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.General_Notice.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = General_Notice.GetNew(newObj.Id);
                DbInstance.General_Notice.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Notice.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Notice.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
           objToSave.Title =  newObj.Title ;
           objToSave.Description =  newObj.Description ;
           objToSave.FilePath =  newObj.FilePath ;
           objToSave.OrderNo =  newObj.OrderNo ;
           objToSave.VisibilityTypeEnumId =  newObj.VisibilityTypeEnumId ;
           objToSave.PublishDate =  newObj.PublishDate ;
           objToSave.ExpiryDate =  newObj.ExpiryDate ;
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

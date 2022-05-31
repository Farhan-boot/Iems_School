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


namespace EMS.Web.UI.Areas.Academic.Controllers.WebApi
{

    public class OnlineClassApiController : EmployeeBaseApiController
	{
        public OnlineClassApiController()
        {  }
       
        #region OnlineClass 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Aca_OnlineClassJson> GetPagedOnlineClass(string textkey, int? pageSize, int? pageNo
           ,Int32 classShiftSectionMapId= 0      
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Aca_OnlineClassJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.OnlineClass.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.OnlineClass.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.OnlineClass.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/

            try
            {
                using (Aca_OnlineClassDataService onlineClassDataService = new Aca_OnlineClassDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<Aca_OnlineClass> query = DbInstance.Aca_OnlineClass.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Name.ToLower().Contains(textkey.ToLower()));
                    }
                    if (classShiftSectionMapId>0)
                    {
                        query = query.Where(q => q.ProgramId== classShiftSectionMapId);
                    }  
                 
                    var entities = onlineClassDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Aca_OnlineClassJson>();
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
        private HttpListResult<Aca_OnlineClassJson> GetAllOnlineClass()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_OnlineClassJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.OnlineClass.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.OnlineClass.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.OnlineClass.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Aca_OnlineClassDataService onlineClassDataService = new Aca_OnlineClassDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<Aca_OnlineClassJson>();
                    var entities = onlineClassDataService.GetAll(out error);
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
        public HttpResult<Aca_OnlineClassJson> GetOnlineClassById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_OnlineClassJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.OnlineClass.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.OnlineClass.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.OnlineClass.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Aca_OnlineClassDataService onlineClassDataService = new Aca_OnlineClassDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Aca_OnlineClassJson();
                    Aca_OnlineClass entity;
                    if (id <= 0)
                    {
                        entity = Aca_OnlineClass.GetNew();
                    }
                    else
                    {
                        entity = onlineClassDataService.GetById(id);
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
        private HttpResult<Aca_OnlineClassJson> GetOnlineClassByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_OnlineClassJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.OnlineClass.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.OnlineClass.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.OnlineClass.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Aca_OnlineClassDataService onlineClassDataService = new Aca_OnlineClassDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Aca_OnlineClassJson();
                    Aca_OnlineClass entity;
                    if (id <= 0)
                    {
                        entity = Aca_OnlineClass.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("Aca_OnlineClass");
                        //includeTables.Add("Aca_OnlineClass");

                        entity = onlineClassDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Aca_OnlineClassJson> GetOnlineClassListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_OnlineClassJson>();
            try
            {
                //Aca_OnlineClassDataService onlineClassDataService =
                //    new Aca_OnlineClassDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.LiveClassMediumEnumList = EnumUtil.GetEnumList(typeof(Aca_OnlineClass.ClassMediaEnum));
                //DropDown Option Tables
                 
                //result.DataExtra.ClassShiftSectionMapList = DbInstance.Aca_ClassShiftSectionMap.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<Aca_OnlineClassJson> GetOnlineClassDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_OnlineClassJson>();
            try
            {
                //Aca_OnlineClassDataService onlineClassDataService =
                //    new Aca_OnlineClassDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.LiveClassMediumEnumList = EnumUtil.GetEnumList(typeof(Aca_OnlineClass.ClassMediaEnum));
                //DropDown Option Tables
                 
                //result.DataExtra.ClassShiftSectionMapList = DbInstance.Aca_ClassShiftSectionMap.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
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
        public HttpResult<Aca_OnlineClassJson> SaveOnlineClass(Aca_OnlineClassJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_OnlineClassJson>();
            //optional permission, check permission in the SaveOnlineClassLogic insted
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.OnlineClass.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.OnlineClass.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                 var entityReceived = new Aca_OnlineClass();
                 var dbAttachedEntity = new Aca_OnlineClass();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SaveOnlineClassLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<Aca_OnlineClassJson> SaveOnlineClass2(Aca_OnlineClassJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_OnlineClassJson>();
            try
            {
                using (Aca_OnlineClassDataService onlineClassDataService = new Aca_OnlineClassDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new Aca_OnlineClass();
                    var dbAttachedEntity = new Aca_OnlineClass();
                    json.Map(entityReceived);
                    if (onlineClassDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<Aca_OnlineClassJson> SaveOnlineClassList(IList<Aca_OnlineClassJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_OnlineClassJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.OnlineClass.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.OnlineClass.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Aca_OnlineClassDataService onlineClassDataService = new Aca_OnlineClassDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<Aca_OnlineClass>();
                    var dbAttachedListEntity = new List<Aca_OnlineClass>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (onlineClassDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<Aca_OnlineClassJson> GetDeleteOnlineClassById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_OnlineClassJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.OnlineClass.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Aca_OnlineClassDataService onlineClassDataService = new Aca_OnlineClassDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!onlineClassDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveOnlineClass(Aca_OnlineClass newObj, out string error)
        {
            error = "";
            if (!newObj.Name.IsValid())
            {
                error += "Class Title is not valid.";
                return false;
            }
            if (newObj.Name.Length>100)
            {
                error += "Class Title exceeded its maximum length 100.";
                return false;
            }
            if (newObj.ClassDuration==null)
            {
                error += "Class Duration required.";
                return false;
            }
            if (newObj.ClassMediaEnumId==null)
            {
                error += "Please select valid Live Class Medium.";
                return false;
            }
            if (newObj.ProgramId<=0)
            {
                error += "Please select valid Class Shift Section Map.";
                return false;
            }
            if (!newObj.ClassUrl.IsValid())
            {
                error += "Class Url is not valid.";
                return false;
            }
            if (newObj.ClassUrl==null)
            {
                error += "Class Url required.";
                return false;
            }
            if (newObj.IsDeleted==null)
            {
                error += "Is Deleted required.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.Aca_OnlineClass.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A OnlineClass already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveOnlineClassLogic(Aca_OnlineClass newObj,ref Aca_OnlineClass dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Online Class to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveOnlineClass(newObj, out error))
                return false;

            bool isNewObject = true;
            Aca_OnlineClass objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Aca_OnlineClass.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Aca_OnlineClass.GetNew(newObj.Id);
                DbInstance.Aca_OnlineClass.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.OnlineClass.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.OnlineClass.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
           objToSave.Name =  newObj.Name ;
           objToSave.ClassDuration =  newObj.ClassDuration ;
           objToSave.ClassMediaEnumId =  newObj.ClassMediaEnumId ;
           objToSave.ProgramId =  newObj.ProgramId;
           objToSave.ClassUrl =  newObj.ClassUrl ;
           objToSave.Remark =  newObj.Remark ;
           objToSave.History =  newObj.History ;
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

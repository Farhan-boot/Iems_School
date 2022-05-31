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

    public class RelationshipApiController : EmployeeBaseApiController
	{
        public RelationshipApiController()
        {  }
       
        #region Relationship 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<User_RelationshipJson> GetPagedRelationship(string textkey, int? pageSize, int? pageNo
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<User_RelationshipJson>();
            //todo fix the permission
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Relationship.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Relationship.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Relationship.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }

            try
            {
                using (User_RelationshipDataService relationshipDataService = new User_RelationshipDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<User_Relationship> query = DbInstance.User_Relationship.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Name.ToLower().Contains(textkey.ToLower()));
                    }
                 
                    var entities = relationshipDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<User_RelationshipJson>();
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
        private HttpListResult<User_RelationshipJson> GetAllRelationship()
        {
            string error = string.Empty;
            var result = new HttpListResult<User_RelationshipJson>();
            //todo fix the permission
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Relationship.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Relationship.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Relationship.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (User_RelationshipDataService relationshipDataService = new User_RelationshipDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<User_RelationshipJson>();
                    var entities = relationshipDataService.GetAll(out error);
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
        public HttpResult<User_RelationshipJson> GetRelationshipById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<User_RelationshipJson>();
            //todo fix the permission
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Relationship.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Relationship.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Relationship.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (User_RelationshipDataService relationshipDataService = new User_RelationshipDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new User_RelationshipJson();
                    User_Relationship entity;
                    if (id <= 0)
                    {
                        entity = User_Relationship.GetNew();
                    }
                    else
                    {
                        entity = relationshipDataService.GetById(id);
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
        private HttpResult<User_RelationshipJson> GetRelationshipByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<User_RelationshipJson>();
            //todo fix the permission
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Relationship.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Relationship.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Relationship.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (User_RelationshipDataService relationshipDataService = new User_RelationshipDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new User_RelationshipJson();
                    User_Relationship entity;
                    if (id <= 0)
                    {
                        entity = User_Relationship.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("User_Relationship");
                        //includeTables.Add("User_Relationship");

                        entity = relationshipDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<User_RelationshipJson> GetRelationshipListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<User_RelationshipJson>();
            try
            {
                //User_RelationshipDataService relationshipDataService =
                //    new User_RelationshipDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.GenderEnumList = EnumUtil.GetEnumList(typeof(User_Account.GenderEnum));
                //DropDown Option Tables
                 
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<User_RelationshipJson> GetRelationshipDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<User_RelationshipJson>();
            try
            {
                //User_RelationshipDataService relationshipDataService =
                //    new User_RelationshipDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.GenderEnumList = EnumUtil.GetEnumList(typeof(User_Account.GenderEnum));
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
        public HttpResult<User_RelationshipJson> SaveRelationship(User_RelationshipJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<User_RelationshipJson>();
            //optional permission, check permission in the SaveRelationshipLogic insted
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Relationship.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Relationship.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                 var entityReceived = new User_Relationship();
                 var dbAttachedEntity = new User_Relationship();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {
                    if (SaveRelationshipLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<User_RelationshipJson> SaveRelationship2(User_RelationshipJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<User_RelationshipJson>();
            try
            {
                using (User_RelationshipDataService relationshipDataService = new User_RelationshipDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new User_Relationship();
                    var dbAttachedEntity = new User_Relationship();
                    json.Map(entityReceived);
                    if (relationshipDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<User_RelationshipJson> SaveRelationshipList(IList<User_RelationshipJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<User_RelationshipJson>();
            //todo enable it while you need the permission
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Relationship.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Relationship.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (User_RelationshipDataService relationshipDataService = new User_RelationshipDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<User_Relationship>();
                    var dbAttachedListEntity = new List<User_Relationship>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (relationshipDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<User_RelationshipJson> GetDeleteRelationshipById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<User_RelationshipJson>();
            //todo fix the permission
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Relationship.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (User_RelationshipDataService relationshipDataService = new User_RelationshipDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!relationshipDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveRelationship(User_Relationship newObj, out string error)
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
            if (newObj.GenderEnumId==null)
            {
                error += "Please select valid Gender.";
                return false;
            }
            if (newObj.OrderNo==null)
            {
                error += "Order No required.";
                return false;
            }
            if (newObj.IsActive==null)
            {
                error += "Is Active required.";
                return false;
            }
            if (newObj.IsDeleted==null)
            {
                error += "Is Deleted required.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.User_Relationship.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A Relationship already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveRelationshipLogic(User_Relationship newObj,ref User_Relationship dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Relationship to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveRelationship(newObj, out error))
                return false;

            bool isNewObject = true;
            User_Relationship objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.User_Relationship.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = User_Relationship.GetNew(newObj.Id);
                DbInstance.User_Relationship.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //todo mandatory fix checking permission, enable it with correction
            if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Relationship.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Relationship.CanEdit,
                   out error))
            {
                return false;
            }

            //binding object  
            objToSave.Name =  newObj.Name ;
           objToSave.GenderEnumId =  newObj.GenderEnumId ;
           objToSave.OrderNo =  newObj.OrderNo ;
           objToSave.IsActive =  newObj.IsActive ;
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

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
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases;
using EMS.Web.Framework.Bases.WebApi;
using EMS.Framework.Objects;
//using EMS.Web.Jsons.Mapper;
using EMS.Web.Jsons.Models;


namespace EMS.Web.UI.Areas.Academic.Controllers.WebApi
{

    public class GradingPolicyApiController : EmployeeBaseApiController
	{
        public GradingPolicyApiController()
        {  }
       
        #region GradingPolicy 
        #region List View Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Aca_GradingPolicyJson> GetPagedGradingPolicy(string textkey, int? pageSize, int? pageNo
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Aca_GradingPolicyJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.ExamArea.PolicyManager.GradingPolicy.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.ExamArea.PolicyManager.GradingPolicy.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.ExamArea.PolicyManager.GradingPolicy.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_GradingPolicyDataService gradingPolicyDataService = new Aca_GradingPolicyDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<Aca_GradingPolicy> query = DbInstance.Aca_GradingPolicy.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Name.ToLower().Contains(textkey.ToLower()));
                    }
                 
                    var entities = gradingPolicyDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Aca_GradingPolicyJson>();
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
        public HttpListResult<Aca_GradingPolicyJson> GetGradingPolicyListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_GradingPolicyJson>();
            try
            {
                //Aca_GradingPolicyDataService gradingPolicyDataService =
                //    new Aca_GradingPolicyDataService(DbInstance, HttpUtil.Profile);
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
        /// <summary>
        /// Caution: Very Dangerous to call When table have huge data!
        /// </summary>
        /// <returns></returns>
        private HttpListResult<Aca_GradingPolicyJson> GetAllGradingPolicy()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_GradingPolicyJson>();
            try
            {
                using (Aca_GradingPolicyDataService gradingPolicyDataService = new Aca_GradingPolicyDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<Aca_GradingPolicyJson>();
                    var entities = gradingPolicyDataService.GetAll(out error);
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
        /// <summary>
        /// Todo pending to implement bulk save
        /// </summary>
        /// <param name="jsonList"></param>
        /// <returns></returns>
        [HttpPost]
        private HttpListResult<Aca_GradingPolicyJson> SaveGradingPolicyList(IList<Aca_GradingPolicyJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_GradingPolicyJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.ExamArea.PolicyManager.GradingPolicy.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.ExamArea.PolicyManager.GradingPolicy.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_GradingPolicyDataService gradingPolicyDataService = new Aca_GradingPolicyDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<Aca_GradingPolicy>();
                    var dbAttachedListEntity = new List<Aca_GradingPolicy>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (gradingPolicyDataService.Save(entity, ref dbAttachedListEntity, out error))
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

        #endregion

        #region Add/Edit View Api
        #region Get
        public HttpResult<Aca_GradingPolicyJson> GetGradingPolicyById(int id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_GradingPolicyJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.ExamArea.PolicyManager.GradingPolicy.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.ExamArea.PolicyManager.GradingPolicy.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.ExamArea.PolicyManager.GradingPolicy.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_GradingPolicyDataService gradingPolicyDataService = new Aca_GradingPolicyDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Aca_GradingPolicyJson();
                    Aca_GradingPolicy entity;
                    if (id <= 0)
                    {
                        entity = Aca_GradingPolicy.GetNew();
                    }
                    else
                    {
                        entity = gradingPolicyDataService.GetById(id);
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
        private HttpResult<Aca_GradingPolicyJson> GetGradingPolicyByIdWithChild(int id)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_GradingPolicyJson>();
            try
            {
                using (Aca_GradingPolicyDataService gradingPolicyDataService = new Aca_GradingPolicyDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Aca_GradingPolicyJson();
                    Aca_GradingPolicy entity;
                    if (id <= 0)
                    {
                        entity = Aca_GradingPolicy.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("Aca_GradingPolicy");
                        //includeTables.Add("Aca_GradingPolicy");

                        entity = gradingPolicyDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Aca_GradingPolicyJson> GetGradingPolicyDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_GradingPolicyJson>();
            try
            {
                //Aca_GradingPolicyDataService gradingPolicyDataService =
                //    new Aca_GradingPolicyDataService(DbInstance, HttpUtil.Profile);
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
        public HttpResult<Aca_GradingPolicyJson> SaveGradingPolicy(Aca_GradingPolicyJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_GradingPolicyJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.ExamArea.PolicyManager.GradingPolicy.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.ExamArea.PolicyManager.GradingPolicy.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                 var entityReceived = new Aca_GradingPolicy();
                 var dbAttachedEntity = new Aca_GradingPolicy();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {
                    if (SaveGradingPolicyLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<Aca_GradingPolicyJson> SaveGradingPolicy2(Aca_GradingPolicyJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_GradingPolicyJson>();
            try
            {
                using (Aca_GradingPolicyDataService gradingPolicyDataService = new Aca_GradingPolicyDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new Aca_GradingPolicy();
                    var dbAttachedEntity = new Aca_GradingPolicy();
                    json.Map(entityReceived);
                    if (gradingPolicyDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        public HttpResult<Aca_GradingPolicyJson> GetDeleteGradingPolicyById(int id)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_GradingPolicyJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.ExamArea.PolicyManager.GradingPolicy.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_GradingPolicyDataService gradingPolicyDataService = new Aca_GradingPolicyDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!gradingPolicyDataService.DeleteById(id, out error))
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
        #endregion

        #region Local Method (should be always private)
        private bool IsValidToSaveGradingPolicy(Aca_GradingPolicy newObj, out string error)
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
            if (newObj.Description.IsValid() && newObj.Description.Length>256)
            {
                error += "Description exceeded its maximum length 256.";
                return false;
            }
            //if (newObj.IsDeleted==null)
            //{
            //    error += "Is Deleted required.";
            //    return false;
            //}
            //if (newObj.IsActive==null)
            //{
            //    error += "Is Active required.";
            //    return false;
            //}
            //TODO write your custom validation here.
            //var res =  DbInstance.Aca_GradingPolicy.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A GradingPolicy already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveGradingPolicyLogic(Aca_GradingPolicy newObj,ref Aca_GradingPolicy dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Grading Policy to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveGradingPolicy(newObj, out error))
                return false;

            bool isNewObject = true;
            Aca_GradingPolicy objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Aca_GradingPolicy.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
    
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Aca_GradingPolicy.GetNew();
                DbInstance.Aca_GradingPolicy.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //checking permission, enable it with correction
            if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.ExamArea.PolicyManager.GradingPolicy.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.ExamArea.PolicyManager.GradingPolicy.CanEdit,
                   out error))
            {
                return false;
            }

            //binding object  
            objToSave.Name =  newObj.Name ;
           objToSave.Description =  newObj.Description ;
           objToSave.IsDeleted =  newObj.IsDeleted ;
           objToSave.IsActive =  newObj.IsActive ;
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

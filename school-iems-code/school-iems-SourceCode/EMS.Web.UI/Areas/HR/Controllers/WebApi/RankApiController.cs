//File: UI Controller
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.Expressions;
using EMS.CoreLibrary;
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


namespace EMS.Web.UI.Areas.Admin.Controllers.WebApi
{

    public class RankApiController : EmployeeBaseApiController
	{
        public RankApiController()
        {  }
       
        #region general Webapi
        #region for List
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<User_RankJson> GetPagedRank(string textkey, int? pageSize, int? pageNo, int categoryEnumId=-1, int jobClassEnumId = -1, int academicLevelEnumId = -1)
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<User_RankJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.RankManager.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.RankManager.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.RankManager.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (User_RankDataService rankDataService = new User_RankDataService(DbInstance, HttpUtil.Profile))
                {
                    IQueryable<User_Rank> query = DbInstance.User_Rank
                        .OrderBy(x => x.CategoryEnumId)
                        .ThenBy(x => x.JobClassEnumId)
                        .ThenBy(x => x.Priority);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Name.ToLower().Contains(textkey.ToLower()));
                    }
                    if (categoryEnumId>=0)
                    {
                        query = query.Where(q => q.CategoryEnumId==(byte)categoryEnumId);
                    }
                    if (jobClassEnumId>=0)
                    {
                        query = query.Where(q => q.JobClassEnumId==(byte)jobClassEnumId);
                    }
                    if (academicLevelEnumId>=0)
                    {
                        query = query.Where(q => q.AcademicLevelEnumId==(byte)academicLevelEnumId);
                    }
                    var policyEntities = rankDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var policyJsons = new List<User_RankJson>();
                    policyEntities.Map(policyJsons);

                    result.Data = policyJsons;
                    result.Count = count;
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.ToString());
            }
            return result;
        }
        /// <summary>
        /// Caution: Very Dangerous to call When table have huge data!
        /// </summary>
        /// <returns></returns>
        private HttpListResult<User_RankJson> GetAllRank()
        {
            string error = string.Empty;
            var result = new HttpListResult<User_RankJson>();
            try
            {
                using (User_RankDataService rankDataService = new User_RankDataService(DbInstance, HttpUtil.Profile))
                {
                    var policyJsons = new List<User_RankJson>();
                    var policyEntities = rankDataService.GetAll(out error);
                    policyEntities.Map(policyJsons);
                    result.Data = policyJsons;
                    result.Count = policyJsons.Count;
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.ToString());
            }
            return result;
        }
        /// <summary>
        /// Todo pending to implement bulk save
        /// </summary>
        /// <param name="jsonList"></param>
        /// <returns></returns>
        [HttpPost]
        private HttpListResult<User_RankJson> SaveRankList(IList<User_RankJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<User_RankJson>();
            try
            {
                using (User_RankDataService rankDataService = new User_RankDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<User_Rank>();
                    var dbAttachedListEntity = new List<User_Rank>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (rankDataService.Save(entity, ref dbAttachedListEntity, out error))
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
                result.Errors.Add(ex.ToString());
            }
            return result;
        }

        #endregion

        #region for Single Row
        public HttpResult<User_RankJson> GetRankById(long id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<User_RankJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.RankManager.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.RankManager.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.RankManager.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (User_RankDataService rankDataService = new User_RankDataService(DbInstance, HttpUtil.Profile))
                {
                    var policyJson = new User_RankJson();
                    User_Rank policyEntity;
                    if (id <= 0)
                    {
                        policyEntity = User_Rank.GetNew();
                    }
                    else
                    {
                        policyEntity = rankDataService.GetById(id);
                    }
                    policyEntity.Map(policyJson);
                    result.Data = policyJson;

                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.ToString());
            }
            return result;
        }
        [HttpPost]
        public HttpResult<User_RankJson> SaveRank(User_RankJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<User_RankJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.RankManager.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.RankManager.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (User_RankDataService rankDataService = new User_RankDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new User_Rank();
                    var dbAttachedEntity = new User_Rank();
                    json.Map(entityReceived);
                    if (rankDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
                result.Errors.Add(ex.ToString());
            }
            return result;
        }
        public HttpResult<User_RankJson> GetDeleteRankById(long id)
        {
            string error = string.Empty;
            var result = new HttpResult<User_RankJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.RankManager.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (User_RankDataService rankDataService = new User_RankDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!rankDataService.DeleteById(id, out error))
                    {
                        result.HasError = true;
                        result.Errors.Add(error);
                    }
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.ToString());
            }
            return result;
        }
        #endregion
        #endregion

        #region Custom WebApi
        private HttpResult<User_RankJson> GetRankByIdWithChild(long id)
        {
            string error = string.Empty;
            var result = new HttpResult<User_RankJson>();
            try
            {
                using (User_RankDataService rankDataService = new User_RankDataService(DbInstance, HttpUtil.Profile))
                {
                    var policyJson = new User_RankJson();
                    User_Rank policyEntity;
                    if (id <= 0)
                    {
                        policyEntity = User_Rank.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("User_Rank");
                        //includeTables.Add("User_Rank");

                        policyEntity = rankDataService.GetById(id, includeTables.ToArray());
                    }
                    policyEntity.Map(policyJson);
                    result.Data = policyJson;
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.ToString());
            }
            return result;
        }
        public HttpListResult<User_RankJson> GetRankListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<User_RankJson>();
            try
            {
                //User_RankDataService rankDataService =
                //    new User_RankDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List

                result.DataExtra.CategoryEnumList = EnumUtil.GetEnumList(typeof(User_Account.UserCategoryEnum));
                result.DataExtra.JobClassEnumList = EnumUtil.GetEnumList(typeof(User_Employee.JobClassEnum));
                result.DataExtra.AcademicLevelEnumList = EnumUtil.GetEnumList(typeof(User_Rank.AcademicLevelEnum));
                //DropDown Option Tables

            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<User_RankJson> GetRankDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<User_RankJson>();
            try
            {
                //User_RankDataService rankDataService =
                //    new User_RankDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List

                result.DataExtra.CategoryEnumList = EnumUtil.GetEnumList(typeof(User_Account.UserCategoryEnum));
                result.DataExtra.JobClassEnumList = EnumUtil.GetEnumList(typeof(User_Employee.JobClassEnum));
                result.DataExtra.AcademicLevelEnumList = EnumUtil.GetEnumList(typeof(User_Rank.AcademicLevelEnum));
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

    }
}

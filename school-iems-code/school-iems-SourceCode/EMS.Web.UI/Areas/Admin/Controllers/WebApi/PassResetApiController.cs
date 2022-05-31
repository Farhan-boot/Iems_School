//File: UI Controller
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

    public class PassResetApiController : EmployeeBaseApiController
    {
        public PassResetApiController()
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
        public HttpListResult<UAC_PassResetJson> GetPagedPassReset(string textkey, int? pageSize, int? pageNo)
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<UAC_PassResetJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.UserCredential.AuditManager.CanViewPasswordChangeAudit, out error)
            )
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (UAC_PassResetDataService passResetDataService = new UAC_PassResetDataService(DbInstance, HttpUtil.Profile))
                {
                    IQueryable<UAC_PassReset> query = DbInstance.UAC_PassReset.OrderByDescending(x => x.Id);
                    //disallow super admin
                    if (!HttpUtil.IsSupperAdmin())
                    {
                        query = query.Where(x => x.UserId != HttpUtil.SuperAdminId );
                    }
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.FromIp.ToLower().Contains(textkey.ToLower()));
                    }
                    var policyEntities = passResetDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var policyJsons = new List<UAC_PassResetJson>();
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
        private HttpListResult<UAC_PassResetJson> GetAllPassReset()
        {
            string error = string.Empty;
            var result = new HttpListResult<UAC_PassResetJson>();
            try
            {
                using (UAC_PassResetDataService passResetDataService = new UAC_PassResetDataService(DbInstance, HttpUtil.Profile))
                {
                    var policyJsons = new List<UAC_PassResetJson>();
                    var policyEntities = passResetDataService.GetAll(out error);
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
        private HttpListResult<UAC_PassResetJson> SavePassResetList(IList<UAC_PassResetJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<UAC_PassResetJson>();
            try
            {
                using (UAC_PassResetDataService passResetDataService = new UAC_PassResetDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<UAC_PassReset>();
                    var dbAttachedListEntity = new List<UAC_PassReset>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (passResetDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<UAC_PassResetJson> GetPassResetById(long id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<UAC_PassResetJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.UserCredential.AuditManager.CanViewPasswordChangeAudit, out error)
            )
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (UAC_PassResetDataService passResetDataService = new UAC_PassResetDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new UAC_PassResetJson();
                    UAC_PassReset entity;
                    if (id <= 0)
                    {
                        entity = UAC_PassReset.GetNew();
                    }
                    else
                    {
                        entity = passResetDataService.GetById(id);
                    }
                    entity.Map(json);
                    result.Data = json;

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
        private HttpResult<UAC_PassResetJson> SavePassReset(UAC_PassResetJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<UAC_PassResetJson>();
            try
            {
                using (UAC_PassResetDataService passResetDataService = new UAC_PassResetDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new UAC_PassReset();
                    var dbAttachedEntity = new UAC_PassReset();
                    json.Map(entityReceived);
                    if (passResetDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<UAC_PassResetJson> GetDeletePassResetById(long id)
        {
            string error = string.Empty;
            var result = new HttpResult<UAC_PassResetJson>();
            try
            {
                using (UAC_PassResetDataService passResetDataService = new UAC_PassResetDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!passResetDataService.DeleteById(id, out error))
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
        private HttpResult<UAC_PassResetJson> GetPassResetByIdWithChild(long id)
        {
            string error = string.Empty;
            var result = new HttpResult<UAC_PassResetJson>();
            try
            {
                using (UAC_PassResetDataService passResetDataService = new UAC_PassResetDataService(DbInstance, HttpUtil.Profile))
                {
                    var policyJson = new UAC_PassResetJson();
                    UAC_PassReset policyEntity;
                    if (id <= 0)
                    {
                        policyEntity = UAC_PassReset.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("UAC_PassReset");
                        //includeTables.Add("UAC_PassReset");

                        policyEntity = passResetDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Aca_MarkDistributionPolicyBreakdownJson> GetpassResetDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_MarkDistributionPolicyBreakdownJson>();
            try
            {
                //UAC_PassResetDataService passResetDataService =
                //    new UAC_PassResetDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.TypeEnumList = EnumUtil.GetEnumList(typeof(UAC_PassReset.TypeEnum));
                result.DataExtra.StatusEnumList = EnumUtil.GetEnumList(typeof(UAC_PassReset.StatusEnum));
                //DropDown Option Tables
                 
                //result.DataExtra.AccountList = DbInstance.User_Account.AsEnumerable().Select(t => new
                //{ Id = t.Id.ToString(),Name = t.Name }).ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.ToString());
            }
            return result;

        }
        #endregion

	}
}

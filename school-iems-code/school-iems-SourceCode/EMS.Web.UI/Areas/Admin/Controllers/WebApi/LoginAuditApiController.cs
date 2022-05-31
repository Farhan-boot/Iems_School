//File: UI Controller
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.CoreLibrary;
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

    public class LoginAuditApiController : EmployeeBaseApiController
    {
        public LoginAuditApiController()
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
        public HttpListResult<UAC_LoginAuditJson> GetPagedLoginAudit(string textkey, string name, int? pageSize, int? pageNo)
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<UAC_LoginAuditJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.UserCredential.AuditManager.CanViewLoginAudit, out error)
                )
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (UAC_LoginAuditDataService loginAuditDataService = new UAC_LoginAuditDataService(DbInstance, HttpUtil.Profile))
                {
                    IQueryable<UAC_LoginAudit> query = DbInstance.UAC_LoginAudit

                        .OrderByDescending(x => x.Id);

                    //disallow super admin
                    if (!HttpUtil.IsSupperAdmin())
                    {
                        query = query.Where(x => x.UserId != HttpUtil.SuperAdminId || !x.UserName.Equals(HttpUtil.SuperAdminUserName, StringComparison.InvariantCultureIgnoreCase));
                    }

                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.UserName.ToLower().Contains(textkey.ToLower()));
                    }
                    if (name.IsValid())
                    {
                        var user = HttpUtil.DbContext.User_Account.Where(x => x.FullName.Contains(name));
                        foreach (var account in user)
                        {
                            query = query.Where(q => q.UserName.ToLower().Contains(account.UserName.ToLower()));
                        }
                    

                    }
                    var policyEntities = loginAuditDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var policyJsons = new List<UAC_LoginAuditJson>();
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
        private HttpListResult<UAC_LoginAuditJson> GetAllLoginAudit()
        {
            string error = string.Empty;
            var result = new HttpListResult<UAC_LoginAuditJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.UserCredential.AuditManager.CanViewLoginAudit, out error)
            )
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (UAC_LoginAuditDataService loginAuditDataService = new UAC_LoginAuditDataService(DbInstance, HttpUtil.Profile))
                {
                    var policyJsons = new List<UAC_LoginAuditJson>();
                    var policyEntities = loginAuditDataService.GetAll(out error);
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
        private HttpListResult<UAC_LoginAuditJson> SaveLoginAuditList(IList<UAC_LoginAuditJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<UAC_LoginAuditJson>();
            //if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.UserCredential.AuditManager.CanViewLoginAudit, out error)
            //)
            //{
            //    result.HasError = true;
            //    result.HasViewPermission = false;
            //    result.Errors.Add(error);
            //    return result;
            //}
            try
            {
                using (UAC_LoginAuditDataService loginAuditDataService = new UAC_LoginAuditDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<UAC_LoginAudit>();
                    var dbAttachedListEntity = new List<UAC_LoginAudit>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (loginAuditDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<UAC_LoginAuditJson> GetLoginAuditById(long id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<UAC_LoginAuditJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.UserCredential.AuditManager.CanViewLoginAudit, out error)
            )
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (UAC_LoginAuditDataService loginAuditDataService = new UAC_LoginAuditDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new UAC_LoginAuditJson();
                    UAC_LoginAudit entity;
                    if (id <= 0)
                    {
                        entity = UAC_LoginAudit.GetNew();
                    }
                    else
                    {
                        entity = loginAuditDataService.GetById(id);
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
        private HttpResult<UAC_LoginAuditJson> SaveLoginAudit(UAC_LoginAuditJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<UAC_LoginAuditJson>();
            //if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.UserCredential.AuditManager.CanViewLoginAudit, out error)
            //)
            //{
            //    result.HasError = true;
            //    result.HasViewPermission = false;
            //    result.Errors.Add(error);
            //    return result;
            //}
            try
            {
                using (UAC_LoginAuditDataService loginAuditDataService = new UAC_LoginAuditDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new UAC_LoginAudit();
                    var dbAttachedEntity = new UAC_LoginAudit();
                    json.Map(entityReceived);
                    if (loginAuditDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<UAC_LoginAuditJson> GetDeleteLoginAuditById(long id)
        {
            string error = string.Empty;
            var result = new HttpResult<UAC_LoginAuditJson>();
            //if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.UserCredential.AuditManager.CanViewLoginAudit, out error)
            //)
            //{
            //    result.HasError = true;
            //    result.HasViewPermission = false;
            //    result.Errors.Add(error);
            //    return result;
            //}
            try
            {
                using (UAC_LoginAuditDataService loginAuditDataService = new UAC_LoginAuditDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!loginAuditDataService.DeleteById(id, out error))
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
        private HttpResult<UAC_LoginAuditJson> GetLoginAuditByIdWithChild(long id)
        {
            string error = string.Empty;
            var result = new HttpResult<UAC_LoginAuditJson>();
            try
            {
                using (UAC_LoginAuditDataService loginAuditDataService = new UAC_LoginAuditDataService(DbInstance, HttpUtil.Profile))
                {
                    var policyJson = new UAC_LoginAuditJson();
                    UAC_LoginAudit policyEntity;
                    if (id <= 0)
                    {
                        policyEntity = UAC_LoginAudit.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("UAC_LoginAudit");
                        //includeTables.Add("UAC_LoginAudit");

                        policyEntity = loginAuditDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Aca_MarkDistributionPolicyBreakdownJson> GetloginAuditDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_MarkDistributionPolicyBreakdownJson>();
            try
            {
                //UAC_LoginAuditDataService loginAuditDataService =
                //    new UAC_LoginAuditDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.StatusEnumList = EnumUtil.GetEnumList(typeof(EnumCollection.UserCredentialAuditsTypeEnum));
                //DropDown Option Tables
                 
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

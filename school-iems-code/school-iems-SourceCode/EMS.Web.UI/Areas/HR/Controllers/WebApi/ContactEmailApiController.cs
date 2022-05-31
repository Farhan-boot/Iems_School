//File: UI Controller

using System;
using System.Collections.Generic;
using System.Linq;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases.WebApi;
using EMS.Web.Framework.Objects;
using EMS.Web.Jsons.Mapper;
using EMS.Web.Jsons.Models;

namespace EMS.Web.UI.Areas.HR.Controllers.WebApi
{

    public class ContactEmailApiController : EmployeeApiController
	{
        public ContactEmailApiController()
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
        public HttpListResult<User_ContactEmailJson> GetPagedContactEmail(string textkey, int? pageSize, int? pageNo)
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<User_ContactEmailJson>();
            try
            {
                using (User_ContactEmailDataService contactEmailDataService = new User_ContactEmailDataService(DbInstance, HttpUtil.Profile))
                {
                    IQueryable<User_ContactEmail> query = DbInstance.User_ContactEmail.OrderBy(x => x.ContactEmailTypeEnumId);
                    if (textkey.IsStringValid())
                    {
                        query = query.Where(q => q.ContactEmail.ToLower().Contains(textkey.ToLower()));
                    }
                    var policyEntities = contactEmailDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var policyJsons = new List<User_ContactEmailJson>();
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
        private HttpListResult<User_ContactEmailJson> GetAllContactEmail()
        {
            string error = string.Empty;
            var result = new HttpListResult<User_ContactEmailJson>();
            try
            {
                using (User_ContactEmailDataService contactEmailDataService = new User_ContactEmailDataService(DbInstance, HttpUtil.Profile))
                {
                    var policyJsons = new List<User_ContactEmailJson>();
                    var policyEntities = contactEmailDataService.GetAll(out error);
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
        private HttpListResult<User_ContactEmailJson> SaveContactEmailList(IList<User_ContactEmailJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<User_ContactEmailJson>();
            try
            {
                using (User_ContactEmailDataService contactEmailDataService = new User_ContactEmailDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<User_ContactEmail>();
                    var dbAttachedListEntity = new List<User_ContactEmail>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (contactEmailDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<User_ContactEmailJson> GetContactEmailById(long id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<User_ContactEmailJson>();
            try
            {
                using (User_ContactEmailDataService contactEmailDataService = new User_ContactEmailDataService(DbInstance, HttpUtil.Profile))
                {
                    var policyJson = new User_ContactEmailJson();
                    User_ContactEmail policyEntity;
                    if (id <= 0)
                    {
                        policyEntity = User_ContactEmail.GetNew();
                    }
                    else
                    {
                        policyEntity = contactEmailDataService.GetById(id);
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
        private HttpResult<User_ContactEmailJson> SaveContactEmail(User_ContactEmailJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<User_ContactEmailJson>();
            try
            {
                using (User_ContactEmailDataService contactEmailDataService = new User_ContactEmailDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new User_ContactEmail();
                    var dbAttachedEntity = new User_ContactEmail();
                    json.Map(entityReceived);
                    //if (contactEmailDataService.Save(entityReceived, ref dbAttachedEntity, out error))
                    //{
                    //    dbAttachedEntity.Map(json);
                    //    result.Data = json;//return object
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
        private HttpResult<User_ContactEmailJson> GetDeleteContactEmailById(long id)
        {
            string error = string.Empty;
            var result = new HttpResult<User_ContactEmailJson>();
            try
            {
                using (User_ContactEmailDataService contactEmailDataService = new User_ContactEmailDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!contactEmailDataService.DeleteById(id, out error))
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
        private HttpResult<User_ContactEmailJson> GetContactEmailByIdWithChild(long id)
        {
            string error = string.Empty;
            var result = new HttpResult<User_ContactEmailJson>();
            try
            {
                using (User_ContactEmailDataService contactEmailDataService = new User_ContactEmailDataService(DbInstance, HttpUtil.Profile))
                {
                    var policyJson = new User_ContactEmailJson();
                    User_ContactEmail policyEntity;
                    if (id <= 0)
                    {
                        policyEntity = User_ContactEmail.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("User_ContactEmail");
                        //includeTables.Add("User_ContactEmail");

                        policyEntity = contactEmailDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Aca_MarkDistributionPolicyBreakdownJson> GetcontactEmailDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_MarkDistributionPolicyBreakdownJson>();
            try
            {
                //User_ContactEmailDataService contactEmailDataService =
                //    new User_ContactEmailDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.ContactEmailTypeEnumList = EnumUtil.GetEnumList(typeof(User_ContactEmail.ContactEmailTypeEnum));
                result.DataExtra.PrivacyEnumList = EnumUtil.GetEnumList(typeof(User_ContactEmail.PrivacyEnum));
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

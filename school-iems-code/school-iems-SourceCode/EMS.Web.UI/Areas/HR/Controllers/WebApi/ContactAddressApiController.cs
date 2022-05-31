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

    public class ContactAddressApiController : EmployeeApiController
	{
        public ContactAddressApiController()
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
        public HttpListResult<User_ContactAddressJson> GetPagedContactAddress(string textkey, int? pageSize, int? pageNo)
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<User_ContactAddressJson>();
            try
            {
                using (User_ContactAddressDataService contactAddressDataService = new User_ContactAddressDataService(DbInstance, HttpUtil.Profile))
                {
                    IQueryable<User_ContactAddress> query = DbInstance.User_ContactAddress.OrderBy(x => x.ContactAddressTypeEnumId);
                    if (textkey.IsStringValid())
                    {
                        query = query.Where(q => q.CareOfPersonName.ToLower().Contains(textkey.ToLower()));
                    }
                    var policyEntities = contactAddressDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var policyJsons = new List<User_ContactAddressJson>();
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
        private HttpListResult<User_ContactAddressJson> GetAllContactAddress()
        {
            string error = string.Empty;
            var result = new HttpListResult<User_ContactAddressJson>();
            try
            {
                using (User_ContactAddressDataService contactAddressDataService = new User_ContactAddressDataService(DbInstance, HttpUtil.Profile))
                {
                    var policyJsons = new List<User_ContactAddressJson>();
                    var policyEntities = contactAddressDataService.GetAll(out error);
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
        private HttpListResult<User_ContactAddressJson> SaveContactAddressList(IList<User_ContactAddressJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<User_ContactAddressJson>();
            try
            {
                using (User_ContactAddressDataService contactAddressDataService = new User_ContactAddressDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<User_ContactAddress>();
                    var dbAttachedListEntity = new List<User_ContactAddress>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (contactAddressDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<User_ContactAddressJson> GetContactAddressById(long id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<User_ContactAddressJson>();
            try
            {
                using (User_ContactAddressDataService contactAddressDataService = new User_ContactAddressDataService(DbInstance, HttpUtil.Profile))
                {
                    var policyJson = new User_ContactAddressJson();
                    User_ContactAddress policyEntity;
                    if (id <= 0)
                    {
                        policyEntity = User_ContactAddress.GetNew();
                    }
                    else
                    {
                        policyEntity = contactAddressDataService.GetById(id);
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
        private HttpResult<User_ContactAddressJson> SaveContactAddress(User_ContactAddressJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<User_ContactAddressJson>();
            try
            {
                using (User_ContactAddressDataService contactAddressDataService = new User_ContactAddressDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new User_ContactAddress();
                    var dbAttachedEntity = new User_ContactAddress();
                    json.Map(entityReceived);
                    //if (contactAddressDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<User_ContactAddressJson> GetDeleteContactAddressById(long id)
        {
            string error = string.Empty;
            var result = new HttpResult<User_ContactAddressJson>();
            try
            {
                using (User_ContactAddressDataService contactAddressDataService = new User_ContactAddressDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!contactAddressDataService.DeleteById(id, out error))
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
        private HttpResult<User_ContactAddressJson> GetContactAddressByIdWithChild(long id)
        {
            string error = string.Empty;
            var result = new HttpResult<User_ContactAddressJson>();
            try
            {
                using (User_ContactAddressDataService contactAddressDataService = new User_ContactAddressDataService(DbInstance, HttpUtil.Profile))
                {
                    var policyJson = new User_ContactAddressJson();
                    User_ContactAddress policyEntity;
                    if (id <= 0)
                    {
                        policyEntity = User_ContactAddress.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("User_ContactAddress");
                        //includeTables.Add("User_ContactAddress");

                        policyEntity = contactAddressDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Aca_MarkDistributionPolicyBreakdownJson> GetcontactAddressDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_MarkDistributionPolicyBreakdownJson>();
            try
            {
                //User_ContactAddressDataService contactAddressDataService =
                //    new User_ContactAddressDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.ContactAddressTypeEnumList = EnumUtil.GetEnumList(typeof(User_ContactAddress.ContactAddressTypeEnum));
                result.DataExtra.PrivacyEnumList = EnumUtil.GetEnumList(typeof(User_ContactAddress.PrivacyEnum));
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

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

    public class ContactNumberApiController : EmployeeApiController
	{
        public ContactNumberApiController()
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
        public HttpListResult<User_ContactNumberJson> GetPagedContactNumber(string textkey, int? pageSize, int? pageNo)
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<User_ContactNumberJson>();
            try
            {
                using (User_ContactNumberDataService contactNumberDataService = new User_ContactNumberDataService(DbInstance, HttpUtil.Profile))
                {
                    IQueryable<User_ContactNumber> query = DbInstance.User_ContactNumber.OrderBy(x => x.ContactNumberTypeEnumId);
                    if (textkey.IsStringValid())
                    {
                        query = query.Where(q => q.ContactNumber.ToLower().Contains(textkey.ToLower()));
                    }
                    var policyEntities = contactNumberDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var policyJsons = new List<User_ContactNumberJson>();
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
        private HttpListResult<User_ContactNumberJson> GetAllContactNumber()
        {
            string error = string.Empty;
            var result = new HttpListResult<User_ContactNumberJson>();
            try
            {
                using (User_ContactNumberDataService contactNumberDataService = new User_ContactNumberDataService(DbInstance, HttpUtil.Profile))
                {
                    var policyJsons = new List<User_ContactNumberJson>();
                    var policyEntities = contactNumberDataService.GetAll(out error);
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
        private HttpListResult<User_ContactNumberJson> SaveContactNumberList(IList<User_ContactNumberJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<User_ContactNumberJson>();
            try
            {
                using (User_ContactNumberDataService contactNumberDataService = new User_ContactNumberDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<User_ContactNumber>();
                    var dbAttachedListEntity = new List<User_ContactNumber>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (contactNumberDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<User_ContactNumberJson> GetContactNumberById(long id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<User_ContactNumberJson>();
            try
            {
                using (User_ContactNumberDataService contactNumberDataService = new User_ContactNumberDataService(DbInstance, HttpUtil.Profile))
                {
                    var policyJson = new User_ContactNumberJson();
                    User_ContactNumber policyEntity;
                    if (id <= 0)
                    {
                        policyEntity = User_ContactNumber.GetNew();
                    }
                    else
                    {
                        policyEntity = contactNumberDataService.GetById(id);
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
        private HttpResult<User_ContactNumberJson> SaveContactNumber(User_ContactNumberJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<User_ContactNumberJson>();
            try
            {
                using (User_ContactNumberDataService contactNumberDataService = new User_ContactNumberDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new User_ContactNumber();
                    var dbAttachedEntity = new User_ContactNumber();
                    json.Map(entityReceived);
                    //if (contactNumberDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<User_ContactNumberJson> GetDeleteContactNumberById(long id)
        {
            string error = string.Empty;
            var result = new HttpResult<User_ContactNumberJson>();
            try
            {
                using (User_ContactNumberDataService contactNumberDataService = new User_ContactNumberDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!contactNumberDataService.DeleteById(id, out error))
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
        private HttpResult<User_ContactNumberJson> GetContactNumberByIdWithChild(long id)
        {
            string error = string.Empty;
            var result = new HttpResult<User_ContactNumberJson>();
            try
            {
                using (User_ContactNumberDataService contactNumberDataService = new User_ContactNumberDataService(DbInstance, HttpUtil.Profile))
                {
                    var policyJson = new User_ContactNumberJson();
                    User_ContactNumber policyEntity;
                    if (id <= 0)
                    {
                        policyEntity = User_ContactNumber.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("User_ContactNumber");
                        //includeTables.Add("User_ContactNumber");

                        policyEntity = contactNumberDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Aca_MarkDistributionPolicyBreakdownJson> GetcontactNumberDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_MarkDistributionPolicyBreakdownJson>();
            try
            {
                //User_ContactNumberDataService contactNumberDataService =
                //    new User_ContactNumberDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.ContactNumberTypeEnumList = EnumUtil.GetEnumList(typeof(User_ContactNumber.ContactNumberTypeEnum));
                result.DataExtra.PrivacyEnumList = EnumUtil.GetEnumList(typeof(User_ContactNumber.PrivacyEnum));
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

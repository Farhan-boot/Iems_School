//File: UI Controller

using System;
using System.Collections.Generic;
using System.Linq;
using EMS.CoreLibrary;
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

    public class AccountApiController : EmployeeApiController
	{
        public AccountApiController()
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
        public HttpListResult<User_AccountJson> GetPagedAccount(string textkey, int? pageSize, int? pageNo)
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<User_AccountJson>();
            try
            {
                using (User_AccountDataService accountDataService = new User_AccountDataService(DbInstance, HttpUtil.Profile))
                {
                    IQueryable<User_Account> query = DbInstance.User_Account.OrderBy(x => x.FullName);
                    if (textkey.IsStringValid())
                    {
                        query = query.Where(q => q.FullName.ToLower().Contains(textkey.ToLower()));
                    }
                    var policyEntities = accountDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var policyJsons = new List<User_AccountJson>();
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
        private HttpListResult<User_AccountJson> GetAllAccount()
        {
            string error = string.Empty;
            var result = new HttpListResult<User_AccountJson>();
            try
            {
                using (User_AccountDataService accountDataService = new User_AccountDataService(DbInstance, HttpUtil.Profile))
                {
                    var policyJsons = new List<User_AccountJson>();
                    var policyEntities = accountDataService.GetAll(out error);
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
        private HttpListResult<User_AccountJson> SaveAccountList(IList<User_AccountJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<User_AccountJson>();
            try
            {
                using (User_AccountDataService accountDataService = new User_AccountDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<User_Account>();
                    var dbAttachedListEntity = new List<User_Account>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (accountDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<User_AccountJson> GetAccountById(long id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<User_AccountJson>();
            try
            {
                using (User_AccountDataService accountDataService = new User_AccountDataService(DbInstance, HttpUtil.Profile))
                {
                    var policyJson = new User_AccountJson();
                    User_Account policyEntity;
                    if (id <= 0)
                    {
                        policyEntity = User_Account.GetNew();
                    }
                    else
                    {
                        policyEntity = accountDataService.GetById(id);
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
        private HttpResult<User_AccountJson> SaveAccount(User_AccountJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<User_AccountJson>();
            try
            {
                using (User_AccountDataService accountDataService = new User_AccountDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new User_Account();
                    var dbAttachedEntity = new User_Account();
                    json.Map(entityReceived);
                    //if (accountDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<User_AccountJson> GetDeleteAccountById(long id)
        {
            string error = string.Empty;
            var result = new HttpResult<User_AccountJson>();
            try
            {
                using (User_AccountDataService accountDataService = new User_AccountDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!accountDataService.DeleteById(id, out error))
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
        private HttpResult<User_AccountJson> GetAccountByIdWithChild(long id)
        {
            string error = string.Empty;
            var result = new HttpResult<User_AccountJson>();
            try
            {
                using (User_AccountDataService accountDataService = new User_AccountDataService(DbInstance, HttpUtil.Profile))
                {
                    var policyJson = new User_AccountJson();
                    User_Account policyEntity;
                    if (id <= 0)
                    {
                        policyEntity = User_Account.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("User_Account");
                        //includeTables.Add("User_Account");

                        policyEntity = accountDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Aca_MarkDistributionPolicyBreakdownJson> GetaccountDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_MarkDistributionPolicyBreakdownJson>();
            try
            {
                //User_AccountDataService accountDataService =
                //    new User_AccountDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.GenderEnumList = EnumUtil.GetEnumList(typeof(EnumCollection.Common.GenderEnum));
                result.DataExtra.ReligionEnumList = EnumUtil.GetEnumList(typeof(EnumCollection.Common.ReligionEnum));
                result.DataExtra.MaritalStatusEnumList = EnumUtil.GetEnumList(typeof(EnumCollection.Common.MaritalStatusEnum));
                result.DataExtra.BloodGroupEnumList = EnumUtil.GetEnumList(typeof(EnumCollection.Common.BloodGroupEnum));
                result.DataExtra.UserTypeEnumList = EnumUtil.GetEnumList(typeof(User_Account.UserTypeEnum));
                result.DataExtra.UserStatusEnumList = EnumUtil.GetEnumList(typeof(EnumCollection.Common.StatusEnum));
                result.DataExtra.CategoryEnumList = EnumUtil.GetEnumList(typeof(EnumCollection.Common.UserCategoryEnum));
                //DropDown Option Tables
                 
                //result.DataExtra.AccountList = DbInstance.User_Account.AsEnumerable().Select(t => new
                //{ Id = t.Id.ToString(),Name = t.Name }).ToList();
                //result.DataExtra.BankList = DbInstance.General_Bank.AsEnumerable().Select(t => new
                //{ Id = t.Id.ToString(),Name = t.Name }).ToList();
                //result.DataExtra.RankList = DbInstance.User_Rank.AsEnumerable().Select(t => new
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

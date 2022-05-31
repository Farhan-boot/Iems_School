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


namespace EMS.Web.UI.Areas.HR.Controllers.WebApi
{

    public class PositionApiController : EmployeeBaseApiController
	{
        public PositionApiController()
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
        public HttpListResult<HR_PositionJson> GetPagedPosition(
            string textkey, 
            int? pageSize, 
            int? pageNo, 
            int jobClassEnumId = -1,
            int salaryTemplateGroupId = -1
            )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<HR_PositionJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.PositionManager.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.PositionManager.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.PositionManager.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (HR_PositionDataService positionDataService = new HR_PositionDataService(DbInstance, HttpUtil.Profile))
                {
                    IQueryable<HR_Position> query = DbInstance.HR_Position
                        .OrderBy(x => x.JobClassEnumId)
                        .ThenBy(x => x.Priority);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Name.ToLower().Contains(textkey.ToLower()));
                    }

                    if (salaryTemplateGroupId > -1)
                    {
                        //Here 0 is repersenting those who's salary template were not generated.
                        if (salaryTemplateGroupId == 0)
                        {
                            query = query.Where(q => q.SalaryTemplateId == null);
                        }
                        else
                        {
                            query = query.Where(q => q.SalaryTemplateId == salaryTemplateGroupId);
                        }
                    }

                    if (jobClassEnumId >= 0)
                    {
                        query = query.Where(q => q.JobClassEnumId == (byte)jobClassEnumId);
                    }
                    var policyEntities = positionDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var policyJsons = new List<HR_PositionJson>();
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
        private HttpListResult<HR_PositionJson> GetAllPosition()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_PositionJson>();
            try
            {
                using (HR_PositionDataService positionDataService = new HR_PositionDataService(DbInstance, HttpUtil.Profile))
                {
                    var policyJsons = new List<HR_PositionJson>();
                    var policyEntities = positionDataService.GetAll(out error);
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
        private HttpListResult<HR_PositionJson> SavePositionList(IList<HR_PositionJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_PositionJson>();
            try
            {
                using (HR_PositionDataService positionDataService = new HR_PositionDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<HR_Position>();
                    var dbAttachedListEntity = new List<HR_Position>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (positionDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<HR_PositionJson> GetPositionById(int id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_PositionJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.PositionManager.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.PositionManager.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.PositionManager.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (HR_PositionDataService positionDataService = new HR_PositionDataService(DbInstance, HttpUtil.Profile))
                {
                    var policyJson = new HR_PositionJson();
                    HR_Position policyEntity;
                    if (id <= 0)
                    {
                        policyEntity = HR_Position.GetNew();
                    }
                    else
                    {
                        policyEntity = positionDataService.GetById(id);
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
        public HttpResult<HR_PositionJson> SavePosition(HR_PositionJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_PositionJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.PositionManager.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.PositionManager.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (HR_PositionDataService positionDataService = new HR_PositionDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new HR_Position();
                    var dbAttachedEntity = new HR_Position();
                    json.Map(entityReceived);
                    if (positionDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        public HttpResult<HR_PositionJson> GetDeletePositionById(int id)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_PositionJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.HumanResourceArea.PositionManager.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (HR_PositionDataService positionDataService = new HR_PositionDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!positionDataService.DeleteById(id, out error))
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
        private HttpResult<HR_PositionJson> GetPositionByIdWithChild(int id)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_PositionJson>();
            try
            {
                using (HR_PositionDataService positionDataService = new HR_PositionDataService(DbInstance, HttpUtil.Profile))
                {
                    var policyJson = new HR_PositionJson();
                    HR_Position policyEntity;
                    if (id <= 0)
                    {
                        policyEntity = HR_Position.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("HR_Position");
                        //includeTables.Add("HR_Position");

                        policyEntity = positionDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<HR_PositionJson> GetPositionListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_PositionJson>();
            try
            {
                //HR_PositionDataService positionDataService =
                //    new HR_PositionDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List

                result.DataExtra.JobClassEnumList = EnumUtil.GetEnumList(typeof(User_Employee.JobClassEnum));
                result.DataExtra.StatusEnumList = EnumUtil.GetEnumList(typeof(HR_Position.StatusEnum));
                //DropDown Option Tables

                result.DataExtra.SalaryTemplateGroupList = DbInstance.HR_SalaryTemplate.AsEnumerable()
                    .Select(t => new
                    {
                        Id = t.Id,
                        Name = t.Name
                    }).ToList();

            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<Aca_MarkDistributionPolicyBreakdownJson> GetpositionDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_MarkDistributionPolicyBreakdownJson>();
            try
            {
                //HR_PositionDataService positionDataService =
                //    new HR_PositionDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.JobClassEnumList = EnumUtil.GetEnumList(typeof(User_Employee.JobClassEnum));
                result.DataExtra.StatusEnumList = EnumUtil.GetEnumList(typeof(HR_Position.StatusEnum));
                //DropDown Option Tables

                result.DataExtra.SalaryTemplateList = DbInstance.HR_SalaryTemplate.Where(x => !x.IsDeleted)
                    .OrderByDescending(x => x.Id)
                    .AsEnumerable()
                    .Select(t => new
                    {
                        Id = t.Id,
                        Name = t.Name
                    }).ToList();

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

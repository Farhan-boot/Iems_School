//File: UI Controller
using System;
using System.Collections.Generic;
using System.Data;
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


namespace EMS.Web.UI.Areas.HR.Controllers.WebApi
{

    public class LeaveAllocationTemplateDetailsApiController : EmployeeBaseApiController
	{
        public LeaveAllocationTemplateDetailsApiController()
        {  }
       
        #region LeaveAllocationTemplateDetails 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<HR_LeaveAllocationTemplateDetailsJson> GetPagedLeaveAllocationTemplateDetails(string textkey, int? pageSize, int? pageNo
           ,Int32 salaryTemplateId= 0      
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<HR_LeaveAllocationTemplateDetailsJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.LeaveAllocationTemplateDetails.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.LeaveAllocationTemplateDetails.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.LeaveAllocationTemplateDetails.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/

            try
            {
                using (HR_LeaveAllocationTemplateDetailsDataService leaveAllocationTemplateDetailsDataService = new HR_LeaveAllocationTemplateDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<HR_LeaveAllocationTemplateDetails> query = DbInstance.HR_LeaveAllocationTemplateDetails.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Name.ToLower().Contains(textkey.ToLower()));
                    }
                    if (salaryTemplateId>0)
                    {
                        query = query.Where(q => q.SalaryTemplateId== salaryTemplateId);
                    }  
                 
                    var entities = leaveAllocationTemplateDetailsDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<HR_LeaveAllocationTemplateDetailsJson>();
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
        private HttpListResult<HR_LeaveAllocationTemplateDetailsJson> GetAllLeaveAllocationTemplateDetails()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_LeaveAllocationTemplateDetailsJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.LeaveAllocationTemplateDetails.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.LeaveAllocationTemplateDetails.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.LeaveAllocationTemplateDetails.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_LeaveAllocationTemplateDetailsDataService leaveAllocationTemplateDetailsDataService = new HR_LeaveAllocationTemplateDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<HR_LeaveAllocationTemplateDetailsJson>();
                    var entities = leaveAllocationTemplateDetailsDataService.GetAll(out error);
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
        public HttpResult<HR_LeaveAllocationTemplateDetailsJson> GetLeaveAllocationTemplateDetailsById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_LeaveAllocationTemplateDetailsJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.LeaveAllocationTemplateDetails.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.LeaveAllocationTemplateDetails.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.LeaveAllocationTemplateDetails.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_LeaveAllocationTemplateDetailsDataService leaveAllocationTemplateDetailsDataService = new HR_LeaveAllocationTemplateDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new HR_LeaveAllocationTemplateDetailsJson();
                    HR_LeaveAllocationTemplateDetails entity;
                    if (id <= 0)
                    {
                        entity = HR_LeaveAllocationTemplateDetails.GetNew();
                    }
                    else
                    {
                        entity = leaveAllocationTemplateDetailsDataService.GetById(id);
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
        private HttpResult<HR_LeaveAllocationTemplateDetailsJson> GetLeaveAllocationTemplateDetailsByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_LeaveAllocationTemplateDetailsJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.LeaveAllocationTemplateDetails.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.LeaveAllocationTemplateDetails.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.LeaveAllocationTemplateDetails.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_LeaveAllocationTemplateDetailsDataService leaveAllocationTemplateDetailsDataService = new HR_LeaveAllocationTemplateDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new HR_LeaveAllocationTemplateDetailsJson();
                    HR_LeaveAllocationTemplateDetails entity;
                    if (id <= 0)
                    {
                        entity = HR_LeaveAllocationTemplateDetails.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("HR_LeaveAllocationTemplateDetails");
                        //includeTables.Add("HR_LeaveAllocationTemplateDetails");

                        entity = leaveAllocationTemplateDetailsDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<HR_LeaveAllocationTemplateDetailsJson> GetLeaveAllocationTemplateDetailsListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_LeaveAllocationTemplateDetailsJson>();
            try
            {
                //HR_LeaveAllocationTemplateDetailsDataService leaveAllocationTemplateDetailsDataService =
                //    new HR_LeaveAllocationTemplateDetailsDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.LeaveTypeEnumList = EnumUtil.GetEnumList(typeof(HR_LeaveAllocationTemplateDetails.LeaveTypeEnum));
                //DropDown Option Tables
                 
                //result.DataExtra.SalaryTemplateList = DbInstance.HR_SalaryTemplate.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<HR_LeaveAllocationTemplateDetailsJson> GetLeaveAllocationTemplateDetailsDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_LeaveAllocationTemplateDetailsJson>();
            try
            {
                //HR_LeaveAllocationTemplateDetailsDataService leaveAllocationTemplateDetailsDataService =
                //    new HR_LeaveAllocationTemplateDetailsDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.LeaveTypeEnumList = EnumUtil.GetEnumList(typeof(HR_LeaveAllocationTemplateDetails.LeaveTypeEnum));
                //DropDown Option Tables
                 
                //result.DataExtra.SalaryTemplateList = DbInstance.HR_SalaryTemplate.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
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
        public HttpResult<HR_LeaveAllocationTemplateDetailsJson> SaveLeaveAllocationTemplateDetails(HR_LeaveAllocationTemplateDetailsJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_LeaveAllocationTemplateDetailsJson>();
            //optional permission, check permission in the SaveLeaveAllocationTemplateDetailsLogic insted
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.LeaveAllocationTemplateDetails.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.LeaveAllocationTemplateDetails.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                 var entityReceived = new HR_LeaveAllocationTemplateDetails();
                 var dbAttachedEntity = new HR_LeaveAllocationTemplateDetails();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SaveLeaveAllocationTemplateDetailsLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<HR_LeaveAllocationTemplateDetailsJson> SaveLeaveAllocationTemplateDetails2(HR_LeaveAllocationTemplateDetailsJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_LeaveAllocationTemplateDetailsJson>();
            try
            {
                using (HR_LeaveAllocationTemplateDetailsDataService leaveAllocationTemplateDetailsDataService = new HR_LeaveAllocationTemplateDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new HR_LeaveAllocationTemplateDetails();
                    var dbAttachedEntity = new HR_LeaveAllocationTemplateDetails();
                    json.Map(entityReceived);
                    if (leaveAllocationTemplateDetailsDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<HR_LeaveAllocationTemplateDetailsJson> SaveLeaveAllocationTemplateDetailsList(IList<HR_LeaveAllocationTemplateDetailsJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_LeaveAllocationTemplateDetailsJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.LeaveAllocationTemplateDetails.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.LeaveAllocationTemplateDetails.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_LeaveAllocationTemplateDetailsDataService leaveAllocationTemplateDetailsDataService = new HR_LeaveAllocationTemplateDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<HR_LeaveAllocationTemplateDetails>();
                    var dbAttachedListEntity = new List<HR_LeaveAllocationTemplateDetails>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (leaveAllocationTemplateDetailsDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<HR_LeaveAllocationTemplateDetailsJson> GetDeleteLeaveAllocationTemplateDetailsById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_LeaveAllocationTemplateDetailsJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.LeaveAllocationTemplateDetails.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_LeaveAllocationTemplateDetailsDataService leaveAllocationTemplateDetailsDataService = new HR_LeaveAllocationTemplateDetailsDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!leaveAllocationTemplateDetailsDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveLeaveAllocationTemplateDetails(HR_LeaveAllocationTemplateDetails newObj, out string error)
        {
            error = "";
            if (!newObj.Name.IsValid())
            {
                error += "Name is not valid.";
                return false;
            }
            if (newObj.Name.Length>150)
            {
                error += "Name exceeded its maximum length 150.";
                return false;
            }
            if (newObj.SalaryTemplateId<=0)
            {
                error += "Please select valid Salary Template.";
                return false;
            }
            if (newObj.LeaveTypeEnumId==null)
            {
                error += "Please select valid Leave Type.";
                return false;
            }
            if (newObj.AllowedDays==null)
            {
                error += "Allowed Days required.";
                return false;
            }
            if (newObj.WorkingHourPerDays==null)
            {
                error += "Working Hour Per Days required.";
                return false;
            }
            if (newObj.IsPaid==null)
            {
                error += "Is Paid required.";
                return false;
            }

            //TODO write your custom validation here.
            //var res =  DbInstance.HR_LeaveAllocationTemplateDetails.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A LeaveAllocationTemplateDetails already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveLeaveAllocationTemplateDetailsLogic(HR_LeaveAllocationTemplateDetails newObj,ref HR_LeaveAllocationTemplateDetails dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Leave Allocation Template Details to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveLeaveAllocationTemplateDetails(newObj, out error))
                return false;

            bool isNewObject = true;
            HR_LeaveAllocationTemplateDetails objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.HR_LeaveAllocationTemplateDetails.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = HR_LeaveAllocationTemplateDetails.GetNew(newObj.Id);
                DbInstance.HR_LeaveAllocationTemplateDetails.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.HRArea.LeaveAllocationTemplateDetails.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.HRArea.LeaveAllocationTemplateDetails.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
           objToSave.Name =  newObj.Name ;
           objToSave.SalaryTemplateId =  newObj.SalaryTemplateId ;
           objToSave.LeaveTypeEnumId =  newObj.LeaveTypeEnumId ;
           objToSave.AllowedDays =  newObj.AllowedDays ;
           objToSave.WorkingHourPerDays =  newObj.WorkingHourPerDays ;
           objToSave.IsPaid =  newObj.IsPaid ;
           objToSave.Remarks =  newObj.Remarks ;
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

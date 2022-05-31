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

    public class AttendanceApiController : EmployeeBaseApiController
	{
        public AttendanceApiController()
        {  }
       
        #region Attendance 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<HR_AttendanceJson> GetPagedAttendance(string textkey, int? pageSize, int? pageNo
           ,Int32 employeeId= 0      
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<HR_AttendanceJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.Attendance.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.Attendance.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.Attendance.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/

            try
            {
                using (HR_AttendanceDataService attendanceDataService = new HR_AttendanceDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<HR_Attendance> query = DbInstance.HR_Attendance.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Remarks.ToLower().Contains(textkey.ToLower()));
                    }
                    if (employeeId>0)
                    {
                        query = query.Where(q => q.EmployeeId== employeeId);
                    }  
                 
                    var entities = attendanceDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<HR_AttendanceJson>();
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
        private HttpListResult<HR_AttendanceJson> GetAllAttendance()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_AttendanceJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.Attendance.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.Attendance.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.Attendance.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_AttendanceDataService attendanceDataService = new HR_AttendanceDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<HR_AttendanceJson>();
                    var entities = attendanceDataService.GetAll(out error);
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
        public HttpResult<HR_AttendanceJson> GetAttendanceById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_AttendanceJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.Attendance.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.Attendance.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.Attendance.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_AttendanceDataService attendanceDataService = new HR_AttendanceDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new HR_AttendanceJson();
                    HR_Attendance entity;
                    if (id <= 0)
                    {
                        entity = HR_Attendance.GetNew();
                    }
                    else
                    {
                        entity = attendanceDataService.GetById(id);
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
        private HttpResult<HR_AttendanceJson> GetAttendanceByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_AttendanceJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.Attendance.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.Attendance.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.Attendance.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_AttendanceDataService attendanceDataService = new HR_AttendanceDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new HR_AttendanceJson();
                    HR_Attendance entity;
                    if (id <= 0)
                    {
                        entity = HR_Attendance.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("HR_Attendance");
                        //includeTables.Add("HR_Attendance");

                        entity = attendanceDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<HR_AttendanceJson> GetAttendanceListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_AttendanceJson>();
            try
            {
                //HR_AttendanceDataService attendanceDataService =
                //    new HR_AttendanceDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.PunchTypeEnumList = EnumUtil.GetEnumList(typeof(HR_Attendance.PunchTypeEnum));
                result.DataExtra.PunchModeEnumList = EnumUtil.GetEnumList(typeof(HR_Attendance.PunchModeEnum));
                //DropDown Option Tables
                 
                //result.DataExtra.EmployeeList = DbInstance.User_Employee.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<HR_AttendanceJson> GetAttendanceDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_AttendanceJson>();
            try
            {
                //HR_AttendanceDataService attendanceDataService =
                //    new HR_AttendanceDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.PunchTypeEnumList = EnumUtil.GetEnumList(typeof(HR_Attendance.PunchTypeEnum));
                result.DataExtra.PunchModeEnumList = EnumUtil.GetEnumList(typeof(HR_Attendance.PunchModeEnum));
                //DropDown Option Tables
                 
                //result.DataExtra.EmployeeList = DbInstance.User_Employee.AsEnumerable().Select(t => new
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
        public HttpResult<HR_AttendanceJson> SaveAttendance(HR_AttendanceJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_AttendanceJson>();
            //optional permission, check permission in the SaveAttendanceLogic insted
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.Attendance.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.Attendance.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                 var entityReceived = new HR_Attendance();
                 var dbAttachedEntity = new HR_Attendance();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SaveAttendanceLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<HR_AttendanceJson> SaveAttendance2(HR_AttendanceJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_AttendanceJson>();
            try
            {
                using (HR_AttendanceDataService attendanceDataService = new HR_AttendanceDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new HR_Attendance();
                    var dbAttachedEntity = new HR_Attendance();
                    json.Map(entityReceived);
                    if (attendanceDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<HR_AttendanceJson> SaveAttendanceList(IList<HR_AttendanceJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_AttendanceJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.Attendance.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.HRArea.Attendance.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_AttendanceDataService attendanceDataService = new HR_AttendanceDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<HR_Attendance>();
                    var dbAttachedListEntity = new List<HR_Attendance>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (attendanceDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<HR_AttendanceJson> GetDeleteAttendanceById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_AttendanceJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.HRArea.Attendance.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (HR_AttendanceDataService attendanceDataService = new HR_AttendanceDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!attendanceDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveAttendance(HR_Attendance newObj, out string error)
        {
            error = "";
            if (newObj.EmployeeId<=0)
            {
                error += "Please select valid Employee.";
                return false;
            }
            if (newObj.PunchTypeEnumId==null)
            {
                error += "Please select valid Punch Type.";
                return false;
            }
            if (!newObj.PunchTime.IsValid())
            {
                error += "Punch Time is not valid.";
                return false;
            }
            if (newObj.PunchModeEnumId==null)
            {
                error += "Please select valid Punch Mode.";
                return false;
            }



            if (newObj.IsDeleted==null)
            {
                error += "Is Deleted required.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.HR_Attendance.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A Attendance already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveAttendanceLogic(HR_Attendance newObj,ref HR_Attendance dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Attendance to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveAttendance(newObj, out error))
                return false;

            bool isNewObject = true;
            HR_Attendance objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.HR_Attendance.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = HR_Attendance.GetNew(newObj.Id);
                DbInstance.HR_Attendance.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.HRArea.Attendance.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.HRArea.Attendance.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
           objToSave.EmployeeId =  newObj.EmployeeId ;
           objToSave.PunchTypeEnumId =  newObj.PunchTypeEnumId ;
           objToSave.PunchTime =  newObj.PunchTime ;
           objToSave.PunchModeEnumId =  newObj.PunchModeEnumId ;
           objToSave.DeviceId =  newObj.DeviceId ;
           objToSave.Remarks =  newObj.Remarks ;
           objToSave.History =  newObj.History ;
           objToSave.IsDeleted =  newObj.IsDeleted ;
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

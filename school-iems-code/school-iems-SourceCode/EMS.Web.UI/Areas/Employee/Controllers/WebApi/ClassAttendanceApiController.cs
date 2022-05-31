using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using EMS.Communication.SMSProxy;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.CustomEntity;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.Framework.Permissions;
using EMS.Framework.Settings;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases.WebApi;
using EMS.Framework.Objects;
using EMS.Web.Jsons.Custom.Academic.ResultModel;
using EMS.Web.Jsons.Models;
using EMS.Web.UI.Areas.HR.Controllers.WebApi;


namespace EMS.Web.UI.Areas.Employee.Controllers.WebApi
{
    public class ClassAttendanceApiController : EmployeeBaseApiController
    {
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public HttpListResult<Aca_ClassAttendanceSettingJson> GetPagedClassAttendanceSettingByClassId(
            string textkey,
            int? pageSize, int? pageNo
           , Int64 classId = 0
           , Int64 routineId = 0
           , Int64 employeeId = 0
         )
        {

            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Aca_ClassAttendanceSettingJson>();

            var res = DbInstance.Aca_ClassTakenByEmployee
                .Any(x => x.ClassId == classId
                          && !x.IsDeleted
                && x.EmployeeId == HttpUtil.Profile.EmployeeId//EmployeeId
                && x.RoleEnumId == (byte)Aca_ClassTakenByEmployee.RoleEnum.Faculty);
            //&& (x.RoleEnumId == (byte)Aca_ClassTakenByEmployee.RoleEnum.
            //todo attendance permission
            var permis = PermissionUtil.HasPermission(
                    PermissionCollection.AcademicArea.OfferedClassManager.ClassAttendance.CanViewAttendance,
                    out error);
            if (!res && !permis)
            {
                error = "You don't have permission to view this!";
                result.HasError = true;

                result.Errors.Add(error);
                return result;
            }



            try
            {
                using (Aca_ClassAttendanceSettingDataService classAttendanceSettingDataService = new Aca_ClassAttendanceSettingDataService(DbInstance, HttpUtil.Profile))
                {
                    IQueryable<Aca_ClassAttendanceSetting> query = DbInstance.Aca_ClassAttendanceSetting
                        .OrderByDescending(x => x.StartTime)
                        .Include(x => x.Aca_ClassAttendanceSmsLog)  //temporary
                        .Include("Aca_ClassAttendanceSmsLog.User_Student") //temporary  
                        .Include("Aca_ClassAttendanceSmsLog.User_Student.User_Account")
                        .Include(x => x.User_Employee)
                        .Include(x => x.User_Employee.User_Account);


                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Remark.ToLower().Contains(textkey.ToLower()));
                    }
                    if (classId > 0)
                    {
                        query = query.Where(q => q.ClassId == classId);
                    }
                    if (routineId > 0)
                    {
                        query = query.Where(q => q.RoutineId == routineId);
                    }
                    if (employeeId > 0)
                    {
                        query = query.Where(q => q.EmployeeId == employeeId);
                    }

                    var entities = classAttendanceSettingDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);

                    var jsons = new List<Aca_ClassAttendanceSettingJson>();
                    entities.Map(jsons);

                    result.Data = jsons;
                    result.Count = count;
                    //data extra
                    result.DataExtra.ClassTypeEnumList = EnumUtil.GetEnumList(typeof(Aca_ClassAttendanceSetting.ClassTypeEnum));

                    var smsLogsList = new List<Aca_ClassAttendanceSmsLogJson>();
                    foreach (var log in jsons.Where(x => x.Aca_ClassAttendanceSmsLogJsonList != null && x.Aca_ClassAttendanceSmsLogJsonList.Count > 0))
                    {
                        smsLogsList.AddRange(log.Aca_ClassAttendanceSmsLogJsonList);
                        log.Aca_ClassAttendanceSmsLogJsonList = null;

                    }
                    result.DataExtra.SmsLogList = smsLogsList;
                    //DropDown Option Tables

                    //result.DataExtra.ClassList = DbInstance.Aca_Class.AsEnumerable().Select(t => new
                    //{ Id = t.Id.ToString(),Name = t.Name }).ToList();
                    //result.DataExtra.ClassRoutineList = DbInstance.Aca_ClassRoutine.AsEnumerable().Select(t => new
                    //{ Id = t.Id.ToString(), Name = t.RoutineDetails }).ToList();
                    //result.DataExtra.EmployeeList = DbInstance.Aca_ClassAttendanceSetting.Where().AsEnumerable().Select(t => new
                    //{ Id = t.Id.ToString(), Name = t.Name }).ToList();
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }


        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public HttpResult<Aca_ClassAttendanceSettingJson> GetAttendanceSettingByClassIdSettingId(long classId = 0, long settingsId = 0)  //, int examType=0
        {
            var result = new HttpResult<Aca_ClassAttendanceSettingJson>();
            var error = "";

            try
            {
                var res = DbInstance.Aca_ClassTakenByEmployee
                .Any(x => x.ClassId == classId 
                          && !x.IsDeleted
                          && x.EmployeeId == HttpUtil.Profile.EmployeeId && x.RoleEnumId == (byte)Aca_ClassTakenByEmployee.RoleEnum.Faculty);  //EmployeeId

                //todo attendance permission
                var permis = PermissionUtil.HasPermission(
                        PermissionCollection.AcademicArea.OfferedClassManager.ClassAttendance.CanViewAttendance,
                        out error);
                if (!res && !permis)
                {
                    error = "You don't have permission to view this!";
                    result.HasError = true;

                    result.Errors.Add(error);
                    return result;
                }
                var json = new Aca_ClassAttendanceSettingJson();

                /*var attendanceSetting = Facade.AcademicFacade.ClassAttendanceFacade.GetAttendanceSettingByClassIdSettingId(classId, settingsId, out error);
                if (attendanceSetting == null)
                {
                    result.HasError = true;
                    result.Errors.Add(error);
                    return result;
                }*/

                //attendanceSetting.Map(json);
                result.Data = json;
                //result.DataExtra.SemesterLevelTerm = attendanceSetting.Aca_Class.Aca_Semester.Name;
                result.DataExtra.ReasonEnumList = EnumUtil.GetEnumList(typeof(Aca_ClassAttendance.ReasonEnum));
                result.DataExtra.ClassTypeEnumList = EnumUtil.GetEnumList(typeof(Aca_ClassAttendanceSetting.ClassTypeEnum));
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.Message.ToString());
            }
            return result;
        }

        [System.Web.Http.HttpPost]
        public HttpResult<Aca_ClassAttendanceSettingJson> SaveClassAttendanceSetting(Aca_ClassAttendanceSettingJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_ClassAttendanceSettingJson>();
            if (json?.Aca_ClassAttendanceListJson == null || json.Aca_ClassAttendanceListJson.Count <= 0)
            {
                error = "There is no Attendance to save!";
                result.HasError = true;

                result.Errors.Add(error);
                return result;
            }


            
            try
            {
                var entityReceived = new Aca_ClassAttendanceSetting();
                var dbAttachedEntity = new Aca_ClassAttendanceSetting();
                json.Map(entityReceived);
               using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {
                    var classId = json.ClassId;
                    var classObj = DbInstance.Aca_Class.Include(x => x.Aca_Semester)
                        .SingleOrDefault(x => x.Id.ToString() == classId);
                    if (classObj == null)
                    {
                        result.HasError = true;
                        result.Errors.Add("Invalid class to update Attendance!");
                        return result;
                    }

                    entityReceived.EndTime = new DateTime(entityReceived.StartTime.Year, entityReceived.StartTime.Month, entityReceived.StartTime.Day, entityReceived.EndTime.Hour, entityReceived.EndTime.Minute, entityReceived.EndTime.Second);
                    if (SaveClassAttendanceSettingLogic(entityReceived, ref dbAttachedEntity, out error))
                    {
                        var attendanceList = json.Aca_ClassAttendanceListJson;

                        List<Aca_ClassAttendanceSmsLog> smsLogsToSent = new List<Aca_ClassAttendanceSmsLog>();
                        foreach (var attendance in attendanceList)
                        {
                            var attendanceEntity = new Aca_ClassAttendance();
                            attendance.Map(attendanceEntity);
                            attendanceEntity.ClassId = entityReceived.ClassId;
                            attendanceEntity.SettingId = entityReceived.Id;
                            attendanceEntity.StartTime = entityReceived.StartTime;
                            attendanceEntity.EndTime = entityReceived.EndTime;
                            var dbAttachedattendanceEntity = new Aca_ClassAttendance();
                            if (!SaveClassAttendanceLogic(attendanceEntity, ref dbAttachedattendanceEntity, out error))
                            {
                                result.HasError = true;
                                result.Errors.Add(error);
                                scope.Rollback();
                                return result;
                            }
                            else
                            {
                                if (SiteSettings.Instance.IsSentSmsToParentOnAbsentAttendance)
                                {
                                    var log = GenerateSmsLogForSent(dbAttachedattendanceEntity, classObj);
                                    if (log != null)
                                    {
                                        smsLogsToSent.Add(log);
                                    }
                                }
                            }
                        }

                        DbInstance.SaveChanges();
                        scope.Commit();

                        //save log and send sms
                        Facade.SmsFacade.SendSmsAndSaveLogForAbsentClassAttendance(smsLogsToSent, true, out error);

                        //dbAttachedEntity.Map(json);
                        //result.Data = json; //return object
                        return GetAttendanceSettingByClassIdSettingId(dbAttachedEntity.ClassId, dbAttachedEntity.Id);
                    }
                    else
                    {
                        result.HasError = true;
                        result.Errors.Add(error);
                        scope.Rollback();
                        return result;
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

        public HttpResult GetDeleteClassAttendanceSettingById(long id)
        {
            string error = string.Empty;
            var result = new HttpResult();
            using (var scope = DbInstance.Database.BeginTransaction())
            {
                try
                {
                    Aca_ClassAttendanceSetting objToDelete = DbInstance.Aca_ClassAttendanceSetting.Find(id);
                    if (objToDelete == null)
                    {
                        return result;
                    }
                    var res = DbInstance.Aca_ClassTakenByEmployee
                        .Any(x => x.ClassId == objToDelete.ClassId 
                                  && !x.IsDeleted
                                  && x.EmployeeId == HttpUtil.Profile.EmployeeId && x.RoleEnumId == (byte)Aca_ClassTakenByEmployee.RoleEnum.Faculty);//EmployeeId

                    var permis = PermissionUtil.HasPermission(
                        PermissionCollection.AcademicArea.OfferedClassManager.ClassAttendance.CanDeleteAttendance,
                        out error);
                    if (!res && !permis)
                    {
                        error = "You don't have permission to delete Attendance for this class, you are not faculty of this Class!";
                        result.HasError = true;
                        result.Errors.Add(error);
                        return result;
                    }
                    List<Aca_ClassAttendanceSmsLog> smslogToDelete = DbInstance.Aca_ClassAttendanceSmsLog.Where(x => x.AttendanceSettingId == id).ToList();

                    if (smslogToDelete.Count > 0)
                    {
                        DbInstance.Aca_ClassAttendanceSmsLog.RemoveRange(smslogToDelete);
                    }
                    List<Aca_ClassAttendance> attendanceToDelete = DbInstance.Aca_ClassAttendance.Where(x => x.SettingId == id).ToList();

                    if (attendanceToDelete.Count > 0)
                    {
                        DbInstance.Aca_ClassAttendance.RemoveRange(attendanceToDelete);
                    }
                   
                    DbInstance.Aca_ClassAttendanceSetting.Remove(objToDelete);
                    DbInstance.SaveChanges();
                    scope.Commit();
                }
                catch (Exception ex)
                {
                    scope.Rollback();
                    result.HasError = true;
                    result.Errors.Add(ex.GetBaseException().Message.ToString());
                }
            }
            return result;
        }
        private HttpResult<Aca_ClassAttendanceJson> GetDeleteClassAttendanceById(long id)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_ClassAttendanceJson>();
            try
            {
                using (Aca_ClassAttendanceDataService classAttendanceDataService = new Aca_ClassAttendanceDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!classAttendanceDataService.DeleteById(id, out error))
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
        private HttpResult<Aca_ClassAttendanceSmsLogJson> GetDeleteClassAttendanceSmsLogById(long id)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_ClassAttendanceSmsLogJson>();
            try
            {
                using (Aca_ClassAttendanceSmsLogDataService classAttendanceSmsLogDataService = new Aca_ClassAttendanceSmsLogDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!classAttendanceSmsLogDataService.DeleteById(id, out error))
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
        #region Local Method (should be always private)
        private bool IsValidToSaveClassAttendanceSetting(Aca_ClassAttendanceSetting newObj, out string error)
        {
            error = "";
            if (newObj.ClassId <= 0)
            {
                error += "Please select valid Class.";
                return false;
            }

            if (!newObj.StartTime.IsValid())
            {
                error += "Start Time is not valid.";
                return false;
            }
            if (!newObj.EndTime.IsValid())
            {
                error += "End Time is not valid.";
                return false;
            }
            if (newObj.StartTime>= newObj.EndTime)
            {
                error += "'End Time' can't be less then or equal 'Start Time'.";
                return false;
            }
            //if (newObj.EmployeeId <= 0)
            //{
            //    error += "Please select valid Employee.";
            //    return false;
            //}
            if (newObj.ClassTypeEnumId == null)
            {
                error += "Please select valid Class Type.";
                return false;
            }
            if (newObj.IsLocked == null)
            {
                error += "Is Locked required.";
                return false;
            }
            var startTime = newObj.StartTime;
            var endTime = newObj.EndTime;
            newObj.StartTime = startTime.Date;
            newObj.StartTime = newObj.StartTime.AddHours(startTime.Hour).AddMinutes(startTime.Minute);
            newObj.EndTime = startTime.Date;
            newObj.EndTime = newObj.EndTime.AddHours(endTime.Hour).AddMinutes(endTime.Minute);
            //newObj.StartTime = newObj.StartTime.AddSeconds(-startTime.Second).AddMilliseconds(-startTime.Millisecond);
            //newObj.EndTime = newObj.EndTime.AddSeconds(-endTime.Second).AddMilliseconds(-endTime.Millisecond);
            //checking over lapping attendance.
            var res = DbInstance.Aca_ClassAttendanceSetting.Any(x => x.Id != newObj.Id
            && x.EmployeeId == HttpUtil.Profile.EmployeeId
            && x.ClassId == newObj.ClassId
            && x.StartTime < newObj.EndTime
            && newObj.StartTime < x.EndTime
            );
            if (res)
            {
                error = "This Class Attendance timing is over lapping with previously taken Attendance timing, please check this period Date and Time range!";
                return false;
            }
            return true;
        }
        private bool SaveClassAttendanceSettingLogic(Aca_ClassAttendanceSetting newObj, ref Aca_ClassAttendanceSetting dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Class Attendance Setting to save can't be null!";
                return false;
            }

            if (!IsValidToSaveClassAttendanceSetting(newObj, out error))
                return false;

            bool isNewObject = true;
            Aca_ClassAttendanceSetting objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Aca_ClassAttendanceSetting.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            else
            {
                newObj.Id = BigInt.NewBigInt();
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Aca_ClassAttendanceSetting.GetNew(newObj.Id);
                DbInstance.Aca_ClassAttendanceSetting.Add(objToSave);
                objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
                objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
                objToSave.EmployeeId = HttpUtil.Profile.EmployeeId;//EmployeeId
            }
            //checking permission, enable it with correction

            var isFaq = DbInstance.Aca_ClassTakenByEmployee
                .Any(x => x.ClassId == newObj.ClassId && x.EmployeeId == HttpUtil.Profile.EmployeeId && x.RoleEnumId == (byte)Aca_ClassTakenByEmployee.RoleEnum.Faculty);//EmployeeId
            var permis = PermissionUtil.HasPermission(
                PermissionCollection.AcademicArea.OfferedClassManager.ClassAttendance.CanAddEditAttendance,
                out error);
            if (!isFaq && !permis)
            {
                error = "You don't have permission to update Attendance for this class, you are not faculty of this Class!";
                return false;
            }
            else if (!isNewObject && !permis && objToSave.EmployeeId != HttpUtil.Profile.EmployeeId)//EmployeeId
            {
                error = "You don't have permission to update Attendance of this class, this owned by other faculty!";
                return false;
            }

            //binding object  
            newObj.EndTime= new DateTime(newObj.StartTime.Year, newObj.StartTime.Month, newObj.StartTime.Day, newObj.EndTime.Hour, newObj.EndTime.Minute, newObj.EndTime.Second);
            objToSave.ClassId = newObj.ClassId;
            objToSave.RoutineId = newObj.RoutineId;
            objToSave.StartTime = newObj.StartTime;
            objToSave.EndTime = newObj.EndTime;

            objToSave.ClassTypeEnumId = newObj.ClassTypeEnumId;
            objToSave.IsLocked = newObj.IsLocked;
            objToSave.Remark = newObj.Remark;
            objToSave.LastChangedById = newObj.LastChangedById = HttpUtil.Profile.UserId;
            objToSave.LastChanged = newObj.LastChanged = DateTime.Now;
            dbAddedObj = objToSave;

            //here save any child table
            return true;
        }
        private bool IsValidToSaveClassAttendance(Aca_ClassAttendance newObj, out string error)
        {
            error = "";
            if (newObj.SettingId <= 0)
            {
                error += "Please select valid Setting.";
                return false;
            }
            if (newObj.ClassId <= 0)
            {
                error += "Please select valid Class.";
                return false;
            }
            if (newObj.StudentId <= 0)
            {
                error += "Please select valid Student.";
                return false;
            }
            if (newObj.IsAbsent == null)
            {
                error += "Is Absent required.";
                return false;
            }
            if (newObj.IsLate == null)
            {
                error += "Is Late required.";
                return false;
            }
            if (newObj.IsLeftEarly == null)
            {
                error += "Is Left Early required.";
                return false;
            }


            if (!newObj.StartTime.IsValid())
            {
                error += "Start Time is not valid.";
                return false;
            }
            if (!newObj.EndTime.IsValid())
            {
                error += "End Time is not valid.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.Aca_ClassAttendance.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
            //error = "A ClassAttendance already exists!";
            //return false;
            //}
            return true;
        }
        private bool SaveClassAttendanceLogic(Aca_ClassAttendance newObj, ref Aca_ClassAttendance dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Class Attendance to save can't be null!";
                return false;
            }

            if (!IsValidToSaveClassAttendance(newObj, out error))
                return false;

            bool isNewObject = true;
            Aca_ClassAttendance objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Aca_ClassAttendance.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            else
            {
                newObj.Id = BigInt.NewBigInt();
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Aca_ClassAttendance.GetNew(newObj.Id);
                DbInstance.Aca_ClassAttendance.Add(objToSave);
                objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
                objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            else
            {
                //
                if (objToSave.IsAbsent != newObj.IsAbsent && objToSave.IsAbsent==true)
                {
                    newObj.Remark += $" (Changed Absent to Present on {DateTime.Now.ToString("d/M/yy HH:mm")})";
                }
            }
            //checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ClassAttendanceManager.ClassAttendance.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ClassAttendanceManager.ClassAttendance.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object
            if (newObj.IsAbsent )
            {
                newObj.IsLate = false;
                newObj.IsLeftEarly = false;
            }
            else
            {
                newObj.ReasonEnumId=null;
            }
            objToSave.SettingId = newObj.SettingId;
            objToSave.ClassId = newObj.ClassId;
            objToSave.StudentId = newObj.StudentId;
            objToSave.IsAbsent = newObj.IsAbsent;
            objToSave.IsLate = newObj.IsLate;
            objToSave.IsLeftEarly = newObj.IsLeftEarly;
            objToSave.ReasonEnumId = newObj.ReasonEnumId;
            objToSave.Remark = newObj.Remark;
            objToSave.StartTime = newObj.StartTime;
            objToSave.EndTime = newObj.EndTime;
            objToSave.LastChangedById = newObj.LastChangedById = HttpUtil.Profile.UserId;
            objToSave.LastChanged = newObj.LastChanged = DateTime.Now;
            dbAddedObj = objToSave;
            //here save any child table
            return true;
        }


        private Aca_ClassAttendanceSmsLog GenerateSmsLogForSent(Aca_ClassAttendance attendance, Aca_Class classObj)
        {
            //check if semester ongoing and null
            if (attendance == null || !attendance.IsAbsent || classObj == null ||
                 classObj.Aca_Semester.StatusEnumId != (byte)Aca_Semester.StatusEnum.Ongoing)
            {
                return null;
            }
            var student = DbInstance.User_Student.SingleOrDefault(x => x.Id == attendance.StudentId);
            if (student == null)
                return null;

            Aca_ClassAttendanceSmsLog obj = new Aca_ClassAttendanceSmsLog();
            obj.Id = BigInt.NewBigInt();
            obj.ClassId = attendance.ClassId;
            obj.StudentId = attendance.StudentId;
            obj.AttendanceSettingId = attendance.SettingId;
            obj.SmsText = string.Format(SiteSettings.Instance.AbsentAttenceSmsTemplate, student.ClassRollNo, classObj.Code, attendance.StartTime.ToString("h:mm tt dd-MM-yy")); ;
            obj.MobileNumber = "";//get mobile number
            obj.DeliveryStatus = DeliveryStatusEnum.NotSent;
            obj.DeliveryErrorText = null;
            obj.CreateDate = DateTime.Now;
            obj.CreateById = attendance.LastChangedById;

            return obj;
        }
        //Resent Unsent SMS
        [System.Web.Http.HttpGet]
        private HttpListResult<Aca_ClassAttendanceSmsLog> SentFailureSms()
        {
            var result = new HttpListResult<Aca_ClassAttendanceSmsLog>();
            string error = String.Empty;
            try
            {
                var objList = DbInstance.Aca_ClassAttendanceSmsLog
                    .Where(x => x.DeliveryStatusEnumId == (byte)DeliveryStatusEnum.NotSent
                    && x.MobileNumber != "[Not Available]"
                    && x.MobileNumber != ""
                    )
                    .ToList();

                //Send SMS
                var isSent = Facade.SmsFacade.SendSmsAndSaveLogForAbsentClassAttendance(objList, true, out error);
                if (!isSent)
                {
                    result.HasError = true;
                    result.Errors.Add(error);
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }

            return result;
        }
        [System.Web.Http.HttpGet]
        private HttpListResult<Aca_ClassAttendanceSmsLog> SentFailureSmsTest()
        {
            var result = new HttpListResult<Aca_ClassAttendanceSmsLog>();
            string error = String.Empty;
            try
            {
                var smsSender = new SmsSender();
                var isSent = smsSender.SendSmsByBanglaPhoneApi("01633300999", "Humayon Kabir Pavel", out error); ;
                if (!isSent.IsSentSuccess)
                {
                    result.HasError = true;
                    result.Errors.Add(error);
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
    }
}

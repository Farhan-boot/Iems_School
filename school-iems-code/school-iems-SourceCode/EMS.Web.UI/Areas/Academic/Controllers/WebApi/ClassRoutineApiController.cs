//File: UI Controller
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
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


namespace EMS.Web.UI.Areas.Academic.Controllers.WebApi
{

    public class ClassRoutineApiController : EmployeeBaseApiController
	{
        public ClassRoutineApiController()
        {  }
       
        #region ClassRoutine 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Aca_ClassRoutineJson> GetPagedClassRoutine(string textkey, int? pageSize, int? pageNo
           ,Int64 classId= 0      
           ,Int64 roomId= 0      
           ,Int64 employeeId= 0      
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Aca_ClassRoutineJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.ClassRoutine.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.ClassRoutine.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.ClassRoutine.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                //check if new class
                if (classId <= 0)
                {
                    return result;
                }
                //check if class exist
                var isClassExist = DbInstance.Aca_Class.Any(x => x.Id == classId);
                if (!isClassExist)
                {
                    result.HasError = true;
                    result.Errors.Add("Invalid Class, no Routine found!");
                    return result;
                }
                using (Aca_ClassRoutineDataService classRoutineDataService = new Aca_ClassRoutineDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<Aca_ClassRoutine> query = DbInstance.Aca_ClassRoutine
                    .Include(x=>x.General_Room)
                    .Include(x=>x.General_Room.General_Building)
                    .Include(x=>x.General_Room.General_Building.General_Campus)
                    .Include(x=>x.User_Employee)
                    .Include(x=>x.User_Employee.User_Account)
                    .OrderBy(x => x.DayEnumId)
                    .ThenBy(x => x.StartTime.Hour);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Remarks.ToLower().Contains(textkey.ToLower()));
                    }
                    if (classId>0)
                    {
                        query = query.Where(q => q.ClassId== classId);
                    }  
                    if (roomId>0)
                    {
                        query = query.Where(q => q.RoomId== roomId);
                    }  
                    if (employeeId>0)
                    {
                        query = query.Where(q => q.EmployeeId== employeeId);
                    }  
                 
                    var entities = classRoutineDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Aca_ClassRoutineJson>();
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
        private HttpListResult<Aca_ClassRoutineJson> GetAllClassRoutine()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_ClassRoutineJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.ClassRoutine.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.ClassRoutine.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.ClassRoutine.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_ClassRoutineDataService classRoutineDataService = new Aca_ClassRoutineDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<Aca_ClassRoutineJson>();
                    var entities = classRoutineDataService.GetAll(out error);
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
        public HttpResult<Aca_ClassRoutineJson> GetClassRoutineById(long id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_ClassRoutineJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.ClassRoutine.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.ClassRoutine.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.ClassRoutine.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_ClassRoutineDataService classRoutineDataService = new Aca_ClassRoutineDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Aca_ClassRoutineJson();
                    Aca_ClassRoutine entity;
                    if (id <= 0)
                    {
                        entity = Aca_ClassRoutine.GetNew();
                    }
                    else
                    {
                        entity = classRoutineDataService.GetById(id);
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
        private HttpResult<Aca_ClassRoutineJson> GetClassRoutineByIdWithChild(long id)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_ClassRoutineJson>();
            try
            {
                using (Aca_ClassRoutineDataService classRoutineDataService = new Aca_ClassRoutineDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Aca_ClassRoutineJson();
                    Aca_ClassRoutine entity;
                    if (id <= 0)
                    {
                        entity = Aca_ClassRoutine.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("Aca_ClassRoutine");
                        //includeTables.Add("Aca_ClassRoutine");

                        entity = classRoutineDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Aca_ClassRoutineJson> GetClassRoutineListDataExtra()
        {

            string error = string.Empty;
            var result = new HttpListResult<Aca_ClassRoutineJson>();

            try
            {
                //Aca_ClassRoutineDataService classRoutineDataService =
                //    new Aca_ClassRoutineDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.DayEnumList = EnumUtil.GetEnumList(typeof(EnumCollection.Common.DayEnum));
                //DropDown Option Tables
                 
                //result.DataExtra.ClassList = DbInstance.Aca_Class.AsEnumerable().Select(t => new
                //{ Id = t.Id.ToString(),Name = t.Name }).ToList();
                //result.DataExtra.RoomList = DbInstance.General_Room.AsEnumerable().Select(t => new
                //{ Id = t.Id.ToString(),Name = t.Name }).ToList();
                //result.DataExtra.EmployeeList = DbInstance.User_Employee.AsEnumerable().Select(t => new
                //{ Id = t.Id.ToString(),Name = t.Name }).ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<Aca_ClassRoutineJson> GetClassRoutineDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_ClassRoutineJson>();

            try
            {
                //Aca_ClassRoutineDataService classRoutineDataService =
                //    new Aca_ClassRoutineDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.DayEnumList = EnumUtil.GetEnumList(typeof(EnumCollection.Common.DayEnum));
                //DropDown Option Tables

                //result.DataExtra.ClassList = DbInstance.Aca_Class.AsEnumerable().Select(t => new
                //{ Id = t.Id.ToString(),Name = t.Name }).ToList();
                result.DataExtra.RoomList = DbInstance.General_Room.AsEnumerable().Select(t => new
                { Id = t.Id.ToString(), Name = t.Name }).ToList();
                //result.DataExtra.EmployeeList = DbInstance.User_Employee.AsEnumerable().Select(t => new
                //{ Id = t.Id.ToString(),Name = t.Name }).ToList();
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
        public HttpResult<Aca_ClassRoutineJson> SaveClassRoutine(Aca_ClassRoutineJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_ClassRoutineJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.ClassRoutine.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.ClassRoutine.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                 var entityReceived = new Aca_ClassRoutine();
                 var dbAttachedEntity = new Aca_ClassRoutine();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {
                    if (SaveClassRoutineLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<Aca_ClassRoutineJson> SaveClassRoutine2(Aca_ClassRoutineJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_ClassRoutineJson>();
            try
            {
                using (Aca_ClassRoutineDataService classRoutineDataService = new Aca_ClassRoutineDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new Aca_ClassRoutine();
                    var dbAttachedEntity = new Aca_ClassRoutine();
                    json.Map(entityReceived);
                    if (classRoutineDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        public HttpListResult<Aca_ClassRoutineJson> SaveClassRoutineList(IList<Aca_ClassRoutineJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_ClassRoutineJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.ClassRoutine.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.ClassRoutine.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_ClassRoutineDataService classRoutineDataService = new Aca_ClassRoutineDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<Aca_ClassRoutine>();
                    var dbAttachedListEntity = new List<Aca_ClassRoutine>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (classRoutineDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<Aca_ClassRoutineJson> GetDeleteClassRoutineById(long id)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_ClassRoutineJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.OfferedClassManager.ClassRoutine.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_ClassRoutineDataService classRoutineDataService = new Aca_ClassRoutineDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!classRoutineDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveClassRoutine(Aca_ClassRoutine newObj, out string error)
        {
            error = "";
            if (newObj.DayEnumId==null)
            {
                error += "Please select valid Day.";
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
            if (newObj.ClassId<=0)
            {
                error += "Please select valid Class.";
                return false;
            }

            

            //TODO write your custom validation here.
            var res =  DbInstance.Aca_ClassRoutine
                .Any(x => x.Id != newObj.Id
                && x.ClassId == newObj.ClassId
                && x.DayEnumId == newObj.DayEnumId
                && (x.StartTime.Hour > newObj.StartTime.Hour)
                && (x.EndTime.Hour < newObj.EndTime.Hour)
                );
            if (res)
            {
                error = "This time duration is conflicted for this class!";
                return false;
            }
            return true;
        }
        private bool SaveClassRoutineLogic(Aca_ClassRoutine newObj,ref Aca_ClassRoutine dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Class Routine to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveClassRoutine(newObj, out error))
                return false;

            bool isNewObject = true;
            Aca_ClassRoutine objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Aca_ClassRoutine.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            else
            {    
                newObj.Id = BigInt.NewBigInt();
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Aca_ClassRoutine.GetNew(newObj.Id);
                DbInstance.Aca_ClassRoutine.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ClassRoutineManager.ClassRoutine.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ClassRoutineManager.ClassRoutine.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
           objToSave.DayEnumId =  newObj.DayEnumId ;
           objToSave.StartTime =  newObj.StartTime ;
           objToSave.EndTime =  newObj.EndTime ;
           objToSave.ClassId =  newObj.ClassId ;
           objToSave.RoomId =  newObj.RoomId ;
           objToSave.EmployeeId =  newObj.EmployeeId ;
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

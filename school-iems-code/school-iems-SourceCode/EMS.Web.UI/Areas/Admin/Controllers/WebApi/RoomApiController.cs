//File: UI Controller
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
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


namespace EMS.Web.UI.Areas.Admin.Controllers.WebApi
{

    public class RoomApiController : EmployeeBaseApiController
    {
        public RoomApiController()
        { }

        #region Room 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<General_RoomJson> GetPagedRoom(string textkey, int? pageSize, int? pageNo
           , Int32 statusEnumId = -1
           , Int32 typeEnumId = -1
           , Int32 floorId = -1
           , Int64 campusId = 0
           , Int64 buildingId = 0
           , Int64 departmentId = 0
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<General_RoomJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Room.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Room.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Room.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (General_RoomDataService roomDataService = new General_RoomDataService(DbInstance, HttpUtil.Profile))
                {
                    IQueryable<General_Room> query = DbInstance.General_Room
                          .Include(x => x.General_Building)
                          .Include(x => x.General_Building.General_Campus)
                          .Include(x => x.General_Floor)
                          .Include(x => x.HR_Department)
                          .OrderBy(x => x.General_Building.General_Campus.OrderNo)
                          .ThenBy(x => x.General_Building.OrderNo)
                          .ThenBy(x => x.General_Floor.FloorNo)
                          .ThenBy(x => x.RoomNo)
                          .ThenBy(x => x.OrderNo);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.RoomNo.ToLower().Contains(textkey.ToLower()));
                    }
                    if (typeEnumId >= 0)
                    {
                        query = query.Where(q => q.TypeEnumId == (byte)typeEnumId);
                    }
                    if (statusEnumId >= 0)
                    {
                        query = query.Where(q => q.StatusEnumId == (byte)statusEnumId);
                    }
                    if (floorId >= 0)
                    {
                        query = query.Where(q => q.FloorId == floorId);
                    }
                    if (buildingId > 0)
                    {
                        query = query.Where(q => q.BuildingId == buildingId);
                    }
                    if (campusId > 0)
                    {
                        query = query.Where(q => q.General_Building.CampusId == (byte)campusId);
                    }
                    if (departmentId > 0)
                    {
                        query = query.Where(q => q.DepartmentId == departmentId);
                    }

                    var entities = roomDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<General_RoomJson>();
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
        public HttpListResult<General_RoomJson> GetAllRoom(string roomNo, string roomName
           , DateTime? startDateTime
           , DateTime? endDateTime
           , Int32 statusEnumId = -1
           , Int32 typeEnumId = -1
           , Int32 floorId = -1
           , Int64 campusId = 0
           , Int64 buildingId = 0
           , Int64 departmentId = 0
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<General_RoomJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Room.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Room.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Room.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            var allClassRoutineList = new List<Aca_ClassRoutine>();
            try
            {
                using (General_RoomDataService roomDataService = new General_RoomDataService(DbInstance, HttpUtil.Profile))
                {
                    IQueryable<General_Room> query = DbInstance.General_Room
                          .Include(x => x.General_Building)
                          .Include(x => x.General_Building.General_Campus)
                          .Include(x => x.General_Floor)
                          .Include(x => x.HR_Department)
                          .OrderBy(x => x.General_Building.General_Campus.OrderNo)
                          .ThenBy(x => x.General_Building.OrderNo)
                          .ThenBy(x => x.General_Floor.FloorNo)
                          .ThenBy(x => x.RoomNo)
                          .ThenBy(x => x.OrderNo);
                    if (roomName.IsValid())
                    {
                        query = query.Where(q => q.Name.ToLower().Contains(roomName.ToLower()));
                    }
                    if (roomNo.IsValid())
                    {
                        query = query.Where(q => q.RoomNo.ToLower().Contains(roomNo.ToLower()));
                    }
                    if (typeEnumId >= 0)
                    {
                        query = query.Where(q => q.TypeEnumId == (byte)typeEnumId);
                    }
                    if (statusEnumId >= 0)
                    {
                        query = query.Where(q => q.StatusEnumId == (byte)statusEnumId);
                    }
                    if (floorId >= 0)
                    {
                        query = query.Where(q => q.FloorId == floorId);
                    }
                    if (buildingId > 0)
                    {
                        query = query.Where(q => q.BuildingId == buildingId);
                    }
                    if (campusId > 0)
                    {
                        query = query.Where(q => q.General_Building.CampusId == (byte)campusId);
                    }
                    if (departmentId > 0)
                    {
                        query = query.Where(q => q.DepartmentId == departmentId);
                    }

                    var entities = roomDataService.GetPagedList(query, null, null, ref count, out error);

                    if (startDateTime == null)
                    {
                        startDateTime = DateTime.Now.Date.AddHours(8);
                    }
                    if (endDateTime == null)
                    {
                        endDateTime = DateTime.Now.Date.AddHours(20);
                    }
                    result.DataExtra.StartDateTime = startDateTime;
                    result.DataExtra.EndDateTime = endDateTime;

                    DateTime dateStart = (DateTime)startDateTime;
                    DateTime dateEnd = (DateTime)endDateTime;
                    var acaSemesterList = DbInstance.Aca_Semester
                        .Where(
                            x => x.StartDate <= endDateTime
                            && x.EndDate >= startDateTime
                        ).ToList();
                    bool haveToCheckInClassRoutine = false;
                    if (acaSemesterList.Any())
                    {
                        haveToCheckInClassRoutine = true;
                    }
                    var typeEnumList = EnumUtil.GetEnumList(typeof(EnumCollection.Common.DayEnum));
                    EnumCollection.Common.DayEnum dayStart = (EnumCollection.Common.DayEnum)typeEnumList.Single(x=>x.Name.ToLower()==dateStart.DayOfWeek.ToString().ToLower()).Id;
                    EnumCollection.Common.DayEnum dayEnd = (EnumCollection.Common.DayEnum)typeEnumList.Single(x => x.Name.ToLower()==dateEnd.DayOfWeek.ToString().ToLower()).Id;
                    int totalDays = dateEnd.Subtract(dateStart).Days;

                    
                    foreach (var entity in entities)
                    {
                        if (haveToCheckInClassRoutine)
                        {
                            IQueryable<Aca_ClassRoutine> queryClassRoutine = DbInstance.Aca_ClassRoutine;
                            queryClassRoutine = queryClassRoutine.Where(x => x.RoomId == entity.Id);
                            if (totalDays<=6 || (totalDays <= 6 && dayStart == dayEnd))
                            {
                                queryClassRoutine = queryClassRoutine.Where(x => x.DayEnumId >= (byte) dayStart && x.DayEnumId <= (byte) dayEnd);
                            }
                            foreach (var acaSemester in acaSemesterList)
                            {
                                IQueryable<Aca_ClassRoutine> queryNewClassRoutine = queryClassRoutine.Where(x => x.Aca_Class.SemesterId == acaSemester.Id);
                                var classRoutineList = queryNewClassRoutine.ToList();
                                if (classRoutineList.Any())
                                {
                                    entity.IsSelected = true;
                                    allClassRoutineList.AddRange(classRoutineList);
                                }
                                else
                                {
                                    entity.IsSelected = false;
                                }
                            }

                        }
                        else
                        {
                            entity.IsSelected = false;
                        }
                    }
                    var jsons = new List<General_RoomJson>();
                    entities.Map(jsons);
                    result.Data = jsons;
                    result.Count = count;

                    var classRoutineJsons = new List<Aca_ClassRoutineJson>();
                    allClassRoutineList.Map(classRoutineJsons);
                    result.DataExtra.ClassRoutineList = classRoutineJsons;
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
        private HttpListResult<General_RoomJson> GetAllRoom()
        {
            string error = string.Empty;
            var result = new HttpListResult<General_RoomJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Room.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Room.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Room.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (General_RoomDataService roomDataService = new General_RoomDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<General_RoomJson>();
                    var entities = roomDataService.GetAll(out error);
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
        public HttpResult<General_RoomJson> GetRoomById(long id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<General_RoomJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Room.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Room.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Room.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (General_RoomDataService roomDataService = new General_RoomDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new General_RoomJson();
                    General_Room entity;
                    if (id <= 0)
                    {
                        entity = General_Room.GetNew();
                    }
                    else
                    {
                        entity = roomDataService.GetById(id);
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
        private HttpResult<General_RoomJson> GetRoomByIdWithChild(long id)
        {
            string error = string.Empty;
            var result = new HttpResult<General_RoomJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Room.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Room.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Room.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (General_RoomDataService roomDataService = new General_RoomDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new General_RoomJson();
                    General_Room entity;
                    if (id <= 0)
                    {
                        entity = General_Room.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("General_Room");
                        //includeTables.Add("General_Room");

                        entity = roomDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<General_RoomJson> GetRoomListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<General_RoomJson>();
            try
            {
                //General_RoomDataService roomDataService =
                //    new General_RoomDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List

                result.DataExtra.TypeEnumList = EnumUtil.GetEnumList(typeof(General_Room.TypeEnum));
                result.DataExtra.StatusEnumList = EnumUtil.GetEnumList(typeof(General_Room.StatusEnum));
                //DropDown Option Tables
                result.DataExtra.CampusList = DbInstance.General_Campus.AsEnumerable()
                    .OrderBy(x => x.OrderNo)
                    .Select(t => new
                    { Id = t.Id, Name = t.Name, ShortName = t.ShortName })
                    .ToList();
                result.DataExtra.BuildingList = DbInstance.General_Building
                    .Include(x => x.General_Campus)
                    .AsEnumerable()
                    .OrderBy(x => x.OrderNo)
                    .Select(t => new
                    {   Id = t.Id.ToString()
                        ,Name = t.Name
                        ,ShortName = t.ShortName
                        ,CampusId = t.CampusId
                        ,CampusName = t.General_Campus.Name
                        ,CampusShortName = t.General_Campus.ShortName
                    })
                    .ToList();
                result.DataExtra.FloorList = DbInstance.General_Floor.AsEnumerable()
                    .OrderBy(x => x.FloorNo)
                    .Select(t => new
                    { Id = t.Id, FloorNo = t.FloorNo, Name = t.Name })
                    .ToList();
                result.DataExtra.DepartmentList = DbInstance.HR_Department.AsEnumerable()
                    .OrderBy(x => x.Name)
                    .Select(t => new
                    { Id = t.Id, Name = t.Name, ShortName = t.ShortName, Type = t.Type.ToString() })
                    .ToList();
                result.DataExtra.StartDateTime = DateTime.Now.Date.AddHours(8);
                result.DataExtra.EndDateTime = DateTime.Now.Date.AddHours(20);
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<General_RoomJson> GetRoomDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<General_RoomJson>();
            try
            {
                //General_RoomDataService roomDataService =
                //    new General_RoomDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List

                result.DataExtra.TypeEnumList = EnumUtil.GetEnumList(typeof(General_Room.TypeEnum));
                result.DataExtra.StatusEnumList = EnumUtil.GetEnumList(typeof(General_Room.StatusEnum));
                //DropDown Option Tables
                result.DataExtra.CampusList = DbInstance.General_Campus.AsEnumerable()
                    .OrderBy(x => x.OrderNo)
                    .Select(t => new
                    { Id = t.Id, Name = t.Name, ShortName = t.ShortName })
                    .ToList();
                result.DataExtra.BuildingList = DbInstance.General_Building
                    .Include(x => x.General_Campus)
                    .AsEnumerable()
                    .OrderBy(x => x.OrderNo)
                    .Select(t => new
                    {   Id = t.Id.ToString()
                        ,Name = t.Name
                        ,ShortName = t.ShortName
                        ,CampusId = t.CampusId
                        ,CampusName = t.General_Campus.Name
                        ,CampusShortName = t.General_Campus.ShortName
                    })
                    .ToList();
                result.DataExtra.FloorList = DbInstance.General_Floor.AsEnumerable()
                    .OrderBy(x => x.FloorNo)
                    .Select(t => new
                    { Id = t.Id, FloorNo = t.FloorNo, Name = t.Name })
                    .ToList();
                result.DataExtra.DepartmentList = DbInstance.HR_Department.AsEnumerable()
                    .OrderBy(x => x.Name)
                    .Select(t => new
                    { Id = t.Id, Name = t.Name, ShortName = t.ShortName, Type = t.Type.ToString() })
                    .ToList();
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
        public HttpResult<General_RoomJson> SaveRoom(General_RoomJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<General_RoomJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Room.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Room.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                var entityReceived = new General_Room();
                var dbAttachedEntity = new General_Room();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {
                    if (SaveRoomLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<General_RoomJson> SaveRoom2(General_RoomJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<General_RoomJson>();
            try
            {
                using (General_RoomDataService roomDataService = new General_RoomDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new General_Room();
                    var dbAttachedEntity = new General_Room();
                    json.Map(entityReceived);
                    if (roomDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<General_RoomJson> SaveRoomList(IList<General_RoomJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<General_RoomJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Room.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Room.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (General_RoomDataService roomDataService = new General_RoomDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<General_Room>();
                    var dbAttachedListEntity = new List<General_Room>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (roomDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<General_RoomJson> GetDeleteRoomById(long id)
        {
            string error = string.Empty;
            var result = new HttpResult<General_RoomJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Room.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (General_RoomDataService roomDataService = new General_RoomDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!roomDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveRoom(General_Room newObj, out string error)
        {
            error = "";
            if (!newObj.RoomNo.IsValid())
            {
                error += "Room No is not valid.";
                return false;
            }
            if (newObj.RoomNo == "0")
            {
                error += "Room No can not only zero.";
                return false;
            }
            if (newObj.RoomNo.Length > 50)
            {
                error += "Room No exceeded its maximum length 50.";
                return false;
            }
            if (!newObj.Name.IsValid())
            {
                error += "Name is not valid.";
                return false;
            }
            if (newObj.Name.Length > 50)
            {
                error += "Name exceeded its maximum length 50.";
                return false;
            }
            if (newObj.OrderNo == null)
            {
                error += "Order No required.";
                return false;
            }
            if (newObj.FloorId == null)
            {
                error += "Please select valid Floor.";
                return false;
            }
            if (newObj.CapacityRegular == null)
            {
                error += "Regular Capacity required.";
                return false;
            }
            //if (newObj.CapacityRegular<=0)
            //{
            //    error += "Regular Capacity should be greater than zero.";
            //    return false;
            //}
            if (newObj.CapacityAdmission == null)
            {
                error += "Admission Capacity required.";
                return false;
            }
            //if (newObj.CapacityAdmission <= 0)
            //{
            //    error += "Admission Capacity should be greater than zero..";
            //    return false;
            //}
            if (newObj.TypeEnumId == null)
            {
                error += "Please select valid Type.";
                return false;
            }
            if (newObj.StatusEnumId == null)
            {
                error += "Please select valid Status.";
                return false;
            }
            if (newObj.BuildingId == null)
            {
                error += "Please select valid Building.";
                return false;
            }
            if (newObj.DepartmentId == null)
            {
                error += "Please select valid Department.";
                return false;
            }
            if (newObj.Length == null)
            {
                error += "Length required.";
                return false;
            }
            if (newObj.Width == null)
            {
                error += "Width required.";
                return false;
            }

            if (newObj.IsShareable == null)
            {
                error += "Is Shareable required.";
                return false;
            }
            if (newObj.IsDeleted == null)
            {
                error += "Is Deleted required.";
                return false;
            }
            //TODO write your custom validation here.
            var resRoomNo = DbInstance.General_Room
                .Any(x => x.Id != newObj.Id
                && x.BuildingId == newObj.BuildingId
                && x.RoomNo == newObj.RoomNo
                );
            if (resRoomNo)
            {
                error = "A Room already exists with this Room No in this Building!";
                return false;
            }
            var resName = DbInstance.General_Room
                .Any(x => x.Id != newObj.Id
                && x.BuildingId == newObj.BuildingId
                && x.RoomNo == newObj.RoomNo
                && x.Name == newObj.Name
                );
            if (resName)
            {
                error = "A Room already exists with this Name and No. in this Building!";
                return false;
            }
            return true;
        }
        private bool SaveRoomLogic(General_Room newObj, ref General_Room dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Room to save can't be null!";
                return false;
            }

            if (!IsValidToSaveRoom(newObj, out error))
                return false;

            bool isNewObject = true;
            General_Room objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.General_Room.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            else
            {
                newObj.Id = BigInt.NewBigInt();
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = General_Room.GetNew(newObj.Id);
                DbInstance.General_Room.Add(objToSave);
                objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
                objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //checking permission, enable it with correction
            if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Room.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Room.CanEdit,
                   out error))
            {
                return false;
            }

            //binding object  
            objToSave.RoomNo = newObj.RoomNo;
            objToSave.Name = newObj.Name;
            objToSave.OrderNo = newObj.OrderNo;
            objToSave.FloorId = newObj.FloorId;
            objToSave.CapacityRegular = newObj.CapacityRegular;
            objToSave.CapacityAdmission = newObj.CapacityAdmission;
            objToSave.TypeEnumId = newObj.TypeEnumId;
            objToSave.StatusEnumId = newObj.StatusEnumId;
            objToSave.BuildingId = newObj.BuildingId;
            objToSave.Length = newObj.Length;
            objToSave.Width = newObj.Width;
            objToSave.DepartmentId = newObj.DepartmentId;
            objToSave.IsShareable = newObj.IsShareable;
            objToSave.Remarks = newObj.Remarks;
            objToSave.IsDeleted = newObj.IsDeleted;
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

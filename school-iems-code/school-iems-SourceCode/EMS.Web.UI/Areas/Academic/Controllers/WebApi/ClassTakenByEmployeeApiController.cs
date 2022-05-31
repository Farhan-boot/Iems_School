//File: UI Controller
using System;
using System.Collections.Generic;
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
using EMS.Framework.Objects;
using EMS.Framework.Permissions;
//using EMS.Web.Jsons.Mapper;
using EMS.Web.Jsons.Models;

//remark only used SaveClassTakenByEmployeeList
namespace EMS.Web.UI.Areas.Academic.Controllers.WebApi
{

    public class ClassTakenByEmployeeApiController : EmployeeBaseApiController
	{
        public ClassTakenByEmployeeApiController()
        {  }
       
        #region ClassTakenByEmployee 
        #region List View Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Aca_ClassTakenByEmployeeJson> GetPagedClassTakenByEmployee(string textkey, int? pageSize, int? pageNo
           ,Int64 classId= 0      
           ,Int64 employeeId= 0      
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Aca_ClassTakenByEmployeeJson>();

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
                using (Aca_ClassTakenByEmployeeDataService classTakenByEmployeeDataService = new Aca_ClassTakenByEmployeeDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<Aca_ClassTakenByEmployee> query = DbInstance.Aca_ClassTakenByEmployee.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Remarks.ToLower().Contains(textkey.ToLower()));
                    }
                    if (classId>0)
                    {
                        query = query.Where(q => q.ClassId== classId);
                    }  
                    if (employeeId>0)
                    {
                        query = query.Where(q => q.EmployeeId== employeeId);
                    }  
                 
                    var entities = classTakenByEmployeeDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Aca_ClassTakenByEmployeeJson>();
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
        public HttpListResult<Aca_ClassTakenByEmployeeJson> GetClassTakenByEmployeeListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_ClassTakenByEmployeeJson>();
            try
            {
                //Aca_ClassTakenByEmployeeDataService classTakenByEmployeeDataService =
                //    new Aca_ClassTakenByEmployeeDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.RoleEnumList = EnumUtil.GetEnumList(typeof(Aca_ClassTakenByEmployee.RoleEnum));
                result.DataExtra.StatusEnumList = EnumUtil.GetEnumList(typeof(Aca_ClassTakenByEmployee.StatusEnum));
                result.DataExtra.SectionEnumList = EnumUtil.GetEnumList(typeof(Aca_ClassTakenByEmployee.SectionEnum));
                //DropDown Option Tables
                 
                //result.DataExtra.ClassList = DbInstance.Aca_Class.AsEnumerable().Select(t => new
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
        /// <summary>
        /// Caution: Very Dangerous to call When table have huge data!
        /// </summary>
        /// <returns></returns>
        private HttpListResult<Aca_ClassTakenByEmployeeJson> GetAllClassTakenByEmployee()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_ClassTakenByEmployeeJson>();
            try
            {
                using (Aca_ClassTakenByEmployeeDataService classTakenByEmployeeDataService = new Aca_ClassTakenByEmployeeDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<Aca_ClassTakenByEmployeeJson>();
                    var entities = classTakenByEmployeeDataService.GetAll(out error);
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
        /// <summary>
        /// Todo pending to implement bulk save
        /// </summary>
        /// <param name="jsonList"></param>
        /// <returns></returns>
        [HttpPost]
        public HttpListResult<Aca_ClassTakenByEmployeeJson> SaveClassTakenByEmployeeList(IList<Aca_ClassTakenByEmployeeJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_ClassTakenByEmployeeJson>();
            try
            {
                using (Aca_ClassTakenByEmployeeDataService classTakenByEmployeeDataService = new Aca_ClassTakenByEmployeeDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<Aca_ClassTakenByEmployee>();
                    var dbAttachedListEntity = new List<Aca_ClassTakenByEmployee>();
                    jsonList.Map(entityListReceived);

                    foreach (var entity in entityListReceived)
                    {
                        if (!classTakenByEmployeeDataService.Save(entity, out error))
                        {
                            result.HasError = true;
                            result.Errors.Add(error);
                        }
                    }
                    entityListReceived.Map(jsonList);
                    result.Data = jsonList;
                    //if (classTakenByEmployeeDataService.Save(entity, ref dbAttachedListEntity, out error))
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

        #endregion

        #region Add/Edit View Api
        #region Get
        public HttpResult<Aca_ClassTakenByEmployeeJson> GetClassTakenByEmployeeById(long id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_ClassTakenByEmployeeJson>();
            try
            {
                using (Aca_ClassTakenByEmployeeDataService classTakenByEmployeeDataService = new Aca_ClassTakenByEmployeeDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Aca_ClassTakenByEmployeeJson();
                    Aca_ClassTakenByEmployee entity;
                    if (id <= 0)
                    {
                        entity = Aca_ClassTakenByEmployee.GetNew();
                    }
                    else
                    {
                        entity = classTakenByEmployeeDataService.GetById(id);
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
        private HttpResult<Aca_ClassTakenByEmployeeJson> GetClassTakenByEmployeeByIdWithChild(long id)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_ClassTakenByEmployeeJson>();
            try
            {
                using (Aca_ClassTakenByEmployeeDataService classTakenByEmployeeDataService = new Aca_ClassTakenByEmployeeDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Aca_ClassTakenByEmployeeJson();
                    Aca_ClassTakenByEmployee entity;
                    if (id <= 0)
                    {
                        entity = Aca_ClassTakenByEmployee.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement 

                        //includeTables.Add("Aca_ClassTakenByEmployee");
                        //includeTables.Add("Aca_ClassTakenByEmployee");

                        entity = classTakenByEmployeeDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Aca_ClassTakenByEmployeeJson> GetClassTakenByEmployeeDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_ClassTakenByEmployeeJson>();
            try
            {
                //Aca_ClassTakenByEmployeeDataService classTakenByEmployeeDataService =
                //    new Aca_ClassTakenByEmployeeDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.RoleEnumList = EnumUtil.GetEnumList(typeof(Aca_ClassTakenByEmployee.RoleEnum));
                result.DataExtra.StatusEnumList = EnumUtil.GetEnumList(typeof(Aca_ClassTakenByEmployee.StatusEnum));
                result.DataExtra.SectionEnumList = EnumUtil.GetEnumList(typeof(Aca_ClassTakenByEmployee.SectionEnum));
                //DropDown Option Tables
                 
                //result.DataExtra.ClassList = DbInstance.Aca_Class.AsEnumerable().Select(t => new
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
        #endregion

        #region Save/Delete
        [HttpPost]
        private HttpResult<Aca_ClassTakenByEmployeeJson> SaveClassTakenByEmployee(Aca_ClassTakenByEmployeeJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_ClassTakenByEmployeeJson>();
            try
            {
                 var entityReceived = new Aca_ClassTakenByEmployee();
                 var dbAttachedEntity = new Aca_ClassTakenByEmployee();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {
                    if (SaveClassTakenByEmployeeLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<Aca_ClassTakenByEmployeeJson> SaveClassTakenByEmployee2(Aca_ClassTakenByEmployeeJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_ClassTakenByEmployeeJson>();
            try
            {
                using (Aca_ClassTakenByEmployeeDataService classTakenByEmployeeDataService = new Aca_ClassTakenByEmployeeDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new Aca_ClassTakenByEmployee();
                    var dbAttachedEntity = new Aca_ClassTakenByEmployee();
                    json.Map(entityReceived);
                    if (classTakenByEmployeeDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<Aca_ClassTakenByEmployeeJson> GetDeleteClassTakenByEmployeeById(long id)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_ClassTakenByEmployeeJson>();
            try
            {
                using (Aca_ClassTakenByEmployeeDataService classTakenByEmployeeDataService = new Aca_ClassTakenByEmployeeDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!classTakenByEmployeeDataService.DeleteById(id, out error))
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
        #endregion

        #region Local Method (should be always private)
        private bool IsValidToSaveClassTakenByEmployee(Aca_ClassTakenByEmployee newObj, out string error)
        {
            error = "";
            if (newObj.EmployeeId<=0)
            {
                error += "Please select valid Employee.";
                return false;
            }
            if (newObj.ClassId<=0)
            {
                error += "Please select valid Class.";
                return false;
            }
            if (newObj.RoleEnumId==null)
            {
                error += "Please select valid Role.";
                return false;
            }
            if (newObj.StatusEnumId==null)
            {
                error += "Please select valid Status.";
                return false;
            }
            if (newObj.SectionEnumId==null)
            {
                error += "Please select valid Section.";
                return false;
            }

            //TODO write your custom validation here.
            //var res =  DbInstance.Aca_ClassTakenByEmployee.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A ClassTakenByEmployee already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveClassTakenByEmployeeLogic(Aca_ClassTakenByEmployee newObj,ref Aca_ClassTakenByEmployee dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Class Taken By Employee to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveClassTakenByEmployee(newObj, out error))
                return false;

            bool isNewObject = true;
            Aca_ClassTakenByEmployee objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Aca_ClassTakenByEmployee.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            else
            {    
                newObj.Id = BigInt.NewBigInt();
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Aca_ClassTakenByEmployee.GetNew(newObj.Id);
                DbInstance.Aca_ClassTakenByEmployee.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ClassTakenByEmployeeManager.ClassTakenByEmployee.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ClassTakenByEmployeeManager.ClassTakenByEmployee.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
           objToSave.EmployeeId =  newObj.EmployeeId ;
           objToSave.ClassId =  newObj.ClassId ;
           objToSave.RoleEnumId =  newObj.RoleEnumId ;
           objToSave.StatusEnumId =  newObj.StatusEnumId ;
           objToSave.SectionEnumId =  newObj.SectionEnumId ;
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

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
//using EMS.Web.Jsons.Mapper;
using EMS.Web.Jsons.Models;

//remark only used SaveClassTakenByStudentList
namespace EMS.Web.UI.Areas.Academic.Controllers.WebApi
{

    public class ClassTakenByStudentApiController : EmployeeBaseApiController
	{
        public ClassTakenByStudentApiController()
        {  }
       
        #region ClassTakenByStudent 
        #region List View Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Aca_ClassTakenByStudentJson> GetPagedClassTakenByStudent(string textkey, int? pageSize, int? pageNo
           ,Int64 classId= 0      
           ,Int64 studentId= 0      
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Aca_ClassTakenByStudentJson>();
            try
            {
                using (Aca_ClassTakenByStudentDataService classTakenByStudentDataService = new Aca_ClassTakenByStudentDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<Aca_ClassTakenByStudent> query = DbInstance.Aca_ClassTakenByStudent.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Remarks.ToLower().Contains(textkey.ToLower()));
                    }
                    if (classId>0)
                    {
                        query = query.Where(q => q.ClassId== classId);
                    }  
                    if (studentId>0)
                    {
                        query = query.Where(q => q.StudentId== studentId);
                    }  
                 
                    var entities = classTakenByStudentDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Aca_ClassTakenByStudentJson>();
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
        public HttpListResult<Aca_ClassTakenByStudentJson> GetClassTakenByStudentListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_ClassTakenByStudentJson>();
            try
            {
                //Aca_ClassTakenByStudentDataService classTakenByStudentDataService =
                //    new Aca_ClassTakenByStudentDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.EnrollTypeEnumList = EnumUtil.GetEnumList(typeof(Aca_ClassTakenByStudent.EnrollTypeEnum));
                result.DataExtra.RegistrationStatusEnumList = EnumUtil.GetEnumList(typeof(Aca_ClassTakenByStudent.RegistrationStatusEnum));
                result.DataExtra.StatusEnumList = EnumUtil.GetEnumList(typeof(Aca_ClassTakenByStudent.StatusEnum));
                //DropDown Option Tables
                 
                //result.DataExtra.ClassList = DbInstance.Aca_Class.AsEnumerable().Select(t => new
                //{ Id = t.Id.ToString(),Name = t.Name }).ToList();
                //result.DataExtra.StudentList = DbInstance.User_Student.AsEnumerable().Select(t => new
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
        private HttpListResult<Aca_ClassTakenByStudentJson> GetAllClassTakenByStudent()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_ClassTakenByStudentJson>();
            try
            {
                using (Aca_ClassTakenByStudentDataService classTakenByStudentDataService = new Aca_ClassTakenByStudentDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<Aca_ClassTakenByStudentJson>();
                    var entities = classTakenByStudentDataService.GetAll(out error);
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
        public HttpListResult<Aca_ClassTakenByStudentJson> SaveClassTakenByStudentList(IList<Aca_ClassTakenByStudentJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_ClassTakenByStudentJson>();
            try
            {
                using (Aca_ClassTakenByStudentDataService classTakenByStudentDataService = new Aca_ClassTakenByStudentDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<Aca_ClassTakenByStudent>();
                    var dbAttachedListEntity = new List<Aca_ClassTakenByStudent>();
                    jsonList.Map(entityListReceived);

                    foreach (var entity in entityListReceived)
                    {
                        if (!classTakenByStudentDataService.Save(entity, out error))
                        {
                            result.HasError = true;
                            result.Errors.Add(error);
                        }
                    }
                    entityListReceived.Map(jsonList);
                    result.Data = jsonList;
                    //if (classTakenByStudentDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<Aca_ClassTakenByStudentJson> GetClassTakenByStudentById(long id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_ClassTakenByStudentJson>();
            try
            {
                using (Aca_ClassTakenByStudentDataService classTakenByStudentDataService = new Aca_ClassTakenByStudentDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Aca_ClassTakenByStudentJson();
                    Aca_ClassTakenByStudent entity;
                    if (id <= 0)
                    {
                        entity = Aca_ClassTakenByStudent.GetNew();
                    }
                    else
                    {
                        entity = classTakenByStudentDataService.GetById(id);
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
        private HttpResult<Aca_ClassTakenByStudentJson> GetClassTakenByStudentByIdWithChild(long id)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_ClassTakenByStudentJson>();
            try
            {
                using (Aca_ClassTakenByStudentDataService classTakenByStudentDataService = new Aca_ClassTakenByStudentDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Aca_ClassTakenByStudentJson();
                    Aca_ClassTakenByStudent entity;
                    if (id <= 0)
                    {
                        entity = Aca_ClassTakenByStudent.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("Aca_ClassTakenByStudent");
                        //includeTables.Add("Aca_ClassTakenByStudent");

                        entity = classTakenByStudentDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Aca_ClassTakenByStudentJson> GetClassTakenByStudentDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_ClassTakenByStudentJson>();
            try
            {
                //Aca_ClassTakenByStudentDataService classTakenByStudentDataService =
                //    new Aca_ClassTakenByStudentDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.EnrollTypeEnumList = EnumUtil.GetEnumList(typeof(Aca_ClassTakenByStudent.EnrollTypeEnum));
                result.DataExtra.RegistrationStatusEnumList = EnumUtil.GetEnumList(typeof(Aca_ClassTakenByStudent.RegistrationStatusEnum));
                result.DataExtra.StatusEnumList = EnumUtil.GetEnumList(typeof(Aca_ClassTakenByStudent.StatusEnum));
                //DropDown Option Tables
                 
                //result.DataExtra.ClassList = DbInstance.Aca_Class.AsEnumerable().Select(t => new
                //{ Id = t.Id.ToString(),Name = t.Name }).ToList();
                //result.DataExtra.StudentList = DbInstance.User_Student.AsEnumerable().Select(t => new
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
        private HttpResult<Aca_ClassTakenByStudentJson> SaveClassTakenByStudent(Aca_ClassTakenByStudentJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_ClassTakenByStudentJson>();
            try
            {
                 var entityReceived = new Aca_ClassTakenByStudent();
                 var dbAttachedEntity = new Aca_ClassTakenByStudent();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {
                    if (SaveClassTakenByStudentLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<Aca_ClassTakenByStudentJson> SaveClassTakenByStudent2(Aca_ClassTakenByStudentJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_ClassTakenByStudentJson>();
            try
            {
                using (Aca_ClassTakenByStudentDataService classTakenByStudentDataService = new Aca_ClassTakenByStudentDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new Aca_ClassTakenByStudent();
                    var dbAttachedEntity = new Aca_ClassTakenByStudent();
                    json.Map(entityReceived);
                    if (classTakenByStudentDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<Aca_ClassTakenByStudentJson> GetDeleteClassTakenByStudentById(long id)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_ClassTakenByStudentJson>();
            try
            {
                using (Aca_ClassTakenByStudentDataService classTakenByStudentDataService = new Aca_ClassTakenByStudentDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!classTakenByStudentDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveClassTakenByStudent(Aca_ClassTakenByStudent newObj, out string error)
        {
            error = "";
            if (newObj.StudentId<=0)
            {
                error += "Please select valid Student.";
                return false;
            }
            if (newObj.ClassId<=0)
            {
                error += "Please select valid Class.";
                return false;
            }
            if (newObj.EnrollTypeEnumId==null)
            {
                error += "Please select valid Enroll Type.";
                return false;
            }
            if (newObj.RegistrationStatusEnumId==null)
            {
                error += "Please select valid Registration Status.";
                return false;
            }
            if (newObj.StatusEnumId==null)
            {
                error += "Please select valid Status.";
                return false;
            }



            //TODO write your custom validation here.
            //var res =  DbInstance.Aca_ClassTakenByStudent.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A ClassTakenByStudent already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveClassTakenByStudentLogic(Aca_ClassTakenByStudent newObj,ref Aca_ClassTakenByStudent dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Class Taken By Student to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveClassTakenByStudent(newObj, out error))
                return false;

            bool isNewObject = true;
            Aca_ClassTakenByStudent objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Aca_ClassTakenByStudent.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            else
            {    
                newObj.Id = BigInt.NewBigInt();
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Aca_ClassTakenByStudent.GetNew(newObj.Id);
                DbInstance.Aca_ClassTakenByStudent.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ClassTakenByStudentManager.ClassTakenByStudent.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ClassTakenByStudentManager.ClassTakenByStudent.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
           objToSave.StudentId =  newObj.StudentId ;
           objToSave.ClassId =  newObj.ClassId ;
           objToSave.EnrollTypeEnumId =  newObj.EnrollTypeEnumId ;
           objToSave.RegistrationStatusEnumId =  newObj.RegistrationStatusEnumId ;
           objToSave.StatusEnumId =  newObj.StatusEnumId ;
           objToSave.TotalMark =  newObj.TotalMark ;
           objToSave.GradeId =  newObj.GradeId ;
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

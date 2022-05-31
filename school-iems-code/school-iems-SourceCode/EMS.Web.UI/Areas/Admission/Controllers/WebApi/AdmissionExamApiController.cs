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
using EMS.Framework.Objects;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases;
using EMS.Web.Framework.Bases.WebApi;
//using EMS.Web.Framework.Objects;
//using EMS.Web.Jsons.Mapper;
using EMS.Web.Jsons.Models;
using EMS.Framework.Permissions;
using EMS.Framework.Settings;
using EMS.Web.Jsons.Custom.Admission.AdmissionExam;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

namespace EMS.Web.UI.Areas.Admission.Controllers.WebApi
{

    public class AdmissionExamApiController : EmployeeBaseApiController
    {

        public AdmissionExamApiController()
        { }

        #region AdmissionExam 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Adm_AdmissionExamJson> GetPagedAdmissionExam(string textkey, int? pageSize, int? pageNo
           , Int64 semesterId = 0
           , int selectedSemesterDurationEnumId = 0
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Adm_AdmissionExamJson>();


            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.AdmissionExam.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.AdmissionExam.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.AdmissionExam.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Adm_AdmissionExamDataService admissionExamDataService = new Adm_AdmissionExamDataService(DbInstance, HttpUtil.Profile))
                {
                    IQueryable<Adm_AdmissionExam> query = DbInstance.Adm_AdmissionExam.Include(x=>x.Aca_Semester).OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Name.ToLower().Contains(textkey.ToLower()));
                    }
                    if (semesterId > 0)
                    {
                        query = query.Where(q => q.SemesterId == semesterId);
                    }

                    if (selectedSemesterDurationEnumId > 0)
                    {
                        query = query.Where(
                            q => q.Aca_Semester.SemesterDurationEnumId == selectedSemesterDurationEnumId);
                    }

                    var entities = admissionExamDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Adm_AdmissionExamJson>();
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
        private HttpListResult<Adm_AdmissionExamJson> GetAllAdmissionExam()
        {
            string error = string.Empty;
            var result = new HttpListResult<Adm_AdmissionExamJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.AdmissionExam.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.AdmissionExam.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.AdmissionExam.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            try
            {
                using (Adm_AdmissionExamDataService admissionExamDataService = new Adm_AdmissionExamDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<Adm_AdmissionExamJson>();
                    var entities = admissionExamDataService.GetAll(out error);
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
        public HttpResult<Adm_AdmissionExamJson> GetAdmissionExamById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Adm_AdmissionExamJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.AdmissionExam.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.AdmissionExam.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.AdmissionExam.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            try
            {
                using (Adm_AdmissionExamDataService admissionExamDataService = new Adm_AdmissionExamDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Adm_AdmissionExamJson();
                    Adm_AdmissionExam entity;
                    if (id <= 0)
                    {
                        entity = Adm_AdmissionExam.GetNew();
                    }
                    else
                    {
                        entity = admissionExamDataService.GetById(id);
                    }
                    entity.Map(json);

                    //Ems 4 update.
                    json.DefaultSettingsJsonObj = GetDefaultSettingsObj(json.DefaultSettingsJson,entity.SemesterId);

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


        //Ems 4 update
        private DefaultSettingsJson GetDefaultSettingsObj(string defaultSettingsJson, long semesterId)
        {
            var defaultSettingsObj = new DefaultSettingsJson();

            var semester = DbInstance.Aca_Semester.FirstOrDefault(x => x.Id == semesterId);
            if (semester == null)
            {
                return null;
            }

            if (defaultSettingsJson.IsValid())
            {
                defaultSettingsObj = JsonConvert.DeserializeObject<DefaultSettingsJson>(defaultSettingsJson);
            }

            if (defaultSettingsObj ==null || defaultSettingsObj.ProgramWiseBatchMapJsonList == null ||
                defaultSettingsObj.ProgramWiseBatchMapJsonList.Count <= 0)
            {
                defaultSettingsObj = new DefaultSettingsJson();
                defaultSettingsObj.ProgramWiseBatchMapJsonList = new List<ProgramWiseBatchMapJson>();
                var programList = DbInstance.Aca_Program.Where(x=>!x.IsDeleted).AsEnumerable().OrderBy(x => x.ShortTitle).Select(t => new
                    { Id = t.Id }).ToList();

                foreach (var program in programList)
                {
                    defaultSettingsObj.ProgramWiseBatchMapJsonList.Add(new ProgramWiseBatchMapJson()
                    {
                        ProgramId = program.Id,
                        BatchId = 0
                    });
                }
            }

            return defaultSettingsObj;
        }

        private HttpResult<Adm_AdmissionExamJson> GetAdmissionExamByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Adm_AdmissionExamJson>();

                  if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.AdmissionExam.CanView, out error)
                      && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.AdmissionExam.CanAdd, out error)
                      && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.AdmissionExam.CanEdit, out error))
                  {
                      result.HasError = true;
                      result.Errors.Add(error);
                      return result;
                  }

            try
            {
                using (Adm_AdmissionExamDataService admissionExamDataService = new Adm_AdmissionExamDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Adm_AdmissionExamJson();
                    Adm_AdmissionExam entity;
                    if (id <= 0)
                    {
                        entity = Adm_AdmissionExam.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("Adm_AdmissionExam");
                        //includeTables.Add("Adm_AdmissionExam");

                        entity = admissionExamDataService.GetById(id, includeTables.ToArray());
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

        public HttpListResult<Adm_AdmissionExamJson> GetAdmissionExamListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Adm_AdmissionExamJson>();
            try
            {
                //Adm_AdmissionExamDataService admissionExamDataService =
                //    new Adm_AdmissionExamDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List

                result.DataExtra.ProgramTypeEnumList = EnumUtil.GetEnumList(typeof(Aca_Program.ProgramTypeEnum));
                result.DataExtra.CircularStatusEnumList = EnumUtil.GetEnumList(typeof(Adm_AdmissionExam.CircularStatusEnum));
                result.DataExtra.SemesterDurationEnumList = Aca_Semester.SemesterDurationEnumDropDownList;
                //DropDown Option Tables

                result.DataExtra.SemesterList = Aca_Semester.GetDropdownList(DbInstance);
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<Adm_AdmissionExamJson> GetAdmissionExamDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Adm_AdmissionExamJson>();
            try
            {
                //Adm_AdmissionExamDataService admissionExamDataService =
                //    new Adm_AdmissionExamDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List

                result.DataExtra.ProgramTypeEnumList = EnumUtil.GetEnumList(typeof(Aca_Program.ProgramTypeEnum));
                result.DataExtra.CircularStatusEnumList = EnumUtil.GetEnumList(typeof(Adm_AdmissionExam.CircularStatusEnum));
                //DropDown Option Tables

                result.DataExtra.SemesterList = DbInstance.Aca_Semester.AsEnumerable().Select(t => new
                { Id = t.Id.ToString(), Name = t.Name }).ToList();

                //EMS 4 Update.
                result.DataExtra.ProgramList = DbInstance.Aca_Program.AsEnumerable().OrderBy(x=>x.ShortTitle).Select(t => new
                    { Id = t.Id, Name = t.ShortTitle }).ToList();
                result.DataExtra.BatchList = DbInstance.Aca_DeptBatch.AsEnumerable().OrderBy(x=>x.BatchNo).Select(t => new
                    { Id = t.Id, Name = t.Name }).ToList();
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

        public HttpResult<Adm_AdmissionExamJson> GetCopyAdmissionExam(int sourceAdmissionExamId
            , long newAdmissionExamSemesterId
            , string newAdmissionExamName
            , string newStudentIdPrefix
            , string studentUgcIdPrefix
            , int incrementBatchCount)
        {
            string error = string.Empty;
            var result = new HttpResult<Adm_AdmissionExamJson>();
            var json = new Adm_AdmissionExamJson();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.AdmissionExam.CanAdd, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            try
            {
                var dbAttachedEntity = new Adm_AdmissionExam();
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {
                    if (Facade.AcademicFacade.SemesterFacade.CopyAndSaveAdmissionExam(sourceAdmissionExamId
                            , newAdmissionExamSemesterId, newAdmissionExamName
                            , newStudentIdPrefix, studentUgcIdPrefix, incrementBatchCount
                            , ref dbAttachedEntity, out error))
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



        [HttpPost]
        public HttpResult<Adm_AdmissionExamJson> SaveAdmissionExam(Adm_AdmissionExamJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Adm_AdmissionExamJson>();


            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.AdmissionExam.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.AdmissionExam.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }


            try
            {
                var entityReceived = new Adm_AdmissionExam();
                var dbAttachedEntity = new Adm_AdmissionExam();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {
                    if (Facade.AcademicFacade.SemesterFacade.SaveAdmissionExamLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<Adm_AdmissionExamJson> SaveAdmissionExam2(Adm_AdmissionExamJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Adm_AdmissionExamJson>();
            try
            {
                using (Adm_AdmissionExamDataService admissionExamDataService = new Adm_AdmissionExamDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new Adm_AdmissionExam();
                    var dbAttachedEntity = new Adm_AdmissionExam();
                    json.Map(entityReceived);
                    if (admissionExamDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<Adm_AdmissionExamJson> SaveAdmissionExamList(IList<Adm_AdmissionExamJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Adm_AdmissionExamJson>();

       if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.AdmissionExam.CanAdd, out error)
           && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.AdmissionExam.CanEdit, out error))
       {
           result.HasError = true;
           result.Errors.Add(error);
           return result;
       }

            try
            {
                using (Adm_AdmissionExamDataService admissionExamDataService = new Adm_AdmissionExamDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<Adm_AdmissionExam>();
                    var dbAttachedListEntity = new List<Adm_AdmissionExam>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (admissionExamDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<Adm_AdmissionExamJson> GetDeleteAdmissionExamById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Adm_AdmissionExamJson>();

       if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.AdmissionExam.CanDelete, out error))
       {
           result.HasError = true;
           result.Errors.Add(error);
           return result;
       }

            try
            {
                using (Adm_AdmissionExamDataService admissionExamDataService = new Adm_AdmissionExamDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!admissionExamDataService.DeleteById(id, out error))
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

        

        #endregion
        #endregion

    }
}

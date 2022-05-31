using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases.WebApi;
using EMS.Framework.Objects;
using EMS.Framework.Settings;
//using EMS.Web.Jsons.Mapper;
using EMS.Web.Jsons.Models;

namespace EMS.Web.UI.Areas.Academic.Controllers.WebApi
{
    public class ProgramApiController : EmployeeBaseApiController
    {
        public enum CreditTypeSystemEnum
        {
            OpenCreditSystem = 1,
            CloseCreditSystem = 2
        }
        // GET api/<controller>
        public HttpListResult<Aca_ProgramJson> GetProgramList(string name, int? pageSize, int? pageNo)
        {
            int count = 0;
            var result = new HttpListResult<Aca_ProgramJson>();
            string error = string.Empty;
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ProgramManager.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ProgramManager.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ProgramManager.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            var jsons = new List<Aca_ProgramJson>();


            var entities = Facade.AcademicFacade.ProgramFacade.GetProgramList(name, pageSize, pageNo, ref count);

            entities.Map(jsons);
            result.Data = jsons;
            result.Count = count;
            return result;
        }

        // GET api/<controller>/5
        public HttpResult<Aca_ProgramJson> Get(long id)
        {
            var result = new HttpResult<Aca_ProgramJson>();
            string error = string.Empty;
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ProgramManager.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ProgramManager.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ProgramManager.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            var entities = Facade.AcademicFacade.ProgramFacade.GetProgramById(id);
            var json = new Aca_ProgramJson();
            entities.Map(json);
            result.Data = json;
            return result;
        }

        // POST api/<controller>
        public HttpResult<Aca_ProgramJson> Post(Aca_ProgramJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_ProgramJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ProgramManager.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ProgramManager.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            var entity = new Aca_Program();
            json.Map(entity);
            if (Facade.AcademicFacade.ProgramFacade.SaveProgram(entity, out error))
            {
                entity.Map(json);
                result.Data = json;
            }
            else
            {
                result.HasError = true;
                result.Errors.Add(error);
            }
            return result;
        }

        public ProgramApiController()
        { }

        #region Program 
        #region List View Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Aca_ProgramJson> GetPagedProgram(string textkey, int? pageSize, int? pageNo
           , Int64 departmentId = 0
           , int selectedDurationEnumId = 0
           , int creditTypeSystemEnumId = 0
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Aca_ProgramJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ProgramManager.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ProgramManager.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ProgramManager.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_ProgramDataService programDataService = new Aca_ProgramDataService(DbInstance, HttpUtil.Profile))
                {
                    IQueryable<Aca_Program> query = DbInstance.Aca_Program
                                        .OrderBy(x => x.ProgramTypeEnumId);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Name.ToLower().Contains(textkey.ToLower()));
                    }


                    var entities = programDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Aca_ProgramJson>();
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
        public HttpListResult<Aca_ProgramJson> GetProgramListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_ProgramJson>();
            try
            {
                //Aca_ProgramDataService programDataService =
                //    new Aca_ProgramDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List

                result.DataExtra.ProgramTypeEnumList = EnumUtil.GetEnumList(typeof(Aca_Program.ProgramTypeEnum));
                //DropDown Option Tables

                //result.DataExtra.DepartmentList = DbInstance.HR_Department.AsEnumerable().Select(t => new
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
        private HttpListResult<Aca_ProgramJson> GetAllProgram()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_ProgramJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ProgramManager.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ProgramManager.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ProgramManager.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_ProgramDataService programDataService = new Aca_ProgramDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<Aca_ProgramJson>();
                    var entities = programDataService.GetAll(out error);
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
        private HttpListResult<Aca_ProgramJson> SaveProgramList(IList<Aca_ProgramJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_ProgramJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ProgramManager.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ProgramManager.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_ProgramDataService programDataService = new Aca_ProgramDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<Aca_Program>();
                    var dbAttachedListEntity = new List<Aca_Program>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (programDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<Aca_ProgramJson> GetProgramById(int id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_ProgramJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ProgramManager.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ProgramManager.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ProgramManager.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_ProgramDataService programDataService = new Aca_ProgramDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Aca_ProgramJson();
                    Aca_Program entity;
                    if (id <= 0)
                    {
                        entity = Aca_Program.GetNew();
                    }
                    else
                    {
                        entity = programDataService.GetById(id);
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
        private HttpResult<Aca_ProgramJson> GetProgramByIdWithChild(int id)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_ProgramJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ProgramManager.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ProgramManager.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ProgramManager.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_ProgramDataService programDataService = new Aca_ProgramDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Aca_ProgramJson();
                    Aca_Program entity;
                    if (id <= 0)
                    {
                        entity = Aca_Program.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("Aca_Program");
                        //includeTables.Add("Aca_Program");

                        entity = programDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Aca_ProgramJson> GetProgramDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_ProgramJson>();
            try
            {
                //Aca_ProgramDataService programDataService =
                //    new Aca_ProgramDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List

                result.DataExtra.ProgramTypeEnumList = EnumUtil.GetEnumList(typeof(Aca_Program.ProgramTypeEnum));
                result.DataExtra.ClassTimingGroupEnumList = EnumUtil.GetEnumList(typeof(Aca_Program.ClassTimingGroupEnum));
                result.DataExtra.LanguageEnumList = EnumUtil.GetEnumList(typeof(Aca_Program.LanguageMediumEnum));
                //DropDown Option Tables
                result.DataExtra.DepartmentList = DbInstance.HR_Department.AsEnumerable()
                    .Where(x => x.TypeEnumId ==(byte)HR_Department.TypeEnum.AcademicDepartment)
                    .Select(t => new
                {
                    Id = t.Id,
                    Name = t.ShortName
                }).ToList();
                //
                /*result.DataExtra.OfficialBankList = DbInstance.Acnt_OfficialBank.AsEnumerable()
                    .Where(x => x.TypeEnumId == (byte)Acnt_OfficialBank.TypeEnum.StudentPaymentAccout)
                    .Select(t => new
                    {
                        Id = t.Id,
                        Name = t.Name,
                        ShortName = t.ShortName
                    }).ToList();*/

                //Ems 4 Update
                result.DataExtra.SemesterDurationEnumList = Aca_Semester.SemesterDurationEnumDropDownList;
                result.DataExtra.CreditTypeSystemEnumList = EnumUtil.GetEnumList(typeof(CreditTypeSystemEnum)); ;
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
        public HttpResult<Aca_ProgramJson> SaveProgram(Aca_ProgramJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_ProgramJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ProgramManager.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ProgramManager.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                var entityReceived = new Aca_Program();
                var dbAttachedEntity = new Aca_Program();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {
                    if (SaveProgramLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<Aca_ProgramJson> SaveProgram2(Aca_ProgramJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_ProgramJson>();
            try
            {
                using (Aca_ProgramDataService programDataService = new Aca_ProgramDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new Aca_Program();
                    var dbAttachedEntity = new Aca_Program();
                    json.Map(entityReceived);
                    if (programDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        public HttpResult<Aca_ProgramJson> GetDeleteProgramById(int id)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_ProgramJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ProgramManager.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_ProgramDataService programDataService = new Aca_ProgramDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!programDataService.DeleteById(id, out error))
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

        private bool IsValidToSaveProgram(Aca_Program newObj, out string error)
        {
            error = "";
            if (!newObj.Name.IsValid())
            {
                error += "Name is not valid.";
                return false;
            }

            if (newObj.Name.Length > 128)
            {
                error += "Name exceeded its maximum length 128.";
                return false;
            }

            if (newObj.ShortName.Length > 128)
            {
                error += "Short Name exceeded its maximum length 128.";
                return false;
            }

            if (!newObj.ShortTitle.IsValid())
            {
                error += "Short Title is not valid.";
                return false;
            }

            if (newObj.ShortTitle.Length > 50)
            {
                error += "Short Title exceeded its maximum length 50.";
                return false;
            }

            if (!newObj.OfficialCertificateTitle.IsValid())
            {
                error += "Official Certificate Title is not valid.";
                return false;
            }

            if (newObj.OfficialCertificateTitle.Length > 256)
            {
                error += "Official Certificate Title exceeded its maximum length 256.";
                return false;
            }

            if (newObj.Description.IsValid() && newObj.Description.Length > 256)
            {
                error += "Description exceeded its maximum length 256.";
                return false;
            }

            if (newObj.Code.IsValid() && newObj.Code.Length > 50)
            {
                error += "Code exceeded its maximum length 50.";
                return false;
            }

            if (newObj.ProgramTypeEnumId == null)
            {
                error += "Please select valid Program Type.";
                return false;
            }

            if (newObj.ClassTimingGroupEnumId == null)
            {
                error += "Please select valid Class Timing Group.";
                return false;
            }

            if (newObj.IsDeleted == null)
            {
                error += "Is Deleted required.";
                return false;
            }

            if (newObj.LanguageMediumEnumId < 0)
            {
                error += "Please Select a Valid Language EnumId";
                return false;
            }
            if (newObj.DefaultClassSectionRange <= 0)
            {
                error += "Default Class Section Range Cannot be Less than or equal zero";
                return false;
            }
            
            if (newObj.SemesterId <= 0)
            {
                error += "Invalid Semester Id";
                return false;
            }
            

            //TODO write your custom validation here.
            var res = DbInstance.Aca_Program.Any(x => x.Id != newObj.Id
            && x.Name==newObj.Name);
            if (res)
            {
                error = "A same named Program already exists!";
                return false;
            }
            return true;
        }
        private bool SaveProgramLogic(Aca_Program newObj, ref Aca_Program dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Program to save can't be null!";
                return false;
            }

            if (!IsValidToSaveProgram(newObj, out error))
                return false;

            bool isNewObject = true;
            Aca_Program objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Aca_Program.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            //else
            //{
            //    newObj.Id = BigInt.NewBigInt();
            //}
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Aca_Program.GetNew();
                DbInstance.Aca_Program.Add(objToSave);
                objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
                objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ProgramManager.Program.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ProgramManager.Program.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
            objToSave.Name = newObj.Name;
            objToSave.ShortName = newObj.ShortName;
            objToSave.ShortTitle = newObj.ShortTitle;
            objToSave.OfficialCertificateTitle = newObj.OfficialCertificateTitle;
            objToSave.Description = newObj.Description;
            objToSave.Code = newObj.Code;
            objToSave.ProgramTypeEnumId = newObj.ProgramTypeEnumId;
            objToSave.ClassTimingGroupEnumId = newObj.ClassTimingGroupEnumId;
            objToSave.IsDeleted = newObj.IsDeleted;
            objToSave.LastChangedById = newObj.LastChangedById = HttpUtil.Profile.UserId;
            objToSave.LastChanged = newObj.LastChanged = DateTime.Now;
            objToSave.LanguageMediumEnumId = newObj.LanguageMediumEnumId;
            objToSave.DefaultClassSectionRange = newObj.DefaultClassSectionRange;
            objToSave.IsEnable = newObj.IsEnable;
            objToSave.ClassTeacherId = objToSave.ClassTeacherId;
            objToSave.CurriculumId = objToSave.CurriculumId;
            objToSave.SectionId = objToSave.SectionId;
            objToSave.RequiredProgramTypeEnumId = objToSave.RequiredProgramTypeEnumId;
            objToSave.SemesterId = objToSave.SemesterId;
            dbAddedObj = objToSave;
            //here save any child table
            return true;
        }
        #endregion
        #endregion
    }
}
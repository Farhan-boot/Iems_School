//File: UI Controller
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

    public class CurriculumElectiveGroupApiController : EmployeeBaseApiController
	{
        public CurriculumElectiveGroupApiController()
        {  }
       
        #region CurriculumElectiveGroup 
        #region List View Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Aca_CurriculumElectiveGroupJson> GetPagedCurriculumElectiveGroup(string textkey, int? pageSize, int? pageNo
           , Int64 programId = 0
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Aca_CurriculumElectiveGroupJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.CurriculumElectiveGroup.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.CurriculumElectiveGroup.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.CurriculumElectiveGroup.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_CurriculumElectiveGroupDataService curriculumElectiveGroupDataService = new Aca_CurriculumElectiveGroupDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<Aca_CurriculumElectiveGroup> query = DbInstance.Aca_CurriculumElectiveGroup
                        .Include(x => x.Aca_Program)
                        .OrderBy(x => x.ProgramId);
                    if (textkey.IsStringValid())
                    {
                        query = query.Where(q => q.Name.ToLower().Contains(textkey.ToLower()));
                    }
                    if (programId > 0)
                    {
                        query = query.Where(q => q.ProgramId == programId);
                    }

                    var entities = curriculumElectiveGroupDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Aca_CurriculumElectiveGroupJson>();
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
        public HttpListResult<Aca_CurriculumElectiveGroupJson> GetCurriculumElectiveGroupListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_CurriculumElectiveGroupJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.CurriculumElectiveGroup.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.CurriculumElectiveGroup.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.CurriculumElectiveGroup.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                //DropDown Option Enum List


                //DropDown Option Tables


                result.DataExtra.ProgramList = DbInstance.Aca_Program.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.ShortTitle }).ToList();
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
        private HttpListResult<Aca_CurriculumElectiveGroupJson> GetAllCurriculumElectiveGroup()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_CurriculumElectiveGroupJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.CurriculumElectiveGroup.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.CurriculumElectiveGroup.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.CurriculumElectiveGroup.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_CurriculumElectiveGroupDataService curriculumElectiveGroupDataService = new Aca_CurriculumElectiveGroupDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<Aca_CurriculumElectiveGroupJson>();
                    var entities = curriculumElectiveGroupDataService.GetAll(out error);
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
        private HttpListResult<Aca_CurriculumElectiveGroupJson> SaveCurriculumElectiveGroupList(IList<Aca_CurriculumElectiveGroupJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_CurriculumElectiveGroupJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.CurriculumElectiveGroup.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.CurriculumElectiveGroup.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_CurriculumElectiveGroupDataService curriculumElectiveGroupDataService = new Aca_CurriculumElectiveGroupDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<Aca_CurriculumElectiveGroup>();
                    var dbAttachedListEntity = new List<Aca_CurriculumElectiveGroup>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (curriculumElectiveGroupDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<Aca_CurriculumElectiveGroupJson> GetCurriculumElectiveGroupById(long id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_CurriculumElectiveGroupJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.CurriculumElectiveGroup.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.CurriculumElectiveGroup.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.CurriculumElectiveGroup.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_CurriculumElectiveGroupDataService curriculumElectiveGroupDataService = new Aca_CurriculumElectiveGroupDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Aca_CurriculumElectiveGroupJson();
                    Aca_CurriculumElectiveGroup entity;
                    if (id <= 0)
                    {
                        entity = Aca_CurriculumElectiveGroup.GetNew();
                    }
                    else
                    {
                        entity = curriculumElectiveGroupDataService.GetById(id);
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
        private HttpResult<Aca_CurriculumElectiveGroupJson> GetCurriculumElectiveGroupByIdWithChild(long id)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_CurriculumElectiveGroupJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.CurriculumElectiveGroup.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.CurriculumElectiveGroup.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.CurriculumElectiveGroup.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_CurriculumElectiveGroupDataService curriculumElectiveGroupDataService = new Aca_CurriculumElectiveGroupDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Aca_CurriculumElectiveGroupJson();
                    Aca_CurriculumElectiveGroup entity;
                    if (id <= 0)
                    {
                        entity = Aca_CurriculumElectiveGroup.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("Aca_CurriculumElectiveGroup");
                        //includeTables.Add("Aca_CurriculumElectiveGroup");

                        entity = curriculumElectiveGroupDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Aca_CurriculumElectiveGroupJson> GetCurriculumElectiveGroupDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_CurriculumElectiveGroupJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.CurriculumElectiveGroup.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.CurriculumElectiveGroup.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.CurriculumElectiveGroup.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                //DropDown Option Enum List

                //DropDown Option Tables

                result.DataExtra.ProgramList = DbInstance.Aca_Program.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.ShortTitle }).ToList();
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
        public HttpResult<Aca_CurriculumElectiveGroupJson> SaveCurriculumElectiveGroup2(Aca_CurriculumElectiveGroupJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_CurriculumElectiveGroupJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.CurriculumElectiveGroup.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.CurriculumElectiveGroup.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_CurriculumElectiveGroupDataService curriculumElectiveGroupDataService = new Aca_CurriculumElectiveGroupDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new Aca_CurriculumElectiveGroup();
                    var dbAttachedEntity = new Aca_CurriculumElectiveGroup();
                    json.Map(entityReceived);
                    if (curriculumElectiveGroupDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        }
        [HttpPost]
        public HttpResult<Aca_CurriculumElectiveGroupJson> SaveCurriculumElectiveGroup(Aca_CurriculumElectiveGroupJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_CurriculumElectiveGroupJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.CurriculumElectiveGroup.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.CurriculumElectiveGroup.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                 var entityReceived = new Aca_CurriculumElectiveGroup();
                 var dbAttachedEntity = new Aca_CurriculumElectiveGroup();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {
                    if (SaveCurriculumElectiveGroupLogic(entityReceived, ref dbAttachedEntity, out error))
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
        public HttpResult<Aca_CurriculumElectiveGroupJson> GetDeleteCurriculumElectiveGroupById(long id)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_CurriculumElectiveGroupJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.CurriculumElectiveGroup.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.CurriculumElectiveGroup.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumManager.CurriculumElectiveGroup.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_CurriculumElectiveGroupDataService curriculumElectiveGroupDataService = new Aca_CurriculumElectiveGroupDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!curriculumElectiveGroupDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveCurriculumElectiveGroup(Aca_CurriculumElectiveGroup newObj, out string error)
        {
            error = "";
            if (!newObj.Name.IsValid())
            {
                error += "Name is not valid.";
                return false;
            }
            if (newObj.Name.Length>256)
            {
                error += "Name exceeded its maximum length 256.";
                return false;
            }
            if (newObj.ShortName.IsValid() && newObj.ShortName.Length>256)
            {
                error += "Short Name exceeded its maximum length 256.";
                return false;
            }
            if (newObj.ProgramId <= 0)
            {
                error += "Please select valid Program.";
                return false;
            }
            //TODO write your custom validation here.
            var program = DbInstance.Aca_Program.SingleOrDefault(x => x.Id == newObj.ProgramId);
            if (program == null)
            {
                error += "Invalid Program.";
                return false;
            }
            if (!DbInstance.Aca_CurriculumElectiveGroup.Any(x => x.Id == newObj.Id))
            {
                var lastObj = DbInstance.Aca_CurriculumElectiveGroup
                    .OrderByDescending(x => x.Id)
                    .FirstOrDefault(
                        x => x.ProgramId == newObj.ProgramId
                    );
                if (lastObj == null)
                {
                    newObj.Id = Convert.ToInt32(newObj.ProgramId + "01");
                }
                else
                {
                    newObj.Id = lastObj.Id + 1;
                }
            }
            //var res =  DbInstance.Aca_CurriculumElectiveGroup.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
            //error = "A CurriculumElectiveGroup already exists!";
            //return false;
            //}
            return true;
        }
        private bool SaveCurriculumElectiveGroupLogic(Aca_CurriculumElectiveGroup newObj,ref Aca_CurriculumElectiveGroup dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Curriculum Elective Group to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveCurriculumElectiveGroup(newObj, out error))
                return false;

            bool isNewObject = true;
            Aca_CurriculumElectiveGroup objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Aca_CurriculumElectiveGroup.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            else
            {    
                newObj.Id = BigInt.NewBigInt();
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Aca_CurriculumElectiveGroup.GetNew(newObj.Id);
                DbInstance.Aca_CurriculumElectiveGroup.Add(objToSave);
               
               
            }
            //checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumElectiveGroupManager.CurriculumElectiveGroup.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.CurriculumElectiveGroupManager.CurriculumElectiveGroup.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
           objToSave.Name =  newObj.Name ;
           objToSave.ShortName =  newObj.ShortName ;
           objToSave.ProgramId =  newObj.ProgramId ;
           
           
           dbAddedObj = objToSave;
           
            //here save any child table
            return true;
        }
        #endregion
        #endregion

	}
}

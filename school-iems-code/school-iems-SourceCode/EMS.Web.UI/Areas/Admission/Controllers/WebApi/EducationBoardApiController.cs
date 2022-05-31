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
using EMS.Web.Jsons.Models;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;

namespace EMS.Web.UI.Areas.Admission.Controllers.WebApi
{

    public class EducationBoardApiController : EmployeeBaseApiController
	{
        public EducationBoardApiController()
        {  }
       
        #region EducationBoard 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Adm_EducationBoardJson> GetPagedEducationBoard(string textkey, int? pageSize, int? pageNo
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Adm_EducationBoardJson>();
        
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.EducationBoard.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.EducationBoard.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.EducationBoard.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            try
            {
                using (Adm_EducationBoardDataService educationBoardDataService = new Adm_EducationBoardDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<Adm_EducationBoard> query = DbInstance.Adm_EducationBoard.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Name.ToLower().Contains(textkey.ToLower()));
                    }
                 
                    var entities = educationBoardDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Adm_EducationBoardJson>();
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
        private HttpListResult<Adm_EducationBoardJson> GetAllEducationBoard()
        {
            string error = string.Empty;
            var result = new HttpListResult<Adm_EducationBoardJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.EducationBoard.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.EducationBoard.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.EducationBoard.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Adm_EducationBoardDataService educationBoardDataService = new Adm_EducationBoardDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<Adm_EducationBoardJson>();
                    var entities = educationBoardDataService.GetAll(out error);
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
        public HttpResult<Adm_EducationBoardJson> GetEducationBoardById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Adm_EducationBoardJson>();
    
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.EducationBoard.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.EducationBoard.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.EducationBoard.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Adm_EducationBoardDataService educationBoardDataService = new Adm_EducationBoardDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Adm_EducationBoardJson();
                    Adm_EducationBoard entity;
                    if (id <= 0)
                    {
                        entity = Adm_EducationBoard.GetNew();
                    }
                    else
                    {
                        entity = educationBoardDataService.GetById(id);
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
        private HttpResult<Adm_EducationBoardJson> GetEducationBoardByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Adm_EducationBoardJson>();
             if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.EducationBoard.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.EducationBoard.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.EducationBoard.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Adm_EducationBoardDataService educationBoardDataService = new Adm_EducationBoardDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Adm_EducationBoardJson();
                    Adm_EducationBoard entity;
                    if (id <= 0)
                    {
                        entity = Adm_EducationBoard.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("Adm_EducationBoard");
                        //includeTables.Add("Adm_EducationBoard");

                        entity = educationBoardDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Adm_EducationBoardJson> GetEducationBoardListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Adm_EducationBoardJson>();
            try
            {
                //Adm_EducationBoardDataService educationBoardDataService =
                //    new Adm_EducationBoardDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.BoardTypeEnumList = EnumUtil.GetEnumList(typeof(Adm_EducationBoard.BoardTypeEnum));
                //DropDown Option Tables
                 
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<Adm_EducationBoardJson> GetEducationBoardDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Adm_EducationBoardJson>();
            try
            {
                //Adm_EducationBoardDataService educationBoardDataService =
                //    new Adm_EducationBoardDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.BoardTypeEnumList = EnumUtil.GetEnumList(typeof(Adm_EducationBoard.BoardTypeEnum));
                //DropDown Option Tables
                 
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
        public HttpResult<Adm_EducationBoardJson> SaveEducationBoard(Adm_EducationBoardJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Adm_EducationBoardJson>();
         
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.EducationBoard.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.EducationBoard.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                 var entityReceived = new Adm_EducationBoard();
                 var dbAttachedEntity = new Adm_EducationBoard();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {
                    if (SaveEducationBoardLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<Adm_EducationBoardJson> SaveEducationBoard2(Adm_EducationBoardJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Adm_EducationBoardJson>();
            try
            {
                using (Adm_EducationBoardDataService educationBoardDataService = new Adm_EducationBoardDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new Adm_EducationBoard();
                    var dbAttachedEntity = new Adm_EducationBoard();
                    json.Map(entityReceived);
                    if (educationBoardDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<Adm_EducationBoardJson> SaveEducationBoardList(IList<Adm_EducationBoardJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Adm_EducationBoardJson>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.EducationBoard.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.EducationBoard.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Adm_EducationBoardDataService educationBoardDataService = new Adm_EducationBoardDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<Adm_EducationBoard>();
                    var dbAttachedListEntity = new List<Adm_EducationBoard>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (educationBoardDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<Adm_EducationBoardJson> GetDeleteEducationBoardById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Adm_EducationBoardJson>();
          
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.EducationBoard.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Adm_EducationBoardDataService educationBoardDataService = new Adm_EducationBoardDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!educationBoardDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveEducationBoard(Adm_EducationBoard newObj, out string error)
        {
            error = "";
            if (!newObj.Name.IsValid())
            {
                error += "Name is not valid.";
                return false;
            }
            if (newObj.Name.Length>250)
            {
                error += "Name exceeded its maximum length 250.";
                return false;
            }
            if (!newObj.FullName.IsValid())
            {
                error += "Full Name is not valid.";
                return false;
            }
            if (newObj.FullName.Length>250)
            {
                error += "Full Name exceeded its maximum length 250.";
                return false;
            }
            if (newObj.BoardTypeEnumId==null)
            {
                error += "Please select valid Board Type.";
                return false;
            }
            if (newObj.OrderNo==null)
            {
                error += "Order No required.";
                return false;
            }
            if (newObj.IsEnable==null)
            {
                error += "Is Enable required.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.Adm_EducationBoard.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A EducationBoard already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveEducationBoardLogic(Adm_EducationBoard newObj,ref Adm_EducationBoard dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Education Board to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveEducationBoard(newObj, out error))
                return false;

            bool isNewObject = true;
            Adm_EducationBoard objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Adm_EducationBoard.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Adm_EducationBoard.GetNew(newObj.Id);
                DbInstance.Adm_EducationBoard.Add(objToSave);
               
               
            }
        
            if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.EducationBoard.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.EducationBoard.CanEdit,
                   out error))
            {
                return false;
            }

            //binding object  
           objToSave.Name =  newObj.Name ;
           objToSave.FullName =  newObj.FullName ;
           objToSave.BoardTypeEnumId =  newObj.BoardTypeEnumId ;
           objToSave.OrderNo =  newObj.OrderNo ;
           objToSave.IsEnable =  newObj.IsEnable ;
           
           
           dbAddedObj = objToSave;
           
            //here save any child table
            return true;
        }
        #endregion
        #endregion

	}
}

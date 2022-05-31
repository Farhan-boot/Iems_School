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

namespace EMS.Web.UI.Areas.HR.Controllers.WebApi
{

    public class OrganizationApiController : EmployeeBaseApiController
	{
        public OrganizationApiController()
        {  }
       
        #region Organization 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<HR_OrganizationJson> GetPagedOrganization(string textkey, int? pageSize, int? pageNo
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<HR_OrganizationJson>();
          if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Organization.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Organization.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Organization.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }

            try
            {
                using (HR_OrganizationDataService organizationDataService = new HR_OrganizationDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<HR_Organization> query = DbInstance.HR_Organization.OrderByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Name.ToLower().Contains(textkey.ToLower()));
                    }
                 
                    var entities = organizationDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<HR_OrganizationJson>();
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
        private HttpListResult<HR_OrganizationJson> GetAllOrganization()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_OrganizationJson>();
           if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Organization.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Organization.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Organization.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (HR_OrganizationDataService organizationDataService = new HR_OrganizationDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<HR_OrganizationJson>();
                    var entities = organizationDataService.GetAll(out error);
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
        public HttpResult<HR_OrganizationJson> GetOrganizationById(Int64 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_OrganizationJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Organization.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Organization.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Organization.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (HR_OrganizationDataService organizationDataService = new HR_OrganizationDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new HR_OrganizationJson();
                    HR_Organization entity;
                    if (id <= 0)
                    {
                        entity = HR_Organization.GetNew();
                    }
                    else
                    {
                        entity = organizationDataService.GetById(id);
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
        private HttpResult<HR_OrganizationJson> GetOrganizationByIdWithChild(Int64 id)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_OrganizationJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Organization.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Organization.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Organization.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (HR_OrganizationDataService organizationDataService = new HR_OrganizationDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new HR_OrganizationJson();
                    HR_Organization entity;
                    if (id <= 0)
                    {
                        entity = HR_Organization.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("HR_Organization");
                        //includeTables.Add("HR_Organization");

                        entity = organizationDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<HR_OrganizationJson> GetOrganizationListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_OrganizationJson>();
            try
            {
                //HR_OrganizationDataService organizationDataService =
                //    new HR_OrganizationDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.TypeEnumList = EnumUtil.GetEnumList(typeof(HR_Organization.TypeEnum));
                result.DataExtra.StatusEnumList = EnumUtil.GetEnumList(typeof(HR_Organization.StatusEnum));
                //DropDown Option Tables
                 
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<HR_OrganizationJson> GetOrganizationDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_OrganizationJson>();
            try
            {
                //HR_OrganizationDataService organizationDataService =
                //    new HR_OrganizationDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.TypeEnumList = EnumUtil.GetEnumList(typeof(HR_Organization.TypeEnum));
                result.DataExtra.StatusEnumList = EnumUtil.GetEnumList(typeof(HR_Organization.StatusEnum));
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
        public HttpResult<HR_OrganizationJson> SaveOrganization(HR_OrganizationJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_OrganizationJson>();
      
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Organization.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Organization.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                 var entityReceived = new HR_Organization();
                 var dbAttachedEntity = new HR_Organization();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {
                    if (SaveOrganizationLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<HR_OrganizationJson> SaveOrganization2(HR_OrganizationJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_OrganizationJson>();
            try
            {
                using (HR_OrganizationDataService organizationDataService = new HR_OrganizationDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new HR_Organization();
                    var dbAttachedEntity = new HR_Organization();
                    json.Map(entityReceived);
                    if (organizationDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<HR_OrganizationJson> SaveOrganizationList(IList<HR_OrganizationJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<HR_OrganizationJson>();
        
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Organization.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Organization.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (HR_OrganizationDataService organizationDataService = new HR_OrganizationDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<HR_Organization>();
                    var dbAttachedListEntity = new List<HR_Organization>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (organizationDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<HR_OrganizationJson> GetDeleteOrganizationById(Int64 id)
        {
            string error = string.Empty;
            var result = new HttpResult<HR_OrganizationJson>();
           
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.Organization.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (HR_OrganizationDataService organizationDataService = new HR_OrganizationDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!organizationDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveOrganization(HR_Organization newObj, out string error)
        {
            error = "";
            if (!newObj.Name.IsValid())
            {
                error += "Name is not valid.";
                return false;
            }
            if (newObj.Name.Length>150)
            {
                error += "Name exceeded its maximum length 150.";
                return false;
            }
            if (!newObj.ShortName.IsValid())
            {
                error += "Short Name is not valid.";
                return false;
            }
            if (newObj.ShortName.Length>50)
            {
                error += "Short Name exceeded its maximum length 50.";
                return false;
            }
            if (newObj.OrganizationNo==null)
            {
                error += "Organization No required.";
                return false;
            }
            if (!newObj.Description.IsValid())
            {
                error += "Description is not valid.";
                return false;
            }
            if (newObj.Description==null)
            {
                error += "Description required.";
                return false;
            }
            if (!newObj.Address.IsValid())
            {
                error += "Address is not valid.";
                return false;
            }
            if (newObj.Address==null)
            {
                error += "Address required.";
                return false;
            }
            if (!newObj.ShortAddress.IsValid())
            {
                error += "Short Address is not valid.";
                return false;
            }
            if (newObj.ShortAddress.Length>256)
            {
                error += "Short Address exceeded its maximum length 256.";
                return false;
            }
            if (!newObj.MapEmbedCode.IsValid())
            {
                error += "Map Embed Code is not valid.";
                return false;
            }
            if (newObj.MapEmbedCode==null)
            {
                error += "Map Embed Code required.";
                return false;
            }
            if (!newObj.Email.IsValid())
            {
                error += "Email is not valid.";
                return false;
            }
            if (newObj.Email==null)
            {
                error += "Email required.";
                return false;
            }
            if (!newObj.Mobile.IsValid())
            {
                error += "Mobile is not valid.";
                return false;
            }
            if (newObj.Mobile==null)
            {
                error += "Mobile required.";
                return false;
            }
            if (!newObj.Phone.IsValid())
            {
                error += "Phone is not valid.";
                return false;
            }
            if (newObj.Phone==null)
            {
                error += "Phone required.";
                return false;
            }
            if (!newObj.Fax.IsValid())
            {
                error += "Fax is not valid.";
                return false;
            }
            if (newObj.Fax==null)
            {
                error += "Fax required.";
                return false;
            }
            if (!newObj.Website.IsValid())
            {
                error += "Website is not valid.";
                return false;
            }
            if (newObj.Website.Length>50)
            {
                error += "Website exceeded its maximum length 50.";
                return false;
            }
            if (!newObj.Founded.IsValid())
            {
                error += "Founded is not valid.";
                return false;
            }
            
            if (newObj.TypeEnumId==null)
            {
                error += "Please select valid Type.";
                return false;
            }
            if (newObj.StatusEnumId==null)
            {
                error += "Please select valid Status.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.HR_Organization.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A Organization already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveOrganizationLogic(HR_Organization newObj,ref HR_Organization dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Organization to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveOrganization(newObj, out error))
                return false;

            bool isNewObject = true;
            HR_Organization objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.HR_Organization.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            else
            {    
                newObj.Id = BigInt.NewBigInt();
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = HR_Organization.GetNew(newObj.Id);
                DbInstance.HR_Organization.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            
            if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Organization.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.Organization.CanEdit,
                   out error))
            {
                return false;
            }

            //binding object  
           objToSave.Name =  newObj.Name ;
           objToSave.ShortName =  newObj.ShortName ;
           objToSave.OrganizationNo =  newObj.OrganizationNo ;
           objToSave.Description =  newObj.Description ;
           objToSave.Address =  newObj.Address ;
           objToSave.ShortAddress =  newObj.ShortAddress ;
           objToSave.MapEmbedCode =  newObj.MapEmbedCode ;
           objToSave.Email =  newObj.Email ;
           objToSave.Mobile =  newObj.Mobile ;
           objToSave.Phone =  newObj.Phone ;
           objToSave.Fax =  newObj.Fax ;
           objToSave.Website =  newObj.Website ;
           objToSave.Founded =  newObj.Founded ;
           objToSave.LogoWithoutNameUrl =  newObj.LogoWithoutNameUrl ;
           objToSave.LogoWithShortNameUrl =  newObj.LogoWithShortNameUrl ;
           objToSave.LogoWithLongNameUrl =  newObj.LogoWithLongNameUrl ;
           objToSave.LogoWithoutNameBlackWhiteUrl =  newObj.LogoWithoutNameBlackWhiteUrl ;
           objToSave.FeviconUrl =  newObj.FeviconUrl ;
           objToSave.TypeEnumId =  newObj.TypeEnumId ;
           objToSave.StatusEnumId =  newObj.StatusEnumId ;
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

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;
using EMS.Framework.Settings;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases.WebApi;
using EMS.Web.Jsons.Custom;
using EMS.Web.Jsons.Models;
using Newtonsoft.Json;

namespace EMS.Web.UI.Areas.Admin.Controllers.WebApi
{
    public class SiteSettingsManagerApiController : EmployeeBaseApiController
    {
        #region SiteSettings 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        /// 
        public HttpResult<SiteSettings> GetSiteSettingsById(bool isReloadCache=false)
        {
            string error = string.Empty;
            var result = new HttpResult<SiteSettings>();

            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.SiteSettingsManager.CanView)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.SiteSettingsManager.CanEdit))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                //using (Adm_SiteSettingsDataService SiteSettingsDataService = new Adm_SiteSettingsDataService(DbInstance, HttpUtil.Profile))
                //{
                if (isReloadCache)
                {
                    if (!SiteSettings.ReloadCacheFromFile(out error))
                    {
                        result.HasError = true;
                        result.Errors.Add(error);
                        return result;
                    }
                }
                result.Data = SiteSettings.Instance;

                //}
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }

        public HttpListResult<SiteSettings> GetSiteSettingsDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<SiteSettings>();
            try
            {
                //Adm_SiteSettingsDataService SiteSettingsDataService =
                //    new Adm_SiteSettingsDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                /*result.DataExtra.ParticularList = DbInstance.Acnt_ParticularName.AsEnumerable()
                    .OrderBy(x => x.Name)
                    .Select(t => new
                        { Id = t.Id, Name = t.Name })
                    .ToList();*/
                result.DataExtra.GradeList = DbInstance.Aca_GradingPolicyOption
                    .Where(x=>x.Aca_GradingPolicy.IsActive )
                    .AsEnumerable()
                    .Select(t => new
                        { Id = t.Id.ToString(), Grade = t.Grade })
                    .ToList();
                result.DataExtra.ExamList = DbInstance.Aca_Exam
                    .Where(x=>!x.IsDeleted && x.ExamTypeEnumId != (byte)Aca_Exam.ExamTypeEnum.FinalTerm)
                    .OrderByDescending(x=>x.Id)
                    .AsEnumerable()
                    .Select(t => new
                        { Id = t.Id.ToString(), Name = t.Name })
                    .ToList();
                result.DataExtra.SemesterList = DbInstance.Aca_Semester.AsEnumerable()
                    .OrderByDescending(x => x.Id)
                    .Where(x => !x.IsDeleted)
                    .Select(t => new
                        { Id = t.Id.ToString(), Name = t.Name })
                    .ToList();

                result.DataExtra.StudentIdGenerateTypeEnumList = EnumUtil.GetEnumList(typeof(SiteSettings.StudentIdGenerateTypeEnum));
                result.DataExtra.CreditTransBillGenTypeEnumList = EnumUtil.GetEnumList(typeof(SiteSettings.CreditTransBillGenTypeEnum));
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
        [System.Web.Mvc.HttpPost]
        public HttpResult<SiteSettings> SaveSiteSettings(SiteSettings json)
        {
            string error = string.Empty;
            var result = new HttpResult<SiteSettings>();

            
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.SiteSettingsManager.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            if (!HttpUtil.IsSupperAdmin())
            {
                result.HasError = true;
                result.Errors.Add("You are not a Super Admin so you can't change any site settings.");
                return result;
            }

            try
            {
                
                    if (SaveSiteSettingsLogic(json, out error))
                    {

                        string basePath = HttpContext.Current.Server.MapPath(SiteSettings.SiteSettingsBasePath);
                        string settingsJson = JsonConvert.SerializeObject(json, Formatting.Indented);
                        string settingsPath = HttpContext.Current.Server.MapPath(SiteSettings.SiteSettingsFullPath);
                        
                        if (!Directory.Exists(basePath))
                        {
                            Directory.CreateDirectory(basePath);
                        }


                        File.WriteAllText(settingsPath, settingsJson);
                        if (!SiteSettings.ReloadCacheFromFile(out error))
                        {
                            result.HasError = true;
                            result.Errors.Add(error);
                        }
                            
                        result.Data = json; //return object
                    }
                    else
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

        
    /*[HttpPost]
    private HttpResult<SiteSettings> SaveSiteSettings2(SiteSettings json)
    {
        string error = string.Empty;
        var result = new HttpResult<SiteSettings>();
        try
        {
            using (Adm_SiteSettingsDataService SiteSettingsDataService = new Adm_SiteSettingsDataService(DbInstance, HttpUtil.Profile))
            {
                var entityReceived = new Adm_SiteSettings();
                var dbAttachedEntity = new Adm_SiteSettings();
                json.Map(entityReceived);
                if (SiteSettingsDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
    [System.Web.Mvc.HttpPost]
        //private HttpListResult<SiteSettings> SaveSiteSettingsList(IList<SiteSettings> jsonList)
        //{
        //    string error = string.Empty;
        //    var result = new HttpListResult<SiteSettings>();

        //    if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.SiteSettings.Instance.CanAdd, out error)
        //        && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.SiteSettings.Instance.CanEdit, out error))
        //    {
        //        result.HasError = true;
        //        result.Errors.Add(error);
        //        return result;
        //    }
        //    try
        //    {
        //        using (Adm_SiteSettingsDataService SiteSettingsDataService = new Adm_SiteSettingsDataService(DbInstance, HttpUtil.Profile))
        //        {
        //            var entityListReceived = new List<Adm_SiteSettings>();
        //            var dbAttachedListEntity = new List<Adm_SiteSettings>();
        //            jsonList.Map(entityListReceived);

        //            //foreach (var entity in entityListReceived)
        //            //{

        //            //}
        //            //if (SiteSettingsDataService.Save(entity, ref dbAttachedListEntity, out error))
        //            //{
        //            //    dbAttachedListEntity.Map(jsonList);
        //            //    result.Data = jsonList;//return object
        //            //}
        //            //else
        //            //{
        //            //    result.HasError = true;
        //            //    result.Errors.Add(error);
        //            //}
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.HasError = true;
        //        result.Errors.Add(ex.GetBaseException().Message.ToString());
        //    }
        //    return result;
        //}
        //public HttpResult<SiteSettings> GetDeleteSiteSettingsById(Int32 id)
        //{
        //    string error = string.Empty;
        //    var result = new HttpResult<SiteSettings>();

        //    if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.SiteSettings.Instance.CanDelete, out error))
        //    {
        //        result.HasError = true;
        //        result.Errors.Add(error);
        //        return result;
        //    }
        //    try
        //    {
        //        using (Adm_SiteSettingsDataService SiteSettingsDataService = new Adm_SiteSettingsDataService(DbInstance, HttpUtil.Profile))
        //        {
        //            if (!SiteSettingsDataService.DeleteById(id, out error))
        //            {
        //                result.HasError = true;
        //                result.Errors.Add(error);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        result.HasError = true;
        //        result.Errors.Add(ex.GetBaseException().Message.ToString());
        //    }
        //    return result;
        //}
        #endregion

        #region Local Method (should be always private)
        private bool IsValidToSaveSiteSettings(SiteSettings newObj, out string error)
        {
            error = "";
            
            return true;
        }
        private bool SaveSiteSettingsLogic(SiteSettings newObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Site Settings to save can't be null!";
                return false;
            }

            if (!IsValidToSaveSiteSettings(newObj, out error))
                return false;

            //bool isNewObject = true;

            //if (newObj.Id != 0)
            //{
            //    objToSave = DbInstance.Adm_SiteSettings.Instance.SingleOrDefault(x => x.Id == newObj.Id);
            //    isNewObject = false;
            //}
            //if (objToSave == null)
            //{   //new object
            //    isNewObject = true;
            //    objToSave = SiteSettings.Instance.GetNew(newObj.Id);
            //    DbInstance.SiteSettings.Instance.Add(objToSave);
            //    objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
            //    objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            //}

            //if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.SiteSettings.Instance.CanAdd,
            //        out error))
            //{
            //    return false;
            //}
            if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.SiteSettingsManager.CanEdit,
                   out error))
            {
                return false;
            }

            //binding object  

            //here save any child table
            return true;
        }
        #endregion
#endregion
    }
}
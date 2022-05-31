//File: UI Controller
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
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
using Microsoft.Ajax.Utilities;


namespace EMS.Web.UI.Areas.Admin.Controllers.WebApi
{

    public class ProfileChangeAuditApiController : EmployeeBaseApiController
	{
        public ProfileChangeAuditApiController()
        {  }
       
        #region ProfileChangeAudit 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<User_ProfileChangeAuditJson> GetPagedProfileChangeAudit(string textkey, int? pageSize, int? pageNo
           ,byte fieldEnumId = 0      
           ,long startSemesterId = 0      
           ,long endSemesterId = 0      
           ,string oldPk = null     
           ,string newPK = null
           ,string userName = null
           ,string newUserName = null  
           ,string oldValue = null  
           ,string newValue = null
           , string startDate = ""
           , string endDate = ""
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<User_ProfileChangeAuditJson>();
            //todo fix the permission
            if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.StudentManager.Report.CanViewProfileChangeAuditReport, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }

            if (fieldEnumId <= 0)
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add("Please Select a Valid Log Field");
                return result;
            }

            try
            {

                DateTime startOnlyDate = new DateTime();
                if (startDate.IsValid())
                {
                    startOnlyDate = Convert.ToDateTime(startDate).ToOnlyDate();
                }

                DateTime endOnlyDate = new DateTime();
                if (endDate.IsValid())
                {
                    endOnlyDate = Convert.ToDateTime(endDate).ToOnlyDate();
                }

                if (startOnlyDate > endOnlyDate && endOnlyDate.IsValid())
                {
                    result.HasError = true;
                    result.Errors.Add("Start date should not be greater than end date!");
                    return result;
                }

                using (User_ProfileChangeAuditDataService profileChangeAuditDataService = new User_ProfileChangeAuditDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<User_ProfileChangeAudit> query = DbInstance.User_ProfileChangeAudit
                      .Include(x=>x.User_Account)
                      .Include(x=>x.User_Account1)
                      .Include(x=>x.Aca_Semester)
                      .Where(x=>x.User_Account.UserTypeEnumId == (byte)User_Account.UserTypeEnum.Student)
                      .OrderByDescending(x => x.SemesterId)
                      .ThenByDescending(x => x.Id);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.OldPk.ToLower().Contains(textkey.ToLower()));
                    }
                    if (fieldEnumId > 0)
                    {
                        query = query.Where(q => q.FieldEnumId== fieldEnumId);
                    }

                    if (startSemesterId > 0)
                    {
                        query = query.Where(q => q.SemesterId >= startSemesterId);
                    }

                    if (endSemesterId > 0)
                    {
                        query = query.Where(q => q.SemesterId <= endSemesterId);
                    }

                    if (oldPk.IsValid())
                    {
                        query = query.Where(q => q.OldPk == oldPk);
                    }

                    if (newPK.IsValid())
                    {
                        query = query.Where(q => q.NewPk == newPK);
                    }

                    if (userName.IsValid())
                    {
                        query = query.Where(q => q.User_Account.UserName.ToLower().Contains(userName.Trim().ToLower()));
                    }
                    if (newUserName.IsValid())
                    {
                        query = query.Where(q => q.User_Account1.UserName.ToLower().Contains(newUserName.Trim().ToLower()));
                    }

                    if (oldValue.IsValid())
                    {
                        query = query.Where(q => q.OldValue.ToLower().Contains(oldValue.ToLower()));
                    }
                    if (newValue.IsValid())
                    {
                        query = query.Where(q => q.NewValue.ToLower().Contains(newValue.ToLower()));
                    }

                    if (startOnlyDate.IsValid())
                    {
                        if (endOnlyDate.IsValid())
                        {
                            query = query.Where(x => EntityFunctions.TruncateTime(x.CreateDate) >= startOnlyDate);
                        }
                        else
                        {
                            query = query.Where(x => EntityFunctions.TruncateTime(x.CreateDate) == startOnlyDate);
                        }
                        
                    }

                    if (endOnlyDate.IsValid())
                    {
                        query = query.Where(x => EntityFunctions.TruncateTime(x.CreateDate) <= endOnlyDate);
                    }


                    var entities = profileChangeAuditDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);

                    var distinctChangeUsersIds =
                        entities.DistinctBy(x => x.CreateById).Select(x => x.CreateById).ToList();

                    var jsons = new List<User_ProfileChangeAuditJson>();
                    entities.Map(jsons);

                    #region Adding Changed User Name

                    var userNameAndIdList = DbInstance.User_Account.Where(x => distinctChangeUsersIds.Contains(x.Id))
                        .Select(
                            x =>
                                new
                                {
                                    UserId = x.Id.ToString(),
                                    UserName = x.UserName,
                                    FullName = x.FullName
                                }).ToList();

                    foreach (var json in jsons)
                    {
                        var user = userNameAndIdList.FirstOrDefault(x => x.UserId == json.CreateById);
                        json.ChangedByUser = $"{user.FullName} [{user.UserName}]";
                    }

                    #endregion

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
        private HttpListResult<User_ProfileChangeAuditJson> GetAllProfileChangeAudit()
        {
            string error = string.Empty;
            var result = new HttpListResult<User_ProfileChangeAuditJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.ProfileChangeAudit.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.ProfileChangeAudit.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.ProfileChangeAudit.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (User_ProfileChangeAuditDataService profileChangeAuditDataService = new User_ProfileChangeAuditDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<User_ProfileChangeAuditJson>();
                    var entities = profileChangeAuditDataService.GetAll(out error);
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
        public HttpResult<User_ProfileChangeAuditJson> GetProfileChangeAuditById(Int32 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<User_ProfileChangeAuditJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.ProfileChangeAudit.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.ProfileChangeAudit.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.ProfileChangeAudit.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (User_ProfileChangeAuditDataService profileChangeAuditDataService = new User_ProfileChangeAuditDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new User_ProfileChangeAuditJson();
                    User_ProfileChangeAudit entity;
                    if (id <= 0)
                    {
                        entity = User_ProfileChangeAudit.GetNew();
                    }
                    else
                    {
                        entity = profileChangeAuditDataService.GetById(id);
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
        private HttpResult<User_ProfileChangeAuditJson> GetProfileChangeAuditByIdWithChild(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<User_ProfileChangeAuditJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.ProfileChangeAudit.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.ProfileChangeAudit.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.ProfileChangeAudit.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (User_ProfileChangeAuditDataService profileChangeAuditDataService = new User_ProfileChangeAuditDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new User_ProfileChangeAuditJson();
                    User_ProfileChangeAudit entity;
                    if (id <= 0)
                    {
                        entity = User_ProfileChangeAudit.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("User_ProfileChangeAudit");
                        //includeTables.Add("User_ProfileChangeAudit");

                        entity = profileChangeAuditDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<User_ProfileChangeAuditJson> GetProfileChangeAuditListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<User_ProfileChangeAuditJson>();
            try
            {
                //User_ProfileChangeAuditDataService profileChangeAuditDataService =
                //    new User_ProfileChangeAuditDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.FieldEnumList = EnumUtil.GetEnumList(typeof(User_ProfileChangeAudit.FieldEnum));
                //DropDown Option Tables

                result.DataExtra.SemesterList = Aca_Semester.GetDropdownList(DbInstance);

                result.DataExtra.ProgramList = DbInstance.Aca_Program
                    .Where(x => !x.IsDeleted)
                    .OrderBy(x => x.ShortTitle)
                    .AsEnumerable().Select(t => new
                    {
                        Id = t.Id,
                        Name = t.ShortTitle,
                    }).ToList();
                //result.DataExtra.AccountList = DbInstance.User_Account.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<User_ProfileChangeAuditJson> GetProfileChangeAuditDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<User_ProfileChangeAuditJson>();
            try
            {
                //User_ProfileChangeAuditDataService profileChangeAuditDataService =
                //    new User_ProfileChangeAuditDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.FieldEnumList = EnumUtil.GetEnumList(typeof(User_ProfileChangeAudit.FieldEnum));
                //DropDown Option Tables
                 
                //result.DataExtra.AccountList = DbInstance.User_Account.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
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
        public HttpResult<User_ProfileChangeAuditJson> SaveProfileChangeAudit(User_ProfileChangeAuditJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<User_ProfileChangeAuditJson>();
            //optional permission, check permission in the SaveProfileChangeAuditLogic insted
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.ProfileChangeAudit.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.ProfileChangeAudit.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                 var entityReceived = new User_ProfileChangeAudit();
                 var dbAttachedEntity = new User_ProfileChangeAudit();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (SaveProfileChangeAuditLogic(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpResult<User_ProfileChangeAuditJson> SaveProfileChangeAudit2(User_ProfileChangeAuditJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<User_ProfileChangeAuditJson>();
            try
            {
                using (User_ProfileChangeAuditDataService profileChangeAuditDataService = new User_ProfileChangeAuditDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new User_ProfileChangeAudit();
                    var dbAttachedEntity = new User_ProfileChangeAudit();
                    json.Map(entityReceived);
                    if (profileChangeAuditDataService.Save(entityReceived, ref dbAttachedEntity, out error))
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
        private HttpListResult<User_ProfileChangeAuditJson> SaveProfileChangeAuditList(IList<User_ProfileChangeAuditJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<User_ProfileChangeAuditJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.ProfileChangeAudit.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.ProfileChangeAudit.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (User_ProfileChangeAuditDataService profileChangeAuditDataService = new User_ProfileChangeAuditDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<User_ProfileChangeAudit>();
                    var dbAttachedListEntity = new List<User_ProfileChangeAudit>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (profileChangeAuditDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<User_ProfileChangeAuditJson> GetDeleteProfileChangeAuditById(Int32 id)
        {
            string error = string.Empty;
            var result = new HttpResult<User_ProfileChangeAuditJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AdminArea.ProfileChangeAudit.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (User_ProfileChangeAuditDataService profileChangeAuditDataService = new User_ProfileChangeAuditDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!profileChangeAuditDataService.DeleteById(id, out error))
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
        private bool IsValidToSaveProfileChangeAudit(User_ProfileChangeAudit newObj, out string error)
        {
            error = "";
            if (newObj.UserId<=0)
            {
                error += "Please select valid User.";
                return false;
            }

            if (newObj.OldPk.IsValid() && newObj.OldPk.Length>20)
            {
                error += "Old Pk exceeded its maximum length 20.";
                return false;
            }
            if (newObj.NewPk.IsValid() && newObj.NewPk.Length>20)
            {
                error += "New Pk exceeded its maximum length 20.";
                return false;
            }
            if (newObj.OldValue.IsValid() && newObj.OldValue.Length>200)
            {
                error += "Old Value exceeded its maximum length 200.";
                return false;
            }
            if (newObj.NewValue.IsValid() && newObj.NewValue.Length>200)
            {
                error += "New Value exceeded its maximum length 200.";
                return false;
            }

            //TODO write your custom validation here.
            //var res =  DbInstance.User_ProfileChangeAudit.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
                //error = "A ProfileChangeAudit already exists!";
                //return false;
            //}
            return true;
        }
        private bool SaveProfileChangeAuditLogic(User_ProfileChangeAudit newObj,ref User_ProfileChangeAudit dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Profile Change Audit to save can't be null!";
                return false;
            }

            if ( !IsValidToSaveProfileChangeAudit(newObj, out error))
                return false;

            bool isNewObject = true;
            User_ProfileChangeAudit objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.User_ProfileChangeAudit.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = User_ProfileChangeAudit.GetNew(newObj.Id);
                DbInstance.User_ProfileChangeAudit.Add(objToSave);
               objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
               objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.ProfileChangeAudit.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdminArea.ProfileChangeAudit.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
           objToSave.UserId =  newObj.UserId ;
           objToSave.FieldEnumId =  newObj.FieldEnumId ;
           objToSave.OldPk =  newObj.OldPk ;
           objToSave.NewPk =  newObj.NewPk ;
           objToSave.OldValue =  newObj.OldValue ;
           objToSave.NewValue =  newObj.NewValue ;
           objToSave.Remark =  newObj.Remark ;
           
           
           dbAddedObj = objToSave;
           
            //here save any child table
            return true;
        }
        #endregion
        #endregion

	}
}

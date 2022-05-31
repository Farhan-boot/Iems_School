//File: DataService for UAC_VerificationAudit
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using EMS.CoreLibrary.Helpers;
using EMS.Framework.Objects;
using EMS.Framework.Utils;
using EMS.DataAccess.Data;
using EMS.DataService.Bases;

namespace EMS.DataService
{
    public class UAC_VerificationAuditDataService : UAC_VerificationAuditDataServiceBase
    {
        public UAC_VerificationAuditDataService(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
        }

        #region base overrides
        /// <summary>
        /// override base IsValidVerificationAuditObj function. use it if you need custom field validation.
        /// </summary>
        /// <param name="newObj"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public override bool IsValidToSave(UAC_VerificationAudit newObj, out string error)
        {
            error = "";
            //comment out this line if you need custom validation for fields.
            if (!base.IsValidToSave(newObj, out error))
            {
                return false;
            }
            //ToDO write your custom field validation here.
            return true;

        }
        /// <summary>
        /// Must implement permission for save VerificationAudit.
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        protected override bool HasSavePermission(out string error)
        {
            //TODO Must check permission
            //if (!PermissionUtil.HasPermission(PermissionCollection.AdminModule.VerificationAudit.CanUpdate, out error))
            //{
            //    return false;
            //}
            error = "";
            return true;
        }
        #endregion

        #region Custom Get VerificationAudit

        #endregion

        #region Custom Save VerificationAudit
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objToSave"></param>
        /// <param name="isNewObject"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        private bool CheckValidExtra(UAC_VerificationAudit objToSave, bool isNewObject, out string error)
        {
            error = "";
            //TODO write your custom validation here.
            return true;
        }
        /// <summary>
        /// an example to implement custom save method.
        /// </summary>
        /// <param name="newObj"></param>
        /// <param name="objToSave"></param>
        /// <param name="isNewObject"></param>
        /// <param name="transactionScope"></param>
        /// <param name="error"></param>
        /// <param name="columnsToCopy"></param>
        /// <returns></returns>
        private bool OnSaveExtra(UAC_VerificationAudit newObj, UAC_VerificationAudit dbObj, bool isNewObject, DbContextTransaction transactionScope, out string error, params string[] columnsToCopy)
        {
            error = "";
            if (CheckValidExtra(newObj, isNewObject, out error))
            {
                return false;
            }
            //ToDO write your custom business here
            //CopyUtil.CopySelectedColumns(newObj, dbObj, columnsToCopy);
                //dbObj.UserId =  newObj.UserId ;
                //dbObj.InitiatedDate =  newObj.InitiatedDate ;
                //dbObj.InitiatedById =  newObj.InitiatedById ;
                //dbObj.Code =  newObj.Code ;
                //dbObj.ValidHours =  newObj.ValidHours ;
                //dbObj.NewDeviceName =  newObj.NewDeviceName ;
                //dbObj.RequestByEnumId =  newObj.RequestByEnumId ;
                //dbObj.RequestTypeEnumId =  newObj.RequestTypeEnumId ;
                //dbObj.StatusEnumId =  newObj.StatusEnumId ;
                //dbObj.ConfirmedDate =  newObj.ConfirmedDate ;
                //dbObj.ConfirmedById =  newObj.ConfirmedById ;
                //dbObj.FromIp =  newObj.FromIp ;

            return true;
        }
        /// <summary>
        /// an example of custom save method.
        /// </summary>
        /// <param name="newObj"></param>
        /// <param name="error"></param>
        private void SaveCustomExample(UAC_VerificationAudit newObj, out string error)
        {
             error = "";
            //Save(newObj, out error);//using default save method, copy all member except child obj
            //Save(newObj, out error, null, UAC_VerificationAudit.Property_FullName);//using default save method, copy only params except child obj
            //Save(newObj, out error, SaveExtraExample, UAC_VerificationAudit.Property_ABC);//customizing default save method using delegate.
            Save(newObj, OnSaveExtra, out error );//customizing default save method using delegate.
          
        }
        #endregion

        #region Coustom Others

        #endregion
    }
}


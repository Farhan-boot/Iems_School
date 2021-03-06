//File: DataService for UCS_SmsLog
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
    public class UCS_SmsLogDataService : UCS_SmsLogDataServiceBase
    {
        public UCS_SmsLogDataService(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
        }

        #region base overrides
        /// <summary>
        /// override base IsValidSmsLogObj function. use it if you need custom field validation.
        /// </summary>
        /// <param name="newObj"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public override bool IsValidToSave(UCS_SmsLog newObj, out string error)
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
        /// Must implement permission for save SmsLog.
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        protected override bool HasSavePermission(out string error)
        {
            //TODO Must check permission
            //if (!PermissionUtil.HasPermission(PermissionCollection.AdminModule.SmsLog.CanUpdate, out error))
            //{
            //    return false;
            //}
            error = "";
            return true;
        }
        #endregion

        #region Custom Get SmsLog

        #endregion

        #region Custom Save SmsLog
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objToSave"></param>
        /// <param name="isNewObject"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        private bool CheckValidExtra(UCS_SmsLog objToSave, bool isNewObject, out string error)
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
        private bool OnSaveExtra(UCS_SmsLog newObj, UCS_SmsLog dbObj, bool isNewObject, DbContextTransaction transactionScope, out string error, params string[] columnsToCopy)
        {
            error = "";
            if (CheckValidExtra(newObj, isNewObject, out error))
            {
                return false;
            }
            //ToDO write your custom business here
            //CopyUtil.CopySelectedColumns(newObj, dbObj, columnsToCopy);
                //dbObj.ProviderId =  newObj.ProviderId ;
                //dbObj.UserId =  newObj.UserId ;
                //dbObj.CallerId =  newObj.CallerId ;
                //dbObj.TextMessage =  newObj.TextMessage ;
                //dbObj.RequestDate =  newObj.RequestDate ;
                //dbObj.SendById =  newObj.SendById ;
                //dbObj.SendDate =  newObj.SendDate ;
                //dbObj.ModuleEnumId =  newObj.ModuleEnumId ;
                //dbObj.MessageId =  newObj.MessageId ;
                //dbObj.StatusEnumId =  newObj.StatusEnumId ;
                //dbObj.StatusText =  newObj.StatusText ;
                //dbObj.ErrorCode =  newObj.ErrorCode ;
                //dbObj.ErrorText =  newObj.ErrorText ;
                //dbObj.SMSCount =  newObj.SMSCount ;
                //dbObj.CurrentCredit =  newObj.CurrentCredit ;

            return true;
        }
        /// <summary>
        /// an example of custom save method.
        /// </summary>
        /// <param name="newObj"></param>
        /// <param name="error"></param>
        private void SaveCustomExample(UCS_SmsLog newObj, out string error)
        {
             error = "";
            //Save(newObj, out error);//using default save method, copy all member except child obj
            //Save(newObj, out error, null, UCS_SmsLog.Property_FullName);//using default save method, copy only params except child obj
            //Save(newObj, out error, SaveExtraExample, UCS_SmsLog.Property_ABC);//customizing default save method using delegate.
            Save(newObj, OnSaveExtra, out error );//customizing default save method using delegate.
          
        }
        #endregion

        #region Coustom Others

        #endregion
    }
}


﻿//File: DataService for User_ContactAddress
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
    public class User_ContactAddressDataService : User_ContactAddressDataServiceBase
    {
        public User_ContactAddressDataService(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
        }

        #region base overrides
        /// <summary>
        /// override base IsValidContactAddressObj function. use it if you need custom field validation.
        /// </summary>
        /// <param name="newObj"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public override bool IsValidToSave(User_ContactAddress newObj, out string error)
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
        /// Must implement permission for save ContactAddress.
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        protected override bool HasSavePermission(out string error)
        {
            //TODO Must check permission
            //if (!PermissionUtil.HasPermission(PermissionCollection.AdminModule.ContactAddress.CanUpdate, out error))
            //{
            //    return false;
            //}
            error = "";
            return true;
        }
        #endregion

        #region Custom Get ContactAddress

        #endregion

        #region Custom Save ContactAddress
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objToSave"></param>
        /// <param name="isNewObject"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        private bool CheckValidExtra(User_ContactAddress objToSave, bool isNewObject, out string error)
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
        private bool SaveExtraExample(User_ContactAddress newObj, User_ContactAddress objToSave, bool isNewObject, DbContextTransaction transactionScope, out string error, params string[] columnsToCopy)
        {
            error = "";
            if (CheckValidExtra(newObj, isNewObject, out error))
            {
                return false;
            }
            //commenting base
            // base.SaveExtra(newObj, objToSave, isNewObject, out error);
            //ToDO write your custom business here
            CopyUtil.CopySelectedColumns(newObj, objToSave, columnsToCopy);
            //objToSave.Column = newObj.Column;
            return true;
        }
        /// <summary>
        /// an example of custom save method.
        /// </summary>
        /// <param name="newObj"></param>
        /// <param name="error"></param>
        private void SaveCustomExample(User_ContactAddress newObj, out string error)
        {
             error = "";
            //Save(newObj, out error);//using default save method, copy all member except child obj
            //Save(newObj, out error, null, User_ContactAddress.Property_FullName);//using default save method, copy only params except child obj
            //Save(newObj, out error, SaveExtraExample, User_ContactAddress.Property_ABC);//customizing default save method using delegate.
            Save(newObj, SaveExtraExample, out error );//customizing default save method using delegate.
          
        }
        #endregion

        #region Coustom Others

        #endregion
    }
}


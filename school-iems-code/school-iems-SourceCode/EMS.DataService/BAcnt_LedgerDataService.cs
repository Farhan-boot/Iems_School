//File: DataService for BAcnt_Ledger
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
    public class BAcnt_LedgerDataService : BAcnt_LedgerDataServiceBase
    {
        public BAcnt_LedgerDataService(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
        }

        #region base overrides
        /// <summary>
        /// override base IsValidLedgerObj function. use it if you need custom field validation.
        /// </summary>
        /// <param name="newObj"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public override bool IsValidToSave(BAcnt_Ledger newObj, out string error)
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
        /// Must implement permission for save Ledger.
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        protected override bool HasSavePermission(out string error)
        {
            //TODO Must check permission
            //if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingModule.Ledger.CanUpdate, out error))
            //{
            //    return false;
            //}
            error = "";
            return true;
        }
        #endregion

        #region Custom Get Ledger

        #endregion

        #region Custom Save Ledger
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objToSave"></param>
        /// <param name="isNewObject"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        private bool CheckValidExtra(BAcnt_Ledger objToSave, bool isNewObject, out string error)
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
        private bool OnSaveExtra(BAcnt_Ledger newObj, BAcnt_Ledger dbObj, bool isNewObject, DbContextTransaction transactionScope, out string error, params string[] columnsToCopy)
        {
            error = "";
            if (CheckValidExtra(newObj, isNewObject, out error))
            {
                return false;
            }
            //ToDO write your custom business here
            //CopyUtil.CopySelectedColumns(newObj, dbObj, columnsToCopy);
                //dbObj.Name =  newObj.Name ;
                //dbObj.Code =  newObj.Code ;
                //dbObj.InternalCode =  newObj.InternalCode ;
                //dbObj.TypeEnumId =  newObj.TypeEnumId ;
                //dbObj.ParentId =  newObj.ParentId ;
                //dbObj.IsGroup =  newObj.IsGroup ;
                //dbObj.OpenningBalance =  newObj.OpenningBalance ;
                //dbObj.OpeningDate =  newObj.OpeningDate ;
                //dbObj.Remark =  newObj.Remark ;
                //dbObj.History =  newObj.History ;
                //dbObj.IsDeleted =  newObj.IsDeleted ;
                //dbObj.BranchId =  newObj.BranchId ;
                //dbObj.CompanyId =  newObj.CompanyId ;

            return true;
        }
        /// <summary>
        /// an example of custom save method.
        /// </summary>
        /// <param name="newObj"></param>
        /// <param name="error"></param>
        private void SaveCustomExample(BAcnt_Ledger newObj, out string error)
        {
             error = "";
            //Save(newObj, out error);//using default save method, copy all member except child obj
            //Save(newObj, out error, null, BAcnt_Ledger.Property_FullName);//using default save method, copy only params except child obj
            //Save(newObj, out error, SaveExtraExample, BAcnt_Ledger.Property_ABC);//customizing default save method using delegate.
            Save(newObj, OnSaveExtra, out error );//customizing default save method using delegate.
          
        }
        #endregion

        #region Coustom Others

        #endregion
    }
}


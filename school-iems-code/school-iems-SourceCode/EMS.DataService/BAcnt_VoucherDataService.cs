//File: DataService for BAcnt_Voucher
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
    public class BAcnt_VoucherDataService : BAcnt_VoucherDataServiceBase
    {
        public BAcnt_VoucherDataService(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
        }

        #region base overrides
        /// <summary>
        /// override base IsValidVoucherObj function. use it if you need custom field validation.
        /// </summary>
        /// <param name="newObj"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public override bool IsValidToSave(BAcnt_Voucher newObj, out string error)
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
        /// Must implement permission for save Voucher.
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        protected override bool HasSavePermission(out string error)
        {
            //TODO Must check permission
            //if (!PermissionUtil.HasPermission(PermissionCollection.BasicAccountingModule.Voucher.CanUpdate, out error))
            //{
            //    return false;
            //}
            error = "";
            return true;
        }
        #endregion

        #region Custom Get Voucher

        #endregion

        #region Custom Save Voucher
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objToSave"></param>
        /// <param name="isNewObject"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        private bool CheckValidExtra(BAcnt_Voucher objToSave, bool isNewObject, out string error)
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
        private bool OnSaveExtra(BAcnt_Voucher newObj, BAcnt_Voucher dbObj, bool isNewObject, DbContextTransaction transactionScope, out string error, params string[] columnsToCopy)
        {
            error = "";
            if (CheckValidExtra(newObj, isNewObject, out error))
            {
                return false;
            }
            //ToDO write your custom business here
            //CopyUtil.CopySelectedColumns(newObj, dbObj, columnsToCopy);
                //dbObj.Date =  newObj.Date ;
                //dbObj.VoucherNo =  newObj.VoucherNo ;
                //dbObj.VoucherTypeId =  newObj.VoucherTypeId ;
                //dbObj.VoucherStatusEnumId =  newObj.VoucherStatusEnumId ;
                //dbObj.Remark =  newObj.Remark ;
                //dbObj.History =  newObj.History ;
                //dbObj.IsDeleted =  newObj.IsDeleted ;
                //dbObj.Narration =  newObj.Narration ;
                //dbObj.JournalTypeEnumId =  newObj.JournalTypeEnumId ;
                //dbObj.JournalStatusEnumId =  newObj.JournalStatusEnumId ;
                //dbObj.BankId =  newObj.BankId ;
                //dbObj.ManualVoucherNo =  newObj.ManualVoucherNo ;
                //dbObj.BranchId =  newObj.BranchId ;
                //dbObj.CompanyId =  newObj.CompanyId ;

            return true;
        }
        /// <summary>
        /// an example of custom save method.
        /// </summary>
        /// <param name="newObj"></param>
        /// <param name="error"></param>
        private void SaveCustomExample(BAcnt_Voucher newObj, out string error)
        {
             error = "";
            //Save(newObj, out error);//using default save method, copy all member except child obj
            //Save(newObj, out error, null, BAcnt_Voucher.Property_FullName);//using default save method, copy only params except child obj
            //Save(newObj, out error, SaveExtraExample, BAcnt_Voucher.Property_ABC);//customizing default save method using delegate.
            Save(newObj, OnSaveExtra, out error );//customizing default save method using delegate.
          
        }
        #endregion

        #region Coustom Others

        #endregion
    }
}


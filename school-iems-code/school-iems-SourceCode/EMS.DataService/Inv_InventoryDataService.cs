//File: DataService for Inv_Inventory
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
    public class Inv_InventoryDataService : Inv_InventoryDataServiceBase
    {
        public Inv_InventoryDataService(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
        }

        #region base overrides
        /// <summary>
        /// override base IsValidInventoryObj function. use it if you need custom field validation.
        /// </summary>
        /// <param name="newObj"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public override bool IsValidToSave(Inv_Inventory newObj, out string error)
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
        /// Must implement permission for save Inventory.
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        protected override bool HasSavePermission(out string error)
        {
            //TODO Must check permission
            //if (!PermissionUtil.HasPermission(PermissionCollection.InventoryModule.Inventory.CanUpdate, out error))
            //{
            //    return false;
            //}
            error = "";
            return true;
        }
        #endregion

        #region Custom Get Inventory

        #endregion

        #region Custom Save Inventory
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objToSave"></param>
        /// <param name="isNewObject"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        private bool CheckValidExtra(Inv_Inventory objToSave, bool isNewObject, out string error)
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
        private bool OnSaveExtra(Inv_Inventory newObj, Inv_Inventory dbObj, bool isNewObject, DbContextTransaction transactionScope, out string error, params string[] columnsToCopy)
        {
            error = "";
            if (CheckValidExtra(newObj, isNewObject, out error))
            {
                return false;
            }
            //ToDO write your custom business here
            //CopyUtil.CopySelectedColumns(newObj, dbObj, columnsToCopy);
                //dbObj.StoreId =  newObj.StoreId ;
                //dbObj.VoucherNo =  newObj.VoucherNo ;
                //dbObj.VoucherDate =  newObj.VoucherDate ;
                //dbObj.InvoiceNo =  newObj.InvoiceNo ;
                //dbObj.InvoiceDate =  newObj.InvoiceDate ;
                //dbObj.PurchaseOrderNo =  newObj.PurchaseOrderNo ;
                //dbObj.PurchaseOrderDate =  newObj.PurchaseOrderDate ;
                //dbObj.ChalanNo =  newObj.ChalanNo ;
                //dbObj.ChalanDate =  newObj.ChalanDate ;
                //dbObj.ReceivedDate =  newObj.ReceivedDate ;
                //dbObj.ReceivedBy =  newObj.ReceivedBy ;
                //dbObj.SupplierId =  newObj.SupplierId ;
                //dbObj.Remark =  newObj.Remark ;
                //dbObj.IsDeleted =  newObj.IsDeleted ;
                //dbObj.History =  newObj.History ;

            return true;
        }
        /// <summary>
        /// an example of custom save method.
        /// </summary>
        /// <param name="newObj"></param>
        /// <param name="error"></param>
        private void SaveCustomExample(Inv_Inventory newObj, out string error)
        {
             error = "";
            //Save(newObj, out error);//using default save method, copy all member except child obj
            //Save(newObj, out error, null, Inv_Inventory.Property_FullName);//using default save method, copy only params except child obj
            //Save(newObj, out error, SaveExtraExample, Inv_Inventory.Property_ABC);//customizing default save method using delegate.
            Save(newObj, OnSaveExtra, out error );//customizing default save method using delegate.
          
        }
        #endregion

        #region Coustom Others

        #endregion
    }
}


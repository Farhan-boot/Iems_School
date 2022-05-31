//File: DataService for Invt_EquipmentDetailIT
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
    public class Invt_EquipmentDetailITDataService : Invt_EquipmentDetailITDataServiceBase
    {
        public Invt_EquipmentDetailITDataService(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
        }

        #region base overrides
        /// <summary>
        /// override base IsValidEquipmentDetailITObj function. use it if you need custom field validation.
        /// </summary>
        /// <param name="newObj"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public override bool IsValidToSave(Invt_EquipmentDetailIT newObj, out string error)
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
        /// Must implement permission for save EquipmentDetailIT.
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        protected override bool HasSavePermission(out string error)
        {
            //TODO Must check permission
            //if (!PermissionUtil.HasPermission(PermissionCollection.AdminModule.EquipmentDetailIT.CanUpdate, out error))
            //{
            //    return false;
            //}
            error = "";
            return true;
        }
        #endregion

        #region Custom Get EquipmentDetailIT

        #endregion

        #region Custom Save EquipmentDetailIT
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objToSave"></param>
        /// <param name="isNewObject"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        private bool CheckValidExtra(Invt_EquipmentDetailIT objToSave, bool isNewObject, out string error)
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
        private bool OnSaveExtra(Invt_EquipmentDetailIT newObj, Invt_EquipmentDetailIT dbObj, bool isNewObject, DbContextTransaction transactionScope, out string error, params string[] columnsToCopy)
        {
            error = "";
            if (CheckValidExtra(newObj, isNewObject, out error))
            {
                return false;
            }
            //ToDO write your custom business here
            //CopyUtil.CopySelectedColumns(newObj, dbObj, columnsToCopy);
                //dbObj.Model =  newObj.Model ;
                //dbObj.Processor =  newObj.Processor ;
                //dbObj.HDD =  newObj.HDD ;
                //dbObj.Ram =  newObj.Ram ;
                //dbObj.Motherboard =  newObj.Motherboard ;
                //dbObj.SoundCard =  newObj.SoundCard ;
                //dbObj.GraphicsCard =  newObj.GraphicsCard ;
                //dbObj.LANCard =  newObj.LANCard ;
                //dbObj.Casing =  newObj.Casing ;
                //dbObj.PowerSupply =  newObj.PowerSupply ;
                //dbObj.FloppyDrive =  newObj.FloppyDrive ;
                //dbObj.OpticalDrive =  newObj.OpticalDrive ;
                //dbObj.Other1Title =  newObj.Other1Title ;
                //dbObj.Other1Desc =  newObj.Other1Desc ;
                //dbObj.Other2Title =  newObj.Other2Title ;
                //dbObj.Other2Desc =  newObj.Other2Desc ;
                //dbObj.IsDeleted =  newObj.IsDeleted ;

            return true;
        }
        /// <summary>
        /// an example of custom save method.
        /// </summary>
        /// <param name="newObj"></param>
        /// <param name="error"></param>
        private void SaveCustomExample(Invt_EquipmentDetailIT newObj, out string error)
        {
             error = "";
            //Save(newObj, out error);//using default save method, copy all member except child obj
            //Save(newObj, out error, null, Invt_EquipmentDetailIT.Property_FullName);//using default save method, copy only params except child obj
            //Save(newObj, out error, SaveExtraExample, Invt_EquipmentDetailIT.Property_ABC);//customizing default save method using delegate.
            Save(newObj, OnSaveExtra, out error );//customizing default save method using delegate.
          
        }
        #endregion

        #region Coustom Others

        #endregion
    }
}


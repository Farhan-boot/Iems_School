//File: DataService for Aca_ClassMaterialFileMap
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
    public class Aca_ClassMaterialFileMapDataService : Aca_ClassMaterialFileMapDataServiceBase
    {
        public Aca_ClassMaterialFileMapDataService(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
        }

        #region base overrides
        /// <summary>
        /// override base IsValidClassMaterialFileMapObj function. use it if you need custom field validation.
        /// </summary>
        /// <param name="newObj"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public override bool IsValidToSave(Aca_ClassMaterialFileMap newObj, out string error)
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
        /// Must implement permission for save ClassMaterialFileMap.
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        protected override bool HasSavePermission(out string error)
        {
            //TODO Must check permission
            //if (!PermissionUtil.HasPermission(PermissionCollection.AcademicModule.ClassMaterialFileMap.CanUpdate, out error))
            //{
            //    return false;
            //}
            error = "";
            return true;
        }
        #endregion

        #region Custom Get ClassMaterialFileMap

        #endregion

        #region Custom Save ClassMaterialFileMap
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objToSave"></param>
        /// <param name="isNewObject"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        private bool CheckValidExtra(Aca_ClassMaterialFileMap objToSave, bool isNewObject, out string error)
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
        private bool OnSaveExtra(Aca_ClassMaterialFileMap newObj, Aca_ClassMaterialFileMap dbObj, bool isNewObject, DbContextTransaction transactionScope, out string error, params string[] columnsToCopy)
        {
            error = "";
            if (CheckValidExtra(newObj, isNewObject, out error))
            {
                return false;
            }
            //ToDO write your custom business here
            //CopyUtil.CopySelectedColumns(newObj, dbObj, columnsToCopy);
                dbObj.ClassId =  newObj.ClassId ;
                dbObj.FileId =  newObj.FileId ;
                dbObj.UserId =  newObj.UserId ;

            return true;
        }
        /// <summary>
        /// an example of custom save method.
        /// </summary>
        /// <param name="newObj"></param>
        /// <param name="error"></param>
        private void SaveCustomExample(Aca_ClassMaterialFileMap newObj, out string error)
        {
             error = "";
            //Save(newObj, out error);//using default save method, copy all member except child obj
            //Save(newObj, out error, null, Aca_ClassMaterialFileMap.Property_FullName);//using default save method, copy only params except child obj
            //Save(newObj, out error, SaveExtraExample, Aca_ClassMaterialFileMap.Property_ABC);//customizing default save method using delegate.
            Save(newObj, OnSaveExtra, out error );//customizing default save method using delegate.
          
        }
        #endregion

        #region Coustom Others

        #endregion
    }
}


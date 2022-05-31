//File: DataService for Aca_GradingPolicyOption
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
    public class Aca_GradingPolicyOptionDataService : Aca_GradingPolicyOptionDataServiceBase
    {
        public Aca_GradingPolicyOptionDataService(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
        }

        #region base overrides
        /// <summary>
        /// override base IsValidGradingPolicyOptionObj function. use it if you need custom field validation.
        /// </summary>
        /// <param name="newObj"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public override bool IsValidToSave(Aca_GradingPolicyOption newObj, out string error)
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
        /// Must implement permission for save GradingPolicyOption.
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        protected override bool HasSavePermission(out string error)
        {
            //TODO Must check permission
            //if (!PermissionUtil.HasPermission(PermissionCollection.AcademicModule.GradingPolicyOption.CanUpdate, out error))
            //{
            //    return false;
            //}
            error = "";
            return true;
        }
        #endregion

        #region Custom Get GradingPolicyOption

        #endregion

        #region Custom Save GradingPolicyOption
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objToSave"></param>
        /// <param name="isNewObject"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        private bool CheckValidExtra(Aca_GradingPolicyOption objToSave, bool isNewObject, out string error)
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
        private bool OnSaveExtra(Aca_GradingPolicyOption newObj, Aca_GradingPolicyOption dbObj, bool isNewObject, DbContextTransaction transactionScope, out string error, params string[] columnsToCopy)
        {
            error = "";
            if (CheckValidExtra(newObj, isNewObject, out error))
            {
                return false;
            }
            //ToDO write your custom business here
            //CopyUtil.CopySelectedColumns(newObj, dbObj, columnsToCopy);
                dbObj.Grade =  newObj.Grade ;
                dbObj.GradePoint =  newObj.GradePoint ;
                dbObj.Description =  newObj.Description ;
                dbObj.GradingPolicyId =  newObj.GradingPolicyId ;
                dbObj.ScoreStartLimit =  newObj.ScoreStartLimit ;
                dbObj.ScoreEndLimit =  newObj.ScoreEndLimit ;
                dbObj.LimitOperatorEnumId =  newObj.LimitOperatorEnumId ;
                dbObj.SerNo =  newObj.SerNo ;
                dbObj.IsCountCredit =  newObj.IsCountCredit ;
                dbObj.IsCountGPA =  newObj.IsCountGPA ;
                dbObj.IsDisplayScore =  newObj.IsDisplayScore ;
                dbObj.IsIncomplete =  newObj.IsIncomplete ;
                dbObj.IsWithdrawn =  newObj.IsWithdrawn ;
                dbObj.IsContinuation =  newObj.IsContinuation ;

            return true;
        }
        /// <summary>
        /// an example of custom save method.
        /// </summary>
        /// <param name="newObj"></param>
        /// <param name="error"></param>
        private void SaveCustomExample(Aca_GradingPolicyOption newObj, out string error)
        {
             error = "";
            //Save(newObj, out error);//using default save method, copy all member except child obj
            //Save(newObj, out error, null, Aca_GradingPolicyOption.Property_FullName);//using default save method, copy only params except child obj
            //Save(newObj, out error, SaveExtraExample, Aca_GradingPolicyOption.Property_ABC);//customizing default save method using delegate.
            Save(newObj, OnSaveExtra, out error );//customizing default save method using delegate.
          
        }
        #endregion

        #region Coustom Others

        #endregion
    }
}


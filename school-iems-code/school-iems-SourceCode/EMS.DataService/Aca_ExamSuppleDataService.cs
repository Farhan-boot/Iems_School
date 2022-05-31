//File: DataService for Aca_ExamSupple
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
    public class Aca_ExamSuppleDataService : Aca_ExamSuppleDataServiceBase
    {
        public Aca_ExamSuppleDataService(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
        }

        #region base overrides
        /// <summary>
        /// override base IsValidExamSuppleObj function. use it if you need custom field validation.
        /// </summary>
        /// <param name="newObj"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public override bool IsValidToSave(Aca_ExamSupple newObj, out string error)
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
        /// Must implement permission for save ExamSupple.
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        protected override bool HasSavePermission(out string error)
        {
            //TODO Must check permission
            //if (!PermissionUtil.HasPermission(PermissionCollection.AcademicModule.ExamSupple.CanUpdate, out error))
            //{
            //    return false;
            //}
            error = "";
            return true;
        }
        #endregion

        #region Custom Get ExamSupple

        #endregion

        #region Custom Save ExamSupple
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objToSave"></param>
        /// <param name="isNewObject"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        private bool CheckValidExtra(Aca_ExamSupple objToSave, bool isNewObject, out string error)
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
        private bool OnSaveExtra(Aca_ExamSupple newObj, Aca_ExamSupple dbObj, bool isNewObject, DbContextTransaction transactionScope, out string error, params string[] columnsToCopy)
        {
            error = "";
            if (CheckValidExtra(newObj, isNewObject, out error))
            {
                return false;
            }
            //ToDO write your custom business here
            //CopyUtil.CopySelectedColumns(newObj, dbObj, columnsToCopy);
                //dbObj.ExamId =  newObj.ExamId ;
                //dbObj.IsOpenTheorySuppleApplyForStudent =  newObj.IsOpenTheorySuppleApplyForStudent ;
                //dbObj.IsOpenNonTheorySuppleApplyForStudent =  newObj.IsOpenNonTheorySuppleApplyForStudent ;
                //dbObj.SuppleApplicationLastDate =  newObj.SuppleApplicationLastDate ;
                //dbObj.IsOpenTheorySuppleApplyForAdmin =  newObj.IsOpenTheorySuppleApplyForAdmin ;
                //dbObj.IsOpenNonTheorySuppleApplyForAdmin =  newObj.IsOpenNonTheorySuppleApplyForAdmin ;
                //dbObj.SuppleApplyAllowedPaymentDueUpto =  newObj.SuppleApplyAllowedPaymentDueUpto ;
                //dbObj.MaxTimeCanApplyForOneTheory =  newObj.MaxTimeCanApplyForOneTheory ;
                //dbObj.MaxTimeCanApplyForOneNonTheory =  newObj.MaxTimeCanApplyForOneNonTheory ;
                //dbObj.ReferredSuppleExamFee =  newObj.ReferredSuppleExamFee ;
                //dbObj.BackLogSuppleExamFee =  newObj.BackLogSuppleExamFee ;
                //dbObj.IsPublishedSuppleAdmitCard =  newObj.IsPublishedSuppleAdmitCard ;
                //dbObj.SuppleAdmitCardDownloadPaymentDueUpto =  newObj.SuppleAdmitCardDownloadPaymentDueUpto ;
                //dbObj.IsOpenTheorySuppleMarkInput =  newObj.IsOpenTheorySuppleMarkInput ;
                //dbObj.IsOpenNonTheorySuppleMarkInput =  newObj.IsOpenNonTheorySuppleMarkInput ;
                //dbObj.AutoGradeGraceMarkForPass =  newObj.AutoGradeGraceMarkForPass ;
                //dbObj.IsPublishTheorySuppleResult =  newObj.IsPublishTheorySuppleResult ;
                //dbObj.IsPublishNonTheorySuppleResult =  newObj.IsPublishNonTheorySuppleResult ;
                //dbObj.SuppleResultAllowedPaymentDueUpto =  newObj.SuppleResultAllowedPaymentDueUpto ;
                //dbObj.StudentsInstructionsForApply =  newObj.StudentsInstructionsForApply ;
                //dbObj.FacultyInstructionsForMarkEntry =  newObj.FacultyInstructionsForMarkEntry ;
                //dbObj.StudentApplyConfirmationMessage =  newObj.StudentApplyConfirmationMessage ;

            return true;
        }
        /// <summary>
        /// an example of custom save method.
        /// </summary>
        /// <param name="newObj"></param>
        /// <param name="error"></param>
        private void SaveCustomExample(Aca_ExamSupple newObj, out string error)
        {
             error = "";
            //Save(newObj, out error);//using default save method, copy all member except child obj
            //Save(newObj, out error, null, Aca_ExamSupple.Property_FullName);//using default save method, copy only params except child obj
            //Save(newObj, out error, SaveExtraExample, Aca_ExamSupple.Property_ABC);//customizing default save method using delegate.
            Save(newObj, OnSaveExtra, out error );//customizing default save method using delegate.
          
        }
        #endregion

        #region Coustom Others

        #endregion
    }
}


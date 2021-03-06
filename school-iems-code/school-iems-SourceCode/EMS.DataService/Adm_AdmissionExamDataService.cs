//File: DataService for Adm_AdmissionExam
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
    public class Adm_AdmissionExamDataService : Adm_AdmissionExamDataServiceBase
    {
        public Adm_AdmissionExamDataService(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
        }

        #region base overrides
        /// <summary>
        /// override base IsValidAdmissionExamObj function. use it if you need custom field validation.
        /// </summary>
        /// <param name="newObj"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public override bool IsValidToSave(Adm_AdmissionExam newObj, out string error)
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
        /// Must implement permission for save AdmissionExam.
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        protected override bool HasSavePermission(out string error)
        {
            //TODO Must check permission
            //if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionModule.AdmissionExam.CanUpdate, out error))
            //{
            //    return false;
            //}
            error = "";
            return true;
        }
        #endregion

        #region Custom Get AdmissionExam

        #endregion

        #region Custom Save AdmissionExam
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objToSave"></param>
        /// <param name="isNewObject"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        private bool CheckValidExtra(Adm_AdmissionExam objToSave, bool isNewObject, out string error)
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
        private bool OnSaveExtra(Adm_AdmissionExam newObj, Adm_AdmissionExam dbObj, bool isNewObject, DbContextTransaction transactionScope, out string error, params string[] columnsToCopy)
        {
            error = "";
            if (CheckValidExtra(newObj, isNewObject, out error))
            {
                return false;
            }
            //ToDO write your custom business here
            //CopyUtil.CopySelectedColumns(newObj, dbObj, columnsToCopy);
                //dbObj.Name =  newObj.Name ;
                //dbObj.ShortName =  newObj.ShortName ;
                //dbObj.SessionName =  newObj.SessionName ;
                //dbObj.ProgramTypeEnumId =  newObj.ProgramTypeEnumId ;
                //dbObj.CircularStatusEnumId =  newObj.CircularStatusEnumId ;
                //dbObj.SemesterId =  newObj.SemesterId ;
                //dbObj.OnlineFormFillupStartDate =  newObj.OnlineFormFillupStartDate ;
                //dbObj.OnlineFormFillupEndDate =  newObj.OnlineFormFillupEndDate ;
                //dbObj.OnlineAdmitCardPublishDate =  newObj.OnlineAdmitCardPublishDate ;
                //dbObj.OnlineAdmitCardLockDate =  newObj.OnlineAdmitCardLockDate ;
                //dbObj.ExamDate =  newObj.ExamDate ;
                //dbObj.ExamResultPublishDate =  newObj.ExamResultPublishDate ;
                //dbObj.AdmissionStartDate =  newObj.AdmissionStartDate ;
                //dbObj.AmissionFormsDownloadStartDate =  newObj.AmissionFormsDownloadStartDate ;
                //dbObj.AmissionFormsDownloadEndDate =  newObj.AmissionFormsDownloadEndDate ;
                //dbObj.CircularNoticeHtml =  newObj.CircularNoticeHtml ;
                //dbObj.Remark =  newObj.Remark ;

            return true;
        }
        /// <summary>
        /// an example of custom save method.
        /// </summary>
        /// <param name="newObj"></param>
        /// <param name="error"></param>
        private void SaveCustomExample(Adm_AdmissionExam newObj, out string error)
        {
             error = "";
            //Save(newObj, out error);//using default save method, copy all member except child obj
            //Save(newObj, out error, null, Adm_AdmissionExam.Property_FullName);//using default save method, copy only params except child obj
            //Save(newObj, out error, SaveExtraExample, Adm_AdmissionExam.Property_ABC);//customizing default save method using delegate.
            Save(newObj, OnSaveExtra, out error );//customizing default save method using delegate.
          
        }
        #endregion

        #region Coustom Others

        #endregion
    }
}


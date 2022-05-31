//File: DataService for Lib_Book
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
    public class Lib_BookDataService : Lib_BookDataServiceBase
    {
        public Lib_BookDataService(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
        }

        #region base overrides
        /// <summary>
        /// override base IsValidBookObj function. use it if you need custom field validation.
        /// </summary>
        /// <param name="newObj"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public override bool IsValidToSave(Lib_Book newObj, out string error)
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
        /// Must implement permission for save Book.
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        protected override bool HasSavePermission(out string error)
        {
            //TODO Must check permission
            //if (!PermissionUtil.HasPermission(PermissionCollection.LibraryModule.Book.CanUpdate, out error))
            //{
            //    return false;
            //}
            error = "";
            return true;
        }
        #endregion

        #region Custom Get Book

        #endregion

        #region Custom Save Book
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objToSave"></param>
        /// <param name="isNewObject"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        private bool CheckValidExtra(Lib_Book objToSave, bool isNewObject, out string error)
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
        private bool OnSaveExtra(Lib_Book newObj, Lib_Book dbObj, bool isNewObject, DbContextTransaction transactionScope, out string error, params string[] columnsToCopy)
        {
            error = "";
            if (CheckValidExtra(newObj, isNewObject, out error))
            {
                return false;
            }
            //ToDO write your custom business here
            //CopyUtil.CopySelectedColumns(newObj, dbObj, columnsToCopy);
                dbObj.RecordId =  newObj.RecordId ;
                dbObj.Title =  newObj.Title ;
                dbObj.SubTitle =  newObj.SubTitle ;
                dbObj.SubSubTitle =  newObj.SubSubTitle ;
                dbObj.Publisher =  newObj.Publisher ;
                dbObj.PublicationPlace =  newObj.PublicationPlace ;
                dbObj.PublicationYear =  newObj.PublicationYear ;
                dbObj.Edition =  newObj.Edition ;
                dbObj.ISBN =  newObj.ISBN ;
                dbObj.Description =  newObj.Description ;
                dbObj.Bibliography =  newObj.Bibliography ;
                dbObj.LcClassification =  newObj.LcClassification ;
                dbObj.DeweyClass =  newObj.DeweyClass ;
                dbObj.Series =  newObj.Series ;
                dbObj.ISSN =  newObj.ISSN ;
                dbObj.Volume =  newObj.Volume ;
                dbObj.Note =  newObj.Note ;
                dbObj.BookStatusEnumId =  newObj.BookStatusEnumId ;
                dbObj.Responsibility =  newObj.Responsibility ;
                dbObj.CallNo =  newObj.CallNo ;
                dbObj.BindingTypeEnumId =  newObj.BindingTypeEnumId ;
                dbObj.FileLocation =  newObj.FileLocation ;
                dbObj.SourceEnumId =  newObj.SourceEnumId ;
                dbObj.Pagination =  newObj.Pagination ;
                dbObj.Size =  newObj.Size ;
                dbObj.OtherPhysicalDetails =  newObj.OtherPhysicalDetails ;
                dbObj.LanguageId =  newObj.LanguageId ;
                dbObj.CategoryEnumId =  newObj.CategoryEnumId ;
                dbObj.Remarks =  newObj.Remarks ;

            return true;
        }
        /// <summary>
        /// an example of custom save method.
        /// </summary>
        /// <param name="newObj"></param>
        /// <param name="error"></param>
        private void SaveCustomExample(Lib_Book newObj, out string error)
        {
             error = "";
            //Save(newObj, out error);//using default save method, copy all member except child obj
            //Save(newObj, out error, null, Lib_Book.Property_FullName);//using default save method, copy only params except child obj
            //Save(newObj, out error, SaveExtraExample, Lib_Book.Property_ABC);//customizing default save method using delegate.
            Save(newObj, OnSaveExtra, out error );//customizing default save method using delegate.
          
        }
        #endregion

        #region Coustom Others

        #endregion
    }
}


//File: DataService for Lib_BookCopy
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
    public class Lib_BookCopyDataService : Lib_BookCopyDataServiceBase
    {
        public Lib_BookCopyDataService(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
        }

        #region base overrides
        /// <summary>
        /// override base IsValidBookCopyObj function. use it if you need custom field validation.
        /// </summary>
        /// <param name="newObj"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public override bool IsValidToSave(Lib_BookCopy newObj, out string error)
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
        /// Must implement permission for save BookCopy.
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        protected override bool HasSavePermission(out string error)
        {
            //TODO Must check permission
            //if (!PermissionUtil.HasPermission(PermissionCollection.LibraryModule.BookCopy.CanUpdate, out error))
            //{
            //    return false;
            //}
            error = "";
            return true;
        }
        #endregion

        #region Custom Get BookCopy
        #region Get By Barcode
        public Lib_BookCopy GetByBarcode(string barcode)
        {
            return DbInstance.Lib_BookCopy.FirstOrDefault(x => x.Barcode == barcode);
        }
        public Lib_BookCopy GetByBarcode(string barcode, params string[] includeMembers)
        {
            IQueryable<Lib_BookCopy> query = (from q in DbInstance.Lib_BookCopy
                                              select q);
            foreach (var member in includeMembers)
            {
                if (member.Trim().IsValid())
                {
                    query = query.Include(member.Trim());
                }
            }
            return query.FirstOrDefault(x => x.Barcode == barcode);
        }
        #endregion
        #endregion

        #region Custom Save BookCopy
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objToSave"></param>
        /// <param name="isNewObject"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        private bool CheckValidExtra(Lib_BookCopy objToSave, bool isNewObject, out string error)
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
        private bool OnSaveExtra(Lib_BookCopy newObj, Lib_BookCopy dbObj, bool isNewObject, DbContextTransaction transactionScope, out string error, params string[] columnsToCopy)
        {
            error = "";
            if (CheckValidExtra(newObj, isNewObject, out error))
            {
                return false;
            }
            //ToDO write your custom business here
            //CopyUtil.CopySelectedColumns(newObj, dbObj, columnsToCopy);
                dbObj.AccessionNo =  newObj.AccessionNo ;
                dbObj.Barcode =  newObj.Barcode ;
                dbObj.BookId =  newObj.BookId ;
                dbObj.DateAcquired =  newObj.DateAcquired ;
                dbObj.Shelf =  newObj.Shelf ;
                dbObj.CopySourceEnumId =  newObj.CopySourceEnumId ;
                dbObj.Price =  newObj.Price ;
                dbObj.SupplierId =  newObj.SupplierId ;
                dbObj.BorrowingAllowedEnumId =  newObj.BorrowingAllowedEnumId ;
                dbObj.AvailabilityEnumId =  newObj.AvailabilityEnumId ;
                dbObj.ReservationEnumId =  newObj.ReservationEnumId ;
                dbObj.CopyStatusEnumId =  newObj.CopyStatusEnumId ;
                dbObj.DeletedEnumId =  newObj.DeletedEnumId ;
                dbObj.CopyTypeEnumId =  newObj.CopyTypeEnumId ;
                dbObj.Remarks =  newObj.Remarks ;
                dbObj.CallNo =  newObj.CallNo ;
                dbObj.PermanentLibraryId =  newObj.PermanentLibraryId ;
                dbObj.PresentLibraryId =  newObj.PresentLibraryId ;

            return true;
        }
        /// <summary>
        /// an example of custom save method.
        /// </summary>
        /// <param name="newObj"></param>
        /// <param name="error"></param>
        private void SaveCustomExample(Lib_BookCopy newObj, out string error)
        {
             error = "";
            //Save(newObj, out error);//using default save method, copy all member except child obj
            //Save(newObj, out error, null, Lib_BookCopy.Property_FullName);//using default save method, copy only params except child obj
            //Save(newObj, out error, SaveExtraExample, Lib_BookCopy.Property_ABC);//customizing default save method using delegate.
            Save(newObj, OnSaveExtra, out error );//customizing default save method using delegate.
          
        }
        #endregion

        #region Coustom Others

        #endregion
    }
}


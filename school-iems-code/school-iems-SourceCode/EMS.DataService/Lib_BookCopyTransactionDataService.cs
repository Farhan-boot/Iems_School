//File: DataService for Lib_BookCopyTransaction
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
    public class Lib_BookCopyTransactionDataService : Lib_BookCopyTransactionDataServiceBase
    {
        public Lib_BookCopyTransactionDataService(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
        }

        #region base overrides
        /// <summary>
        /// override base IsValidBookCopyTransactionObj function. use it if you need custom field validation.
        /// </summary>
        /// <param name="newObj"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public override bool IsValidToSave(Lib_BookCopyTransaction newObj, out string error)
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
        /// Must implement permission for save BookCopyTransaction.
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        protected override bool HasSavePermission(out string error)
        {
            //TODO Must check permission
            //if (!PermissionUtil.HasPermission(PermissionCollection.LibraryModule.BookCopyTransaction.CanUpdate, out error))
            //{
            //    return false;
            //}
            error = "";
            return true;
        }
        #endregion

        #region Custom Get BookCopyTransaction

        #region Get By Barcode
        public List<Lib_BookCopyTransaction> GetByBarcode(string barcode)
        {
            return DbInstance.Lib_BookCopyTransaction
                .Include(x=>x.Lib_BookCopy)
                .Where(x => x.Lib_BookCopy.Barcode == barcode)
                .ToList();
        }
        public List<Lib_BookCopyTransaction> GetByBarcode(string barcode, params string[] includeMembers)
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
            return DbInstance.Lib_BookCopyTransaction
                .Include(x => x.Lib_BookCopy)
                .Where(x => x.Lib_BookCopy.Barcode == barcode)
                .ToList();
        }
        #endregion
        #endregion

        #region Custom Save BookCopyTransaction
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objToSave"></param>
        /// <param name="isNewObject"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        private bool CheckValidExtra(Lib_BookCopyTransaction objToSave, bool isNewObject, out string error)
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
        private bool OnSaveExtra(Lib_BookCopyTransaction newObj, Lib_BookCopyTransaction dbObj, bool isNewObject, DbContextTransaction transactionScope, out string error, params string[] columnsToCopy)
        {
            error = "";
            if (CheckValidExtra(newObj, isNewObject, out error))
            {
                return false;
            }
            //ToDO write your custom business here
            //CopyUtil.CopySelectedColumns(newObj, dbObj, columnsToCopy);
                dbObj.BorrowerId =  newObj.BorrowerId ;
                dbObj.BookCopyId =  newObj.BookCopyId ;
                dbObj.BorrowedDate =  newObj.BorrowedDate ;
                dbObj.IssuedById =  newObj.IssuedById ;
                dbObj.ExpectedReturnDate =  newObj.ExpectedReturnDate ;
                dbObj.ReturnedDate =  newObj.ReturnedDate ;
                dbObj.CollectedById =  newObj.CollectedById ;
                dbObj.Fine =  newObj.Fine ;

            return true;
        }
        /// <summary>
        /// an example of custom save method.
        /// </summary>
        /// <param name="newObj"></param>
        /// <param name="error"></param>
        private void SaveCustomExample(Lib_BookCopyTransaction newObj, out string error)
        {
             error = "";
            //Save(newObj, out error);//using default save method, copy all member except child obj
            //Save(newObj, out error, null, Lib_BookCopyTransaction.Property_FullName);//using default save method, copy only params except child obj
            //Save(newObj, out error, SaveExtraExample, Lib_BookCopyTransaction.Property_ABC);//customizing default save method using delegate.
            Save(newObj, OnSaveExtra, out error );//customizing default save method using delegate.
          
        }
        #endregion

        #region Coustom Others

        #endregion
    }
}


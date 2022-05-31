//File: DataService for Aca_MarkDistributionPolicy
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
    public class Aca_MarkDistributionPolicyDataService : Aca_MarkDistributionPolicyDataServiceBase
    {
        public Aca_MarkDistributionPolicyDataService(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
        }

        #region base overrides
        /// <summary>
        /// override base IsValidMarkDistributionPolicyObj function. use it if you need custom field validation.
        /// </summary>
        /// <param name="newObj"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public override bool IsValidToSave(Aca_MarkDistributionPolicy newObj, out string error)
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
        /// Must implement permission for save MarkDistributionPolicy.
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        protected override bool HasSavePermission(out string error)
        {
            //TODO Must check permission
            //if (!PermissionUtil.HasPermission(PermissionCollection.AcademicModule.MarkDistributionPolicy.CanUpdate, out error))
            //{
            //    return false;
            //}
            error = "";
            return true;
        }
        #endregion

        #region Custom Get MarkDistributionPolicy
        /// <summary>
        /// TODO need to implement.
        /// </summary>
        /// <param name="searchKey"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <param name="count"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public List<Aca_MarkDistributionPolicy> GetPagedList(string searchKey, int? pageSize, int? pageNo, ref int count, out string error)
        {
            throw new NotImplementedException();
            IQueryable<Aca_MarkDistributionPolicy> query = (from q in DbInstance.Aca_MarkDistributionPolicy select q)
                                            //.Include("Aca_MarkDistributionPolicy")
                                            //.OrderBy(x => x.FullName)
                                            ;
            //if (searchKey.IsValid() )
            //{
            //    query = from q in query
            //            where q.FullName.Contains(searchKey)
            //            select q;
            //}
            error = "";
            return base.GetPagedList(query, pageSize, pageNo, ref count, out error);
        }
        #endregion

        #region Custom Save MarkDistributionPolicy
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objToSave"></param>
        /// <param name="isNewObject"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        private bool CheckValidExtra(Aca_MarkDistributionPolicy objToSave, bool isNewObject, out string error)
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
        private bool SaveExtraExample(Aca_MarkDistributionPolicy newObj, Aca_MarkDistributionPolicy objToSave, bool isNewObject, DbContextTransaction transactionScope, out string error, params string[] columnsToCopy)
        {
            error = "";
            if (CheckValidExtra(newObj, isNewObject, out error))
            {
                return false;
            }

            //ToDO write your custom business here
            CopyUtil.CopySelectedColumns(newObj, objToSave, columnsToCopy);
            //objToSave.Column = newObj.Column;
            return true;
        }
        /// <summary>
        /// an example of custom save method.
        /// </summary>
        /// <param name="newObj"></param>
        /// <param name="error"></param>
        private void SaveCustomExample(Aca_MarkDistributionPolicy newObj, out string error)
        {
             error = "";
            //Save(newObj, out error);//using default save method, copy all member except child obj
            //Save(newObj, out error, null, Aca_MarkDistributionPolicy.Property_FullName);//using default save method, copy only params except child obj
            //Save(newObj, out error, SaveExtraExample, Aca_MarkDistributionPolicy.Property_ABC);//customizing default save method using delegate.
            Save(newObj, out error,null,true, SaveExtraExample);//customizing default save method using delegate.
          
        }

        #endregion

        #region Coustom Others
        ///// <summary>
        ///// testing update code
        ///// </summary>
        ///// <param name="objToUpdate"></param>
        ///// <param name="error"></param>
        ///// <param name="scope"></param>
        ///// <param name="checkIsValidToDelete"></param>
        ///// <returns></returns>
        //private bool UpdateStudent(Aca_MarkDistributionPolicy objToUpdate, out string error, DbContextTransaction scope = null, bool checkIsValidToDelete = true)
        //{
        //    bool closeTransaction = false;
        //    if (scope == null)
        //    {
        //        closeTransaction = true;
        //        scope = DbInstance.Database.BeginTransaction();
        //    }
        //    try
        //    {
        //        error = string.Empty;
        //        DbInstance.Entry(objToUpdate).State = EntityState.Modified;
        //        DbInstance.SaveChanges();
        //        if (closeTransaction)
        //        {   //save changes to DB 
        //            scope.Commit();
        //        }
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        error = ex.ToString();
        //        if (closeTransaction)
        //            scope.Rollback();
        //        throw;
        //    }
        //    finally
        //    {   //dispose Transaction scope
        //        if (closeTransaction)
        //        {
        //            scope.Dispose();
        //        }
        //    }
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="newObj"></param>
        ///// <param name="error"></param>
        ///// <param name="saveCallBackDelegate"></param>
        ///// <param name="columnsToCopy">keep it empty to include all columns</param>
        ///// <returns></returns>
        //public bool Save(Aca_MarkDistributionPolicy newObj, out string error, SaveCallBackDelegate saveCallBackDelegate = null, params string[] columnsToCopy)
        //{
        //    //check permission
        //    error = string.Empty;
        //    if (!HasSavePermission(out error))
        //    {
        //        return false;
        //    }
        //    if (IsValidToSave(newObj, out error))
        //    {
        //        using (var scope = DbInstance.Database.BeginTransaction())
        //        {
        //            try
        //            {
        //                bool isNewObject = true;
        //                Aca_MarkDistributionPolicy objToSave = null;

        //                if (newObj.Id != 0)
        //                {
        //                    objToSave = DbInstance.Aca_MarkDistributionPolicy.SingleOrDefault(x => x.Id == newObj.Id);
        //                    isNewObject = false;
        //                }
        //                else
        //                {
        //                    newObj.Id = BigInt.NewBigInt();
        //                }
        //                if (objToSave == null)
        //                {
        //                    isNewObject = true;
        //                    objToSave = Aca_MarkDistributionPolicy.GetNew(newObj.Id);
        //                    DbInstance.Aca_MarkDistributionPolicy.Add(objToSave);
        //                    //must set it in coming object
        //                    objToSave.CreateById = newObj.CreateById = Profile.UserId;
        //                    objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
        //                }
        //                objToSave.LastChangedById = newObj.LastChangedById = Profile.UserId;
        //                objToSave.LastChanged = newObj.LastChanged = DateTime.Now;
        //                if (saveCallBackDelegate == null)
        //                {
        //                    error = "";
        //                    CopyUtil.CopySelectedColumns(newObj, objToSave, columnsToCopy);
        //                }
        //                else
        //                {
        //                    if (!saveCallBackDelegate(newObj, objToSave, isNewObject, scope, out error, columnsToCopy))
        //                    {
        //                        scope.Rollback();
        //                        return false;
        //                    }
        //                }
        //                DbInstance.SaveChanges();
        //                scope.Commit();
        //                if (isNewObject)
        //                {
        //                    newObj.Id = objToSave.Id;
        //                }
        //                return true;
        //            }
        //            catch (Exception ex)
        //            {
        //                error = ex.ToString();
        //                scope.Rollback();
        //                return false;
        //            }
        //        }
        //    }
        //    return false;
        //}



        //public bool Save(Aca_MarkDistributionPolicy newObj, out string error, bool checkIsValidToSave = true, params string[] columnsToCopy)
        //{
        //    using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
        //    {
        //        try
        //        {
        //            error = string.Empty;
        //            var success = Save(newObj, scope, out error, checkIsValidToSave, null, columnsToCopy);
        //            if (success)
        //            {
        //                scope.Commit();
        //            }
        //            else
        //            {
        //                scope.Rollback();
        //            }
        //            return success;
        //        }
        //        catch (Exception)
        //        {
        //            scope.Rollback();
        //            throw;
        //        }
        //    }
        //}
        //public bool Save(Aca_MarkDistributionPolicy newObj, DbContextTransaction scope, out string error, bool checkIsValidToSave = true, params string[] columnsToCopy)
        //{
        //    error = string.Empty;
        //    return Save(newObj, scope, out error, checkIsValidToSave, null, columnsToCopy);
        //}
        #endregion
    }
}


﻿//File: DataServiceBase for Lib_BookDeptMap
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

using EMS.CoreLibrary.Helpers;
using EMS.Framework.Objects;
using EMS.Framework.Utils;
using EMS.DataAccess.Data;

namespace EMS.DataService.Bases
{
    public abstract class Lib_BookDeptMapDataServiceBase : BaseDataService
    {
        public Lib_BookDeptMapDataServiceBase(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
        }
        #region Get By Foreign Key
        public List<Lib_BookDeptMap> GetAllByBookId(Int64 bookId)
        {
            return DbInstance.Lib_BookDeptMap.Where(c=>c.BookId==bookId).ToList();
        }

        public List<Lib_BookDeptMap> GetAllByDepartmentId(Int32 departmentId)
        {
            return DbInstance.Lib_BookDeptMap.Where(c=>c.DepartmentId==departmentId).ToList();
        }

        #endregion
        #region Get By Unique Key
        #endregion
        
        #region Get By Id
        public Lib_BookDeptMap GetById(Int64 id)
        {
            return DbInstance.Lib_BookDeptMap.Find(id);
        }
        public Lib_BookDeptMap GetById(Int64 id, params string[] includeMembers)
        {
            IQueryable<Lib_BookDeptMap> query = (from q in DbInstance.Lib_BookDeptMap
                                              select q);
            foreach (var member in includeMembers)
            {
                if (member.Trim().IsValid())
                {
                    query= query.Include(member.Trim());
                }
            }
            return query.FirstOrDefault(x => x.Id == id);
        }
        #endregion
        
        #region Get List
        /// <summary>
        ///  to get all Lib_BookDeptMap set query parameter null.
        /// </summary>
        /// <param name="query">ex: IQueryable<Lib_BookDeptMap> query = DbInstance.Lib_BookDeptMap.Where(x => x.Test=="ABC");</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <param name="count"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public List<Lib_BookDeptMap> GetPagedList(IQueryable<Lib_BookDeptMap> query, int? pageSize, int? pageNo, ref int count, out string error)
        {
            error = "";
            try
            {
                if (query == null)
                {
                    query = DbInstance.Lib_BookDeptMap;
                }
                count = query.Count();
                if (pageNo.HasValue && pageSize.HasValue && pageNo.Value >= 0 && pageSize.Value >= 0)
                {
                    if (pageNo <= 1)
                    {
                        pageNo = 0;
                    }
                    else
                    {
                        pageNo = pageNo - 1;
                    }

                    query = (from q in query
                             select q)
                            .Skip(pageNo.Value * pageSize.Value)
                        .Take(pageSize.Value);
                }
                return query.ToList();
            }
            catch (Exception ex)
            {
                error = ex.ToString();
            }
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query">ex: IQueryable<Lib_BookDeptMap> query = DbInstance.Lib_BookDeptMap.Where(x => x.Test==test);</param>
        /// <param name="error"></param>
        /// <returns></returns>
        public List<Lib_BookDeptMap> GetListByQuery(IQueryable<Lib_BookDeptMap> query, out string error)
        {
            try
            {
                error = "";
                if (query == null)
                {
                    error = "Parameter 'query' can't be null!";
                    return null;
                }
                return query.ToList();
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return null;
            }
        }
        public List<Lib_BookDeptMap> GetAll(out string error)
        {
            error = "";
            try
            {
                return DbInstance.Lib_BookDeptMap.ToList();
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return null;
            }
        }
        #endregion

        #region Save BookDeptMap
        
        public delegate bool SaveCallBackDelegate(Lib_BookDeptMap newObj, Lib_BookDeptMap unchangedDbObj, bool isNewObject, DbContextTransaction transactionScope, out string error, params string[] columnsToCopy);
        protected abstract bool HasSavePermission(out string error);
        
        public virtual bool IsValidToSave(Lib_BookDeptMap newObj, out string error)
        {
            error = "";
            //DbInstance.Configuration.ValidateOnSaveEnabled = false;
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newObj"></param>
        /// <param name="error"></param>
        /// <param name="saveCallBackDelegate"></param>
        /// <param name="columnsToCopy"></param>
        /// <returns></returns>
        public bool Save(Lib_BookDeptMap newObj, SaveCallBackDelegate saveCallBackDelegate, out string error,  params string[] columnsToCopy)
        {
            error = string.Empty;
            var success = Save(newObj, out error,null, true, saveCallBackDelegate, columnsToCopy);
            return success;
        }
                /// <summary>
        /// 
        /// </summary>
        /// <param name="newObj">object to save</param>
        /// <param name="error">out error message</param>
        /// <param name="scope">Transaction scope, default it creates scope by itself</param>
        /// <param name="checkIsValidToSave"></param>
        /// <param name="saveCallBackDelegate"></param>
        /// <param name="columnsToCopy">keep it empty to include all columns</param>
        /// <returns></returns>
        public bool Save(Lib_BookDeptMap newObj, out string error, DbContextTransaction scope = null, bool checkIsValidToSave = true, SaveCallBackDelegate saveCallBackDelegate = null, params string[] columnsToCopy)
        {
            error = string.Empty;
            Lib_BookDeptMap dbAddedObj =null;
            var success = Save(newObj,ref dbAddedObj, out error, scope, true, saveCallBackDelegate, columnsToCopy);
            return success;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="newObj">object to save</param>
        /// <param name="dbAddedObj">return object attached in dbcontext</param>
        /// <param name="error">out error message</param>
        /// <param name="scope">Transaction scope, default it creates scope by itself. But if you send scope it will not call Context.SaveChanges() & Scope.Commit() </param>
        /// <param name="checkIsValidToSave"></param>
        /// <param name="saveCallBackDelegate"></param>
        /// <param name="columnsToCopy">keep it empty to include all columns</param>
        /// <returns></returns>
        public bool Save(Lib_BookDeptMap newObj,ref Lib_BookDeptMap dbAddedObj, out string error,DbContextTransaction scope=null, bool checkIsValidToSave = true, SaveCallBackDelegate saveCallBackDelegate = null, params string[] columnsToCopy)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Book Dept Map to save can't be null!";
                return false;
            }
            //checking permission
            if (!HasSavePermission(out error))
            {
                return false;
            }
            //checking entity validity
            if (checkIsValidToSave)
            {
                if ( !IsValidToSave(newObj, out error))
                    return false;
            }
            //checking  Transaction
            bool closeTransaction = false;
            if (scope == null)
            {
                closeTransaction = true;
                scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted);
            }
            try
            {
                bool isNewObject = true;
                Lib_BookDeptMap objToSave = null;

                if (newObj.Id != 0)
                {
                    objToSave = DbInstance.Lib_BookDeptMap.SingleOrDefault(x => x.Id == newObj.Id);
                    isNewObject = false;
                }
                else
                {    
                    newObj.Id = BigInt.NewBigInt();
                }
                if (objToSave == null)
                {
                    isNewObject = true;
                    objToSave = Lib_BookDeptMap.GetNew(newObj.Id);
                    DbInstance.Lib_BookDeptMap.Add(objToSave);
                    //must set it in coming object
                   
                   
                }
                   dbAddedObj = objToSave;
                   
                   
                if (saveCallBackDelegate==null)
                {
                    error = "";
                    CopyUtil.CopySelectedColumns(newObj, objToSave, columnsToCopy);
                }
                else
                {
                    if (!saveCallBackDelegate(newObj, objToSave, isNewObject, scope, out error, columnsToCopy))
                    {
                       if (closeTransaction)
                          scope.Rollback();
                        return false;
                    }
                }
                if (closeTransaction)
                {   //save changes to DB
                    DbInstance.SaveChanges();
                    scope.Commit();
                }
                if (isNewObject)
                {
                    newObj.Id = objToSave.Id;
                }
                return true;
            }
             catch (Exception ex)
            {
                error = ex.ToString();
                if (closeTransaction)
                    scope.Rollback();
                throw;
            }
            finally
            {   //dispose Transaction scope
                if (closeTransaction)
                {
                    scope.Dispose();
                }
            }
        }
        #endregion

        #region Delete BookDeptMap
        /// <summary>
        /// not implemented yet.
        /// </summary>
        /// <param name="objToDelete"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public virtual bool IsValidToDelete(Lib_BookDeptMap objToDelete, out string error)
        {
            error = "";
            return true;
        }
        public bool DeleteById(Int64 id, out string error, DbContextTransaction scope = null, bool checkIsValidToDelete = true)
        {
            error = string.Empty;
            bool closeTransaction = false;
            if (scope == null)
            {
                closeTransaction = true;
                scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted);
            }
            try
            {
                Lib_BookDeptMap objToDelete = DbInstance.Lib_BookDeptMap.Find(id);
                if (objToDelete == null)
                {
                    return true;
                }
                //checking is valid to delete
                if (checkIsValidToDelete)
                {
                    if (!IsValidToDelete(objToDelete, out error))
                        return false;
                }
                DbInstance.Lib_BookDeptMap.Remove(objToDelete);
                
                if (closeTransaction)
                {   //save changes to DB 
                    DbInstance.SaveChanges();
                    scope.Commit();
                }
                return true;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                if (closeTransaction)
                    scope.Rollback();
                throw;
            }
            finally
            {   //dispose Transaction scope
                if (closeTransaction)
                {
                    scope.Dispose();
                }
            }
        }
        public bool Delete(Lib_BookDeptMap objToDelete, out string error, DbContextTransaction scope = null, bool checkIsValidToDelete = true)
        {
            error = string.Empty;        
            if (objToDelete == null)
            {
                error = "Object to delete can't be null!";
                return false;
            }
            //checking is valid to delete
            if (checkIsValidToDelete)
            {
                if (!IsValidToDelete(objToDelete, out error))
                    return false;
            }
            bool closeTransaction = false;
            if (scope == null)
            {
                closeTransaction = true;
                scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted);
            }
            try
            {
                if (DbInstance.Entry(objToDelete).State == EntityState.Detached)
                {
                    DbInstance.Lib_BookDeptMap.Attach(objToDelete);
                }
                DbInstance.Lib_BookDeptMap.Remove(objToDelete);
                
                if (closeTransaction)
                {   //save changes to DB 
                    DbInstance.SaveChanges();
                    scope.Commit();
                }
                return true;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                if (closeTransaction)
                    scope.Rollback();
                throw;
            }
            finally
            {   //dispose Transaction scope
                if (closeTransaction)
                {
                    scope.Dispose();
                }
            }
        }
        #endregion
    }
}


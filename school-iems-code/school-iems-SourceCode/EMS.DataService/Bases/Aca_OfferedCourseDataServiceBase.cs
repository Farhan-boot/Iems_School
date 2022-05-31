﻿//File: DataServiceBase for Aca_OfferedCourse
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
    public abstract class Aca_OfferedCourseDataServiceBase : BaseDataService
    {
        public Aca_OfferedCourseDataServiceBase(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
        }
        #region Get By Foreign Key
        public List<Aca_OfferedCourse> GetAllByStudyLevelTermId(Int32 studyLevelTermId)
        {
            return DbInstance.Aca_OfferedCourse.Where(c=>c.StudyLevelTermId==studyLevelTermId).ToList();
        }

        public List<Aca_OfferedCourse> GetAllBySemesterId(Int64 semesterId)
        {
            return DbInstance.Aca_OfferedCourse.Where(c=>c.SemesterId==semesterId).ToList();
        }

        public List<Aca_OfferedCourse> GetAllByRegularExamMarkDistributionPolicyId(Int64 regularExamMarkDistributionPolicyId)
        {
            return DbInstance.Aca_OfferedCourse.Where(c=>c.RegularExamMarkDistributionPolicyId==regularExamMarkDistributionPolicyId).ToList();
        }

        public List<Aca_OfferedCourse> GetAllByReferredExamMarkDistributionPolicyId(Int64 referredExamMarkDistributionPolicyId)
        {
            return DbInstance.Aca_OfferedCourse.Where(c=>c.ReferredExamMarkDistributionPolicyId==referredExamMarkDistributionPolicyId).ToList();
        }

        public List<Aca_OfferedCourse> GetAllByBacklogExamMarkDistributionPolicyId(Int64 backlogExamMarkDistributionPolicyId)
        {
            return DbInstance.Aca_OfferedCourse.Where(c=>c.BacklogExamMarkDistributionPolicyId==backlogExamMarkDistributionPolicyId).ToList();
        }

        #endregion
        #region Get By Unique Key
        #endregion
        
        #region Get By Id
        public Aca_OfferedCourse GetById(Int64 id)
        {
            return DbInstance.Aca_OfferedCourse.Find(id);
        }
        public Aca_OfferedCourse GetById(Int64 id, params string[] includeMembers)
        {
            IQueryable<Aca_OfferedCourse> query = (from q in DbInstance.Aca_OfferedCourse
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
        ///  to get all Aca_OfferedCourse set query parameter null.
        /// </summary>
        /// <param name="query">ex: IQueryable<Aca_OfferedCourse> query = DbInstance.Aca_OfferedCourse.Where(x => x.Test=="ABC");</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <param name="count"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public List<Aca_OfferedCourse> GetPagedList(IQueryable<Aca_OfferedCourse> query, int? pageSize, int? pageNo, ref int count, out string error)
        {
            error = "";
            try
            {
                if (query == null)
                {
                    query = DbInstance.Aca_OfferedCourse;
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
        /// <param name="query">ex: IQueryable<Aca_OfferedCourse> query = DbInstance.Aca_OfferedCourse.Where(x => x.Test==test);</param>
        /// <param name="error"></param>
        /// <returns></returns>
        public List<Aca_OfferedCourse> GetListByQuery(IQueryable<Aca_OfferedCourse> query, out string error)
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
        public List<Aca_OfferedCourse> GetAll(out string error)
        {
            error = "";
            try
            {
                return DbInstance.Aca_OfferedCourse.ToList();
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return null;
            }
        }
        #endregion

        #region Save OfferedCourse
        
        public delegate bool SaveCallBackDelegate(Aca_OfferedCourse newObj, Aca_OfferedCourse unchangedDbObj, bool isNewObject, DbContextTransaction transactionScope, out string error, params string[] columnsToCopy);
        protected abstract bool HasSavePermission(out string error);
        
        public virtual bool IsValidToSave(Aca_OfferedCourse newObj, out string error)
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
        public bool Save(Aca_OfferedCourse newObj, SaveCallBackDelegate saveCallBackDelegate, out string error,  params string[] columnsToCopy)
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
        public bool Save(Aca_OfferedCourse newObj, out string error, DbContextTransaction scope = null, bool checkIsValidToSave = true, SaveCallBackDelegate saveCallBackDelegate = null, params string[] columnsToCopy)
        {
            error = string.Empty;
            Aca_OfferedCourse dbAddedObj =null;
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
        public bool Save(Aca_OfferedCourse newObj,ref Aca_OfferedCourse dbAddedObj, out string error,DbContextTransaction scope=null, bool checkIsValidToSave = true, SaveCallBackDelegate saveCallBackDelegate = null, params string[] columnsToCopy)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Offered Course to save can't be null!";
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
                Aca_OfferedCourse objToSave = null;

                if (newObj.Id != 0)
                {
                    objToSave = DbInstance.Aca_OfferedCourse.SingleOrDefault(x => x.Id == newObj.Id);
                    isNewObject = false;
                }
                else
                {    
                    newObj.Id = BigInt.NewBigInt();
                }
                if (objToSave == null)
                {
                    isNewObject = true;
                    objToSave = Aca_OfferedCourse.GetNew(newObj.Id);
                    DbInstance.Aca_OfferedCourse.Add(objToSave);
                    //must set it in coming object
                   objToSave.CreateById = newObj.CreateById = Profile.UserId;
                   objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
                }
                   dbAddedObj = objToSave;
                   objToSave.LastChangedById = newObj.LastChangedById = Profile.UserId;
                    objToSave.LastChanged = newObj.LastChanged = DateTime.Now;
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

        #region Delete OfferedCourse
        /// <summary>
        /// not implemented yet.
        /// </summary>
        /// <param name="objToDelete"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public virtual bool IsValidToDelete(Aca_OfferedCourse objToDelete, out string error)
        {
            error = "";
            if (DbInstance.Aca_OfferedCourseForCurriculum.Any(x => x.OfferedCourseId == objToDelete.Id))
            {
                error = "This data used in Offered Course For Curriculum table, first delete those!";
                return false;
            }
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
                Aca_OfferedCourse objToDelete = DbInstance.Aca_OfferedCourse.Find(id);
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
                DbInstance.Aca_OfferedCourse.Remove(objToDelete);
                
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
        public bool Delete(Aca_OfferedCourse objToDelete, out string error, DbContextTransaction scope = null, bool checkIsValidToDelete = true)
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
                    DbInstance.Aca_OfferedCourse.Attach(objToDelete);
                }
                DbInstance.Aca_OfferedCourse.Remove(objToDelete);
                
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


//File: DataServiceBase for Aca_CurriculumCourse
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
    public abstract class Aca_CurriculumCourseDataServiceBase : BaseDataService
    {
        public Aca_CurriculumCourseDataServiceBase(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
        }
        #region Get By Foreign Key
        public List<Aca_CurriculumCourse> GetAllByCourseId(Int64 courseId)
        {
            return DbInstance.Aca_CurriculumCourse.Where(c=>c.CourseId==courseId).ToList();
        }

        public List<Aca_CurriculumCourse> GetAllByCurriculumId(Int64 curriculumId)
        {
            return DbInstance.Aca_CurriculumCourse.Where(c=>c.CurriculumId==curriculumId).ToList();
        }

        public List<Aca_CurriculumCourse> GetAllByElectiveGroupId(Int64 electiveGroupId)
        {
            return DbInstance.Aca_CurriculumCourse.Where(c=>c.ElectiveGroupId==electiveGroupId).ToList();
        }

        #endregion
        #region Get By Unique Key
        #endregion
        
        #region Get By Id
        public Aca_CurriculumCourse GetById(Int64 id)
        {
            return DbInstance.Aca_CurriculumCourse.Find(id);
        }
        public Aca_CurriculumCourse GetById(Int64 id, params string[] includeMembers)
        {
            IQueryable<Aca_CurriculumCourse> query = (from q in DbInstance.Aca_CurriculumCourse
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
        ///  to get all Aca_CurriculumCourse set query parameter null.
        /// </summary>
        /// <param name="query">ex: IQueryable<Aca_CurriculumCourse> query = DbInstance.Aca_CurriculumCourse.Where(x => x.Test=="ABC");</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <param name="count"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public List<Aca_CurriculumCourse> GetPagedList(IQueryable<Aca_CurriculumCourse> query, int? pageSize, int? pageNo, ref int count, out string error)
        {
            error = "";
            try
            {
                if (query == null)
                {
                    query = DbInstance.Aca_CurriculumCourse;
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
        /// <param name="query">ex: IQueryable<Aca_CurriculumCourse> query = DbInstance.Aca_CurriculumCourse.Where(x => x.Test==test);</param>
        /// <param name="error"></param>
        /// <returns></returns>
        public List<Aca_CurriculumCourse> GetListByQuery(IQueryable<Aca_CurriculumCourse> query, out string error)
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
        public List<Aca_CurriculumCourse> GetAll(out string error)
        {
            error = "";
            try
            {
                return DbInstance.Aca_CurriculumCourse.ToList();
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return null;
            }
        }
        #endregion

        #region Save CurriculumCourse
        
        public delegate bool SaveCallBackDelegate(Aca_CurriculumCourse newObj, Aca_CurriculumCourse unchangedDbObj, bool isNewObject, DbContextTransaction transactionScope, out string error, params string[] columnsToCopy);
        protected abstract bool HasSavePermission(out string error);
        
        public virtual bool IsValidToSave(Aca_CurriculumCourse newObj, out string error)
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
        public bool Save(Aca_CurriculumCourse newObj, SaveCallBackDelegate saveCallBackDelegate, out string error,  params string[] columnsToCopy)
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
        public bool Save(Aca_CurriculumCourse newObj, out string error, DbContextTransaction scope = null, bool checkIsValidToSave = true, SaveCallBackDelegate saveCallBackDelegate = null, params string[] columnsToCopy)
        {
            error = string.Empty;
            Aca_CurriculumCourse dbAddedObj =null;
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
        public bool Save(Aca_CurriculumCourse newObj,ref Aca_CurriculumCourse dbAddedObj, out string error,DbContextTransaction scope=null, bool checkIsValidToSave = true, SaveCallBackDelegate saveCallBackDelegate = null, params string[] columnsToCopy)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Curriculum Course to save can't be null!";
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
                Aca_CurriculumCourse objToSave = null;

                if (newObj.Id != 0)
                {
                    objToSave = DbInstance.Aca_CurriculumCourse.SingleOrDefault(x => x.Id == newObj.Id);
                    isNewObject = false;
                }
                else
                {    
                    newObj.Id = BigInt.NewBigInt();
                }
                if (objToSave == null)
                {
                    isNewObject = true;
                    objToSave = Aca_CurriculumCourse.GetNew(newObj.Id);
                    DbInstance.Aca_CurriculumCourse.Add(objToSave);
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

        #region Delete CurriculumCourse
        /// <summary>
        /// not implemented yet.
        /// </summary>
        /// <param name="objToDelete"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public virtual bool IsValidToDelete(Aca_CurriculumCourse objToDelete, out string error)
        {
            error = "";
            if (DbInstance.Aca_Class.Any(x => x.CurriculumCourseId == objToDelete.Id))
            {
                error = "This data used in Class table, first delete those!";
                return false;
            }
            if (DbInstance.Aca_CoursePrerequisiteMap.Any(x => x.CourseId == objToDelete.Id))
            {
                error = "This data used in Course Prerequisite Map table, first delete those!";
                return false;
            }
            if (DbInstance.Aca_CoursePrerequisiteMap.Any(x => x.PrerequisiteCourseId == objToDelete.Id))
            {
                error = "This data used in Course Prerequisite Map table, first delete those!";
                return false;
            }
            if (DbInstance.Aca_CreditTransferCourses.Any(x => x.CurriculumCourseId == objToDelete.Id))
            {
                error = "This data used in Credit Transfer Courses table, first delete those!";
                return false;
            }
            if (DbInstance.Aca_OnlineClass.Any(x => x.CurriculumCourseId == objToDelete.Id))
            {
                error = "This data used in Online Class table, first delete those!";
                return false;
            }
            if (DbInstance.Aca_OnlineExam.Any(x => x.CurriculumCourseId == objToDelete.Id))
            {
                error = "This data used in Online Exam table, first delete those!";
                return false;
            }
            if (DbInstance.Aca_QuestionBank.Any(x => x.CurriculumCourseId == objToDelete.Id))
            {
                error = "This data used in Question Bank table, first delete those!";
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
                Aca_CurriculumCourse objToDelete = DbInstance.Aca_CurriculumCourse.Find(id);
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
                DbInstance.Aca_CurriculumCourse.Remove(objToDelete);
                
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
        public bool Delete(Aca_CurriculumCourse objToDelete, out string error, DbContextTransaction scope = null, bool checkIsValidToDelete = true)
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
                    DbInstance.Aca_CurriculumCourse.Attach(objToDelete);
                }
                DbInstance.Aca_CurriculumCourse.Remove(objToDelete);
                
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


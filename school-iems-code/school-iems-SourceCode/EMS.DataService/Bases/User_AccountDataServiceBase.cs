//File: DataServiceBase for User_Account
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
    public abstract class User_AccountDataServiceBase : BaseDataService
    {
        public User_AccountDataServiceBase(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
        }
        #region Get By Foreign Key
        public List<User_Account> GetAllByPlaceOfBirthCountryId(Int32 placeOfBirthCountryId)
        {
            return DbInstance.User_Account.Where(c=>c.PlaceOfBirthCountryId==placeOfBirthCountryId).ToList();
        }

        public List<User_Account> GetAllByEmergencyContactRelationshipId(Int32 emergencyContactRelationshipId)
        {
            return DbInstance.User_Account.Where(c=>c.EmergencyContactRelationshipId==emergencyContactRelationshipId).ToList();
        }

        public List<User_Account> GetAllByCampusId(Int32 campusId)
        {
            return DbInstance.User_Account.Where(c=>c.CampusId==campusId).ToList();
        }

        public List<User_Account> GetAllByDepartmentId(Int32 departmentId)
        {
            return DbInstance.User_Account.Where(c=>c.DepartmentId==departmentId).ToList();
        }

        public List<User_Account> GetAllByJoiningSemesterId(Int64 joiningSemesterId)
        {
            return DbInstance.User_Account.Where(c=>c.JoiningSemesterId==joiningSemesterId).ToList();
        }

        #endregion
        #region Get By Unique Key
        public List<User_Account> GetAllByUserName(String userName)
        {
            return DbInstance.User_Account.Where(c=>c.UserName==userName).ToList();
        }

        #endregion
        
        #region Get By Id
        public User_Account GetById(Int32 id)
        {
            return DbInstance.User_Account.Find(id);
        }
        public User_Account GetById(Int32 id, params string[] includeMembers)
        {
            IQueryable<User_Account> query = (from q in DbInstance.User_Account
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
        ///  to get all User_Account set query parameter null.
        /// </summary>
        /// <param name="query">ex: IQueryable<User_Account> query = DbInstance.User_Account.Where(x => x.Test=="ABC");</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <param name="count"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public List<User_Account> GetPagedList(IQueryable<User_Account> query, int? pageSize, int? pageNo, ref int count, out string error)
        {
            error = "";
            try
            {
                if (query == null)
                {
                    query = DbInstance.User_Account;
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
        /// <param name="query">ex: IQueryable<User_Account> query = DbInstance.User_Account.Where(x => x.Test==test);</param>
        /// <param name="error"></param>
        /// <returns></returns>
        public List<User_Account> GetListByQuery(IQueryable<User_Account> query, out string error)
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
        public List<User_Account> GetAll(out string error)
        {
            error = "";
            try
            {
                return DbInstance.User_Account.ToList();
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return null;
            }
        }
        #endregion

        #region Save Account
        
        public delegate bool SaveCallBackDelegate(User_Account newObj, User_Account unchangedDbObj, bool isNewObject, DbContextTransaction transactionScope, out string error, params string[] columnsToCopy);
        protected abstract bool HasSavePermission(out string error);
        
        public virtual bool IsValidToSave(User_Account newObj, out string error)
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
        public bool Save(User_Account newObj, SaveCallBackDelegate saveCallBackDelegate, out string error,  params string[] columnsToCopy)
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
        public bool Save(User_Account newObj, out string error, DbContextTransaction scope = null, bool checkIsValidToSave = true, SaveCallBackDelegate saveCallBackDelegate = null, params string[] columnsToCopy)
        {
            error = string.Empty;
            User_Account dbAddedObj =null;
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
        public bool Save(User_Account newObj,ref User_Account dbAddedObj, out string error,DbContextTransaction scope=null, bool checkIsValidToSave = true, SaveCallBackDelegate saveCallBackDelegate = null, params string[] columnsToCopy)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Account to save can't be null!";
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
                User_Account objToSave = null;

                if (newObj.Id != 0)
                {
                    objToSave = DbInstance.User_Account.SingleOrDefault(x => x.Id == newObj.Id);
                    isNewObject = false;
                }
                if (objToSave == null)
                {
                    isNewObject = true;
                    objToSave = User_Account.GetNew(newObj.Id);
                    DbInstance.User_Account.Add(objToSave);
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

        #region Delete Account
        /// <summary>
        /// not implemented yet.
        /// </summary>
        /// <param name="objToDelete"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public virtual bool IsValidToDelete(User_Account objToDelete, out string error)
        {
            error = "";
            if (DbInstance.Aca_ClassMaterialFileMap.Any(x => x.UserId == objToDelete.Id))
            {
                error = "This data used in Class Material File Map table, first delete those!";
                return false;
            }
            if (DbInstance.Aca_CreditTransfer.Any(x => x.ApprovedById == objToDelete.Id))
            {
                error = "This data used in Credit Transfer table, first delete those!";
                return false;
            }
            if (DbInstance.General_AllSmsLog.Any(x => x.SentToUserId == objToDelete.Id))
            {
                error = "This data used in All Sms Log table, first delete those!";
                return false;
            }
            if (DbInstance.Lib_Borrower.Any(x => x.Id == objToDelete.Id))
            {
                error = "This data used in Borrower table, first delete those!";
                return false;
            }
            if (DbInstance.UAC_PassReset.Any(x => x.UserId == objToDelete.Id))
            {
                error = "This data used in Pass Reset table, first delete those!";
                return false;
            }
            if (DbInstance.UAC_RoleUserMap.Any(x => x.UserId == objToDelete.Id))
            {
                error = "This data used in Role User Map table, first delete those!";
                return false;
            }
            if (DbInstance.UAC_VerificationAudit.Any(x => x.UserId == objToDelete.Id))
            {
                error = "This data used in Verification Audit table, first delete those!";
                return false;
            }
            if (DbInstance.UAC_VerificationAudit.Any(x => x.ConfirmedById == objToDelete.Id))
            {
                error = "This data used in Verification Audit table, first delete those!";
                return false;
            }
            if (DbInstance.UAC_VerificationAudit.Any(x => x.InitiatedById == objToDelete.Id))
            {
                error = "This data used in Verification Audit table, first delete those!";
                return false;
            }
            if (DbInstance.User_ContactAddress.Any(x => x.UserId == objToDelete.Id))
            {
                error = "This data used in Contact Address table, first delete those!";
                return false;
            }
            if (DbInstance.User_ContactEmail.Any(x => x.UserId == objToDelete.Id))
            {
                error = "This data used in Contact Email table, first delete those!";
                return false;
            }
            if (DbInstance.User_ContactNumber.Any(x => x.UserId == objToDelete.Id))
            {
                error = "This data used in Contact Number table, first delete those!";
                return false;
            }
            if (DbInstance.User_Education.Any(x => x.UserId == objToDelete.Id))
            {
                error = "This data used in Education table, first delete those!";
                return false;
            }
            if (DbInstance.User_Employee.Any(x => x.UserId == objToDelete.Id))
            {
                error = "This data used in Employee table, first delete those!";
                return false;
            }
            if (DbInstance.User_Experience.Any(x => x.UserId == objToDelete.Id))
            {
                error = "This data used in Experience table, first delete those!";
                return false;
            }
            if (DbInstance.User_Image.Any(x => x.UserId == objToDelete.Id))
            {
                error = "This data used in Image table, first delete those!";
                return false;
            }
            if (DbInstance.User_ProfileChangeAudit.Any(x => x.UserId == objToDelete.Id))
            {
                error = "This data used in Profile Change Audit table, first delete those!";
                return false;
            }
            if (DbInstance.User_ProfileChangeAudit.Any(x => x.NewUserId == objToDelete.Id))
            {
                error = "This data used in Profile Change Audit table, first delete those!";
                return false;
            }
            if (DbInstance.User_Publication.Any(x => x.UserId == objToDelete.Id))
            {
                error = "This data used in Publication table, first delete those!";
                return false;
            }
            if (DbInstance.User_RankHistory.Any(x => x.UserId == objToDelete.Id))
            {
                error = "This data used in Rank History table, first delete those!";
                return false;
            }
            if (DbInstance.User_Student.Any(x => x.UserId == objToDelete.Id))
            {
                error = "This data used in Student table, first delete those!";
                return false;
            }
            return true;
        }
        public bool DeleteById(Int32 id, out string error, DbContextTransaction scope = null, bool checkIsValidToDelete = true)
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
                User_Account objToDelete = DbInstance.User_Account.Find(id);
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
                DbInstance.User_Account.Remove(objToDelete);
                
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
        public bool Delete(User_Account objToDelete, out string error, DbContextTransaction scope = null, bool checkIsValidToDelete = true)
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
                    DbInstance.User_Account.Attach(objToDelete);
                }
                DbInstance.User_Account.Remove(objToDelete);
                
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


//File: DataServiceBase for User_Employee
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
    public abstract class User_EmployeeDataServiceBase : BaseDataService
    {
        public User_EmployeeDataServiceBase(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
        }
        #region Get By Foreign Key
        public List<User_Employee> GetAllByUserId(Int32 userId)
        {
            return DbInstance.User_Employee.Where(c=>c.UserId==userId).ToList();
        }

        public List<User_Employee> GetAllByJoiningDepartmentId(Int32 joiningDepartmentId)
        {
            return DbInstance.User_Employee.Where(c=>c.JoiningDepartmentId==joiningDepartmentId).ToList();
        }

        public List<User_Employee> GetAllByPositionId(Int32 positionId)
        {
            return DbInstance.User_Employee.Where(c=>c.PositionId==positionId).ToList();
        }

        #endregion
        #region Get By Unique Key
        #endregion
        
        #region Get By Id
        public User_Employee GetById(Int32 id)
        {
            return DbInstance.User_Employee.Find(id);
        }
        public User_Employee GetById(Int32 id, params string[] includeMembers)
        {
            IQueryable<User_Employee> query = (from q in DbInstance.User_Employee
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
        ///  to get all User_Employee set query parameter null.
        /// </summary>
        /// <param name="query">ex: IQueryable<User_Employee> query = DbInstance.User_Employee.Where(x => x.Test=="ABC");</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <param name="count"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public List<User_Employee> GetPagedList(IQueryable<User_Employee> query, int? pageSize, int? pageNo, ref int count, out string error)
        {
            error = "";
            try
            {
                if (query == null)
                {
                    query = DbInstance.User_Employee;
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
        /// <param name="query">ex: IQueryable<User_Employee> query = DbInstance.User_Employee.Where(x => x.Test==test);</param>
        /// <param name="error"></param>
        /// <returns></returns>
        public List<User_Employee> GetListByQuery(IQueryable<User_Employee> query, out string error)
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
        public List<User_Employee> GetAll(out string error)
        {
            error = "";
            try
            {
                return DbInstance.User_Employee.ToList();
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return null;
            }
        }
        #endregion

        #region Save Employee
        
        public delegate bool SaveCallBackDelegate(User_Employee newObj, User_Employee unchangedDbObj, bool isNewObject, DbContextTransaction transactionScope, out string error, params string[] columnsToCopy);
        protected abstract bool HasSavePermission(out string error);
        
        public virtual bool IsValidToSave(User_Employee newObj, out string error)
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
        public bool Save(User_Employee newObj, SaveCallBackDelegate saveCallBackDelegate, out string error,  params string[] columnsToCopy)
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
        public bool Save(User_Employee newObj, out string error, DbContextTransaction scope = null, bool checkIsValidToSave = true, SaveCallBackDelegate saveCallBackDelegate = null, params string[] columnsToCopy)
        {
            error = string.Empty;
            User_Employee dbAddedObj =null;
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
        public bool Save(User_Employee newObj,ref User_Employee dbAddedObj, out string error,DbContextTransaction scope=null, bool checkIsValidToSave = true, SaveCallBackDelegate saveCallBackDelegate = null, params string[] columnsToCopy)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Employee to save can't be null!";
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
                User_Employee objToSave = null;

                if (newObj.Id != 0)
                {
                    objToSave = DbInstance.User_Employee.SingleOrDefault(x => x.Id == newObj.Id);
                    isNewObject = false;
                }
                if (objToSave == null)
                {
                    isNewObject = true;
                    objToSave = User_Employee.GetNew(newObj.Id);
                    DbInstance.User_Employee.Add(objToSave);
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

        #region Delete Employee
        /// <summary>
        /// not implemented yet.
        /// </summary>
        /// <param name="objToDelete"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public virtual bool IsValidToDelete(User_Employee objToDelete, out string error)
        {
            error = "";
            if (DbInstance.Aca_ClassAttendanceSetting.Any(x => x.EmployeeId == objToDelete.Id))
            {
                error = "This data used in Class Attendance Setting table, first delete those!";
                return false;
            }
            if (DbInstance.Aca_ClassNotice.Any(x => x.EmployeeId == objToDelete.Id))
            {
                error = "This data used in Class Notice table, first delete those!";
                return false;
            }
            if (DbInstance.Aca_ClassRoutine.Any(x => x.EmployeeId == objToDelete.Id))
            {
                error = "This data used in Class Routine table, first delete those!";
                return false;
            }
            if (DbInstance.Aca_ClassTakenByEmployee.Any(x => x.EmployeeId == objToDelete.Id))
            {
                error = "This data used in Class Taken By Employee table, first delete those!";
                return false;
            }
            if (DbInstance.Aca_HomeWorkSubmission.Any(x => x.TeacherId == objToDelete.Id))
            {
                error = "This data used in Home Work Submission table, first delete those!";
                return false;
            }
            if (DbInstance.HR_AppointmentHistory.Any(x => x.EmployeeId == objToDelete.Id))
            {
                error = "This data used in Appointment History table, first delete those!";
                return false;
            }
            if (DbInstance.HR_Attendance.Any(x => x.EmployeeId == objToDelete.Id))
            {
                error = "This data used in Attendance table, first delete those!";
                return false;
            }
            if (DbInstance.HR_EmpLeaveAllocationSettings.Any(x => x.EmployeeId == objToDelete.Id))
            {
                error = "This data used in Emp Leave Allocation Settings table, first delete those!";
                return false;
            }
            if (DbInstance.HR_EmpLeaveApplication.Any(x => x.EmployeeId == objToDelete.Id))
            {
                error = "This data used in Emp Leave Application table, first delete those!";
                return false;
            }
            if (DbInstance.HR_EmpLeaveApplication.Any(x => x.RecommendByEmployeeId == objToDelete.Id))
            {
                error = "This data used in Emp Leave Application table, first delete those!";
                return false;
            }
            if (DbInstance.HR_EmployementHistory.Any(x => x.EmployeeId == objToDelete.Id))
            {
                error = "This data used in Employement History table, first delete those!";
                return false;
            }
            if (DbInstance.HR_MonthlyPayslip.Any(x => x.EmployeeId == objToDelete.Id))
            {
                error = "This data used in Monthly Payslip table, first delete those!";
                return false;
            }
            if (DbInstance.HR_MonthlyPayslipDetails.Any(x => x.EmployeeId == objToDelete.Id))
            {
                error = "This data used in Monthly Payslip Details table, first delete those!";
                return false;
            }
            if (DbInstance.HR_SalarySettings.Any(x => x.EmployeeId == objToDelete.Id))
            {
                error = "This data used in Salary Settings table, first delete those!";
                return false;
            }
            if (DbInstance.User_Employee.Any(x => x.Id == objToDelete.Id))
            {
                error = "This data used in Employee table, first delete those!";
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
                User_Employee objToDelete = DbInstance.User_Employee.Find(id);
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
                DbInstance.User_Employee.Remove(objToDelete);
                
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
        public bool Delete(User_Employee objToDelete, out string error, DbContextTransaction scope = null, bool checkIsValidToDelete = true)
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
                    DbInstance.User_Employee.Attach(objToDelete);
                }
                DbInstance.User_Employee.Remove(objToDelete);
                
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


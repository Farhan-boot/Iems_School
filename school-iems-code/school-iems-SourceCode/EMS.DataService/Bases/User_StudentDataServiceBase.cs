//File: DataServiceBase for User_Student
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
    public abstract class User_StudentDataServiceBase : BaseDataService
    {
        public User_StudentDataServiceBase(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
        }
        #region Get By Foreign Key
        public List<User_Student> GetAllByUserId(Int32 userId)
        {
            return DbInstance.User_Student.Where(c=>c.UserId==userId).ToList();
        }

        public List<User_Student> GetAllByGradingPolicyId(Int32 gradingPolicyId)
        {
            return DbInstance.User_Student.Where(c=>c.GradingPolicyId==gradingPolicyId).ToList();
        }

        public List<User_Student> GetAllByLevelTermId(Int32 levelTermId)
        {
            return DbInstance.User_Student.Where(c=>c.LevelTermId==levelTermId).ToList();
        }

        public List<User_Student> GetAllByProgramId(Int32 programId)
        {
            return DbInstance.User_Student.Where(c=>c.ProgramId==programId).ToList();
        }

        public List<User_Student> GetAllByContinuingBatchId(Int32 continuingBatchId)
        {
            return DbInstance.User_Student.Where(c=>c.ContinuingBatchId==continuingBatchId).ToList();
        }

        public List<User_Student> GetAllByCurriculumId(Int64 curriculumId)
        {
            return DbInstance.User_Student.Where(c=>c.CurriculumId==curriculumId).ToList();
        }

        public List<User_Student> GetAllByMajorCurriculumId(Int64 majorCurriculumId)
        {
            return DbInstance.User_Student.Where(c=>c.MajorCurriculumId==majorCurriculumId).ToList();
        }

        public List<User_Student> GetAllBySecondMajorCurriculumId(Int64 secondMajorCurriculumId)
        {
            return DbInstance.User_Student.Where(c=>c.SecondMajorCurriculumId==secondMajorCurriculumId).ToList();
        }

        public List<User_Student> GetAllByMinorCurriculumId(Int64 minorCurriculumId)
        {
            return DbInstance.User_Student.Where(c=>c.MinorCurriculumId==minorCurriculumId).ToList();
        }

        public List<User_Student> GetAllByElectiveCurriculumId(Int64 electiveCurriculumId)
        {
            return DbInstance.User_Student.Where(c=>c.ElectiveCurriculumId==electiveCurriculumId).ToList();
        }

        public List<User_Student> GetAllByStudentQuotaNameId(Int32 studentQuotaNameId)
        {
            return DbInstance.User_Student.Where(c=>c.StudentQuotaNameId==studentQuotaNameId).ToList();
        }

        public List<User_Student> GetAllByParentsPrimaryJobTypeId(Int32 parentsPrimaryJobTypeId)
        {
            return DbInstance.User_Student.Where(c=>c.ParentsPrimaryJobTypeId==parentsPrimaryJobTypeId).ToList();
        }

        public List<User_Student> GetAllByAdmissionExamId(Int32 admissionExamId)
        {
            return DbInstance.User_Student.Where(c=>c.AdmissionExamId==admissionExamId).ToList();
        }

        public List<User_Student> GetAllByGraduationSemesterId(Int64 graduationSemesterId)
        {
            return DbInstance.User_Student.Where(c=>c.GraduationSemesterId==graduationSemesterId).ToList();
        }

        public List<User_Student> GetAllByClassSectionId(Int32 classSectionId)
        {
            return DbInstance.User_Student.Where(c=>c.ClassSectionId==classSectionId).ToList();
        }

        public List<User_Student> GetAllByJoiningBatchId(Int32 joiningBatchId)
        {
            return DbInstance.User_Student.Where(c=>c.JoiningBatchId==joiningBatchId).ToList();
        }

        public List<User_Student> GetAllByPreviousProgramId(Int32 previousProgramId)
        {
            return DbInstance.User_Student.Where(c=>c.PreviousProgramId==previousProgramId).ToList();
        }

        public List<User_Student> GetAllByDiscontinueSemesterId(Int64 discontinueSemesterId)
        {
            return DbInstance.User_Student.Where(c=>c.DiscontinueSemesterId==discontinueSemesterId).ToList();
        }

        #endregion
        #region Get By Unique Key
        public List<User_Student> GetAllByClassRollNo(String classRollNo)
        {
            return DbInstance.User_Student.Where(c=>c.ClassRollNo==classRollNo).ToList();
        }

        #endregion
        
        #region Get By Id
        public User_Student GetById(Int32 id)
        {
            return DbInstance.User_Student.Find(id);
        }
        public User_Student GetById(Int32 id, params string[] includeMembers)
        {
            IQueryable<User_Student> query = (from q in DbInstance.User_Student
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
        ///  to get all User_Student set query parameter null.
        /// </summary>
        /// <param name="query">ex: IQueryable<User_Student> query = DbInstance.User_Student.Where(x => x.Test=="ABC");</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <param name="count"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public List<User_Student> GetPagedList(IQueryable<User_Student> query, int? pageSize, int? pageNo, ref int count, out string error)
        {
            error = "";
            try
            {
                if (query == null)
                {
                    query = DbInstance.User_Student;
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
        /// <param name="query">ex: IQueryable<User_Student> query = DbInstance.User_Student.Where(x => x.Test==test);</param>
        /// <param name="error"></param>
        /// <returns></returns>
        public List<User_Student> GetListByQuery(IQueryable<User_Student> query, out string error)
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
        public List<User_Student> GetAll(out string error)
        {
            error = "";
            try
            {
                return DbInstance.User_Student.ToList();
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return null;
            }
        }
        #endregion

        #region Save Student
        
        public delegate bool SaveCallBackDelegate(User_Student newObj, User_Student unchangedDbObj, bool isNewObject, DbContextTransaction transactionScope, out string error, params string[] columnsToCopy);
        protected abstract bool HasSavePermission(out string error);
        
        public virtual bool IsValidToSave(User_Student newObj, out string error)
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
        public bool Save(User_Student newObj, SaveCallBackDelegate saveCallBackDelegate, out string error,  params string[] columnsToCopy)
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
        public bool Save(User_Student newObj, out string error, DbContextTransaction scope = null, bool checkIsValidToSave = true, SaveCallBackDelegate saveCallBackDelegate = null, params string[] columnsToCopy)
        {
            error = string.Empty;
            User_Student dbAddedObj =null;
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
        public bool Save(User_Student newObj,ref User_Student dbAddedObj, out string error,DbContextTransaction scope=null, bool checkIsValidToSave = true, SaveCallBackDelegate saveCallBackDelegate = null, params string[] columnsToCopy)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Student to save can't be null!";
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
                User_Student objToSave = null;

                if (newObj.Id != 0)
                {
                    objToSave = DbInstance.User_Student.SingleOrDefault(x => x.Id == newObj.Id);
                    isNewObject = false;
                }
                if (objToSave == null)
                {
                    isNewObject = true;
                    objToSave = User_Student.GetNew(newObj.Id);
                    DbInstance.User_Student.Add(objToSave);
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

        #region Delete Student
        /// <summary>
        /// not implemented yet.
        /// </summary>
        /// <param name="objToDelete"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public virtual bool IsValidToDelete(User_Student objToDelete, out string error)
        {
            error = "";
            if (DbInstance.Aca_ClassAttendance.Any(x => x.StudentId == objToDelete.Id))
            {
                error = "This data used in Class Attendance table, first delete those!";
                return false;
            }
            if (DbInstance.Aca_ClassAttendanceSmsLog.Any(x => x.StudentId == objToDelete.Id))
            {
                error = "This data used in Class Attendance Sms Log table, first delete those!";
                return false;
            }
            if (DbInstance.Aca_ClassTakenByStudent.Any(x => x.StudentId == objToDelete.Id))
            {
                error = "This data used in Class Taken By Student table, first delete those!";
                return false;
            }
            if (DbInstance.Aca_ClassWaiverStudent.Any(x => x.StudentId == objToDelete.Id))
            {
                error = "This data used in Class Waiver Student table, first delete those!";
                return false;
            }
            if (DbInstance.Aca_CreditTransfer.Any(x => x.StudentId == objToDelete.Id))
            {
                error = "This data used in Credit Transfer table, first delete those!";
                return false;
            }
            if (DbInstance.Aca_CreditTransferCourses.Any(x => x.StudentId == objToDelete.Id))
            {
                error = "This data used in Credit Transfer Courses table, first delete those!";
                return false;
            }
            if (DbInstance.Aca_CurriculumStudentMap.Any(x => x.StudentId == objToDelete.Id))
            {
                error = "This data used in Curriculum Student Map table, first delete those!";
                return false;
            }
            if (DbInstance.Aca_ExamRoll.Any(x => x.StudentId == objToDelete.Id))
            {
                error = "This data used in Exam Roll table, first delete those!";
                return false;
            }
            if (DbInstance.Aca_HomeWorkSubmission.Any(x => x.StudentId == objToDelete.Id))
            {
                error = "This data used in Home Work Submission table, first delete those!";
                return false;
            }
            if (DbInstance.Aca_OnlineExamResultDetail.Any(x => x.StudentId == objToDelete.Id))
            {
                error = "This data used in Online Exam Result Detail table, first delete those!";
                return false;
            }
            if (DbInstance.Aca_OnlineExamTakenByStudent.Any(x => x.StudentId == objToDelete.Id))
            {
                error = "This data used in Online Exam Taken By Student table, first delete those!";
                return false;
            }
            if (DbInstance.Aca_ProgramTakenByStudent.Any(x => x.StudentId == objToDelete.Id))
            {
                error = "This data used in Program Taken By Student table, first delete those!";
                return false;
            }
            if (DbInstance.Aca_StudentRegistration.Any(x => x.StudentId == objToDelete.Id))
            {
                error = "This data used in Student Registration table, first delete those!";
                return false;
            }
            if (DbInstance.Htl_HostelBuildingStudentMap.Any(x => x.StudentId == objToDelete.Id))
            {
                error = "This data used in Hostel Building Student Map table, first delete those!";
                return false;
            }
            if (DbInstance.Htl_StudentMealCancel.Any(x => x.StudentId == objToDelete.Id))
            {
                error = "This data used in Student Meal Cancel table, first delete those!";
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
                User_Student objToDelete = DbInstance.User_Student.Find(id);
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
                DbInstance.User_Student.Remove(objToDelete);
                
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
        public bool Delete(User_Student objToDelete, out string error, DbContextTransaction scope = null, bool checkIsValidToDelete = true)
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
                    DbInstance.User_Student.Attach(objToDelete);
                }
                DbInstance.User_Student.Remove(objToDelete);
                
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


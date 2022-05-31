using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web.Hosting;
using EMS.CoreLibrary;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;
using EMS.Framework.Settings;
using EMS.Framework.Utils;
using EMS.Web.Jsons.Models;

namespace EMS.Facade.AcademicArea
{
    public class ClassFacade : BaseFacade
    {
        private Aca_ClassDataService classService = null;
        public ClassFacade(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
            classService = new Aca_ClassDataService(emsDbContext, usersProfile);
        }

        #region Class
        public List<Aca_Class> GetClassList(string name, long semesterId, long studyLevelTermId, long departmentId, int? pageSize, int? pageNo, ref int count)
        {
            IQueryable<Aca_Class> query = (from q in DbInstance.Aca_Class
                                           select q)
                                        .Include(x => x.Aca_CurriculumCourse)
                                        .Include(x => x.Aca_Semester)
                                        .Include(x => x.Aca_StudyLevelTerm)
                                        .OrderBy(x => x.Aca_CurriculumCourse.Aca_Curriculum.CurriculumNo)
                                        .ThenBy(x => x.Aca_CurriculumCourse.Aca_Curriculum.Aca_Program.ProgramTypeEnumId)
                                        .ThenByDescending(x => x.Aca_CurriculumCourse.Aca_Curriculum.Aca_Program.Id)
                                        .ThenByDescending(x => x.Aca_CurriculumCourse.CurriculumId)
                                        .ThenByDescending(x => x.SemesterId)
                                        .ThenBy(x => x.StudyLevelTermId)
                                        .ThenBy(x => x.Aca_CurriculumCourse.Code)
                                        .ThenBy(x => x.ClassNo);
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrWhiteSpace(name))
            {
                query = from q in query
                        where q.Name.Contains(name)
                        select q;
            }
            if (semesterId > 0)
            {
                query = from q in query
                        where q.SemesterId == semesterId
                        select q;
            }
            if (studyLevelTermId > 0)
            {
                query = from q in query
                        where q.StudyLevelTermId == studyLevelTermId
                        select q;
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

        private bool ValidateClass(Aca_Class newObj, out string error)
        {
            error = string.Empty;
            try
            {
                if (newObj.SemesterId <= 0)
                {
                    error += "Please select valid Semester.";
                    return false;
                }
                if (newObj.ProgramId <= 0)
                {
                    error += "Please select valid Program.";
                    return false;
                }

                if (newObj.CurriculumCourseId <= 0)
                {
                    error += "Please select valid Curriculum Course.";
                    return false;
                }
                if (newObj.StudyLevelTermId <= 0)
                {
                    error += "Please select valid Study Level Term.";
                    return false;
                }
                if (newObj.ClassSectionId <= 0)
                {
                    error += "Please select Valid Section.";
                    return false;
                }

                var isFinalExamExists = DbInstance.Aca_Exam.Any(x =>
                    x.SemesterId == newObj.SemesterId && x.ExamTypeEnumId == (byte)Aca_Exam.ExamTypeEnum.FinalTerm &&
                    !x.IsDeleted);

                if (!isFinalExamExists)
                {
                    error += "No Final Term Exam Created for this Semester. Please created Exam Settings first then offer courses.";
                    return false;
                }

                //todo check departmentwise permission, Only Dept faculties are able to save class
                //var emp = DbInstance.User_Employee
                //    .SingleOrDefault(x => x.UserId == HttpUtil.Profile.UserId);
                //if (emp != null
                //    && emp.DepartmentId != newObj.DepartmentId
                //    && !HttpUtil.IsSupperAdmin()
                //    )
                //{
                //    error = "You can't add/edit others department's offered course.";
                //    return false;
                //}


                // Get Class list for checking for this class Exist or not
                var existThisCourseClassList = DbInstance.Aca_Class.Where(x => x.Id != newObj.Id
                                && x.ClassSectionId == newObj.ClassSectionId
                                && x.CurriculumCourseId == newObj.CurriculumCourseId
                                //&& x.StudyLevelTermId == newObj.StudyLevelTermId
                                && x.SemesterId == newObj.SemesterId
                                && x.ProgramId == newObj.ProgramId
                                && x.CampusId == newObj.CampusId
                    ).ToList();


                if (newObj.StudentBatchId == null)
                {
                    var isExistInThisLevelTerm = existThisCourseClassList
                        .Any(x => x.StudyLevelTermId == newObj.StudyLevelTermId && x.StudentBatchId !=null);

                    if (isExistInThisLevelTerm)
                    {
                        error = "Class exist with same Section and Specific Continuing Batch in this level term! If you Cannot add one undefined and one specific Class.";
                        return false;
                    }
                }

                //if same Course+Section, exist in same program, level term
                var existClassInThisLevelTerm = existThisCourseClassList
                    .FirstOrDefault(x=>x.StudyLevelTermId == newObj.StudyLevelTermId && (x.StudentBatchId == null || x.StudentBatchId == newObj.StudentBatchId));
                if (existClassInThisLevelTerm!=null)
                {
                    if (existClassInThisLevelTerm.StudentBatchId == null)
                    {
                        error = "Class exist with Undefined Batch in this level term! If you Want to Offer Multiple classes with same section please provide specific batch for each class.";
                        return false;
                    }

                    error = "Class exist with same Section & Continuing Batch in this level term!";
                    return false;
                }



                if (newObj.DepartmentId <= 0)
                {
                    error += "Please select valid Department.";
                    return false;
                }
              

                var curriculumCourse = DbInstance.Aca_CurriculumCourse
                    .Include(x => x.Aca_Curriculum)
                    .Include(x => x.Aca_Curriculum.Aca_Program)
                    .SingleOrDefault(
                        x => x.Id == newObj.CurriculumCourseId);

                if (curriculumCourse == null)
                {
                    error += "Invalid Curriculum Course.";
                    return false;
                }

                /*
                 * if same Course+Section, exist in same program, same semester but another Level Term
                 * Then Change Class Name like 'Analytic Mechanics [A-L1T1]'
                 */
                var isExistAnotherLevelTerm = existThisCourseClassList.Any(x => x.StudyLevelTermId != newObj.StudyLevelTermId);

                var classSection = DbInstance.Aca_ClassSection.SingleOrDefault(x => x.Id == newObj.ClassSectionId);
                if (classSection == null)
                {
                    error += "Please select Valid Section.";
                    return false;
                }

                var batchName = $"";

                if (isExistAnotherLevelTerm)
                {
                    var levelTerm = DbInstance.Aca_StudyLevelTerm.SingleOrDefault(x => x.Id == newObj.StudyLevelTermId);
                    if (levelTerm==null)
                    {
                        error += "Please select valid Study Level Term.";
                        return false;
                    }

                    if (newObj.StudentBatchId == null)
                    {
                        // Analytic Mechanics [A-L1T1]
                        newObj.Name = $"{curriculumCourse.Name} [{classSection.Name}-{levelTerm.ShortName.Replace("-", "")}]";
                    }
                    else
                    {
                        // Analytic Mechanics [A-L1T1-B25]
                        newObj.Name = $"{curriculumCourse.Name} [{classSection.Name}-{levelTerm.ShortName.Replace("-", "")}-B{newObj.StudentBatchId}]";
                    }
                }
                else
                {
                    if (newObj.StudentBatchId == null)
                    {
                        // Analytic Mechanics [A]
                        newObj.Name = $"{curriculumCourse.Name} [{classSection.Name}]";
                    }
                    else
                    {
                        // Analytic Mechanics [A-B25]
                        newObj.Name = $"{curriculumCourse.Name} [{classSection.Name}-B{newObj.StudentBatchId}]";
                    }
                    
                }

                //if (DbInstance.Aca_ClassTakenByStudent.Any(x => x.ClassId == newObj.Id))
                //{
                //    if (DbInstance.Aca_ClassTakenByStudent
                //        .Include(x=>x.User_Student)
                //        .First(x => x.ClassId == newObj.Id)
                //        .User_Student.CurriculumId != curriculumCourse.CurriculumId)
                //    {
                //        error += "This Curriculum is not for the Students of this Course.";
                //        return false;
                //    }
                //}
                if (newObj.ProgramId != curriculumCourse.Aca_Curriculum.ProgramId)
                {
                    error += "Please select the Curriculum Course again.";
                    return false;
                }
                var dept = DbInstance.HR_Department.SingleOrDefault(x => x.Id == newObj.DepartmentId);
                if (dept == null)
                {
                    error += "Invalid Department.";
                    return false;
                }

                if (!newObj.Code.IsValid())
                {
                    error += "Code is not valid.";
                    return false;
                }
                if (newObj.Code.Length > 128)
                {
                    error += "Code exceeded its maximum length 128.";
                    return false;
                }
                if (!newObj.Name.IsValid())
                {
                    error += "Name is not valid.";
                    return false;
                }
                if (newObj.Name.Length > 256)
                {
                    error += "Name exceeded its maximum length 256.";
                    return false;
                }
                if (newObj.ClassNo == null)
                {
                    error += "Class No required.";
                    return false;
                }
              
           
                //if (newObj.SemesterId <= 0)
                //{
                //    error += "Please select valid Semester Level Term.";
                //    return false;
                //}
                //var semesterLevelTerm = DbInstance.Aca_SemesterLevelTerm
                //    .SingleOrDefault(x => x.Id == newObj.SemesterId);
                //if (semesterLevelTerm == null)
                //{
                //    error += "Invalid Semester Level Term.";
                //    return false;
                //}
                //newObj.SemesterId = semesterLevelTerm.SemesterId;
                //var studyLevelTerm = DbInstance.Aca_StudyLevelTerm
                //    .SingleOrDefault(x => x.Id == semesterLevelTerm.LevelTermId);


              
                if (newObj.ProgramTypeEnumId == null)
                {
                    error += "Please select valid Program Type.";
                    return false;
                }
                if (newObj.RegularCapacity <10)
                {
                    error += "Student Seat Capacity Must be Grater Then 10.";
                    return false;
                }
                if (newObj.StatusEnumId == null)
                {
                    error += "Please select valid Status.";
                    return false;
                }

                if (newObj.RegularExamMarkDistributionPolicyId == null)
                {
                    error += "Regular Exam Mark Distribution Policy Id required.";
                    return false;
                }
                if (newObj.RegularExamMarkDistributionPolicyId <=0)
                {
                    error += "Regular Exam Mark Distribution Policy required.";
                    return false;
                }
                //if (newObj.ProgramTypeEnumId == (byte)Aca_Program.ProgramTypeEnum.Undergraduate
                //    && newObj.ReferredExamMarkDistributionPolicyId == null
                //    && curriculumCourse.CourseCategoryEnumId != (byte)Aca_CurriculumCourse.CourseCategoryEnum.Thesis)
                //{
                //    error += "Referred Exam Mark Distribution Policy Id required.";
                //    return false;
                //}

                //if (newObj.ProgramTypeEnumId == (byte)Aca_Program.ProgramTypeEnum.Undergraduate
                //    && newObj.BacklogExamMarkDistributionPolicyId == null
                //    && curriculumCourse.CourseCategoryEnumId != (byte)Aca_CurriculumCourse.CourseCategoryEnum.Thesis)
                //{
                //    error += "Backlog Exam Mark Distribution Policy Id required.";
                //    return false;
                //}

                if (newObj.IsDeleted == null)
                {
                    error += "Is Deleted required.";
                    return false;
                }

                var obj = DbInstance.Aca_Class
                    .SingleOrDefault(x => x.Id == newObj.Id);
                if (obj != null)
                {
                    var hasStudent = DbInstance.Aca_ClassTakenByStudent.Any(x => x.ClassId == newObj.Id);
                    bool isPermit = PermissionUtil.HasPermission(
                        PermissionCollection.AcademicArea.OfferedClassManager.OfferedClass.CanForceEdit);
                    if (hasStudent && !isPermit)
                    {
                        var msg = "Because this Class has already taken by Students.";
                        //if (newObj.RegularCapacity != obj.RegularCapacity)
                        //{
                        //    error = "Regular Capacity can't be editable." + msg;
                        //    return false;
                        //}
                        if (newObj.CurriculumCourseId != obj.CurriculumCourseId)
                        {
                            error = "Curriculum Course can't be editable." + msg;
                            return false;
                        }
                        if (newObj.SemesterId != obj.SemesterId)
                        {
                            error = "Semester can't be editable." + msg;
                            return false;
                        }
                        if (newObj.CampusId != obj.CampusId)
                        {
                            error = "Campus can't be editable." + msg;
                            return false;
                        }
                        //if (newObj.RegularExamMarkDistributionPolicyId != obj.RegularExamMarkDistributionPolicyId)
                        //{
                        //    error = "Regular Exam Mark Distribution Policy can't be editable." + msg;
                        //    return false;
                        //}
                        //if (newObj.ReferredExamMarkDistributionPolicyId != obj.ReferredExamMarkDistributionPolicyId)
                        //{
                        //    error = "Referred Exam Mark Distribution Policy can't be editable." + msg;
                        //    return false;
                        //}
                        //if (newObj.BacklogExamMarkDistributionPolicyId != obj.BacklogExamMarkDistributionPolicyId)
                        //{
                        //    error = "Backlog Exam Mark Distribution Policy can't be editable." + msg;
                        //    return false;
                        //}
                    }
                }
                else
                {
                    //if (curriculumCourse.CreditTypeEnumId==(byte)Aca_CurriculumCourse.CreditTypeEnum.CourseWaiver)
                    //{
                    //    error = $"Don't Create {Aca_CurriculumCourse.CreditTypeEnum.CourseWaiver.ToString().AddSpacesToSentence()} Credit Type Course.";
                    //    return false;
                    //}

                    var lastObj = DbInstance.Aca_Class
                        //.Include(x => x.Aca_CurriculumCourse)
                        //.Include(x => x.Aca_CurriculumCourse.Aca_Curriculum)
                        //.Include(x => x.Aca_CurriculumCourse.Aca_Curriculum.Aca_Program)
                        .OrderByDescending(x => x.Id)
                        .FirstOrDefault(
                            x => x.Aca_CurriculumCourse.Aca_Curriculum.Aca_Program.ProgramTypeEnumId
                                 == curriculumCourse.Aca_Curriculum.Aca_Program.ProgramTypeEnumId
                                 && x.DepartmentId == dept.Id
                                 && x.SemesterId == newObj.SemesterId
                        );
                    if (lastObj == null)
                    {
                        newObj.ClassNo = Convert.ToInt32(
                            dept.DepartmentNo +
                            curriculumCourse.Aca_Curriculum.Aca_Program.ProgramTypeEnumId.ToString().PadLeft(1, '0') +
                            "001");
                    }
                    else
                    {
                        newObj.ClassNo = lastObj.ClassNo + 1;
                    }
                }



                return true;
            }
            catch (Exception ex)
            {
                error += ex.Message;
                return false;
            }
        }
        public bool Offer1stSemesterCourses(int programId, long semesterId, int classSectionId, long curriculumId, int departmentId, DbContextTransaction scope, out string error)
        {
            error = string.Empty;

            var isAlreadyCourseOffered = DbInstance.Aca_Class
                .Any(x => x.ProgramId == programId && x.SemesterId == semesterId &&
                            x.ClassSectionId == classSectionId && x.StudyLevelTermId == 1 && !x.IsDeleted);

            if (isAlreadyCourseOffered)
                return true;

            //If no course is then offer course from curriculum

            IQueryable<Aca_CurriculumCourse> query = DbInstance.Aca_CurriculumCourse.Where(x =>
                    x.CurriculumId == curriculumId);

            var curriculumCourseList = query.ToList();

            //var departmentId = DbInstance.Aca_Program.Where(x => x.Id == programId).Select(x => x.DepartmentId)
            //    .FirstOrDefault();

            foreach (var curriculumCourse in curriculumCourseList)
            {
                var markDistributionList = GetMarkDistributionPolicyList(curriculumCourse.CreditHour,
                    curriculumCourse.CourseCategoryEnumId, semesterId, programId, true);

                //if (markDistributionList.Count <= 0)
                //{
                //    error += $"No Mark Distribution Found For {curriculumCourse.Name}. Please create a Mark Distribution First.";
                //}

                var entity = Aca_Class.GetNew();
                entity.CurriculumCourseId = curriculumCourse.Id;
                entity.SemesterId = semesterId;
                entity.StudyLevelTermId = 1;
                entity.StudentBatchId = null;

                if (markDistributionList.Count > 0)
                {
                    entity.RegularExamMarkDistributionPolicyId = Convert.ToInt64(markDistributionList[0].Id);
                }
                else
                {
                    entity.RegularExamMarkDistributionPolicyId = 0;
                }

                entity.RegularCapacity = (short)SiteSettings.Instance.DefaultClassSectionRange;
                entity.ProgramId = programId;
                entity.DepartmentId = departmentId;
                entity.Code = curriculumCourse.Code;

                entity.ClassSectionId = classSectionId;

                entity.History += $"Class was Offered While Admission, by {HttpUtil.Profile.Name}[{HttpUtil.Profile.UserName}] at {DateTime.Now}, IP:{HttpUtil.GetUserIp()}<br>"; ;

                if (!SaveClass(entity, out error, scope))
                {
                    return false;
                }
            }

            return true;
        }

        public bool SaveClass(Aca_Class classObj, out string error, DbContextTransaction scope = null)
        {
            error = string.Empty;
            try
            {
                var dbAttachedEntity = new Aca_Class();
                bool isNeedToCommit = false;

                if (scope==null)
                {
                    scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted);
                    isNeedToCommit = true;
                }

                if (SaveClassLogic(classObj, ref dbAttachedEntity, out error))
                {
                    if (isNeedToCommit)
                    {
                        DbInstance.SaveChanges();
                        scope.Commit();
                    }
                    
                    classObj = dbAttachedEntity;
                    return true;
                }
                else
                {
                    return false;
                }
                
            }

            catch (Exception ex)
            {
                error=ex.GetBaseException().Message.ToString();
                return false;
            }
            
        }

        public bool SaveClass2(Aca_Class classObj, out string error)
        {
            //check permission
            error = string.Empty;
            if (ValidateClass(classObj, out error))
            {
                using (var scope = DbInstance.Database.BeginTransaction())
                {
                    try
                    {
                        bool isNewObject = true;
                        Aca_Class objToSave = null;
                        if (classObj.Id != 0)
                        {
                            objToSave = DbInstance.Aca_Class.SingleOrDefault(x => x.Id == classObj.Id);
                            isNewObject = false;
                        }
                        if (objToSave == null)
                        {
                            isNewObject = true;
                            objToSave = Aca_Class.GetNew(BigInt.NewBigInt());
                            DbInstance.Aca_Class.Add(objToSave);
                            objToSave.CreateDate = classObj.CreateDate = DateTime.Now;
                            objToSave.CreateById = classObj.CreateById = Profile.UserId;
                        }
                        CopyUtil.Copy(classObj, objToSave);
                        objToSave.LastChanged = classObj.LastChanged = DateTime.Now;
                        objToSave.LastChangedById = classObj.LastChangedById = Profile.UserId;
                        DbInstance.SaveChanges();
                        scope.Commit();
                        if (isNewObject)
                        {
                            classObj.Id = objToSave.Id;
                        }
                        return true;
                    }
                    catch (Exception ex)
                    {
                        error = ex.Message;
                        scope.Rollback();
                        return false;
                    }
                }

            }

            return false;
        }

        private bool SaveClassLogic(Aca_Class newObj, ref Aca_Class dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Class to save can't be null!";
                return false;
            }

            if (!ValidateClass(newObj, out error))
                return false;

            bool isNewObject = true;
            Aca_Class objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Aca_Class.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            else
            {
                newObj.Id = BigInt.NewBigInt();
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Aca_Class.GetNew(newObj.Id);
                DbInstance.Aca_Class.Add(objToSave);
                objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
                objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            
            //binding object  
            objToSave.ProgramId = newObj.ProgramId;
            objToSave.DepartmentId = newObj.DepartmentId;
            objToSave.CurriculumCourseId = newObj.CurriculumCourseId;
            objToSave.Code = newObj.Code;
            objToSave.Name = newObj.Name;
            objToSave.ClassNo = newObj.ClassNo;
            objToSave.ClassSectionId = newObj.ClassSectionId;
            objToSave.StudyLevelTermId = newObj.StudyLevelTermId;
           

            // Semester Change
            if (!isNewObject && objToSave.SemesterId != newObj.SemesterId)
            {
                if (!IsClassSemesterChange(objToSave, newObj,out error))
                {
                    return false;
                }
                objToSave.History += $"{DateTime.Now} Semester changed, SemesterId:{objToSave.SemesterId} to {newObj.SemesterId}, by {HttpUtil.Profile.Name}[{HttpUtil.Profile.UserName}], IP:{HttpUtil.GetUserIp()}<br>";
            }
            objToSave.SemesterId = newObj.SemesterId;
            objToSave.ProgramTypeEnumId = newObj.ProgramTypeEnumId;
            objToSave.RegularCapacity = newObj.RegularCapacity;
            objToSave.StatusEnumId = newObj.StatusEnumId;
            objToSave.CampusId = newObj.CampusId;
            objToSave.RegularExamMarkDistributionPolicyId = newObj.RegularExamMarkDistributionPolicyId;
            objToSave.ReferredExamMarkDistributionPolicyId = newObj.ReferredExamMarkDistributionPolicyId;
            objToSave.BacklogExamMarkDistributionPolicyId = newObj.BacklogExamMarkDistributionPolicyId;
            objToSave.StudentBatchId = newObj.StudentBatchId;
            objToSave.IsDeleted = newObj.IsDeleted;
            objToSave.IsDasbleOnlineRegistration = newObj.IsDasbleOnlineRegistration;
            //objToSave.History = newObj.History;
            objToSave.IsMidTermHardCopySubmitted = newObj.IsMidTermHardCopySubmitted;
            objToSave.IsFinalTermHardCopySubmitted = newObj.IsFinalTermHardCopySubmitted;
            objToSave.IsBlockStudentPanelResult = newObj.IsBlockStudentPanelResult;
            objToSave.IsSpecialUnlock = newObj.IsSpecialUnlock;
            objToSave.UnlockExpiryDate = newObj.UnlockExpiryDate;
            //objToSave.LockUnlockHistory = newObj.LockUnlockHistory;
            objToSave.FacultyCanAddRoutine = newObj.FacultyCanAddRoutine;
            objToSave.FacultyCanEditRoutine = newObj.FacultyCanEditRoutine;
            objToSave.LastChangedById = newObj.LastChangedById = HttpUtil.Profile.UserId;
            objToSave.LastChanged = newObj.LastChanged = DateTime.Now;
            dbAddedObj = objToSave;

            //here save any child table
            return true;
        }

        private bool IsClassSemesterChange(Aca_Class dbClassObj, Aca_Class updateClassObj,out string error)
        {
            error=String.Empty;
            /*
             Aca_Class=> SemesterId (Done)
            Aca_ResultClass=> SemesterId,ExamId (Done)
            Aca_ResultComponent=> ExamId (Done)
           
            Aca_ResultClassSetting=> ExamId (Done)
            Aca_ResultComponentSetting=>ExamId (Done)
             Aca_ResultComponentBreakdownSetting=>ExamId (Done)
             */
            var dbClassExamId = DbInstance.Aca_Exam.SingleOrDefault(x => x.SemesterId == dbClassObj.SemesterId
                                                                         && x.ExamTypeEnumId ==
                                                                         (byte) Aca_Exam.ExamTypeEnum.FinalTerm)?.Id;

            if (dbClassExamId==null)
            {
                error = $"Final Term Exam not Found by SemesterId:{dbClassObj.SemesterId}";
                return false;
            }

            var updateClassExam = DbInstance.Aca_Exam.SingleOrDefault(x => x.SemesterId == updateClassObj.SemesterId
                                                                         && x.ExamTypeEnumId ==
                                                                         (byte)Aca_Exam.ExamTypeEnum.FinalTerm);

            if (updateClassExam == null)
            {
                error = $"Final Term Exam not Found by SemesterId:{updateClassObj.SemesterId}";
                return false;
            }

            //Aca_ResultClass
            /*var allResultClassForThisClass = DbInstance.Aca_ResultClass.Where(x => x.ClassId == dbClassObj.Id).ToList();
            if (allResultClassForThisClass.Any(x=>x.ExamId!=dbClassExamId))
            {
                error = $"Semester change not possible, because {SiteSettings.Instance.SuppleExamViewName} class result found.";
                return false;
            }*/

            //Aca_ResultComponent
            /*var allResultComponentForThisClass = DbInstance.Aca_ResultComponent.Where(x => x.ClassId == dbClassObj.Id).ToList();
            if (allResultComponentForThisClass.Any(x => x.ExamId != dbClassExamId))
            {
                error = $"Semester change not possible, because {SiteSettings.Instance.SuppleExamViewName} component result found.";
                return false;
            }*/

            //Aca_ResultClassSetting
            //var resultClassSettingList = DbInstance.Aca_ResultClassSetting.Where(x => x.ClassId == dbClassObj.Id).ToList();

            //Aca_ResultComponentSetting
            //var resultComponentSettingList = DbInstance.Aca_ResultComponentSetting.Where(x => x.ClassId == dbClassObj.Id).ToList();

            //Aca_ResultComponentBreakdownSetting
            //var resultComponentBreakdownSettingList = DbInstance.Aca_ResultComponentBreakdownSetting.Where(x => x.ClassId == dbClassObj.Id).ToList();

            // Aca_ResultClass=> SemesterId,ExamId Update
            /*foreach (var resultClass in allResultClassForThisClass)
            {
                resultClass.SemesterId = updateClassObj.SemesterId;
                resultClass.ExamId = updateClassExam.Id;
            }*/

            //Aca_ResultComponent=> ExamId
            /*foreach (var resultComponent in allResultComponentForThisClass)
            {
                resultComponent.ExamId = updateClassExam.Id;
            }*/

            //Aca_ResultClassSetting => ExamId
            /*foreach (var resultClassSetting in resultClassSettingList)
            {
                resultClassSetting.ExamId = updateClassExam.Id;
            }*/

            //Aca_ResultComponentSetting => ExamId
            /*foreach (var resultComponentSetting in resultComponentSettingList)
            {
                resultComponentSetting.ExamId = updateClassExam.Id;
            }*/
            //Aca_ResultComponentBreakdownSetting => ExamId
            /*foreach (var resultComponentBreakdownSetting in resultComponentBreakdownSettingList)
            {
                resultComponentBreakdownSetting.ExamId = updateClassExam.Id;
            }*/

            return true;
        }


        public List<Aca_MarkDistributionPolicyJson> GetMarkDistributionPolicyList(float creditHour = 0, int courseCategoryEnumId = -1, long semesterId = 0, int? programId = null, bool isNewCreateMode = false)
        {
            var markDistributionPolicyJsonList = new List<Aca_MarkDistributionPolicyJson>();

            // Specific Program wise Start
            IQueryable<Aca_MarkDistributionPolicy> queryProgramWise = DbInstance.Aca_MarkDistributionPolicy
                .Where(x => x.CreditHour == creditHour
                            && x.CourseCategoryEnumId == courseCategoryEnumId
                            && x.StartSemesterId <= semesterId
                            && x.ProgramId == programId);

            if (isNewCreateMode)
            {
                queryProgramWise = queryProgramWise.Where(x =>
                    x.StatusEnumId == (byte)Aca_MarkDistributionPolicy.StatusEnum.Active);
            }

            var markDistributionPolicyList = queryProgramWise.ToList();

            markDistributionPolicyList =
                markDistributionPolicyList.Where(x => x.EndSemesterId >= semesterId || x.EndSemesterId == null)
                    .ToList();

            if (markDistributionPolicyList.Count > 0)
            {
                markDistributionPolicyList.Map(markDistributionPolicyJsonList);

                return markDistributionPolicyJsonList;
            }
            // Specific Program wise End


            // All Program wise Start
            IQueryable<Aca_MarkDistributionPolicy> queryAllProgram = DbInstance.Aca_MarkDistributionPolicy
                .Where(x => x.CreditHour == creditHour
                            && x.CourseCategoryEnumId == courseCategoryEnumId
                            && x.ProgramId == null
                            && x.StartSemesterId <= semesterId);

            if (isNewCreateMode)
            {
                queryAllProgram = queryAllProgram.Where(x =>
                    x.StatusEnumId == (byte)Aca_MarkDistributionPolicy.StatusEnum.Active);
            }


            markDistributionPolicyList = queryAllProgram.ToList();

            markDistributionPolicyList =
                markDistributionPolicyList.Where(x => x.EndSemesterId >= semesterId || x.EndSemesterId == null)
                    .ToList();

            if (markDistributionPolicyList.Count > 0)
            {
                markDistributionPolicyList.Map(markDistributionPolicyJsonList);
            }

            return markDistributionPolicyJsonList;
        }
        #endregion Class

        #region Class Students
        public List<Aca_Semester> GetSemesterLevelTermListByStudentId(long studentId)
        {
            List<Aca_ClassTakenByStudent> classTakenList = DbInstance.Aca_ClassTakenByStudent
                .Include(x => x.Aca_Class.Aca_Semester)
                .Include(x => x.Aca_Class.Aca_StudyLevelTerm)
                //.Include(x => x.Aca_Class.Aca_SemesterLevelTerm.Aca_Semester)
                .OrderByDescending(x => x.Aca_Class.Aca_Semester.Year)
                .ThenBy(x => x.Aca_Class.Aca_StudyLevelTerm.LevelId)
                .ThenByDescending(x => x.Aca_Class.Aca_StudyLevelTerm.TermId)
                .Where(x => x.StudentId == studentId).ToList();

            List<Aca_Semester> semesterList = new List<Aca_Semester>();
            if (classTakenList.Count > 0)
            {
                foreach (var classTaken in classTakenList)
                {
                    if (classTaken.Aca_Class?.Aca_Semester != null)
                    {
                        //making order of class list
                        //classTaken.Aca_Class.Aca_Semester.Aca_Class = classTaken.Aca_Class.Aca_Semester.Aca_Class.OrderBy(x => x.HR_Department.ShortName).ThenBy(x => x.Code).ToList();
                        var classToAdd = semesterList.SingleOrDefault(x => x.Id == classTaken.Aca_Class.Aca_Semester.Id);
                        if (classToAdd == null)
                        {
                            semesterList.Add(classTaken.Aca_Class.Aca_Semester);
                        }
                    }
                }
                semesterList = semesterList.OrderByDescending(x => x.Year).ThenByDescending(x => x.TermId).ToList();   //
            }
            return semesterList;
        }
        /// <summary>
        /// used in student dashboard, included all;
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="SemesterId"></param>
        /// <returns></returns>
        public List<Aca_Class> GetClassListByStudentIdSemesterId(long studentId, long SemesterId)
        {
            List<Aca_ClassTakenByStudent> classTakenList = DbInstance.Aca_ClassTakenByStudent
                .Include(x => x.Aca_Class.HR_Department)
                .Include(x => x.Aca_Class.Aca_ClassRoutine)
                .Include("Aca_Class.Aca_ClassRoutine.General_Room")
                .Include("Aca_Class.Aca_ClassRoutine.General_Room.General_Floor")
                .Include("Aca_Class.Aca_ClassRoutine.General_Room.General_Building")
                .Include("Aca_Class.Aca_ClassRoutine.General_Room.General_Building.General_Campus")
                .Include(x => x.Aca_Class.Aca_ClassSection)
                .Include(x => x.Aca_Class.Aca_Semester)
                .Include(x => x.Aca_Class.Aca_CurriculumCourse)
                //.Include(x => x.Aca_Class.Aca_SemesterLevelTerm)
                //.Include(x => x.Aca_Class.Aca_SemesterLevelTerm.Aca_Semester)
                .Include(x => x.Aca_Class.Aca_StudyLevelTerm.Aca_StudyLevel)

                .Where(x => x.StudentId == studentId 
                            && x.Aca_Class.SemesterId == SemesterId).ToList();

            List<Aca_Class> classList = new List<Aca_Class>();
            if (classTakenList.Count > 0)
            {
                foreach (var classTaken in classTakenList)
                {
                    if (classTaken.Aca_Class != null && classTaken.Aca_Class.Aca_Semester != null)
                    {
                        //making order of class list
                        var classToAdd = classList.SingleOrDefault(x => x.Id == classTaken.Aca_Class.Id);
                        if (classToAdd == null)
                        {
                            classToAdd = classTaken.Aca_Class;
                            classList.Add(classToAdd);
                            classToAdd.ClassTakenByStudentHistory = classTaken.History;

                            classToAdd.IsSelected = true;
                            classToAdd.StudentRegistrationStatus = classTaken.RegistrationStatus;
                            classToAdd.StudentRegistrationStatusEnumId = classTaken.RegistrationStatusEnumId;
                            classToAdd.StudentStatus = classTaken.Status;

                            if (classTaken.RegistrationStatus != Aca_ClassTakenByStudent.RegistrationStatusEnum.FreshRegistration || classTaken.Status != Aca_ClassTakenByStudent.StatusEnum.Valid)
                            {
                                classToAdd.IsSelected = false;
                            }
                            //adding faculty Name
                            classToAdd.TeacherNameList = new List<string>();
                            var teacherList = DbInstance.Aca_ClassTakenByEmployee
                                //.Include(x => x.User_Employee)
                                //.Include(x => x.User_Employee.User_Account)
                                .Where(x => x.ClassId == classTaken.ClassId
                                            && x.RoleEnumId == (byte)Aca_ClassTakenByEmployee.RoleEnum.Faculty
                                            && x.EmployeeId != 1
                                            && !x.IsDeleted)
                                .Select(x => x.User_Employee.User_Account.FullName);
                            // GetTeacherListByClassIdByRoleEnumId(classTaken.ClassId, (byte)Aca_ClassTakenByEmployee.RoleEnum.Faculty);
                            if (teacherList.Any())
                            {
                                classToAdd.TeacherNameList.AddRange(teacherList.OrderBy(x => x)); //
                            }
                        }

                    }
                }
                if (classList.Count > 0)
                {
                    classList = classList.OrderBy(x => x.HR_Department.ShortName).ThenBy(x => x.Aca_StudyLevelTerm.LevelId).ThenBy(x => x.Code).ToList();
                }
            }
            return classList;
        }
        public List<Aca_ClassTakenByStudent> GetStudentListByClassId(long classId)
        {
            List<Aca_ClassTakenByStudent> classStudentMapList = DbInstance.Aca_ClassTakenByStudent
                                                        .Include(x => x.User_Student)
                                                        .Include(x => x.User_Student.User_Account)
                                                        .Where(x => x.ClassId == classId)
                                                        .OrderBy(x => x.User_Student.ClassRollNo)
                                                        .ToList();

            return classStudentMapList;
        }
        public List<Aca_ClassWaiverStudent> GetWaiverStudentListByClassId(long classId)
        {
            List<Aca_ClassWaiverStudent> classWaiverStudenList = DbInstance.Aca_ClassWaiverStudent
                                                        .Include(x => x.User_Student)
                                                        .Include(x => x.User_Student.User_Account)
                                                        .Where(x => x.ClassId == classId)
                                                        .OrderBy(x => x.User_Student.ClassRollNo)
                                                        .ToList();

            return classWaiverStudenList;
        }
        public List<User_Student> GetStudentListByClassIdByEnrollTypeId(long classId, byte enrollTypeEnumId)
        {
            List<Aca_ClassTakenByStudent> classStudentMapList = DbInstance.Aca_ClassTakenByStudent
                                                        .Include(x => x.User_Student)
                                                        .Include(x => x.User_Student.User_Account)
                                                        .Where(x => x.ClassId == classId
                                                        && x.EnrollTypeEnumId == enrollTypeEnumId)
                                                        .OrderBy(x => x.User_Student.ClassRollNo).ToList();

            var studentList = classStudentMapList.Select(classStudentMap => classStudentMap.User_Student).ToList();

            //List<User_Student> studentList = DbInstance.Aca_ClassTakenByStudent
            //                                            .Include(x => x.User_Student)
            //                                            .Include(x=>x.User_Student.User_Account)
            //                                            .Where(x => x.ClassId == classId)
            //                                            .OrderBy(x => x.StudentId)
            //                                            .Select(classStudentMap => classStudentMap.User_Student)
            //                                            .ToList();
            return studentList;
        }
        public List<User_Student> GetStudentWaiverListByClassId(long classId)
        {
            List<Aca_ClassWaiverStudent> classWaiverStudentList = DbInstance.Aca_ClassWaiverStudent
                                                        .Include(x => x.User_Student)
                                                        .Include(x => x.User_Student.User_Account)
                                                        .Where(x => x.ClassId == classId)
                                                        .OrderBy(x => x.User_Student.ClassRollNo).ToList();

            var studentList = classWaiverStudentList.Select(cws => cws.User_Student).ToList();
            return studentList;
        }
        public List<User_Student> GetStudentList(long deptId, int levelTermId, int termId)
        {
            List<User_Student> studentList = DbInstance.User_Student
                                                        .Include(x => x.Aca_ClassTakenByStudent)
                                                        .Include(x => x.User_Account)
                                                        .Where(x => x.User_Account.DepartmentId == deptId
                                                        && x.LevelTermId == levelTermId
                                                        //&& x.TermId == termId
                                                        )
                                                        .OrderBy(x => x.User_Account.UserName).ToList();
            return studentList;
        }
        public List<User_Student> GetStudentListByClassRoll(int classRoll)
        {
            List<User_Student> studentList = DbInstance.User_Student
                                                        .Include(x => x.Aca_ClassTakenByStudent)
                                                        .Include(x => x.User_Account)
                                                        .Where(x => x.ClassRollNo == classRoll.ToString())
                                                        .OrderBy(x => x.User_Account.UserName).ToList();
            return studentList;
        }
        public List<User_Student> GetStudentGraduatedList(long deptId, int levelId, int termId)
        {
            List<User_Student> studentList = DbInstance.User_Student
                                                        .Include(x => x.Aca_ClassTakenByStudent)
                                                        .Include(x => x.User_Account)
                                                        .Where(x => x.User_Account.DepartmentId == deptId
                                                        && x.LevelTermId == levelId
                                                        //&& x.TermId == termId
                                                        && x.EnrollmentStatusEnumId == (byte)User_Student.EnrollmentStatusEnum.Graduated)
                                                        .OrderBy(x => x.User_Account.UserName).ToList();
            return studentList;
        }
        public List<User_Student> GetStudentNotGraduatedList(long deptId, int levelId, int termId, int campusId)
        {
            List<User_Student> studentList = DbInstance.User_Student
                                                        .Include(x => x.Aca_ClassTakenByStudent)
                                                        .Include(x => x.User_Account)
                                                        .Where(x => x.User_Account.DepartmentId == deptId
                                                        && x.User_Account.CampusId == campusId
                                                         && x.LevelTermId == levelId
                                                        //&& x.TermId == termId
                                                        && x.EnrollmentStatusEnumId != (byte)User_Student.EnrollmentStatusEnum.Graduated)
                                                        .OrderBy(x => x.User_Account.UserName).ToList();
            return studentList;
        }

        public List<User_Student> GetStudentNotGraduatedPagedList(long deptId, int levelTermId, int termId, int campusId, int? pageSize, int? pageNo, ref int count)
        {
            IQueryable<User_Student> query = DbInstance.User_Student
                                                        .Include(x => x.Aca_ClassTakenByStudent)
                                                        .Include(x => x.User_Account)
                                                        .Where(x => x.User_Account.DepartmentId == deptId
                                                        && x.User_Account.CampusId == campusId
                                                        && x.LevelTermId == levelTermId
                                                        //&& x.TermId == termId
                                                        && x.EnrollmentStatusEnumId != (byte)User_Student.EnrollmentStatusEnum.Graduated)
                                                        .OrderBy(x => x.User_Account.UserName);



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
        public List<User_Student> GetStudentContinuingList(long deptId, int levelTermId, int termId)
        {
            List<User_Student> studentList = DbInstance.User_Student
                                                        .Include(x => x.Aca_ClassTakenByStudent)
                                                        .Include(x => x.User_Account)
                                                        .Where(x => x.User_Account.DepartmentId == deptId
                                                        && x.LevelTermId == levelTermId
                                                        //&& x.TermId == termId
                                                        && x.EnrollmentStatusEnumId == (byte)User_Student.EnrollmentStatusEnum.Continuing)
                                                        .OrderBy(x => x.User_Account.UserName).ToList();
            return studentList;
        }
        public List<Aca_ClassTakenByStudent> GetClassStudentMapList(long classId, int? pageSize, int? pageNo, ref int count)
        {
            IQueryable<Aca_ClassTakenByStudent> query = (from q in DbInstance.Aca_ClassTakenByStudent
                                                         select q)
                                                     .Include(x => x.User_Student)
                                                     .Where(x => x.ClassId == classId)
                                                     .OrderBy(x => x.StudentId);
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

        public bool SaveClassStudentList(List<User_Student> studentList, long classId, byte enrollTypeEnumId, byte registrationStatusEnumId, byte statusEnumId, out string error)
        {
            //check permission
            var objClass = DbInstance.Aca_Class.SingleOrDefault(x => x.Id == classId);
            var enrolledStudentList = DbInstance.Aca_ClassTakenByStudent
                .Where(x => x.ClassId == classId && x.EnrollTypeEnumId == enrollTypeEnumId)
                .ToList();
            if (objClass == null)
            {
                error = "Invalid Class!";
                return false;
            }
            if (studentList == null || studentList.Count == 0)
            {
                error = "Select Student for this class.";
                return false;
            }
            if (enrollTypeEnumId == (byte)Aca_ClassTakenByStudent.EnrollTypeEnum.Regular
                && objClass.RegularCapacity < (studentList.Count + enrolledStudentList.Count)
            )
            {
                error = "Class Regular Capacity Exists!";
                return false;
            }

            var regSemester = DbInstance.Aca_Semester.Find(objClass.SemesterId);

            if (regSemester == null)
            {
                error = "There is no valid semester Associated with this Class!";
                return false;
            }
            //TODO Check for validation
            error = string.Empty;
            using (var scope = DbInstance.Database.BeginTransaction())
            {
                try
                {
                    foreach (var userStudent in studentList)
                    {
                        if (userStudent.CurriculumId == objClass.CurriculumCourseId)
                        {
                            error = "Selected Students and this Course's Curriculum is different.";
                            scope.Rollback();
                            return false;
                        }
                        if (enrollTypeEnumId == (byte)Aca_ClassTakenByStudent.EnrollTypeEnum.Referred
                            && DbInstance.Aca_ClassTakenByStudent
                            .Any(x => x.Id == objClass.Id
                            && x.EnrollTypeEnumId == (byte)Aca_ClassTakenByStudent.EnrollTypeEnum.Regular
                            && x.StudentId == userStudent.Id))
                        {
                            error = "Selected Students are not enrolled as a regular student in this Course.";
                            scope.Rollback();
                            return false;
                        }
                        if (enrollTypeEnumId == (byte)Aca_ClassTakenByStudent.EnrollTypeEnum.Backlog
                            && DbInstance.Aca_ClassTakenByStudent
                            .Any(x => x.Id == objClass.Id
                            && x.EnrollTypeEnumId == (byte)Aca_ClassTakenByStudent.EnrollTypeEnum.Referred
                            && x.StudentId == userStudent.Id))
                        {
                            error = "Selected Students are not enrolled as a regular & referred student in this Course.";
                            scope.Rollback();
                            return false;
                        }
                        //todo check is already taken in same semester
                        var isTaken = DbInstance.Aca_ClassTakenByStudent
                             .Any(x => x.Aca_Class.CurriculumCourseId == objClass.CurriculumCourseId
                             
                             && x.Aca_Class.SemesterId == objClass.SemesterId
                             && x.StudentId == userStudent.Id);
                        if (isTaken)
                        {
                            //msg = ;
                            error = $"Selected Student({userStudent.ClassRollNo}) already taken Another Section of this Course!";
                            scope.Rollback();
                            return false;

                        }
                        //todo add check is already taken in other semester or retakable

                        var isRetakeable = false;
                        //var isCompleted = IsCourseAlreadyTakenInOtherSemester(objClass, userStudent, regSemester, ref isRetakeable, out error);
                        //if (isCompleted)
                        //{
                        //    //msg = ;
                        //    error = $"Selected Student({userStudent.ClassRollNo}) Already Taken this Course in Other Semester, to Add Him In this Course First Drop or Cancel the Previous Registration!";
                        //    scope.Rollback();
                        //    return false;

                        //}


                        bool isNewObject = true;
                        Aca_ClassTakenByStudent objToSave = DbInstance.Aca_ClassTakenByStudent
                            .SingleOrDefault(
                                x => x.ClassId == objClass.Id
                                && x.StudentId == userStudent.Id
                                && x.EnrollTypeEnumId == enrollTypeEnumId
                                && x.RegistrationStatusEnumId == registrationStatusEnumId
                                && x.StatusEnumId == statusEnumId
                            );
                        if (objToSave == null)
                        {
                            objToSave = Aca_ClassTakenByStudent.GetNew(BigInt.NewBigInt());

                            objToSave.ClassId = classId;
                            objToSave.StudentId = userStudent.Id;

                            DbInstance.Aca_ClassTakenByStudent.Add(objToSave);
                            objToSave.CreateDate = DateTime.Now;
                            objToSave.CreateById = Profile.UserId;
                        }
                        objToSave.EnrollTypeEnumId = enrollTypeEnumId;
                        objToSave.RegistrationStatusEnumId = registrationStatusEnumId;
                        objToSave.StatusEnumId = statusEnumId;

                        objToSave.LastChanged = DateTime.Now;
                        objToSave.LastChangedById = Profile.UserId;


                        DbInstance.SaveChanges();
                        //todo update payment for this semester regenerate 


                    }

                    scope.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    scope.Rollback();
                    return false;
                }
            }
        }
        private bool SaveStudentClassList(
            List<Aca_Class> classList
            , long userId
            , byte enrollTypeEnumId
            , byte registrationStatusEnumId
            , byte statusEnumId
            , out string error
        )
        {
            //check permission
            var student = DbInstance.User_Student.SingleOrDefault(x => x.UserId == userId);
            if (student == null)
            {
                error = "Invalid Student!";
                return false;
            }
            if (classList == null || classList.Count == 0)
            {
                error = "Select Student for this class.";
                return false;
            }
            //TODO Check for validation
            error = string.Empty;
            using (var scope = DbInstance.Database.BeginTransaction())
            {
                try
                {
                    foreach (var objClass in classList)
                    {
                        bool isNewObject = true;
                        Aca_ClassTakenByStudent objToSave = DbInstance.Aca_ClassTakenByStudent
                            .SingleOrDefault(
                                x => x.StudentId == student.Id
                                && x.ClassId == objClass.Id
                                && x.EnrollTypeEnumId == enrollTypeEnumId
                                && x.RegistrationStatusEnumId == registrationStatusEnumId
                                && x.StatusEnumId == statusEnumId
                            );
                        if (objToSave == null)
                        {
                            objToSave = Aca_ClassTakenByStudent.GetNew(BigInt.NewBigInt());

                            objToSave.ClassId = objClass.Id;
                            objToSave.StudentId = student.Id;

                            DbInstance.Aca_ClassTakenByStudent.Add(objToSave);
                            objToSave.CreateDate = DateTime.Now;
                            objToSave.CreateById = Profile.UserId;
                        }
                        objToSave.EnrollTypeEnumId = enrollTypeEnumId;
                        objToSave.RegistrationStatusEnumId = registrationStatusEnumId;
                        objToSave.StatusEnumId = statusEnumId;

                        objToSave.IsAlreadyAdded = true;

                        objToSave.LastChanged = DateTime.Now;
                        objToSave.LastChangedById = Profile.UserId;
                    }
                    DbInstance.SaveChanges();
                    scope.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    scope.Rollback();
                    return false;
                }
            }
        }
        public bool SaveClassWaiverStudentList(List<User_Student> studentList, long classId, out string error)
        {
            //check permission
            var Class = DbInstance.Aca_Class.SingleOrDefault(x => x.Id == classId);
            //var enrolledStudentList = DbInstance.Aca_ClassWaiverStudent.Where(x => x.ClassId == classId).ToList();
            if (Class == null)
            {
                error = "Invalid Class!";
                return false;
            }
            if (studentList == null || studentList.Count == 0)
            {
                error = "Select Student for this class.";
                return false;
            }
            //if (Class.RegularCapacity < (studentList.Count + enrolledStudentList.Count))
            //{
            //    error = "Class Regular Capacity Exists!";
            //    return false;
            //}
            //TODO Check for validation
            error = string.Empty;
            using (var scope = DbInstance.Database.BeginTransaction())
            {
                try
                {
                    foreach (var userStudent in studentList)
                    {
                        if (DbInstance.Aca_ClassTakenByStudent.Any(x => x.Id == Class.Id && x.StudentId == userStudent.Id))
                        {
                            error = "Selected Student are already enrolled in this class for Regular/Referred/Backlog.";
                            scope.Rollback();
                            return false;
                        }
                        bool isNewObject = true;
                        Aca_ClassWaiverStudent objToSave = DbInstance.Aca_ClassWaiverStudent.SingleOrDefault(
                                x => x.Id == Class.Id
                                && x.StudentId == userStudent.Id
                                );
                        if (objToSave == null)
                        {
                            objToSave = Aca_ClassWaiverStudent.GetNew(BigInt.NewBigInt());

                            objToSave.ClassId = classId;
                            objToSave.StudentId = userStudent.Id;

                            DbInstance.Aca_ClassWaiverStudent.Add(objToSave);
                            objToSave.CreateDate = DateTime.Now;
                            objToSave.CreateById = Profile.UserId;
                        }

                        objToSave.LastChanged = DateTime.Now;
                        objToSave.LastChangedById = Profile.UserId;
                    }
                    DbInstance.SaveChanges();
                    scope.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    scope.Rollback();
                    return false;
                }
            }
        }
        public bool SaveClassWaiverStudentList(List<Aca_ClassWaiverStudent> entityListReceived, out string error)
        {
            if (entityListReceived == null || entityListReceived.Count == 0)
            {
                error = "Select Student for this class.";
                return false;
            }
            //TODO Check for validation
            error = string.Empty;
            try
            {
                var classId = entityListReceived.First().ClassId;
                var gradingPolicy = DbInstance.Aca_Class
                    .Include(x => x.Aca_CurriculumCourse)
                    .Include(x => x.Aca_CurriculumCourse.Aca_Curriculum)
                    .Where(x => x.Id == classId)
                    .Select(x => x.Aca_CurriculumCourse.Aca_Curriculum)
                    .First();
                if (gradingPolicy == null)
                {
                    error = "There is no Grading Policy in this course curriculum.";
                    return false;
                }

                using (Aca_ClassWaiverStudentDataService classWaiverStudentDataService = new Aca_ClassWaiverStudentDataService(DbInstance, Profile))
                {
                    foreach (var entity in entityListReceived)
                    {
                        /*ClassResultFacade classResultFacade = new ClassResultFacade(DbInstance, Profile);
                        var gpo = classResultFacade.GetGradeByMark(entity.Mark, gradingPolicy.GradingPolicyId, out error);
                        if (gpo == null)
                        {
                            return false;
                        }
                        entity.GradePoint = gpo.GradePoint;
                        entity.Grade = gpo.Grade;
                        classWaiverStudentDataService.Save(entity, out error);*/
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
        public bool PermanentDeleteStudentFromClass(Aca_ClassTakenByStudent entity, out string error)
        {
            //check permission
            //TODO Check for validation
            error = string.Empty;
            Aca_ClassTakenByStudent result = DbInstance.Aca_ClassTakenByStudent
                .Include(x=>x.Aca_Class)
                .SingleOrDefault(x => x.Id == entity.Id);
            //var classObj= DbInstance.Aca_ClassTakenByStudent.SingleOrDefault(x => x.Id == entity.Id);

            if (result != null)
            {
                try
                {
                    using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                    {
                        var semesterId = result.Aca_Class.SemesterId;
                        var studentId = result.StudentId;
                        DbInstance.Aca_ClassTakenByStudent.Remove(result);
                        DbInstance.SaveChanges();


                        /*StudentPaymentFacade facade = new StudentPaymentFacade(DbInstance, HttpUtil.Profile);

                        if (!facade.GenerateSemesterFeesThisToForward(studentId, null, semesterId,null, null, out error))
                        {

                            scope.Rollback();
                            return false;
                        }*/
                        DbInstance.SaveChanges();
                        scope.Commit();

                        return true;
                    }
                }
                catch (Exception ex)
                {
                    error = ex.GetBaseException().ToString();
                    return false;
                }
            }
            return false;
        }
        public bool DeleteClassAllStudent(List<Aca_ClassTakenByStudent> entities, out string error)
        {
            //check permission
            //TODO Check for validation
            error = string.Empty;
            if (entities.Count > 0)
            {
                try
                {

                    using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                    {

                        foreach (var entity in entities)
                        {
                            Aca_ClassTakenByStudent result =
                                DbInstance.Aca_ClassTakenByStudent
                                .Include(x => x.Aca_Class)
                                .SingleOrDefault(x => x.Id == entity.Id);
                            if (result==null)
                            {
                              continue;  
                            }
                            var semesterId = result.Aca_Class.SemesterId;
                            var studentId = result.StudentId;
                            DbInstance.Aca_ClassTakenByStudent.Remove(result);
                            DbInstance.SaveChanges();
                            /*StudentPaymentFacade facade = new StudentPaymentFacade(DbInstance, HttpUtil.Profile);

                            if (!facade.GenerateSemesterFeesThisToForward(studentId, null, semesterId,null, null, out error))
                            {

                                scope.Rollback();
                                return false;
                            }*/
                           


                        }
                        DbInstance.SaveChanges();
                        scope.Commit();
                        return true;
                    }

                  
                }
                catch (Exception ex)
                {
                    error = ex.GetBaseException().ToString();
                    return false;
                }
            }
            else
            {
                error += "No Students found in this Offered Course";
            }
            return false;
        }
        public bool DeleteClassWaiverStudent(Aca_ClassWaiverStudent entity, out string error)
        {
            //check permission
            //TODO Check for validation
            error = string.Empty;
            Aca_ClassWaiverStudent result = DbInstance.Aca_ClassWaiverStudent.SingleOrDefault(x => x.Id == entity.Id);
            if (result != null)
            {
                try
                {
                    DbInstance.Aca_ClassWaiverStudent.Remove(result);
                    DbInstance.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    return false;
                }
            }
            return false;
        }

        #endregion Class Students


        #region Regiatration And Payment
        //public bool IsCourseAlreadyTakenInOtherSemester(Aca_Class courseToAdd, User_Student student, Aca_Semester regSemester, ref bool isRetakeable, out string error)
        //{
        //    //get classtakenbystudent
        //    //get class result


        //    error = "";
        //    bool isCompleted = false;
        //    isRetakeable = false;
        //    if (regSemester == null)
        //    {
        //        regSemester = DbInstance.Aca_Semester.Find(courseToAdd.SemesterId);
        //    }
        //    if (regSemester == null)
        //    {
        //        error = "There is no valid semester Associated with this Class!";
        //        return false;
        //    }
        //    //checked if curriculmn courseId Not Same
        //    var takenCourseList = DbInstance.Aca_ClassTakenByStudent
        //    //.Include(x => x.Aca_Class)
        //    //.Include(x => x.Aca_Class.Aca_CurriculumCourse)
        //    .Where(x => x.StudentId == student.Id
        //    && x.Aca_Class.CurriculumCourseId == courseToAdd.CurriculumCourseId
        //    && (x.RegistrationStatus == Aca_ClassTakenByStudent.RegistrationStatusEnum.FreshRegistration
        //            || x.RegistrationStatus == Aca_ClassTakenByStudent.RegistrationStatusEnum.RetakeRegistration)
        //    ).ToList();
        //    if (!takenCourseList.Any())
        //        return false;

        //    ////check grade, get only passed course 
        //    //var takenCourseResult = DbInstance.Aca_ResultClass
        //    //    //.Include(x => x.Aca_Class)
        //    //    //.Include(x => x.Aca_Class.Aca_CurriculumCourse)
        //    //    .OrderByDescending(x => x.GradePoint)
        //    //    .Where(x => x.StudentId == student.Id
        //    //    && x.CurriculumCourseId == courseToAdd.CurriculumCourseId
        //    //    && x.GradePoint > 0
        //    //    );

        //    //if (!takenCourseResult.Any())
        //    //    return false;

        //    //foreach (var courseTaken in takenCourseList)
        //    //{
        //    //    //if (courseTaken.Aca_Class.CurriculumCourseId != courseToAdd.CurriculumCourseId)
        //    //    //{
        //    //    //    continue;
        //    //    //}
        //    //    // Check Registration Status (not drop/cancelled)
        //    //    if (courseTaken.RegistrationStatus == Aca_ClassTakenByStudent.RegistrationStatusEnum.FreshRegistration
        //    //        || courseTaken.RegistrationStatus == Aca_ClassTakenByStudent.RegistrationStatusEnum.RetakeRegistration)
        //    //    {
        //    //        //status good, now check grade
        //    //        var result = takenCourseResult.OrderByDescending(x => x.GradePoint)
        //    //            .FirstOrDefault(x => x.ClassId == courseTaken.ClassId);
        //    //        if (null != result)
        //    //        {
        //    //            if (result.GradePoint <= regSemester.MinimumGradePointForRetake)
        //    //            {
        //    //                isRetakeable = true;
        //    //            }
        //    //            return true;
        //    //        }
        //    //    }
        //    //}

        //    return false;

        //}

        bool UpdateSemesterPayment(User_Student student, Aca_Semester regSemester, out string error)
        {
            error = "";
            //1. todo get all course and check below
            var takenAllCourseList = DbInstance.Aca_ClassTakenByStudent
            .Include(x => x.Aca_Class)
            .Include(x => x.Aca_Class.Aca_CurriculumCourse)
            .Where(x => x.StudentId == student.Id
             //&& (x.RegistrationStatus != Aca_ClassTakenByStudent.RegistrationStatusEnum.Cancelled)//todo check status
            ).ToList();

            var takenCourseListInSemester = takenAllCourseList
            .Where(x => x.StudentId == student.Id
            && x.Aca_Class.SemesterId== regSemester.Id
            //&& (x.RegistrationStatus != Aca_ClassTakenByStudent.RegistrationStatusEnum.Cancelled)//todo check status
            ).ToList();

            if (!takenCourseListInSemester.Any())
            {
                //todo remove payment
                return DeleteAssesments(student.Id, regSemester, out error);
            }


            //2. todo check is retake course
            var retakenCourse =new List<Aca_ClassTakenByStudent>();
            foreach (var courseTaken in takenCourseListInSemester)
            {
                if (takenAllCourseList.Any(x=>x.Aca_Class.CurriculumCourseId==courseTaken.Aca_Class.CurriculumCourseId ))//todo check status
                {
                    retakenCourse.Add(courseTaken);
                }
            }
            //todo if not retake check is over credit
            /*var feeCode = DbInstance.Acnt_FeeCode.Find(student.FeeCodeId);
            if (feeCode == null)
            {
                error = $"Student({student.ClassRollNo}) Fee Code Not Found, This Student's Course Registration is not Possible!";
                return false;
            }*/
            var totalAllCreditTaken= takenAllCourseList.Sum(x => x.Aca_Class.Aca_CurriculumCourse.CreditLoad);
            var totalCreditTakenInSemester = takenCourseListInSemester.Sum(x => x.Aca_Class.Aca_CurriculumCourse.CreditLoad);

            var totalRetakeCreditInSemester = 0.0f;
            if (retakenCourse.Any())
            {
                totalRetakeCreditInSemester= retakenCourse.Sum(x => x.Aca_Class.Aca_CurriculumCourse.CreditLoad);
            }

            var totalFreshCreditTaken = totalCreditTakenInSemester - totalRetakeCreditInSemester;

            //var allowedCredit = feeCode.TotalCredit;

            //todo check previous over credit
            //todo check previous retake credit
            //todo check credit transfer
            //var allowedCreditRemain = allowedCredit - (totalAllCreditTaken - totalRetakeCreditInSemester);

            //todo check is over semester
            //var allowedSemester = feeCode.TotalSemester;
            var totalRegistrationSemester = takenAllCourseList.Select(x => x.Aca_Class.SemesterId).Distinct();
            //var isOverSemester = (totalRegistrationSemester.Count() - allowedSemester) > 0;


            //todo check is thesis or internship semester
            //todo check credit transfer or regular student 
            //GetGenerateSemesterFees

            return true;

        }

        public bool UpdateRegistration(Aca_StudentRegistration newObj, ref Aca_StudentRegistration dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Student Ragistration to save can't be null!";
                return false;
            }


            bool isNewObject = true;
            Aca_StudentRegistration objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Aca_StudentRegistration.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Aca_StudentRegistration.GetNew(newObj.Id);
                DbInstance.Aca_StudentRegistration.Add(objToSave);
                objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
                objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }


            //binding object  
            objToSave.SemesterId = newObj.SemesterId;
            objToSave.LevelId = newObj.LevelId;
            objToSave.StudentId = newObj.StudentId;
            objToSave.RegistrationNo = newObj.RegistrationNo;
            objToSave.IPAddress = newObj.IPAddress;
            objToSave.HostAddress = newObj.HostAddress;
            objToSave.RegStatusEnumId = newObj.RegStatusEnumId;
            objToSave.FeeCodeId = newObj.FeeCodeId;
            objToSave.Remark = newObj.Remark;
            objToSave.BankId = newObj.BankId;
            objToSave.LastChangedById = newObj.LastChangedById = HttpUtil.Profile.UserId;
            objToSave.LastChanged = newObj.LastChanged = DateTime.Now;
            dbAddedObj = objToSave;

            //here save any child table
            return true;

        }


        public bool DeleteAssesments(int studentId, Aca_Semester semester, out string error)
        {
            error = "";
         
            //using (var scope = DbInstance.Database.BeginTransaction())
            /*{
                Acnt_StdTransaction transaction = DbInstance.Acnt_StdTransaction
                    .Include(x => x.Acnt_StdTransactionDetail)
                    .FirstOrDefault(x => x.StudentId == studentId
                                         && x.SemesterId == semester.Id
                                         && x.IsSemesterFee
                    );
                if (transaction != null)
                {
                    foreach (var detail in transaction.Acnt_StdTransactionDetail)
                    {
                        DbInstance.Acnt_StdTransactionDetail.Remove(detail);
                    }
                    DbInstance.Acnt_StdTransaction.Remove(transaction);
                }
                DbInstance.SaveChanges();

            }*/
            return true;
        }
        

        #endregion


        #region Class Teachers



        //Only used in Employee Area class management
        public List<Aca_Semester> GetSemesterListByTeacherId(long teacherId)
        {
            List<Aca_ClassTakenByEmployee> classTakenList = DbInstance.Aca_ClassTakenByEmployee
                .Include(x => x.Aca_Class.HR_Department)
                .Include(x => x.Aca_Class.Aca_ClassSection)
                .Include(x => x.Aca_Class.Aca_Semester)
                .Include(x => x.Aca_Class.Aca_DeptBatch)
                //.Include(x => x.Aca_Class.Aca_SemesterLevelTerm)
                .Include(x => x.Aca_Class.Aca_CurriculumCourse)//for pro
                .Include(x => x.Aca_Class.Aca_CurriculumCourse.Aca_Curriculum)//for pro
                .Include(x => x.Aca_Class.Aca_CurriculumCourse.Aca_Curriculum.Aca_Program)//for pro
                .OrderByDescending(x => x.Aca_Class.SemesterId)
                //.ThenByDescending(x => x.Aca_Class.Aca_SemesterLevelTerm.Aca_Semester.Year)
                .Where(x => x.EmployeeId == teacherId
                && !x.IsDeleted).ToList();

            List<Aca_Semester> semesterList = new List<Aca_Semester>();
            if (classTakenList.Count > 0)
            {
                foreach (var classTaken in classTakenList)
                {
                    if (classTaken.Aca_Class != null && classTaken.Aca_Class.Aca_Semester != null)
                    {
                        //making order of class list
                        classTaken.Aca_Class.Aca_Semester.Aca_Class = classTaken
                            .Aca_Class
                            .Aca_Semester
                            .Aca_Class.OrderBy(x => x.HR_Department.ShortName)
                            .ThenBy(x => x.Code)
                            .ToList();
                        semesterList.Add(classTaken.Aca_Class.Aca_Semester);
                    }
                }
                semesterList = semesterList.OrderByDescending(x => x.Year).ThenBy(x => x.TermId).ToList();
            }
            return semesterList;
        }



        //for dashboard class List

        //for employee dashboard semester list
        public List<Aca_Semester> GetSemesterListByTeacherIdTypeId(long teacherId)
        {
            List<Aca_ClassTakenByEmployee> classTakenList = DbInstance.Aca_ClassTakenByEmployee
                //.Include(x => x.Aca_Class.HR_Department)
                //.Include(x => x.Aca_Class.Aca_ClassRoutine)
                //.Include("Aca_Class.Aca_ClassRoutine.General_Room")
                //.Include("Aca_Class.Aca_ClassRoutine.General_Room.General_Floor")
                //.Include("Aca_Class.Aca_ClassRoutine.General_Room.General_Building")
                //.Include("Aca_Class.Aca_ClassRoutine.General_Room.General_Building.General_Campus")
                //.Include(x => x.Aca_Class.Aca_ClassSection)
                .Include(x => x.Aca_Class.Aca_Semester)
                ////.Include(x => x.Aca_Class.Aca_SemesterLevelTerm)
                ////.Include(x => x.Aca_Class.Aca_SemesterLevelTerm.Aca_Semester)
                //.Include(x => x.Aca_Class.Aca_SemesterLevelTerm.Aca_StudyLevelTerm.Aca_StudyLevel)
                .Where(x => x.EmployeeId == teacherId
                && !x.IsDeleted
                ).ToList();

            List<Aca_Semester> semesterList = new List<Aca_Semester>();
            if (classTakenList.Count > 0)
            {
                foreach (var classTaken in classTakenList)
                {
                    if (classTaken.Aca_Class?.Aca_Semester != null)
                    {
                        //making order of class list
                        //classTaken.Aca_Class.Aca_Semester.Aca_Class = classTaken.Aca_Class.Aca_Semester.Aca_Class.OrderBy(x => x.HR_Department.ShortName).ThenBy(x => x.Code).ToList();
                        var classToAdd = semesterList.SingleOrDefault(x => x.Id == classTaken.Aca_Class.Aca_Semester.Id);
                        if (classToAdd == null)
                        {
                            semesterList.Add(classTaken.Aca_Class.Aca_Semester);
                        }

                    }
                }
                semesterList = semesterList.OrderByDescending(x => x.Year).ThenBy(x => x.TermId).ToList();
            }
            return semesterList;
        }
        //for employee dashboard ClassList By Semester list
        public List<Aca_Class> GetClassListByTeacherIdSemesterId(long teacherId, long semeterId)
        {
            List<Aca_ClassTakenByEmployee> classTakenList = DbInstance.Aca_ClassTakenByEmployee
                .Include(x => x.Aca_Class.HR_Department)
                .Include(x => x.Aca_Class.Aca_CurriculumCourse)//for pro
                .Include(x => x.Aca_Class.Aca_CurriculumCourse.Aca_Curriculum)//for pro
                .Include(x => x.Aca_Class.Aca_CurriculumCourse.Aca_Curriculum.Aca_Program)//for pro
                .Include(x => x.Aca_Class.Aca_ClassRoutine)
                .Include("Aca_Class.Aca_ClassRoutine.General_Room")
                .Include("Aca_Class.Aca_ClassRoutine.General_Room.General_Floor")
                .Include("Aca_Class.Aca_ClassRoutine.General_Room.General_Building")
                .Include("Aca_Class.Aca_ClassRoutine.General_Room.General_Building.General_Campus")
                .Include(x => x.Aca_Class.Aca_ClassSection)
                .Include(x => x.Aca_Class.Aca_Semester)
                //.Include(x => x.Aca_Class.Aca_SemesterLevelTerm)
                //.Include(x => x.Aca_Class.Aca_SemesterLevelTerm.Aca_Semester)
                .Include(x => x.Aca_Class.Aca_StudyLevelTerm)
                .OrderByDescending(x => x.Aca_Class.StudyLevelTermId)
                .Where(x => x.EmployeeId == teacherId && x.Aca_Class.SemesterId == semeterId
                && !x.IsDeleted
                ).ToList();

            List<Aca_Class> classList = new List<Aca_Class>();
            if (classTakenList.Count > 0)
            {
                foreach (var classTaken in classTakenList)
                {
                    if (classTaken.Aca_Class != null && classTaken.Aca_Class.Aca_Semester != null)
                    {
                        //making order of class list
                        var classToAdd = classList.SingleOrDefault(x => x.Id == classTaken.Aca_Class.Id);
                        if (classToAdd == null)
                        {
                            classToAdd = classTaken.Aca_Class;
                            classToAdd.EmployeeInRoleList = new List<string>();
                            classList.Add(classTaken.Aca_Class);
                        }

                        classToAdd.ProgramName = classToAdd.Aca_CurriculumCourse.Aca_Curriculum.Aca_Program.Name;
                        classToAdd.ProgramShortTitle = classToAdd.Aca_CurriculumCourse.Aca_Curriculum.Aca_Program.ShortTitle;
                        classToAdd.ProgramShortName = classToAdd.Aca_CurriculumCourse.Aca_Curriculum.Aca_Program.ShortName;

                        classToAdd.EmployeeInRoleList.Add(classTaken.Role.ToString().AddSpacesToSentence());
                    }
                }
                if (classList.Count > 0)
                {
                    classList = classList.OrderBy(x => x.HR_Department.ShortName).ThenBy(x => x.StudyLevelTermId).ThenBy(x => x.Code).ToList();
                }
            }
            return classList;
        }

        public bool IsEmployeeHasInRoleInClass(long teacherId, long classId, Aca_ClassTakenByEmployee.RoleEnum role)
        {
            var res = DbInstance.Aca_ClassTakenByEmployee
                .Any(x => x.ClassId == classId && x.EmployeeId == teacherId 
                                               && !x.IsDeleted
                                               && x.RoleEnumId == (byte)role);
            return res;

        }

        public List<Aca_ClassTakenByEmployee> GetTeacherListByClassId(long classId, bool isShowTrashedFaculty)
        {
            IQueryable<Aca_ClassTakenByEmployee> query = DbInstance.Aca_ClassTakenByEmployee
                .Include(x => x.User_Employee)
                .Include(x => x.User_Employee.User_Account)
                .Where(x => x.ClassId == classId)
                .OrderBy(x => x.User_Employee.User_Account.UserName);

            if (isShowTrashedFaculty)
            {
                query = query.Where(x => x.IsDeleted);
            }
            else
            {
                query = query.Where(x => !x.IsDeleted);
            }

            List<Aca_ClassTakenByEmployee> classEmployeeMapList = query.ToList();
            return classEmployeeMapList;
        }
        public List<User_Employee> GetTeacherList(long deptId, int campusId)
        {
            //TODO Filtered for Dept. Id
            List<User_Employee> employeeList = DbInstance.User_Employee
                                                        .Include(x => x.Aca_ClassTakenByEmployee)
                                                        .Include(x => x.User_Account)
                                                        .Where(x => x.User_Account.UserStatusEnumId == (byte)User_Account.UserStatusEnum.Active
                                                        && x.User_Account.CampusId == campusId
                                                        //&& x.User_Account.IsApproved
                                                        && x.WorkingStatusEnumId == (byte)User_Employee.WorkingStatusEnum.Active
                                                        && x.JobClassEnumId == (byte)User_Employee.JobClassEnum.FirstGrade//only 1st class can take class
                                                        && x.User_Account.DepartmentId == deptId)
                                                        .OrderBy(x => x.User_Account.UserName).ToList();
            return employeeList;
        }
        public List<User_Employee> GetTeacherListByClassIdByRoleEnumId(long classId, byte roleEnumId)
        {
            List<Aca_ClassTakenByEmployee> classMapList = DbInstance.Aca_ClassTakenByEmployee
                                                        .Include(x => x.User_Employee)
                                                        .Include(x => x.User_Employee.User_Account)
                                                        .Where(x => x.ClassId == classId
                                                        && x.RoleEnumId == roleEnumId
                                                       )
                                                        .OrderBy(x => x.User_Employee.User_Account.UserName).ToList();

            var list = classMapList.Select(classMap => classMap.User_Employee).ToList();
            return list;
        }
        public List<Aca_ClassTakenByEmployee> GetClassTeacherMapList(long classId, int? pageSize, int? pageNo, ref int count)
        {
            IQueryable<Aca_ClassTakenByEmployee> query = (from q in DbInstance.Aca_ClassTakenByEmployee
                                                          select q)
                                                     .Include(x => x.User_Employee)
                                                     .Where(x => x.ClassId == classId)
                                                     .OrderBy(x => x.EmployeeId);
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

        public bool SaveClassTeacherList(List<User_Employee> employeeList, long classId, byte roleEnumId, byte sectionEnumId, byte statusEnumId, out string error)
        {
            //check permission
            var Class = DbInstance.Aca_Class.SingleOrDefault(x => x.Id == classId);
            var enrolledEmployeeList = DbInstance.Aca_ClassTakenByEmployee.Where(x => x.ClassId == classId).ToList();
            if (Class == null)
            {
                error = "Invalid Class!";
                return false;
            }
            if (employeeList == null || employeeList.Count == 0)
            {
                error = "Select Employee for this class.";
                return false;
            }
            //TODO Check for validation
            //TODO Check for result exists, can't delete.
            //TODO Check for any payment exists, refund amount.
            error = string.Empty;
            using (var scope = DbInstance.Database.BeginTransaction())
            {
                try
                {
                    foreach (var userEmployee in employeeList)
                    {
                        bool isNewObject = true;
                        Aca_ClassTakenByEmployee objToSave = DbInstance.Aca_ClassTakenByEmployee.SingleOrDefault(
                            x => x.ClassId == Class.Id
                            && x.EmployeeId == userEmployee.Id
                            && x.RoleEnumId == roleEnumId
                            && x.SectionEnumId == sectionEnumId
                            && x.StatusEnumId == statusEnumId
                            );
                        if (objToSave == null)
                        {
                            objToSave = Aca_ClassTakenByEmployee.GetNew(BigInt.NewBigInt());

                            objToSave.ClassId = classId;
                            objToSave.EmployeeId = userEmployee.Id;

                            DbInstance.Aca_ClassTakenByEmployee.Add(objToSave);
                            objToSave.CreateDate = DateTime.Now;
                            objToSave.CreateById = Profile.UserId;
                        }

                        objToSave.RoleEnumId = roleEnumId;
                        objToSave.SectionEnumId = sectionEnumId;
                        objToSave.StatusEnumId = statusEnumId;

                        objToSave.LastChanged = DateTime.Now;
                        objToSave.LastChangedById = Profile.UserId;
                    }
                    DbInstance.SaveChanges();
                    scope.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    scope.Rollback();
                    return false;
                }
            }
        }
        public bool DeleteClassTeacher(Aca_ClassTakenByEmployee entity, out string error)
        {
            try
            {
                //check permission
                //TODO Check for validation
                //TODO Check for Result Inputted, can't delete.
                error = string.Empty;
                Aca_ClassTakenByEmployee result = DbInstance.Aca_ClassTakenByEmployee
                    .FirstOrDefault(x => x.Id == entity.Id);
                if (result == null)
                {
                    error = "Can't find this faculty! ";
                    return false;
                }

                var classObj = DbInstance.Aca_Class.FirstOrDefault(x => x.Id == result.ClassId);
                var faculty = DbInstance.User_Employee
                    .Include(x => x.User_Account)
                    .Where(x => x.Id == result.EmployeeId)
                    .Select(x => new
                    {
                        Name = x.User_Account.FullName,
                        UserName = x.User_Account.UserName
                    }).FirstOrDefault();
                    
                if (classObj != null)
                {
                    classObj.History += $"{DateTime.Now} {faculty?.Name}[{faculty?.UserName}] was permanently deleted From {classObj.Name} class, by {HttpUtil.Profile.Name}[{HttpUtil.Profile.UserName}], IP:{HttpUtil.GetUserIp()}<br>"; ;
                }

                DbInstance.Aca_ClassTakenByEmployee.Remove(result);
                DbInstance.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }
        #endregion Class Teachers
    }
}

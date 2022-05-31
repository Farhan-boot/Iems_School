using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EMS.CoreLibrary;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases.WebApi;
using EMS.Framework.Objects;
//using EMS.Web.Jsons.Mapper;
using EMS.Web.Jsons.Models;

namespace EMS.Web.UI.Areas.Academic.Controllers.WebApi
{
    public class SemesterApiController : EmployeeBaseApiController
    {
        public SemesterApiController()
        { }

        #region Semester 
        #region List View Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Aca_SemesterJson> GetPagedSemester(string textkey, int? pageSize, int? pageNo
           , Int32 termId = 0
           ,int semesterDurationEnumId = 0
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Aca_SemesterJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.SemesterManager.Semester.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.SemesterManager.Semester.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.SemesterManager.Semester.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_SemesterDataService semesterDataService = new Aca_SemesterDataService(DbInstance, HttpUtil.Profile))
                {
                    IQueryable<Aca_Semester> query = DbInstance
                        .Aca_Semester
                        .Include(x=>x.Aca_Exam)
                        .Include(x=>x.Adm_AdmissionExam)
                        .OrderByDescending(x => x.Id);
                        //.ThenByDescending(x => x.TermId);
                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Name.ToLower().Contains(textkey.ToLower()));
                    }
                    if (termId > 0)
                    {
                        query = query.Where(q => q.TermId == termId);
                    }

                    if (semesterDurationEnumId > 0)
                    {
                        query = query.Where(q => q.SemesterDurationEnumId == semesterDurationEnumId);
                    }

                    var entities = semesterDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Aca_SemesterJson>();
                    entities.Map(jsons);

                    result.Data = jsons;
                    result.Count = count;
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<Aca_SemesterJson> GetSemesterListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_SemesterJson>();
            try
            {
                //Aca_SemesterDataService semesterDataService =
                //    new Aca_SemesterDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                result.DataExtra.ProgramTypeEnumList = EnumUtil.GetEnumList(typeof(Aca_Program.ProgramTypeEnum));
                result.DataExtra.StatusEnumList = EnumUtil.GetEnumList(typeof(Aca_Semester.StatusEnum));
                //DropDown Option Tables

                //result.DataExtra.StudyTermList = DbInstance.Aca_StudyTerm.AsEnumerable().Select(t => new
                //{ Id = t.Id,Name = t.Name }).ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        /// <summary>
        /// Caution: Very Dangerous to call When table have huge data!
        /// </summary>
        /// <returns></returns>
        private HttpListResult<Aca_SemesterJson> GetAllSemester()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_SemesterJson>();
            try
            {
                using (Aca_SemesterDataService semesterDataService = new Aca_SemesterDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<Aca_SemesterJson>();
                    var entities = semesterDataService.GetAll(out error);
                    entities.Map(jsons);
                    result.Data = jsons;
                    result.Count = jsons.Count;
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        /// <summary>
        /// Todo pending to implement bulk save
        /// </summary>
        /// <param name="jsonList"></param>
        /// <returns></returns>
        [HttpPost]
        private HttpListResult<Aca_SemesterJson> SaveSemesterList(IList<Aca_SemesterJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_SemesterJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.SemesterManager.Semester.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.SemesterManager.Semester.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_SemesterDataService semesterDataService = new Aca_SemesterDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<Aca_Semester>();
                    var dbAttachedListEntity = new List<Aca_Semester>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (semesterDataService.Save(entity, ref dbAttachedListEntity, out error))
                    //{
                    //    dbAttachedListEntity.Map(jsonList);
                    //    result.Data = jsonList;//return object
                    //}
                    //else
                    //{
                    //    result.HasError = true;
                    //    result.Errors.Add(error);
                    //}
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }

        #endregion

        #region Add/Edit View Api
        #region Get
        public HttpResult<Aca_SemesterJson> GetSemesterById(long id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_SemesterJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.SemesterManager.Semester.CanView, out error)
             && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.SemesterManager.Semester.CanAdd, out error)
             && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.SemesterManager.Semester.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_SemesterDataService semesterDataService = new Aca_SemesterDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Aca_SemesterJson();
                    Aca_Semester entity;
                    if (id <= 0)
                    {
                        entity = Aca_Semester.GetNew();
                    }
                    else
                    {
                        entity = DbInstance.Aca_Semester.Include(x=>x.Aca_Exam).Include(x=>x.Adm_AdmissionExam).FirstOrDefault(x=>x.Id == id);
                    }
                    entity.Map(json);
                    result.Data = json;

                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        private HttpResult<Aca_SemesterJson> GetSemesterByIdWithChild(long id)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_SemesterJson>();
            try
            {
                using (Aca_SemesterDataService semesterDataService = new Aca_SemesterDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Aca_SemesterJson();
                    Aca_Semester entity;
                    if (id <= 0)
                    {
                        entity = Aca_Semester.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("Aca_Semester");
                        //includeTables.Add("Aca_Semester");

                        entity = semesterDataService.GetById(id, includeTables.ToArray());
                    }
                    entity.Map(json);
                    result.Data = json;
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<Aca_SemesterJson> GetSemesterDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_SemesterJson>();
            try
            {
                //Aca_SemesterDataService semesterDataService =
                //    new Aca_SemesterDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                result.DataExtra.ProgramTypeEnumList = EnumUtil.GetEnumList(typeof(Aca_Program.ProgramTypeEnum));
                result.DataExtra.StatusEnumList = EnumUtil.GetEnumList(typeof(Aca_Semester.StatusEnum));
                result.DataExtra.EnabledRegistrationTypeEnumList = EnumUtil.GetEnumList(typeof(Aca_Semester.EnabledRegistrationTypeEnum));
                result.DataExtra.SemesterDurationList = Aca_Semester.SemesterDurationEnumDropDownList;
                result.DataExtra.MonthEnumList = EnumUtil.GetEnumList(typeof(EnumCollection.Common.MonthEnum));
                //DropDown Option Tables

                result.DataExtra.StudyTermList = DbInstance.Aca_StudyTerm.AsEnumerable().Select(t => new
                { Id = t.Id, Name = t.SemesterName,SemesterDurationEnumId = t.SemesterDurationEnumId }).ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        #endregion

        #region Save/Delete
        [HttpPost]
        public HttpResult<Aca_SemesterJson> SaveSemester(Aca_SemesterJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_SemesterJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.SemesterManager.Semester.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.SemesterManager.Semester.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                var entityReceived = new Aca_Semester();
                var dbAttachedEntity = new Aca_Semester();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction())
                {
                    if (SaveSemesterLogic(entityReceived, ref dbAttachedEntity, out error))
                    {
                        DbInstance.SaveChanges();

                        if (json.IsCreateAdmissionExam)
                        {
                            if (!CreateAdmissionExamFromPreviousSemester(json,dbAttachedEntity.Id,dbAttachedEntity.SemesterNo,out error))
                            {
                                result.HasError = true;
                                result.Errors.Add(error);
                                return result;
                            }

                            DbInstance.SaveChanges();
                        }


                        if (json.IsCreateFinalTermExam)
                        {
                            if (!CreateFinalTermExamFromPreviousSemester(json,dbAttachedEntity.Id,dbAttachedEntity.SemesterNo,out error))
                            {
                                result.HasError = true;
                                result.Errors.Add(error);
                                return result;
                            }
                            DbInstance.SaveChanges();
                        }

                        scope.Commit();

                        dbAttachedEntity.Map(json);
                        result.Data = json; //return object
                    }
                    else
                    {
                        result.HasError = true;
                        result.Errors.Add(error);
                        scope.Rollback();
                    }
                }
            }

            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }

        public HttpResult<Aca_SemesterJson> GetDeleteSemesterById(long id)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_SemesterJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.SemesterManager.Semester.CanDelete, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_SemesterDataService semesterDataService = new Aca_SemesterDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!semesterDataService.DeleteById(id, out error))
                    {
                        result.HasError = true;
                        result.Errors.Add(error);
                    }
                }
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        #endregion
        #endregion

        #region Local Method (should be always private)

        private bool CreateAdmissionExamFromPreviousSemester(Aca_SemesterJson json,long semesterId,int orderNo, out string error)
        {
            error = string.Empty;

            var dbAttachedEntity = new Adm_AdmissionExam();

            var isAdmissionExamAlreadyCreated = DbInstance.Adm_AdmissionExam.Any(x => x.SemesterId == semesterId);

            if (isAdmissionExamAlreadyCreated)
            {
                error += "Admission Exam Already Created.";
                return false;
            }

            var previousSemesterId = DbInstance.Aca_Semester.Where(x => x.SemesterNo == orderNo - 1 && x.SemesterDurationEnumId == json.SemesterDurationEnumId).Select(x=>x.Id).FirstOrDefault();

            if (previousSemesterId == null || previousSemesterId<=0)
            {
                error += "Admission Exam Cannot be created.Previous Semester Not found.";
                return false;
            }

            var sourceAdmissionExamId = DbInstance.Adm_AdmissionExam.Where(x => x.SemesterId == previousSemesterId)
                .Select(x => x.Id).FirstOrDefault();

            if (sourceAdmissionExamId == null || sourceAdmissionExamId <= 0)
            {
                error += "Admission Exam Cannot be created. Previous Semester Does Not have any Admission Exam Created. Create Previous Semester Admission Exam First.";
                return false;
            }

            return Facade.AcademicFacade.SemesterFacade.CopyAndSaveAdmissionExam(sourceAdmissionExamId
                , semesterId, json.AdmExamName
                , json.StudentIdPrefix,json.StudentUgcIdPrefix, json.IncrementBatchCount
                , ref dbAttachedEntity, out error);
        }

        private bool CreateFinalTermExamFromPreviousSemester(Aca_SemesterJson json, long semesterId, int orderNo, out string error)
        {
            error = string.Empty;

            var isExamAlreadyCreated = DbInstance.Aca_Exam.Any(x => x.SemesterId == semesterId);

            if (isExamAlreadyCreated)
            {
                error += "Final Term Exam Already Created.";
                return false;
            }

            var previousSemesterId = DbInstance.Aca_Semester.Where(x => x.SemesterNo == orderNo - 1 && x.SemesterDurationEnumId == json.SemesterDurationEnumId).Select(x => x.Id).FirstOrDefault();

            if (previousSemesterId == null || previousSemesterId <= 0)
            {
                error += "Final Term Exam Cannot be created.Previous Semester Not found.";
                return false;
            }

            var sourceAcaExam = DbInstance.Aca_Exam.FirstOrDefault(x => x.SemesterId == previousSemesterId);
            if (sourceAcaExam == null)
            {
                error += "Final Term Exam Cannot be created. Previous Semester Does Not have any Final Term Exam Created. Create Previous Semester Final Term Exam First.";
                return false;
            }
            var entityReceived = Aca_Exam.GetNew();

            //General Settings
            entityReceived.Name = json.FinalTermExamName;
            entityReceived.ShortName = json.FinalTermExamShortName;
            entityReceived.OfficialExamName = json.OfficialExamName;
            entityReceived.AcademicSession = json.AcademicSession;
            entityReceived.SemesterId = semesterId;
            entityReceived.ExamTypeEnumId = (byte)Aca_Exam.ExamTypeEnum.FinalTerm;
            //entityReceived.ProgramTypeEnumId = (byte)Aca_Program.ProgramTypeEnum.Any;

            entityReceived.StartDateOfMidtermExam = sourceAcaExam.StartDateOfMidtermExam?.AddMonths(json.SemesterDurationEnumId);
            entityReceived.EndDateOfMidtermExam = sourceAcaExam.EndDateOfMidtermExam?.AddMonths(json.SemesterDurationEnumId);

            entityReceived.DateOfMidtermResultPublication = sourceAcaExam.DateOfMidtermResultPublication;
            
            entityReceived.StartDateOfTermFinalExam = sourceAcaExam.StartDateOfTermFinalExam.AddMonths(json.SemesterDurationEnumId);
            entityReceived.EndDateOfTermFinalExam = sourceAcaExam.EndDateOfTermFinalExam.AddMonths(json.SemesterDurationEnumId);

            entityReceived.DateOfTermFinalResultPublication = sourceAcaExam.DateOfTermFinalResultPublication.AddMonths(json.SemesterDurationEnumId);

            //Allowed Payment Due Settings
            entityReceived.ContinuousAssessmentMarksAllowedPaymentDueUpto = sourceAcaExam.ContinuousAssessmentMarksAllowedPaymentDueUpto;
            entityReceived.MidtermResultAllowedPaymentDueUpto = sourceAcaExam.MidtermResultAllowedPaymentDueUpto;
            entityReceived.FinalTermResultAllowedPaymentDueUpto = sourceAcaExam.FinalTermResultAllowedPaymentDueUpto;

            entityReceived.MidtermAdmitCardDownloadPaymentDueUpto = sourceAcaExam.MidtermAdmitCardDownloadPaymentDueUpto;
            entityReceived.FinalTermAdmitCardDownloadPaymentDueUpto = sourceAcaExam.FinalTermAdmitCardDownloadPaymentDueUpto;

            //result Generate Settings.

            entityReceived.AbsentMarkSymbol = sourceAcaExam.AbsentMarkSymbol;
            entityReceived.AbsentGradeId = sourceAcaExam.AbsentGradeId;
            entityReceived.ContinuationGradeId = sourceAcaExam.ContinuationGradeId;

            entityReceived.MaximumGradeForImproveExamGradeId = sourceAcaExam.MaximumGradeForImproveExamGradeId;
            entityReceived.MaximumImproveExamAllowGradeId = sourceAcaExam.MaximumImproveExamAllowGradeId;

            entityReceived.MaximummGradeForRetakeCourseGradeId = sourceAcaExam.MaximummGradeForRetakeCourseGradeId;
            entityReceived.MaximumRetakeCourseAllowGradeId = sourceAcaExam.MaximumRetakeCourseAllowGradeId;

            var dbAttachedEntity = new Aca_Exam();

            return Facade.AcademicFacade.SemesterFacade.SaveExamLogic(entityReceived, ref dbAttachedEntity, out error);
        }

        private bool IsValidToSaveSemester(Aca_Semester newObj, out string error)
        {
            error = "";
            if (newObj.SemesterNo == null)
            {
                error += "Semester No required.";
                return false;
            }
            if (!newObj.Name.IsValid())
            {
                error += "Name is not valid.";
                return false;
            }
            if (newObj.Name.Length > 128)
            {
                error += "Name exceeded its maximum length 128.";
                return false;
            }
            if (newObj.TermId <= 0)
            {
                error += "Please select valid Term.";
                return false;
            }

            if (newObj.StartingMonthEnumId <= 0 || newObj.StartingMonthEnumId == null)
            {
                error += "Please select valid Starting Month.";
                return false;
            }

            if (newObj.SemesterDurationEnumId <= 0 || newObj.SemesterDurationEnumId == null)
            {
                error += "Please select a Valid Duration.";
                return false;
            }
            if (!newObj.StartDate.IsValid())
            {
                error += "Start Date is not valid.";
                return false;
            }
            if (!newObj.EndDate.IsValid())
            {
                error += "End Date is not valid.";
                return false;
            }
            if (newObj.Year == null)
            {
                error += "Year required.";
                return false;
            }
            if (newObj.Year <= 1990)
            {
                error += "Year can't be less then 1990.";
                return false;
            }
            if (!newObj.AcademicSession.IsValid())
            {
                error += "Academic Session is not valid.";
                return false;
            }
            if (newObj.AcademicSession.Length > 50)
            {
                error += "Academic Session exceeded its maximum length 50.";
                return false;
            }
            if (newObj.StatusEnumId < 0)
            {
                error += "Status required.";
                return false;
            }
            if (newObj.IsDeleted == null)
            {
                error += "Is Deleted required.";
                return false;
            }
            //new add
            if (newObj.IsRegistrationSemester == null)
            {
                error += "Is Enable Pre Registration required.";
                return false;
            }

            if (newObj.RegistrationStartDate != null && !newObj.RegistrationStartDate.IsValid())
            {
                newObj.RegistrationStartDate = null;//just put null,if its nullable and not valid.
                                                    //error += "Pre Registration Start Date is not valid.";
                                                    //return false;
            }
            if (newObj.RegistrationEndDate != null && !newObj.RegistrationEndDate.IsValid())
            {
                newObj.RegistrationEndDate = null;//just put null,if its nullable and not valid.
                                                  //error += "Pre Registration End Date is not valid.";
                                                  //return false;
            }

            if (newObj.IsEnableLateRegFee == null)
            {
                error += "Is Enable Late Reg Fee required.";
                return false;
            }
            if (newObj.LateRegFeeStartDate != null && !newObj.LateRegFeeStartDate.IsValid())
            {
                newObj.LateRegFeeStartDate = null;//just put null,if its nullable and not valid.
                                                  //error += "Late Reg Fee Start Date is not valid.";
                                                  //return false;
            }
            if (newObj.LateRegFeeAmount == null)
            {
                error += "Late Reg Fee Amount required.";
                return false;
            }
            if (newObj.MaximumUGCredit == null)
            {
                error += "Maximum U G Credit required.";
                return false;
            }
            if (newObj.MinimumUGCredit == null)
            {
                error += "Minimum U G Credit required.";
                return false;
            }
            if (newObj.MaximumPGCredit == null)
            {
                error += "Maximum P G Credit required.";
                return false;
            }
            if (newObj.MinimumPGCredit == null)
            {
                error += "Minimum P G Credit required.";
                return false;
            }
            if (!newObj.ExamPermitPublishDate.IsValid())
            {
                error += "Exam Permit Publish Date is not valid.";
                return false;
            }
            if (newObj.CurrentDropPercentage == null)
            {
                error += "Current Drop Percentage required.";
                return false;
            }
            if (newObj.NewStudentIdPrefix == null)
            {
                error += "New Student Id Prefix required.";
                return false;
            }
            if (newObj.NewStudentSuffix == null)
            {
                error += "New Student Suffix required.";
                return false;
            }
            if (newObj.SectionTolarange == null)
            {
                error += "Section Tolarange required.";
                return false;
            }
            if (newObj.IsTpeEnable == null)
            {
                error += "Is Tpe Enable required.";
                return false;
            }
            if (newObj.IsForceTpeSubmit == null)
            {
                error += "Is Force Tpe Submit required.";
                return false;
            }

            bool res;
            if (newObj.SemesterNo == 0)
            {
                int generateSemesterNo = GetSemesterNo(newObj.Id, newObj.SemesterNo, newObj.SemesterDurationEnumId);

                //If auto generate semester No clashes with existing then send a different msg.
                res = DbInstance.Aca_Semester.Any(x => x.Id != newObj.Id && x.SemesterNo == generateSemesterNo && x.SemesterDurationEnumId == newObj.SemesterDurationEnumId);
                if (res)
                {
                    error += $"While Auto generating Semester Order No, Newly Generated Semester Order No {generateSemesterNo} is Already Used Please free this Semester No and try again.";
                    return false;
                }

                newObj.SemesterNo = generateSemesterNo;
            }
            else
            {
                res = DbInstance.Aca_Semester.Any(x =>x.Id!=newObj.Id && x.SemesterNo == newObj.SemesterNo && x.SemesterDurationEnumId == newObj.SemesterDurationEnumId);
                if (res)
                {
                    error += $"Semester Order No {newObj.SemesterNo} is Already Used Please free this Semester No and try again.";
                    return false;
                }
            }
            

            res = DbInstance.Aca_Semester.Any(x => x.Id != newObj.Id
                         && x.Name == newObj.Name);
            if (res)
            {
                error = "Another Semester have same name in the system!";
                return false;
            }
            res = DbInstance.Aca_Semester.Any(x => x.Id != newObj.Id
                        && x.Year == newObj.Year && x.TermId == newObj.TermId);
            if (res)
            {
                error = "Another same Semester already exist for this year!";
                return false;
            }

            if (newObj.EnabledRegistrationType != Aca_Semester.EnabledRegistrationTypeEnum.None &&
                (!newObj.RegistrationStartDate.IsValid() || !newObj.RegistrationEndDate.IsValid()))
            {
                error = "While Registration is Enabled, You mast have to put Registration Start Date & End Date";
                return false;
            }


           
            if (newObj.RegistrationEndDate <= newObj.RegistrationStartDate)
            {
                error = "Registration End Date mast be greater then Registration Start Date";
                return false;
            }

            if (newObj.StatusEnumId == (byte)Aca_Semester.StatusEnum.Completed && newObj.IsOpenOnlineRegistration)
            {
                error = "You Cannot Enable Online Registration of a Completed Semester!";
                return false;
            }


            //checking registration duplicate registration enable or not
            if (newObj.StatusEnumId != (byte)Aca_Semester.StatusEnum.Completed && newObj.IsOpenOnlineRegistration)
            {
                res = DbInstance.Aca_Semester.Any(x => x.Id != newObj.Id
                        && x.StatusEnumId != (byte)Aca_Semester.StatusEnum.Completed && x.IsOpenOnlineRegistration && x.SemesterDurationEnumId == newObj.SemesterDurationEnumId);
                if (res)
                {
                    error = "Another Semester also has online Registration Open, disable that first!";
                    return false;
                }
            }

            //TODO write your custom validation here.
            if (newObj.StatusEnumId != (byte)Aca_Semester.StatusEnum.Ongoing)
            {
                res = DbInstance.Aca_Semester.Any(x => x.Id != newObj.Id
                                                       && x.StatusEnumId == (byte)Aca_Semester.StatusEnum.Ongoing && x.SemesterDurationEnumId == newObj.SemesterDurationEnumId);
                if (!res)
                {
                    error = "There Must be One Ongoing Semester Please check it and You can't Change the Status";
                    return false;
                }
            }
            

            ////Only 0/1 Semester will be registration open.
            //if (newObj.IsCloseOnlineRegistration)
            //{
            //    var openRegistrationList = DbInstance.Aca_Semester.Where(x => x.IsCloseOnlineRegistration).ToList();
            //    foreach (var openRegistration in openRegistrationList)
            //    {
            //        openRegistration.IsCloseOnlineRegistration = false;
            //        openRegistration.IsRegistrationSemester = false;
            //    }

            //    DbInstance.SaveChanges();
            //}
            return true;
        }
        private bool SaveSemesterLogic(Aca_Semester newObj, ref Aca_Semester dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Semester to save can't be null!";
                return false;
            }

            if (!IsValidToSaveSemester(newObj, out error))
                return false;

            bool isNewObject = true;
            Aca_Semester objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Aca_Semester.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
       
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                newObj.Id = long.Parse(newObj.Year + "" + newObj.TermId.ToStringPadLeft(2, '0') );
                objToSave = Aca_Semester.GetNew(newObj.Id);
                DbInstance.Aca_Semester.Add(objToSave);
                objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
                objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }
            //checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.SemesterManager.Semester.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.SemesterManager.Semester.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
            //Here Getting the semester No / order no of the semester.
            objToSave.SemesterNo = newObj.SemesterNo;
            objToSave.Name = newObj.Name;
            objToSave.TermId = newObj.TermId;
            objToSave.StartDate = newObj.StartDate;
            objToSave.EndDate = newObj.EndDate;
            objToSave.Year = newObj.Year;

            //todo
            //objToSave.AcademicSession = newObj.AcademicSession;
            objToSave.AcademicSession = (objToSave.Year - 1) + "-" + objToSave.Year;

            //If We made a semester Ongoing it will automatically update other Semesters.
            if ((objToSave.StatusEnumId != newObj.StatusEnumId) &&
                newObj.StatusEnumId == (byte) Aca_Semester.StatusEnum.Ongoing)
                UpdateOtherSemesterStatus(newObj.Id,objToSave.SemesterNo,newObj.SemesterDurationEnumId);//we are using semester no of obj


            objToSave.StatusEnumId = newObj.StatusEnumId;
            objToSave.IsEnable = newObj.IsEnable;
            objToSave.IsDeleted = newObj.IsDeleted;
            //new added
            objToSave.IsRegistrationSemester = newObj.IsOpenOnlineRegistration; // now is Registration semester will be the semeseter if online semester is open.
            objToSave.EnabledRegistrationTypeEnumId = newObj.EnabledRegistrationTypeEnumId;

            objToSave.RegistrationStartDate = newObj.RegistrationStartDate;
            objToSave.RegistrationEndDate = newObj.RegistrationEndDate;
            objToSave.IsEnableLateRegFee = newObj.IsEnableLateRegFee;
            objToSave.LateRegFeeStartDate = newObj.LateRegFeeStartDate;
            objToSave.LateRegFeeAmount = newObj.LateRegFeeAmount;
            objToSave.MaximumUGCredit = newObj.MaximumUGCredit;
            objToSave.MinimumUGCredit = newObj.MinimumUGCredit;
            objToSave.MaximumPGCredit = newObj.MaximumPGCredit;
            objToSave.MinimumPGCredit = newObj.MinimumPGCredit;
            objToSave.ExamPermitPublishDate = newObj.ExamPermitPublishDate;
            objToSave.CurrentDropPercentage = newObj.CurrentDropPercentage;
            objToSave.NewStudentIdPrefix = newObj.NewStudentIdPrefix;
            objToSave.NewStudentSuffix = newObj.NewStudentSuffix;
            objToSave.SectionTolarange = newObj.SectionTolarange;
            objToSave.IsTpeEnable = newObj.IsTpeEnable;
            objToSave.IsForceTpeSubmit = newObj.IsForceTpeSubmit;
            objToSave.MinimumGradePointForRetake = newObj.MinimumGradePointForRetake;

            //New columns.
            objToSave.StartingMonthEnumId = newObj.StartingMonthEnumId;
            objToSave.SemesterDurationEnumId = newObj.SemesterDurationEnumId;
            
            objToSave.IsOpenOnlineRegistration = newObj.IsOpenOnlineRegistration;
            objToSave.IsOpenBulkRegistration = newObj.IsOpenBulkRegistration;
            objToSave.IsCloseStudentProfileRegistration = newObj.IsCloseStudentProfileRegistration;
            objToSave.CanCancelCompletedSemesterCourse = newObj.CanCancelCompletedSemesterCourse;
            objToSave.RegistrationMessage = newObj.RegistrationMessage;
            //

            objToSave.LastChangedById = newObj.LastChangedById = HttpUtil.Profile.UserId;
            objToSave.LastChanged = newObj.LastChanged = DateTime.Now;
            dbAddedObj = objToSave;




            //res = DbInstance.Aca_Semester.Any(x => x.Id != newObj.Id
            //                                       && x.StatusEnumId == (byte)Aca_Semester.StatusEnum.Ongoing && x.SemesterDurationEnumId == newObj.SemesterDurationEnumId);
            
            ////TODO write your custom validation here.
            //if (newObj.StatusEnumId == (byte)Aca_Semester.StatusEnum.Ongoing)
            //{
            //    if (res)
            //    {
            //        var openRegistrationList = DbInstance.Aca_Semester.Where(x => x.StatusEnumId == (byte)Aca_Semester.StatusEnum.Ongoing && x.SemesterDurationEnumId == newObj.SemesterDurationEnumId).ToList();
            //        foreach (var openRegistration in openRegistrationList)
            //        {
            //            if (newObj.SemesterNo > openRegistration.SemesterNo)
            //            {
            //                openRegistration.StatusEnumId = (byte)Aca_Semester.StatusEnum.Completed;
            //            }
            //            else
            //            {
            //                openRegistration.StatusEnumId = (byte)Aca_Semester.StatusEnum.Upcomming;
            //            }
            //        }

            //        DbInstance.SaveChanges();
            //    }
            //}
            //else
            //{
            //    if (!res)
            //    {
            //        error = "There Must be One Ongoing Semester You can't Change the Status";
            //        return false;
            //    }
            //}
            //here save any child table
            return true;
        }

        private void UpdateOtherSemesterStatus(long semesterId,int semesterNo, int semesterDurationEnumId)
        {
            var semesterList = DbInstance.Aca_Semester
                .Where(x => x.Id != semesterId && x.SemesterDurationEnumId == semesterDurationEnumId).ToList();

            foreach (var semester in semesterList)
            {
                if (semesterNo <= semester.SemesterNo)
                {
                    semester.StatusEnumId = (byte) (byte) Aca_Semester.StatusEnum.Upcomming;
                }
                else
                {
                    semester.StatusEnumId = (byte)(byte)Aca_Semester.StatusEnum.Completed;
                    semester.IsOpenOnlineRegistration = false;
                }
            }

            DbInstance.SaveChanges();
        }

        private int GetSemesterNo(long semesterId, int semesterNo, int semesterDurationEnumId)
        {
            var semesterBeforeThisCount = 0;
            if (semesterId <= 0)
            {
                semesterBeforeThisCount = DbInstance.Aca_Semester.Count(x =>x.SemesterDurationEnumId == semesterDurationEnumId);
            }
            else
            {
                semesterBeforeThisCount = DbInstance.Aca_Semester.Count(x => x.Id < semesterId && x.SemesterDurationEnumId == semesterDurationEnumId);
            }
                
            return semesterBeforeThisCount+1;
        }
        #endregion
        #endregion
    }
}
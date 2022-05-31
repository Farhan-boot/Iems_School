//File: UI Controller
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Data;
using System.Web.WebPages;
using EMS.Framework;
using EMS.Framework.Attributes;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.CoreLibrary.Helpers;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases;
using EMS.Web.Framework.Bases.WebApi;
using EMS.Web.Jsons.Models;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;
using EMS.Framework.Settings;


namespace EMS.Web.UI.Areas.ExamSection.Controllers.WebApi
{

    public class ExamApiController : EmployeeBaseApiController
	{
        public ExamApiController()
        {  }
       
        #region Exam 
        #region Get Api
        /// <summary>
        /// 
        /// </summary>
        /// <param name="textkey">text to search</param>
        /// <param name="pageSize"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public HttpListResult<Aca_ExamJson> GetPagedExam(string textkey, int? pageSize, int? pageNo
           ,Int64 absentGradeId= 0      
           ,Int64 continuationGradeId= 0      
           ,Int64 maximumGradeForImproveExamGradeId= 0      
           ,Int64 maximumImproveExamAllowGradeId= 0      
           ,Int64 maximummGradeForRetakeCourseGradeId= 0      
           ,Int64 maximumRetakeCourseAllowGradeId= 0      
           ,Int64 semesterId= 0      
         )
        {
            string error = string.Empty;
            int count = 0;
            var result = new HttpListResult<Aca_ExamJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.ExamArea.ExamManager.Exam.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.ExamArea.ExamManager.Exam.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.ExamArea.ExamManager.Exam.CanEdit, out error)
            )
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }

            try
            {
                using (Aca_ExamDataService examDataService = new Aca_ExamDataService(DbInstance, HttpUtil.Profile))
                {
                  IQueryable<Aca_Exam> query = DbInstance.Aca_Exam
                      .Include(x => x.Aca_Semester)
                      .OrderByDescending(x => x.Aca_Semester.Year)
                      .ThenByDescending(x => x.Aca_Semester.TermId)
                      .ThenBy(x => x.ExamTypeEnumId); 

                    if (textkey.IsValid())
                    {
                        query = query.Where(q => q.Name.ToLower().Contains(textkey.ToLower()));
                    }
                    if (absentGradeId>0)
                    {
                        query = query.Where(q => q.AbsentGradeId== absentGradeId);
                    }  
                    if (continuationGradeId>0)
                    {
                        query = query.Where(q => q.ContinuationGradeId== continuationGradeId);
                    }  
                    if (maximumGradeForImproveExamGradeId>0)
                    {
                        query = query.Where(q => q.MaximumGradeForImproveExamGradeId== maximumGradeForImproveExamGradeId);
                    }  
                    if (maximumImproveExamAllowGradeId>0)
                    {
                        query = query.Where(q => q.MaximumImproveExamAllowGradeId== maximumImproveExamAllowGradeId);
                    }  
                    if (maximummGradeForRetakeCourseGradeId>0)
                    {
                        query = query.Where(q => q.MaximummGradeForRetakeCourseGradeId== maximummGradeForRetakeCourseGradeId);
                    }  
                    if (maximumRetakeCourseAllowGradeId>0)
                    {
                        query = query.Where(q => q.MaximumRetakeCourseAllowGradeId== maximumRetakeCourseAllowGradeId);
                    }  
                    if (semesterId>0)
                    {
                        query = query.Where(q => q.SemesterId== semesterId);
                    }  
                 
                    var entities = examDataService.GetPagedList(query, pageSize, pageNo, ref count, out error);
                    var jsons = new List<Aca_ExamJson>();
                    entities.Map(jsons);

                    result.DataExtra.SemesterList = DbInstance.Aca_Semester.AsEnumerable().Select(t => new
                        { Id = t.Id.ToString(), Name = t.Name }).ToList();

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
        /// <summary>
        /// Caution: Very Dangerous to call When table have huge data!
        /// </summary>
        /// <returns></returns>
        private HttpListResult<Aca_ExamJson> GetAllExam()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_ExamJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.Exam.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.Exam.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.Exam.CanEdit, out error))
            {
                result.HasError = true;
                 result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Aca_ExamDataService examDataService = new Aca_ExamDataService(DbInstance, HttpUtil.Profile))
                {
                    var jsons = new List<Aca_ExamJson>();
                    var entities = examDataService.GetAll(out error);
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
        public HttpResult<Aca_ExamJson> GetExamById(Int64 id = 0)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_ExamJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.ExamArea.ExamManager.Exam.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.ExamArea.ExamManager.Exam.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.ExamArea.ExamManager.Exam.CanEdit, out error)
            )
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_ExamDataService examDataService = new Aca_ExamDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Aca_ExamJson();
                    Aca_Exam entity;
                    if (id <= 0)
                    {
                        entity = Aca_Exam.GetNew();
                        entity.ExamSupple = Aca_ExamSupple.GetNew();
                    }
                    else
                    {
                        entity = examDataService.GetById(id);
                       
                        if (entity.ExamTypeEnumId==(byte)Aca_Exam.ExamTypeEnum.ImprovementMidTerm
                        || entity.ExamTypeEnumId==(byte)Aca_Exam.ExamTypeEnum.ImprovementFinalTerm)
                        {
                            entity.ExamSupple = DbInstance.Aca_ExamSupple.SingleOrDefault(x => x.ExamId == entity.Id);
                            if (entity.ExamSupple==null)
                            {
                                entity.ExamSupple = Aca_ExamSupple.GetNew();
                                entity.ExamSupple.ExamId = entity.Id;
                            }
                            
                        }
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
        private HttpResult<Aca_ExamJson> GetExamByIdWithChild(Int64 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_ExamJson>();
            //todo fix the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.Exam.CanView, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.Exam.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.Exam.CanEdit, out error))
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Aca_ExamDataService examDataService = new Aca_ExamDataService(DbInstance, HttpUtil.Profile))
                {
                    var json = new Aca_ExamJson();
                    Aca_Exam entity;
                    if (id <= 0)
                    {
                        entity = Aca_Exam.GetNew();
                    }
                    else
                    {
                        List<string> includeTables = new List<string>();
                        //Todo Implement latter
                        //includeTables.Add("Aca_Exam");
                        //includeTables.Add("Aca_Exam");

                        entity = examDataService.GetById(id, includeTables.ToArray());
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
        public HttpListResult<Aca_ExamJson> GetExamListDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_ExamJson>();
            try
            {
                //Aca_ExamDataService examDataService =
                //    new Aca_ExamDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.ProgramTypeEnumList = EnumUtil.GetEnumList(typeof(Aca_Program.ProgramTypeEnum));
                result.DataExtra.ExamTypeEnumList = EnumUtil.GetEnumList(typeof(Aca_Exam.ExamTypeEnum));
                //DropDown Option Tables

                //result.DataExtra.GradingPolicyOptionList = DbInstance.Aca_GradingPolicyOption.AsEnumerable().Select(t => new
                //{ Id = t.Id.ToString(),Name = t.Name }).ToList();
                result.DataExtra.SemesterList = DbInstance.Aca_Semester.AsEnumerable().Select(t => new
                { Id = t.Id.ToString(), Name = t.Name }).ToList();
            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        public HttpListResult<Aca_ExamJson> GetExamDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_ExamJson>();
            try
            {
                //Aca_ExamDataService examDataService =
                //    new Aca_ExamDataService(DbInstance, HttpUtil.Profile);
                //DropDown Option Enum List
                
                result.DataExtra.ProgramTypeEnumList = EnumUtil.GetEnumList(typeof(Aca_Program.ProgramTypeEnum));
                result.DataExtra.ExamTypeEnumList = EnumUtil.GetEnumList(typeof(Aca_Exam.ExamTypeEnum));
                //DropDown Option Tables

                result.DataExtra.GradingPolicyOptionList = DbInstance.Aca_GradingPolicyOption
                    .Include(x=>x.Aca_GradingPolicy)
                    .AsEnumerable().Select(t => new
                { Id = t.Id.ToString(), Name = $"{t.Grade} [{t.Aca_GradingPolicy.Name}]" }).ToList();

                result.DataExtra.SemesterList = DbInstance.Aca_Semester.AsEnumerable().Select(t => new
                { Id = t.Id.ToString(), Name = t.Name }).ToList();
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
        public HttpResult<Aca_ExamJson> SaveExam(Aca_ExamJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_ExamJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.ExamArea.ExamManager.Exam.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.ExamArea.ExamManager.Exam.CanEdit, out error)
            )
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                 var entityReceived = new Aca_Exam();
                 var dbAttachedEntity = new Aca_Exam();
                json.Map(entityReceived);
                using (DbContextTransaction scope = DbInstance.Database.BeginTransaction(IsolationLevel.ReadUncommitted))
                {
                    if (Facade.AcademicFacade.SemesterFacade.SaveExamLogic(entityReceived, ref dbAttachedEntity, out error))
                    {
                        DbInstance.SaveChanges();

                        if (entityReceived.ExamTypeEnumId==(byte)Aca_Exam.ExamTypeEnum.ImprovementMidTerm
                        || entityReceived.ExamTypeEnumId==(byte)Aca_Exam.ExamTypeEnum.ImprovementFinalTerm)
                        {
                            entityReceived.ExamSupple= Aca_ExamSupple.GetNew();
                            entityReceived.ExamSupple.ExamId = dbAttachedEntity.Id;

                            var entityReceivedExamSupple = new Aca_ExamSupple();
                            var dbAttachedEntityExamSupple = new Aca_ExamSupple();
                            json.ExamSuppleJson.Map(entityReceivedExamSupple);

                            if (entityReceivedExamSupple.IsOpenTheorySuppleApplyForStudent
                                || entityReceivedExamSupple.IsOpenNonTheorySuppleApplyForStudent)
                            {
                                var activeExamList = DbInstance.Aca_ExamSupple
                                    .Include(x => x.Aca_Exam)
                                    .Where(x =>
                                        x.Id != entityReceivedExamSupple.Id &&
                                        (x.IsOpenTheorySuppleApplyForStudent || x.IsOpenNonTheorySuppleApplyForStudent)).ToList();

                                if (activeExamList.Count > 0)
                                {
                                    var activeExam = activeExamList.FirstOrDefault();
                                    if (activeExam != null)
                                    {
                                        result.HasError = true;
                                        result.Errors.Add($"'{activeExam.Aca_Exam.Name}' this exam student application already open. Please close first, then try again.");
                                        return result;
                                    }
                                }
                            }
                            
                          

                            if (!SaveExamSuppleLogic(dbAttachedEntity, entityReceivedExamSupple, ref dbAttachedEntityExamSupple, out error))
                            {
                                result.HasError = true;
                                result.HasViewPermission = false;
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
                result.Errors.Add(ex.GetBaseException().ToString());
            }
            return result;
        }
        
        /*[HttpPost]
        private HttpResult<Aca_ExamJson> SaveExam2(Aca_ExamJson json)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_ExamJson>();
            try
            {
                using (Aca_ExamDataService examDataService = new Aca_ExamDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityReceived = new Aca_Exam();
                    var dbAttachedEntity = new Aca_Exam();
                    json.Map(entityReceived);
                    if (examDataService.Save(entityReceived, ref dbAttachedEntity, out error))
                    {
                        dbAttachedEntity.Map(json);
                        result.Data = json;//return object
                    }
                    else
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
        }*/
        /// <summary>
        /// Todo pending to implement bulk save
        /// </summary>
        /// <param name="jsonList"></param>
        /// <returns></returns>
        [HttpPost]
        private HttpListResult<Aca_ExamJson> SaveExamList(IList<Aca_ExamJson> jsonList)
        {
            string error = string.Empty;
            var result = new HttpListResult<Aca_ExamJson>();
            //todo enable it while you need the permission
            /*if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.Exam.CanAdd, out error)
                && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.Exam.CanEdit, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return result;
            }*/
            try
            {
                using (Aca_ExamDataService examDataService = new Aca_ExamDataService(DbInstance, HttpUtil.Profile))
                {
                    var entityListReceived = new List<Aca_Exam>();
                    var dbAttachedListEntity = new List<Aca_Exam>();
                    jsonList.Map(entityListReceived);

                    //foreach (var entity in entityListReceived)
                    //{

                    //}
                    //if (examDataService.Save(entity, ref dbAttachedListEntity, out error))
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
        public HttpResult<Aca_ExamJson> GetDeleteExamById(Int64 id)
        {
            string error = string.Empty;
            var result = new HttpResult<Aca_ExamJson>();
            if (!PermissionUtil.HasPermission(PermissionCollection.ExamArea.ExamManager.Exam.CanDelete, out error)
            )
            {
                result.HasError = true;
                result.HasViewPermission = false;
                result.Errors.Add(error);
                return result;
            }
            try
            {
                using (Aca_ExamDataService examDataService = new Aca_ExamDataService(DbInstance, HttpUtil.Profile))
                {
                    if (!examDataService.DeleteById(id, out error))
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

        #region Local Method (should be always private)
        

        #region Supple Exam

        private bool IsValidToSaveExamSupple(Aca_ExamSupple newObj, out string error)
        {
            error = "";
            /*if (newObj.ExamId <= 0)
            {
                error += $"Please select valid {SiteSettings.Instance.SuppleExamViewName} Exam.";
                return false;
            }*/
            if (newObj.IsOpenTheorySuppleApplyForStudent == null)
            {
                error += "Is Open Theory Supple Apply For Student required.";
                return false;
            }
            if (newObj.IsOpenNonTheorySuppleApplyForStudent == null)
            {
                error += "Is Open Non Theory Supple Apply For Student required.";
                return false;
            }
            if (!newObj.SuppleApplicationLastDate.IsValid())
            {
                error += "Supple Application Last Date is not valid.";
                return false;
            }
            if (newObj.IsOpenTheorySuppleApplyForAdmin == null)
            {
                error += "Is Open Theory Supple Apply For Admin required.";
                return false;
            }
            if (newObj.IsOpenNonTheorySuppleApplyForAdmin == null)
            {
                error += "Is Open Non Theory Supple Apply For Admin required.";
                return false;
            }
            if (newObj.SuppleApplyAllowedPaymentDueUpto == null)
            {
                error += "Supple Apply Allowed Payment Due Upto required.";
                return false;
            }
            if (newObj.MaxTimeCanApplyForOneTheory == null)
            {
                error += "Max Time Can Apply For One Theory required.";
                return false;
            }
            if (newObj.MaxTimeCanApplyForOneNonTheory == null)
            {
                error += "Max Time Can Apply For One Non Theory required.";
                return false;
            }
            if (newObj.MaxTimeCanApplyForOneTheory<=0)
            {
                error += "Max Time Can Apply For One Theory required Minimum 1.";
                return false;
            }
            if (newObj.MaxTimeCanApplyForOneNonTheory <=0)
            {
                error += "Max Time Can Apply For One Non Theory required Minimum 1.";
                return false;
            }


            if (newObj.ReferredSuppleExamFee == null)
            {
                error += "Referred Supple Exam Fee required.";
                return false;
            }
            if (newObj.BackLogSuppleExamFee == null)
            {
                error += "Back Log Supple Exam Fee required.";
                return false;
            }
            if (newObj.IsPublishedSuppleAdmitCard == null)
            {
                error += "Is Published Supple Admit Card required.";
                return false;
            }
            if (newObj.SuppleAdmitCardDownloadPaymentDueUpto == null)
            {
                error += "Supple Admit Card Download Payment Due Upto required.";
                return false;
            }
            if (newObj.IsOpenTheorySuppleMarkInput == null)
            {
                error += "Is Open Theory Supple Mark Input required.";
                return false;
            }
            if (newObj.IsOpenNonTheorySuppleMarkInput == null)
            {
                error += "Is Open Non Theory Supple Mark Input required.";
                return false;
            }
            if (newObj.AutoGradeGraceMarkForPass == null)
            {
                error += "Auto Grade Grace Mark For Pass required.";
                return false;
            }
            if (newObj.IsPublishTheorySuppleResult == null)
            {
                error += "Is Publish Theory Supple Result required.";
                return false;
            }
            if (newObj.IsPublishNonTheorySuppleResult == null)
            {
                error += "Is Publish Non Theory Supple Result required.";
                return false;
            }
            if (newObj.SuppleResultAllowedPaymentDueUpto == null)
            {
                error += "Supple Result Allowed Payment Due Upto required.";
                return false;
            }
            if (!newObj.StudentsInstructionsForApply.IsValid())
            {
                error += "Students Instructions For Apply is not valid.";
                return false;
            }
            if (newObj.StudentsInstructionsForApply == null)
            {
                error += "Students Instructions For Apply required.";
                return false;
            }
            if (!newObj.FacultyInstructionsForMarkEntry.IsValid())
            {
                error += "Faculty Instructions For Mark Entry is not valid.";
                return false;
            }
            if (newObj.FacultyInstructionsForMarkEntry == null)
            {
                error += "Faculty Instructions For Mark Entry required.";
                return false;
            }
            if (!newObj.StudentApplyConfirmationMessage.IsValid())
            {
                error += "Student Apply Confirmation Message is not valid.";
                return false;
            }
            if (newObj.StudentApplyConfirmationMessage == null)
            {
                error += "Student Apply Confirmation Message required.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.Aca_ExamSupple.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
            //error = "A ExamSupple already exists!";
            //return false;
            //}
            return true;
        }
        private bool SaveExamSuppleLogic(Aca_Exam exam, Aca_ExamSupple newObj, ref Aca_ExamSupple dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Exam Supple to save can't be null!";
                return false;
            }

            if (!IsValidToSaveExamSupple(newObj, out error))
                return false;

            bool isNewObject = true;
            Aca_ExamSupple objToSave = null;

            objToSave = DbInstance.Aca_ExamSupple.SingleOrDefault(x =>  x.ExamId == exam.Id);//x.Id == newObj.Id &&

            if (newObj.Id != 0)
            {
                if (objToSave != null)
                {
                    exam.History += MakeHistoryForSuppleExam(objToSave, newObj);
                }
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Aca_ExamSupple.GetNew(newObj.Id);
                DbInstance.Aca_ExamSupple.Add(objToSave);
            }
            else
            {
                isNewObject = false;
            }
            //todo mandatory fix checking permission, enable it with correction
            /*if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ExamSupple.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AcademicArea.ExamSupple.CanEdit,
                   out error))
            {
                return false;
            }*/

            //binding object  
            objToSave.ExamId = exam.Id;
            objToSave.IsOpenTheorySuppleApplyForStudent = newObj.IsOpenTheorySuppleApplyForStudent;
            objToSave.IsOpenNonTheorySuppleApplyForStudent = newObj.IsOpenNonTheorySuppleApplyForStudent;
            objToSave.SuppleApplicationLastDate = newObj.SuppleApplicationLastDate;
            objToSave.IsOpenTheorySuppleApplyForAdmin = newObj.IsOpenTheorySuppleApplyForAdmin;
            objToSave.IsOpenNonTheorySuppleApplyForAdmin = newObj.IsOpenNonTheorySuppleApplyForAdmin;
            objToSave.SuppleApplyAllowedPaymentDueUpto = newObj.SuppleApplyAllowedPaymentDueUpto;
            objToSave.MaxTimeCanApplyForOneTheory = newObj.MaxTimeCanApplyForOneTheory;
            objToSave.MaxTimeCanApplyForOneNonTheory = newObj.MaxTimeCanApplyForOneNonTheory;
            objToSave.ReferredSuppleExamFee = newObj.ReferredSuppleExamFee;
            objToSave.BackLogSuppleExamFee = newObj.BackLogSuppleExamFee;
            objToSave.IsPublishedSuppleAdmitCard = newObj.IsPublishedSuppleAdmitCard;
            objToSave.SuppleAdmitCardDownloadPaymentDueUpto = newObj.SuppleAdmitCardDownloadPaymentDueUpto;
            objToSave.IsOpenTheorySuppleMarkInput = newObj.IsOpenTheorySuppleMarkInput;
            objToSave.IsOpenNonTheorySuppleMarkInput = newObj.IsOpenNonTheorySuppleMarkInput;
            objToSave.AutoGradeGraceMarkForPass = newObj.AutoGradeGraceMarkForPass;
            
            objToSave.SuppleResultAllowedPaymentDueUpto = newObj.SuppleResultAllowedPaymentDueUpto;
            objToSave.StudentsInstructionsForApply = newObj.StudentsInstructionsForApply;
            objToSave.FacultyInstructionsForMarkEntry = newObj.FacultyInstructionsForMarkEntry;
            objToSave.StudentApplyConfirmationMessage = newObj.StudentApplyConfirmationMessage;

            if (PermissionUtil.HasPermission(PermissionCollection.ExamArea.ExamManager.Exam.CanEditResultPublishingSettings))
            {
                objToSave.IsPublishTheorySuppleResult = newObj.IsPublishTheorySuppleResult;
                objToSave.IsPublishNonTheorySuppleResult = newObj.IsPublishNonTheorySuppleResult;
            }


            dbAddedObj = objToSave;

            //here save any child table
            return true;
        }

        #endregion

        private string MakeHistoryForSuppleExam(Aca_ExamSupple dbObj, Aca_ExamSupple newObj)
        {
            string history = String.Empty;

            //binding object  


            if (dbObj.IsOpenTheorySuppleApplyForStudent != newObj.IsOpenTheorySuppleApplyForStudent)
            {
                history +=
                    $"IsOpenTheorySuppleApplyForStudent: {dbObj.IsOpenTheorySuppleApplyForStudent} to {newObj.IsOpenTheorySuppleApplyForStudent};";
            }

            if (dbObj.IsOpenNonTheorySuppleApplyForStudent != newObj.IsOpenNonTheorySuppleApplyForStudent)
            {
                history +=
                    $"IsOpenNonTheorySuppleApplyForStudent : {dbObj.IsOpenNonTheorySuppleApplyForStudent } to {newObj.IsOpenNonTheorySuppleApplyForStudent };";
            }

            if (dbObj.SuppleApplicationLastDate != newObj.SuppleApplicationLastDate)
            {
                history +=
                    $"SuppleApplicationLastDate : {dbObj.SuppleApplicationLastDate } to {newObj.SuppleApplicationLastDate };";
            }

            if (dbObj.IsOpenTheorySuppleApplyForAdmin != newObj.IsOpenTheorySuppleApplyForAdmin)
            {
                history +=
                    $"IsOpenTheorySuppleApplyForAdmin : {dbObj.IsOpenTheorySuppleApplyForAdmin } to {newObj.IsOpenTheorySuppleApplyForAdmin };";
            }

            if (dbObj.IsOpenNonTheorySuppleApplyForAdmin != newObj.IsOpenNonTheorySuppleApplyForAdmin)
            {
                history +=
                    $"IsOpenNonTheorySuppleApplyForAdmin : {dbObj.IsOpenNonTheorySuppleApplyForAdmin } to {newObj.IsOpenNonTheorySuppleApplyForAdmin };";
            }

            if (dbObj.SuppleApplyAllowedPaymentDueUpto != newObj.SuppleApplyAllowedPaymentDueUpto)
            {
                history +=
                    $"SuppleApplyAllowedPaymentDueUpto : {dbObj.SuppleApplyAllowedPaymentDueUpto } to {newObj.SuppleApplyAllowedPaymentDueUpto };";
            }

            if (dbObj.MaxTimeCanApplyForOneTheory != newObj.MaxTimeCanApplyForOneTheory)
            {
                history +=
                    $"MaxTimeCanApplyForOneTheory : {dbObj.MaxTimeCanApplyForOneTheory } to {newObj.MaxTimeCanApplyForOneTheory };";
            }

            if (dbObj.MaxTimeCanApplyForOneNonTheory != newObj.MaxTimeCanApplyForOneNonTheory)
            {
                history +=
                    $"MaxTimeCanApplyForOneNonTheory : {dbObj.MaxTimeCanApplyForOneNonTheory } to {newObj.MaxTimeCanApplyForOneNonTheory };";
            }

            if (dbObj.ReferredSuppleExamFee != newObj.ReferredSuppleExamFee)
            {
                history +=
                    $"ReferredSuppleExamFee : {dbObj.ReferredSuppleExamFee } to {newObj.ReferredSuppleExamFee };";
            }

            if (dbObj.BackLogSuppleExamFee != newObj.BackLogSuppleExamFee)
            {
                history +=
                    $"BackLogSuppleExamFee : {dbObj.BackLogSuppleExamFee } to {newObj.BackLogSuppleExamFee };";
            }

            if (dbObj.IsPublishedSuppleAdmitCard != newObj.IsPublishedSuppleAdmitCard)
            {
                history +=
                    $"IsPublishedSuppleAdmitCard : {dbObj.IsPublishedSuppleAdmitCard } to {newObj.IsPublishedSuppleAdmitCard };";
            }

            if (dbObj.SuppleAdmitCardDownloadPaymentDueUpto != newObj.SuppleAdmitCardDownloadPaymentDueUpto)
            {
                history +=
                    $"SuppleAdmitCardDownloadPaymentDueUpto : {dbObj.SuppleAdmitCardDownloadPaymentDueUpto } to {newObj.SuppleAdmitCardDownloadPaymentDueUpto };";
            }

            if (dbObj.IsOpenTheorySuppleMarkInput != newObj.IsOpenTheorySuppleMarkInput)
            {
                history +=
                    $"IsOpenTheorySuppleMarkInput : {dbObj.IsOpenTheorySuppleMarkInput } to {newObj.IsOpenTheorySuppleMarkInput };";
            }

            if (dbObj.IsOpenNonTheorySuppleMarkInput != newObj.IsOpenNonTheorySuppleMarkInput)
            {
                history +=
                    $"IsOpenNonTheorySuppleMarkInput : {dbObj.IsOpenNonTheorySuppleMarkInput } to {newObj.IsOpenNonTheorySuppleMarkInput };";
            }

            if (dbObj.AutoGradeGraceMarkForPass != newObj.AutoGradeGraceMarkForPass)
            {
                history +=
                    $"AutoGradeGraceMarkForPass : {dbObj.AutoGradeGraceMarkForPass } to {newObj.AutoGradeGraceMarkForPass };";
            }

            if (dbObj.IsPublishTheorySuppleResult != newObj.IsPublishTheorySuppleResult)
            {
                history +=
                    $"IsPublishTheorySuppleResult : {dbObj.IsPublishTheorySuppleResult } to {newObj.IsPublishTheorySuppleResult };";
            }

            if (dbObj.IsPublishNonTheorySuppleResult != newObj.IsPublishNonTheorySuppleResult)
            {
                history +=
                    $"IsPublishNonTheorySuppleResult : {dbObj.IsPublishNonTheorySuppleResult } to {newObj.IsPublishNonTheorySuppleResult };";
            }

            if (dbObj.SuppleResultAllowedPaymentDueUpto != newObj.SuppleResultAllowedPaymentDueUpto)
            {
                history +=
                    $"SuppleResultAllowedPaymentDueUpto : {dbObj.SuppleResultAllowedPaymentDueUpto } to {newObj.SuppleResultAllowedPaymentDueUpto };";
            }


            if (!history.IsEmpty())
            {
                // history=$"{history}"

                history = $"{DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")} Changed {history}  By {HttpUtil.Profile.Name}[{HttpUtil.Profile.UserName}], IP:{HttpUtil.GetUserIp()}.<br>";

            }

            return history;
        }

        #endregion
        #endregion

    }
}

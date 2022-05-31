using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebPages;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.Facade.Library;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;
using EMS.Framework.Settings;
using EMS.Framework.Utils;
using EMS.Web.Jsons.Custom.Admission.AdmissionExam;
using EMS.Web.Jsons.Models;
using Newtonsoft.Json;

namespace EMS.Facade.Academic
{
    public class SemesterFacade : BaseFacade
    {
        public SemesterFacade(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
        }

        #region Semester
        public long GetCurrentSemesterId()
        {
            long id = 0;
            var semester = DbInstance.Aca_Semester.FirstOrDefault(x => x.StatusEnumId == (byte)Aca_Semester.StatusEnum.Ongoing);
            if (semester !=null)
            {
                 id =semester.Id;
            }

            return id;
        }

        #endregion Semester

        #region Exam

        private bool IsValidToSaveExam(Aca_Exam newObj, out string error)
        {
            error = "";
            if (!newObj.Name.IsValid())
            {
                error += "Name is not valid.";
                return false;
            }
            if (newObj.Name.Length > 255)
            {
                error += "Name exceeded its maximum length 255.";
                return false;
            }
            if (!newObj.ShortName.IsValid())
            {
                error += "Short Name is not valid.";
                return false;
            }
            if (newObj.ShortName.Length > 128)
            {
                error += "Short Name exceeded its maximum length 128.";
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
            if (newObj.SemesterId <= 0)
            {
                error += "Please select valid Semester.";
                return false;
            }
            if (newObj.ProgramTypeEnumId == null)
            {
                error += "Please select valid Program Type.";
                return false;
            }
            if (newObj.ExamTypeEnumId == null)
            {
                error += "Please select valid Exam Type.";
                return false;
            }
            if (!newObj.StartDateOfTermFinalExam.IsValid())
            {
                error += "Start Date Of Term Final Exam is not valid.";
                return false;
            }
            if (!newObj.EndDateOfTermFinalExam.IsValid())
            {
                error += "End Date Of Term Final Exam is not valid.";
                return false;
            }
            if (!newObj.DateOfTermFinalResultPublication.IsValid())
            {
                error += "Date Of Term Final Result Publication is not valid.";
                return false;
            }

            if (newObj.IsDeleted == null)
            {
                error += "Is Deleted required.";
                return false;
            }

            if (!newObj.OfficialExamName.IsValid())
            {
                error += "Official Exam Name is not valid.";
                return false;
            }
            if (newObj.OfficialExamName.Length > 255)
            {
                error += "Official Exam Name exceeded its maximum length 255.";
                return false;
            }
            if (newObj.StartDateOfMidtermExam != null && !newObj.StartDateOfMidtermExam.IsValid())
            {
                newObj.StartDateOfMidtermExam = null;//just put null,if its nullable and not valid.
                                                     //error += "Start Date Of Midterm Exam is not valid.";
                                                     //return false;
            }
            if (newObj.EndDateOfMidtermExam != null && !newObj.EndDateOfMidtermExam.IsValid())
            {
                newObj.EndDateOfMidtermExam = null;//just put null,if its nullable and not valid.
                                                   //error += "End Date Of Midterm Exam is not valid.";
                                                   //return false;
            }
            if (newObj.DateOfMidtermResultPublication != null && !newObj.DateOfMidtermResultPublication.IsValid())
            {
                newObj.DateOfMidtermResultPublication = null;//just put null,if its nullable and not valid.
                                                             //error += "Date Of Midterm Result Publication is not valid.";
                                                             //return false;
            }
            if (newObj.IsPublishTheoryContinuousAssessmentMarks == null)
            {
                error += "Is Publish Theory Continuous Assessment Marks required.";
                return false;
            }
            if (newObj.IsPublishNonTheoryContinuousAssessmentMarks == null)
            {
                error += "Is Publish Non Theory Continuous Assessment Marks required.";
                return false;
            }
            if (newObj.ContinuousAssessmentMarksAllowedPaymentDueUpto == null)
            {
                error += "Continuous Assessment Marks Allowed Payment Due Upto required.";
                return false;
            }
            if (newObj.IsPublishTheoryMidtermResult == null)
            {
                error += "Is Publish Theory Midterm Result required.";
                return false;
            }
            if (newObj.IsPublishNonTheoryMidtermResult == null)
            {
                error += "Is Publish Non Theory Midterm Result required.";
                return false;
            }
            if (newObj.MidtermResultAllowedPaymentDueUpto == null)
            {
                error += "Midterm Result Allowed Payment Due Upto required.";
                return false;
            }
            if (newObj.IsPublishTheoryFinalTermResult == null)
            {
                error += "Is Publish Theory Final Term Result required.";
                return false;
            }
            if (newObj.IsPublishNonTheoryFinalTermResult == null)
            {
                error += "Is Publish Non Theory Final Term Result required.";
                return false;
            }
            if (newObj.FinalTermResultAllowedPaymentDueUpto == null)
            {
                error += "Final Term Result Allowed Payment Due Upto required.";
                return false;
            }
            if (newObj.IsPublishedMidtermAdmitCard == null)
            {
                error += "Is Published Midterm Admit Card required.";
                return false;
            }
            if (newObj.MidtermAdmitCardDownloadPaymentDueUpto == null)
            {
                error += "Midterm Admit Card Download Payment Due Upto required.";
                return false;
            }
            if (newObj.IsPublishedFinalTermAdmitCard == null)
            {
                error += "Is Published Final Term Admit Card required.";
                return false;
            }
            if (newObj.FinalTermAdmitCardDownloadPaymentDueUpto == null)
            {
                error += "Final Term Admit Card Download Payment Due Upto required.";
                return false;
            }
            if (newObj.IsOpenTheoryContinuousAssessmentMarkInput == null)
            {
                error += "Is Open Theory Continuous Assessment Mark Input required.";
                return false;
            }
            if (newObj.IsOpenNonTheoryContinuousAssessmentMarkInput == null)
            {
                error += "Is Open Non Theory Continuous Assessment Mark Input required.";
                return false;
            }
            if (newObj.IsOpenTheoryMidTermMarkInput == null)
            {
                error += "Is Open Theory Mid Term Mark Input required.";
                return false;
            }
            if (newObj.IsOpenNonTheoryMidTermMarkInput == null)
            {
                error += "Is Open Non Theory Mid Term Mark Input required.";
                return false;
            }
            if (newObj.IsOpenTheoryFinalTermMarkInput == null)
            {
                error += "Is Open Theory Final Term Mark Input required.";
                return false;
            }
            if (newObj.IsOpenNonTheoryFinalTermMarkInput == null)
            {
                error += "Is Open Non Theory Final Term Mark Input required.";
                return false;
            }
            if (newObj.AutoGradeGraceMark == null)
            {
                error += "Auto Grade Grace Mark required.";
                return false;
            }
            if (newObj.AbsentGradeId <= 0)
            {
                error += "Please select valid Absent Grade.";
                return false;
            }
            if (!newObj.AbsentMarkSymbol.IsValid())
            {
                error += "Absent Mark Symbol is not valid.";
                return false;
            }
            if (newObj.AbsentMarkSymbol.Length > 5)
            {
                error += "Absent Mark Symbol exceeded its maximum length 5.";
                return false;
            }
            if (newObj.ContinuationGradeId <= 0)
            {
                error += "Please select valid Continuation Grade.";
                return false;
            }
            if (newObj.MaximumImproveExamAllowGradeId <= 0)
            {
                error += "Please select valid Maximum Improve Exam Allow Grade.";
                return false;
            }
            if (newObj.MaximumRetakeCourseAllowGradeId <= 0)
            {
                error += "Please select valid Maximum Retake Course Allow Grade.";
                return false;
            }
            if (newObj.MaximumGradeForImproveExamGradeId <= 0)
            {
                error += "Please select valid Maximum Grade For Improve Exam Grade.";
                return false;
            }
            if (newObj.MaximummGradeForRetakeCourseGradeId <= 0)
            {
                error += "Please select valid Maximumm Grade For Retake Course Grade.";
                return false;
            }
            //TODO write your custom validation here.
            //var res =  DbInstance.Aca_Exam.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
            //error = "A Exam already exists!";
            //return false;
            //}

            if (newObj.ContinuousAssessmentMarksAllowedPaymentDueUpto < -1)
            {
                error += "Please give continuous assessment marks allowed payment due amount -1 or greater than.";
                return false;
            }
            if (newObj.MidtermResultAllowedPaymentDueUpto < -1)
            {
                error += "Please give midterm result allowed payment due amount -1 or greater than.";
                return false;
            }
            if (newObj.FinalTermResultAllowedPaymentDueUpto < -1)
            {
                error += "Please give final term result allowed payment due amount -1 or greater than.";
                return false;
            }
            if (newObj.MidtermAdmitCardDownloadPaymentDueUpto < -1)
            {
                error += "Please give midterm admit card download payment due amount -1 or greater than.";
                return false;
            }
            if (newObj.FinalTermAdmitCardDownloadPaymentDueUpto < -1)
            {
                error += "Please give final term admit card download payment due amount -1 or greater than.";
                return false;
            }

            return true;
        }
        public bool SaveExamLogic(Aca_Exam newObj, ref Aca_Exam dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Exam to save can't be null!";
                return false;
            }

            if (!IsValidToSaveExam(newObj, out error))
                return false;

            bool isNewObject = true;
            Aca_Exam objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Aca_Exam.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
                if (objToSave != null)
                {
                    objToSave.History += MakeHistory(objToSave, newObj);
                }



            }
            else
            {
                newObj.Id = BigInt.NewBigInt();
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Aca_Exam.GetNew(newObj.Id);
                DbInstance.Aca_Exam.Add(objToSave);
                objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
                objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }

            if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.ExamArea.ExamManager.Exam.CanAdd,
                out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.ExamArea.ExamManager.Exam.CanEdit,
                out error))
            {
                return false;
            }

            //binding object  
            objToSave.Name = newObj.Name;
            objToSave.ShortName = newObj.ShortName;
            objToSave.AcademicSession = newObj.AcademicSession;
            objToSave.SemesterId = newObj.SemesterId;
            objToSave.ProgramTypeEnumId = newObj.ProgramTypeEnumId;
            objToSave.ExamTypeEnumId = newObj.ExamTypeEnumId;
            objToSave.StartDateOfTermFinalExam = newObj.StartDateOfTermFinalExam;
            objToSave.EndDateOfTermFinalExam = newObj.EndDateOfTermFinalExam;
            objToSave.DateOfTermFinalResultPublication = newObj.DateOfTermFinalResultPublication;
            objToSave.Remarks = newObj.Remarks;
            objToSave.IsDeleted = newObj.IsDeleted;

            //objToSave.History =  newObj.History ;

            objToSave.OfficialExamName = newObj.OfficialExamName;
            objToSave.StartDateOfMidtermExam = newObj.StartDateOfMidtermExam;
            objToSave.EndDateOfMidtermExam = newObj.EndDateOfMidtermExam;
            objToSave.DateOfMidtermResultPublication = newObj.DateOfMidtermResultPublication;

            objToSave.ContinuousAssessmentMarksAllowedPaymentDueUpto = newObj.ContinuousAssessmentMarksAllowedPaymentDueUpto;

            objToSave.MidtermResultAllowedPaymentDueUpto = newObj.MidtermResultAllowedPaymentDueUpto;

            objToSave.FinalTermResultAllowedPaymentDueUpto = newObj.FinalTermResultAllowedPaymentDueUpto;
            objToSave.IsPublishedMidtermAdmitCard = newObj.IsPublishedMidtermAdmitCard;

            objToSave.MidtermAdmitCardDownloadPaymentDueUpto = newObj.MidtermAdmitCardDownloadPaymentDueUpto;
            objToSave.IsPublishedFinalTermAdmitCard = newObj.IsPublishedFinalTermAdmitCard;
            objToSave.FinalTermAdmitCardDownloadPaymentDueUpto = newObj.FinalTermAdmitCardDownloadPaymentDueUpto;
            objToSave.IsOpenTheoryContinuousAssessmentMarkInput = newObj.IsOpenTheoryContinuousAssessmentMarkInput;

            objToSave.IsOpenNonTheoryContinuousAssessmentMarkInput = newObj.IsOpenNonTheoryContinuousAssessmentMarkInput;
            objToSave.IsOpenTheoryMidTermMarkInput = newObj.IsOpenTheoryMidTermMarkInput;
            objToSave.IsOpenNonTheoryMidTermMarkInput = newObj.IsOpenNonTheoryMidTermMarkInput;
            objToSave.IsOpenTheoryFinalTermMarkInput = newObj.IsOpenTheoryFinalTermMarkInput;
            objToSave.IsOpenNonTheoryFinalTermMarkInput = newObj.IsOpenNonTheoryFinalTermMarkInput;

            objToSave.LastChangedById = newObj.LastChangedById = HttpUtil.Profile.UserId;
            objToSave.LastChanged = newObj.LastChanged = DateTime.Now;

            if (PermissionUtil.HasPermission(PermissionCollection.ExamArea.ExamManager.Exam.CanEditResultPublishingSettings))
            {
                objToSave.IsPublishTheoryContinuousAssessmentMarks = newObj.IsPublishTheoryContinuousAssessmentMarks;
                objToSave.IsPublishNonTheoryContinuousAssessmentMarks = newObj.IsPublishNonTheoryContinuousAssessmentMarks;

                objToSave.IsPublishTheoryMidtermResult = newObj.IsPublishTheoryMidtermResult;
                objToSave.IsPublishNonTheoryMidtermResult = newObj.IsPublishNonTheoryMidtermResult;

                objToSave.IsPublishTheoryFinalTermResult = newObj.IsPublishTheoryFinalTermResult;
                objToSave.IsPublishNonTheoryFinalTermResult = newObj.IsPublishNonTheoryFinalTermResult;
            }

            if (HttpUtil.IsSupperAdmin())
            {
                objToSave.AutoGradeGraceMark = newObj.AutoGradeGraceMark;
                objToSave.AbsentGradeId = newObj.AbsentGradeId;
                objToSave.AbsentMarkSymbol = newObj.AbsentMarkSymbol;
                objToSave.ContinuationGradeId = newObj.ContinuationGradeId;
                objToSave.MaximumImproveExamAllowGradeId = newObj.MaximumImproveExamAllowGradeId;
                objToSave.MaximumRetakeCourseAllowGradeId = newObj.MaximumRetakeCourseAllowGradeId;
                objToSave.MaximumGradeForImproveExamGradeId = newObj.MaximumGradeForImproveExamGradeId;
                objToSave.MaximummGradeForRetakeCourseGradeId = newObj.MaximummGradeForRetakeCourseGradeId;
            }


            dbAddedObj = objToSave;

            //here save any child table
            return true;
        }
        private string MakeHistory(Aca_Exam dbObj, Aca_Exam newObj)
        {
            string history = String.Empty;

            //binding object  


            if (dbObj.IsPublishTheoryContinuousAssessmentMarks != newObj.IsPublishTheoryContinuousAssessmentMarks)
            {
                history +=
                    $"IsPublishTheoryContinuousAssessmentMarks: {dbObj.IsPublishTheoryContinuousAssessmentMarks} to {newObj.IsPublishTheoryContinuousAssessmentMarks};";
            }

            if (dbObj.IsPublishNonTheoryContinuousAssessmentMarks != newObj.IsPublishNonTheoryContinuousAssessmentMarks)
            {
                history +=
                    $"IsPublishNonTheoryContinuousAssessmentMarks: {dbObj.IsPublishNonTheoryContinuousAssessmentMarks} to {newObj.IsPublishNonTheoryContinuousAssessmentMarks};";
            }

            if (dbObj.ContinuousAssessmentMarksAllowedPaymentDueUpto != newObj.ContinuousAssessmentMarksAllowedPaymentDueUpto)
            {
                history +=
                    $"ContinuousAssessmentMarksAllowedPaymentDueUpto: {dbObj.ContinuousAssessmentMarksAllowedPaymentDueUpto} to {newObj.ContinuousAssessmentMarksAllowedPaymentDueUpto};";
            }

            if (dbObj.IsPublishTheoryMidtermResult != newObj.IsPublishTheoryMidtermResult)
            {
                history +=
                    $"IsPublishTheoryMidtermResult: {dbObj.IsPublishTheoryMidtermResult} to {newObj.IsPublishTheoryMidtermResult};";
            }

            if (dbObj.IsPublishNonTheoryMidtermResult != newObj.IsPublishNonTheoryMidtermResult)
            {
                history +=
                    $"IsPublishNonTheoryMidtermResult: {dbObj.IsPublishNonTheoryMidtermResult} to {newObj.IsPublishNonTheoryMidtermResult};";
            }

            if (dbObj.MidtermResultAllowedPaymentDueUpto != newObj.MidtermResultAllowedPaymentDueUpto)
            {
                history +=
                    $"MidtermResultAllowedPaymentDueUpto: {dbObj.MidtermResultAllowedPaymentDueUpto} to {newObj.MidtermResultAllowedPaymentDueUpto};";
            }

            if (dbObj.IsPublishTheoryFinalTermResult != newObj.IsPublishTheoryFinalTermResult)
            {
                history +=
                    $"IsPublishTheoryFinalTermResult: {dbObj.IsPublishTheoryFinalTermResult} to {newObj.IsPublishTheoryFinalTermResult};";
            }

            if (dbObj.IsPublishNonTheoryFinalTermResult != newObj.IsPublishNonTheoryFinalTermResult)
            {
                history +=
                    $"IsPublishNonTheoryFinalTermResult: {dbObj.IsPublishNonTheoryFinalTermResult} to {newObj.IsPublishNonTheoryFinalTermResult};";
            }

            if (dbObj.FinalTermResultAllowedPaymentDueUpto != newObj.FinalTermResultAllowedPaymentDueUpto)
            {
                history +=
                    $"FinalTermResultAllowedPaymentDueUpto: {dbObj.FinalTermResultAllowedPaymentDueUpto} to {newObj.FinalTermResultAllowedPaymentDueUpto};";
            }

            if (dbObj.IsPublishedMidtermAdmitCard != newObj.IsPublishedMidtermAdmitCard)
            {
                history +=
                    $"IsPublishedMidtermAdmitCard: {dbObj.IsPublishedMidtermAdmitCard} to {newObj.IsPublishedMidtermAdmitCard};";
            }

            if (dbObj.MidtermAdmitCardDownloadPaymentDueUpto != newObj.MidtermAdmitCardDownloadPaymentDueUpto)
            {
                history +=
                    $"MidtermAdmitCardDownloadPaymentDueUpto: {dbObj.MidtermAdmitCardDownloadPaymentDueUpto} to {newObj.MidtermAdmitCardDownloadPaymentDueUpto};";
            }

            if (dbObj.IsPublishedFinalTermAdmitCard != newObj.IsPublishedFinalTermAdmitCard)
            {
                history +=
                    $"IsPublishedFinalTermAdmitCard: {dbObj.IsPublishedFinalTermAdmitCard} to {newObj.IsPublishedFinalTermAdmitCard};";
            }

            if (dbObj.FinalTermAdmitCardDownloadPaymentDueUpto != newObj.FinalTermAdmitCardDownloadPaymentDueUpto)
            {
                history +=
                    $"FinalTermAdmitCardDownloadPaymentDueUpto: {dbObj.FinalTermAdmitCardDownloadPaymentDueUpto} to {newObj.FinalTermAdmitCardDownloadPaymentDueUpto};";
            }

            if (dbObj.IsOpenTheoryContinuousAssessmentMarkInput != newObj.IsOpenTheoryContinuousAssessmentMarkInput)
            {
                history +=
                    $"IsOpenTheoryContinuousAssessmentMarkInput: {dbObj.IsOpenTheoryContinuousAssessmentMarkInput} to {newObj.IsOpenTheoryContinuousAssessmentMarkInput};";
            }

            if (dbObj.IsOpenNonTheoryContinuousAssessmentMarkInput != newObj.IsOpenNonTheoryContinuousAssessmentMarkInput)
            {
                history +=
                    $"IsOpenNonTheoryContinuousAssessmentMarkInput: {dbObj.IsOpenNonTheoryContinuousAssessmentMarkInput} to {newObj.IsOpenNonTheoryContinuousAssessmentMarkInput};";
            }

            if (dbObj.IsOpenTheoryMidTermMarkInput != newObj.IsOpenTheoryMidTermMarkInput)
            {
                history +=
                    $"IsOpenTheoryMidTermMarkInput: {dbObj.IsOpenTheoryMidTermMarkInput} to {newObj.IsOpenTheoryMidTermMarkInput};";
            }

            if (dbObj.IsOpenNonTheoryMidTermMarkInput != newObj.IsOpenNonTheoryMidTermMarkInput)
            {
                history +=
                    $"IsOpenNonTheoryMidTermMarkInput: {dbObj.IsOpenNonTheoryMidTermMarkInput} to {newObj.IsOpenNonTheoryMidTermMarkInput};";
            }

            if (dbObj.IsOpenTheoryFinalTermMarkInput != newObj.IsOpenTheoryFinalTermMarkInput)
            {
                history +=
                    $"IsOpenTheoryFinalTermMarkInput: {dbObj.IsOpenTheoryFinalTermMarkInput} to {newObj.IsOpenTheoryFinalTermMarkInput};";
            }

            if (dbObj.IsOpenNonTheoryFinalTermMarkInput != newObj.IsOpenNonTheoryFinalTermMarkInput)
            {
                history +=
                    $"IsOpenNonTheoryFinalTermMarkInput: {dbObj.IsOpenNonTheoryFinalTermMarkInput} to {newObj.IsOpenNonTheoryFinalTermMarkInput};";
            }

            if (dbObj.AutoGradeGraceMark != newObj.AutoGradeGraceMark)
            {
                history +=
                    $"AutoGradeGraceMark: {dbObj.AutoGradeGraceMark} to {newObj.AutoGradeGraceMark};";
            }

            if (!history.IsEmpty())
            {
                // history=$"{history}"

                history = $"{DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")} Changed {history}  By {HttpUtil.Profile.Name}[{HttpUtil.Profile.UserName}], IP:{HttpUtil.GetUserIp()}.<br>";

            }

            return history;
        }

        #endregion

        #region Admission Exam

        public bool CopyAndSaveAdmissionExam(int sourceAdmissionExamId
            , long newAdmissionExamSemesterId, string newAdmissionExamName
            , string newStudentIdPrefix, string studentUgcIdPrefix
            , int incrementBatchCount
            , ref Adm_AdmissionExam dbAttachedEntity, out string error)
        {
            error = String.Empty;
            var sourceAdmissionExam =
                DbInstance.Adm_AdmissionExam.Include(x => x.Aca_Semester).FirstOrDefault(x => x.Id == sourceAdmissionExamId);

            var newAdmissionSemester =
                DbInstance.Aca_Semester.FirstOrDefault(x => x.Id == newAdmissionExamSemesterId);

            if (!IsValidToCopyAdmissionExam(sourceAdmissionExam, newAdmissionSemester, newAdmissionExamName, newStudentIdPrefix, studentUgcIdPrefix, incrementBatchCount, out error))
            {
                return false;
            }

            var entityReceived = GetNewCopiedAdmissionExam(sourceAdmissionExam, newAdmissionSemester
                , newAdmissionExamName, newStudentIdPrefix, studentUgcIdPrefix,incrementBatchCount);

            return SaveAdmissionExamLogic(entityReceived, ref dbAttachedEntity, out error);
        }

        private bool IsValidToCopyAdmissionExam(Adm_AdmissionExam sourceAdmissionExam, Aca_Semester newAdmissionSemester
            , string newAdmissionExamName
            , string newStudentIdPrefix
            , string studentUgcIdPrefix
            , int incrementBatchCount, out string error)
        {
            error = string.Empty;

            if (sourceAdmissionExam == null)
            {
                error += "Please select a valid Source Admission Exam.";
                return false;
            }

            if (newAdmissionSemester == null)
            {
                error += "Please select a valid New Admission Semester.";
                return false;
            }

            if (sourceAdmissionExam.Aca_Semester.SemesterDurationEnumId != newAdmissionSemester.SemesterDurationEnumId)
            {
                error += "Destination Semester's Duration and New admission Semester Duration Doesn't Match.";
                return false;
            }

            if (!newAdmissionExamName.IsValid())
            {
                error += "New Admission Exam Name is Not Valid";
                return false;
            }

            if (!newStudentIdPrefix.IsValid())
            {
                error += "Please Provide a Valid Student ID Prefix.";
                return false;
            }
            if (!studentUgcIdPrefix.IsValid())
            {
                error += "Please Provide a Valid UGC Unique ID Prefix.";
                return false;
            }

            if (incrementBatchCount <= 0)
            {
                error += "Please Provide a Positive Increment Batch Count.";
                return false;
            }

            return true;
        }
        private Adm_AdmissionExam GetNewCopiedAdmissionExam(Adm_AdmissionExam sourceAdmissionExam, Aca_Semester newAdmissionSemester
            , string newAdmissionExamName
            , string newStudentIdPrefix
            , string studentUgcIdPrefix
            , int incrementBatchCount)
        {
            var copiedAdmissionExam = Adm_AdmissionExam.GetNew();

            copiedAdmissionExam.Name = newAdmissionExamName;
            copiedAdmissionExam.ShortName = newAdmissionExamName;

            //New Sesstion will be semester's year and next semester's year.
            copiedAdmissionExam.SessionName = (newAdmissionSemester.Year - 1) + "-" + newAdmissionSemester.Year;

            copiedAdmissionExam.StudentIdPrefix = newStudentIdPrefix;
            copiedAdmissionExam.StudentIdSuffix = "";
            copiedAdmissionExam.UgcIdPrefix = studentUgcIdPrefix;

            copiedAdmissionExam.ProgramTypeEnumId = sourceAdmissionExam.ProgramTypeEnumId;

            copiedAdmissionExam.CircularStatusEnumId = (byte)Adm_AdmissionExam.CircularStatusEnum.Expired;

            copiedAdmissionExam.SemesterId = newAdmissionSemester.Id;

            copiedAdmissionExam.CircularNoticeHtml = sourceAdmissionExam.CircularNoticeHtml;
            copiedAdmissionExam.Remark = "";
            copiedAdmissionExam.IsEnableProgramWiseBatchMap = true;

            //Here Getting the Updated Json with updated 
            copiedAdmissionExam.DefaultSettingsJson = GetNewProgramWiseAdmBatchJson(sourceAdmissionExam.DefaultSettingsJson, incrementBatchCount);

            return copiedAdmissionExam;
        }
        private string GetNewProgramWiseAdmBatchJson(string programWiseAdmBatchJson, int incrementBatchCount)
        {
            try
            {
                var updatedProgramWiseAdmBatchJson = "";
                if (programWiseAdmBatchJson == null)
                    return null;

                var programWiseJsonObj =
                    JsonConvert.DeserializeObject<DefaultSettingsJson>(programWiseAdmBatchJson);

                //if no proper object found then it will return null
                if (programWiseJsonObj == null || programWiseJsonObj.ProgramWiseBatchMapJsonList.Count <= 0)
                    return null;

                foreach (var programWiseBatchMap in programWiseJsonObj.ProgramWiseBatchMapJsonList)
                {
                    if (programWiseBatchMap.BatchId != 0)
                    {
                        programWiseBatchMap.BatchId += incrementBatchCount;
                    }
                }

                updatedProgramWiseAdmBatchJson = JsonConvert.SerializeObject(programWiseJsonObj);

                return updatedProgramWiseAdmBatchJson;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private bool IsValidToSaveAdmissionExam(Adm_AdmissionExam newObj, out string error)
        {
            error = "";
            if (!newObj.Name.IsValid())
            {
                error += "Name is not valid.";
                return false;
            }
            if (newObj.Name.Length > 250)
            {
                error += "Name exceeded its maximum length 250.";
                return false;
            }
            if (!newObj.ShortName.IsValid())
            {
                error += "Short Name is not valid.";
                return false;
            }
            if (newObj.ShortName.Length > 100)
            {
                error += "Short Name exceeded its maximum length 100.";
                return false;
            }
            if (!newObj.SessionName.IsValid())
            {
                error += "Session Name is not valid.";
                return false;
            }
            if (newObj.SessionName.Length > 100)
            {
                error += "Session Name exceeded its maximum length 100.";
                return false;
            }
            if (!newObj.StudentIdPrefix.IsValid())
            {
                error += "Student Id Prefix is not valid.";
                return false;
            }
            if (newObj.StudentIdPrefix.Length > 10)
            {
                error += "Student Id Prefix exceeded its maximum length 10.";
                return false;
            }
            if (newObj.StudentIdSuffix.IsValid() && newObj.StudentIdSuffix.Length > 10)
            {
                error += "Student Id Suffix exceeded its maximum length 10.";
                return false;
            }
            if (newObj.ProgramTypeEnumId == null)
            {
                error += "Please select valid Program Type.";
                return false;
            }
            if (newObj.CircularStatusEnumId == null)
            {
                error += "Please select valid Circular Status.";
                return false;
            }
            if (newObj.SemesterId <= 0)
            {
                error += "Please select valid Semester.";
                return false;
            }
            if (!newObj.OnlineFormFillupStartDate.IsValid())
            {
                error += "Online Form Fillup Start Date is not valid.";
                return false;
            }
            if (!newObj.OnlineFormFillupEndDate.IsValid())
            {
                error += "Online Form Fillup End Date is not valid.";
                return false;
            }
            if (!newObj.OnlineAdmitCardPublishDate.IsValid())
            {
                error += "Online Admit Card Publish Date is not valid.";
                return false;
            }
            if (!newObj.OnlineAdmitCardLockDate.IsValid())
            {
                error += "Online Admit Card Lock Date is not valid.";
                return false;
            }
            if (!newObj.ExamDate.IsValid())
            {
                error += "Exam Date is not valid.";
                return false;
            }
            if (!newObj.ExamResultPublishDate.IsValid())
            {
                error += "Exam Result Publish Date is not valid.";
                return false;
            }
            if (!newObj.AdmissionStartDate.IsValid())
            {
                error += "Admission Start Date is not valid.";
                return false;
            }
            if (!newObj.AmissionFormsDownloadStartDate.IsValid())
            {
                error += "Amission Forms Download Start Date is not valid.";
                return false;
            }
            if (!newObj.AmissionFormsDownloadEndDate.IsValid())
            {
                error += "Amission Forms Download End Date is not valid.";
                return false;
            }

            if (SiteSettings.Instance.StudentIdGenerateType == SiteSettings.StudentIdGenerateTypeEnum.PrefixAutoGenerateAsUGC
                || SiteSettings.Instance.StudentIdGenerateType ==
                SiteSettings.StudentIdGenerateTypeEnum.FullAutoGenerateAsUGC || SiteSettings.Instance.AddNewApplicantFormSettings.IsAutoGenerateUgcId)
            {
                if (!newObj.UgcIdPrefix.IsValid())
                {
                    error += "Please Provide a Valid UGC Id Prefix";
                    return false;
                }
            }

            /* if (!newObj.CircularNoticeHtml.IsValid())
             {
                 error += "Circular Notice Html is not valid.";
                 return false;
             }*/
            /*if (newObj.CircularNoticeHtml==null)
            {
                error += "Circular Notice Html required.";
                return false;
            }*/
            /*if (!newObj.Remark.IsValid())
            {
                error += "Remark is not valid.";
                return false;
            }*/
            /*if (newObj.Remark==null)
            {
                error += "Remark required.";
                return false;
            }*/
            //TODO write your custom validation here.
            //var res =  DbInstance.Adm_AdmissionExam.Any(x => x.Id != newObj.Id );
            //if (res)
            //{
            //error = "A AdmissionExam already exists!";
            //return false;
            //}


            var res = DbInstance.Adm_AdmissionExam.Any(x => x.Id != newObj.Id
                         && x.Name == newObj.Name);
            if (res)
            {
                error = "Another Admission Circular have same name in the system!";
                return false;
            }
            res = DbInstance.Adm_AdmissionExam.Any(x => x.Id != newObj.Id
                        && x.SemesterId == newObj.SemesterId && x.ProgramTypeEnumId == newObj.ProgramTypeEnumId);
            if (res)
            {
                error = "Another Admission Circular already exist for this Semester!";
                return false;
            }

            //if (newObj.EnabledRegistrationType != Aca_Semester.EnabledRegistrationTypeEnum.None &&
            //    (!newObj.RegistrationStartDate.IsNotAndValid() || !newObj.RegistrationEndDate.IsNotAndValid()))
            //{
            //    error = "While Registration is Enabled, You mast have to put Registration Start Date & End Date";
            //    return false;
            //}

            //if (newObj.RegistrationEndDate <= newObj.RegistrationStartDate)
            //{
            //    error = "Registration End Date mast be grather then Registration Start Date";
            //    return false;
            //}


            if (newObj.CircularStatus == Adm_AdmissionExam.CircularStatusEnum.Current)
            {
                res = DbInstance.Adm_AdmissionExam.Any(x => x.Id != newObj.Id
                         && x.CircularStatusEnumId == (byte)Adm_AdmissionExam.CircularStatusEnum.Current);
                if (res)
                {
                    error = "Another Admission Circular have also Status Current. Change that first then make it Current!";
                    return false;
                }
            }

            return true;
        }
        public bool SaveAdmissionExamLogic(Adm_AdmissionExam newObj, ref Adm_AdmissionExam dbAddedObj, out string error)
        {
            error = string.Empty;
            if (newObj == null)
            {
                error = "Admission Circular to save can't be null!";
                return false;
            }

            if (!IsValidToSaveAdmissionExam(newObj, out error))
                return false;

            bool isNewObject = true;
            Adm_AdmissionExam objToSave = null;

            if (newObj.Id != 0)
            {
                objToSave = DbInstance.Adm_AdmissionExam.SingleOrDefault(x => x.Id == newObj.Id);
                isNewObject = false;
            }
            if (objToSave == null)
            {   //new object
                isNewObject = true;
                objToSave = Adm_AdmissionExam.GetNew(newObj.Id);
                DbInstance.Adm_AdmissionExam.Add(objToSave);
                objToSave.CreateById = newObj.CreateById = HttpUtil.Profile.UserId;
                objToSave.CreateDate = newObj.CreateDate = DateTime.Now;
            }

            if (isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.AdmissionExam.CanAdd,
                    out error))
            {
                return false;
            }
            else if (!isNewObject && !PermissionUtil.HasPermission(PermissionCollection.AdmissionArea.AdmissionExam.CanEdit,
                   out error))
            {
                return false;
            }

            //binding object  
            objToSave.Name = newObj.Name;
            objToSave.ShortName = newObj.ShortName;
            objToSave.SessionName = newObj.SessionName;
            objToSave.StudentIdPrefix = newObj.StudentIdPrefix;
            objToSave.StudentIdSuffix = newObj.StudentIdSuffix;
            objToSave.ProgramTypeEnumId = newObj.ProgramTypeEnumId;
            objToSave.SemesterId = newObj.SemesterId;

            if ((objToSave.CircularStatusEnumId != newObj.CircularStatusEnumId) &&
                newObj.CircularStatusEnumId == (byte)Adm_AdmissionExam.CircularStatusEnum.Current)
                UpdateOtherAdmissionExamStatus(newObj.Id, objToSave.SemesterId);//we are using semester no of obj

            objToSave.CircularStatusEnumId = newObj.CircularStatusEnumId;


            objToSave.OnlineFormFillupStartDate = newObj.OnlineFormFillupStartDate;
            objToSave.OnlineFormFillupEndDate = newObj.OnlineFormFillupEndDate;
            objToSave.OnlineAdmitCardPublishDate = newObj.OnlineAdmitCardPublishDate;
            objToSave.OnlineAdmitCardLockDate = newObj.OnlineAdmitCardLockDate;
            objToSave.ExamDate = newObj.ExamDate;
            objToSave.ExamResultPublishDate = newObj.ExamResultPublishDate;
            objToSave.AdmissionStartDate = newObj.AdmissionStartDate;
            objToSave.AmissionFormsDownloadStartDate = newObj.AmissionFormsDownloadStartDate;
            objToSave.AmissionFormsDownloadEndDate = newObj.AmissionFormsDownloadEndDate;
            objToSave.CircularNoticeHtml = newObj.CircularNoticeHtml;
            objToSave.Remark = newObj.Remark;
            objToSave.IsEnable = newObj.IsEnable;
            objToSave.UgcIdPrefix = newObj.UgcIdPrefix;
            objToSave.LastChangedById = newObj.LastChangedById = HttpUtil.Profile.UserId;
            objToSave.LastChanged = newObj.LastChanged = DateTime.Now;

            if (newObj.IsEnableProgramWiseBatchMap)
            {
                objToSave.DefaultSettingsJson = newObj.DefaultSettingsJson;
            }
            else
            {
                var programWiseJsonObj =
                    JsonConvert.DeserializeObject<DefaultSettingsJson>(newObj.DefaultSettingsJson == null ? "" : newObj.DefaultSettingsJson);

                if (programWiseJsonObj != null && programWiseJsonObj.ProgramWiseBatchMapJsonList.Count > 0)
                {
                    programWiseJsonObj.IsEnableProgramWiseBatchMap = false;
                    objToSave.DefaultSettingsJson = JsonConvert.SerializeObject(programWiseJsonObj);
                }
            }

            dbAddedObj = objToSave;


            //here save any child table
            return true;
        }

        private void UpdateOtherAdmissionExamStatus(int admissionExamId, long semesterId)
        {
            var admissionExamList = DbInstance.Adm_AdmissionExam
                .Where(x => x.Id != admissionExamId).ToList();

            foreach (var admissionExam in admissionExamList)
            {
                if (semesterId <= admissionExam.SemesterId)
                {
                    admissionExam.CircularStatusEnumId = (byte)Adm_AdmissionExam.CircularStatusEnum.Upcoming;
                }
                else
                {
                    admissionExam.CircularStatusEnumId = (byte)Adm_AdmissionExam.CircularStatusEnum.Expired;
                }
            }

            DbInstance.SaveChanges();
        }

        #endregion
    }
}

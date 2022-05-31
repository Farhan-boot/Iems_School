//File:Json Model Base Partial
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;

using EMS.Framework;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;

namespace EMS.Web.Jsons.Models
{
    /// <summary>
    /// Do not modify this class, edit partial instead.
    /// </summary>   
	public partial class Aca_ExamJson 
	{
          public String Id { get; set; }
          public String Name { get; set; }
          public String ShortName { get; set; }
          public String AcademicSession { get; set; }
          public String SemesterId { get; set; }
          public Byte ProgramTypeEnumId { get; set; }
          public string ProgramType { get; set; }
          public Byte ExamTypeEnumId { get; set; }
          public string ExamType { get; set; }
          public DateTime StartDateOfTermFinalExam { get; set; }
          public DateTime EndDateOfTermFinalExam { get; set; }
          public DateTime DateOfTermFinalResultPublication { get; set; }
          public String Remarks { get; set; }
          public DateTime CreateDate { get; set; }
          public String CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public String LastChangedById { get; set; }
          public Boolean IsDeleted { get; set; }
          public String History { get; set; }
          public String OfficialExamName { get; set; }
          public Nullable<DateTime> StartDateOfMidtermExam { get; set; }
          public Nullable<DateTime> EndDateOfMidtermExam { get; set; }
          public Nullable<DateTime> DateOfMidtermResultPublication { get; set; }
          public Boolean IsPublishTheoryContinuousAssessmentMarks { get; set; }
          public Boolean IsPublishNonTheoryContinuousAssessmentMarks { get; set; }
          public Single ContinuousAssessmentMarksAllowedPaymentDueUpto { get; set; }
          public Boolean IsPublishTheoryMidtermResult { get; set; }
          public Boolean IsPublishNonTheoryMidtermResult { get; set; }
          public Single MidtermResultAllowedPaymentDueUpto { get; set; }
          public Boolean IsPublishTheoryFinalTermResult { get; set; }
          public Boolean IsPublishNonTheoryFinalTermResult { get; set; }
          public Single FinalTermResultAllowedPaymentDueUpto { get; set; }
          public Boolean IsPublishedMidtermAdmitCard { get; set; }
          public Single MidtermAdmitCardDownloadPaymentDueUpto { get; set; }
          public Boolean IsPublishedFinalTermAdmitCard { get; set; }
          public Single FinalTermAdmitCardDownloadPaymentDueUpto { get; set; }
          public Boolean IsOpenTheoryContinuousAssessmentMarkInput { get; set; }
          public Boolean IsOpenNonTheoryContinuousAssessmentMarkInput { get; set; }
          public Boolean IsOpenTheoryMidTermMarkInput { get; set; }
          public Boolean IsOpenNonTheoryMidTermMarkInput { get; set; }
          public Boolean IsOpenTheoryFinalTermMarkInput { get; set; }
          public Boolean IsOpenNonTheoryFinalTermMarkInput { get; set; }
          public Single AutoGradeGraceMark { get; set; }
          public String AbsentGradeId { get; set; }
          public String AbsentMarkSymbol { get; set; }
          public String ContinuationGradeId { get; set; }
          public String MaximumImproveExamAllowGradeId { get; set; }
          public String MaximumRetakeCourseAllowGradeId { get; set; }
          public String MaximumGradeForImproveExamGradeId { get; set; }
          public String MaximummGradeForRetakeCourseGradeId { get; set; }
        /// <summary>
        /// This is for detect selected columns in List.
        /// This is not database property.
        /// </summary>
        public bool IsSelected { get; set; }
        /// <summary>
        /// This is detect already added item in desired table
        /// This is not database property.
        /// </summary>
        public bool IsAlreadyAdded { get; set; }
	}
    #region Mapper
     /// <summary>
    /// Do not modify this class, edit partial instead.
    /// </summary>   
	public static partial class EntityMapper
	{
	   
		public static void Map(this Aca_Exam entity, Aca_ExamJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.Name = entity.Name ; 
			 toJson.ShortName = entity.ShortName ; 
			 toJson.AcademicSession = entity.AcademicSession ; 
			 toJson.SemesterId = entity.SemesterId.ToString() ; 
			 toJson.ProgramTypeEnumId = entity.ProgramTypeEnumId ; 
             if(entity.ProgramTypeEnumId!=null)
             toJson.ProgramType = entity.ProgramType.ToString().AddSpacesToSentence();
			 toJson.ExamTypeEnumId = entity.ExamTypeEnumId ; 
             if(entity.ExamTypeEnumId!=null)
             toJson.ExamType = entity.ExamType.ToString().AddSpacesToSentence();
			 toJson.StartDateOfTermFinalExam = entity.StartDateOfTermFinalExam ; 
			 toJson.EndDateOfTermFinalExam = entity.EndDateOfTermFinalExam ; 
			 toJson.DateOfTermFinalResultPublication = entity.DateOfTermFinalResultPublication ; 
			 toJson.Remarks = entity.Remarks ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 
			 toJson.IsDeleted = entity.IsDeleted ; 
			 toJson.History = entity.History ; 
			 toJson.OfficialExamName = entity.OfficialExamName ; 
			 toJson.StartDateOfMidtermExam = entity.StartDateOfMidtermExam ; 
			 toJson.EndDateOfMidtermExam = entity.EndDateOfMidtermExam ; 
			 toJson.DateOfMidtermResultPublication = entity.DateOfMidtermResultPublication ; 
			 toJson.IsPublishTheoryContinuousAssessmentMarks = entity.IsPublishTheoryContinuousAssessmentMarks ; 
			 toJson.IsPublishNonTheoryContinuousAssessmentMarks = entity.IsPublishNonTheoryContinuousAssessmentMarks ; 
			 toJson.ContinuousAssessmentMarksAllowedPaymentDueUpto = entity.ContinuousAssessmentMarksAllowedPaymentDueUpto ; 
			 toJson.IsPublishTheoryMidtermResult = entity.IsPublishTheoryMidtermResult ; 
			 toJson.IsPublishNonTheoryMidtermResult = entity.IsPublishNonTheoryMidtermResult ; 
			 toJson.MidtermResultAllowedPaymentDueUpto = entity.MidtermResultAllowedPaymentDueUpto ; 
			 toJson.IsPublishTheoryFinalTermResult = entity.IsPublishTheoryFinalTermResult ; 
			 toJson.IsPublishNonTheoryFinalTermResult = entity.IsPublishNonTheoryFinalTermResult ; 
			 toJson.FinalTermResultAllowedPaymentDueUpto = entity.FinalTermResultAllowedPaymentDueUpto ; 
			 toJson.IsPublishedMidtermAdmitCard = entity.IsPublishedMidtermAdmitCard ; 
			 toJson.MidtermAdmitCardDownloadPaymentDueUpto = entity.MidtermAdmitCardDownloadPaymentDueUpto ; 
			 toJson.IsPublishedFinalTermAdmitCard = entity.IsPublishedFinalTermAdmitCard ; 
			 toJson.FinalTermAdmitCardDownloadPaymentDueUpto = entity.FinalTermAdmitCardDownloadPaymentDueUpto ; 
			 toJson.IsOpenTheoryContinuousAssessmentMarkInput = entity.IsOpenTheoryContinuousAssessmentMarkInput ; 
			 toJson.IsOpenNonTheoryContinuousAssessmentMarkInput = entity.IsOpenNonTheoryContinuousAssessmentMarkInput ; 
			 toJson.IsOpenTheoryMidTermMarkInput = entity.IsOpenTheoryMidTermMarkInput ; 
			 toJson.IsOpenNonTheoryMidTermMarkInput = entity.IsOpenNonTheoryMidTermMarkInput ; 
			 toJson.IsOpenTheoryFinalTermMarkInput = entity.IsOpenTheoryFinalTermMarkInput ; 
			 toJson.IsOpenNonTheoryFinalTermMarkInput = entity.IsOpenNonTheoryFinalTermMarkInput ; 
			 toJson.AutoGradeGraceMark = entity.AutoGradeGraceMark ; 
			 toJson.AbsentGradeId = entity.AbsentGradeId.ToString() ; 
			 toJson.AbsentMarkSymbol = entity.AbsentMarkSymbol ; 
			 toJson.ContinuationGradeId = entity.ContinuationGradeId.ToString() ; 
			 toJson.MaximumImproveExamAllowGradeId = entity.MaximumImproveExamAllowGradeId.ToString() ; 
			 toJson.MaximumRetakeCourseAllowGradeId = entity.MaximumRetakeCourseAllowGradeId.ToString() ; 
			 toJson.MaximumGradeForImproveExamGradeId = entity.MaximumGradeForImproveExamGradeId.ToString() ; 
			 toJson.MaximummGradeForRetakeCourseGradeId = entity.MaximummGradeForRetakeCourseGradeId.ToString() ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Aca_ExamJson json, Aca_Exam toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
                 toEntity.ShortName = json.ShortName?.Trim() ?? json.ShortName;
                 toEntity.AcademicSession = json.AcademicSession?.Trim() ?? json.AcademicSession;
                 toEntity.SemesterId = long.TryParse(json.SemesterId, out output) ? output : 0;
    			 toEntity.ProgramTypeEnumId=json.ProgramTypeEnumId;
    			 toEntity.ExamTypeEnumId=json.ExamTypeEnumId;
    			 toEntity.StartDateOfTermFinalExam=json.StartDateOfTermFinalExam;
    			 toEntity.EndDateOfTermFinalExam=json.EndDateOfTermFinalExam;
    			 toEntity.DateOfTermFinalResultPublication=json.DateOfTermFinalResultPublication;
                 toEntity.Remarks = json.Remarks?.Trim() ?? json.Remarks;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;
    			 toEntity.IsDeleted=json.IsDeleted;
                 toEntity.History = json.History?.Trim() ?? json.History;
                 toEntity.OfficialExamName = json.OfficialExamName?.Trim() ?? json.OfficialExamName;
    			 toEntity.StartDateOfMidtermExam=json.StartDateOfMidtermExam;
    			 toEntity.EndDateOfMidtermExam=json.EndDateOfMidtermExam;
    			 toEntity.DateOfMidtermResultPublication=json.DateOfMidtermResultPublication;
    			 toEntity.IsPublishTheoryContinuousAssessmentMarks=json.IsPublishTheoryContinuousAssessmentMarks;
    			 toEntity.IsPublishNonTheoryContinuousAssessmentMarks=json.IsPublishNonTheoryContinuousAssessmentMarks;
    			 toEntity.ContinuousAssessmentMarksAllowedPaymentDueUpto=json.ContinuousAssessmentMarksAllowedPaymentDueUpto;
    			 toEntity.IsPublishTheoryMidtermResult=json.IsPublishTheoryMidtermResult;
    			 toEntity.IsPublishNonTheoryMidtermResult=json.IsPublishNonTheoryMidtermResult;
    			 toEntity.MidtermResultAllowedPaymentDueUpto=json.MidtermResultAllowedPaymentDueUpto;
    			 toEntity.IsPublishTheoryFinalTermResult=json.IsPublishTheoryFinalTermResult;
    			 toEntity.IsPublishNonTheoryFinalTermResult=json.IsPublishNonTheoryFinalTermResult;
    			 toEntity.FinalTermResultAllowedPaymentDueUpto=json.FinalTermResultAllowedPaymentDueUpto;
    			 toEntity.IsPublishedMidtermAdmitCard=json.IsPublishedMidtermAdmitCard;
    			 toEntity.MidtermAdmitCardDownloadPaymentDueUpto=json.MidtermAdmitCardDownloadPaymentDueUpto;
    			 toEntity.IsPublishedFinalTermAdmitCard=json.IsPublishedFinalTermAdmitCard;
    			 toEntity.FinalTermAdmitCardDownloadPaymentDueUpto=json.FinalTermAdmitCardDownloadPaymentDueUpto;
    			 toEntity.IsOpenTheoryContinuousAssessmentMarkInput=json.IsOpenTheoryContinuousAssessmentMarkInput;
    			 toEntity.IsOpenNonTheoryContinuousAssessmentMarkInput=json.IsOpenNonTheoryContinuousAssessmentMarkInput;
    			 toEntity.IsOpenTheoryMidTermMarkInput=json.IsOpenTheoryMidTermMarkInput;
    			 toEntity.IsOpenNonTheoryMidTermMarkInput=json.IsOpenNonTheoryMidTermMarkInput;
    			 toEntity.IsOpenTheoryFinalTermMarkInput=json.IsOpenTheoryFinalTermMarkInput;
    			 toEntity.IsOpenNonTheoryFinalTermMarkInput=json.IsOpenNonTheoryFinalTermMarkInput;
    			 toEntity.AutoGradeGraceMark=json.AutoGradeGraceMark;
                 toEntity.AbsentGradeId = long.TryParse(json.AbsentGradeId, out output) ? output : 0;
                 toEntity.AbsentMarkSymbol = json.AbsentMarkSymbol?.Trim() ?? json.AbsentMarkSymbol;
                 toEntity.ContinuationGradeId = long.TryParse(json.ContinuationGradeId, out output) ? output : 0;
                 toEntity.MaximumImproveExamAllowGradeId = long.TryParse(json.MaximumImproveExamAllowGradeId, out output) ? output : 0;
                 toEntity.MaximumRetakeCourseAllowGradeId = long.TryParse(json.MaximumRetakeCourseAllowGradeId, out output) ? output : 0;
                 toEntity.MaximumGradeForImproveExamGradeId = long.TryParse(json.MaximumGradeForImproveExamGradeId, out output) ? output : 0;
                 toEntity.MaximummGradeForRetakeCourseGradeId = long.TryParse(json.MaximummGradeForRetakeCourseGradeId, out output) ? output : 0;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Aca_Exam> entityList, IList<Aca_ExamJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new Aca_ExamJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Aca_ExamJson> jsonList, ICollection<Aca_Exam> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                Aca_Exam obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = Aca_Exam.GetNew();
                    json.Map(obj);
                    toEntityList.Add(obj);
                    
                }
                else
                {
                   json.Map(obj);
                }
            }
        }
	}
    #endregion
}

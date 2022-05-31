//File: Entity Partial Base
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;

namespace EMS.DataAccess.Data
{

    /// <summary>
    /// Do not modify this class, edit partial instead.
    /// </summary>   
    public partial class Aca_Exam
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_Name = "Name";		            
		public const string Property_ShortName = "ShortName";		            
		public const string Property_AcademicSession = "AcademicSession";		            
		public const string Property_SemesterId = "SemesterId";		            
		public const string Property_ProgramTypeEnumId = "ProgramTypeEnumId";		            
        public const string Property_ProgramType = "ProgramType";
		public const string Property_ExamTypeEnumId = "ExamTypeEnumId";		            
        public const string Property_ExamType = "ExamType";
		public const string Property_StartDateOfTermFinalExam = "StartDateOfTermFinalExam";		            
		public const string Property_EndDateOfTermFinalExam = "EndDateOfTermFinalExam";		            
		public const string Property_DateOfTermFinalResultPublication = "DateOfTermFinalResultPublication";		            
		public const string Property_Remarks = "Remarks";		            
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_CreateById = "CreateById";		            
		public const string Property_LastChanged = "LastChanged";		            
		public const string Property_LastChangedById = "LastChangedById";		            
		public const string Property_IsDeleted = "IsDeleted";		            
		public const string Property_History = "History";		            
		public const string Property_OfficialExamName = "OfficialExamName";		            
		public const string Property_StartDateOfMidtermExam = "StartDateOfMidtermExam";		            
		public const string Property_EndDateOfMidtermExam = "EndDateOfMidtermExam";		            
		public const string Property_DateOfMidtermResultPublication = "DateOfMidtermResultPublication";		            
		public const string Property_IsPublishTheoryContinuousAssessmentMarks = "IsPublishTheoryContinuousAssessmentMarks";		            
		public const string Property_IsPublishNonTheoryContinuousAssessmentMarks = "IsPublishNonTheoryContinuousAssessmentMarks";		            
		public const string Property_ContinuousAssessmentMarksAllowedPaymentDueUpto = "ContinuousAssessmentMarksAllowedPaymentDueUpto";		            
		public const string Property_IsPublishTheoryMidtermResult = "IsPublishTheoryMidtermResult";		            
		public const string Property_IsPublishNonTheoryMidtermResult = "IsPublishNonTheoryMidtermResult";		            
		public const string Property_MidtermResultAllowedPaymentDueUpto = "MidtermResultAllowedPaymentDueUpto";		            
		public const string Property_IsPublishTheoryFinalTermResult = "IsPublishTheoryFinalTermResult";		            
		public const string Property_IsPublishNonTheoryFinalTermResult = "IsPublishNonTheoryFinalTermResult";		            
		public const string Property_FinalTermResultAllowedPaymentDueUpto = "FinalTermResultAllowedPaymentDueUpto";		            
		public const string Property_IsPublishedMidtermAdmitCard = "IsPublishedMidtermAdmitCard";		            
		public const string Property_MidtermAdmitCardDownloadPaymentDueUpto = "MidtermAdmitCardDownloadPaymentDueUpto";		            
		public const string Property_IsPublishedFinalTermAdmitCard = "IsPublishedFinalTermAdmitCard";		            
		public const string Property_FinalTermAdmitCardDownloadPaymentDueUpto = "FinalTermAdmitCardDownloadPaymentDueUpto";		            
		public const string Property_IsOpenTheoryContinuousAssessmentMarkInput = "IsOpenTheoryContinuousAssessmentMarkInput";		            
		public const string Property_IsOpenNonTheoryContinuousAssessmentMarkInput = "IsOpenNonTheoryContinuousAssessmentMarkInput";		            
		public const string Property_IsOpenTheoryMidTermMarkInput = "IsOpenTheoryMidTermMarkInput";		            
		public const string Property_IsOpenNonTheoryMidTermMarkInput = "IsOpenNonTheoryMidTermMarkInput";		            
		public const string Property_IsOpenTheoryFinalTermMarkInput = "IsOpenTheoryFinalTermMarkInput";		            
		public const string Property_IsOpenNonTheoryFinalTermMarkInput = "IsOpenNonTheoryFinalTermMarkInput";		            
		public const string Property_AutoGradeGraceMark = "AutoGradeGraceMark";		            
		public const string Property_AbsentGradeId = "AbsentGradeId";		            
		public const string Property_AbsentMarkSymbol = "AbsentMarkSymbol";		            
		public const string Property_ContinuationGradeId = "ContinuationGradeId";		            
		public const string Property_MaximumImproveExamAllowGradeId = "MaximumImproveExamAllowGradeId";		            
		public const string Property_MaximumRetakeCourseAllowGradeId = "MaximumRetakeCourseAllowGradeId";		            
		public const string Property_MaximumGradeForImproveExamGradeId = "MaximumGradeForImproveExamGradeId";		            
		public const string Property_MaximummGradeForRetakeCourseGradeId = "MaximummGradeForRetakeCourseGradeId";		            
              
        /// <summary>
        /// This is for detect selected columns in List.
        /// This is not database property.
        /// </summary>
        public const string Property_IsSelected = "IsSelected";
        /// <summary>
        /// This is detect already added item in desired table
        /// This is not database property.
        /// </summary>
        public const string Property_IsAlreadyAdded = "IsAlreadyAdded";
	  #endregion
      
        /// <summary>
        /// This is for detect selected columns in List.
        /// This is not database property.
        /// </summary>
        public bool IsSelected { get; set; }
        /// <summary>
        /// This is detect already added item in desired table
        /// This is not database property.
        /// </summary>
        public bool IsAlreadyAdded = true;
      
     
     
      
        public static Aca_Exam GetNew(Int64 id=0)
        {
           Aca_Exam obj = new Aca_Exam();
               obj.Id = id;
                obj.Name = string.Empty ; 
                obj.ShortName = string.Empty ; 
                obj.AcademicSession = string.Empty ; 
                obj.SemesterId = 0 ; 
                obj.ProgramTypeEnumId = 0 ; 
                obj.ExamTypeEnumId = 0 ; 
                obj.StartDateOfTermFinalExam = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.EndDateOfTermFinalExam = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.DateOfTermFinalResultPublication = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.Remarks = null ; 
                obj.CreateDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreateById = 1 ;
                obj.LastChanged = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LastChangedById = 1 ;
                obj.IsDeleted = false ; 
                obj.History = null ; 
                obj.OfficialExamName = string.Empty ; 
                obj.StartDateOfMidtermExam = null ; 
                obj.EndDateOfMidtermExam = null ; 
                obj.DateOfMidtermResultPublication = null ; 
                obj.IsPublishTheoryContinuousAssessmentMarks = false ; 
                obj.IsPublishNonTheoryContinuousAssessmentMarks = false ; 
                obj.ContinuousAssessmentMarksAllowedPaymentDueUpto = 0.0F ; 
                obj.IsPublishTheoryMidtermResult = false ; 
                obj.IsPublishNonTheoryMidtermResult = false ; 
                obj.MidtermResultAllowedPaymentDueUpto = 0.0F ; 
                obj.IsPublishTheoryFinalTermResult = false ; 
                obj.IsPublishNonTheoryFinalTermResult = false ; 
                obj.FinalTermResultAllowedPaymentDueUpto = 0.0F ; 
                obj.IsPublishedMidtermAdmitCard = false ; 
                obj.MidtermAdmitCardDownloadPaymentDueUpto = 0.0F ; 
                obj.IsPublishedFinalTermAdmitCard = false ; 
                obj.FinalTermAdmitCardDownloadPaymentDueUpto = 0.0F ; 
                obj.IsOpenTheoryContinuousAssessmentMarkInput = false ; 
                obj.IsOpenNonTheoryContinuousAssessmentMarkInput = false ; 
                obj.IsOpenTheoryMidTermMarkInput = false ; 
                obj.IsOpenNonTheoryMidTermMarkInput = false ; 
                obj.IsOpenTheoryFinalTermMarkInput = false ; 
                obj.IsOpenNonTheoryFinalTermMarkInput = false ; 
                obj.AutoGradeGraceMark = 0.0F ; 
                obj.AbsentGradeId = 0 ; 
                obj.AbsentMarkSymbol = string.Empty ; 
                obj.ContinuationGradeId = 0 ; 
                obj.MaximumImproveExamAllowGradeId = 0 ; 
                obj.MaximumRetakeCourseAllowGradeId = 0 ; 
                obj.MaximumGradeForImproveExamGradeId = 0 ; 
                obj.MaximummGradeForRetakeCourseGradeId = 0 ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

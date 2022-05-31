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
    public partial class Aca_ExamSupple
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_ExamId = "ExamId";		            
		public const string Property_IsOpenTheorySuppleApplyForStudent = "IsOpenTheorySuppleApplyForStudent";		            
		public const string Property_IsOpenNonTheorySuppleApplyForStudent = "IsOpenNonTheorySuppleApplyForStudent";		            
		public const string Property_SuppleApplicationLastDate = "SuppleApplicationLastDate";		            
		public const string Property_IsOpenTheorySuppleApplyForAdmin = "IsOpenTheorySuppleApplyForAdmin";		            
		public const string Property_IsOpenNonTheorySuppleApplyForAdmin = "IsOpenNonTheorySuppleApplyForAdmin";		            
		public const string Property_SuppleApplyAllowedPaymentDueUpto = "SuppleApplyAllowedPaymentDueUpto";		            
		public const string Property_MaxTimeCanApplyForOneTheory = "MaxTimeCanApplyForOneTheory";		            
		public const string Property_MaxTimeCanApplyForOneNonTheory = "MaxTimeCanApplyForOneNonTheory";		            
		public const string Property_ReferredSuppleExamFee = "ReferredSuppleExamFee";		            
		public const string Property_BackLogSuppleExamFee = "BackLogSuppleExamFee";		            
		public const string Property_IsPublishedSuppleAdmitCard = "IsPublishedSuppleAdmitCard";		            
		public const string Property_SuppleAdmitCardDownloadPaymentDueUpto = "SuppleAdmitCardDownloadPaymentDueUpto";		            
		public const string Property_IsOpenTheorySuppleMarkInput = "IsOpenTheorySuppleMarkInput";		            
		public const string Property_IsOpenNonTheorySuppleMarkInput = "IsOpenNonTheorySuppleMarkInput";		            
		public const string Property_AutoGradeGraceMarkForPass = "AutoGradeGraceMarkForPass";		            
		public const string Property_IsPublishTheorySuppleResult = "IsPublishTheorySuppleResult";		            
		public const string Property_IsPublishNonTheorySuppleResult = "IsPublishNonTheorySuppleResult";		            
		public const string Property_SuppleResultAllowedPaymentDueUpto = "SuppleResultAllowedPaymentDueUpto";		            
		public const string Property_StudentsInstructionsForApply = "StudentsInstructionsForApply";		            
		public const string Property_FacultyInstructionsForMarkEntry = "FacultyInstructionsForMarkEntry";		            
		public const string Property_StudentApplyConfirmationMessage = "StudentApplyConfirmationMessage";		            
              
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
      
     
     
      
        public static Aca_ExamSupple GetNew(Int32 id=0)
        {
           Aca_ExamSupple obj = new Aca_ExamSupple();
               obj.Id = id;
                obj.ExamId = 0 ; 
                obj.IsOpenTheorySuppleApplyForStudent = false ; 
                obj.IsOpenNonTheorySuppleApplyForStudent = false ; 
                obj.SuppleApplicationLastDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.IsOpenTheorySuppleApplyForAdmin = false ; 
                obj.IsOpenNonTheorySuppleApplyForAdmin = false ; 
                obj.SuppleApplyAllowedPaymentDueUpto = 0.0F ; 
                obj.MaxTimeCanApplyForOneTheory = 0 ; 
                obj.MaxTimeCanApplyForOneNonTheory = 0 ; 
                obj.ReferredSuppleExamFee = 0.0F ; 
                obj.BackLogSuppleExamFee = 0.0F ; 
                obj.IsPublishedSuppleAdmitCard = false ; 
                obj.SuppleAdmitCardDownloadPaymentDueUpto = 0.0F ; 
                obj.IsOpenTheorySuppleMarkInput = false ; 
                obj.IsOpenNonTheorySuppleMarkInput = false ; 
                obj.AutoGradeGraceMarkForPass = 0.0F ; 
                obj.IsPublishTheorySuppleResult = false ; 
                obj.IsPublishNonTheorySuppleResult = false ; 
                obj.SuppleResultAllowedPaymentDueUpto = 0.0F ; 
                obj.StudentsInstructionsForApply = string.Empty ; 
                obj.FacultyInstructionsForMarkEntry = string.Empty ; 
                obj.StudentApplyConfirmationMessage = string.Empty ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

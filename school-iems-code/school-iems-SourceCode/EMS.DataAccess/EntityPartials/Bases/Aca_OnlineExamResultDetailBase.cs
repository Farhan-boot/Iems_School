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
    public partial class Aca_OnlineExamResultDetail
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_StudentId = "StudentId";		            
		public const string Property_OnlineExamId = "OnlineExamId";		            
		public const string Property_SubmittedQuestionOptionId = "SubmittedQuestionOptionId";		            
		public const string Property_SubmittedAnswerText = "SubmittedAnswerText";		            
		public const string Property_SubmittedAnswerFilePath = "SubmittedAnswerFilePath";		            
		public const string Property_IsCorrect = "IsCorrect";		            
		public const string Property_ObtainedMark = "ObtainedMark";		            
		public const string Property_History = "History";		            
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_CreateById = "CreateById";		            
		public const string Property_LastChanged = "LastChanged";		            
		public const string Property_LastChangedById = "LastChangedById";		            
		public const string Property_IsDeleted = "IsDeleted";		            
		public const string Property_QuestionBankId = "QuestionBankId";		            
		public const string Property_SubmittedQuestionOptionsJson = "SubmittedQuestionOptionsJson";		            
              
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
      
     
     
      
        public static Aca_OnlineExamResultDetail GetNew(Int32 id=0)
        {
           Aca_OnlineExamResultDetail obj = new Aca_OnlineExamResultDetail();
               obj.Id = id;
                obj.StudentId = 0 ; 
                obj.OnlineExamId = 0 ; 
                obj.SubmittedQuestionOptionId = null ; 
                obj.SubmittedAnswerText = null ; 
                obj.SubmittedAnswerFilePath = null ; 
                obj.IsCorrect = false ; 
                obj.ObtainedMark = 0.0F ; 
                obj.History = null ; 
                obj.CreateDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreateById = 1 ;
                obj.LastChanged = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LastChangedById = 1 ;
                obj.IsDeleted = false ; 
                obj.QuestionBankId = 0 ; 
                obj.SubmittedQuestionOptionsJson = null ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

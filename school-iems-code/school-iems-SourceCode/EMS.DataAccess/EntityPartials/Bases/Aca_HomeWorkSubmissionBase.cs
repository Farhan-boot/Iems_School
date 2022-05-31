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
    public partial class Aca_HomeWorkSubmission
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_HomeworkId = "HomeworkId";		            
		public const string Property_StudentId = "StudentId";		            
		public const string Property_SubmissionDate = "SubmissionDate";		            
		public const string Property_SubmissionStatusEnumId = "SubmissionStatusEnumId";		            
        public const string Property_SubmissionStatus = "SubmissionStatus";
		public const string Property_Checked = "Checked";		            
		public const string Property_TeacherId = "TeacherId";		            
		public const string Property_Feedback = "Feedback";		            
		public const string Property_Remark = "Remark";		            
		public const string Property_History = "History";		            
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_CreateById = "CreateById";		            
		public const string Property_LastChanged = "LastChanged";		            
		public const string Property_LastChangedById = "LastChangedById";		            
		public const string Property_IsDeleted = "IsDeleted";		            
		public const string Property_SubmittedText = "SubmittedText";		            
		public const string Property_ObtainedMark = "ObtainedMark";		            
              
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
      
     
     
      
        public static Aca_HomeWorkSubmission GetNew(Int32 id=0)
        {
           Aca_HomeWorkSubmission obj = new Aca_HomeWorkSubmission();
               obj.Id = id;
                obj.HomeworkId = 0 ; 
                obj.StudentId = 0 ; 
                obj.SubmissionDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.SubmissionStatusEnumId = 0 ; 
                obj.Checked = false ; 
                obj.TeacherId = 0 ; 
                obj.Feedback = null ; 
                obj.Remark = null ; 
                obj.History = null ; 
                obj.CreateDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreateById = 1 ;
                obj.LastChanged = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LastChangedById = 1 ;
                obj.IsDeleted = false ; 
                obj.SubmittedText = null ; 
                obj.ObtainedMark = 0.0F ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

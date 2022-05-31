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
    public partial class Aca_QuestionBank
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_Name = "Name";		            
		public const string Property_QuestionImagePath = "QuestionImagePath";		            
		public const string Property_QuestionTypeEnumId = "QuestionTypeEnumId";		            
        public const string Property_QuestionType = "QuestionType";
		public const string Property_Mark = "Mark";		            
		public const string Property_QuestionGroupId = "QuestionGroupId";		            
		public const string Property_ProgramTypeEnumId = "ProgramTypeEnumId";		            
        public const string Property_ProgramType = "ProgramType";
		public const string Property_CurriculumCourseId = "CurriculumCourseId";		            
		public const string Property_LanguageMediumEnumId = "LanguageMediumEnumId";		            
        public const string Property_LanguageMedium = "LanguageMedium";
		public const string Property_IsShareable = "IsShareable";		            
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_CreateById = "CreateById";		            
		public const string Property_LastChangedDate = "LastChangedDate";		            
		public const string Property_LastChangedById = "LastChangedById";		            
		public const string Property_IsActive = "IsActive";		            
              
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
      
     
     
      
        public static Aca_QuestionBank GetNew(Int32 id=0)
        {
           Aca_QuestionBank obj = new Aca_QuestionBank();
               obj.Id = id;
                obj.Name = string.Empty ; 
                obj.QuestionImagePath = null ; 
                obj.QuestionTypeEnumId = 0 ; 
                obj.Mark = 0.0F ; 
                obj.QuestionGroupId = null ; 
                obj.ProgramTypeEnumId = null ; 
                obj.CurriculumCourseId = null ; 
                obj.LanguageMediumEnumId = null ; 
                obj.IsShareable = false ; 
                obj.CreateDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreateById = 1 ;
                obj.LastChangedDate = null ; 
                obj.LastChangedById = 1 ;
                obj.IsActive = false ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

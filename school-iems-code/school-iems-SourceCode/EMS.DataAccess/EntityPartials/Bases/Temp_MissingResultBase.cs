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
    public partial class Temp_MissingResult
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_StudentId = "StudentId";		            
		public const string Property_UserName = "UserName";		            
		public const string Property_ProgramId = "ProgramId";		            
		public const string Property_CurriculumId = "CurriculumId";		            
		public const string Property_DeptBatchId = "DeptBatchId";		            
		public const string Property_CourseCode = "CourseCode";		            
		public const string Property_CourseCredit = "CourseCredit";		            
		public const string Property_MidTerm = "MidTerm";		            
		public const string Property_FinalTerm = "FinalTerm";		            
		public const string Property_Assignment = "Assignment";		            
		public const string Property_OralTest = "OralTest";		            
		public const string Property_Attendance = "Attendance";		            
		public const string Property_TotalMark = "TotalMark";		            
		public const string Property_Grade = "Grade";		            
		public const string Property_GradePoint = "GradePoint";		            
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_AcLgId = "AcLgId";		            
		public const string Property_TypeEnumId = "TypeEnumId";		            
        public const string Property_Type = "Type";
		public const string Property_EmsCourseCode = "EmsCourseCode";		            
		public const string Property_Remarks = "Remarks";		            
		public const string Property_ResultCode = "ResultCode";		            
		public const string Property_ResultCredit = "ResultCredit";		            
		public const string Property_ResultDate = "ResultDate";		            
		public const string Property_RegCode = "RegCode";		            
		public const string Property_RegCredit = "RegCredit";		            
		public const string Property_RegDate = "RegDate";		            
		public const string Property_ResultPkId = "ResultPkId";		            
		public const string Property_RegPkId = "RegPkId";		            
		public const string Property_RegSemesterId = "RegSemesterId";		            
		public const string Property_ClassId = "ClassId";		            
              
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
      
     
     
      
        public static Temp_MissingResult GetNew(Int32 id=0)
        {
           Temp_MissingResult obj = new Temp_MissingResult();
               obj.Id = id;
                obj.StudentId = 0 ; 
                obj.UserName = string.Empty ; 
                obj.ProgramId = 0 ; 
                obj.CurriculumId = 0 ; 
                obj.DeptBatchId = 0 ; 
                obj.CourseCode = string.Empty ; 
                obj.CourseCredit = 0.0F ; 
                obj.MidTerm = 0.0F ; 
                obj.FinalTerm = 0.0F ; 
                obj.Assignment = 0.0F ; 
                obj.OralTest = 0.0F ; 
                obj.Attendance = 0.0F ; 
                obj.TotalMark = 0.0F ; 
                obj.Grade = string.Empty ; 
                obj.GradePoint = 0.0F ; 
                obj.CreateDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.AcLgId = 0 ; 
                obj.TypeEnumId = 0 ; 
                obj.EmsCourseCode = null ; 
                obj.Remarks = null ; 
                obj.ResultCode = null ; 
                obj.ResultCredit = null ; 
                obj.ResultDate = null ; 
                obj.RegCode = null ; 
                obj.RegCredit = null ; 
                obj.RegDate = null ; 
                obj.ResultPkId = null ; 
                obj.RegPkId = null ; 
                obj.RegSemesterId = null ; 
                obj.ClassId = null ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

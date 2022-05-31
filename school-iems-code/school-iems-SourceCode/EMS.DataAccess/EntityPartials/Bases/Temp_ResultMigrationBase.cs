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
    public partial class Temp_ResultMigration
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_StudentId = "StudentId";		            
		public const string Property_UserName = "UserName";		            
		public const string Property_ProgramId = "ProgramId";		            
		public const string Property_CurriculumId = "CurriculumId";		            
		public const string Property_DeptBatchId = "DeptBatchId";		            
		public const string Property_SemesterId = "SemesterId";		            
		public const string Property_ClassId = "ClassId";		            
		public const string Property_CourseCode = "CourseCode";		            
		public const string Property_CourseCredit = "CourseCredit";		            
		public const string Property_AttendanceMark = "AttendanceMark";		            
		public const string Property_AssignmentMark = "AssignmentMark";		            
		public const string Property_MidTermMark = "MidTermMark";		            
		public const string Property_FinalTermMark = "FinalTermMark";		            
		public const string Property_TotalMark = "TotalMark";		            
		public const string Property_Grade = "Grade";		            
		public const string Property_GradePoint = "GradePoint";		            
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_TypeEnumId = "TypeEnumId";		            
        public const string Property_Type = "Type";
		public const string Property_StatusEnumId = "StatusEnumId";		            
        public const string Property_Status = "Status";
		public const string Property_ResultGenerateTypeEnumId = "ResultGenerateTypeEnumId";		            
        public const string Property_ResultGenerateType = "ResultGenerateType";
		public const string Property_Remarks = "Remarks";		            
              
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
      
     
     
      
        public static Temp_ResultMigration GetNew(Int32 id=0)
        {
           Temp_ResultMigration obj = new Temp_ResultMigration();
               obj.Id = id;
                obj.StudentId = null ; 
                obj.UserName = string.Empty ; 
                obj.ProgramId = null ; 
                obj.CurriculumId = null ; 
                obj.DeptBatchId = null ; 
                obj.SemesterId = 0 ; 
                obj.ClassId = null ; 
                obj.CourseCode = string.Empty ; 
                obj.CourseCredit = null ; 
                obj.AttendanceMark = null ; 
                obj.AssignmentMark = null ; 
                obj.MidTermMark = null ; 
                obj.FinalTermMark = null ; 
                obj.TotalMark = null ; 
                obj.Grade = null ; 
                obj.GradePoint = null ; 
                obj.CreateDate = null ; 
                obj.TypeEnumId = 0 ; 
                obj.StatusEnumId = 0 ; 
                obj.ResultGenerateTypeEnumId = 0 ; 
                obj.Remarks = null ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

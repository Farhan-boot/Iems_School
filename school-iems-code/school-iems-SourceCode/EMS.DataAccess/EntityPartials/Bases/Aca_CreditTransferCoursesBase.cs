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
    public partial class Aca_CreditTransferCourses
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_StudentId = "StudentId";		            
		public const string Property_CreditTransfarId = "CreditTransfarId";		            
		public const string Property_CurriculumCourseId = "CurriculumCourseId";		            
		public const string Property_TransferedCourseCode = "TransferedCourseCode";		            
		public const string Property_TransferedCourseName = "TransferedCourseName";		            
		public const string Property_AcceptedCourseCredit = "AcceptedCourseCredit";		            
		public const string Property_AcceptedGrade = "AcceptedGrade";		            
		public const string Property_AcceptedGradePoint = "AcceptedGradePoint";		            
		public const string Property_Remarks = "Remarks";		            
		public const string Property_History = "History";		            
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_CreateById = "CreateById";		            
		public const string Property_LastChanged = "LastChanged";		            
		public const string Property_LastChangedById = "LastChangedById";		            
              
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
      
     
     
      
        public static Aca_CreditTransferCourses GetNew(Int32 id=0)
        {
           Aca_CreditTransferCourses obj = new Aca_CreditTransferCourses();
               obj.Id = id;
                obj.StudentId = 0 ; 
                obj.CreditTransfarId = 0 ; 
                obj.CurriculumCourseId = null ; 
                obj.TransferedCourseCode = null ; 
                obj.TransferedCourseName = null ; 
                obj.AcceptedCourseCredit = 0.0F ; 
                obj.AcceptedGrade = string.Empty ; 
                obj.AcceptedGradePoint = 0.0F ; 
                obj.Remarks = null ; 
                obj.History = null ; 
                obj.CreateDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreateById = 1 ;
                obj.LastChanged = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LastChangedById = 1 ;
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

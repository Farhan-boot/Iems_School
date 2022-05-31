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
    public partial class Aca_OfferedCourse
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_Code = "Code";		            
		public const string Property_Name = "Name";		            
		public const string Property_ShortName = "ShortName";		            
		public const string Property_CurriculumCourseId = "CurriculumCourseId";		            
		public const string Property_StudyLevelTermId = "StudyLevelTermId";		            
		public const string Property_SemesterId = "SemesterId";		            
		public const string Property_SemesterLevelTermId = "SemesterLevelTermId";		            
		public const string Property_RegularExamMarkDistributionPolicyId = "RegularExamMarkDistributionPolicyId";		            
		public const string Property_ReferredExamMarkDistributionPolicyId = "ReferredExamMarkDistributionPolicyId";		            
		public const string Property_BacklogExamMarkDistributionPolicyId = "BacklogExamMarkDistributionPolicyId";		            
		public const string Property_Description = "Description";		            
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_CreateById = "CreateById";		            
		public const string Property_LastChanged = "LastChanged";		            
		public const string Property_LastChangedById = "LastChangedById";		            
		public const string Property_IsDeleted = "IsDeleted";		            
              
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
      
     
     
      
        public static Aca_OfferedCourse GetNew(Int64 id=0)
        {
           Aca_OfferedCourse obj = new Aca_OfferedCourse();
               obj.Id = id;
                obj.Code = string.Empty ; 
                obj.Name = string.Empty ; 
                obj.ShortName = null ; 
                obj.CurriculumCourseId = 0 ; 
                obj.StudyLevelTermId = 0 ; 
                obj.SemesterId = 0 ; 
                obj.SemesterLevelTermId = 0 ; 
                obj.RegularExamMarkDistributionPolicyId = 0 ; 
                obj.ReferredExamMarkDistributionPolicyId = 0 ; 
                obj.BacklogExamMarkDistributionPolicyId = 0 ; 
                obj.Description = null ; 
                obj.CreateDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreateById = 1 ;
                obj.LastChanged = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LastChangedById = 1 ;
                obj.IsDeleted = false ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

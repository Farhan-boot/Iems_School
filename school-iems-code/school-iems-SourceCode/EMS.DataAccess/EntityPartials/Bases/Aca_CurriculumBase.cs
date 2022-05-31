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
    public partial class Aca_Curriculum
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_Name = "Name";		            
		public const string Property_ShortName = "ShortName";		            
		public const string Property_Year = "Year";		            
		public const string Property_Session = "Session";		            
		public const string Property_CurriculumNo = "CurriculumNo";		            
		public const string Property_ProgramId = "ProgramId";		            
		public const string Property_Description = "Description";		            
		public const string Property_IsValid = "IsValid";		            
		public const string Property_IsOffering = "IsOffering";		            
		public const string Property_TotalCourse = "TotalCourse";		            
		public const string Property_GradingPolicyId = "GradingPolicyId";		            
		public const string Property_CourseRequireForGraduation = "CourseRequireForGraduation";		            
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
      
     
     
      
        public static Aca_Curriculum GetNew(Int64 id=0)
        {
           Aca_Curriculum obj = new Aca_Curriculum();
               obj.Id = id;
                obj.Name = string.Empty ; 
                obj.ShortName = string.Empty ; 
                obj.Year = 0 ; 
                obj.Session = string.Empty ; 
                obj.CurriculumNo = 0 ; 
                obj.ProgramId = 0 ; 
                obj.Description = null ; 
                obj.IsValid = false ; 
                obj.IsOffering = false ; 
                obj.TotalCourse = 0.0F ; 
                obj.GradingPolicyId = 0 ; 
                obj.CourseRequireForGraduation = 0.0F ; 
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

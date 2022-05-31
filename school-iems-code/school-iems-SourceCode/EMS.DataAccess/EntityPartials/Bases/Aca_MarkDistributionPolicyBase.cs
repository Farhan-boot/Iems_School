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
    public partial class Aca_MarkDistributionPolicy
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_Name = "Name";		            
		public const string Property_CreditHour = "CreditHour";		            
		public const string Property_TotalMark = "TotalMark";		            
		public const string Property_CourseCategoryEnumId = "CourseCategoryEnumId";		            
        public const string Property_CourseCategory = "CourseCategory";
		public const string Property_ConvertPercentage = "ConvertPercentage";		            
		public const string Property_ExamTypeEnumId = "ExamTypeEnumId";		            
        public const string Property_ExamType = "ExamType";
		public const string Property_IsFixedPolicy = "IsFixedPolicy";		            
		public const string Property_StatusEnumId = "StatusEnumId";		            
        public const string Property_Status = "Status";
		public const string Property_Remark = "Remark";		            
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_CreateById = "CreateById";		            
		public const string Property_LastChanged = "LastChanged";		            
		public const string Property_LastChangedById = "LastChangedById";		            
		public const string Property_IsDeleted = "IsDeleted";		            
		public const string Property_ProgramId = "ProgramId";		            
		public const string Property_StartSemesterId = "StartSemesterId";		            
		public const string Property_EndSemesterId = "EndSemesterId";		            
              
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
      
     
     
      
        public static Aca_MarkDistributionPolicy GetNew(Int64 id=0)
        {
           Aca_MarkDistributionPolicy obj = new Aca_MarkDistributionPolicy();
               obj.Id = id;
                obj.Name = string.Empty ; 
                obj.CreditHour = 0.0F ; 
                obj.TotalMark = 0.0F ; 
                obj.CourseCategoryEnumId = 0 ; 
                obj.ConvertPercentage = 0.0F ; 
                obj.ExamTypeEnumId = 0 ; 
                obj.IsFixedPolicy = false ; 
                obj.StatusEnumId = 0 ; 
                obj.Remark = null ; 
                obj.CreateDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreateById = 1 ;
                obj.LastChanged = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LastChangedById = 1 ;
                obj.IsDeleted = false ; 
                obj.ProgramId = null ; 
                obj.StartSemesterId = 0 ; 
                obj.EndSemesterId = null ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

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
    public partial class Aca_CurriculumCourse
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_CourseId = "CourseId";		            
		public const string Property_Code = "Code";		            
		public const string Property_Name = "Name";		            
		public const string Property_ShortName = "ShortName";		            
		public const string Property_Description = "Description";		            
		public const string Property_CurriculumId = "CurriculumId";		            
		public const string Property_CreditLoad = "CreditLoad";		            
		public const string Property_CreditHour = "CreditHour";		            
		public const string Property_CourseCategoryEnumId = "CourseCategoryEnumId";		            
        public const string Property_CourseCategory = "CourseCategory";
		public const string Property_CourseTypeEnumId = "CourseTypeEnumId";		            
        public const string Property_CourseType = "CourseType";
		public const string Property_StatusEnumId = "StatusEnumId";		            
        public const string Property_Status = "Status";
		public const string Property_ElectiveGroupId = "ElectiveGroupId";		            
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_CreateById = "CreateById";		            
		public const string Property_LastChanged = "LastChanged";		            
		public const string Property_LastChangedById = "LastChangedById";		            
		public const string Property_IsDeleted = "IsDeleted";		            
		public const string Property_History = "History";		            
		public const string Property_OrderNo = "OrderNo";		            
              
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
      
     
     
      
        public static Aca_CurriculumCourse GetNew(Int64 id=0)
        {
           Aca_CurriculumCourse obj = new Aca_CurriculumCourse();
               obj.Id = id;
                obj.CourseId = 0 ; 
                obj.Code = string.Empty ; 
                obj.Name = string.Empty ; 
                obj.ShortName = null ; 
                obj.Description = null ; 
                obj.CurriculumId = 0 ; 
                obj.CreditLoad = 0.0F ; 
                obj.CreditHour = 0.0F ; 
                obj.CourseCategoryEnumId = 0 ; 
                obj.CourseTypeEnumId = 0 ; 
                obj.StatusEnumId = 0 ; 
                obj.ElectiveGroupId = null ; 
                obj.CreateDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreateById = 1 ;
                obj.LastChanged = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LastChangedById = 1 ;
                obj.IsDeleted = false ; 
                obj.History = null ; 
                obj.OrderNo = 0 ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

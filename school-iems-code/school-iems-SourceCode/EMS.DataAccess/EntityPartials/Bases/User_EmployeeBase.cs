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
    public partial class User_Employee
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_UserId = "UserId";		            
		public const string Property_EmployeeCategoryEnumId = "EmployeeCategoryEnumId";		            
        public const string Property_EmployeeCategory = "EmployeeCategory";
		public const string Property_EmployeeTypeEnumId = "EmployeeTypeEnumId";		            
        public const string Property_EmployeeType = "EmployeeType";
		public const string Property_JobClassEnumId = "JobClassEnumId";		            
        public const string Property_JobClass = "JobClass";
		public const string Property_JobTypeEnumId = "JobTypeEnumId";		            
        public const string Property_JobType = "JobType";
		public const string Property_WorkingStatusEnumId = "WorkingStatusEnumId";		            
        public const string Property_WorkingStatus = "WorkingStatus";
		public const string Property_TinNumber = "TinNumber";		            
		public const string Property_ImmediateSuperVisorId = "ImmediateSuperVisorId";		            
		public const string Property_IsAcademician = "IsAcademician";		            
		public const string Property_IncrementMonthEnumId = "IncrementMonthEnumId";		            
        public const string Property_IncrementMonth = "IncrementMonth";
		public const string Property_JoiningDepartmentId = "JoiningDepartmentId";		            
		public const string Property_PositionId = "PositionId";		            
		public const string Property_HasAdminAccess = "HasAdminAccess";		            
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_CreateById = "CreateById";		            
		public const string Property_LastChanged = "LastChanged";		            
		public const string Property_LastChangedById = "LastChangedById";		            
		public const string Property_SalaryTemplateId = "SalaryTemplateId";		            
		public const string Property_SalaryGradeEnumId = "SalaryGradeEnumId";		            
        public const string Property_SalaryGrade = "SalaryGrade";
              
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
      
     
     
      
        public static User_Employee GetNew(Int32 id=0)
        {
           User_Employee obj = new User_Employee();
               obj.Id = id;
                obj.UserId = 0 ; 
                obj.EmployeeCategoryEnumId = 0 ; 
                obj.EmployeeTypeEnumId = 0 ; 
                obj.JobClassEnumId = 0 ; 
                obj.JobTypeEnumId = 0 ; 
                obj.WorkingStatusEnumId = 0 ; 
                obj.TinNumber = null ; 
                obj.ImmediateSuperVisorId = null ; 
                obj.IsAcademician = false ; 
                obj.IncrementMonthEnumId = 0 ; 
                obj.JoiningDepartmentId = 0 ; 
                obj.PositionId = null ; 
                obj.HasAdminAccess = false ; 
                obj.CreateDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreateById = 1 ;
                obj.LastChanged = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LastChangedById = 1 ;
                obj.SalaryTemplateId = null ; 
                obj.SalaryGradeEnumId = null ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

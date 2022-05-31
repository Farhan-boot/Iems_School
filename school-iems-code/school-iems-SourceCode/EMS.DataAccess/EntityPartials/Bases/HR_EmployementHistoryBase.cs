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
    public partial class HR_EmployementHistory
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_EmployeeId = "EmployeeId";		            
		public const string Property_RankId = "RankId";		            
		public const string Property_DepartmentId = "DepartmentId";		            
		public const string Property_StartDate = "StartDate";		            
		public const string Property_EndDate = "EndDate";		            
		public const string Property_IsActive = "IsActive";		            
		public const string Property_EmployementTypeEnumId = "EmployementTypeEnumId";		            
        public const string Property_EmployementType = "EmployementType";
		public const string Property_JobTypeEnumId = "JobTypeEnumId";		            
        public const string Property_JobType = "JobType";
		public const string Property_HistoryTypeEnumId = "HistoryTypeEnumId";		            
        public const string Property_HistoryType = "HistoryType";
		public const string Property_IsPrimary = "IsPrimary";		            
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
      
     
     
      
        public static HR_EmployementHistory GetNew(Int64 id=0)
        {
           HR_EmployementHistory obj = new HR_EmployementHistory();
               obj.Id = id;
                obj.EmployeeId = 0 ; 
                obj.RankId = 0 ; 
                obj.DepartmentId = 0 ; 
                obj.StartDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.EndDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.IsActive = false ; 
                obj.EmployementTypeEnumId = 0 ; 
                obj.JobTypeEnumId = 0 ; 
                obj.HistoryTypeEnumId = 0 ; 
                obj.IsPrimary = false ; 
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

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
    public partial class HR_EmpLeaveApplication
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_LeaveAllocationSettingsId = "LeaveAllocationSettingsId";		            
		public const string Property_EmployeeId = "EmployeeId";		            
		public const string Property_LeaveTypeEnumId = "LeaveTypeEnumId";		            
        public const string Property_LeaveType = "LeaveType";
		public const string Property_LeaveFrom = "LeaveFrom";		            
		public const string Property_LeaveTill = "LeaveTill";		            
		public const string Property_LeaveHour = "LeaveHour";		            
		public const string Property_LeaveReason = "LeaveReason";		            
		public const string Property_StatusEnumId = "StatusEnumId";		            
        public const string Property_Status = "Status";
		public const string Property_RecommendByEmployeeId = "RecommendByEmployeeId";		            
		public const string Property_Remarks = "Remarks";		            
		public const string Property_History = "History";		            
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
      
     
     
      
        public static HR_EmpLeaveApplication GetNew(Int32 id=0)
        {
           HR_EmpLeaveApplication obj = new HR_EmpLeaveApplication();
               obj.Id = id;
                obj.LeaveAllocationSettingsId = 0 ; 
                obj.EmployeeId = 0 ; 
                obj.LeaveTypeEnumId = 0 ; 
                obj.LeaveFrom = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LeaveTill = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LeaveHour = 0.0F ; 
                obj.LeaveReason = string.Empty ; 
                obj.StatusEnumId = 0 ; 
                obj.RecommendByEmployeeId = null ; 
                obj.Remarks = null ; 
                obj.History = null ; 
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

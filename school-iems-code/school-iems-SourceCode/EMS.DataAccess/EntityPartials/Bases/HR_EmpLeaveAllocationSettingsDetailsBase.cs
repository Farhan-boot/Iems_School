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
    public partial class HR_EmpLeaveAllocationSettingsDetails
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_Name = "Name";		            
		public const string Property_EmpLeaveAllocationSettingsId = "EmpLeaveAllocationSettingsId";		            
		public const string Property_LeaveTypeEnumId = "LeaveTypeEnumId";		            
        public const string Property_LeaveType = "LeaveType";
		public const string Property_AllowedDays = "AllowedDays";		            
		public const string Property_WorkingHourPerDays = "WorkingHourPerDays";		            
		public const string Property_HourUsed = "HourUsed";		            
		public const string Property_IsPaid = "IsPaid";		            
		public const string Property_Remarks = "Remarks";		            
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
      
     
     
      
        public static HR_EmpLeaveAllocationSettingsDetails GetNew(Int32 id=0)
        {
           HR_EmpLeaveAllocationSettingsDetails obj = new HR_EmpLeaveAllocationSettingsDetails();
               obj.Id = id;
                obj.Name = string.Empty ; 
                obj.EmpLeaveAllocationSettingsId = 0 ; 
                obj.LeaveTypeEnumId = 0 ; 
                obj.AllowedDays = 0 ; 
                obj.WorkingHourPerDays = 0.0F ; 
                obj.HourUsed = 0.0F ; 
                obj.IsPaid = false ; 
                obj.Remarks = null ; 
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

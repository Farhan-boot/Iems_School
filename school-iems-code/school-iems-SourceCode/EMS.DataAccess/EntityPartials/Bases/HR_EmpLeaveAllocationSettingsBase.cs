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
    public partial class HR_EmpLeaveAllocationSettings
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_EmployeeId = "EmployeeId";		            
		public const string Property_TotalLeaveHour = "TotalLeaveHour";		            
		public const string Property_IsCurrent = "IsCurrent";		            
		public const string Property_EffectiveFrom = "EffectiveFrom";		            
		public const string Property_EffectiveTill = "EffectiveTill";		            
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
      
     
     
      
        public static HR_EmpLeaveAllocationSettings GetNew(Int32 id=0)
        {
           HR_EmpLeaveAllocationSettings obj = new HR_EmpLeaveAllocationSettings();
               obj.Id = id;
                obj.EmployeeId = 0 ; 
                obj.TotalLeaveHour = 0.0F ; 
                obj.IsCurrent = false ; 
                obj.EffectiveFrom = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.EffectiveTill = new DateTime(1753,1,1,12,0,0,0) ; 
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

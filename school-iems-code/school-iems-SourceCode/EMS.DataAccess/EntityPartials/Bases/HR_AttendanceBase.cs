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
    public partial class HR_Attendance
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_EmployeeId = "EmployeeId";		            
		public const string Property_PunchTypeEnumId = "PunchTypeEnumId";		            
        public const string Property_PunchType = "PunchType";
		public const string Property_PunchTime = "PunchTime";		            
		public const string Property_PunchModeEnumId = "PunchModeEnumId";		            
        public const string Property_PunchMode = "PunchMode";
		public const string Property_DeviceId = "DeviceId";		            
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
      
     
     
      
        public static HR_Attendance GetNew(Int32 id=0)
        {
           HR_Attendance obj = new HR_Attendance();
               obj.Id = id;
                obj.EmployeeId = 0 ; 
                obj.PunchTypeEnumId = 0 ; 
                obj.PunchTime = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.PunchModeEnumId = 0 ; 
                obj.DeviceId = null ; 
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

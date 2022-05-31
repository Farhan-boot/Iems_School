﻿//File: Entity Partial Base
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;

namespace EMS.DataAccess.Data
{

    /// <summary>
    /// Do not modify this class, edit partial instead.
    /// </summary>   
    public partial class HR_AppointmentHistory
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_EmployeeId = "EmployeeId";		            
		public const string Property_PositionId = "PositionId";		            
		public const string Property_DepartmentId = "DepartmentId";		            
		public const string Property_StartDate = "StartDate";		            
		public const string Property_EndDate = "EndDate";		            
		public const string Property_IsActive = "IsActive";		            
		public const string Property_IsActing = "IsActing";		            
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
      
     
     
      
        public static HR_AppointmentHistory GetNew(Int64 id=0)
        {
           HR_AppointmentHistory obj = new HR_AppointmentHistory();
               obj.Id = id;
                obj.EmployeeId = 0 ; 
                obj.PositionId = 0 ; 
                obj.DepartmentId = 0 ; 
                obj.StartDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.EndDate = null ; 
                obj.IsActive = false ; 
                obj.IsActing = false ; 
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

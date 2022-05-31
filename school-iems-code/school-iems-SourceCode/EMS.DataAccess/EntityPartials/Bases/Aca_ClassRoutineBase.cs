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
    public partial class Aca_ClassRoutine
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_DayEnumId = "DayEnumId";		            
        public const string Property_Day = "Day";
		public const string Property_StartTime = "StartTime";		            
		public const string Property_EndTime = "EndTime";		            
		public const string Property_ClassId = "ClassId";		            
		public const string Property_RoomId = "RoomId";		            
		public const string Property_EmployeeId = "EmployeeId";		            
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_CreateById = "CreateById";		            
		public const string Property_LastChanged = "LastChanged";		            
		public const string Property_LastChangedById = "LastChangedById";		            
		public const string Property_Remarks = "Remarks";		            
              
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
      
     
     
      
        public static Aca_ClassRoutine GetNew(Int64 id=0)
        {
           Aca_ClassRoutine obj = new Aca_ClassRoutine();
               obj.Id = id;
                obj.DayEnumId = 0 ; 
                obj.StartTime = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.EndTime = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.ClassId = 0 ; 
                obj.RoomId = null ; 
                obj.EmployeeId = null ; 
                obj.CreateDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreateById = 1 ;
                obj.LastChanged = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LastChangedById = 1 ;
                obj.Remarks = null ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

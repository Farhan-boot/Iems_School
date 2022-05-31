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
    public partial class HR_WorkWeek
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_DayEnumId = "DayEnumId";		            
        public const string Property_Day = "Day";
		public const string Property_IsHalfDay = "IsHalfDay";		            
		public const string Property_WorkingDayTypeEnumId = "WorkingDayTypeEnumId";		            
        public const string Property_WorkingDayType = "WorkingDayType";
		public const string Property_CalendarYearId = "CalendarYearId";		            
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
      
     
     
      
        public static HR_WorkWeek GetNew(Int32 id=0)
        {
           HR_WorkWeek obj = new HR_WorkWeek();
               obj.Id = id;
                obj.DayEnumId = 0 ; 
                obj.IsHalfDay = false ; 
                obj.WorkingDayTypeEnumId = 0 ; 
                obj.CalendarYearId = 0 ; 
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

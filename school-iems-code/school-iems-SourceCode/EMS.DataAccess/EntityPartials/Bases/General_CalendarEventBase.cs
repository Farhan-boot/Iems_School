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
    public partial class General_CalendarEvent
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_Name = "Name";		            
		public const string Property_Description = "Description";		            
		public const string Property_Location = "Location";		            
		public const string Property_StartTime = "StartTime";		            
		public const string Property_EndTime = "EndTime";		            
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_CreateById = "CreateById";		            
		public const string Property_LastChanged = "LastChanged";		            
		public const string Property_LastChangedById = "LastChangedById";		            
		public const string Property_Remarks = "Remarks";		            
		public const string Property_IsAllDay = "IsAllDay";		            
		public const string Property_EventColor = "EventColor";		            
		public const string Property_VisibilityEnumId = "VisibilityEnumId";		            
        public const string Property_Visibility = "Visibility";
		public const string Property_RepeatsEnumId = "RepeatsEnumId";		            
        public const string Property_Repeats = "Repeats";
		public const string Property_RepeatsPeriodEnumId = "RepeatsPeriodEnumId";		            
        public const string Property_RepeatsPeriod = "RepeatsPeriod";
              
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
      
     
     
      
        public static General_CalendarEvent GetNew(Int64 id=0)
        {
           General_CalendarEvent obj = new General_CalendarEvent();
               obj.Id = id;
                obj.Name = string.Empty ; 
                obj.Description = string.Empty ; 
                obj.Location = null ; 
                obj.StartTime = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.EndTime = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreateDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreateById = 1 ;
                obj.LastChanged = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LastChangedById = 1 ;
                obj.Remarks = null ; 
                obj.IsAllDay = false ; 
                obj.EventColor = string.Empty ; 
                obj.VisibilityEnumId = 0 ; 
                obj.RepeatsEnumId = 0 ; 
                obj.RepeatsPeriodEnumId = 0 ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

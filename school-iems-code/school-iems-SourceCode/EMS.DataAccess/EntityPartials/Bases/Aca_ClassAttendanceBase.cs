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
    public partial class Aca_ClassAttendance
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_SettingId = "SettingId";		            
		public const string Property_ClassId = "ClassId";		            
		public const string Property_StudentId = "StudentId";		            
		public const string Property_IsAbsent = "IsAbsent";		            
		public const string Property_IsLate = "IsLate";		            
		public const string Property_IsLeftEarly = "IsLeftEarly";		            
		public const string Property_ReasonEnumId = "ReasonEnumId";		            
        public const string Property_Reason = "Reason";
		public const string Property_Remark = "Remark";		            
		public const string Property_StartTime = "StartTime";		            
		public const string Property_EndTime = "EndTime";		            
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
      
     
     
      
        public static Aca_ClassAttendance GetNew(Int64 id=0)
        {
           Aca_ClassAttendance obj = new Aca_ClassAttendance();
               obj.Id = id;
                obj.SettingId = 0 ; 
                obj.ClassId = 0 ; 
                obj.StudentId = 0 ; 
                obj.IsAbsent = false ; 
                obj.IsLate = false ; 
                obj.IsLeftEarly = false ; 
                obj.ReasonEnumId = null ; 
                obj.Remark = null ; 
                obj.StartTime = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.EndTime = new DateTime(1753,1,1,12,0,0,0) ; 
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

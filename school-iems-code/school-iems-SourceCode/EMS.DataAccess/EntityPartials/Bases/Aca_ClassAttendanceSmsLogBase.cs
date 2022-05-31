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
    public partial class Aca_ClassAttendanceSmsLog
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_ClassId = "ClassId";		            
		public const string Property_StudentId = "StudentId";		            
		public const string Property_AttendanceSettingId = "AttendanceSettingId";		            
		public const string Property_SmsText = "SmsText";		            
		public const string Property_MobileNumber = "MobileNumber";		            
		public const string Property_DeliveryStatusEnumId = "DeliveryStatusEnumId";		            
        public const string Property_DeliveryStatus = "DeliveryStatus";
		public const string Property_DeliveryErrorText = "DeliveryErrorText";		            
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_CreateById = "CreateById";		            
              
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
      
     
     
      
        public static Aca_ClassAttendanceSmsLog GetNew(Int64 id=0)
        {
           Aca_ClassAttendanceSmsLog obj = new Aca_ClassAttendanceSmsLog();
               obj.Id = id;
                obj.ClassId = 0 ; 
                obj.StudentId = 0 ; 
                obj.AttendanceSettingId = 0 ; 
                obj.SmsText = string.Empty ; 
                obj.MobileNumber = string.Empty ; 
                obj.DeliveryStatusEnumId = 0 ; 
                obj.DeliveryErrorText = null ; 
                obj.CreateDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreateById = 1 ;
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

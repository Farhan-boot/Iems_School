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
    public partial class Aca_OnlineExamTakenByStudent
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_StudentId = "StudentId";		            
		public const string Property_OnlineExamId = "OnlineExamId";		            
		public const string Property_ObtainedTotalMark = "ObtainedTotalMark";		            
		public const string Property_AttendanceTypeEnumId = "AttendanceTypeEnumId";		            
        public const string Property_AttendanceType = "AttendanceType";
		public const string Property_IsResultPublished = "IsResultPublished";		            
		public const string Property_Remark = "Remark";		            
		public const string Property_History = "History";		            
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
      
     
     
      
        public static Aca_OnlineExamTakenByStudent GetNew(Int32 id=0)
        {
           Aca_OnlineExamTakenByStudent obj = new Aca_OnlineExamTakenByStudent();
               obj.Id = id;
                obj.StudentId = 0 ; 
                obj.OnlineExamId = 0 ; 
                obj.ObtainedTotalMark = 0.0F ; 
                obj.AttendanceTypeEnumId = 0 ; 
                obj.IsResultPublished = false ; 
                obj.Remark = string.Empty ; 
                obj.History = string.Empty ; 
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

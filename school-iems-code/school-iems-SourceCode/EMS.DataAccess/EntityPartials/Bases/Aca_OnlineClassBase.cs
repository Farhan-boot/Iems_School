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
    public partial class Aca_OnlineClass
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_Name = "Name";		            
		public const string Property_ClassDateTime = "ClassDateTime";		            
		public const string Property_ClassDuration = "ClassDuration";		            
		public const string Property_ProgramId = "ProgramId";		            
		public const string Property_CurriculumCourseId = "CurriculumCourseId";		            
		public const string Property_ClassMediaEnumId = "ClassMediaEnumId";		            
        public const string Property_ClassMedia = "ClassMedia";
		public const string Property_ClassUrl = "ClassUrl";		            
		public const string Property_Remark = "Remark";		            
		public const string Property_History = "History";		            
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_CreateById = "CreateById";		            
		public const string Property_LastChanged = "LastChanged";		            
		public const string Property_LastChangedById = "LastChangedById";		            
		public const string Property_IsDeleted = "IsDeleted";		            
		public const string Property_InquiryMobileNumber = "InquiryMobileNumber";		            
		public const string Property_SyllabusInformation = "SyllabusInformation";		            
		public const string Property_IsRecurring = "IsRecurring";		            
		public const string Property_RecurringDayEnumId = "RecurringDayEnumId";		            
        public const string Property_RecurringDay = "RecurringDay";
              
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
      
     
     
      
        public static Aca_OnlineClass GetNew(Int32 id=0)
        {
           Aca_OnlineClass obj = new Aca_OnlineClass();
               obj.Id = id;
                obj.Name = string.Empty ; 
                obj.ClassDateTime = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.ClassDuration = 0.0F ; 
                obj.ProgramId = 0 ; 
                obj.CurriculumCourseId = 0 ; 
                obj.ClassMediaEnumId = 0 ; 
                obj.ClassUrl = string.Empty ; 
                obj.Remark = null ; 
                obj.History = null ; 
                obj.CreateDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreateById = 1 ;
                obj.LastChanged = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LastChangedById = 1 ;
                obj.IsDeleted = false ; 
                obj.InquiryMobileNumber = null ; 
                obj.SyllabusInformation = null ; 
                obj.IsRecurring = false ; 
                obj.RecurringDayEnumId = null ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

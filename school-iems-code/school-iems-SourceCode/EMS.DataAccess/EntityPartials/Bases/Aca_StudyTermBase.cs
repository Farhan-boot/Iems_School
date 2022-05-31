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
    public partial class Aca_StudyTerm
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_Name = "Name";		            
		public const string Property_TermNo = "TermNo";		            
		public const string Property_SemesterName = "SemesterName";		            
		public const string Property_IsEnabled = "IsEnabled";		            
		public const string Property_SemesterDurationEnumId = "SemesterDurationEnumId";		            
        public const string Property_SemesterDuration = "SemesterDuration";
              
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
      
     
     
      
        public static Aca_StudyTerm GetNew(Int32 id=0)
        {
           Aca_StudyTerm obj = new Aca_StudyTerm();
               obj.Id = id;
                obj.Name = string.Empty ; 
                obj.TermNo = 0 ; 
                obj.SemesterName = string.Empty ; 
                obj.IsEnabled = false ; 
                obj.SemesterDurationEnumId = 0 ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

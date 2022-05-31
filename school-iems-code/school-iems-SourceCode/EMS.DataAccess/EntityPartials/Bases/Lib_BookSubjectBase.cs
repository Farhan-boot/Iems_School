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
    public partial class Lib_BookSubject
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_SubjectName = "SubjectName";		            
		public const string Property_StatusEnumId = "StatusEnumId";		            
        public const string Property_Status = "Status";
              
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
      
     
     
      
        public static Lib_BookSubject GetNew(Int64 id=0)
        {
           Lib_BookSubject obj = new Lib_BookSubject();
               obj.Id = id;
                obj.SubjectName = string.Empty ; 
                obj.StatusEnumId = 0 ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

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
    public partial class Lib_BookSubjectMap
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_BookId = "BookId";		            
		public const string Property_SubjectId = "SubjectId";		            
		public const string Property_StatusEnumId = "StatusEnumId";		            
        public const string Property_Status = "Status";
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
      
     
     
      
        public static Lib_BookSubjectMap GetNew(Int64 id=0)
        {
           Lib_BookSubjectMap obj = new Lib_BookSubjectMap();
               obj.Id = id;
                obj.BookId = 0 ; 
                obj.SubjectId = 0 ; 
                obj.StatusEnumId = 0 ; 
                obj.Remarks = string.Empty ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

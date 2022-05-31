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
    public partial class Lib_BookActivityLog
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_BorrowerId = "BorrowerId";		            
		public const string Property_BarcodeId = "BarcodeId";		            
		public const string Property_BookActivityEnumId = "BookActivityEnumId";		            
        public const string Property_BookActivity = "BookActivity";
		public const string Property_DoneOn = "DoneOn";		            
              
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
      
     
     
      
        public static Lib_BookActivityLog GetNew(Int64 id=0)
        {
           Lib_BookActivityLog obj = new Lib_BookActivityLog();
               obj.Id = id;
                obj.BorrowerId = 0 ; 
                obj.BarcodeId = 0 ; 
                obj.BookActivityEnumId = 0 ; 
                obj.DoneOn = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

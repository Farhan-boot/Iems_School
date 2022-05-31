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
    public partial class Lib_BookCopyTransaction
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_BorrowerId = "BorrowerId";		            
		public const string Property_BookCopyId = "BookCopyId";		            
		public const string Property_BorrowedDate = "BorrowedDate";		            
		public const string Property_IssuedById = "IssuedById";		            
		public const string Property_ExpectedReturnDate = "ExpectedReturnDate";		            
		public const string Property_ReturnedDate = "ReturnedDate";		            
		public const string Property_CollectedById = "CollectedById";		            
		public const string Property_Fine = "Fine";		            
              
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
      
     
     
      
        public static Lib_BookCopyTransaction GetNew(Int64 id=0)
        {
           Lib_BookCopyTransaction obj = new Lib_BookCopyTransaction();
               obj.Id = id;
                obj.BorrowerId = 0 ; 
                obj.BookCopyId = 0 ; 
                obj.BorrowedDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.IssuedById = 0 ; 
                obj.ExpectedReturnDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.ReturnedDate = null ; 
                obj.CollectedById = null ; 
                obj.Fine = null ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

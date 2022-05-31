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
    public partial class Lib_BookCopy
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_AccessionNo = "AccessionNo";		            
		public const string Property_Barcode = "Barcode";		            
		public const string Property_BookId = "BookId";		            
		public const string Property_DateAcquired = "DateAcquired";		            
		public const string Property_Shelf = "Shelf";		            
		public const string Property_CopySourceEnumId = "CopySourceEnumId";		            
        public const string Property_CopySource = "CopySource";
		public const string Property_Price = "Price";		            
		public const string Property_SupplierId = "SupplierId";		            
		public const string Property_BorrowingAllowedEnumId = "BorrowingAllowedEnumId";		            
        public const string Property_BorrowingAllowed = "BorrowingAllowed";
		public const string Property_AvailabilityEnumId = "AvailabilityEnumId";		            
        public const string Property_Availability = "Availability";
		public const string Property_ReservationEnumId = "ReservationEnumId";		            
        public const string Property_Reservation = "Reservation";
		public const string Property_CopyStatusEnumId = "CopyStatusEnumId";		            
        public const string Property_CopyStatus = "CopyStatus";
		public const string Property_DeletedEnumId = "DeletedEnumId";		            
        public const string Property_Deleted = "Deleted";
		public const string Property_CopyTypeEnumId = "CopyTypeEnumId";		            
        public const string Property_CopyType = "CopyType";
		public const string Property_Remarks = "Remarks";		            
		public const string Property_CallNo = "CallNo";		            
		public const string Property_PermanentLibraryId = "PermanentLibraryId";		            
		public const string Property_PresentLibraryId = "PresentLibraryId";		            
		public const string Property_CategoryEnumId = "CategoryEnumId";		            
        public const string Property_Category = "Category";
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
      
     
     
      
        public static Lib_BookCopy GetNew(Int64 id=0)
        {
           Lib_BookCopy obj = new Lib_BookCopy();
               obj.Id = id;
                obj.AccessionNo = 0 ; 
                obj.Barcode = string.Empty ; 
                obj.BookId = 0 ; 
                obj.DateAcquired = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.Shelf = null ; 
                obj.CopySourceEnumId = 0 ; 
                obj.Price = null ; 
                obj.SupplierId = null ; 
                obj.BorrowingAllowedEnumId = 0 ; 
                obj.AvailabilityEnumId = 0 ; 
                obj.ReservationEnumId = 0 ; 
                obj.CopyStatusEnumId = 0 ; 
                obj.DeletedEnumId = 0 ; 
                obj.CopyTypeEnumId = 0 ; 
                obj.Remarks = null ; 
                obj.CallNo = null ; 
                obj.PermanentLibraryId = null ; 
                obj.PresentLibraryId = null ; 
                obj.CategoryEnumId = 0 ; 
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

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
    public partial class Lib_Borrower
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_BorrowerTypeEnumId = "BorrowerTypeEnumId";		            
        public const string Property_BorrowerType = "BorrowerType";
		public const string Property_FineAmount = "FineAmount";		            
		public const string Property_BorrowLimit = "BorrowLimit";		            
		public const string Property_CreditLimit = "CreditLimit";		            
		public const string Property_BorrowerStatusEnumId = "BorrowerStatusEnumId";		            
        public const string Property_BorrowerStatus = "BorrowerStatus";
		public const string Property_TotalFine = "TotalFine";		            
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
      
     
     
      
        public static Lib_Borrower GetNew(Int32 id=0)
        {
           Lib_Borrower obj = new Lib_Borrower();
               obj.Id = id;
                obj.BorrowerTypeEnumId = 0 ; 
                obj.FineAmount = 0.0m ; 
                obj.BorrowLimit = 0 ; 
                obj.CreditLimit = 0.0m ; 
                obj.BorrowerStatusEnumId = 0 ; 
                obj.TotalFine = 0.0m ; 
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

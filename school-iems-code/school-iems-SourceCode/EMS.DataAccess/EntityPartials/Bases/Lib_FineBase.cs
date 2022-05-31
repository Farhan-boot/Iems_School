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
    public partial class Lib_Fine
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_BorrowerId = "BorrowerId";		            
		public const string Property_LibraryFineTypeEnumId = "LibraryFineTypeEnumId";		            
        public const string Property_LibraryFineType = "LibraryFineType";
		public const string Property_IsCredit = "IsCredit";		            
		public const string Property_Amount = "Amount";		            
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
      
     
     
      
        public static Lib_Fine GetNew(Int64 id=0)
        {
           Lib_Fine obj = new Lib_Fine();
               obj.Id = id;
                obj.BorrowerId = 0 ; 
                obj.LibraryFineTypeEnumId = 0 ; 
                obj.IsCredit = false ; 
                obj.Amount = 0.0m ; 
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

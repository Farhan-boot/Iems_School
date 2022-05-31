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
    public partial class Inv_ProductCategoryDetails
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_ProductCategoryId = "ProductCategoryId";		            
		public const string Property_Unit = "Unit";		            
		public const string Property_UnitType = "UnitType";
        public const string Property_UnitTypeEnumId = "UnitTypeEnumId";
        public const string Property_WarrningQuantity = "WarrningQuantity";		            
		public const string Property_Details = "Details";		            
		public const string Property_HasBarcode = "HasBarcode";		            
		public const string Property_LastChanged = "LastChanged";		            
		public const string Property_LastChangedById = "LastChangedById";		            
		public const string Property_IsDeleted = "IsDeleted";		            
              
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
      
     
     
      
        public static Inv_ProductCategoryDetails GetNew(Int32 id=0)
        {
           Inv_ProductCategoryDetails obj = new Inv_ProductCategoryDetails();
               obj.Id = id;
                obj.ProductCategoryId = 0 ; 
                obj.Unit = null ; 
                obj.UnitTypeEnumId = null ; 
                obj.WarrningQuantity = null ; 
                obj.Details = null ; 
                obj.HasBarcode = null ; 
                obj.LastChanged = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LastChangedById = 1 ;
                obj.IsDeleted = false ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

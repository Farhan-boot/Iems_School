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
    public partial class Inv_InventoryDetails
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_InventoryId = "InventoryId";		            
		public const string Property_ProductCategoryId = "ProductCategoryId";		            
		public const string Property_Quantity = "Quantity";		            
		public const string Property_UnitPrice = "UnitPrice";		            
		public const string Property_WarrentyStartDate = "WarrentyStartDate";		            
		public const string Property_WarrentyExpairDate = "WarrentyExpairDate";		            
		public const string Property_Description = "Description";		            
		public const string Property_OwnBarcode = "OwnBarcode";		            
		public const string Property_OurBarcode = "OurBarcode";		            
		public const string Property_StatusEnumId = "StatusEnumId";		            
        public const string Property_Status = "Status";
		public const string Property_Remark = "Remark";		            
		public const string Property_IsBarcoded = "IsBarcoded";		            
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
      
     
     
      
        public static Inv_InventoryDetails GetNew(Int32 id=0)
        {
           Inv_InventoryDetails obj = new Inv_InventoryDetails();
               obj.Id = id;
                obj.InventoryId = null ; 
                obj.ProductCategoryId = null ; 
                obj.Quantity = null ; 
                obj.UnitPrice = null ; 
                obj.WarrentyStartDate = null ; 
                obj.WarrentyExpairDate = null ; 
                obj.Description = null ; 
                obj.OwnBarcode = null ; 
                obj.OurBarcode = null ; 
                obj.StatusEnumId = null ; 
                obj.Remark = null ; 
                obj.IsBarcoded = null ; 
                obj.LastChanged = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LastChangedById = 1 ;
                obj.IsDeleted = false ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

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
    public partial class Inv_PurchaseRequisitionDetail
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_ItemName = "ItemName";		            
		public const string Property_ProductCategoryId = "ProductCategoryId";		            
		public const string Property_Detail = "Detail";		            
		public const string Property_Quantity = "Quantity";		            
		public const string Property_UnitPrice = "UnitPrice";		            
		public const string Property_PurchaseRequisitionId = "PurchaseRequisitionId";		            
		public const string Property_UnitTypeEnumId = "UnitTypeEnumId";		            
        public const string Property_UnitType = "UnitType";
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
      
     
     
      
        public static Inv_PurchaseRequisitionDetail GetNew(Int32 id=0)
        {
           Inv_PurchaseRequisitionDetail obj = new Inv_PurchaseRequisitionDetail();
               obj.Id = id;
                obj.ItemName = null ; 
                obj.ProductCategoryId = null ; 
                obj.Detail = null ; 
                obj.Quantity = null ; 
                obj.UnitPrice = null ; 
                obj.PurchaseRequisitionId = 0 ; 
                obj.UnitTypeEnumId = 0 ; 
                obj.LastChanged = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LastChangedById = 1 ;
                obj.IsDeleted = false ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

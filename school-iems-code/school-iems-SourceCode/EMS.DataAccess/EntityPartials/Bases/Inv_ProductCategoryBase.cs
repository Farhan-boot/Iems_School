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
    public partial class Inv_ProductCategory
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_Name = "Name";		            
		public const string Property_SubTitle = "SubTitle";		            
		public const string Property_ParentId = "ParentId";		            
		public const string Property_Description = "Description";		            
		public const string Property_AssetTypeEnumId = "AssetTypeEnumId";		            
        public const string Property_AssetType = "AssetType";
		public const string Property_IsBarcoded = "IsBarcoded";		            
		public const string Property_ProductCode = "ProductCode";		            
		public const string Property_DefaultStoreId = "DefaultStoreId";		            
		public const string Property_UnitTypeEnumId = "UnitTypeEnumId";		            
        public const string Property_UnitType = "UnitType";
		public const string Property_WarningQuantity = "WarningQuantity";		            
		public const string Property_Remark = "Remark";		            
		public const string Property_ParentCategoryId = "ParentCategoryId";		            
		public const string Property_DepriciationRate = "DepriciationRate";		            
		public const string Property_History = "History";		            
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_CreateById = "CreateById";		            
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
      
     
     
      
        public static Inv_ProductCategory GetNew(Int32 id=0)
        {
           Inv_ProductCategory obj = new Inv_ProductCategory();
               obj.Id = id;
                obj.Name = string.Empty ; 
                obj.SubTitle = null ; 
                obj.ParentId = null ; 
                obj.Description = string.Empty ; 
                obj.AssetTypeEnumId = null ; 
                obj.IsBarcoded = null ; 
                obj.ProductCode = null ; 
                obj.DefaultStoreId = null ; 
                obj.UnitTypeEnumId = null ; 
                obj.WarningQuantity = null ; 
                obj.Remark = null ; 
                obj.ParentCategoryId = null ; 
                obj.DepriciationRate = null ; 
                obj.History = null ; 
                obj.CreateDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreateById = 1 ;
                obj.LastChanged = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LastChangedById = 1 ;
                obj.IsDeleted = false ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

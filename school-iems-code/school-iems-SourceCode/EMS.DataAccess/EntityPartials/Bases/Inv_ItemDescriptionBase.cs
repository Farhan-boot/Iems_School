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
    public partial class Inv_ItemDescription
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_ItemId = "ItemId";		            
		public const string Property_CompanyItemBarcode = "CompanyItemBarcode";		            
		public const string Property_WarrentyDate = "WarrentyDate";		            
		public const string Property_Description = "Description";		            
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
      
     
     
      
        public static Inv_ItemDescription GetNew(Int32 id=0)
        {
           Inv_ItemDescription obj = new Inv_ItemDescription();
               obj.Id = id;
                obj.ItemId = null ; 
                obj.CompanyItemBarcode = null ; 
                obj.WarrentyDate = null ; 
                obj.Description = null ; 
                obj.LastChanged = null ; 
                obj.LastChangedById = 1 ;
                obj.IsDeleted = null ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

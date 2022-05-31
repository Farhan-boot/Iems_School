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
    public partial class Invt_RequestedItemAssign
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_ItemId = "ItemId";		            
		public const string Property_UnitTypeEnumId = "UnitTypeEnumId";		            
        public const string Property_UnitType = "UnitType";
		public const string Property_Unit = "Unit";		            
		public const string Property_AssginPersonName = "AssginPersonName";		            
		public const string Property_ReturnDate = "ReturnDate";		            
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_CreateById = "CreateById";		            
		public const string Property_LastChanged = "LastChanged";		            
		public const string Property_LastChangedById = "LastChangedById";		            
		public const string Property_IsDeleted = "IsDeleted";		            
		public const string Property_IsReturn = "IsReturn";		            
              
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
      
     
     
      
        public static Invt_RequestedItemAssign GetNew(Int32 id=0)
        {
           Invt_RequestedItemAssign obj = new Invt_RequestedItemAssign();
               obj.Id = id;
                obj.ItemId = 0 ; 
                obj.UnitTypeEnumId = 0 ; 
                obj.Unit = 0.0f ; 
                obj.AssginPersonName = string.Empty ; 
                obj.ReturnDate = null ; 
                obj.CreateDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreateById = 1 ;
                obj.LastChanged = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LastChangedById = 1 ;
                obj.IsDeleted = false ; 
                obj.IsReturn = false ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

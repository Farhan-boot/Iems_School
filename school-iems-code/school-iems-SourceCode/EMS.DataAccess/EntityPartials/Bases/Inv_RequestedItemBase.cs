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
    public partial class Inv_RequestedItem
    {
    	#region Constants
		public const string Property_Id = "Id";
        public const string Property_RequisitionId = "RequisitionId";
        public const string Property_CatagoryId = "CatagoryId";
        public const string Property_Quantity = "Quantity";
        public const string Property_ItemStatus = "ItemStatus";
        public const string Property_Remark = "Remark";
        public const string Property_ItemCategory = "ItemCategory";
       

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
      
     
     
      
        public static Inv_RequestedItem GetNew(Int32 id=0)
        {
            Inv_RequestedItem obj = new Inv_RequestedItem();
               obj.Id = id;
                obj.RequisitionId = 0 ; 
                obj.CatagoryId = 0 ; 
                obj.Quantity = 0 ; 
                obj.ItemStatus = 0 ; 
                obj.Remark = string.Empty ;
                obj.ItemCategory = string.Empty;
                
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

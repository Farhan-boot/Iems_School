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
    public partial class Inv_ItemDelivery
    {
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_ItemAllocationId = "ItemAllocationId";
        public const string Property_DeliveredQuantity = "DeliveredQuantity";
        public const string Property_DeliveredDate = "DeliveredDate";
        public const string Property_DeliveredTo = "DeliveredTo";
        public const string Property_Note = "Note";
        public const string Property_ItemStockId = "ItemStockId";
        public const string Property_ReceivedByBPId = "ReceivedByBPId";
        public const string Property_ReceivedByName = "ReceivedByName";
        public const string Property_ReceivedByMobile = "ReceivedByMobile";
        public const string Property_ReceivedByRankId = "ReceivedByRankId";
        public const string Property_ReceivedByDepartmentId = "ReceivedByDepartmentId";
        public const string Property_ReceivedByAreaId = "ReceivedByAreaId";

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
        
        public static Inv_ItemDelivery GetNew(Int32 id=0)
        {
            Inv_ItemDelivery obj = new Inv_ItemDelivery();
               obj.Id = id;
                obj.ItemAllocationId = 0 ; 
                obj.DeliveredQuantity = 0 ; 
                obj.DeliveredDate = new DateTime(1753, 1, 1, 12, 0, 0, 0); 
                obj.DeliveredTo = 0 ; 
                obj.Note = null ; 
                obj.ItemStockId = 0 ; 
                obj.ReceivedByBPId = 0 ; 
                obj.ReceivedByName = null ; 
                obj.ReceivedByMobile = null ; 
                obj.ReceivedByRankId = 0 ; 
                obj.ReceivedByDepartmentId = 0 ;
                obj.ReceivedByAreaId = 0;
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

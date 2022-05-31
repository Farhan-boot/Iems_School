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
    public partial class Inv_Inventory
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_StoreId = "StoreId";		            
		public const string Property_VoucherNo = "VoucherNo";		            
		public const string Property_VoucherDate = "VoucherDate";		            
		public const string Property_InvoiceNo = "InvoiceNo";		            
		public const string Property_InvoiceDate = "InvoiceDate";		            
		public const string Property_PurchaseOrderNo = "PurchaseOrderNo";		            
		public const string Property_PurchaseOrderDate = "PurchaseOrderDate";		            
		public const string Property_ChalanNo = "ChalanNo";		            
		public const string Property_ChalanDate = "ChalanDate";		            
		public const string Property_ReceivedDate = "ReceivedDate";		            
		public const string Property_ReceivedBy = "ReceivedBy";		            
		public const string Property_SupplierId = "SupplierId";		            
		public const string Property_Remark = "Remark";		            
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_CreateById = "CreateById";		            
		public const string Property_LastChanged = "LastChanged";		            
		public const string Property_LastChangedById = "LastChangedById";		            
		public const string Property_IsDeleted = "IsDeleted";		            
		public const string Property_History = "History";		            
              
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
      
     
     
      
        public static Inv_Inventory GetNew(Int32 id=0)
        {
           Inv_Inventory obj = new Inv_Inventory();
               obj.Id = id;
                obj.StoreId = 0 ; 
                obj.VoucherNo = null ; 
                obj.VoucherDate = null ; 
                obj.InvoiceNo = null ; 
                obj.InvoiceDate = null ; 
                obj.PurchaseOrderNo = null ; 
                obj.PurchaseOrderDate = null ; 
                obj.ChalanNo = null ; 
                obj.ChalanDate = null ; 
                obj.ReceivedDate = null ; 
                obj.ReceivedBy = null ; 
                obj.SupplierId = null ; 
                obj.Remark = null ; 
                obj.CreateDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreateById = 1 ;
                obj.LastChanged = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LastChangedById = 1 ;
                obj.IsDeleted = false ; 
                obj.History = null ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

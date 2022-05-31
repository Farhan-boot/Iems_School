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
    public partial class Inv_ItemAllocation
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_ItemId = "ItemId";		            
		public const string Property_FromStore = "FromStore";		            
		public const string Property_AllocatedTo = "AllocatedTo";		            
		public const string Property_AllocationDate = "AllocationDate";		            
		public const string Property_AllocationStatus = "AllocationStatus";		            
		public const string Property_ReferenceBy = "ReferenceBy";		            
		public const string Property_Remarks = "Remarks";		            
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_CreateById = "CreateById";		            
		public const string Property_LastChanged = "LastChanged";		            
		public const string Property_LastChangedById = "LastChangedById";		            
		public const string Property_IsDeleted = "IsDeleted";
        //New Proparty
        public const string Property_RequestedItemId = "RequestedItemId";
        public const string Property_ApprovedQuantity = "ApprovedQuantity";
        public const string Property_ApprovedById = "ApprovedById";

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
      
     
     
      
        public static Inv_ItemAllocation GetNew(Int32 id=0)
        {
           Inv_ItemAllocation obj = new Inv_ItemAllocation();
               obj.Id = id;
                obj.ItemId = 0 ; 
                obj.FromStore = null ; 
                obj.AllocatedTo = null ; 
                obj.AllocationDate = null ; 
                obj.AllocationStatus = null ; 
                obj.ReferenceBy = null ; 
                obj.Remarks = null ; 
                obj.CreateDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreateById = 1 ;
                obj.LastChanged = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LastChangedById = 1 ;
                obj.IsDeleted = false ; 
                obj.IsAlreadyAdded = false;
            //New Object
            obj.RequestedItemId = 0;
            obj.ApprovedQuantity = 0;
            obj.ApprovedById = 0;

            GetNewExtra(obj);
                return obj;
        }
       
	}
}

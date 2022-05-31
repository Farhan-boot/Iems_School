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
    public partial class Inv_PurchaseRequisition
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_RequisitionDate = "RequisitionDate";		            
		public const string Property_RequisitionByUserId = "RequisitionByUserId";		            
		public const string Property_Status = "Status";		            
		public const string Property_Purpose = "Purpose";		            
		public const string Property_Remarks = "Remarks";		            
		public const string Property_ApprovedByUserId = "ApprovedByUserId";		            
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
      
     
     
      
        public static Inv_PurchaseRequisition GetNew(Int32 id=0)
        {
           Inv_PurchaseRequisition obj = new Inv_PurchaseRequisition();
               obj.Id = id;
                obj.RequisitionDate = null ; 
                obj.RequisitionByUserId = null ; 
                obj.Status = null ; 
                obj.Purpose = null ; 
                obj.Remarks = null ; 
                obj.ApprovedByUserId = null ; 
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

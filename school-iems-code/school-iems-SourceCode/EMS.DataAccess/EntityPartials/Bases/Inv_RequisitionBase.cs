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
    public partial class Inv_Requisition
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_RequestedByEmployeeId = "RequestedByEmployeeId";		            
		public const string Property_RequisitionDate = "RequisitionDate";		            
		public const string Property_RequireDate = "RequireDate";		            
		public const string Property_Purpose = "Purpose";		            
		public const string Property_Remark = "Remark";		            
		public const string Property_ReferencedByEmployeeId = "ReferencedByEmployeeId";		            
		public const string Property_IssuedOrReleaseByUserId = "IssuedOrReleaseByUserId";		            
		public const string Property_ApprovedByAdminId = "ApprovedByAdminId";		            
		public const string Property_ReceivedByEmployeeId = "ReceivedByEmployeeId";		            
		public const string Property_Status = "Status";		            
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_CreateById = "CreateById";		            
		public const string Property_LastChanged = "LastChanged";		            
		public const string Property_LastChangedById = "LastChangedById";		            
		public const string Property_IsDeleted = "IsDeleted";
        //New Proparty
        public const string Property_RequisitionStatusId = "RequisitionStatusId";
        public const string Property_ApprovedById = "ApprovedById";
        public const string Property_DeliveredById = "DeliveredById";
        public const string Property_ItemCategoryId = "ItemCategoryId";
        public const string Property_Quantity = "Quantity";

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
      
     
     
      
        public static Inv_Requisition GetNew(Int32 id=0)
        {
           Inv_Requisition obj = new Inv_Requisition();
               obj.Id = id;
                obj.RequestedByEmployeeId = null ; 
                obj.RequisitionDate = null ; 
                obj.RequireDate = null ; 
                obj.Purpose = null ; 
                obj.Remark = null ; 
                obj.ReferencedByEmployeeId = null ; 
                obj.IssuedOrReleaseByUserId = null ; 
                obj.ApprovedByAdminId = null ; 
                obj.ReceivedByEmployeeId = null ; 
                obj.Status = null ; 
                obj.CreateDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreateById = 1 ;
                obj.LastChanged = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LastChangedById = 1 ;
                obj.IsDeleted = false ; 
                obj.IsAlreadyAdded = false;
                //new obj
                obj.RequisitionStatusId = 0;
                obj.ApprovedById = 0;
                obj.DeliveredById = 0;
                obj.ItemCategoryId = 0;
                obj.Quantity = 0;

            GetNewExtra(obj);
                return obj;
        }
       
	}
}

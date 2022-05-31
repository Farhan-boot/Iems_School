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
    public partial class Inv_RequisitionDetails
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_RequisitionId = "RequisitionId";		            
		public const string Property_ItemName = "ItemName";		            
		public const string Property_ItemId = "ItemId";		            
		public const string Property_Detail = "Detail";		            
		public const string Property_Quantity = "Quantity";		            
		public const string Property_ApprovedQuantity = "ApprovedQuantity";		            
		public const string Property_LastChanged = "LastChanged";		            
		public const string Property_LastChangedById = "LastChangedById";		            
		public const string Property_IsDeleted = "IsDeleted";
        //New Proparty
        public const string Property_RequisitionNo = "RequisitionNo";
        public const string Property_RequisitionByBPId = "RequisitionByBPId";
        public const string Property_RequisitionByName = "RequisitionByName";
        public const string Property_RequisitionByRank = "RequisitionByRank";
        public const string Property_RequestedByDepartment = "RequestedByDepartment";
        public const string Property_RequestedByDepartmentArea = "RequestedByDepartmentArea";
        public const string Property_RequisitionDate = "RequisitionDate";
        public const string Property_RequierDate = "RequierDate";
        public const string Property_ApprovedByBPId = "ApprovedByBPId";
        public const string Property_ApprovedByRank = "ApprovedByRank";
        public const string Property_ApprovedByDepartment = "ApprovedByDepartment";
        public const string Property_StatusEnumId = "StatusEnumId";
        public const string Property_Purpose = "Purpose";

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
      
     
     
      
        public static Inv_RequisitionDetails GetNew(Int32 id=0)
        {
           Inv_RequisitionDetails obj = new Inv_RequisitionDetails();
               obj.Id = id;
                obj.RequisitionId = null ; 
                obj.ItemName = null ; 
                obj.ItemId = null ; 
                obj.Detail = null ; 
                obj.Quantity = null ; 
                obj.ApprovedQuantity = null ; 
                obj.LastChanged = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LastChangedById = 1 ;
                obj.IsDeleted = false ; 
                obj.IsAlreadyAdded = false;
            //New Object
            obj.RequisitionNo = null;
            obj.RequisitionByBPId = 0;
            obj.RequisitionByName = null;
            obj.RequisitionByRank = null;
            obj.RequestedByDepartment = null;
            obj.RequestedByDepartmentArea = null;
            obj.RequisitionDate = null;
            obj.RequierDate = null;
            obj.ApprovedByBPId = 0;
            obj.ApprovedByRank = null;
            obj.ApprovedByDepartment = null;
            obj.StatusEnumId = null;
            obj.Purpose = null;

            GetNewExtra(obj);
                return obj;
        }
       
	}
}

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
    public partial class General_Room
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_RoomNo = "RoomNo";		            
		public const string Property_Name = "Name";		            
		public const string Property_OrderNo = "OrderNo";		            
		public const string Property_FloorId = "FloorId";		            
		public const string Property_CapacityRegular = "CapacityRegular";		            
		public const string Property_CapacityAdmission = "CapacityAdmission";		            
		public const string Property_TypeEnumId = "TypeEnumId";		            
        public const string Property_Type = "Type";
		public const string Property_StatusEnumId = "StatusEnumId";		            
        public const string Property_Status = "Status";
		public const string Property_BuildingId = "BuildingId";		            
		public const string Property_Length = "Length";		            
		public const string Property_Width = "Width";		            
		public const string Property_DepartmentId = "DepartmentId";		            
		public const string Property_IsShareable = "IsShareable";		            
		public const string Property_Remarks = "Remarks";		            
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
      
     
     
      
        public static General_Room GetNew(Int64 id=0)
        {
           General_Room obj = new General_Room();
               obj.Id = id;
                obj.RoomNo = string.Empty ; 
                obj.Name = string.Empty ; 
                obj.OrderNo = 0 ; 
                obj.FloorId = 0 ; 
                obj.CapacityRegular = 0 ; 
                obj.CapacityAdmission = 0 ; 
                obj.TypeEnumId = 0 ; 
                obj.StatusEnumId = 0 ; 
                obj.BuildingId = 0 ; 
                obj.Length = 0.0F ; 
                obj.Width = 0.0F ; 
                obj.DepartmentId = 0 ; 
                obj.IsShareable = false ; 
                obj.Remarks = null ; 
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

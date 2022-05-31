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
    public partial class Htl_HostalBuilding
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_Name = "Name";		            
		public const string Property_BuildingTypeEnumId = "BuildingTypeEnumId";		            
        public const string Property_BuildingType = "BuildingType";
		public const string Property_HouseNo = "HouseNo";		            
		public const string Property_RoadNo = "RoadNo";		            
		public const string Property_Address = "Address";		            
		public const string Property_TotalFloars = "TotalFloars";		            
		public const string Property_TotalRooms = "TotalRooms";		            
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
      
     
     
      
        public static Htl_HostalBuilding GetNew(Int32 id=0)
        {
           Htl_HostalBuilding obj = new Htl_HostalBuilding();
               obj.Id = id;
                obj.Name = string.Empty ; 
                obj.BuildingTypeEnumId = 0 ; 
                obj.HouseNo = null ; 
                obj.RoadNo = null ; 
                obj.Address = null ; 
                obj.TotalFloars = null ; 
                obj.TotalRooms = null ; 
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

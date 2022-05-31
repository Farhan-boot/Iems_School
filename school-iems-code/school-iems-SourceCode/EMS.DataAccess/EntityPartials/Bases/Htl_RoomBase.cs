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
    public partial class Htl_Room
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_HostelBuildingId = "HostelBuildingId";		            
		public const string Property_FloorId = "FloorId";		            
		public const string Property_RoomNumber = "RoomNumber";		            
		public const string Property_Seat = "Seat";		            
		public const string Property_Bathroom = "Bathroom";		            
		public const string Property_Veranda = "Veranda";		            
		public const string Property_HasAC = "HasAC";		            
		public const string Property_Occupied = "Occupied";		            
		public const string Property_Vaccancy = "Vaccancy";		            
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
      
     
     
      
        public static Htl_Room GetNew(Int32 id=0)
        {
           Htl_Room obj = new Htl_Room();
               obj.Id = id;
                obj.HostelBuildingId = 0 ; 
                obj.FloorId = 0 ; 
                obj.RoomNumber = string.Empty ; 
                obj.Seat = 0 ; 
                obj.Bathroom = 0 ; 
                obj.Veranda = 0 ; 
                obj.HasAC = false ; 
                obj.Occupied = 0 ; 
                obj.Vaccancy = 0 ; 
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

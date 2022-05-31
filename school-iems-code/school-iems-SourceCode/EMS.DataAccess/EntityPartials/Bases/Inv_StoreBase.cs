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
    public partial class Inv_Store
	{
    	#region Constants
		public const string Property_Id = "Id";
        public const string Property_Name = "Name";
        public const string Property_RoomId = "RoomId";		            
		public const string Property_CampusId = "CampusId";		            
		public const string Property_Phone = "Phone";		            
		public const string Property_Details = "Details";
        public const string Property_Address = "Address";
        public const string Property_Description = "Description";
        public const string Property_Remark = "Remark";

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
      
     
     
      
        public static Inv_Store GetNew(Int32 id=0)
        {
           Inv_Store obj = new Inv_Store();
                obj.Id = id;
                obj.Name = string.Empty ; 
                obj.Phone = null ; 
                obj.RoomId = 0 ; 
                obj.CampusId = 0 ; 
                obj.Phone = string.Empty ;
                obj.Details = string.Empty;
                obj.Address = string.Empty;
                obj.Description = string.Empty;
                obj.Remark = string.Empty;

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

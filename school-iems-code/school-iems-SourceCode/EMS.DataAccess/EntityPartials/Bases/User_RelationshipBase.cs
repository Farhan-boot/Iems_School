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
    public partial class User_Relationship
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_Name = "Name";		            
		public const string Property_GenderEnumId = "GenderEnumId";		            
        public const string Property_Gender = "Gender";
		public const string Property_OrderNo = "OrderNo";		            
		public const string Property_IsActive = "IsActive";		            
		public const string Property_IsDeleted = "IsDeleted";		            
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_CreateById = "CreateById";		            
		public const string Property_LastChanged = "LastChanged";		            
		public const string Property_LastChangedById = "LastChangedById";		            
              
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
      
     
     
      
        public static User_Relationship GetNew(Int32 id=0)
        {
           User_Relationship obj = new User_Relationship();
               obj.Id = id;
                obj.Name = string.Empty ; 
                obj.GenderEnumId = 0 ; 
                obj.OrderNo = 0 ; 
                obj.IsActive = false ; 
                obj.IsDeleted = false ; 
                obj.CreateDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreateById = 1 ;
                obj.LastChanged = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LastChangedById = 1 ;
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

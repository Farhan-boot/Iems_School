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
    public partial class Inv_ItemReturnAllocationMap
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_ItemAllocationId = "ItemAllocationId";		            
		public const string Property_ItemReturnId = "ItemReturnId";		            
              
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
      
     
     
      
        public static Inv_ItemReturnAllocationMap GetNew(Int32 id=0)
        {
           Inv_ItemReturnAllocationMap obj = new Inv_ItemReturnAllocationMap();
               obj.Id = id;
                obj.ItemAllocationId = null ; 
                obj.ItemReturnId = null ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

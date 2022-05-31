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
    public partial class Lib_Supplier
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_SupplierName = "SupplierName";		            
		public const string Property_FullAddress = "FullAddress";		            
		public const string Property_Phone = "Phone";		            
		public const string Property_Fax = "Fax";		            
		public const string Property_Email = "Email";		            
		public const string Property_URL = "URL";		            
		public const string Property_ContactPersonName = "ContactPersonName";		            
		public const string Property_ContactPersonPhone = "ContactPersonPhone";		            
		public const string Property_SupplierTypeEnumId = "SupplierTypeEnumId";		            
        public const string Property_SupplierType = "SupplierType";
		public const string Property_SupplierStatusEnumId = "SupplierStatusEnumId";		            
        public const string Property_SupplierStatus = "SupplierStatus";
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
      
     
     
      
        public static Lib_Supplier GetNew(Int64 id=0)
        {
           Lib_Supplier obj = new Lib_Supplier();
               obj.Id = id;
                obj.SupplierName = string.Empty ; 
                obj.FullAddress = string.Empty ; 
                obj.Phone = null ; 
                obj.Fax = null ; 
                obj.Email = null ; 
                obj.URL = null ; 
                obj.ContactPersonName = null ; 
                obj.ContactPersonPhone = null ; 
                obj.SupplierTypeEnumId = 0 ; 
                obj.SupplierStatusEnumId = 0 ; 
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

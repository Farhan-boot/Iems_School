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
    public partial class Inv_Supplier
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_CompanyName = "CompanyName";		            
		public const string Property_ContactName = "ContactName";		            
		public const string Property_ContactDesignation = "ContactDesignation";		            
		public const string Property_Address = "Address";		            
		public const string Property_Phone = "Phone";		            
		public const string Property_Fax = "Fax";		            
		public const string Property_Email = "Email";		            
		public const string Property_WebsiteURL = "WebsiteURL";		            
		public const string Property_ContactPersonName = "ContactPersonName";		            
		public const string Property_ContactPersonPhone = "ContactPersonPhone";		            
		public const string Property_ContactPersonEmail = "ContactPersonEmail";		            
		public const string Property_Description = "Description";		            
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
      
     
     
      
        public static Inv_Supplier GetNew(Int32 id=0)
        {
           Inv_Supplier obj = new Inv_Supplier();
               obj.Id = id;
                obj.CompanyName = null ; 
                obj.ContactName = null ; 
                obj.ContactDesignation = null ; 
                obj.Address = string.Empty ; 
                obj.Phone = string.Empty ; 
                obj.Fax = null ; 
                obj.Email = null ; 
                obj.WebsiteURL = null ; 
                obj.ContactPersonName = null ; 
                obj.ContactPersonPhone = null ; 
                obj.ContactPersonEmail = null ; 
                obj.Description = null ; 
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

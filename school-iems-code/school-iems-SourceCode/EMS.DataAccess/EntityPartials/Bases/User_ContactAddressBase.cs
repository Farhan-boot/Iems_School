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
    public partial class User_ContactAddress
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_UserId = "UserId";		            
		public const string Property_Address1 = "Address1";		            
		public const string Property_Address2 = "Address2";		            
		public const string Property_PostOffice = "PostOffice";		            
		public const string Property_PoliceStation = "PoliceStation";		            
		public const string Property_District = "District";		            
		public const string Property_CountryId = "CountryId";		            
		public const string Property_IsValid = "IsValid";		            
		public const string Property_ContactAddressTypeEnumId = "ContactAddressTypeEnumId";		            
        public const string Property_ContactAddressType = "ContactAddressType";
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
      
     
     
      
        public static User_ContactAddress GetNew(Int32 id=0)
        {
           User_ContactAddress obj = new User_ContactAddress();
               obj.Id = id;
                obj.UserId = 0 ; 
                obj.Address1 = null ; 
                obj.Address2 = null ; 
                obj.PostOffice = null ; 
                obj.PoliceStation = null ; 
                obj.District = null ; 
                obj.CountryId = null ; 
                obj.IsValid = false ; 
                obj.ContactAddressTypeEnumId = 0 ; 
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

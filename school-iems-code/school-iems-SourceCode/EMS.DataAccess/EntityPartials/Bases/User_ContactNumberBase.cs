﻿//File: Entity Partial Base
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;

namespace EMS.DataAccess.Data
{

    /// <summary>
    /// Do not modify this class, edit partial instead.
    /// </summary>   
    public partial class User_ContactNumber
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_UserId = "UserId";		            
		public const string Property_ContactNumber = "ContactNumber";		            
		public const string Property_ContactNumberTypeEnumId = "ContactNumberTypeEnumId";		            
        public const string Property_ContactNumberType = "ContactNumberType";
		public const string Property_CountryId = "CountryId";		            
		public const string Property_IsValid = "IsValid";		            
		public const string Property_PrivacyEnumId = "PrivacyEnumId";		            
        public const string Property_Privacy = "Privacy";
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
      
     
     
      
        public static User_ContactNumber GetNew(Int64 id=0)
        {
           User_ContactNumber obj = new User_ContactNumber();
               obj.Id = id;
                obj.UserId = 0 ; 
                obj.ContactNumber = string.Empty ; 
                obj.ContactNumberTypeEnumId = 0 ; 
                obj.CountryId = 0 ; 
                obj.IsValid = false ; 
                obj.PrivacyEnumId = 0 ; 
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

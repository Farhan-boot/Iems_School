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
    public partial class UCS_SmsProvider
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_Name = "Name";		            
		public const string Property_Description = "Description";		            
		public const string Property_UserName = "UserName";		            
		public const string Property_Password = "Password";		            
		public const string Property_SenderId = "SenderId";		            
		public const string Property_ServiceUrl = "ServiceUrl";		            
		public const string Property_ApiKey = "ApiKey";		            
              
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
      
     
     
      
        public static UCS_SmsProvider GetNew(Int64 id=0)
        {
           UCS_SmsProvider obj = new UCS_SmsProvider();
               obj.Id = id;
                obj.Name = string.Empty ; 
                obj.Description = null ; 
                obj.UserName = string.Empty ; 
                obj.Password = string.Empty ; 
                obj.SenderId = string.Empty ; 
                obj.ServiceUrl = string.Empty ; 
                obj.ApiKey = null ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

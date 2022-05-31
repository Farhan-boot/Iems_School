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
    public partial class UCS_SmtpSetting
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_Name = "Name";		            
		public const string Property_Description = "Description";		            
		public const string Property_DomainName = "DomainName";		            
		public const string Property_EmailAddress = "EmailAddress";		            
		public const string Property_Host = "Host";		            
		public const string Property_Port = "Port";		            
		public const string Property_UserName = "UserName";		            
		public const string Property_Password = "Password";		            
		public const string Property_EnableSsl = "EnableSsl";		            
              
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
      
     
     
      
        public static UCS_SmtpSetting GetNew(Int64 id=0)
        {
           UCS_SmtpSetting obj = new UCS_SmtpSetting();
               obj.Id = id;
                obj.Name = string.Empty ; 
                obj.Description = null ; 
                obj.DomainName = string.Empty ; 
                obj.EmailAddress = string.Empty ; 
                obj.Host = string.Empty ; 
                obj.Port = 0 ; 
                obj.UserName = string.Empty ; 
                obj.Password = string.Empty ; 
                obj.EnableSsl = false ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

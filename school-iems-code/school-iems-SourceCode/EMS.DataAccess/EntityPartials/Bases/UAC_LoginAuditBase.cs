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
    public partial class UAC_LoginAudit
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_UserId = "UserId";		            
		public const string Property_UserName = "UserName";		            
		public const string Property_Password = "Password";		            
		public const string Property_AuditDate = "AuditDate";		            
		public const string Property_FromIp = "FromIp";		            
		public const string Property_MacAddress = "MacAddress";		            
		public const string Property_StatusEnumId = "StatusEnumId";		            
        public const string Property_Status = "Status";
		public const string Property_IsMobileBrowser = "IsMobileBrowser";		            
		public const string Property_Browser = "Browser";		            
              
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
      
     
     
      
        public static UAC_LoginAudit GetNew(Int64 id=0)
        {
           UAC_LoginAudit obj = new UAC_LoginAudit();
               obj.Id = id;
                obj.UserId = 0 ; 
                obj.UserName = string.Empty ; 
                obj.Password = string.Empty ; 
                obj.AuditDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.FromIp = string.Empty ; 
                obj.MacAddress = string.Empty ; 
                obj.StatusEnumId = 0 ; 
                obj.IsMobileBrowser = false ; 
                obj.Browser = string.Empty ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

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
    public partial class UAC_PassReset
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_UserId = "UserId";		            
		public const string Property_RequestDate = "RequestDate";		            
		public const string Property_RequestById = "RequestById";		            
		public const string Property_Code = "Code";		            
		public const string Property_ValidHours = "ValidHours";		            
		public const string Property_TypeEnumId = "TypeEnumId";		            
        public const string Property_Type = "Type";
		public const string Property_StatusEnumId = "StatusEnumId";		            
        public const string Property_Status = "Status";
		public const string Property_ResetDate = "ResetDate";		            
		public const string Property_ResetById = "ResetById";		            
		public const string Property_FromIp = "FromIp";		            
              
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
      
     
     
      
        public static UAC_PassReset GetNew(Int64 id=0)
        {
           UAC_PassReset obj = new UAC_PassReset();
               obj.Id = id;
                obj.UserId = 0 ; 
                obj.RequestDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.RequestById = 0 ; 
                obj.Code = 0 ; 
                obj.ValidHours = 0 ; 
                obj.TypeEnumId = 0 ; 
                obj.StatusEnumId = 0 ; 
                obj.ResetDate = null ; 
                obj.ResetById = null ; 
                obj.FromIp = string.Empty ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

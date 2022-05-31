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
    public partial class UAC_VerificationAudit
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_UserId = "UserId";		            
		public const string Property_InitiatedDate = "InitiatedDate";		            
		public const string Property_InitiatedById = "InitiatedById";		            
		public const string Property_Code = "Code";		            
		public const string Property_ValidHours = "ValidHours";		            
		public const string Property_NewDeviceName = "NewDeviceName";		            
		public const string Property_RequestByEnumId = "RequestByEnumId";		            
        public const string Property_RequestBy = "RequestBy";
		public const string Property_RequestTypeEnumId = "RequestTypeEnumId";		            
        public const string Property_RequestType = "RequestType";
		public const string Property_StatusEnumId = "StatusEnumId";		            
        public const string Property_Status = "Status";
		public const string Property_ConfirmedDate = "ConfirmedDate";		            
		public const string Property_ConfirmedById = "ConfirmedById";		            
		public const string Property_FromIp = "FromIp";		            
		public const string Property_Remark = "Remark";		            
              
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
      
     
     
      
        public static UAC_VerificationAudit GetNew(Int32 id=0)
        {
           UAC_VerificationAudit obj = new UAC_VerificationAudit();
               obj.Id = id;
                obj.UserId = 0 ; 
                obj.InitiatedDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.InitiatedById = 0 ; 
                obj.Code = 0 ; 
                obj.ValidHours = 0 ; 
                obj.NewDeviceName = string.Empty ; 
                obj.RequestByEnumId = 0 ; 
                obj.RequestTypeEnumId = 0 ; 
                obj.StatusEnumId = 0 ; 
                obj.ConfirmedDate = null ; 
                obj.ConfirmedById = null ; 
                obj.FromIp = string.Empty ; 
                obj.Remark = string.Empty ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

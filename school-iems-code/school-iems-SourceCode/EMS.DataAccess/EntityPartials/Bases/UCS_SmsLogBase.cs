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
    public partial class UCS_SmsLog
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_ProviderId = "ProviderId";		            
		public const string Property_UserId = "UserId";		            
		public const string Property_CallerId = "CallerId";		            
		public const string Property_TextMessage = "TextMessage";		            
		public const string Property_RequestDate = "RequestDate";		            
		public const string Property_SendById = "SendById";		            
		public const string Property_SendDate = "SendDate";		            
		public const string Property_ModuleEnumId = "ModuleEnumId";		            
        public const string Property_Module = "Module";
		public const string Property_MessageId = "MessageId";		            
		public const string Property_StatusEnumId = "StatusEnumId";		            
        public const string Property_Status = "Status";
		public const string Property_StatusText = "StatusText";		            
		public const string Property_ErrorCode = "ErrorCode";		            
		public const string Property_ErrorText = "ErrorText";		            
		public const string Property_SMSCount = "SMSCount";		            
		public const string Property_CurrentCredit = "CurrentCredit";		            
              
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
      
     
     
      
        public static UCS_SmsLog GetNew(Int64 id=0)
        {
           UCS_SmsLog obj = new UCS_SmsLog();
               obj.Id = id;
                obj.ProviderId = null ; 
                obj.UserId = 0 ; 
                obj.CallerId = string.Empty ; 
                obj.TextMessage = string.Empty ; 
                obj.RequestDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.SendById = 0 ; 
                obj.SendDate = null ; 
                obj.ModuleEnumId = 0 ; 
                obj.MessageId = null ; 
                obj.StatusEnumId = null ; 
                obj.StatusText = null ; 
                obj.ErrorCode = null ; 
                obj.ErrorText = null ; 
                obj.SMSCount = null ; 
                obj.CurrentCredit = null ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

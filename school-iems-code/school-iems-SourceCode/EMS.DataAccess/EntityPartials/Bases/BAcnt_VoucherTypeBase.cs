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
    public partial class BAcnt_VoucherType
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_Name = "Name";		            
		public const string Property_TypeEnumId = "TypeEnumId";		            
        public const string Property_Type = "Type";
		public const string Property_StatusEnumId = "StatusEnumId";		            
        public const string Property_Status = "Status";
		public const string Property_DefaultDebitLedgerId = "DefaultDebitLedgerId";		            
		public const string Property_DefaultCreditLedgerId = "DefaultCreditLedgerId";		            
		public const string Property_BranchId = "BranchId";		            
		public const string Property_CompanyId = "CompanyId";		            
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
      
     
     
      
        public static BAcnt_VoucherType GetNew(Int32 id=0)
        {
           BAcnt_VoucherType obj = new BAcnt_VoucherType();
               obj.Id = id;
                obj.Name = string.Empty ; 
                obj.TypeEnumId = 0 ; 
                obj.StatusEnumId = 0 ; 
                obj.DefaultDebitLedgerId = null ; 
                obj.DefaultCreditLedgerId = null ; 
                obj.BranchId = 0 ; 
                obj.CompanyId = 0 ; 
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

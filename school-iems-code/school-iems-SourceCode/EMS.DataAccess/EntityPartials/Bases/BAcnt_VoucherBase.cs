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
    public partial class BAcnt_Voucher
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_Date = "Date";		            
		public const string Property_VoucherNo = "VoucherNo";		            
		public const string Property_VoucherTypeId = "VoucherTypeId";		            
		public const string Property_VoucherStatusEnumId = "VoucherStatusEnumId";		            
        public const string Property_VoucherStatus = "VoucherStatus";
		public const string Property_Remark = "Remark";		            
		public const string Property_History = "History";		            
		public const string Property_IsDeleted = "IsDeleted";		            
		public const string Property_Narration = "Narration";		            
		public const string Property_JournalTypeEnumId = "JournalTypeEnumId";		            
        public const string Property_JournalType = "JournalType";
		public const string Property_BankId = "BankId";		            
		public const string Property_ManualSlipId = "ManualSlipId";		            
		public const string Property_ChequeNo = "ChequeNo";		            
		public const string Property_ChequeDate = "ChequeDate";		            
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
      
     
     
      
        public static BAcnt_Voucher GetNew(Int32 id=0)
        {
           BAcnt_Voucher obj = new BAcnt_Voucher();
               obj.Id = id;
                obj.Date = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.VoucherNo = string.Empty ; 
                obj.VoucherTypeId = 0 ; 
                obj.VoucherStatusEnumId = 0 ; 
                obj.Remark = string.Empty ; 
                obj.History = string.Empty ; 
                obj.IsDeleted = false ; 
                obj.Narration = string.Empty ; 
                obj.JournalTypeEnumId = 0 ; 
                obj.BankId = null ; 
                obj.ManualSlipId = string.Empty ; 
                obj.ChequeNo = string.Empty ; 
                obj.ChequeDate = string.Empty ; 
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

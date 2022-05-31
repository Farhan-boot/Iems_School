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
    public partial class BAcnt_VoucherDetail
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_LedgerId = "LedgerId";		            
		public const string Property_DebitAmount = "DebitAmount";		            
		public const string Property_CreditAmount = "CreditAmount";		            
		public const string Property_IsDebited = "IsDebited";		            
		public const string Property_VoucherId = "VoucherId";		            
		public const string Property_Remark = "Remark";		            
		public const string Property_History = "History";		            
		public const string Property_IsDeleted = "IsDeleted";		            
		public const string Property_Narration = "Narration";		            
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
      
     
     
      
        public static BAcnt_VoucherDetail GetNew(Int32 id=0)
        {
           BAcnt_VoucherDetail obj = new BAcnt_VoucherDetail();
               obj.Id = id;
                obj.LedgerId = 0 ; 
                obj.DebitAmount = 0.0f ; 
                obj.CreditAmount = 0.0f ; 
                obj.IsDebited = false ; 
                obj.VoucherId = 0 ; 
                obj.Remark = string.Empty ; 
                obj.History = string.Empty ; 
                obj.IsDeleted = false ; 
                obj.Narration = string.Empty ; 
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

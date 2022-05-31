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
    public partial class BAcnt_Ledger
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_Name = "Name";		            
		public const string Property_Code = "Code";		            
		public const string Property_CodeName = "CodeName";		            
		public const string Property_TypeEnumId = "TypeEnumId";		            
        public const string Property_Type = "Type";
		public const string Property_ParentId = "ParentId";		            
		public const string Property_IsGroup = "IsGroup";		            
		public const string Property_OpeningBalance = "OpeningBalance";		            
		public const string Property_OpeningDate = "OpeningDate";		            
		public const string Property_Remark = "Remark";		            
		public const string Property_History = "History";		            
		public const string Property_IsDeleted = "IsDeleted";		            
		public const string Property_BranchId = "BranchId";		            
		public const string Property_CompanyId = "CompanyId";		            
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_CreateById = "CreateById";		            
		public const string Property_LastChanged = "LastChanged";		            
		public const string Property_LastChangedById = "LastChangedById";		            
		public const string Property_IsBank = "IsBank";		            
              
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
      
     
     
      
        public static BAcnt_Ledger GetNew(Int32 id=0)
        {
           BAcnt_Ledger obj = new BAcnt_Ledger();
               obj.Id = id;
                obj.Name = string.Empty ; 
                obj.Code = string.Empty ; 
                obj.CodeName = string.Empty ; 
                obj.TypeEnumId = 0 ; 
                obj.ParentId = null ; 
                obj.IsGroup = false ; 
                obj.OpeningBalance = null ; 
                obj.OpeningDate = null ; 
                obj.Remark = string.Empty ; 
                obj.History = string.Empty ; 
                obj.IsDeleted = false ; 
                obj.BranchId = 0 ; 
                obj.CompanyId = 0 ; 
                obj.CreateDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreateById = 1 ;
                obj.LastChanged = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LastChangedById = 1 ;
                obj.IsBank = false ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

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
    public partial class User_ProfileChangeAudit
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_UserId = "UserId";		            
		public const string Property_FieldEnumId = "FieldEnumId";		            
        public const string Property_Field = "Field";
		public const string Property_OldPk = "OldPk";		            
		public const string Property_NewPk = "NewPk";		            
		public const string Property_OldValue = "OldValue";		            
		public const string Property_NewValue = "NewValue";		            
		public const string Property_Remark = "Remark";		            
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_CreateById = "CreateById";		            
		public const string Property_NewUserId = "NewUserId";		            
		public const string Property_SemesterId = "SemesterId";		            
		public const string Property_ChangeByIpAddress = "ChangeByIpAddress";		            
              
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
      
     
     
      
        public static User_ProfileChangeAudit GetNew(Int32 id=0)
        {
           User_ProfileChangeAudit obj = new User_ProfileChangeAudit();
               obj.Id = id;
                obj.UserId = 0 ; 
                obj.FieldEnumId = 0 ; 
                obj.OldPk = null ; 
                obj.NewPk = null ; 
                obj.OldValue = null ; 
                obj.NewValue = null ; 
                obj.Remark = null ; 
                obj.CreateDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreateById = 1 ;
                obj.NewUserId = null ; 
                obj.SemesterId = null ; 
                obj.ChangeByIpAddress = null ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

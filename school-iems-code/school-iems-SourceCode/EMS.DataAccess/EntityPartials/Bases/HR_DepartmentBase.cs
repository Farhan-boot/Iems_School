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
    public partial class HR_Department
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_Name = "Name";		            
		public const string Property_ShortName = "ShortName";		            
		public const string Property_DepartmentNo = "DepartmentNo";		            
		public const string Property_ParentDeptId = "ParentDeptId";		            
		public const string Property_Description = "Description";		            
		public const string Property_Address = "Address";		            
		public const string Property_MapEmbedCode = "MapEmbedCode";		            
		public const string Property_Email = "Email";		            
		public const string Property_Mobile = "Mobile";		            
		public const string Property_Phone = "Phone";		            
		public const string Property_Fax = "Fax";		            
		public const string Property_Website = "Website";		            
		public const string Property_Founded = "Founded";		            
		public const string Property_TypeEnumId = "TypeEnumId";		            
        public const string Property_Type = "Type";
		public const string Property_StatusEnumId = "StatusEnumId";		            
        public const string Property_Status = "Status";
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_CreateById = "CreateById";		            
		public const string Property_LastChanged = "LastChanged";		            
		public const string Property_LastChangedById = "LastChangedById";		            
		public const string Property_Priority = "Priority";		            
		public const string Property_OrgId = "OrgId";		            
		public const string Property_IsDeleted = "IsDeleted";		            
              
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
      
     
     
      
        public static HR_Department GetNew(Int32 id=0)
        {
           HR_Department obj = new HR_Department();
               obj.Id = id;
                obj.Name = string.Empty ; 
                obj.ShortName = string.Empty ; 
                obj.DepartmentNo = 0 ; 
                obj.ParentDeptId = 0 ; 
                obj.Description = string.Empty ; 
                obj.Address = string.Empty ; 
                obj.MapEmbedCode = string.Empty ; 
                obj.Email = string.Empty ; 
                obj.Mobile = string.Empty ; 
                obj.Phone = string.Empty ; 
                obj.Fax = string.Empty ; 
                obj.Website = string.Empty ; 
                obj.Founded = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.TypeEnumId = 0 ; 
                obj.StatusEnumId = 0 ; 
                obj.CreateDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreateById = 1 ;
                obj.LastChanged = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LastChangedById = 1 ;
                obj.Priority = 0 ; 
                obj.OrgId = 0 ; 
                obj.IsDeleted = false ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

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
    public partial class General_Notice
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_Title = "Title";		            
		public const string Property_Description = "Description";		            
		public const string Property_FilePath = "FilePath";		            
		public const string Property_OrderNo = "OrderNo";		            
		public const string Property_VisibilityTypeEnumId = "VisibilityTypeEnumId";		            
        public const string Property_VisibilityType = "VisibilityType";
		public const string Property_PublishDate = "PublishDate";		            
		public const string Property_ExpiryDate = "ExpiryDate";		            
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_CreateById = "CreateById";		            
		public const string Property_LastChanged = "LastChanged";		            
		public const string Property_LastChangedById = "LastChangedById";		            
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
      
     
     
      
        public static General_Notice GetNew(Int32 id=0)
        {
           General_Notice obj = new General_Notice();
               obj.Id = id;
                obj.Title = string.Empty ; 
                obj.Description = null ; 
                obj.FilePath = null ; 
                obj.OrderNo = 0 ; 
                obj.VisibilityTypeEnumId = 0 ; 
                obj.PublishDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.ExpiryDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreateDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreateById = 1 ;
                obj.LastChanged = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LastChangedById = 1 ;
                obj.IsDeleted = false ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

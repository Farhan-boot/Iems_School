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
    public partial class General_Division
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_Name = "Name";		            
		public const string Property_DivisionNo = "DivisionNo";		            
		public const string Property_CountryId = "CountryId";		            
		public const string Property_OrderNo = "OrderNo";		            
		public const string Property_IsDeleted = "IsDeleted";		            
		public const string Property_IsActive = "IsActive";		            
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_CreatedById = "CreatedById";		            
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
      
     
     
      
        public static General_Division GetNew(Int64 id=0)
        {
           General_Division obj = new General_Division();
               obj.Id = id;
                obj.Name = string.Empty ; 
                obj.DivisionNo = null ; 
                obj.CountryId = 0 ; 
                obj.OrderNo = 0 ; 
                obj.IsDeleted = false ; 
                obj.IsActive = false ; 
                obj.CreateDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreatedById = 0 ; 
                obj.LastChanged = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LastChangedById = 1 ;
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

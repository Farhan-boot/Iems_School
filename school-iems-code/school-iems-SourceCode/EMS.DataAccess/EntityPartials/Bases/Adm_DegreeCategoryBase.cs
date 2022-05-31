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
    public partial class Adm_DegreeCategory
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_Name = "Name";		            
		public const string Property_FullName = "FullName";		            
		public const string Property_DegreeEquivalentEnumId = "DegreeEquivalentEnumId";		            
        public const string Property_DegreeEquivalent = "DegreeEquivalent";
		public const string Property_BoardTypeEnumId = "BoardTypeEnumId";		            
        public const string Property_BoardType = "BoardType";
		public const string Property_OrderNo = "OrderNo";		            
		public const string Property_IsEnable = "IsEnable";		            
              
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
      
     
     
      
        public static Adm_DegreeCategory GetNew(Int32 id=0)
        {
           Adm_DegreeCategory obj = new Adm_DegreeCategory();
               obj.Id = id;
                obj.Name = string.Empty ; 
                obj.FullName = string.Empty ; 
                obj.DegreeEquivalentEnumId = 0 ; 
                obj.BoardTypeEnumId = 0 ; 
                obj.OrderNo = 0 ; 
                obj.IsEnable = false ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

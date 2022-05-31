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
    public partial class Aca_ClassMaterialFileMap
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_ClassId = "ClassId";		            
		public const string Property_FileId = "FileId";		            
		public const string Property_UserId = "UserId";		            
              
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
      
     
     
      
        public static Aca_ClassMaterialFileMap GetNew(Int64 id=0)
        {
           Aca_ClassMaterialFileMap obj = new Aca_ClassMaterialFileMap();
               obj.Id = id;
                obj.ClassId = 0 ; 
                obj.FileId = 0 ; 
                obj.UserId = 0 ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

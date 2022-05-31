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
    public partial class User_Rank
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_Name = "Name";		            
		public const string Property_LocalName = "LocalName";		            
		public const string Property_ShortName = "ShortName";		            
		public const string Property_Priority = "Priority";		            
		public const string Property_CategoryEnumId = "CategoryEnumId";		            
        public const string Property_Category = "Category";
		public const string Property_JobClassEnumId = "JobClassEnumId";		            
        public const string Property_JobClass = "JobClass";
		public const string Property_AcademicLevelEnumId = "AcademicLevelEnumId";		            
        public const string Property_AcademicLevel = "AcademicLevel";
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
      
     
     
      
        public static User_Rank GetNew(Int64 id=0)
        {
           User_Rank obj = new User_Rank();
               obj.Id = id;
                obj.Name = string.Empty ; 
                obj.LocalName = null ; 
                obj.ShortName = string.Empty ; 
                obj.Priority = 0 ; 
                obj.CategoryEnumId = 0 ; 
                obj.JobClassEnumId = 0 ; 
                obj.AcademicLevelEnumId = 0 ; 
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

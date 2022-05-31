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
    public partial class User_Experience
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_UserId = "UserId";		            
		public const string Property_CompanyName = "CompanyName";		            
		public const string Property_CompanyBusiness = "CompanyBusiness";		            
		public const string Property_CompanyLocation = "CompanyLocation";		            
		public const string Property_Position = "Position";		            
		public const string Property_Department = "Department";		            
		public const string Property_Responsibilities = "Responsibilities";		            
		public const string Property_JoinDate = "JoinDate";		            
		public const string Property_EndDate = "EndDate";		            
		public const string Property_IsActive = "IsActive";		            
		public const string Property_IsActing = "IsActing";		            
              
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
      
     
     
      
        public static User_Experience GetNew(Int64 id=0)
        {
           User_Experience obj = new User_Experience();
               obj.Id = id;
                obj.UserId = 0 ; 
                obj.CompanyName = string.Empty ; 
                obj.CompanyBusiness = string.Empty ; 
                obj.CompanyLocation = string.Empty ; 
                obj.Position = string.Empty ; 
                obj.Department = string.Empty ; 
                obj.Responsibilities = string.Empty ; 
                obj.JoinDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.EndDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.IsActive = false ; 
                obj.IsActing = false ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

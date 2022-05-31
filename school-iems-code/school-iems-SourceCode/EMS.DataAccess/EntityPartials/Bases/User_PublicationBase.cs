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
    public partial class User_Publication
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_UserId = "UserId";		            
		public const string Property_PublicationDate = "PublicationDate";		            
		public const string Property_Title = "Title";		            
		public const string Property_Volume = "Volume";		            
		public const string Property_Journal = "Journal";		            
              
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
      
     
     
      
        public static User_Publication GetNew(Int64 id=0)
        {
           User_Publication obj = new User_Publication();
               obj.Id = id;
                obj.UserId = 0 ; 
                obj.PublicationDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.Title = string.Empty ; 
                obj.Volume = string.Empty ; 
                obj.Journal = string.Empty ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

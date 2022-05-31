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
    public partial class Aca_StudentRegistration
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_SemesterId = "SemesterId";		            
		public const string Property_LevelId = "LevelId";		            
		public const string Property_StudentId = "StudentId";		            
		public const string Property_RegistrationNo = "RegistrationNo";		            
		public const string Property_IPAddress = "IPAddress";		            
		public const string Property_HostAddress = "HostAddress";		            
		public const string Property_RegStatusEnumId = "RegStatusEnumId";		            
        public const string Property_RegStatus = "RegStatus";
		public const string Property_FeeCodeId = "FeeCodeId";		            
		public const string Property_BankId = "BankId";		            
		public const string Property_Remark = "Remark";		            
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
      
     
     
      
        public static Aca_StudentRegistration GetNew(Int32 id=0)
        {
           Aca_StudentRegistration obj = new Aca_StudentRegistration();
               obj.Id = id;
                obj.SemesterId = 0 ; 
                obj.LevelId = 0 ; 
                obj.StudentId = 0 ; 
                obj.RegistrationNo = string.Empty ; 
                obj.IPAddress = string.Empty ; 
                obj.HostAddress = string.Empty ; 
                obj.RegStatusEnumId = 0 ; 
                obj.FeeCodeId = 0 ; 
                obj.BankId = 0 ; 
                obj.Remark = null ; 
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

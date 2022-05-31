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
    public partial class Aca_Program
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_Name = "Name";		            
		public const string Property_ShortName = "ShortName";		            
		public const string Property_ShortTitle = "ShortTitle";		            
		public const string Property_OfficialCertificateTitle = "OfficialCertificateTitle";		            
		public const string Property_Description = "Description";		            
		public const string Property_Code = "Code";		            
		public const string Property_ProgramTypeEnumId = "ProgramTypeEnumId";		            
        public const string Property_ProgramType = "ProgramType";
		public const string Property_ClassTimingGroupEnumId = "ClassTimingGroupEnumId";		            
        public const string Property_ClassTimingGroup = "ClassTimingGroup";
		public const string Property_CreateDate = "CreateDate";		            
		public const string Property_CreateById = "CreateById";		            
		public const string Property_LastChanged = "LastChanged";		            
		public const string Property_LastChangedById = "LastChangedById";		            
		public const string Property_IsDeleted = "IsDeleted";		            
		public const string Property_LanguageMediumEnumId = "LanguageMediumEnumId";		            
        public const string Property_LanguageMedium = "LanguageMedium";
		public const string Property_DefaultClassSectionRange = "DefaultClassSectionRange";		            
		public const string Property_IsEnable = "IsEnable";		            
		public const string Property_ClassTeacherId = "ClassTeacherId";		            
		public const string Property_CurriculumId = "CurriculumId";		            
		public const string Property_SectionId = "SectionId";		            
		public const string Property_RequiredProgramTypeEnumId = "RequiredProgramTypeEnumId";		            
        public const string Property_RequiredProgramType = "RequiredProgramType";
		public const string Property_SemesterId = "SemesterId";		            
		public const string Property_DepartmentId = "DepartmentId";		            
		public const string Property_OrderNo = "OrderNo";		            
              
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
      
     
     
      
        public static Aca_Program GetNew(Int32 id=0)
        {
           Aca_Program obj = new Aca_Program();
               obj.Id = id;
                obj.Name = string.Empty ; 
                obj.ShortName = string.Empty ; 
                obj.ShortTitle = string.Empty ; 
                obj.OfficialCertificateTitle = string.Empty ; 
                obj.Description = null ; 
                obj.Code = null ; 
                obj.ProgramTypeEnumId = 0 ; 
                obj.ClassTimingGroupEnumId = 0 ; 
                obj.CreateDate = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.CreateById = 1 ;
                obj.LastChanged = new DateTime(1753,1,1,12,0,0,0) ; 
                obj.LastChangedById = 1 ;
                obj.IsDeleted = false ; 
                obj.LanguageMediumEnumId = 0 ; 
                obj.DefaultClassSectionRange = 0 ; 
                obj.IsEnable = false ; 
                obj.ClassTeacherId = null ; 
                obj.CurriculumId = null ; 
                obj.SectionId = null ; 
                obj.RequiredProgramTypeEnumId = null ; 
                obj.SemesterId = 0 ; 
                obj.DepartmentId = 0 ; 
                obj.OrderNo = 0.0F ; 
                obj.IsAlreadyAdded = false;
                GetNewExtra(obj);
                return obj;
        }
       
	}
}

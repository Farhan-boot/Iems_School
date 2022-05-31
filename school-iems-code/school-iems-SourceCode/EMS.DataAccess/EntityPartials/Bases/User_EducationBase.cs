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
    public partial class User_Education
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_UserId = "UserId";		            
		public const string Property_DegreeEquivalentEnumId = "DegreeEquivalentEnumId";		            
        public const string Property_DegreeEquivalent = "DegreeEquivalent";
		public const string Property_DegreeCategoryId = "DegreeCategoryId";		            
		public const string Property_DegreeTitle = "DegreeTitle";		            
		public const string Property_DegreeGroupMajorEnumId = "DegreeGroupMajorEnumId";		            
        public const string Property_DegreeGroupMajor = "DegreeGroupMajor";
		public const string Property_DegreeGroupMajorOther = "DegreeGroupMajorOther";		            
		public const string Property_InstitutionName = "InstitutionName";		            
		public const string Property_BoardId = "BoardId";		            
		public const string Property_CourseDuration = "CourseDuration";		            
		public const string Property_YearOfPassing = "YearOfPassing";		            
		public const string Property_RegistrationNo = "RegistrationNo";		            
		public const string Property_StudentRollOrIdNo = "StudentRollOrIdNo";		            
		public const string Property_ResultSystemEnumId = "ResultSystemEnumId";		            
        public const string Property_ResultSystem = "ResultSystem";
		public const string Property_ObtainedGpaOrMarks = "ObtainedGpaOrMarks";		            
		public const string Property_ObtainedGradeOrClass = "ObtainedGradeOrClass";		            
		public const string Property_GpaNo4Sub = "GpaNo4Sub";		            
		public const string Property_FullMarksOrScale = "FullMarksOrScale";		            
		public const string Property_IsGolden = "IsGolden";		            
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
      
     
     
      
        public static User_Education GetNew(Int32 id=0)
        {
           User_Education obj = new User_Education();
               obj.Id = id;
                obj.UserId = 0 ; 
                obj.DegreeEquivalentEnumId = 0 ; 
                obj.DegreeCategoryId = 0 ; 
                obj.DegreeTitle = string.Empty ; 
                obj.DegreeGroupMajorEnumId = 0 ; 
                obj.DegreeGroupMajorOther = null ; 
                obj.InstitutionName = null ; 
                obj.BoardId = null ; 
                obj.CourseDuration = null ; 
                obj.YearOfPassing = null ; 
                obj.RegistrationNo = null ; 
                obj.StudentRollOrIdNo = null ; 
                obj.ResultSystemEnumId = 0 ; 
                obj.ObtainedGpaOrMarks = null ; 
                obj.ObtainedGradeOrClass = null ; 
                obj.GpaNo4Sub = null ; 
                obj.FullMarksOrScale = null ; 
                obj.IsGolden = null ; 
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

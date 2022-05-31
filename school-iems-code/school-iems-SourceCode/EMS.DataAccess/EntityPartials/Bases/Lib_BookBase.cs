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
    public partial class Lib_Book
	{
    	#region Constants
		public const string Property_Id = "Id";		            
		public const string Property_RecordId = "RecordId";		            
		public const string Property_Title = "Title";		            
		public const string Property_SubTitle = "SubTitle";		            
		public const string Property_SubSubTitle = "SubSubTitle";		            
		public const string Property_Publisher = "Publisher";		            
		public const string Property_PublicationPlace = "PublicationPlace";		            
		public const string Property_PublicationYear = "PublicationYear";		            
		public const string Property_Edition = "Edition";		            
		public const string Property_ISBN = "ISBN";		            
		public const string Property_Description = "Description";		            
		public const string Property_Bibliography = "Bibliography";		            
		public const string Property_LcClassification = "LcClassification";		            
		public const string Property_DeweyClass = "DeweyClass";		            
		public const string Property_Series = "Series";		            
		public const string Property_ISSN = "ISSN";		            
		public const string Property_Volume = "Volume";		            
		public const string Property_Note = "Note";		            
		public const string Property_BookStatusEnumId = "BookStatusEnumId";		            
        public const string Property_BookStatus = "BookStatus";
		public const string Property_Responsibility = "Responsibility";		            
		public const string Property_CallNo = "CallNo";		            
		public const string Property_BindingTypeEnumId = "BindingTypeEnumId";		            
        public const string Property_BindingType = "BindingType";
		public const string Property_FileLocation = "FileLocation";		            
		public const string Property_SourceEnumId = "SourceEnumId";		            
        public const string Property_Source = "Source";
		public const string Property_Pagination = "Pagination";		            
		public const string Property_Size = "Size";		            
		public const string Property_OtherPhysicalDetails = "OtherPhysicalDetails";		            
		public const string Property_LanguageId = "LanguageId";		            
		public const string Property_CategoryEnumId = "CategoryEnumId";		            
        public const string Property_Category = "Category";
		public const string Property_Remarks = "Remarks";		            
		public const string Property_MarcXml = "MarcXml";		            
		public const string Property_Marc = "Marc";		            
		public const string Property_MarcUrl = "MarcUrl";		            
		public const string Property_CopyrightYear = "CopyrightYear";		            
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
      
     
     
      
        public static Lib_Book GetNew(Int64 id=0)
        {
           Lib_Book obj = new Lib_Book();
               obj.Id = id;
                obj.RecordId = 0 ; 
                obj.Title = string.Empty ; 
                obj.SubTitle = null ; 
                obj.SubSubTitle = null ; 
                obj.Publisher = null ; 
                obj.PublicationPlace = null ; 
                obj.PublicationYear = null ; 
                obj.Edition = null ; 
                obj.ISBN = string.Empty ; 
                obj.Description = null ; 
                obj.Bibliography = null ; 
                obj.LcClassification = null ; 
                obj.DeweyClass = string.Empty ; 
                obj.Series = null ; 
                obj.ISSN = null ; 
                obj.Volume = null ; 
                obj.Note = null ; 
                obj.BookStatusEnumId = 0 ; 
                obj.Responsibility = string.Empty ; 
                obj.CallNo = string.Empty ; 
                obj.BindingTypeEnumId = 0 ; 
                obj.FileLocation = null ; 
                obj.SourceEnumId = 0 ; 
                obj.Pagination = null ; 
                obj.Size = null ; 
                obj.OtherPhysicalDetails = null ; 
                obj.LanguageId = null ; 
                obj.CategoryEnumId = 0 ; 
                obj.Remarks = null ; 
                obj.MarcXml = null ; 
                obj.Marc = null ; 
                obj.MarcUrl = null ; 
                obj.CopyrightYear = null ; 
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

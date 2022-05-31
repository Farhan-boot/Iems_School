//File:Json Model Base Partial
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;

using EMS.Framework;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;

namespace EMS.Web.Jsons.Models
{
    /// <summary>
    /// Do not modify this class, edit partial instead.
    /// </summary>   
	public partial class Lib_BookJson 
	{
          public String Id { get; set; }
          public Int32 RecordId { get; set; }
          public String Title { get; set; }
          public String SubTitle { get; set; }
          public String SubSubTitle { get; set; }
          public String Publisher { get; set; }
          public String PublicationPlace { get; set; }
          public String PublicationYear { get; set; }
          public String Edition { get; set; }
          public String ISBN { get; set; }
          public String Description { get; set; }
          public String Bibliography { get; set; }
          public String LcClassification { get; set; }
          public String DeweyClass { get; set; }
          public String Series { get; set; }
          public String ISSN { get; set; }
          public String Volume { get; set; }
          public String Note { get; set; }
          public Byte BookStatusEnumId { get; set; }
          public string BookStatus { get; set; }
          public String Responsibility { get; set; }
          public String CallNo { get; set; }
          public Byte BindingTypeEnumId { get; set; }
          public string BindingType { get; set; }
          public String FileLocation { get; set; }
          public Byte SourceEnumId { get; set; }
          public string Source { get; set; }
          public String Pagination { get; set; }
          public String Size { get; set; }
          public String OtherPhysicalDetails { get; set; }
          public String LanguageId { get; set; }
          public Byte CategoryEnumId { get; set; }
          public string Category { get; set; }
          public String Remarks { get; set; }
          public String MarcXml { get; set; }
          public Byte[] Marc { get; set; }
          public String MarcUrl { get; set; }
          public Nullable<Int16> CopyrightYear { get; set; }
          public DateTime CreateDate { get; set; }
          public String CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public String LastChangedById { get; set; }
        /// <summary>
        /// This is for detect selected columns in List.
        /// This is not database property.
        /// </summary>
        public bool IsSelected { get; set; }
        /// <summary>
        /// This is detect already added item in desired table
        /// This is not database property.
        /// </summary>
        public bool IsAlreadyAdded { get; set; }
	}
    #region Mapper
     /// <summary>
    /// Do not modify this class, edit partial instead.
    /// </summary>   
	public static partial class EntityMapper
	{
	   
		public static void Map(this Lib_Book entity, Lib_BookJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.RecordId = entity.RecordId ; 
			 toJson.Title = entity.Title ; 
			 toJson.SubTitle = entity.SubTitle ; 
			 toJson.SubSubTitle = entity.SubSubTitle ; 
			 toJson.Publisher = entity.Publisher ; 
			 toJson.PublicationPlace = entity.PublicationPlace ; 
			 toJson.PublicationYear = entity.PublicationYear ; 
			 toJson.Edition = entity.Edition ; 
			 toJson.ISBN = entity.ISBN ; 
			 toJson.Description = entity.Description ; 
			 toJson.Bibliography = entity.Bibliography ; 
			 toJson.LcClassification = entity.LcClassification ; 
			 toJson.DeweyClass = entity.DeweyClass ; 
			 toJson.Series = entity.Series ; 
			 toJson.ISSN = entity.ISSN ; 
			 toJson.Volume = entity.Volume ; 
			 toJson.Note = entity.Note ; 
			 toJson.BookStatusEnumId = entity.BookStatusEnumId ; 
             if(entity.BookStatusEnumId!=null)
             toJson.BookStatus = entity.BookStatus.ToString().AddSpacesToSentence();
			 toJson.Responsibility = entity.Responsibility ; 
			 toJson.CallNo = entity.CallNo ; 
			 toJson.BindingTypeEnumId = entity.BindingTypeEnumId ; 
             if(entity.BindingTypeEnumId!=null)
             toJson.BindingType = entity.BindingType.ToString().AddSpacesToSentence();
			 toJson.FileLocation = entity.FileLocation ; 
			 toJson.SourceEnumId = entity.SourceEnumId ; 
             if(entity.SourceEnumId!=null)
             toJson.Source = entity.Source.ToString().AddSpacesToSentence();
			 toJson.Pagination = entity.Pagination ; 
			 toJson.Size = entity.Size ; 
			 toJson.OtherPhysicalDetails = entity.OtherPhysicalDetails ; 
             if(entity.LanguageId!=null)
			 toJson.LanguageId = entity.LanguageId.ToString() ; 
			 toJson.CategoryEnumId = entity.CategoryEnumId ; 
             if(entity.CategoryEnumId!=null)
             toJson.Category = entity.Category.ToString().AddSpacesToSentence();
			 toJson.Remarks = entity.Remarks ; 
			 toJson.MarcXml = entity.MarcXml ; 
			 toJson.Marc = entity.Marc ; 
			 toJson.MarcUrl = entity.MarcUrl ; 
			 toJson.CopyrightYear = entity.CopyrightYear ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Lib_BookJson json, Lib_Book toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
    			 toEntity.RecordId=json.RecordId;
                 toEntity.Title = json.Title?.Trim() ?? json.Title;
                 toEntity.SubTitle = json.SubTitle?.Trim() ?? json.SubTitle;
                 toEntity.SubSubTitle = json.SubSubTitle?.Trim() ?? json.SubSubTitle;
                 toEntity.Publisher = json.Publisher?.Trim() ?? json.Publisher;
                 toEntity.PublicationPlace = json.PublicationPlace?.Trim() ?? json.PublicationPlace;
                 toEntity.PublicationYear = json.PublicationYear?.Trim() ?? json.PublicationYear;
                 toEntity.Edition = json.Edition?.Trim() ?? json.Edition;
                 toEntity.ISBN = json.ISBN?.Trim() ?? json.ISBN;
                 toEntity.Description = json.Description?.Trim() ?? json.Description;
                 toEntity.Bibliography = json.Bibliography?.Trim() ?? json.Bibliography;
                 toEntity.LcClassification = json.LcClassification?.Trim() ?? json.LcClassification;
                 toEntity.DeweyClass = json.DeweyClass?.Trim() ?? json.DeweyClass;
                 toEntity.Series = json.Series?.Trim() ?? json.Series;
                 toEntity.ISSN = json.ISSN?.Trim() ?? json.ISSN;
                 toEntity.Volume = json.Volume?.Trim() ?? json.Volume;
                 toEntity.Note = json.Note?.Trim() ?? json.Note;
    			 toEntity.BookStatusEnumId=json.BookStatusEnumId;
                 toEntity.Responsibility = json.Responsibility?.Trim() ?? json.Responsibility;
                 toEntity.CallNo = json.CallNo?.Trim() ?? json.CallNo;
    			 toEntity.BindingTypeEnumId=json.BindingTypeEnumId;
                 toEntity.FileLocation = json.FileLocation?.Trim() ?? json.FileLocation;
    			 toEntity.SourceEnumId=json.SourceEnumId;
                 toEntity.Pagination = json.Pagination?.Trim() ?? json.Pagination;
                 toEntity.Size = json.Size?.Trim() ?? json.Size;
                 toEntity.OtherPhysicalDetails = json.OtherPhysicalDetails?.Trim() ?? json.OtherPhysicalDetails;
                 toEntity.LanguageId = long.TryParse(json.LanguageId, out output) ? output : (Int64?)null;
    			 toEntity.CategoryEnumId=json.CategoryEnumId;
                 toEntity.Remarks = json.Remarks?.Trim() ?? json.Remarks;
                 toEntity.MarcXml = json.MarcXml?.Trim() ?? json.MarcXml;
    			 toEntity.Marc=json.Marc;
                 toEntity.MarcUrl = json.MarcUrl?.Trim() ?? json.MarcUrl;
    			 toEntity.CopyrightYear=json.CopyrightYear;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Lib_Book> entityList, IList<Lib_BookJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new Lib_BookJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Lib_BookJson> jsonList, ICollection<Lib_Book> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                Lib_Book obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = Lib_Book.GetNew();
                    json.Map(obj);
                    toEntityList.Add(obj);
                    
                }
                else
                {
                   json.Map(obj);
                }
            }
        }
	}
    #endregion
}

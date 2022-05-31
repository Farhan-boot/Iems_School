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
	public partial class Lib_BookCopyJson 
	{
          public String Id { get; set; }
          public Int32 AccessionNo { get; set; }
          public String Barcode { get; set; }
          public String BookId { get; set; }
          public DateTime DateAcquired { get; set; }
          public String Shelf { get; set; }
          public Byte CopySourceEnumId { get; set; }
          public string CopySource { get; set; }
          public Nullable<Decimal> Price { get; set; }
          public String SupplierId { get; set; }
          public Byte BorrowingAllowedEnumId { get; set; }
          public string BorrowingAllowed { get; set; }
          public Byte AvailabilityEnumId { get; set; }
          public string Availability { get; set; }
          public Byte ReservationEnumId { get; set; }
          public string Reservation { get; set; }
          public Byte CopyStatusEnumId { get; set; }
          public string CopyStatus { get; set; }
          public Byte DeletedEnumId { get; set; }
          public string Deleted { get; set; }
          public Byte CopyTypeEnumId { get; set; }
          public string CopyType { get; set; }
          public String Remarks { get; set; }
          public String CallNo { get; set; }
          public String PermanentLibraryId { get; set; }
          public String PresentLibraryId { get; set; }
          public Byte CategoryEnumId { get; set; }
          public string Category { get; set; }
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
	   
		public static void Map(this Lib_BookCopy entity, Lib_BookCopyJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.AccessionNo = entity.AccessionNo ; 
			 toJson.Barcode = entity.Barcode ; 
			 toJson.BookId = entity.BookId.ToString() ; 
			 toJson.DateAcquired = entity.DateAcquired ; 
			 toJson.Shelf = entity.Shelf ; 
			 toJson.CopySourceEnumId = entity.CopySourceEnumId ; 
             if(entity.CopySourceEnumId!=null)
             toJson.CopySource = entity.CopySource.ToString().AddSpacesToSentence();
			 toJson.Price = entity.Price ; 
             if(entity.SupplierId!=null)
			 toJson.SupplierId = entity.SupplierId.ToString() ; 
			 toJson.BorrowingAllowedEnumId = entity.BorrowingAllowedEnumId ; 
             if(entity.BorrowingAllowedEnumId!=null)
             toJson.BorrowingAllowed = entity.BorrowingAllowed.ToString().AddSpacesToSentence();
			 toJson.AvailabilityEnumId = entity.AvailabilityEnumId ; 
             if(entity.AvailabilityEnumId!=null)
             toJson.Availability = entity.Availability.ToString().AddSpacesToSentence();
			 toJson.ReservationEnumId = entity.ReservationEnumId ; 
             if(entity.ReservationEnumId!=null)
             toJson.Reservation = entity.Reservation.ToString().AddSpacesToSentence();
			 toJson.CopyStatusEnumId = entity.CopyStatusEnumId ; 
             if(entity.CopyStatusEnumId!=null)
             toJson.CopyStatus = entity.CopyStatus.ToString().AddSpacesToSentence();
			 toJson.DeletedEnumId = entity.DeletedEnumId ; 
             if(entity.DeletedEnumId!=null)
             toJson.Deleted = entity.Deleted.ToString().AddSpacesToSentence();
			 toJson.CopyTypeEnumId = entity.CopyTypeEnumId ; 
             if(entity.CopyTypeEnumId!=null)
             toJson.CopyType = entity.CopyType.ToString().AddSpacesToSentence();
			 toJson.Remarks = entity.Remarks ; 
			 toJson.CallNo = entity.CallNo ; 
             if(entity.PermanentLibraryId!=null)
			 toJson.PermanentLibraryId = entity.PermanentLibraryId.ToString() ; 
             if(entity.PresentLibraryId!=null)
			 toJson.PresentLibraryId = entity.PresentLibraryId.ToString() ; 
			 toJson.CategoryEnumId = entity.CategoryEnumId ; 
             if(entity.CategoryEnumId!=null)
             toJson.Category = entity.Category.ToString().AddSpacesToSentence();
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Lib_BookCopyJson json, Lib_BookCopy toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
    			 toEntity.AccessionNo=json.AccessionNo;
                 toEntity.Barcode = json.Barcode?.Trim() ?? json.Barcode;
                 toEntity.BookId = long.TryParse(json.BookId, out output) ? output : 0;
    			 toEntity.DateAcquired=json.DateAcquired;
                 toEntity.Shelf = json.Shelf?.Trim() ?? json.Shelf;
    			 toEntity.CopySourceEnumId=json.CopySourceEnumId;
    			 toEntity.Price=json.Price;
                 toEntity.SupplierId = long.TryParse(json.SupplierId, out output) ? output : (Int64?)null;
    			 toEntity.BorrowingAllowedEnumId=json.BorrowingAllowedEnumId;
    			 toEntity.AvailabilityEnumId=json.AvailabilityEnumId;
    			 toEntity.ReservationEnumId=json.ReservationEnumId;
    			 toEntity.CopyStatusEnumId=json.CopyStatusEnumId;
    			 toEntity.DeletedEnumId=json.DeletedEnumId;
    			 toEntity.CopyTypeEnumId=json.CopyTypeEnumId;
                 toEntity.Remarks = json.Remarks?.Trim() ?? json.Remarks;
                 toEntity.CallNo = json.CallNo?.Trim() ?? json.CallNo;
                 toEntity.PermanentLibraryId = long.TryParse(json.PermanentLibraryId, out output) ? output : (Int64?)null;
                 toEntity.PresentLibraryId = long.TryParse(json.PresentLibraryId, out output) ? output : (Int64?)null;
    			 toEntity.CategoryEnumId=json.CategoryEnumId;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Lib_BookCopy> entityList, IList<Lib_BookCopyJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new Lib_BookCopyJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Lib_BookCopyJson> jsonList, ICollection<Lib_BookCopy> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                Lib_BookCopy obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = Lib_BookCopy.GetNew();
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

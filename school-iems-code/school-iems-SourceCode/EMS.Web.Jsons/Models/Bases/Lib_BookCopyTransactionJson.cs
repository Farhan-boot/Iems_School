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
	public partial class Lib_BookCopyTransactionJson 
	{
          public String Id { get; set; }
          public Int32 BorrowerId { get; set; }
          public String BookCopyId { get; set; }
          public DateTime BorrowedDate { get; set; }
          public String IssuedById { get; set; }
          public DateTime ExpectedReturnDate { get; set; }
          public Nullable<DateTime> ReturnedDate { get; set; }
          public String CollectedById { get; set; }
          public Nullable<Decimal> Fine { get; set; }
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
	   
		public static void Map(this Lib_BookCopyTransaction entity, Lib_BookCopyTransactionJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.BorrowerId = entity.BorrowerId ; 
			 toJson.BookCopyId = entity.BookCopyId.ToString() ; 
			 toJson.BorrowedDate = entity.BorrowedDate ; 
			 toJson.IssuedById = entity.IssuedById.ToString() ; 
			 toJson.ExpectedReturnDate = entity.ExpectedReturnDate ; 
			 toJson.ReturnedDate = entity.ReturnedDate ; 
             if(entity.CollectedById!=null)
			 toJson.CollectedById = entity.CollectedById.ToString() ; 
			 toJson.Fine = entity.Fine ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Lib_BookCopyTransactionJson json, Lib_BookCopyTransaction toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
    			 toEntity.BorrowerId=json.BorrowerId;
                 toEntity.BookCopyId = long.TryParse(json.BookCopyId, out output) ? output : 0;
    			 toEntity.BorrowedDate=json.BorrowedDate;
                 toEntity.IssuedById = long.TryParse(json.IssuedById, out output) ? output : 0;
    			 toEntity.ExpectedReturnDate=json.ExpectedReturnDate;
    			 toEntity.ReturnedDate=json.ReturnedDate;
                 toEntity.CollectedById = long.TryParse(json.CollectedById, out output) ? output : (Int64?)null;
    			 toEntity.Fine=json.Fine;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Lib_BookCopyTransaction> entityList, IList<Lib_BookCopyTransactionJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new Lib_BookCopyTransactionJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Lib_BookCopyTransactionJson> jsonList, ICollection<Lib_BookCopyTransaction> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                Lib_BookCopyTransaction obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = Lib_BookCopyTransaction.GetNew();
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

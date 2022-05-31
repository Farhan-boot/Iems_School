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
	public partial class Lib_BorrowerJson 
	{
          public Int32 Id { get; set; }
          public Byte BorrowerTypeEnumId { get; set; }
          public string BorrowerType { get; set; }
          public Decimal FineAmount { get; set; }
          public Int16 BorrowLimit { get; set; }
          public Decimal CreditLimit { get; set; }
          public Byte BorrowerStatusEnumId { get; set; }
          public string BorrowerStatus { get; set; }
          public Decimal TotalFine { get; set; }
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
	   
		public static void Map(this Lib_Borrower entity, Lib_BorrowerJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.BorrowerTypeEnumId = entity.BorrowerTypeEnumId ; 
             if(entity.BorrowerTypeEnumId!=null)
             toJson.BorrowerType = entity.BorrowerType.ToString().AddSpacesToSentence();
			 toJson.FineAmount = entity.FineAmount ; 
			 toJson.BorrowLimit = entity.BorrowLimit ; 
			 toJson.CreditLimit = entity.CreditLimit ; 
			 toJson.BorrowerStatusEnumId = entity.BorrowerStatusEnumId ; 
             if(entity.BorrowerStatusEnumId!=null)
             toJson.BorrowerStatus = entity.BorrowerStatus.ToString().AddSpacesToSentence();
			 toJson.TotalFine = entity.TotalFine ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Lib_BorrowerJson json, Lib_Borrower toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
    			 toEntity.BorrowerTypeEnumId=json.BorrowerTypeEnumId;
    			 toEntity.FineAmount=json.FineAmount;
    			 toEntity.BorrowLimit=json.BorrowLimit;
    			 toEntity.CreditLimit=json.CreditLimit;
    			 toEntity.BorrowerStatusEnumId=json.BorrowerStatusEnumId;
    			 toEntity.TotalFine=json.TotalFine;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Lib_Borrower> entityList, IList<Lib_BorrowerJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Lib_BorrowerJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Lib_BorrowerJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Lib_BorrowerJson> jsonList, ICollection<Lib_Borrower> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Lib_Borrower obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Lib_Borrower.GetNew();
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

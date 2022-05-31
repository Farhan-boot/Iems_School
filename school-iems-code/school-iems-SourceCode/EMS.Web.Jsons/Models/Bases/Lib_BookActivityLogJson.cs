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
	public partial class Lib_BookActivityLogJson 
	{
          public String Id { get; set; }
          public String BorrowerId { get; set; }
          public Int32 BarcodeId { get; set; }
          public Int32 BookActivityEnumId { get; set; }
          public string BookActivity { get; set; }
          public DateTime DoneOn { get; set; }
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
	   
		public static void Map(this Lib_BookActivityLog entity, Lib_BookActivityLogJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.BorrowerId = entity.BorrowerId.ToString() ; 
			 toJson.BarcodeId = entity.BarcodeId ; 
			 toJson.BookActivityEnumId = entity.BookActivityEnumId ; 
             if(entity.BookActivityEnumId!=null)
             toJson.BookActivity = entity.BookActivity.ToString().AddSpacesToSentence();
			 toJson.DoneOn = entity.DoneOn ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Lib_BookActivityLogJson json, Lib_BookActivityLog toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
                 toEntity.BorrowerId = long.TryParse(json.BorrowerId, out output) ? output : 0;
    			 toEntity.BarcodeId=json.BarcodeId;
    			 toEntity.BookActivityEnumId=json.BookActivityEnumId;
    			 toEntity.DoneOn=json.DoneOn;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Lib_BookActivityLog> entityList, IList<Lib_BookActivityLogJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new Lib_BookActivityLogJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Lib_BookActivityLogJson> jsonList, ICollection<Lib_BookActivityLog> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                Lib_BookActivityLog obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = Lib_BookActivityLog.GetNew();
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

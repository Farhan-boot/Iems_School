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
	public partial class Lib_BorrowerActivityLogJson 
	{
          public String Id { get; set; }
          public Int32 BorrowerId { get; set; }
          public Byte BorrowerActivityEnumId { get; set; }
          public string BorrowerActivity { get; set; }
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
	   
		public static void Map(this Lib_BorrowerActivityLog entity, Lib_BorrowerActivityLogJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.BorrowerId = entity.BorrowerId ; 
			 toJson.BorrowerActivityEnumId = entity.BorrowerActivityEnumId ; 
             if(entity.BorrowerActivityEnumId!=null)
             toJson.BorrowerActivity = entity.BorrowerActivity.ToString().AddSpacesToSentence();
			 toJson.DoneOn = entity.DoneOn ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Lib_BorrowerActivityLogJson json, Lib_BorrowerActivityLog toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
    			 toEntity.BorrowerId=json.BorrowerId;
    			 toEntity.BorrowerActivityEnumId=json.BorrowerActivityEnumId;
    			 toEntity.DoneOn=json.DoneOn;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Lib_BorrowerActivityLog> entityList, IList<Lib_BorrowerActivityLogJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new Lib_BorrowerActivityLogJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Lib_BorrowerActivityLogJson> jsonList, ICollection<Lib_BorrowerActivityLog> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                Lib_BorrowerActivityLog obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = Lib_BorrowerActivityLog.GetNew();
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

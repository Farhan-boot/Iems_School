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
	public partial class General_NoticeJson 
	{
          public Int32 Id { get; set; }
          public String Title { get; set; }
          public String Description { get; set; }
          public String FilePath { get; set; }
          public Int32 OrderNo { get; set; }
          public Byte VisibilityTypeEnumId { get; set; }
          public string VisibilityType { get; set; }
          public DateTime PublishDate { get; set; }
          public DateTime ExpiryDate { get; set; }
          public DateTime CreateDate { get; set; }
          public String CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public String LastChangedById { get; set; }
          public Boolean IsDeleted { get; set; }
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
	   
		public static void Map(this General_Notice entity, General_NoticeJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.Title = entity.Title ; 
			 toJson.Description = entity.Description ; 
			 toJson.FilePath = entity.FilePath ; 
			 toJson.OrderNo = entity.OrderNo ; 
			 toJson.VisibilityTypeEnumId = entity.VisibilityTypeEnumId ; 
             if(entity.VisibilityTypeEnumId!=null)
             toJson.VisibilityType = entity.VisibilityType.ToString().AddSpacesToSentence();
			 toJson.PublishDate = entity.PublishDate ; 
			 toJson.ExpiryDate = entity.ExpiryDate ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 
			 toJson.IsDeleted = entity.IsDeleted ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this General_NoticeJson json, General_Notice toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.Title = json.Title?.Trim() ?? json.Title;
                 toEntity.Description = json.Description?.Trim() ?? json.Description;
                 toEntity.FilePath = json.FilePath?.Trim() ?? json.FilePath;
    			 toEntity.OrderNo=json.OrderNo;
    			 toEntity.VisibilityTypeEnumId=json.VisibilityTypeEnumId;
    			 toEntity.PublishDate=json.PublishDate;
    			 toEntity.ExpiryDate=json.ExpiryDate;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;
    			 toEntity.IsDeleted=json.IsDeleted;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<General_Notice> entityList, IList<General_NoticeJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 General_NoticeJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new General_NoticeJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<General_NoticeJson> jsonList, ICollection<General_Notice> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    General_Notice obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = General_Notice.GetNew();
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

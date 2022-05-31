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
	public partial class Inv_ProductCategoryJson 
	{
          public Int32 Id { get; set; }
          public String Name { get; set; }
          public String SubTitle { get; set; }
          public Nullable<Int32> ParentId { get; set; }
          public String Description { get; set; }
          public Nullable<Int32> AssetTypeEnumId { get; set; }
          public string AssetType { get; set; }
          public Nullable<Boolean> IsBarcoded { get; set; }
          public String ProductCode { get; set; }
          public Nullable<Int32> DefaultStoreId { get; set; }
          public Nullable<Int32> UnitTypeEnumId { get; set; }
          public string UnitType { get; set; }
          public Nullable<Int32> WarningQuantity { get; set; }
          public String Remark { get; set; }
          public Nullable<Int32> ParentCategoryId { get; set; }
          public Nullable<Single> DepriciationRate { get; set; }
          public String History { get; set; }
          public DateTime CreateDate { get; set; }
          public Int32 CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public Int32 LastChangedById { get; set; }
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
	   
		public static void Map(this Inv_ProductCategory entity, Inv_ProductCategoryJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.Name = entity.Name ; 
			 toJson.SubTitle = entity.SubTitle ; 
			 toJson.ParentId = entity.ParentId ; 
			 toJson.Description = entity.Description ; 
			 toJson.AssetTypeEnumId = entity.AssetTypeEnumId ; 
             if(entity.AssetTypeEnumId!=null)
             toJson.AssetType = entity.AssetType.ToString().AddSpacesToSentence();
			 toJson.IsBarcoded = entity.IsBarcoded ; 
			 toJson.ProductCode = entity.ProductCode ; 
			 toJson.DefaultStoreId = entity.DefaultStoreId ; 
			 toJson.UnitTypeEnumId = entity.UnitTypeEnumId ; 
             if(entity.UnitTypeEnumId!=null)
             toJson.UnitType = entity.UnitType.ToString().AddSpacesToSentence();
			 toJson.WarningQuantity = entity.WarningQuantity ; 
			 toJson.Remark = entity.Remark ; 
			 toJson.ParentCategoryId = entity.ParentCategoryId ; 
			 toJson.DepriciationRate = entity.DepriciationRate ; 
			 toJson.History = entity.History ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById ; 
			 toJson.IsDeleted = entity.IsDeleted ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Inv_ProductCategoryJson json, Inv_ProductCategory toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
                 toEntity.SubTitle = json.SubTitle?.Trim() ?? json.SubTitle;
    			 toEntity.ParentId=json.ParentId;
                 toEntity.Description = json.Description?.Trim() ?? json.Description;
    			 toEntity.AssetTypeEnumId=json.AssetTypeEnumId;
    			 toEntity.IsBarcoded=json.IsBarcoded;
                 toEntity.ProductCode = json.ProductCode?.Trim() ?? json.ProductCode;
    			 toEntity.DefaultStoreId=json.DefaultStoreId;
    			 toEntity.UnitTypeEnumId=json.UnitTypeEnumId;
    			 toEntity.WarningQuantity=json.WarningQuantity;
                 toEntity.Remark = json.Remark?.Trim() ?? json.Remark;
    			 toEntity.ParentCategoryId=json.ParentCategoryId;
    			 toEntity.DepriciationRate=json.DepriciationRate;
                 toEntity.History = json.History?.Trim() ?? json.History;
    			 toEntity.CreateDate=json.CreateDate;
    			 toEntity.CreateById=json.CreateById;
    			 toEntity.LastChanged=json.LastChanged;
    			 toEntity.LastChangedById=json.LastChangedById;
    			 toEntity.IsDeleted=json.IsDeleted;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Inv_ProductCategory> entityList, IList<Inv_ProductCategoryJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Inv_ProductCategoryJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Inv_ProductCategoryJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Inv_ProductCategoryJson> jsonList, ICollection<Inv_ProductCategory> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Inv_ProductCategory obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Inv_ProductCategory.GetNew();
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

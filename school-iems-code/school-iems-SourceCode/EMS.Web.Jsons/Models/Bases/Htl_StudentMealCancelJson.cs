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
	public partial class Htl_StudentMealCancelJson 
	{
          public Int32 Id { get; set; }
          public DateTime CancelDate { get; set; }
          public Int32 MealTypeId { get; set; }
          public Int32 StudentId { get; set; }
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
	   
		public static void Map(this Htl_StudentMealCancel entity, Htl_StudentMealCancelJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.CancelDate = entity.CancelDate ; 
			 toJson.MealTypeId = entity.MealTypeId ; 
			 toJson.StudentId = entity.StudentId ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById ; 
			 toJson.IsDeleted = entity.IsDeleted ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Htl_StudentMealCancelJson json, Htl_StudentMealCancel toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
    			 toEntity.CancelDate=json.CancelDate;
    			 toEntity.MealTypeId=json.MealTypeId;
    			 toEntity.StudentId=json.StudentId;
    			 toEntity.CreateDate=json.CreateDate;
    			 toEntity.CreateById=json.CreateById;
    			 toEntity.LastChanged=json.LastChanged;
    			 toEntity.LastChangedById=json.LastChangedById;
    			 toEntity.IsDeleted=json.IsDeleted;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Htl_StudentMealCancel> entityList, IList<Htl_StudentMealCancelJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Htl_StudentMealCancelJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Htl_StudentMealCancelJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Htl_StudentMealCancelJson> jsonList, ICollection<Htl_StudentMealCancel> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Htl_StudentMealCancel obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Htl_StudentMealCancel.GetNew();
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

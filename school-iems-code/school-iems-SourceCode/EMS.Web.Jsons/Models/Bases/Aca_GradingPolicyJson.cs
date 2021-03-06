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
	public partial class Aca_GradingPolicyJson 
	{
          public Int32 Id { get; set; }
          public String Name { get; set; }
          public String Description { get; set; }
          public Boolean IsDeleted { get; set; }
          public Boolean IsActive { get; set; }
          public Byte ProgramTypeEnumId { get; set; }
          public string ProgramType { get; set; }
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
	   
		public static void Map(this Aca_GradingPolicy entity, Aca_GradingPolicyJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.Name = entity.Name ; 
			 toJson.Description = entity.Description ; 
			 toJson.IsDeleted = entity.IsDeleted ; 
			 toJson.IsActive = entity.IsActive ; 
			 toJson.ProgramTypeEnumId = entity.ProgramTypeEnumId ; 
             if(entity.ProgramTypeEnumId!=null)
             toJson.ProgramType = entity.ProgramType.ToString().AddSpacesToSentence();
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Aca_GradingPolicyJson json, Aca_GradingPolicy toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
                 toEntity.Description = json.Description?.Trim() ?? json.Description;
    			 toEntity.IsDeleted=json.IsDeleted;
    			 toEntity.IsActive=json.IsActive;
    			 toEntity.ProgramTypeEnumId=json.ProgramTypeEnumId;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Aca_GradingPolicy> entityList, IList<Aca_GradingPolicyJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Aca_GradingPolicyJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Aca_GradingPolicyJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Aca_GradingPolicyJson> jsonList, ICollection<Aca_GradingPolicy> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Aca_GradingPolicy obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Aca_GradingPolicy.GetNew();
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

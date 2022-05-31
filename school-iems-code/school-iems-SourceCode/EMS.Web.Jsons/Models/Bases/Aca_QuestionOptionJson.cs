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
	public partial class Aca_QuestionOptionJson 
	{
          public Int32 Id { get; set; }
          public Int32 QuestionBankId { get; set; }
          public String Name { get; set; }
          public String OptionImagePath { get; set; }
          public Boolean IsCorrectAnswer { get; set; }
          public Int32 OrderId { get; set; }
          public DateTime CreateDate { get; set; }
          public Int32 CreateById { get; set; }
          public Nullable<DateTime> LastChanged { get; set; }
          public Nullable<Int32> LastChangedById { get; set; }
          public Boolean IsActive { get; set; }
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
	   
		public static void Map(this Aca_QuestionOption entity, Aca_QuestionOptionJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.QuestionBankId = entity.QuestionBankId ; 
			 toJson.Name = entity.Name ; 
			 toJson.OptionImagePath = entity.OptionImagePath ; 
			 toJson.IsCorrectAnswer = entity.IsCorrectAnswer ; 
			 toJson.OrderId = entity.OrderId ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById ; 
			 toJson.IsActive = entity.IsActive ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Aca_QuestionOptionJson json, Aca_QuestionOption toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
    			 toEntity.QuestionBankId=json.QuestionBankId;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
                 toEntity.OptionImagePath = json.OptionImagePath?.Trim() ?? json.OptionImagePath;
    			 toEntity.IsCorrectAnswer=json.IsCorrectAnswer;
    			 toEntity.OrderId=json.OrderId;
    			 toEntity.CreateDate=json.CreateDate;
    			 toEntity.CreateById=json.CreateById;
    			 toEntity.LastChanged=json.LastChanged;
    			 toEntity.LastChangedById=json.LastChangedById;
    			 toEntity.IsActive=json.IsActive;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Aca_QuestionOption> entityList, IList<Aca_QuestionOptionJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Aca_QuestionOptionJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Aca_QuestionOptionJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Aca_QuestionOptionJson> jsonList, ICollection<Aca_QuestionOption> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Aca_QuestionOption obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Aca_QuestionOption.GetNew();
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

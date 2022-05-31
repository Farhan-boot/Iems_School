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
	public partial class Aca_GradingPolicyOptionJson 
	{
          public String Id { get; set; }
          public String Grade { get; set; }
          public Single GradePoint { get; set; }
          public String Description { get; set; }
          public Int32 GradingPolicyId { get; set; }
          public Single ScoreStartLimit { get; set; }
          public Nullable<Single> ScoreEndLimit { get; set; }
          public Byte LimitOperatorEnumId { get; set; }
          public string LimitOperator { get; set; }
          public Int32 SerNo { get; set; }
          public Boolean IsCountCredit { get; set; }
          public Boolean IsCountGPA { get; set; }
          public Boolean IsDisplayScore { get; set; }
          public Boolean IsIncomplete { get; set; }
          public Boolean IsWithdrawn { get; set; }
          public Boolean IsContinuation { get; set; }
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
	   
		public static void Map(this Aca_GradingPolicyOption entity, Aca_GradingPolicyOptionJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.Grade = entity.Grade ; 
			 toJson.GradePoint = entity.GradePoint ; 
			 toJson.Description = entity.Description ; 
			 toJson.GradingPolicyId = entity.GradingPolicyId ; 
			 toJson.ScoreStartLimit = entity.ScoreStartLimit ; 
			 toJson.ScoreEndLimit = entity.ScoreEndLimit ; 
			 toJson.LimitOperatorEnumId = entity.LimitOperatorEnumId ; 
             if(entity.LimitOperatorEnumId!=null)
             toJson.LimitOperator = entity.LimitOperator.ToString().AddSpacesToSentence();
			 toJson.SerNo = entity.SerNo ; 
			 toJson.IsCountCredit = entity.IsCountCredit ; 
			 toJson.IsCountGPA = entity.IsCountGPA ; 
			 toJson.IsDisplayScore = entity.IsDisplayScore ; 
			 toJson.IsIncomplete = entity.IsIncomplete ; 
			 toJson.IsWithdrawn = entity.IsWithdrawn ; 
			 toJson.IsContinuation = entity.IsContinuation ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Aca_GradingPolicyOptionJson json, Aca_GradingPolicyOption toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
                 toEntity.Grade = json.Grade?.Trim() ?? json.Grade;
    			 toEntity.GradePoint=json.GradePoint;
                 toEntity.Description = json.Description?.Trim() ?? json.Description;
    			 toEntity.GradingPolicyId=json.GradingPolicyId;
    			 toEntity.ScoreStartLimit=json.ScoreStartLimit;
    			 toEntity.ScoreEndLimit=json.ScoreEndLimit;
    			 toEntity.LimitOperatorEnumId=json.LimitOperatorEnumId;
    			 toEntity.SerNo=json.SerNo;
    			 toEntity.IsCountCredit=json.IsCountCredit;
    			 toEntity.IsCountGPA=json.IsCountGPA;
    			 toEntity.IsDisplayScore=json.IsDisplayScore;
    			 toEntity.IsIncomplete=json.IsIncomplete;
    			 toEntity.IsWithdrawn=json.IsWithdrawn;
    			 toEntity.IsContinuation=json.IsContinuation;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Aca_GradingPolicyOption> entityList, IList<Aca_GradingPolicyOptionJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new Aca_GradingPolicyOptionJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Aca_GradingPolicyOptionJson> jsonList, ICollection<Aca_GradingPolicyOption> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                Aca_GradingPolicyOption obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = Aca_GradingPolicyOption.GetNew();
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

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
	public partial class Aca_MarkDistributionPolicyComponentJson 
	{
          public String Id { get; set; }
          public String Name { get; set; }
          public String PolicyId { get; set; }
          public Byte TestTypeEnumId { get; set; }
          public string TestType { get; set; }
          public Single DefaultTotalMark { get; set; }
          public Single ConvertPercentage { get; set; }
          public Boolean IsFixedPolicy { get; set; }
          public Int32 BestCount { get; set; }
          public String Remark { get; set; }
          public Boolean IsDeleted { get; set; }
          public Int32 SerNo { get; set; }
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
	   
		public static void Map(this Aca_MarkDistributionPolicyComponent entity, Aca_MarkDistributionPolicyComponentJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.Name = entity.Name ; 
			 toJson.PolicyId = entity.PolicyId.ToString() ; 
			 toJson.TestTypeEnumId = entity.TestTypeEnumId ; 
             if(entity.TestTypeEnumId!=null)
             toJson.TestType = entity.TestType.ToString().AddSpacesToSentence();
			 toJson.DefaultTotalMark = entity.DefaultTotalMark ; 
			 toJson.ConvertPercentage = entity.ConvertPercentage ; 
			 toJson.IsFixedPolicy = entity.IsFixedPolicy ; 
			 toJson.BestCount = entity.BestCount ; 
			 toJson.Remark = entity.Remark ; 
			 toJson.IsDeleted = entity.IsDeleted ; 
			 toJson.SerNo = entity.SerNo ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Aca_MarkDistributionPolicyComponentJson json, Aca_MarkDistributionPolicyComponent toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
                 toEntity.PolicyId = long.TryParse(json.PolicyId, out output) ? output : 0;
    			 toEntity.TestTypeEnumId=json.TestTypeEnumId;
    			 toEntity.DefaultTotalMark=json.DefaultTotalMark;
    			 toEntity.ConvertPercentage=json.ConvertPercentage;
    			 toEntity.IsFixedPolicy=json.IsFixedPolicy;
    			 toEntity.BestCount=json.BestCount;
                 toEntity.Remark = json.Remark?.Trim() ?? json.Remark;
    			 toEntity.IsDeleted=json.IsDeleted;
    			 toEntity.SerNo=json.SerNo;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Aca_MarkDistributionPolicyComponent> entityList, IList<Aca_MarkDistributionPolicyComponentJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new Aca_MarkDistributionPolicyComponentJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Aca_MarkDistributionPolicyComponentJson> jsonList, ICollection<Aca_MarkDistributionPolicyComponent> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                Aca_MarkDistributionPolicyComponent obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = Aca_MarkDistributionPolicyComponent.GetNew();
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

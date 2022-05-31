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
	public partial class Aca_MarkDistributionPolicyJson 
	{
          public String Id { get; set; }
          public String Name { get; set; }
          public Single CreditHour { get; set; }
          public Single TotalMark { get; set; }
          public Byte CourseCategoryEnumId { get; set; }
          public string CourseCategory { get; set; }
          public Single ConvertPercentage { get; set; }
          public Byte ExamTypeEnumId { get; set; }
          public string ExamType { get; set; }
          public Boolean IsFixedPolicy { get; set; }
          public Byte StatusEnumId { get; set; }
          public string Status { get; set; }
          public String Remark { get; set; }
          public DateTime CreateDate { get; set; }
          public String CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public String LastChangedById { get; set; }
          public Boolean IsDeleted { get; set; }
          public Nullable<Int32> ProgramId { get; set; }
          public String StartSemesterId { get; set; }
          public String EndSemesterId { get; set; }
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
	   
		public static void Map(this Aca_MarkDistributionPolicy entity, Aca_MarkDistributionPolicyJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.Name = entity.Name ; 
			 toJson.CreditHour = entity.CreditHour ; 
			 toJson.TotalMark = entity.TotalMark ; 
			 toJson.CourseCategoryEnumId = entity.CourseCategoryEnumId ; 
             if(entity.CourseCategoryEnumId!=null)
             toJson.CourseCategory = entity.CourseCategory.ToString().AddSpacesToSentence();
			 toJson.ConvertPercentage = entity.ConvertPercentage ; 
			 toJson.ExamTypeEnumId = entity.ExamTypeEnumId ; 
             if(entity.ExamTypeEnumId!=null)
             toJson.ExamType = entity.ExamType.ToString().AddSpacesToSentence();
			 toJson.IsFixedPolicy = entity.IsFixedPolicy ; 
			 toJson.StatusEnumId = entity.StatusEnumId ; 
             if(entity.StatusEnumId!=null)
             toJson.Status = entity.Status.ToString().AddSpacesToSentence();
			 toJson.Remark = entity.Remark ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 
			 toJson.IsDeleted = entity.IsDeleted ; 
			 toJson.ProgramId = entity.ProgramId ; 
			 toJson.StartSemesterId = entity.StartSemesterId.ToString() ; 
             if(entity.EndSemesterId!=null)
			 toJson.EndSemesterId = entity.EndSemesterId.ToString() ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Aca_MarkDistributionPolicyJson json, Aca_MarkDistributionPolicy toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
    			 toEntity.CreditHour=json.CreditHour;
    			 toEntity.TotalMark=json.TotalMark;
    			 toEntity.CourseCategoryEnumId=json.CourseCategoryEnumId;
    			 toEntity.ConvertPercentage=json.ConvertPercentage;
    			 toEntity.ExamTypeEnumId=json.ExamTypeEnumId;
    			 toEntity.IsFixedPolicy=json.IsFixedPolicy;
    			 toEntity.StatusEnumId=json.StatusEnumId;
                 toEntity.Remark = json.Remark?.Trim() ?? json.Remark;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;
    			 toEntity.IsDeleted=json.IsDeleted;
    			 toEntity.ProgramId=json.ProgramId;
                 toEntity.StartSemesterId = long.TryParse(json.StartSemesterId, out output) ? output : 0;
                 toEntity.EndSemesterId = long.TryParse(json.EndSemesterId, out output) ? output : (Int64?)null;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Aca_MarkDistributionPolicy> entityList, IList<Aca_MarkDistributionPolicyJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new Aca_MarkDistributionPolicyJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Aca_MarkDistributionPolicyJson> jsonList, ICollection<Aca_MarkDistributionPolicy> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                Aca_MarkDistributionPolicy obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = Aca_MarkDistributionPolicy.GetNew();
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

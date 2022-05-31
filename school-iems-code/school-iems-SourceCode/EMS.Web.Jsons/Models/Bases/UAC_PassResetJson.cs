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
	public partial class UAC_PassResetJson 
	{
          public String Id { get; set; }
          public Int32 UserId { get; set; }
          public DateTime RequestDate { get; set; }
          public String RequestById { get; set; }
          public Int32 Code { get; set; }
          public Byte ValidHours { get; set; }
          public Byte TypeEnumId { get; set; }
          public string Type { get; set; }
          public Byte StatusEnumId { get; set; }
          public string Status { get; set; }
          public Nullable<DateTime> ResetDate { get; set; }
          public String ResetById { get; set; }
          public String FromIp { get; set; }
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
	   
		public static void Map(this UAC_PassReset entity, UAC_PassResetJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.UserId = entity.UserId ; 
			 toJson.RequestDate = entity.RequestDate ; 
			 toJson.RequestById = entity.RequestById.ToString() ; 
			 toJson.Code = entity.Code ; 
			 toJson.ValidHours = entity.ValidHours ; 
			 toJson.TypeEnumId = entity.TypeEnumId ; 
             if(entity.TypeEnumId!=null)
             toJson.Type = entity.Type.ToString().AddSpacesToSentence();
			 toJson.StatusEnumId = entity.StatusEnumId ; 
             if(entity.StatusEnumId!=null)
             toJson.Status = entity.Status.ToString().AddSpacesToSentence();
			 toJson.ResetDate = entity.ResetDate ; 
             if(entity.ResetById!=null)
			 toJson.ResetById = entity.ResetById.ToString() ; 
			 toJson.FromIp = entity.FromIp ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this UAC_PassResetJson json, UAC_PassReset toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
    			 toEntity.UserId=json.UserId;
    			 toEntity.RequestDate=json.RequestDate;
                 toEntity.RequestById = long.TryParse(json.RequestById, out output) ? output : 0;
    			 toEntity.Code=json.Code;
    			 toEntity.ValidHours=json.ValidHours;
    			 toEntity.TypeEnumId=json.TypeEnumId;
    			 toEntity.StatusEnumId=json.StatusEnumId;
    			 toEntity.ResetDate=json.ResetDate;
                 toEntity.ResetById = long.TryParse(json.ResetById, out output) ? output : (Int64?)null;
                 toEntity.FromIp = json.FromIp?.Trim() ?? json.FromIp;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<UAC_PassReset> entityList, IList<UAC_PassResetJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new UAC_PassResetJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<UAC_PassResetJson> jsonList, ICollection<UAC_PassReset> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                UAC_PassReset obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = UAC_PassReset.GetNew();
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

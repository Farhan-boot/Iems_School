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
	public partial class UAC_VerificationAuditJson 
	{
          public Int32 Id { get; set; }
          public Int32 UserId { get; set; }
          public DateTime InitiatedDate { get; set; }
          public Int32 InitiatedById { get; set; }
          public Int32 Code { get; set; }
          public Byte ValidHours { get; set; }
          public String NewDeviceName { get; set; }
          public Byte RequestByEnumId { get; set; }
          public string RequestBy { get; set; }
          public Byte RequestTypeEnumId { get; set; }
          public string RequestType { get; set; }
          public Byte StatusEnumId { get; set; }
          public string Status { get; set; }
          public Nullable<DateTime> ConfirmedDate { get; set; }
          public Nullable<Int32> ConfirmedById { get; set; }
          public String FromIp { get; set; }
          public String Remark { get; set; }
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
	   
		public static void Map(this UAC_VerificationAudit entity, UAC_VerificationAuditJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.UserId = entity.UserId ; 
			 toJson.InitiatedDate = entity.InitiatedDate ; 
			 toJson.InitiatedById = entity.InitiatedById ; 
			 toJson.Code = entity.Code ; 
			 toJson.ValidHours = entity.ValidHours ; 
			 toJson.NewDeviceName = entity.NewDeviceName ; 
			 toJson.RequestByEnumId = entity.RequestByEnumId ; 
             if(entity.RequestByEnumId!=null)
             toJson.RequestBy = entity.RequestBy.ToString().AddSpacesToSentence();
			 toJson.RequestTypeEnumId = entity.RequestTypeEnumId ; 
             if(entity.RequestTypeEnumId!=null)
             toJson.RequestType = entity.RequestType.ToString().AddSpacesToSentence();
			 toJson.StatusEnumId = entity.StatusEnumId ; 
             if(entity.StatusEnumId!=null)
             toJson.Status = entity.Status.ToString().AddSpacesToSentence();
			 toJson.ConfirmedDate = entity.ConfirmedDate ; 
			 toJson.ConfirmedById = entity.ConfirmedById ; 
			 toJson.FromIp = entity.FromIp ; 
			 toJson.Remark = entity.Remark ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this UAC_VerificationAuditJson json, UAC_VerificationAudit toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
    			 toEntity.UserId=json.UserId;
    			 toEntity.InitiatedDate=json.InitiatedDate;
    			 toEntity.InitiatedById=json.InitiatedById;
    			 toEntity.Code=json.Code;
    			 toEntity.ValidHours=json.ValidHours;
                 toEntity.NewDeviceName = json.NewDeviceName?.Trim() ?? json.NewDeviceName;
    			 toEntity.RequestByEnumId=json.RequestByEnumId;
    			 toEntity.RequestTypeEnumId=json.RequestTypeEnumId;
    			 toEntity.StatusEnumId=json.StatusEnumId;
    			 toEntity.ConfirmedDate=json.ConfirmedDate;
    			 toEntity.ConfirmedById=json.ConfirmedById;
                 toEntity.FromIp = json.FromIp?.Trim() ?? json.FromIp;
                 toEntity.Remark = json.Remark?.Trim() ?? json.Remark;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<UAC_VerificationAudit> entityList, IList<UAC_VerificationAuditJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 UAC_VerificationAuditJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new UAC_VerificationAuditJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<UAC_VerificationAuditJson> jsonList, ICollection<UAC_VerificationAudit> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    UAC_VerificationAudit obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = UAC_VerificationAudit.GetNew();
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

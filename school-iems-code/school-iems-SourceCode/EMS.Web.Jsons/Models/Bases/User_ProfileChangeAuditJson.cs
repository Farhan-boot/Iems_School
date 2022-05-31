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
	public partial class User_ProfileChangeAuditJson 
	{
          public Int32 Id { get; set; }
          public Int32 UserId { get; set; }
          public Byte FieldEnumId { get; set; }
          public string Field { get; set; }
          public String OldPk { get; set; }
          public String NewPk { get; set; }
          public String OldValue { get; set; }
          public String NewValue { get; set; }
          public String Remark { get; set; }
          public DateTime CreateDate { get; set; }
          public String CreateById { get; set; }
          public Nullable<Int32> NewUserId { get; set; }
          public String SemesterId { get; set; }
          public String ChangeByIpAddress { get; set; }
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
	   
		public static void Map(this User_ProfileChangeAudit entity, User_ProfileChangeAuditJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.UserId = entity.UserId ; 
			 toJson.FieldEnumId = entity.FieldEnumId ; 
             if(entity.FieldEnumId!=null)
             toJson.Field = entity.Field.ToString().AddSpacesToSentence();
			 toJson.OldPk = entity.OldPk ; 
			 toJson.NewPk = entity.NewPk ; 
			 toJson.OldValue = entity.OldValue ; 
			 toJson.NewValue = entity.NewValue ; 
			 toJson.Remark = entity.Remark ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.NewUserId = entity.NewUserId ; 
             if(entity.SemesterId!=null)
			 toJson.SemesterId = entity.SemesterId.ToString() ; 
			 toJson.ChangeByIpAddress = entity.ChangeByIpAddress ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this User_ProfileChangeAuditJson json, User_ProfileChangeAudit toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
    			 toEntity.UserId=json.UserId;
    			 toEntity.FieldEnumId=json.FieldEnumId;
                 toEntity.OldPk = json.OldPk?.Trim() ?? json.OldPk;
                 toEntity.NewPk = json.NewPk?.Trim() ?? json.NewPk;
                 toEntity.OldValue = json.OldValue?.Trim() ?? json.OldValue;
                 toEntity.NewValue = json.NewValue?.Trim() ?? json.NewValue;
                 toEntity.Remark = json.Remark?.Trim() ?? json.Remark;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.NewUserId=json.NewUserId;
                 toEntity.SemesterId = long.TryParse(json.SemesterId, out output) ? output : (Int64?)null;
                 toEntity.ChangeByIpAddress = json.ChangeByIpAddress?.Trim() ?? json.ChangeByIpAddress;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<User_ProfileChangeAudit> entityList, IList<User_ProfileChangeAuditJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 User_ProfileChangeAuditJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new User_ProfileChangeAuditJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<User_ProfileChangeAuditJson> jsonList, ICollection<User_ProfileChangeAudit> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    User_ProfileChangeAudit obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = User_ProfileChangeAudit.GetNew();
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

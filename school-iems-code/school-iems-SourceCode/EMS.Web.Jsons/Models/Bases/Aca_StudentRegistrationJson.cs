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
	public partial class Aca_StudentRegistrationJson 
	{
          public Int32 Id { get; set; }
          public String SemesterId { get; set; }
          public Int32 LevelId { get; set; }
          public Int32 StudentId { get; set; }
          public String RegistrationNo { get; set; }
          public String IPAddress { get; set; }
          public String HostAddress { get; set; }
          public Byte RegStatusEnumId { get; set; }
          public string RegStatus { get; set; }
          public Int32 FeeCodeId { get; set; }
          public Int32 BankId { get; set; }
          public String Remark { get; set; }
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
	   
		public static void Map(this Aca_StudentRegistration entity, Aca_StudentRegistrationJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.SemesterId = entity.SemesterId.ToString() ; 
			 toJson.LevelId = entity.LevelId ; 
			 toJson.StudentId = entity.StudentId ; 
			 toJson.RegistrationNo = entity.RegistrationNo ; 
			 toJson.IPAddress = entity.IPAddress ; 
			 toJson.HostAddress = entity.HostAddress ; 
			 toJson.RegStatusEnumId = entity.RegStatusEnumId ; 
             if(entity.RegStatusEnumId!=null)
             toJson.RegStatus = entity.RegStatus.ToString().AddSpacesToSentence();
			 toJson.FeeCodeId = entity.FeeCodeId ; 
			 toJson.BankId = entity.BankId ; 
			 toJson.Remark = entity.Remark ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Aca_StudentRegistrationJson json, Aca_StudentRegistration toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.SemesterId = long.TryParse(json.SemesterId, out output) ? output : 0;
    			 toEntity.LevelId=json.LevelId;
    			 toEntity.StudentId=json.StudentId;
                 toEntity.RegistrationNo = json.RegistrationNo?.Trim() ?? json.RegistrationNo;
                 toEntity.IPAddress = json.IPAddress?.Trim() ?? json.IPAddress;
                 toEntity.HostAddress = json.HostAddress?.Trim() ?? json.HostAddress;
    			 toEntity.RegStatusEnumId=json.RegStatusEnumId;
    			 toEntity.FeeCodeId=json.FeeCodeId;
    			 toEntity.BankId=json.BankId;
                 toEntity.Remark = json.Remark?.Trim() ?? json.Remark;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Aca_StudentRegistration> entityList, IList<Aca_StudentRegistrationJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Aca_StudentRegistrationJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Aca_StudentRegistrationJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Aca_StudentRegistrationJson> jsonList, ICollection<Aca_StudentRegistration> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Aca_StudentRegistration obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Aca_StudentRegistration.GetNew();
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

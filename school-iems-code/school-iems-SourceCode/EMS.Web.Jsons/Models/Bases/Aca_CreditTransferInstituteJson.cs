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
	public partial class Aca_CreditTransferInstituteJson 
	{
          public Int32 Id { get; set; }
          public String Name { get; set; }
          public Boolean IsDisabled { get; set; }
          public DateTime CreateDate { get; set; }
          public Int32 CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public Int32 LastChangedById { get; set; }
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
	   
		public static void Map(this Aca_CreditTransferInstitute entity, Aca_CreditTransferInstituteJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.Name = entity.Name ; 
			 toJson.IsDisabled = entity.IsDisabled ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Aca_CreditTransferInstituteJson json, Aca_CreditTransferInstitute toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
    			 toEntity.IsDisabled=json.IsDisabled;
    			 toEntity.CreateDate=json.CreateDate;
    			 toEntity.CreateById=json.CreateById;
    			 toEntity.LastChanged=json.LastChanged;
    			 toEntity.LastChangedById=json.LastChangedById;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Aca_CreditTransferInstitute> entityList, IList<Aca_CreditTransferInstituteJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Aca_CreditTransferInstituteJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Aca_CreditTransferInstituteJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Aca_CreditTransferInstituteJson> jsonList, ICollection<Aca_CreditTransferInstitute> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Aca_CreditTransferInstitute obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Aca_CreditTransferInstitute.GetNew();
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

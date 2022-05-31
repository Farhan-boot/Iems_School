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
	public partial class Lib_SupplierJson 
	{
          public String Id { get; set; }
          public String SupplierName { get; set; }
          public String FullAddress { get; set; }
          public String Phone { get; set; }
          public String Fax { get; set; }
          public String Email { get; set; }
          public String URL { get; set; }
          public String ContactPersonName { get; set; }
          public String ContactPersonPhone { get; set; }
          public Byte SupplierTypeEnumId { get; set; }
          public string SupplierType { get; set; }
          public Byte SupplierStatusEnumId { get; set; }
          public string SupplierStatus { get; set; }
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
	   
		public static void Map(this Lib_Supplier entity, Lib_SupplierJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.SupplierName = entity.SupplierName ; 
			 toJson.FullAddress = entity.FullAddress ; 
			 toJson.Phone = entity.Phone ; 
			 toJson.Fax = entity.Fax ; 
			 toJson.Email = entity.Email ; 
			 toJson.URL = entity.URL ; 
			 toJson.ContactPersonName = entity.ContactPersonName ; 
			 toJson.ContactPersonPhone = entity.ContactPersonPhone ; 
			 toJson.SupplierTypeEnumId = entity.SupplierTypeEnumId ; 
             if(entity.SupplierTypeEnumId!=null)
             toJson.SupplierType = entity.SupplierType.ToString().AddSpacesToSentence();
			 toJson.SupplierStatusEnumId = entity.SupplierStatusEnumId ; 
             if(entity.SupplierStatusEnumId!=null)
             toJson.SupplierStatus = entity.SupplierStatus.ToString().AddSpacesToSentence();
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Lib_SupplierJson json, Lib_Supplier toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
                 toEntity.SupplierName = json.SupplierName?.Trim() ?? json.SupplierName;
                 toEntity.FullAddress = json.FullAddress?.Trim() ?? json.FullAddress;
                 toEntity.Phone = json.Phone?.Trim() ?? json.Phone;
                 toEntity.Fax = json.Fax?.Trim() ?? json.Fax;
                 toEntity.Email = json.Email?.Trim() ?? json.Email;
                 toEntity.URL = json.URL?.Trim() ?? json.URL;
                 toEntity.ContactPersonName = json.ContactPersonName?.Trim() ?? json.ContactPersonName;
                 toEntity.ContactPersonPhone = json.ContactPersonPhone?.Trim() ?? json.ContactPersonPhone;
    			 toEntity.SupplierTypeEnumId=json.SupplierTypeEnumId;
    			 toEntity.SupplierStatusEnumId=json.SupplierStatusEnumId;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Lib_Supplier> entityList, IList<Lib_SupplierJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new Lib_SupplierJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Lib_SupplierJson> jsonList, ICollection<Lib_Supplier> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                Lib_Supplier obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = Lib_Supplier.GetNew();
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

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
	public partial class Invt_SupplierJson 
	{
          public Int32 Id { get; set; }
          public String Name { get; set; }
          public String Phone { get; set; }
          public String Email { get; set; }
          public String Address { get; set; }
          public String ContactPersonName { get; set; }
          public String ContactPersonPhone { get; set; }
          public String ContactPersonEmail { get; set; }
          public String Description { get; set; }
          public DateTime CreateDate { get; set; }
          public Int32 CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public Int32 LastChangedById { get; set; }
          public Boolean IsDeleted { get; set; }
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
	   
		public static void Map(this Invt_Supplier entity, Invt_SupplierJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.Name = entity.Name ; 
			 toJson.Phone = entity.Phone ; 
			 toJson.Email = entity.Email ; 
			 toJson.Address = entity.Address ; 
			 toJson.ContactPersonName = entity.ContactPersonName ; 
			 toJson.ContactPersonPhone = entity.ContactPersonPhone ; 
			 toJson.ContactPersonEmail = entity.ContactPersonEmail ; 
			 toJson.Description = entity.Description ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById ; 
			 toJson.IsDeleted = entity.IsDeleted ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Invt_SupplierJson json, Invt_Supplier toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
                 toEntity.Phone = json.Phone?.Trim() ?? json.Phone;
                 toEntity.Email = json.Email?.Trim() ?? json.Email;
                 toEntity.Address = json.Address?.Trim() ?? json.Address;
                 toEntity.ContactPersonName = json.ContactPersonName?.Trim() ?? json.ContactPersonName;
                 toEntity.ContactPersonPhone = json.ContactPersonPhone?.Trim() ?? json.ContactPersonPhone;
                 toEntity.ContactPersonEmail = json.ContactPersonEmail?.Trim() ?? json.ContactPersonEmail;
                 toEntity.Description = json.Description?.Trim() ?? json.Description;
    			 toEntity.CreateDate=json.CreateDate;
    			 toEntity.CreateById=json.CreateById;
    			 toEntity.LastChanged=json.LastChanged;
    			 toEntity.LastChangedById=json.LastChangedById;
    			 toEntity.IsDeleted=json.IsDeleted;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Invt_Supplier> entityList, IList<Invt_SupplierJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Invt_SupplierJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Invt_SupplierJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Invt_SupplierJson> jsonList, ICollection<Invt_Supplier> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Invt_Supplier obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Invt_Supplier.GetNew();
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

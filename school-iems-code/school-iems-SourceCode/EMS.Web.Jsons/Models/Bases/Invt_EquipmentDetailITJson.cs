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
	public partial class Invt_EquipmentDetailITJson 
	{
          public Int32 Id { get; set; }
          public String Model { get; set; }
          public String Processor { get; set; }
          public Byte[] HDD { get; set; }
          public String Ram { get; set; }
          public String Motherboard { get; set; }
          public String SoundCard { get; set; }
          public String GraphicsCard { get; set; }
          public String LANCard { get; set; }
          public String Casing { get; set; }
          public String PowerSupply { get; set; }
          public String FloppyDrive { get; set; }
          public String OpticalDrive { get; set; }
          public String Other1Title { get; set; }
          public String Other1Desc { get; set; }
          public String Other2Title { get; set; }
          public String Other2Desc { get; set; }
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
	   
		public static void Map(this Invt_EquipmentDetailIT entity, Invt_EquipmentDetailITJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.Model = entity.Model ; 
			 toJson.Processor = entity.Processor ; 
			 toJson.HDD = entity.HDD ; 
			 toJson.Ram = entity.Ram ; 
			 toJson.Motherboard = entity.Motherboard ; 
			 toJson.SoundCard = entity.SoundCard ; 
			 toJson.GraphicsCard = entity.GraphicsCard ; 
			 toJson.LANCard = entity.LANCard ; 
			 toJson.Casing = entity.Casing ; 
			 toJson.PowerSupply = entity.PowerSupply ; 
			 toJson.FloppyDrive = entity.FloppyDrive ; 
			 toJson.OpticalDrive = entity.OpticalDrive ; 
			 toJson.Other1Title = entity.Other1Title ; 
			 toJson.Other1Desc = entity.Other1Desc ; 
			 toJson.Other2Title = entity.Other2Title ; 
			 toJson.Other2Desc = entity.Other2Desc ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById ; 
			 toJson.IsDeleted = entity.IsDeleted ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Invt_EquipmentDetailITJson json, Invt_EquipmentDetailIT toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.Model = json.Model?.Trim() ?? json.Model;
                 toEntity.Processor = json.Processor?.Trim() ?? json.Processor;
    			 toEntity.HDD=json.HDD;
                 toEntity.Ram = json.Ram?.Trim() ?? json.Ram;
                 toEntity.Motherboard = json.Motherboard?.Trim() ?? json.Motherboard;
                 toEntity.SoundCard = json.SoundCard?.Trim() ?? json.SoundCard;
                 toEntity.GraphicsCard = json.GraphicsCard?.Trim() ?? json.GraphicsCard;
                 toEntity.LANCard = json.LANCard?.Trim() ?? json.LANCard;
                 toEntity.Casing = json.Casing?.Trim() ?? json.Casing;
                 toEntity.PowerSupply = json.PowerSupply?.Trim() ?? json.PowerSupply;
                 toEntity.FloppyDrive = json.FloppyDrive?.Trim() ?? json.FloppyDrive;
                 toEntity.OpticalDrive = json.OpticalDrive?.Trim() ?? json.OpticalDrive;
                 toEntity.Other1Title = json.Other1Title?.Trim() ?? json.Other1Title;
                 toEntity.Other1Desc = json.Other1Desc?.Trim() ?? json.Other1Desc;
                 toEntity.Other2Title = json.Other2Title?.Trim() ?? json.Other2Title;
                 toEntity.Other2Desc = json.Other2Desc?.Trim() ?? json.Other2Desc;
    			 toEntity.CreateDate=json.CreateDate;
    			 toEntity.CreateById=json.CreateById;
    			 toEntity.LastChanged=json.LastChanged;
    			 toEntity.LastChangedById=json.LastChangedById;
    			 toEntity.IsDeleted=json.IsDeleted;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Invt_EquipmentDetailIT> entityList, IList<Invt_EquipmentDetailITJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Invt_EquipmentDetailITJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Invt_EquipmentDetailITJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Invt_EquipmentDetailITJson> jsonList, ICollection<Invt_EquipmentDetailIT> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Invt_EquipmentDetailIT obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Invt_EquipmentDetailIT.GetNew();
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

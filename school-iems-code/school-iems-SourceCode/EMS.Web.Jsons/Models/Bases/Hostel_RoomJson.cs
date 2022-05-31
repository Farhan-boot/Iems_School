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
	public partial class Hostel_RoomJson 
	{
          public String Id { get; set; }
          public String RoomNo { get; set; }
          public String Name { get; set; }
          public Int32 OrderNo { get; set; }
          public String BuildingId { get; set; }
          public Int32 FloorId { get; set; }
          public Int32 SeatCapacity { get; set; }
          public Int32 SeatOccupied { get; set; }
          public Int32 Bathroom { get; set; }
          public Int32 Veranda { get; set; }
          public Boolean IsHasAc { get; set; }
          public Single PerSeatFee { get; set; }
          public Byte TypeEnumId { get; set; }
          public string Type { get; set; }
          public Byte StatusEnumId { get; set; }
          public string Status { get; set; }
          public Single Length { get; set; }
          public Single Width { get; set; }
          public String Remark { get; set; }
          public DateTime CreateDate { get; set; }
          public String CreateById { get; set; }
          public DateTime LastChanged { get; set; }
          public String LastChangedById { get; set; }
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
	   
		public static void Map(this Hostel_Room entity, Hostel_RoomJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.RoomNo = entity.RoomNo ; 
			 toJson.Name = entity.Name ; 
			 toJson.OrderNo = entity.OrderNo ; 
			 toJson.BuildingId = entity.BuildingId.ToString() ; 
			 toJson.FloorId = entity.FloorId ; 
			 toJson.SeatCapacity = entity.SeatCapacity ; 
			 toJson.SeatOccupied = entity.SeatOccupied ; 
			 toJson.Bathroom = entity.Bathroom ; 
			 toJson.Veranda = entity.Veranda ; 
			 toJson.IsHasAc = entity.IsHasAc ; 
			 toJson.PerSeatFee = entity.PerSeatFee ; 
			 toJson.TypeEnumId = entity.TypeEnumId ; 
             if(entity.TypeEnumId!=null)
             toJson.Type = entity.Type.ToString().AddSpacesToSentence();
			 toJson.StatusEnumId = entity.StatusEnumId ; 
             if(entity.StatusEnumId!=null)
             toJson.Status = entity.Status.ToString().AddSpacesToSentence();
			 toJson.Length = entity.Length ; 
			 toJson.Width = entity.Width ; 
			 toJson.Remark = entity.Remark ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 
			 toJson.IsDeleted = entity.IsDeleted ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Hostel_RoomJson json, Hostel_Room toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
                 toEntity.RoomNo = json.RoomNo?.Trim() ?? json.RoomNo;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
    			 toEntity.OrderNo=json.OrderNo;
                 toEntity.BuildingId = long.TryParse(json.BuildingId, out output) ? output : 0;
    			 toEntity.FloorId=json.FloorId;
    			 toEntity.SeatCapacity=json.SeatCapacity;
    			 toEntity.SeatOccupied=json.SeatOccupied;
    			 toEntity.Bathroom=json.Bathroom;
    			 toEntity.Veranda=json.Veranda;
    			 toEntity.IsHasAc=json.IsHasAc;
    			 toEntity.PerSeatFee=json.PerSeatFee;
    			 toEntity.TypeEnumId=json.TypeEnumId;
    			 toEntity.StatusEnumId=json.StatusEnumId;
    			 toEntity.Length=json.Length;
    			 toEntity.Width=json.Width;
                 toEntity.Remark = json.Remark?.Trim() ?? json.Remark;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;
    			 toEntity.IsDeleted=json.IsDeleted;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Hostel_Room> entityList, IList<Hostel_RoomJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new Hostel_RoomJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Hostel_RoomJson> jsonList, ICollection<Hostel_Room> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                Hostel_Room obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = Hostel_Room.GetNew();
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

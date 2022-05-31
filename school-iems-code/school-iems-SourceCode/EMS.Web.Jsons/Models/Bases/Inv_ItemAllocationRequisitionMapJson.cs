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
	public partial class Inv_ItemAllocationRequisitionMapJson 
	{
          public Int32 Id { get; set; }
          public Nullable<Int32> ItemAllocationId { get; set; }
          public Nullable<Int32> RequisitionId { get; set; }
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
	   
		public static void Map(this Inv_ItemAllocationRequisitionMap entity, Inv_ItemAllocationRequisitionMapJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.ItemAllocationId = entity.ItemAllocationId ; 
			 toJson.RequisitionId = entity.RequisitionId ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Inv_ItemAllocationRequisitionMapJson json, Inv_ItemAllocationRequisitionMap toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
    			 toEntity.ItemAllocationId=json.ItemAllocationId;
    			 toEntity.RequisitionId=json.RequisitionId;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Inv_ItemAllocationRequisitionMap> entityList, IList<Inv_ItemAllocationRequisitionMapJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Inv_ItemAllocationRequisitionMapJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Inv_ItemAllocationRequisitionMapJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Inv_ItemAllocationRequisitionMapJson> jsonList, ICollection<Inv_ItemAllocationRequisitionMap> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Inv_ItemAllocationRequisitionMap obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Inv_ItemAllocationRequisitionMap.GetNew();
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

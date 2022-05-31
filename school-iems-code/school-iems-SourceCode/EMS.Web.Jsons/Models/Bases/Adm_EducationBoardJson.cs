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
	public partial class Adm_EducationBoardJson 
	{
          public Int32 Id { get; set; }
          public String Name { get; set; }
          public String FullName { get; set; }
          public Byte BoardTypeEnumId { get; set; }
          public string BoardType { get; set; }
          public Int32 OrderNo { get; set; }
          public Boolean IsEnable { get; set; }
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
	   
		public static void Map(this Adm_EducationBoard entity, Adm_EducationBoardJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.Name = entity.Name ; 
			 toJson.FullName = entity.FullName ; 
			 toJson.BoardTypeEnumId = entity.BoardTypeEnumId ; 
             if(entity.BoardTypeEnumId!=null)
             toJson.BoardType = entity.BoardType.ToString().AddSpacesToSentence();
			 toJson.OrderNo = entity.OrderNo ; 
			 toJson.IsEnable = entity.IsEnable ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Adm_EducationBoardJson json, Adm_EducationBoard toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
                 toEntity.FullName = json.FullName?.Trim() ?? json.FullName;
    			 toEntity.BoardTypeEnumId=json.BoardTypeEnumId;
    			 toEntity.OrderNo=json.OrderNo;
    			 toEntity.IsEnable=json.IsEnable;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Adm_EducationBoard> entityList, IList<Adm_EducationBoardJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Adm_EducationBoardJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Adm_EducationBoardJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Adm_EducationBoardJson> jsonList, ICollection<Adm_EducationBoard> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Adm_EducationBoard obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Adm_EducationBoard.GetNew();
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

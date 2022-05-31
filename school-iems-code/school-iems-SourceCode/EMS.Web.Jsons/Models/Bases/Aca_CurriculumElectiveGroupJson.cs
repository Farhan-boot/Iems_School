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
	public partial class Aca_CurriculumElectiveGroupJson 
	{
          public String Id { get; set; }
          public String Name { get; set; }
          public String ShortName { get; set; }
          public Int32 ProgramId { get; set; }
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
	   
		public static void Map(this Aca_CurriculumElectiveGroup entity, Aca_CurriculumElectiveGroupJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.Name = entity.Name ; 
			 toJson.ShortName = entity.ShortName ; 
			 toJson.ProgramId = entity.ProgramId ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Aca_CurriculumElectiveGroupJson json, Aca_CurriculumElectiveGroup toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
                 toEntity.Name = json.Name?.Trim() ?? json.Name;
                 toEntity.ShortName = json.ShortName?.Trim() ?? json.ShortName;
    			 toEntity.ProgramId=json.ProgramId;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Aca_CurriculumElectiveGroup> entityList, IList<Aca_CurriculumElectiveGroupJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new Aca_CurriculumElectiveGroupJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Aca_CurriculumElectiveGroupJson> jsonList, ICollection<Aca_CurriculumElectiveGroup> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                Aca_CurriculumElectiveGroup obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = Aca_CurriculumElectiveGroup.GetNew();
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

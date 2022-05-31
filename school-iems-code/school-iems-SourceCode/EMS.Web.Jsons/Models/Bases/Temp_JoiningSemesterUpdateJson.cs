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
	public partial class Temp_JoiningSemesterUpdateJson 
	{
          public Int32 Id { get; set; }
          public Int32 ProgramId { get; set; }
          public String ProgramSTitle { get; set; }
          public String StartAdmSemesterIdFor6M { get; set; }
          public String EndAdmSemesterIdFor6M { get; set; }
          public String StartAdmSemesterIdFor4M { get; set; }
          public String EndAdmSemesterIdFor4M { get; set; }
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
	   
		public static void Map(this Temp_JoiningSemesterUpdate entity, Temp_JoiningSemesterUpdateJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.ProgramId = entity.ProgramId ; 
			 toJson.ProgramSTitle = entity.ProgramSTitle ; 
             if(entity.StartAdmSemesterIdFor6M!=null)
			 toJson.StartAdmSemesterIdFor6M = entity.StartAdmSemesterIdFor6M.ToString() ; 
             if(entity.EndAdmSemesterIdFor6M!=null)
			 toJson.EndAdmSemesterIdFor6M = entity.EndAdmSemesterIdFor6M.ToString() ; 
             if(entity.StartAdmSemesterIdFor4M!=null)
			 toJson.StartAdmSemesterIdFor4M = entity.StartAdmSemesterIdFor4M.ToString() ; 
             if(entity.EndAdmSemesterIdFor4M!=null)
			 toJson.EndAdmSemesterIdFor4M = entity.EndAdmSemesterIdFor4M.ToString() ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Temp_JoiningSemesterUpdateJson json, Temp_JoiningSemesterUpdate toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
    			 toEntity.ProgramId=json.ProgramId;
                 toEntity.ProgramSTitle = json.ProgramSTitle?.Trim() ?? json.ProgramSTitle;
                 toEntity.StartAdmSemesterIdFor6M = long.TryParse(json.StartAdmSemesterIdFor6M, out output) ? output : (Int64?)null;
                 toEntity.EndAdmSemesterIdFor6M = long.TryParse(json.EndAdmSemesterIdFor6M, out output) ? output : (Int64?)null;
                 toEntity.StartAdmSemesterIdFor4M = long.TryParse(json.StartAdmSemesterIdFor4M, out output) ? output : (Int64?)null;
                 toEntity.EndAdmSemesterIdFor4M = long.TryParse(json.EndAdmSemesterIdFor4M, out output) ? output : (Int64?)null;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Temp_JoiningSemesterUpdate> entityList, IList<Temp_JoiningSemesterUpdateJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Temp_JoiningSemesterUpdateJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Temp_JoiningSemesterUpdateJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Temp_JoiningSemesterUpdateJson> jsonList, ICollection<Temp_JoiningSemesterUpdate> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Temp_JoiningSemesterUpdate obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Temp_JoiningSemesterUpdate.GetNew();
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

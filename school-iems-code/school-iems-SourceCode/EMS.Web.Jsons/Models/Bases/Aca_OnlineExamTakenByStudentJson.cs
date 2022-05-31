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
	public partial class Aca_OnlineExamTakenByStudentJson 
	{
          public Int32 Id { get; set; }
          public Int32 StudentId { get; set; }
          public Int32 OnlineExamId { get; set; }
          public Single ObtainedTotalMark { get; set; }
          public Byte AttendanceTypeEnumId { get; set; }
          public string AttendanceType { get; set; }
          public Boolean IsResultPublished { get; set; }
          public String Remark { get; set; }
          public String History { get; set; }
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
	   
		public static void Map(this Aca_OnlineExamTakenByStudent entity, Aca_OnlineExamTakenByStudentJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id;

			 toJson.StudentId = entity.StudentId ; 
			 toJson.OnlineExamId = entity.OnlineExamId ; 
			 toJson.ObtainedTotalMark = entity.ObtainedTotalMark ; 
			 toJson.AttendanceTypeEnumId = entity.AttendanceTypeEnumId ; 
             if(entity.AttendanceTypeEnumId!=null)
             toJson.AttendanceType = entity.AttendanceType.ToString().AddSpacesToSentence();
			 toJson.IsResultPublished = entity.IsResultPublished ; 
			 toJson.Remark = entity.Remark ; 
			 toJson.History = entity.History ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Aca_OnlineExamTakenByStudentJson json, Aca_OnlineExamTakenByStudent toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
    			 toEntity.Id=json.Id;
    			 toEntity.StudentId=json.StudentId;
    			 toEntity.OnlineExamId=json.OnlineExamId;
    			 toEntity.ObtainedTotalMark=json.ObtainedTotalMark;
    			 toEntity.AttendanceTypeEnumId=json.AttendanceTypeEnumId;
    			 toEntity.IsResultPublished=json.IsResultPublished;
                 toEntity.Remark = json.Remark?.Trim() ?? json.Remark;
                 toEntity.History = json.History?.Trim() ?? json.History;
    			 toEntity.CreateDate=json.CreateDate;
    			 toEntity.CreateById=json.CreateById;
    			 toEntity.LastChanged=json.LastChanged;
    			 toEntity.LastChangedById=json.LastChangedById;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Aca_OnlineExamTakenByStudent> entityList, IList<Aca_OnlineExamTakenByStudentJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
				 Aca_OnlineExamTakenByStudentJson obj = null; //toJsonList.FirstOrDefault(j => j.Id == entity.Id);
                if (obj == null)
                {
                    obj = new Aca_OnlineExamTakenByStudentJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Aca_OnlineExamTakenByStudentJson> jsonList, ICollection<Aca_OnlineExamTakenByStudent> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
			    Aca_OnlineExamTakenByStudent obj = null;// toEntityList.FirstOrDefault(j => j.Id == json.Id);
                if (obj == null)
                {
                    obj = Aca_OnlineExamTakenByStudent.GetNew();
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

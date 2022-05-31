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
	public partial class Aca_ClassWaiverStudentJson 
	{
          public String Id { get; set; }
          public Int32 StudentId { get; set; }
          public String ClassId { get; set; }
          public String PreviousClassId { get; set; }
          public Single Mark { get; set; }
          public String Grade { get; set; }
          public Single GradePoint { get; set; }
          public String Remarks { get; set; }
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
	   
		public static void Map(this Aca_ClassWaiverStudent entity, Aca_ClassWaiverStudentJson toJson)
        {
            if (entity == null || toJson == null)
                return;

			 toJson.Id = entity.Id.ToString();

			 toJson.StudentId = entity.StudentId ; 
			 toJson.ClassId = entity.ClassId.ToString() ; 
             if(entity.PreviousClassId!=null)
			 toJson.PreviousClassId = entity.PreviousClassId.ToString() ; 
			 toJson.Mark = entity.Mark ; 
			 toJson.Grade = entity.Grade ; 
			 toJson.GradePoint = entity.GradePoint ; 
			 toJson.Remarks = entity.Remarks ; 
			 toJson.CreateDate = entity.CreateDate ; 
			 toJson.CreateById = entity.CreateById.ToString() ; 
			 toJson.LastChanged = entity.LastChanged ; 
			 toJson.LastChangedById = entity.LastChangedById.ToString() ; 

			MapExtra( entity,  toJson);
        }
		
		public static void Map(this Aca_ClassWaiverStudentJson json, Aca_ClassWaiverStudent toEntity)
        {
            if (toEntity == null || json == null)
                return;
                long output;
                 toEntity.Id = long.TryParse(json.Id, out output) ? output : 0;
    			 toEntity.StudentId=json.StudentId;
                 toEntity.ClassId = long.TryParse(json.ClassId, out output) ? output : 0;
                 toEntity.PreviousClassId = long.TryParse(json.PreviousClassId, out output) ? output : (Int64?)null;
    			 toEntity.Mark=json.Mark;
                 toEntity.Grade = json.Grade?.Trim() ?? json.Grade;
    			 toEntity.GradePoint=json.GradePoint;
                 toEntity.Remarks = json.Remarks?.Trim() ?? json.Remarks;
    			 toEntity.CreateDate=json.CreateDate;
                 toEntity.CreateById = long.TryParse(json.CreateById, out output) ? output : 0;
    			 toEntity.LastChanged=json.LastChanged;
                 toEntity.LastChangedById = long.TryParse(json.LastChangedById, out output) ? output : 0;

                 MapExtra(json, toEntity);
        }
		
		public static void Map(this ICollection<Aca_ClassWaiverStudent> entityList, IList<Aca_ClassWaiverStudentJson> toJsonList)
        {
            if (entityList == null || toJsonList == null)
                return;
            foreach (var entity in entityList)
            {
                var obj = toJsonList.FirstOrDefault(j => j.Id == entity.Id.ToString());
                if (obj == null)
                {
                    obj = new Aca_ClassWaiverStudentJson();
                    entity.Map(obj);
                    toJsonList.Add(obj);
                }
                else
                {
                   entity.Map(obj);
                }
            }
        }
        public static void Map(this IList<Aca_ClassWaiverStudentJson> jsonList, ICollection<Aca_ClassWaiverStudent> toEntityList)
        {
            if (jsonList == null || toEntityList == null)
                return;

            foreach (var json in jsonList)
            {
                Aca_ClassWaiverStudent obj = toEntityList.FirstOrDefault(j => j.Id == long.Parse(json.Id));
                if (obj == null)
                {
                    obj = Aca_ClassWaiverStudent.GetNew();
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

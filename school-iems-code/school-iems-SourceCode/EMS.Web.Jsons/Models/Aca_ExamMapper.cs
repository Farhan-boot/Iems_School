//File: Json Partial Mapper
using System;
using System.Collections.Generic;
using System.Linq;

using EMS.Framework;
using EMS.DataAccess.Data;
using EMS.Web.Jsons.Models;


namespace EMS.Web.Jsons.Models
{
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public static partial class EntityMapper
	{
		private static void MapExtra(Aca_Exam entity, Aca_ExamJson toJson)
        {
		    if (entity.Aca_Semester != null)
		    {
                toJson.Aca_SemesterJson=  new Aca_SemesterJson();
                entity.Aca_Semester.Map(toJson.Aca_SemesterJson);
            }

            if (entity.ExamSupple!=null)
            {
                toJson.ExamSuppleJson=new Aca_ExamSuppleJson();
                entity.ExamSupple.Map(toJson.ExamSuppleJson);
            }
        }

        private static void MapExtra(Aca_ExamJson json, Aca_Exam toEntity)
        {
            
        }
	}
}


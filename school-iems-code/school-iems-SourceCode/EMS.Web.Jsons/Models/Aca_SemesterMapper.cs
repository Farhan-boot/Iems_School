//File: Json Partial Mapper
using System;
using System.Collections.Generic;
using System.Linq;
using EMS.CoreLibrary.Helpers;
using EMS.Framework;
using EMS.DataAccess.Data;
using EMS.Web.Jsons.Models;


namespace EMS.Web.Jsons.Models
{
    public partial class Aca_SemesterJson
    {
        public List<Aca_ClassJson> Aca_ClassListJson { get; set; }
       
        #region Custom Variables

        public string AdmExamName { get; set; }
        public int AdmExamId { get; set; }
        public string FinalTermExamName { get; set; }
        public string FinalTermExamId { get; set; }
        public bool IsCreateAdmissionExam { get; set; }
        public string StudentIdPrefix { get; set; }
        public string StudentUgcIdPrefix { get; set; }
        public int IncrementBatchCount { get; set; }
        public bool IsCreateFinalTermExam { get; set; }
        public string FinalTermExamShortName { get; set; }
        public string OfficialExamName { get; set; }

        #endregion
    }
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public static partial class EntityMapper
	{
		private static void MapExtra(Aca_Semester entity, Aca_SemesterJson toJson)
        {
            if (toJson.SemesterDuration.IsValid())
            {
                toJson.SemesterDuration.Replace("_ ", "");
            }
            
            if (entity.Aca_Class != null && entity.Aca_Class.Count > 0)
            {
                toJson.Aca_ClassListJson = new List<Aca_ClassJson>();
                entity.Aca_Class.Map(toJson.Aca_ClassListJson);
            }

            if (entity.Adm_AdmissionExam.Count == 1)
            {
                toJson.AdmExamId = entity.Adm_AdmissionExam.First().Id;
                toJson.AdmExamName = entity.Adm_AdmissionExam.First().Name;
            }

            if (entity.Aca_Exam.Count == 1)
            {
                toJson.FinalTermExamId = entity.Aca_Exam.First().Id.ToString();
                toJson.FinalTermExamName = entity.Aca_Exam.First().Name;
            }
           
        }
	    //public List<Aca_ClassJson> Aca_ClassListJson { get; set; }
        private static void MapExtra(Aca_SemesterJson json, Aca_Semester toEntity)
        {
            
            
        }
	}
}


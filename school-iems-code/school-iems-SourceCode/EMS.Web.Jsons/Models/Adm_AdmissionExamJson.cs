//File: Json Model Partial
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using EMS.CoreLibrary.Helpers;
using EMS.Framework;
using EMS.DataAccess.Data;
using EMS.Web.Jsons.Custom.Admission.AdmissionExam;
using Newtonsoft.Json;

namespace EMS.Web.Jsons.Models
{
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public partial class Adm_AdmissionExamJson
	{
	    #region Custom Variables

        public DefaultSettingsJson DefaultSettingsJsonObj { get; set; }

        public int SemesterDurationEnumId { get; set; }

        #endregion
	}
    #region Mapper
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public static partial class EntityMapper
	{
		private static void MapExtra(Adm_AdmissionExam entity, Adm_AdmissionExamJson toJson)
        {
             toJson.IsSelected = entity.IsSelected;
             toJson.IsAlreadyAdded = entity.IsAlreadyAdded;
             if (entity.Aca_Semester != null)
             {
                 toJson.SemesterDurationEnumId = entity.Aca_Semester.SemesterDurationEnumId;
             }
        }

        private static void MapExtra(Adm_AdmissionExamJson json, Adm_AdmissionExam toEntity)
        {
             toEntity.IsSelected = json.IsSelected;
             toEntity.IsAlreadyAdded = json.IsAlreadyAdded;

             if (json.DefaultSettingsJsonObj !=null)
             {
                 toEntity.IsEnableProgramWiseBatchMap = json.DefaultSettingsJsonObj.IsEnableProgramWiseBatchMap;
                 toEntity.DefaultSettingsJson = JsonConvert.SerializeObject(json.DefaultSettingsJsonObj);
             }
        }
	}
    #endregion
}

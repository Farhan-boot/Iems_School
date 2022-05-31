//File: Json Model Partial
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;

using EMS.Framework;
using EMS.DataAccess.Data;

namespace EMS.Web.Jsons.Models
{
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public partial class Aca_ExamJson
	{
        #region Custom Variables
        public Aca_SemesterJson Aca_SemesterJson { get; set; }
        public Aca_ExamSuppleJson ExamSuppleJson { get; set; }
        #endregion
    }
}

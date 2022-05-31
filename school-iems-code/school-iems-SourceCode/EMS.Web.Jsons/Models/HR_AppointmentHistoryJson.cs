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
	public partial class HR_AppointmentHistoryJson
	{
        #region Custom Variables
        public string StartDateString { get; set; }
        public string EndDateString { get; set; }

        #endregion
    }
}

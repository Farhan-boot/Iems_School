using System;
using System.Collections.Generic;
using System.Linq;

using EMS.Framework;
using EMS.DataAccess.Data;
using EMS.Web.Jsons.Models;
using Lib_BookJson = EMS.Web.Jsons.Models.Lib_BookJson;

namespace EMS.Web.Jsons.Models
{
	public partial class Lib_BookSubjectMapJson
	{
        #region Custom Variables
        public Lib_BookJson Lib_BookJson { get; set; }
        public Lib_BookSubjectJson Lib_BookSubjectJson { get; set; }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

using EMS.Framework;
using EMS.DataAccess.Data;

namespace EMS.Web.Jsons.Models
{
	public partial class Lib_BookSubjectJson
	{
        #region Custom Variables
        public IList<Lib_BookSubjectMapJson> Lib_BookSubjectMapJsonList { get; set; }
        #endregion
    }
}

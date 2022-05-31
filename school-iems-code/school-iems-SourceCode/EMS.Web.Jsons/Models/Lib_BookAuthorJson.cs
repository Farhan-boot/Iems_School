using System;
using System.Collections.Generic;
using System.Linq;

using EMS.Framework;
using EMS.DataAccess.Data;
using EMS.Web.Jsons.Models;

namespace EMS.Web.Jsons.Models
{
	public partial class Lib_BookAuthorJson
	{
        #region Custom Variables


        public IList<Lib_BookAuthorMapJson> Lib_BookAuthorMapJsonList { get; set; }
        #endregion
    }
}

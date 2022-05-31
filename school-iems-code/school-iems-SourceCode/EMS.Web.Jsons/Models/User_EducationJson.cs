using System;
using System.Collections.Generic;
using System.Linq;

using EMS.Framework;
using EMS.DataAccess.Data;

namespace EMS.Web.Jsons.Models
{
	public partial class User_EducationJson
	{
	    #region Custom Variabls
	    public bool IsDisable { get; set; } = true; //This Property has used, when add new education for DegreeEquivalentEnum change in UI part.
        public byte BoardTypeEnumId { get; set; }
	    public bool IsRemove { get; set; } = false;

	    #endregion

	}
}

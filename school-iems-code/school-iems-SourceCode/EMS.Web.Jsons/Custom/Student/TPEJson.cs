//File:Json Model Base Partial
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;

using EMS.Framework;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.Web.Jsons.Models;

namespace EMS.Web.Jsons.Custom.Student
{
    /// <summary>
    /// Do not modify this class, edit partial instead.
    /// </summary>   
	public partial class TPEJson 
	{
        //public Aca_TPEJson Aca_TPEJson { get; set; }
        //public Aca_TPECommentJson Aca_TPECommentJson { get; set; }
        public bool IsSelected { get; set; }
        public bool IsAlreadyAdded { get; set; }
	}
}

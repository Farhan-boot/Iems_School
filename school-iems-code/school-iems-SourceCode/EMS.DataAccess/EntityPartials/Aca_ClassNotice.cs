﻿//File: Entity Partial
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

using EMS.DataAccess.Data;

namespace EMS.DataAccess.Data
{
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
    public partial class Aca_ClassNotice
	{
        #region Enum declaration
        #endregion Enum Property

        public string EmployeeName { get; set; }
        private static void  GetNewExtra(Aca_ClassNotice obj)
          {
            obj.PostDate = DateTime.Now;
          }
        
	}
}

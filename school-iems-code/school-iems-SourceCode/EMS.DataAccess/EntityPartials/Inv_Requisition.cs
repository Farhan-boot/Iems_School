//File: Entity Partial
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
    public partial class Inv_Requisition
	{
          #region Enum declaration
           #endregion Enum Property

           public string RequestedByEmployeeName { get; set; }
           public string ReferencedByEmployeeName { get; set; }
           public string IssuedOrReleaseByUserName { get; set; }
           public string ApprovedByAdminName { get; set; }
           public string ReceivedByEmployeeName { get; set; }


        private static void  GetNewExtra(Inv_Requisition obj)
          {
          
          }
        
	}
}

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
    public partial class Aca_ExamSupple
	{
          #region Enum declaration
           #endregion Enum Property
          
          private static void  GetNewExtra(Aca_ExamSupple obj)
          {
              obj.MaxTimeCanApplyForOneTheory = 1;
              obj.MaxTimeCanApplyForOneNonTheory = 1;
              obj.SuppleApplicationLastDate=DateTime.Now;
              obj.SuppleApplyAllowedPaymentDueUpto = 0;
              obj.SuppleAdmitCardDownloadPaymentDueUpto = 0;
              obj.SuppleResultAllowedPaymentDueUpto = 0;

          }
        
	}
}

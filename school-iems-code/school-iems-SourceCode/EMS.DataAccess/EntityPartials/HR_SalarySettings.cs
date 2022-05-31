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
    public partial class HR_SalarySettings
	{
          #region Enum declaration
          
            [DataMember]
            public HR_SalaryTemplateDetails.SalaryTypeEnum SalaryType
            {
                get{return (HR_SalaryTemplateDetails.SalaryTypeEnum)SalaryTypeEnumId;}
                set
                {
                    SalaryTypeEnumId = (Byte)value;
                }
            }
           #endregion Enum Property
          
          private static void  GetNewExtra(HR_SalarySettings obj)
          {
                obj.EffectiveFrom = DateTime.Now;
                obj.NextPromotionDate = DateTime.Now.AddYears(1);
          }
        
	}
}

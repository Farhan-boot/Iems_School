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
    public partial class HR_SalarySettingsDetails
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
            [DataMember]
            public HR_SalaryTemplateDetails.CategoryTypeEnum CategoryType
            {
                get{return (HR_SalaryTemplateDetails.CategoryTypeEnum)CategoryTypeEnumId;}
                set
                {
                    CategoryTypeEnumId = (Byte)value;
                }
            }
           #endregion Enum Property
          
          private static void  GetNewExtra(HR_SalarySettingsDetails obj)
          {
          
          }
        
	}
}

//File: Entity Partial
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using EMS.CoreLibrary;
using EMS.DataAccess.Data;

namespace EMS.DataAccess.Data
{
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
    public partial class HR_Payroll
	{
        public DateTime PayrollMonthYear
        {
            get
            {
                return new DateTime(Year, MonthEnumId, 1);
            }
        }

        #region Enum declaration
        [DataMember]
            public EnumCollection.Common.MonthEnum Month
            {
                get{return (EnumCollection.Common.MonthEnum)MonthEnumId;}
                set
                {
                    MonthEnumId = (Byte)value;
                }
            }


           #endregion Enum Property
           
          private static void  GetNewExtra(HR_Payroll obj)
          {
          
          }
        
	}
}

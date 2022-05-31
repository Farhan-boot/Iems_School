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
    public partial class HR_SalaryTemplateDetails
	{
          #region Enum declaration
           [Flags]
           public enum SalaryTypeEnum
           {
                FixedAmount=0,
                Daily=1,
                Hourly=2,
                Weekly=3,
                Rate=4,
                WorkingValue=5,
           }
           [Flags]

           /*This is a very Important Enum. The Value here is very important.
             Here 1,2 is fixed Salary Type
             3-50 will be Allowance type,
             51-75 will be Bonus type,
             76-100 will be Adjustment type,
             101-150 will be Deduction type
           */
           public enum CategoryTypeEnum
           {
                BasicSalary=1,
                Overload=2,
                HousingAllowance = 3,
                MedicalAllowance =4,
                TransportAllowance =5,
                TelephoneAllowance=6,
                EntertainmentAllowance=7,
                OtherAllowance = 50,
                FestivalBonus = 51,
                OtherBonus = 75,
                PreviousAdjustment=76,
                OtherAdjustment=100,
                TaxDeduction=101,
                PFDeduction=102,
                AbsentDeduction=103,
                UnderTimeDeduction=104,
                RevenueStamp=105,
                OtherDeduction=150,
           }
            [DataMember]
            public SalaryTypeEnum SalaryType
            {
                get{return (SalaryTypeEnum)SalaryTypeEnumId;}
                set
                {
                    SalaryTypeEnumId = (Byte)value;
                }
            }
            [DataMember]
            public CategoryTypeEnum CategoryType
            {
                get{return (CategoryTypeEnum)CategoryTypeEnumId;}
                set
                {
                    CategoryTypeEnumId = (Byte)value;
                }
            }
           #endregion Enum Property
          
          private static void  GetNewExtra(HR_SalaryTemplateDetails obj)
          {
          
          }
        
	}
}

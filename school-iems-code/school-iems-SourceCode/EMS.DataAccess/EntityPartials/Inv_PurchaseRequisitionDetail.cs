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
    public partial class Inv_PurchaseRequisitionDetail
	{
          #region Enum declaration
           [Flags]
           public enum UnitTypeEnum
           { 
               //none = 0,
            Box = 0,
            Pkt = 1,
            Case = 2,
            Ltr = 3,
            Feet = 4,
            Set = 5,
            Meter = 6,
            Centimeter = 7,
            Sacks = 8,
            Dozen = 9,
            Roll = 10,
            Lbs = 11,
            Bundle = 12,
            Bottle = 13,
            Ream = 14,
            Gallon = 15
        }
            [DataMember]
            public UnitTypeEnum UnitType
            {
                get{return (UnitTypeEnum)UnitTypeEnumId;}
                set
                {
                    UnitTypeEnumId = (Int32)value;
                }
            }
           #endregion Enum Property
          
          private static void  GetNewExtra(Inv_PurchaseRequisitionDetail obj)
          {
          
          }
        
	}
}

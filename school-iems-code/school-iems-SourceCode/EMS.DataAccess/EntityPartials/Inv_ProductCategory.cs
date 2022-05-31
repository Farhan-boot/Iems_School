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
    public partial class Inv_ProductCategory
	{
          #region Enum declaration
           [Flags]
           public enum AssetTypeEnum
           {
            CurrentAssets = 0,
            FixedAssets = 1
           }
           [Flags]
           public enum UnitTypeEnum
           {
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
            public AssetTypeEnum? AssetType
            {
                get { return AssetTypeEnumId != null ? (AssetTypeEnum?)AssetTypeEnumId : null; }
                set
                {
                    AssetTypeEnumId = (Nullable<Int32>)value;
                }
            }
            [DataMember]
            public UnitTypeEnum? UnitType
            {
                get { return UnitTypeEnumId != null ? (UnitTypeEnum?)UnitTypeEnumId : null; }
                set
                {
                    UnitTypeEnumId = (Nullable<Int32>)value;
                }
            }
           #endregion Enum Property
          
          private static void  GetNewExtra(Inv_ProductCategory obj)
          {
          
          }
        
	}
}

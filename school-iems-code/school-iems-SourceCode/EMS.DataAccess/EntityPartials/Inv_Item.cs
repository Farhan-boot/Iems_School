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
    public partial class Inv_Item
	{
          #region Enum declaration
           [Flags]
           public enum AssetTypeEnum
           {
                none = 0,
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
           #endregion Enum Property
          
          private static void  GetNewExtra(Inv_Item obj)
          {
          
          }
        
	}
}

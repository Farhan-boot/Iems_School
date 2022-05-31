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
    public partial class Inv_InventoryDetails
	{
          #region Enum declaration
           [Flags]
           public enum StatusEnum
           {
               InUser = 0,
               InStock = 1,
               Discarded = 2,
           }
            [DataMember]
            public StatusEnum? Status
            {
                get { return StatusEnumId != null ? (StatusEnum?)StatusEnumId : null; }
                set
                {
                    StatusEnumId = (Nullable<Int32>)value;
                }
            }
           #endregion Enum Property
          
          private static void  GetNewExtra(Inv_InventoryDetails obj)
          {
          
          }
        
	}
}

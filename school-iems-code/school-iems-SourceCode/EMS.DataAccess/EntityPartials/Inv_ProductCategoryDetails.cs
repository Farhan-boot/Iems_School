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
    public partial class Inv_ProductCategoryDetails
	{
        #region Enum declaration
        #endregion Enum Property
        /*public enum UnitTypeEnum
        {
            None = 1,
            
        }*/

        [DataMember]
        public Inv_PurchaseRequisitionDetail.UnitTypeEnum UnitType
        {
            get
            {
                return (Inv_PurchaseRequisitionDetail.UnitTypeEnum)UnitTypeEnumId;
            }
            set
            {
                UnitTypeEnumId = (Byte)value;
            }
        }
        private static void  GetNewExtra(Inv_ProductCategoryDetails obj)
          {
          
          }
        
	}
}

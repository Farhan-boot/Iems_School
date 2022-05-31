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
    public partial class Invt_Item
    {
        #region Enum declaration
        [Flags]
        public enum UnitTypeEnum
        {
            none = 0,
            Kg = 1,
            Piece = 2,
            Liter = 3,
        }
        [DataMember]
        public UnitTypeEnum UnitType
        {
            get { return (UnitTypeEnum)UnitTypeEnumId; }
            set
            {
                UnitTypeEnumId = (Int32)value;
            }
        }
        #endregion Enum Property

        private static void GetNewExtra(Invt_Item obj)
        {

        }

    }
}

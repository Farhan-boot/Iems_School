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
    public partial class Aca_GradingPolicyOption
    {
        #region Enum declaration
        [Flags]
        public enum LimitOperatorEnum
        {
            Equals = 0,
            Between = 1,
            Above = 2,
            Below = 3

        }

        [DataMember]
        public LimitOperatorEnum LimitOperator
        {
            get { return (LimitOperatorEnum)LimitOperatorEnumId; }
            set
            {
                LimitOperatorEnumId = (Byte)value;
            }
        }
        #endregion Enum Property

        private static void GetNewExtra(Aca_GradingPolicyOption obj)
        {

        }

    }
}

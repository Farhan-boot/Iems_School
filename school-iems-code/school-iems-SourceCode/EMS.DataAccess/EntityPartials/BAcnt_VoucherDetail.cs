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
    public partial class BAcnt_VoucherDetail
	{
        #region Enum declaration
        /// <summary>
        /// This enum using only UI, Not attach with database
        /// </summary>
        public enum TransactionTypeEnum
        {
            Dr = 1,
            Cr = 2
        }

        #endregion Enum Property

        private static void  GetNewExtra(BAcnt_VoucherDetail obj)
          {
          
          }

        #region Constants
        public const string Property_LedgerName = "LedgerName";


        #endregion

    }
}

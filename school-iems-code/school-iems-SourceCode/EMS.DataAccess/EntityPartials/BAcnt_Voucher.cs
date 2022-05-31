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
    public partial class BAcnt_Voucher
	{

          #region Enum declaration
           [Flags]
           public enum VoucherStatusEnum
           {
                //todo need to change database bool type like IsPosted
                NotPosted = 0,
                Posted = 1
            }
           [Flags]
           public enum JournalTypeEnum
           {
                /*Currently not defined any business by this enum.
                 At now 'Voucher' type hard coded set
                 */
                Journal = 0,
                Voucher = 1,
                Invoice = 2,
                Transfer = 3,
                Adjustment = 4
            }
           
            [DataMember]
            public VoucherStatusEnum VoucherStatus
            {
                get{return (VoucherStatusEnum)VoucherStatusEnumId;}
                set
                {
                    VoucherStatusEnumId = (Byte)value;
                }
            }
            [DataMember]
            public JournalTypeEnum JournalType
            {
                get{return (JournalTypeEnum)JournalTypeEnumId;}
                set
                {
                    JournalTypeEnumId = (Byte)value;
                }
            }

        #endregion Enum Property

          private static void  GetNewExtra(BAcnt_Voucher obj)
          {
          
          }

        #region Constants
        public const string Property_VoucherTotalAmount = "VoucherTotalAmount";
        public const string Property_VoucherType = "VoucherType";
        public const string Property_OfficialBankName = "OfficialBankName";
        public const string Property_BranchName = "BranchName";
        public const string Property_CreateByName = "CreateByName";
        public const string Property_LastChangedByName = "LastChangedByName";
        #endregion
    }
}

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
    public partial class BAcnt_VoucherType
    {
        #region Enum declaration
        [Flags]
        public enum TypeEnum
        {
            Payment = 0,
            Receipt = 1,
            Contra = 2,
            Journal = 3
        }
        [Flags]
        public enum StatusEnum
        {
            Enable = 0,
            Disable = 1
        }
        [DataMember]
        public TypeEnum Type
        {
            get { return (TypeEnum)TypeEnumId; }
            set
            {
                TypeEnumId = (Byte)value;
            }
        }
        [DataMember]
        public StatusEnum Status
        {
            get { return (StatusEnum)StatusEnumId; }
            set
            {
                StatusEnumId = (Byte)value;
            }
        }
        #endregion Enum Property
        public string Type2
        {
            get
            {
                if (TypeEnumId == (byte)BAcnt_VoucherType.TypeEnum.Payment)
                {
                    return "Debit";
                }
                if (TypeEnumId == (byte)BAcnt_VoucherType.TypeEnum.Receipt)
                {
                    return "Credit";
                }
                if (TypeEnumId == (byte)BAcnt_VoucherType.TypeEnum.Journal)
                {
                    return "Journal";
                }
                if (TypeEnumId == (byte)BAcnt_VoucherType.TypeEnum.Contra)
                {
                    return "Contra";
                }

                return "";

            }
        }
        private static void GetNewExtra(BAcnt_VoucherType obj)
        {

        }

        //private string _type2;





    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EMS.DataAccess.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class BAcnt_VoucherDetail
    {
        public int Id { get; set; }
        public int LedgerId { get; set; }
        public double DebitAmount { get; set; }
        public double CreditAmount { get; set; }
        public bool IsDebited { get; set; }
        public int VoucherId { get; set; }
        public string Remark { get; set; }
        public string History { get; set; }
        public bool IsDeleted { get; set; }
        public string Narration { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateById { get; set; }
        public System.DateTime LastChanged { get; set; }
        public int LastChangedById { get; set; }
    
        public virtual BAcnt_Ledger BAcnt_Ledger { get; set; }
        public virtual BAcnt_Voucher BAcnt_Voucher { get; set; }
    }
}

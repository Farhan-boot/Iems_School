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
    
    public partial class BAcnt_VoucherType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BAcnt_VoucherType()
        {
            this.BAcnt_Voucher = new HashSet<BAcnt_Voucher>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public byte TypeEnumId { get; set; }
        public byte StatusEnumId { get; set; }
        public Nullable<int> DefaultDebitLedgerId { get; set; }
        public Nullable<int> DefaultCreditLedgerId { get; set; }
        public int BranchId { get; set; }
        public int CompanyId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateById { get; set; }
        public System.DateTime LastChanged { get; set; }
        public int LastChangedById { get; set; }
    
        public virtual BAcnt_Branch BAcnt_Branch { get; set; }
        public virtual BAcnt_CompanyAccount BAcnt_CompanyAccount { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BAcnt_Voucher> BAcnt_Voucher { get; set; }
    }
}

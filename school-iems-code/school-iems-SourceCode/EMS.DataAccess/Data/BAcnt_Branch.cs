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
    
    public partial class BAcnt_Branch
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BAcnt_Branch()
        {
            this.BAcnt_Ledger = new HashSet<BAcnt_Ledger>();
            this.BAcnt_Voucher = new HashSet<BAcnt_Voucher>();
            this.BAcnt_VoucherType = new HashSet<BAcnt_VoucherType>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public int OrderNo { get; set; }
        public bool IsEnable { get; set; }
        public bool IsDeleted { get; set; }
        public int CompanyId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateById { get; set; }
        public System.DateTime LastChanged { get; set; }
        public int LastChangedById { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BAcnt_Ledger> BAcnt_Ledger { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BAcnt_Voucher> BAcnt_Voucher { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BAcnt_VoucherType> BAcnt_VoucherType { get; set; }
    }
}

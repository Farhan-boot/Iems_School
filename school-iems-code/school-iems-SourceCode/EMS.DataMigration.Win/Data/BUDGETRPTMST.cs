//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EMS.DataMigration.Win.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class BUDGETRPTMST
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BUDGETRPTMST()
        {
            this.BUDGETRPTDTL = new HashSet<BUDGETRPTDTL>();
        }
    
        public string BGRPMID { get; set; }
        public string LGID { get; set; }
        public string NMSH { get; set; }
        public Nullable<decimal> BGMSEQ { get; set; }
        public decimal RPTTAG { get; set; }
        public Nullable<decimal> MGRP { get; set; }
        public string COMPID { get; set; }
        public string SFUSRID { get; set; }
        public string DBUSRNM { get; set; }
        public string ACTTP { get; set; }
        public System.DateTime ACTDT { get; set; }
    
        public virtual ACCCOMPANY ACCCOMPANY { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BUDGETRPTDTL> BUDGETRPTDTL { get; set; }
    }
}

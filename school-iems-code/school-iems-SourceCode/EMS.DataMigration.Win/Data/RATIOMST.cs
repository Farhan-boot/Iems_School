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
    
    public partial class RATIOMST
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RATIOMST()
        {
            this.RATIODTL = new HashSet<RATIODTL>();
        }
    
        public string RATMID { get; set; }
        public string NMSH { get; set; }
        public Nullable<decimal> RATMSEQ { get; set; }
        public decimal RPTTAG { get; set; }
        public Nullable<decimal> MGRP { get; set; }
        public string COMPID { get; set; }
        public string SFUSRID { get; set; }
        public string DBUSRNM { get; set; }
        public string ACTTP { get; set; }
        public System.DateTime ACTDT { get; set; }
    
        public virtual ACCCOMPANY ACCCOMPANY { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RATIODTL> RATIODTL { get; set; }
    }
}

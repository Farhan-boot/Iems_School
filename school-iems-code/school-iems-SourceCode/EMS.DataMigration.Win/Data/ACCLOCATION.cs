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
    
    public partial class ACCLOCATION
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ACCLOCATION()
        {
            this.ACCCHARTOFACCOUNT = new HashSet<ACCCHARTOFACCOUNT>();
            this.BUDGETMST = new HashSet<BUDGETMST>();
            this.ACCTRANMST = new HashSet<ACCTRANMST>();
        }
    
        public string LOCID { get; set; }
        public string LOCNM { get; set; }
        public string LOCSNM { get; set; }
        public string LOCBNM { get; set; }
        public string LOCBSNM { get; set; }
        public string LOCADDR { get; set; }
        public string LOCBADDR { get; set; }
        public string DEFSTS { get; set; }
        public string SFUSRID { get; set; }
        public string DBUSRNM { get; set; }
        public string ACTTP { get; set; }
        public System.DateTime ACTDT { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACCCHARTOFACCOUNT> ACCCHARTOFACCOUNT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BUDGETMST> BUDGETMST { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACCTRANMST> ACCTRANMST { get; set; }
    }
}
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
    
    public partial class ACCCOMPANY
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ACCCOMPANY()
        {
            this.ACCCHARTOFACCOUNT = new HashSet<ACCCHARTOFACCOUNT>();
            this.BUDGETRPTMST = new HashSet<BUDGETRPTMST>();
            this.BALSHEETMST = new HashSet<BALSHEETMST>();
            this.BUDGETMST = new HashSet<BUDGETMST>();
            this.CASHFLOWMST = new HashSet<CASHFLOWMST>();
            this.INCOMEMST = new HashSet<INCOMEMST>();
            this.LOANTRANS = new HashSet<LOANTRANS>();
            this.ACCMONTHCLOSEBACKUP = new HashSet<ACCMONTHCLOSEBACKUP>();
            this.ACCMONTHCLOSE = new HashSet<ACCMONTHCLOSE>();
            this.RATIOMST = new HashSet<RATIOMST>();
            this.ACCSUBLEDGER = new HashSet<ACCSUBLEDGER>();
            this.TRIALBALMST = new HashSet<TRIALBALMST>();
            this.TRIALBALONECOA = new HashSet<TRIALBALONECOA>();
            this.TRIALBALONECOAPROJ = new HashSet<TRIALBALONECOAPROJ>();
            this.ACCTRANMST = new HashSet<ACCTRANMST>();
            this.ACCYEARCLOSEBACKUP = new HashSet<ACCYEARCLOSEBACKUP>();
            this.ACCYEARCLOSE = new HashSet<ACCYEARCLOSE>();
        }
    
        public string COMPID { get; set; }
        public string COMPNM { get; set; }
        public string COMPNMBAN { get; set; }
        public string COMPSNM { get; set; }
        public string COMPSNMBAN { get; set; }
        public string COMPADDR { get; set; }
        public string COMPLOC { get; set; }
        public string COMPPH { get; set; }
        public string COMPFAX { get; set; }
        public string COMPEMAIL { get; set; }
        public string COMPWWW { get; set; }
        public byte[] COMPLOGO { get; set; }
        public string COMPBCURR { get; set; }
        public string COMPVATREGNO { get; set; }
        public string COMPCNTPSN { get; set; }
        public System.DateTime COMPFYEAR { get; set; }
        public Nullable<System.DateTime> COMPACTCLSDT { get; set; }
        public Nullable<System.DateTime> COMPESTDT { get; set; }
        public string COMPVCTAG { get; set; }
        public string COMPGPID { get; set; }
        public string DEFSTS { get; set; }
        public string SFUSRID { get; set; }
        public string DBUSRNM { get; set; }
        public string ACTTP { get; set; }
        public System.DateTime ACTDT { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACCCHARTOFACCOUNT> ACCCHARTOFACCOUNT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BUDGETRPTMST> BUDGETRPTMST { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BALSHEETMST> BALSHEETMST { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BUDGETMST> BUDGETMST { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CASHFLOWMST> CASHFLOWMST { get; set; }
        public virtual ACCCOMPANYGROUP ACCCOMPANYGROUP { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<INCOMEMST> INCOMEMST { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LOANTRANS> LOANTRANS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACCMONTHCLOSEBACKUP> ACCMONTHCLOSEBACKUP { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACCMONTHCLOSE> ACCMONTHCLOSE { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RATIOMST> RATIOMST { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACCSUBLEDGER> ACCSUBLEDGER { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TRIALBALMST> TRIALBALMST { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TRIALBALONECOA> TRIALBALONECOA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TRIALBALONECOAPROJ> TRIALBALONECOAPROJ { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACCTRANMST> ACCTRANMST { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACCYEARCLOSEBACKUP> ACCYEARCLOSEBACKUP { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ACCYEARCLOSE> ACCYEARCLOSE { get; set; }
    }
}

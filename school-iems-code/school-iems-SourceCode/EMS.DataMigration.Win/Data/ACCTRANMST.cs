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
    
    public partial class ACCTRANMST
    {
        public string TMID { get; set; }
        public System.DateTime VCDT { get; set; }
        public string VCTP { get; set; }
        public string VCNO { get; set; }
        public string VMINO { get; set; }
        public string VCMRIN { get; set; }
        public string MOP { get; set; }
        public string CHEQNO { get; set; }
        public Nullable<System.DateTime> CHEQDT { get; set; }
        public string PYRVTO { get; set; }
        public string NARRMST { get; set; }
        public string PTFLAG { get; set; }
        public string COMPID { get; set; }
        public string LOCID { get; set; }
        public string PRJID { get; set; }
        public string SFUSRID { get; set; }
        public string DBUSRNM { get; set; }
        public string ACTTP { get; set; }
        public System.DateTime ACTDT { get; set; }
        public Nullable<double> BANKID { get; set; }
        public string VTP { get; set; }
        public string BVN { get; set; }
        public string SEMESTER { get; set; }
        public Nullable<double> SYEAR { get; set; }
        public string SID { get; set; }
        public string BANKSLIP_MRNO { get; set; }
        public string VOUCHER_NO { get; set; }
        public string MANUAL_ID { get; set; }
        public string LOCATION { get; set; }
        public string USER_ID { get; set; }
        public string USER_ID_UP { get; set; }
    
        public virtual ACCCOMPANY ACCCOMPANY { get; set; }
        public virtual ACCLOCATION ACCLOCATION { get; set; }
        public virtual ACCPROJECT ACCPROJECT { get; set; }
    }
}

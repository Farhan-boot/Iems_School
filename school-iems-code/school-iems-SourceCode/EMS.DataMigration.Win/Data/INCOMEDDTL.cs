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
    
    public partial class INCOMEDDTL
    {
        public string INDDID { get; set; }
        public string INDID { get; set; }
        public string LGID { get; set; }
        public string NMSHDDTL { get; set; }
        public Nullable<decimal> INDDSEQ { get; set; }
        public string ADDTP { get; set; }
        public Nullable<decimal> DDGRP { get; set; }
        public string SFUSRID { get; set; }
        public string DBUSRNM { get; set; }
        public string ACTTP { get; set; }
        public System.DateTime ACTDT { get; set; }
    
        public virtual INCOMEDTL INCOMEDTL { get; set; }
    }
}
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
    
    public partial class TRIALBALFIVECOA
    {
        public string TBFVCAID { get; set; }
        public string TBFRCAID { get; set; }
        public string FVLGID { get; set; }
        public string FVNMSH { get; set; }
        public Nullable<decimal> FVTBSEQ { get; set; }
        public Nullable<decimal> FVRPTTAG { get; set; }
        public string FVADDTP { get; set; }
        public Nullable<decimal> FVACCLVL { get; set; }
        public Nullable<decimal> FVNAT { get; set; }
        public Nullable<decimal> FVOPBAL { get; set; }
        public Nullable<decimal> FVDRAMT { get; set; }
        public Nullable<decimal> FVCRAMT { get; set; }
        public Nullable<decimal> FVCLBAL { get; set; }
        public string SFUSRID { get; set; }
        public string DBUSRNM { get; set; }
        public string ACTTP { get; set; }
        public System.DateTime ACTDT { get; set; }
    
        public virtual TRIALBALFOURCOA TRIALBALFOURCOA { get; set; }
    }
}

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
    
    public partial class ACCORDER
    {
        public string ORDID { get; set; }
        public string ORDNO { get; set; }
        public string ORDSNO { get; set; }
        public string BUYID { get; set; }
        public string SFUSRID { get; set; }
        public string DBUSRNM { get; set; }
        public string ACTTP { get; set; }
        public System.DateTime ACTDT { get; set; }
        public string MONTH { get; set; }
        public Nullable<double> YEAR { get; set; }
    
        public virtual ACCBUYER ACCBUYER { get; set; }
    }
}
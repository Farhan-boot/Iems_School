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
    
    public partial class LOOKUPDTL
    {
        public string LKDCD { get; set; }
        public string LOOKNM { get; set; }
        public string LOOKSNM { get; set; }
        public string REFLKDCD { get; set; }
        public string LKMCD { get; set; }
        public string SFUSRID { get; set; }
        public string DBUSRNM { get; set; }
        public string ACTTP { get; set; }
        public System.DateTime ACTDT { get; set; }
    
        public virtual LOOKUPMST LOOKUPMST { get; set; }
    }
}

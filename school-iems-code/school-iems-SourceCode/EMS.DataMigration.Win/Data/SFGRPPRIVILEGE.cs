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
    
    public partial class SFGRPPRIVILEGE
    {
        public string GRPPRVID { get; set; }
        public string SFUSRGRPID { get; set; }
        public string MODUID { get; set; }
        public string SBMODUID { get; set; }
        public string GRPPRVVIEW { get; set; }
        public string GRPPRVADD { get; set; }
        public string GRPPRVEDIT { get; set; }
        public string GRPPRVDEL { get; set; }
    
        public virtual SFMODULE SFMODULE { get; set; }
        public virtual SFSUBMODULE SFSUBMODULE { get; set; }
        public virtual SFUSERSGROUP SFUSERSGROUP { get; set; }
    }
}

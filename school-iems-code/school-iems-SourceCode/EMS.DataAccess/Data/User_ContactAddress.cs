//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EMS.DataAccess.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class User_ContactAddress
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PostOffice { get; set; }
        public string PoliceStation { get; set; }
        public string District { get; set; }
        public Nullable<int> CountryId { get; set; }
        public bool IsValid { get; set; }
        public byte ContactAddressTypeEnumId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public long CreateById { get; set; }
        public System.DateTime LastChanged { get; set; }
        public long LastChangedById { get; set; }
    
        public virtual General_Country General_Country { get; set; }
        public virtual User_Account User_Account { get; set; }
    }
}

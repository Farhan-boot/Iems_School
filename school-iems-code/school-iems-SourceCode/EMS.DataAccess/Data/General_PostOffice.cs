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
    
    public partial class General_PostOffice
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PostCode { get; set; }
        public int PoliceStationId { get; set; }
        public int DistrictId { get; set; }
        public int OrderNo { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime CreateDate { get; set; }
        public long CreateById { get; set; }
        public System.DateTime LastChanged { get; set; }
        public long LastChangedById { get; set; }
    
        public virtual General_District General_District { get; set; }
        public virtual General_PoliceStation General_PoliceStation { get; set; }
    }
}
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
    
    public partial class Htl_FloorHostelBuildingMap
    {
        public int Id { get; set; }
        public int HostelBuildingId { get; set; }
        public int FloorId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateById { get; set; }
        public System.DateTime LastChanged { get; set; }
        public int LastChangedById { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual Htl_Floor Htl_Floor { get; set; }
        public virtual Htl_HostalBuilding Htl_HostalBuilding { get; set; }
    }
}

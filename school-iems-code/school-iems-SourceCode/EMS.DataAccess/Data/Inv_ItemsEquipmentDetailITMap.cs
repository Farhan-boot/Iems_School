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
    
    public partial class Inv_ItemsEquipmentDetailITMap
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public Nullable<int> EquipmentDetailITId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateById { get; set; }
        public System.DateTime LastChanged { get; set; }
        public int LastChangedById { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual Invt_EquipmentDetailIT Invt_EquipmentDetailIT { get; set; }
    }
}
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
    
    public partial class Inv_InventoryDetails
    {
        public int Id { get; set; }
        public Nullable<int> InventoryId { get; set; }
        public Nullable<int> ProductCategoryId { get; set; }
        public Nullable<float> Quantity { get; set; }
        public Nullable<float> UnitPrice { get; set; }
        public Nullable<System.DateTime> WarrentyStartDate { get; set; }
        public Nullable<System.DateTime> WarrentyExpairDate { get; set; }
        public string Description { get; set; }
        public string OwnBarcode { get; set; }
        public string OurBarcode { get; set; }
        public Nullable<int> StatusEnumId { get; set; }
        public string Remark { get; set; }
        public Nullable<bool> IsBarcoded { get; set; }
        public System.DateTime LastChanged { get; set; }
        public int LastChangedById { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual Inv_Inventory Inv_Inventory { get; set; }
    }
}

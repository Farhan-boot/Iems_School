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
    
    public partial class Inv_ItemDelivery
    {
        public int Id { get; set; }
        public Nullable<int> ItemAllocationId { get; set; }
        public Nullable<int> DeliveredQuantity { get; set; }
        public Nullable<System.DateTime> DeliveredDate { get; set; }
        public Nullable<int> DeliveredTo { get; set; }
        public string Note { get; set; }
        public Nullable<int> ItemStockId { get; set; }
        public Nullable<int> ReceivedByBPId { get; set; }
        public string ReceivedByName { get; set; }
        public string ReceivedByMobile { get; set; }
        public Nullable<int> ReceivedByRankId { get; set; }
        public Nullable<int> ReceivedByDepartmentId { get; set; }
        public Nullable<int> ReceivedByAreaId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateById { get; set; }
        public System.DateTime LastChanged { get; set; }
        public int LastChangedById { get; set; }
        public bool IsDeleted { get; set; }
    }
}

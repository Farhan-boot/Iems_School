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
    
    public partial class Inv_RequisitionDetails
    {
        public int Id { get; set; }
        public Nullable<int> RequisitionId { get; set; }
        public string ItemName { get; set; }
        public Nullable<int> ItemId { get; set; }
        public string Detail { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> ApprovedQuantity { get; set; }
        public string RequisitionNo { get; set; }
        public Nullable<int> RequisitionByBPId { get; set; }
        public string RequisitionByName { get; set; }
        public string RequisitionByRank { get; set; }
        public string RequestedByDepartment { get; set; }
        public string RequestedByDepartmentArea { get; set; }
        public Nullable<System.DateTime> RequisitionDate { get; set; }
        public Nullable<System.DateTime> RequierDate { get; set; }
        public Nullable<int> ApprovedByBPId { get; set; }
        public string ApprovedByRank { get; set; }
        public string ApprovedByDepartment { get; set; }
        public Nullable<int> StatusEnumId { get; set; }
        public string Purpose { get; set; }
        public System.DateTime LastChanged { get; set; }
        public int LastChangedById { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual Inv_Requisition Inv_Requisition { get; set; }
    }
}

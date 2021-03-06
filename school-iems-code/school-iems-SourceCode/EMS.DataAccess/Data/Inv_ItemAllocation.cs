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
    
    public partial class Inv_ItemAllocation
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Inv_ItemAllocation()
        {
            this.Inv_ItemAllocationRequisitionMap = new HashSet<Inv_ItemAllocationRequisitionMap>();
            this.Inv_ItemReturnAllocationMap = new HashSet<Inv_ItemReturnAllocationMap>();
        }
    
        public int Id { get; set; }
        public Nullable<int> ItemId { get; set; }
        public Nullable<int> FromStore { get; set; }
        public Nullable<int> AllocatedTo { get; set; }
        public Nullable<System.DateTime> AllocationDate { get; set; }
        public Nullable<int> AllocationStatus { get; set; }
        public Nullable<int> ReferenceBy { get; set; }
        public string Remarks { get; set; }
        public Nullable<int> RequestedItemId { get; set; }
        public Nullable<int> ApprovedQuantity { get; set; }
        public Nullable<int> ApprovedById { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateById { get; set; }
        public System.DateTime LastChanged { get; set; }
        public int LastChangedById { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual Inv_ItemAllocation Inv_ItemAllocation1 { get; set; }
        public virtual Inv_ItemAllocation Inv_ItemAllocation2 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inv_ItemAllocationRequisitionMap> Inv_ItemAllocationRequisitionMap { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inv_ItemReturnAllocationMap> Inv_ItemReturnAllocationMap { get; set; }
    }
}

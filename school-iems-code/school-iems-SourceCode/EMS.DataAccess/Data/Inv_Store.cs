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
    
    public partial class Inv_Store
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Inv_Store()
        {
            this.Inv_Item = new HashSet<Inv_Item>();
            this.Inv_StoreManager = new HashSet<Inv_StoreManager>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> RoomId { get; set; }
        public Nullable<int> CampusId { get; set; }
        public string Phone { get; set; }
        public string Details { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string Remark { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateById { get; set; }
        public System.DateTime LastChanged { get; set; }
        public int LastChangedById { get; set; }
        public bool IsDeleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inv_Item> Inv_Item { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inv_StoreManager> Inv_StoreManager { get; set; }
    }
}
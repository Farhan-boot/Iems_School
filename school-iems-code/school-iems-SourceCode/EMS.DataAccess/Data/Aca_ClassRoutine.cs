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
    
    public partial class Aca_ClassRoutine
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Aca_ClassRoutine()
        {
            this.Aca_ClassAttendanceSetting = new HashSet<Aca_ClassAttendanceSetting>();
        }
    
        public long Id { get; set; }
        public byte DayEnumId { get; set; }
        public System.DateTime StartTime { get; set; }
        public System.DateTime EndTime { get; set; }
        public long ClassId { get; set; }
        public Nullable<long> RoomId { get; set; }
        public Nullable<int> EmployeeId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public long CreateById { get; set; }
        public System.DateTime LastChanged { get; set; }
        public long LastChangedById { get; set; }
        public string Remarks { get; set; }
    
        public virtual Aca_Class Aca_Class { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Aca_ClassAttendanceSetting> Aca_ClassAttendanceSetting { get; set; }
        public virtual General_Room General_Room { get; set; }
        public virtual User_Employee User_Employee { get; set; }
    }
}

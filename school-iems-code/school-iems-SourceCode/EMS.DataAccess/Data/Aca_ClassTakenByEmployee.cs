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
    
    public partial class Aca_ClassTakenByEmployee
    {
        public long Id { get; set; }
        public int EmployeeId { get; set; }
        public long ClassId { get; set; }
        public byte RoleEnumId { get; set; }
        public byte StatusEnumId { get; set; }
        public byte SectionEnumId { get; set; }
        public string Remarks { get; set; }
        public System.DateTime CreateDate { get; set; }
        public long CreateById { get; set; }
        public System.DateTime LastChanged { get; set; }
        public long LastChangedById { get; set; }
        public bool IsDeleted { get; set; }
        public string History { get; set; }
    
        public virtual Aca_Class Aca_Class { get; set; }
        public virtual User_Employee User_Employee { get; set; }
    }
}

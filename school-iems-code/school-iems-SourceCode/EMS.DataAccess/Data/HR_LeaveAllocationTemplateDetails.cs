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
    
    public partial class HR_LeaveAllocationTemplateDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SalaryTemplateId { get; set; }
        public byte LeaveTypeEnumId { get; set; }
        public int AllowedDays { get; set; }
        public float WorkingHourPerDays { get; set; }
        public bool IsPaid { get; set; }
        public string Remarks { get; set; }
        public System.DateTime CreateDate { get; set; }
        public long CreateById { get; set; }
        public System.DateTime LastChanged { get; set; }
        public long LastChangedById { get; set; }
    
        public virtual HR_SalaryTemplate HR_SalaryTemplate { get; set; }
    }
}

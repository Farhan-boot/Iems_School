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
    
    public partial class HR_EmployementHistory
    {
        public long Id { get; set; }
        public int EmployeeId { get; set; }
        public long RankId { get; set; }
        public int DepartmentId { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public byte EmployementTypeEnumId { get; set; }
        public byte JobTypeEnumId { get; set; }
        public byte HistoryTypeEnumId { get; set; }
        public bool IsPrimary { get; set; }
        public System.DateTime CreateDate { get; set; }
        public long CreateById { get; set; }
        public System.DateTime LastChanged { get; set; }
        public long LastChangedById { get; set; }
    
        public virtual HR_Department HR_Department { get; set; }
        public virtual User_Employee User_Employee { get; set; }
        public virtual User_Rank User_Rank { get; set; }
    }
}
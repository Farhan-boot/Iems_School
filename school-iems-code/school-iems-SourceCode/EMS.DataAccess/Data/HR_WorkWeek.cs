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
    
    public partial class HR_WorkWeek
    {
        public int Id { get; set; }
        public byte DayEnumId { get; set; }
        public bool IsHalfDay { get; set; }
        public byte WorkingDayTypeEnumId { get; set; }
        public int CalendarYearId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public long CreateById { get; set; }
        public System.DateTime LastChanged { get; set; }
        public long LastChangedById { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual HR_CalendarYear HR_CalendarYear { get; set; }
    }
}
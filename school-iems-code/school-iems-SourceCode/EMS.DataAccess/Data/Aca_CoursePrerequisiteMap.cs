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
    
    public partial class Aca_CoursePrerequisiteMap
    {
        public int Id { get; set; }
        public long CourseId { get; set; }
        public long PrerequisiteCourseId { get; set; }
    
        public virtual Aca_CurriculumCourse Aca_CurriculumCourse { get; set; }
        public virtual Aca_CurriculumCourse Aca_CurriculumCourse1 { get; set; }
    }
}

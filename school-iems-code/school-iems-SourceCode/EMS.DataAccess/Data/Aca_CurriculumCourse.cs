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
    
    public partial class Aca_CurriculumCourse
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Aca_CurriculumCourse()
        {
            this.Aca_Class = new HashSet<Aca_Class>();
            this.Aca_CoursePrerequisiteMap = new HashSet<Aca_CoursePrerequisiteMap>();
            this.Aca_CoursePrerequisiteMap1 = new HashSet<Aca_CoursePrerequisiteMap>();
            this.Aca_CreditTransferCourses = new HashSet<Aca_CreditTransferCourses>();
            this.Aca_OnlineClass = new HashSet<Aca_OnlineClass>();
            this.Aca_OnlineExam = new HashSet<Aca_OnlineExam>();
            this.Aca_QuestionBank = new HashSet<Aca_QuestionBank>();
        }
    
        public long Id { get; set; }
        public long CourseId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public long CurriculumId { get; set; }
        public float CreditLoad { get; set; }
        public float CreditHour { get; set; }
        public byte CourseCategoryEnumId { get; set; }
        public byte CourseTypeEnumId { get; set; }
        public byte StatusEnumId { get; set; }
        public Nullable<long> ElectiveGroupId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public long CreateById { get; set; }
        public System.DateTime LastChanged { get; set; }
        public long LastChangedById { get; set; }
        public bool IsDeleted { get; set; }
        public string History { get; set; }
        public int OrderNo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Aca_Class> Aca_Class { get; set; }
        public virtual Aca_Course Aca_Course { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Aca_CoursePrerequisiteMap> Aca_CoursePrerequisiteMap { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Aca_CoursePrerequisiteMap> Aca_CoursePrerequisiteMap1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Aca_CreditTransferCourses> Aca_CreditTransferCourses { get; set; }
        public virtual Aca_Curriculum Aca_Curriculum { get; set; }
        public virtual Aca_CurriculumElectiveGroup Aca_CurriculumElectiveGroup { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Aca_OnlineClass> Aca_OnlineClass { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Aca_OnlineExam> Aca_OnlineExam { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Aca_QuestionBank> Aca_QuestionBank { get; set; }
    }
}

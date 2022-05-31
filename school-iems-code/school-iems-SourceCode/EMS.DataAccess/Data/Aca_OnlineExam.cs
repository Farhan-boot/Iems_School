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
    
    public partial class Aca_OnlineExam
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Aca_OnlineExam()
        {
            this.Aca_OnlineExamResultDetail = new HashSet<Aca_OnlineExamResultDetail>();
            this.Aca_OnlineExamTakenByStudent = new HashSet<Aca_OnlineExamTakenByStudent>();
            this.Aca_QuestionBankOnlineExamMap = new HashSet<Aca_QuestionBankOnlineExamMap>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProgramId { get; set; }
        public System.DateTime StartTime { get; set; }
        public System.DateTime EndTime { get; set; }
        public bool HasNegativeMarking { get; set; }
        public Nullable<float> NegativeMarkPercentage { get; set; }
        public bool IsOnlineExamActive { get; set; }
        public string Remark { get; set; }
        public string History { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateById { get; set; }
        public Nullable<System.DateTime> LastChanged { get; set; }
        public Nullable<int> LastChangedById { get; set; }
        public bool IsActive { get; set; }
        public bool IsResultPublished { get; set; }
        public float TotalMarks { get; set; }
        public long CurriculumCourseId { get; set; }
    
        public virtual Aca_CurriculumCourse Aca_CurriculumCourse { get; set; }
        public virtual Aca_Program Aca_Program { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Aca_OnlineExamResultDetail> Aca_OnlineExamResultDetail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Aca_OnlineExamTakenByStudent> Aca_OnlineExamTakenByStudent { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Aca_QuestionBankOnlineExamMap> Aca_QuestionBankOnlineExamMap { get; set; }
    }
}

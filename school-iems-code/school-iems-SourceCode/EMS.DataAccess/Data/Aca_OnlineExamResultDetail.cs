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
    
    public partial class Aca_OnlineExamResultDetail
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int OnlineExamId { get; set; }
        public Nullable<int> SubmittedQuestionOptionId { get; set; }
        public string SubmittedAnswerText { get; set; }
        public string SubmittedAnswerFilePath { get; set; }
        public bool IsCorrect { get; set; }
        public float ObtainedMark { get; set; }
        public string History { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateById { get; set; }
        public System.DateTime LastChanged { get; set; }
        public int LastChangedById { get; set; }
        public bool IsDeleted { get; set; }
        public int QuestionBankId { get; set; }
        public string SubmittedQuestionOptionsJson { get; set; }
    
        public virtual Aca_OnlineExam Aca_OnlineExam { get; set; }
        public virtual Aca_QuestionBank Aca_QuestionBank { get; set; }
        public virtual Aca_QuestionOption Aca_QuestionOption { get; set; }
        public virtual User_Student User_Student { get; set; }
    }
}

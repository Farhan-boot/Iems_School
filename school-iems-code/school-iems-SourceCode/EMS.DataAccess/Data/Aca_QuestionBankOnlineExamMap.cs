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
    
    public partial class Aca_QuestionBankOnlineExamMap
    {
        public int Id { get; set; }
        public int OnlineExamId { get; set; }
        public int QuestionBankId { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateById { get; set; }
        public Nullable<System.DateTime> LastChanged { get; set; }
        public Nullable<int> LastChangedById { get; set; }
        public bool IsActive { get; set; }
    
        public virtual Aca_OnlineExam Aca_OnlineExam { get; set; }
        public virtual Aca_QuestionBank Aca_QuestionBank { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.DataAccess.Data;

namespace EMS.Web.Jsons.Custom.Academic
{
    public class StudentPaymentCollectionJson
    {
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public List<SemesterJson> SemesterList { get; set; }
        public string AutoMrNo { get; set; }
        public string VoucherNo { get; set; }
        public string BankSlipOrMRNo { get; set; }
        public float PreviousBalance { get; set; }
        public float NetBalance { get; set; }
        public float MrPayableAmount { get; set; }
        public float ThisSemesterTotal { get; set; }
        public float Total { get; set; }
        public System.DateTime Date { get; set; }
        public dynamic DataBag { get; set; }
        public List<PayableParticularJson> PayableParticularList { get; set; }
        public List<PaidParticularJson> PaidParticularList { get; set; }
        public List<TransactionItemToSaveJson> TransactionItemToSaveList { get; set; }

        public static StudentPaymentCollectionJson GetNew()
        {
            StudentPaymentCollectionJson std=new StudentPaymentCollectionJson();
            std.StudentId = "";
            std.StudentName = "";
            std.SemesterList = new List<SemesterJson>();
            std.AutoMrNo = "";
            std.VoucherNo = "";
            std.BankSlipOrMRNo = "";
            std.PreviousBalance = 0;
            std.NetBalance = 0;
            std.MrPayableAmount = 0;
            std.ThisSemesterTotal = 0;
            std.Total = 0;
            std.Date= new DateTime(1753, 1, 1, 12, 0, 0, 0);
            std.PayableParticularList = new List<PayableParticularJson>();
            std.PaidParticularList = new List<PaidParticularJson>();
            std.TransactionItemToSaveList = new List<TransactionItemToSaveJson>();
            std.DataBag = new ExpandoObject();
            return std;
        }
    }

    public class SemesterJson
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public static SemesterJson GetNew()
        {
            SemesterJson semester =new SemesterJson();
            semester.Id = "";
            semester.Name = "";
            return semester;
        }
    }

    public class PayableParticularJson
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Amount { get; set; }
        public DateTime CreateDate { get; set; }
        public static PayableParticularJson GetNew()
        {
            PayableParticularJson payableParticular=new PayableParticularJson();
            payableParticular.Id = "";
            payableParticular.Name = "";
            payableParticular.Amount = "";
            payableParticular.CreateDate = new DateTime(1753, 1, 1, 12, 0, 0, 0);
            return payableParticular;
        }
    }

    public class PaidParticularJson
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Amount { get; set; }
        public DateTime CreateDate { get; set; }

        public static PaidParticularJson GetNew()
        {
            PaidParticularJson paidParticular=new PaidParticularJson();
            paidParticular.Id = "";
            paidParticular.Name = "";
            paidParticular.Amount = "";
            paidParticular.CreateDate = new DateTime(1753, 1, 1, 12, 0, 0, 0);
            return paidParticular;
        }
    }
    public class TransactionItemToSaveJson
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public static TransactionItemToSaveJson GetNew()
        {
            TransactionItemToSaveJson transactionItemToSave = new TransactionItemToSaveJson();
            transactionItemToSave.Id = "";
            transactionItemToSave.Name = "";
            return transactionItemToSave;
        }
    }

}

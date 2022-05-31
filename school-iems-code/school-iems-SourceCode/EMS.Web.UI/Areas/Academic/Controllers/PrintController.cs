using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Web.Framework.Bases.Mvc;
using EMS.Web.Jsons.Custom.Account.StudentPayment;
using EMS.Web.Jsons.Models;
using Microsoft.Ajax.Utilities;

namespace EMS.Web.UI.Areas.Academic.Controllers
{
    public class PrintController : EmployeeBaseController
    {
        // GET: Academic/Print
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult StudentMoneyReceipt(int transId=0,int stdId=0)
        {
            var result = new MvcModelResult<Acnt_StdTransaction>();
            string error = string.Empty;

            if (!PermissionUtil.HasPermission(PermissionCollection.AcademicArea.StudentManager.AccountManager.CanPrint, out error))
            {
                result.HasError = true;
                result.Errors.Add(error);
                return View(result);
            }

            if (!Facade.StudentAreaFacade.RegistrationFacade.PrintPaySlipOrMoneyReceipt(ref result, transId, 0 ,stdId))
            {

                return View(result);
            }

            return View(result);
        }

        
        public ActionResult StudentCollectionReport(int id=0)
        {

            var result = new MvcModelListResult<SemesterWiseStdTransectionModel>();

            var student = DbInstance.User_Student
                        .Include(x=>x.User_Account)
                        .Include(x=>x.Aca_Program)
                        .SingleOrDefault(x => x.UserId == id);

            if (student.IsNull())
            {
                result.HasError = true;
                result.Errors.Add("Invalid Student ID!");
            }
            result.DataBag.Student = student;

            //There is no Collection Item Found. (Student not paid any amount yet!)


            List<Acnt_StdTransaction> transactionList = DbInstance.Acnt_StdTransaction
                .Include(x => x.Aca_Semester)
                .Include(x => x.Acnt_StdTransactionDetail)
                .Where(x => x.StudentId == id && !x.IsDeleted)
                .OrderBy(x => x.SemesterId)
                .ToList();


            if (transactionList.IsNull())
            {
                result.HasError = true;
                result.Errors.Add("There is no Collection Item Found. (Student not paid any amount yet!)");
            }

            //result.DataExtra.SemesterDebitList =
            //         Facade.AccountingFacade.StudentPayment.GetSemesterWiseStdTransectionList(id, stdTransactionList,
            //             true, out error);
            //result.DataExtra.SemesterCreditList = Facade.AccountingFacade.StudentPayment.GetSemesterWiseStdTransectionList(id, stdTransactionList,
            //    false, out error);




            var semesterList = transactionList.Where(x=>!x.IsDebited).DistinctBy(x => x.Aca_Semester).Select(x=>x.Aca_Semester).ToList();

            float totalDebitAmount = transactionList.Sum(x => x.DebitAmount);
            float totalCreditAmount = transactionList.Sum(x => x.CreditAmount);
            float totalBalance = totalDebitAmount - totalCreditAmount;

            result.DataBag.TotalCreditAmount = totalCreditAmount;
            result.DataBag.TotalDebitAmount = totalDebitAmount;
            result.DataBag.TotalBalance = totalBalance;

            List<SemesterWiseStdTransectionModel> collectionSemesterWiseList =new  List<SemesterWiseStdTransectionModel>();

            foreach (var semester in semesterList)
            {

                

                var transList = transactionList.Where(x => !x.IsDebited && x.SemesterId == semester.Id).ToList();

                var totalCredit = transList.Sum(x =>x.CreditAmount );


                var semesterWiseCollection = SemesterWiseStdTransectionModel.GetNew();
          
                semesterWiseCollection.SemesterId = (int)semester.Id;
                semesterWiseCollection.SemesterName = semester.Name;
                semesterWiseCollection.TotalCreditAmount = totalCredit;
                
                semesterWiseCollection.TransactionList.AddRange(transList);

                collectionSemesterWiseList.Add(semesterWiseCollection);

            }
            result.DataBag.CollectionSemesterWiseList = collectionSemesterWiseList;
            return View(result);

        }
        public ActionResult StudentSemesterBill(int id=0)
        {

            var result = new MvcModelListResult<SemesterWiseStdTransectionModel>();

            var student = DbInstance.User_Student
                        .Include(x => x.User_Account)
                        .Include(x => x.Aca_Program)
                        .SingleOrDefault(x => x.UserId == id);

            if (student.IsNull())
            {
                result.HasError = true;
                result.Errors.Add("Invalid Student ID!");
            }
            result.DataBag.Student = student;

            //There is no Collection Item Found. (Student not paid any amount yet!)


            List<Acnt_StdTransaction> transactionList = DbInstance.Acnt_StdTransaction
                .Include(x => x.Aca_Semester)
                .Include(x => x.Acnt_StdTransactionDetail)
                .Where(x => x.StudentId == id && !x.IsDeleted)
                .OrderBy(x => x.SemesterId)
                .ToList();


            //if (transactionList.IsNull())
            //{
            //    result.HasError = true;
            //    result.Errors.Add("There is no Collection Item Found. (Student not paid any amount yet!)");
            //}

            var semesterList = transactionList.Where(x => x.IsDebited).DistinctBy(x => x.Aca_Semester).Select(x => x.Aca_Semester).ToList();

            float totalDebitAmount = transactionList.Sum(x => x.DebitAmount);
            float totalCreditAmount = transactionList.Sum(x => x.CreditAmount);
            float totalBalance = totalDebitAmount - totalCreditAmount;

            result.DataBag.TotalCreditAmount = totalCreditAmount;
            result.DataBag.TotalDebitAmount = totalDebitAmount;
            result.DataBag.TotalBalance = totalBalance;

            List<SemesterWiseStdTransectionModel> collectionSemesterWiseList = new List<SemesterWiseStdTransectionModel>();

            foreach (var semester in semesterList)
            {
                var transList = transactionList.Where(x => x.IsDebited && x.SemesterId == semester.Id).ToList();
                var totalCredit = transList.Sum(x => x.DebitAmount);
                var semesterWiseCollection = SemesterWiseStdTransectionModel.GetNew();
                semesterWiseCollection.TotalDebitAmount = totalCredit;
                semesterWiseCollection.SemesterId = (int)semester.Id;
                semesterWiseCollection.SemesterName = semester.Name;

                semesterWiseCollection.TransactionList.AddRange(transList);

                collectionSemesterWiseList.Add(semesterWiseCollection);

            }
            result.DataBag.CollectionSemesterWiseList = collectionSemesterWiseList;
            return View(result);

        }

    }
}
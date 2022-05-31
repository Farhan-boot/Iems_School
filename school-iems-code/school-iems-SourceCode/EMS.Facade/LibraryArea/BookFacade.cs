using System;
using System.Drawing;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Runtime.Remoting.Contexts;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using EMS.Communication.EmailProxy;
using EMS.Communication.SMSProxy;
using EMS.CoreLibrary;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.Framework.Objects;
using EMS.Framework.Permissions;
using EMS.Framework.Utils;
using EMS.Framework.Settings;
namespace EMS.Facade.Library
{
    public class BookFacade : BaseFacade
    {
        private Lib_BookDataService bookDataService = null;
        private Lib_BookCopyDataService bookCopyDataService = null;
        private Lib_BorrowerDataService borrowerDataService = null;
        private Lib_BookCopyTransactionDataService bookCopyTransactionDataService = null;
        private Lib_FineDataService fineDataService = null;
        public BookFacade(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
            bookDataService = new Lib_BookDataService(DbInstance, HttpUtil.Profile);
            bookCopyDataService = new Lib_BookCopyDataService(DbInstance, HttpUtil.Profile);
            borrowerDataService = new Lib_BorrowerDataService(DbInstance, HttpUtil.Profile);
            bookCopyTransactionDataService = new Lib_BookCopyTransactionDataService(DbInstance, HttpUtil.Profile);
            fineDataService = new Lib_FineDataService(DbInstance, HttpUtil.Profile);
        }

        public BorrowerFacade BorrowerFacade
        {
            get { return new BorrowerFacade(DbInstance, Profile); }
        }

        #region Search Books
        #endregion  Search Books

        #region Issue Book
        private bool ValidateBookIssue(Lib_Borrower borrower, IEnumerable<Lib_BookCopy> copies, DateTime expectedReturnDate, out string error)
        {
            error = string.Empty;

            if (borrower == null || borrower.Id <= 0)
            {
                error = "Enter BorrowerId First to issue book!";
                return false;
            }

            if (!copies.Any())
            {
                error = "Enter copies to issue!";
                return false;
            }

            if (borrower.BorrowerStatus == Lib_Borrower.BorrowerStatusEnum.Inactive)
            {
                error = "Inactive Borrrower cannot issue books !";
                return false;
            }

            if (borrower.BorrowerStatus == Lib_Borrower.BorrowerStatusEnum.TemporaryInactive)
            {
                error = "Temporary Inactive Borrrower cannot issue books !";
                return false;
            }

            if (borrower.BorrowerStatus == Lib_Borrower.BorrowerStatusEnum.Deleted)
            {
                error = "Deleted Borrrower cannot issue books !";
                return false;
            }

            if (expectedReturnDate < DateTime.Today)
            {
                error = "Expected return date cannot be before issue date";
                return false;
            }

            return true;
        }

        public bool IssueBooks(int borrowerId, string[] copies, DateTime expectedReturnDate, out string error)
        {
            error = string.Empty;
            var message = string.Empty;
            List<string> includeTables = new List<string>();
            includeTables.Add("User_Account");
            includeTables.Add("User_Account.User_ContactEmail");
            includeTables.Add("User_Account.User_ContactNumber");
            var borrower = borrowerDataService.GetById(borrowerId, includeTables.ToArray());

            var issueCopies = new List<Lib_BookCopy>();

            List<string> incTables = new List<string>();
            incTables.Add("Lib_Book");
            foreach (var issueCopy in copies.Select(barcodeId => bookCopyDataService.GetByBarcode(barcodeId, incTables.ToArray())))
            {
                issueCopies.Add(issueCopy);
            }

            if (!ValidateBookIssue(borrower, issueCopies, expectedReturnDate, out error))
                return false;
            using (var scope = DbInstance.Database.BeginTransaction())
            {
                try
                {
                    var validBookCopies = issueCopies
                        .Where(cp => cp.Deleted == (byte)Lib_BookCopy.DeletedEnum.UnDeleted)
                        .ToArray();

                    foreach (var bookCopy in validBookCopies)
                    {
                        if (bookCopy.BorrowingAllowed == (byte)Lib_BookCopy.BorrowingAllowedEnum.NotAllowed)
                        {
                            error += "Barcode : " + bookCopy.Barcode + " borrowing can't allowed! ";
                            continue;
                        }
                        //Create transaction record new entry:
                        Lib_BookCopyTransaction newObj = Lib_BookCopyTransaction.GetNew(BigInt.NewBigInt());
                        newObj.BorrowerId = borrower.Id;
                        newObj.BookCopyId = bookCopy.Id;
                        newObj.BorrowedDate = DateTime.Now;
                        newObj.IssuedById = HttpUtil.Profile.UserId;
                        newObj.CollectedById = null;
                        newObj.Fine = null;

                        //Get transactionRecord original entry:
                        //var originalState = newObj.EntityState;
                        //var originalEntity = newObj.GetOriginalEntity();

                        //Log transaction record entry:
                        bookCopyTransactionDataService.Save(newObj, out error, scope);
                        //TODO log
                        ////LogUtil<Lib_BookCopyTransaction>.Record(newObj, originalEntity, originalState, Context.TheClient);

                        //Update copy property:

                        bookCopy.BorrowingAllowed = (byte)Lib_BookCopy.BorrowingAllowedEnum.NotAllowed;
                        bookCopy.CopyStatusEnumId = (byte)Lib_BookCopy.CopyStatusEnum.Changed;

                        //Get copyProperty original entry:
                        //var originalcopyState = cp.EntityState;
                        //var originalcopyEntity = cp.GetOriginalEntity();

                        // Log copy property update entry:

                        bookCopyDataService.Save(bookCopy, out error, scope);
                        //TODO log
                        ////LogUtil<Lib_BookCopy>.Record(bookCopy, originalcopyEntity, originalcopyState, Context.TheClient);
                        message += "\nBarcode : " + bookCopy.Barcode + " Title: " + bookCopy.Lib_Book.Title;
                    }

                    DbInstance.SaveChanges();
                    scope.Commit();

                    //Send Message
                    if (borrower.User_Account != null)
                    {
                        //Send Email
                        User_ContactEmail contactEmail = borrower.User_Account.User_ContactEmail
                            .FirstOrDefault(x =>
                                    x.ContactEmailTypeEnumId ==
                                    (byte)User_ContactEmail.ContactEmailTypeEnum.PersonalEmail);
                        if (contactEmail != null)
                        {
                            string subject = "{SiteSettings.Instance.InstituteShortName} Library Book Check Out";
                            string body = $"Hi {borrower.User_Account.FullName},";
                            body += "\n\nYou have check out book from Library.";
                            body += "\nList of Book: " + message;
                            body += "\n\nPowered By";
                            body += "\n" + SiteSettings.PoweredBy;
                            body += "\nWeb: " + SiteSettings.CompanyWebsite;
                            body += "\n---------------------------------------------\n ";
                            body += "\nDO NOT reply to this email. This email is system generated.";
                            body += $"\n{SiteSettings.PoweredBy} Automated Email Sender Service.";

                            if (!SendEmail(borrower.User_Account.FullName, contactEmail.ContactEmail, subject, body, out error))
                            {
                                error = "Please try again later. Email Can't send for " + error;
                            }
                        }
                        else
                        {
                            error = "Book check out Successfully, But Email doesn't send for blank mobile no! ";
                            return false;
                        }

                        //Send SMS
                        User_ContactNumber contactNumber = borrower.User_Account.User_ContactNumber
                            .FirstOrDefault(x =>
                                    x.ContactNumberTypeEnumId ==
                                    (byte)User_ContactNumber.ContactNumberTypeEnum.PersonalMobile);
                        if (contactNumber != null)
                        {
                            var textMessage = "You have check out book from Library.";
                            // RobiApi smsSender = new RobiApi();
                            var smsSender = new SmsSender();
                            var status = smsSender.SendSmsByBanglaPhoneApi(contactNumber.ContactNumber, textMessage, out error);
                        }
                        else
                        {
                            error = "Book check out Successfully, But SMS doesn't send for blank mobile no! ";
                            return false;
                        }
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    scope.Rollback();
                    return false;
                }
            }
        }
        #endregion Issue Book

        #region Return Book

        private bool SetBorrowerFine(Lib_Borrower borrower, decimal currentFine, out string error)
        {
            error = string.Empty;

            if (borrower.BorrowerType != (byte)Lib_Borrower.BorrowerTypeEnum.Student) return true;

            // Create fine record new entry:

            Lib_Fine fineRecordCredit = new Lib_Fine
            {
                BorrowerId = borrower.Id,
                CreateById = HttpUtil.Profile.UserId,
                LibraryFineTypeEnumId = (byte)Lib_Fine.LibraryFineTypeEnum.LateFine,
                CreateDate = DateTime.Now,
                IsCredit = false,
                Amount = currentFine
            };

            // Get fine record original entry: 

            //var originalState = fineRecordCredit.EntityState;
            //var originalObject = fineRecordCredit.GetOriginalEntity();

            //Log Fine record new entry:

            //if (fineRecord.EntityState != EntityState.Added) return;

            fineDataService.Save(fineRecordCredit, out error);
            //LogUtil<FineRecord>.Record(fineRecordCredit, originalObject, originalState, Context.TheClient);

            // Borrower Fine Student Account Posting:
            /*var student = _StudentService.GetByStudentId(borrower.BorrowerId.Trim());
            if (student == null)
            {
                error = "Student not found";
                return false;
            }

            var studentAccount = new Account()
            {
                StudentId = student.Id,
                SemesterId = ApplicationSettings.CurrentSemesterId,
                TransactionDescription = Account.TransactionDescriptionEnum.LibraryFine.ToString().SplitCamelCase()
                                            + " [Ref: " + fineRecordCredit.Id + " ]",
                TransactionDate = DateTime.Now,
                DebitAmount = (float)currentFine,
                CreditAmount = 0,
                AllowEdit = true,
                ORNumber = "(N/A)",
                PaymentType = (byte)Account.PaymentTypeEnum.Other,
                CheckNumber = string.Empty,
                BankName = string.Empty,
                DateOfCheck = DateTime.Now, //DateTime.MinValue,
                TransactionType = (byte)Account.TransactiomTypeEnum.Others
            };

            var result = SaveStudentAccountForLibraryFine(studentAccount, out error);

            if (result)
            {
                // Create fine record new entry:

                Lib_Fine fineRecordDebit = new Lib_Fine
                {
                    BorrowerId = borrower.Id,
                    CreateById = HttpUtil.Profile.UserId,
                    LibraryFineTypeEnumId = (byte)Lib_Fine.LibraryFineTypeEnum.FinePaid,
                    CreateDate = DateTime.Now,
                    IsCredit = true,
                    Amount = currentFine
                };

                // Get fine record original entry: 

                //var originalState1 = fineRecordDebit.EntityState;
                //var originalObject1 = fineRecordDebit.GetOriginalEntity();

                //Log Fine record new entry:

                //if (fineRecord.EntityState != EntityState.Added) return;

                fineDataService.Save(fineRecordDebit,out error);
                //LogUtil<FineRecord>.Record(fineRecordDebit, originalObject1, originalState1, Context.TheClient);

                return true;
            }
            */

            return false;
        }

        private static int GetDaysDue(Lib_BookCopyTransaction transactionRecord)
        {
            if (transactionRecord.ReturnedDate == null)
            {
                return 0;
            }

            var ts = transactionRecord.ReturnedDate.Value.Date.Subtract(transactionRecord.ExpectedReturnDate);
            var daysdue = ts.Days;

            return daysdue > 0 ? daysdue : 0;
        }

        public Lib_BookCopyTransaction ReturnBook(string barcode, out string error)
        {
            error = string.Empty;

            //var trObj = new Lib_BookCopyTransaction();

            var copyProperty = bookCopyDataService.GetByBarcode(barcode);
            if (copyProperty == null)
            {
                error = "The barcoded item is not found";
                return null;
            }

            using (var scope = DbInstance.Database.BeginTransaction())
            {

                try
                {
                    // Update copyProperty :

                    copyProperty.BorrowingAllowedEnumId = (byte)Lib_BookCopy.BorrowingAllowedEnum.Allowed;
                    copyProperty.CopyStatusEnumId = (byte)Lib_BookCopy.CopyStatusEnum.Changed;

                    //Get copy property original entity:

                    //var originalCopyState = copyProperty.EntityState;
                    //var originalCopyEntity = copyProperty.GetOriginalEntity();

                    //Log copy property update entity:

                    bookCopyDataService.Save(copyProperty, out error, scope);

                    //LogUtil<Lib_BookCopy>.Record(copyProperty, originalCopyEntity, originalCopyState, Context.TheClient);

                    var transactionRecord = bookCopyTransactionDataService.GetByBarcode(copyProperty.Barcode)
                        .FirstOrDefault(tr => tr.ReturnedDate.HasValue == false);

                    if (transactionRecord == null)
                    {
                        error = "The book has already been returned or not issued !";
                        return null;
                    }

                    transactionRecord.ReturnedDate = DateTime.Now;

                    var borrower = borrowerDataService.GetById(transactionRecord.BorrowerId);
                    if (borrower == null)
                    {
                        error = "Borrower not found";
                        return null;
                    }

                    //var originalBorrowerState = borrower.EntityState;
                    //var originalBorrowerEntity = borrower.GetOriginalEntity();

                    var daysDue = GetDaysDue(transactionRecord);
                    var previousFine = 0; //GetBorrowerPreviousFine(borrower.Id);
                    var currentFine = 0;

                    currentFine = borrower.BorrowerType == (byte)Lib_Borrower.BorrowerTypeEnum.Student
                        ? 5//daysDue*Convert.ToInt32(ApplicationSettings.DefaultFineAmountStudent)
                        : 0//daysDue*Convert.ToInt32(ApplicationSettings.DefaultFineAmountFaculty)
                        ;

                    borrower.FineAmount = currentFine;
                    borrower.TotalFine = previousFine + currentFine;

                    //Log borrower update entity:

                    borrowerDataService.Save(borrower, out error, scope);
                    /*if (borrower.EntityState == EntityState.Changed)
                    {
                        borrowerDataService.Save(borrower,out error,scope);
                        //LogUtil<Borrower>.Record(borrower, originalBorrowerEntity, originalBorrowerState, Context.TheClient);
                    }*/

                    if (currentFine > 0)
                    {
                        if (!SetBorrowerFine(borrower, currentFine, out error))
                            return null;
                    }


                    // Update transaction record:

                    transactionRecord.CollectedById = HttpUtil.Profile.UserId;
                    transactionRecord.Fine = currentFine;

                    //Get transaction record original entity:

                    //var originalTransactionState = transactionRecord.EntityState;
                    //var originalTransactionEntity = transactionRecord.GetOriginalEntity();

                    //Log transaction record update entity:

                    bookCopyTransactionDataService.Save(transactionRecord, out error, scope);

                    //LogUtil<Lib_BookCopyTransaction>.Record(transactionRecord, originalTransactionEntity, originalTransactionState, Context.TheClient);

                    DbInstance.SaveChanges();
                    scope.Commit();

                    return transactionRecord;
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    scope.Rollback();
                    return null;
                }
            }
        }
        /*
        public Lib_Borrower GetReturnTransactionBorrower(int transactionRecordId, out string error)
        {
            error = string.Empty;

            var transactionRecord = bookCopyTransactionDataService.GetById(transactionRecordId);
            if (transactionRecord == null)
                return null;

            var borrower = borrowerDataService.GetById(transactionRecord.BorrowerId);
            if (borrower == null)
            {
                error = "Borrower not found";
                return null;
            }

            //Fill Borrower additional Property:
            borrower.User = _UsersService.GetByUserName(borrower.BorrowerId);

            borrower.daysDue = GetDaysDue(transactionRecord);

            if (transactionRecord.Fine == null)
                transactionRecord.Fine = 0;

            borrower.previousFine = (decimal)(borrower.TotalFine - transactionRecord.Fine);
            borrower.currentFine = (decimal)transactionRecord.Fine;

            borrower.creditLeft = borrower.BorrowerType == (byte)Borrower.BorrowerTypeEnum.Student ?
                Convert.ToInt32(ApplicationSettings.DefaultCreditLimitStudent) - borrower.TotalFine :
                Convert.ToInt32(ApplicationSettings.DefaultCreditLimitFaculty) - borrower.TotalFine;

            borrower.credited = decimal.Zero;
            borrower.debited = decimal.Zero;

            // Load other unreturned transactions list for the following borrower:

            var unreturnedTransactions = bookCopyTransactionDataService.GetByBorrowerId(borrower.Id).Where(tr => tr.ReturnedDate.HasValue == false);

            var transactionRecords = unreturnedTransactions as IList<Lib_BookCopyTransaction> ?? unreturnedTransactions.ToList();
            foreach (var unreturnedTransaction in transactionRecords)
            {
                unreturnedTransaction.Lib_BookCopy = bookCopyTransactionDataService.GetByBarcodeId(unreturnedTransaction.BarcodeId);

                if (unreturnedTransaction.Lib_BookCopy != null)
                    unreturnedTransaction.Lib_BookCopy.BookDetails = _BookDetailsService.GetById(unreturnedTransaction.Lib_BookCopy.BookId);
            }

            borrower.Lib_BookCopyTransactionCollection = new TList<Lib_BookCopyTransaction>(transactionRecords.ToArray());

            return borrower;
        }
        */
        /*
        public Lib_BookCopy GetReturnTransactionLib_BookCopy(int transactionRecordId, out string error)
        {
            error = string.Empty;

            var transactionRecord = bookCopyTransactionDataService.GetById(transactionRecordId);
            if (transactionRecord == null)
                return null;

            var copyProperty = bookCopyTransactionDataService.GetByBarcodeId(transactionRecord.BarcodeId);
            if (copyProperty == null)
            {
                error = "Copy not found";
                return null;
            }

            copyProperty.BookDetails = _BookDetailsService.GetById(copyProperty.BookId);
            return copyProperty;
        }*/
        /*
        public Lib_Borrower PayFine(int borrowerId, decimal fineAmount, out string error)
        {
            error = string.Empty;

            try
            {
                ConnectionScope.Current.TransactionManager = ConnectionScope.CreateTransaction();

                var borrower = borrowerDataService.GetById(borrowerId);

                // Get borrower original entitiy:

                //var originalState = borrower.EntityState;
                //var originalEntity = borrower.GetOriginalEntity();

                //Update borrower :

                borrower.TotalFine = GetBorrowerPreviousFine(borrower.Id) - fineAmount; //borrower.TotalFine - borrower.payfine;

                //Log borrower update entity:

                borrowerDataService.Save(borrower);

                //LogUtil<Borrower>.Record(borrower, originalEntity, originalState, Context.TheClient);

                //Create fine record new entity:

                var fr = new FineRecord
                {
                    BorrowerId = borrower.Id,
                    Activity = (byte)FineRecord.ActivityEnum.FinePaid,
                    DebitAmount = fineAmount,
                    CreditAmount = 0,
                    DateOfActivity = System.DateTime.Now,
                    SupervisorId = 1
                };

                //Get fine record new entity:

                //var originalfineState = fr.EntityState;
                //var originalfineEntity = fr.GetOriginalEntity();

                // Save and Log fine record new entity:

                fineDataService.Save(fr);

                //LogUtil<FineRecord>.Record(fr, originalfineEntity, originalfineState, Context.TheClient);

                //fill borrower additional property:

                //borrower.payfine = 0;
                borrower.daysDue = 0;
                borrower.currentFine = 0;
                borrower.previousFine = borrower.TotalFine;
                borrower.creditLeft = borrower.CreditLimit - borrower.TotalFine;

                ConnectionScope.Current.TransactionManager.Commit();
                return borrower;
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            finally
            {
                if (ConnectionScope.Current.TransactionManager != null && ConnectionScope.Current.TransactionManager.IsOpen)
                    ConnectionScope.Current.TransactionManager.Rollback();
            }
            return null;
        }*/
        /*
        public bool SaveStudentAccountForLibraryFine(Account studentAccount, out string error)
        {
            error = string.Empty;

            if ((byte)studentAccount.CreditAmount == 0 && (byte)studentAccount.DebitAmount == 0)
            {
                error = "Both Debit & Credit amount can't be 0";
                return false;
            }

            try
            {
                studentAccount.UpdateBy = HttpUtil.Profile.UserId;
                studentAccount.UpdateDate = DateTime.Now;

                //Account Saved and Logged:
                //var originalObject = studentAccount.GetOriginalEntity();

                _AccountService.Save(studentAccount);
                //studentAccount.Student = _StudentService.GetById(studentAccount.StudentId);

                //_LogFacade.LogAccount(obj, null, LogFacade.LogType.Insert, out error);

                return _StudentFacade.ReCalculateAccountBalance(studentAccount.StudentId, out error);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }

        }
        */
        #endregion

        #region Renew
        public bool RenewBook(int borrowerId, string[] copies, DateTime expectedReturnDate, out string error)
        {
            error = string.Empty;
            foreach (var barcode in copies)
            {
                ReturnBook(barcode, out error);
            }
            IssueBooks(borrowerId, copies, expectedReturnDate, out error);
            return true;
        }
        //error = string.Empty;
        #endregion Renew

        #region CopyBook
        //public Lib_BookCopy GetLib_BookCopyByBarcode(string barcode, out string error)
        //{
        //    error = string.Empty;

        //    var copyProperty = bookCopyDataService.GetByBarcode(barcode);

        //    if (copyProperty == null)
        //    {
        //        error = "The barcoded book is not found";
        //        return null;
        //    }

        //    copyProperty.BookDetails = _BookDetailsService.GetById(copyProperty.BookId);

        //    var bookSearchView = _BookSearchViewService.GetByBookId(copyProperty.BookId).FirstOrDefault();
        //    if (bookSearchView == null) return copyProperty;

        //    copyProperty.BookDetails.Authors = bookSearchView.AuthorNames;
        //    copyProperty.BookDetails.Editors = bookSearchView.EditorNames;
        //    copyProperty.BookDetails.Subjects = bookSearchView.SubjectNames;

        //    if (!string.IsNullOrWhiteSpace(copyProperty.BookDetails.Authors))
        //    {
        //        var allAuthors = copyProperty.BookDetails.Authors.Trim();
        //        var authors = allAuthors.Split(';');

        //        copyProperty.BookDetails.BookAuthors = new ObservableCollection<string>();
        //        foreach (var eachAuthor in authors)
        //        {
        //            copyProperty.BookDetails.BookAuthors.Add(eachAuthor.TrimStart().TrimEnd());
        //        }
        //    }

        //    if (!string.IsNullOrWhiteSpace(copyProperty.BookDetails.Subjects))
        //    {
        //        var allSubjects = copyProperty.BookDetails.Subjects.Trim();
        //        var subjects = allSubjects.Split(';');

        //        copyProperty.BookDetails.BookSubjects = new ObservableCollection<string>();
        //        foreach (var eachSubject in subjects)
        //        {
        //            copyProperty.BookDetails.BookSubjects.Add(eachSubject.TrimStart().TrimEnd());
        //        }
        //    }

        //    //var subjects = _SubjectService.GetByBookId(copyProperty.BookDetails.Id);
        //    //copyProperty.BookDetails.BookSubjects = new ObservableCollection<SubjectDetails>();
        //    //foreach (var sd in subjects.Select(subject1 => _SubjectDetailsService.GetById(subject1.SubjectId)))
        //    //{
        //    //    copyProperty.BookDetails.BookSubjects.Add(sd);
        //    //}

        //    return copyProperty;
        //}

        ////private bool ValidateCopy(Lib_BookCopy copyProperty, out string error)
        ////{
        ////    error = string.Empty;

        ////    if (string.IsNullOrWhiteSpace(copyProperty.Barcode))
        ////    {
        ////        error = "Enter barcode for copy";
        ////        return false;
        ////    }

        ////    var existingCopy = bookCopyDataService.GetByBarcode(copyProperty.Barcode);
        ////    if (existingCopy != null && existingCopy.BarcodeId != copyProperty.BarcodeId)
        ////    {
        ////        error = "The barcoded copy already exist. Enter New barcode.";
        ////        return false;
        ////    }

        ////    if (string.IsNullOrWhiteSpace(copyProperty.Shelf))
        ////    {
        ////        error = "Enter shelf for copy";
        ////        return false;
        ////    }

        ////    if (copyProperty.BorrowingAllowed == (byte)Lib_BookCopy.BorrowingAllowedEnum.Allowed)
        ////    {
        ////        var unreturnedCopyTransaction =
        ////            bookCopyTransactionDataService.GetByBarcodeId(copyProperty.BarcodeId).SingleOrDefault(tr => tr.ReturnedDate.HasValue == false);

        ////        if (unreturnedCopyTransaction == null)
        ////            return true;

        ////        error = "The copy cannot be allowed for borrowing. It is already borrowed";
        ////        return false;
        ////    }

        ////    return true;
        ////}

        ////public bool SaveLib_BookCopy(Lib_BookCopy copyProperty, out string error)
        ////{
        ////    error = string.Empty;

        ////    if (!ValidateCopy(copyProperty, out error))
        ////        return false;

        ////    try
        ////    {
        ////        ConnectionScope.Current.TransactionManager = ConnectionScope.CreateTransaction();

        ////        //var originalState = copyProperty.EntityState;
        ////        //var originalEntity = copyProperty.GetOriginalEntity();

        ////        bookCopyDataService.Save(copyProperty);

        ////        //var bookforCopy = _BookDetailsService.GetById(copyProperty.BookId);
        ////        //bookforCopy.HasCopies = 1;
        ////        //_BookDetailsService.Update(bookforCopy);

        ////        //LogUtil<Lib_BookCopy>.Record(copyProperty, originalEntity, originalState, Context.TheClient);

        ////        ConnectionScope.Current.TransactionManager.Commit();
        ////        return true;
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        error = ex.Message;
        ////    }
        ////    finally
        ////    {
        ////        if (ConnectionScope.Current.TransactionManager != null && ConnectionScope.Current.TransactionManager.IsOpen)
        ////            ConnectionScope.Current.TransactionManager.Rollback();
        ////    }

        ////    return false;
        ////}

        //public Lib_BookCopy GetLib_BookCopyTransactions(int barcodeId, out string error)
        //{
        //    error = string.Empty;

        //    var copyProperty = bookCopyDataService.GetByBarcodeId(barcodeId);
        //    if (copyProperty == null)
        //        return null;

        //    var transactionRecords = bookCopyTransactionDataService.GetByBarcodeId(copyProperty.BarcodeId);

        //    if (!transactionRecords.Any())
        //        return copyProperty;

        //    //Fill Lib_BookCopyTransaction List:
        //    var list = new TList<Lib_BookCopyTransaction>();
        //    foreach (var transactionRecord in transactionRecords)
        //    {
        //        transactionRecord.Issuer = _UsersService.GetById(transactionRecord.IssuedBy);
        //        transactionRecord.Borrower = borrowerDataService.GetById(transactionRecord.BorrowerId);

        //        if (transactionRecord.CollectedBy != null)
        //            transactionRecord.Collector = _UsersService.GetById(int.Parse(transactionRecord.CollectedBy.ToString()));

        //        list.Add(transactionRecord);
        //    }

        //    copyProperty.Lib_BookCopyTransactionCollection = list;
        //    return copyProperty;
        //}

        //public TList<Lib_BookCopy> GetLib_BookCopyByBookId(int bookId)
        //{
        //    var copyPropertyList = new TList<Lib_BookCopy>(bookCopyDataService.GetByBookId(bookId));

        //    return copyPropertyList;
        //}
        #endregion   CopyBook


        #region BookCopy
        public Lib_BookCopy GetBookCopyById(long id, out string error)
        {
            error = string.Empty;
            var entity = DbInstance.Lib_BookCopy
                .SingleOrDefault(x => x.Id == id);
            if (entity == null)
            {
                error = "Book Not Found";
                return null;
            }
            return entity;
        }
        public Lib_BookCopy GetBookCopyByIdWithBook(long id, out string error)
        {
            error = string.Empty;
            var entity = DbInstance.Lib_BookCopy
                .Include(x => x.Lib_Book)
                .SingleOrDefault(x => x.Id == id);
            if (entity == null)
            {
                error = "Book Not Found";
                return null;
            }
            return entity;
        }
        public Lib_BookCopy GetBookCopyByBarcodeWithBook(string barcode, out string error)
        {
            error = string.Empty;
            var entity = DbInstance.Lib_BookCopy
                .Include(x => x.Lib_Book)
                .SingleOrDefault(x => x.Barcode == barcode);
            if (entity == null)
            {
                error = "Book Not Found";
                return null;
            }
            return entity;
        }


        private bool ValidateCopy(Lib_BookCopy copyProperty, out string error)
        {
            error = string.Empty;

            if (string.IsNullOrWhiteSpace(copyProperty.Barcode))
            {
                error = "Enter barcode for copy";
                return false;
            }

            var existingCopy = DbInstance.Lib_BookCopy.Where(b => b.Barcode == copyProperty.Barcode && b.Id != copyProperty.Id);// bookCopyDataService.GetByBarcode(copyProperty.Barcode);
            //if (existingCopy != null && existingCopy.BarcodeId != copyProperty.BarcodeId)
            if (existingCopy.Any())
            {
                error = "The barcoded copy already exist. Enter New barcode.";
                return false;
            }
            //TODO check if institute  need
            //if (string.IsNullOrWhiteSpace(copyProperty.Shelf))
            //{
            //    error = "Enter shelf for copy";
            //    return false;
            //}
            //TODO  need to enable when borrowing is done
            //if (copyProperty.BorrowingAllowed == Lib_BookCopy.BorrowingAllowedEnum.Allowed)
            //{
            //    var unreturnedCopyTransaction =
            //        bookCopyTransactionDataService.GetByBarcodeId(copyProperty.BarcodeId).SingleOrDefault(tr => tr.ReturnedDate.HasValue == false);

            //    if (unreturnedCopyTransaction == null)
            //        return true;

            //    error = "The copy cannot be allowed for borrowing. It is already borrowed";
            //    return false;
            //}

            return true;
        }
        public bool SaveBookCopy(Lib_BookCopy bookCopy, out string error)
        {
            error = string.Empty;

            if (!ValidateCopy(bookCopy, out error))
                return false;
            using (var scope = DbInstance.Database.BeginTransaction())
            {
                try
                {
                    bool isNewObject = true;
                    Lib_BookCopy objToSave = null;
                    if (bookCopy.Id != 0)
                    {
                        objToSave = DbInstance.Lib_BookCopy.SingleOrDefault(x => x.Id == bookCopy.Id);
                        isNewObject = false;
                    }

                    if (objToSave == null)
                    {
                        isNewObject = true;
                        objToSave = Lib_BookCopy.GetNew();
                        DbInstance.Lib_BookCopy.Add(objToSave);
                        //must set it in coming object before copy
                        objToSave.CreateDate = DateTime.Now;
                        objToSave.CreateById = Profile.UserId;
                    }

                    CopyUtil.Copy(bookCopy, objToSave);

                    objToSave.LastChanged = DateTime.Now;
                    objToSave.LastChangedById = Profile.UserId;
                    DbInstance.SaveChanges();
                    scope.Commit();

                    //var bookforCopy = _BookDetailsService.GetById(copyProperty.BookId);
                    //bookforCopy.HasCopies = 1;
                    //_BookDetailsService.Update(bookforCopy);

                    //TODO log
                    ////LogUtil<Lib_BookCopy>.Record(copyProperty, originalEntity, originalState, Context.TheClient);

                    CopyUtil.CopyBack(objToSave, bookCopy);

                    return true;
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                    scope.Rollback();
                    return false;
                }

            }

            return false;
        }

        #endregion   BookCopy

        #region Book
        public List<Lib_Book> GetBookList(string title, string barcode, int? pageSize, int? pageNo, ref int count)
        {
            IQueryable<Lib_Book> query = (from q in DbInstance.Lib_Book
                                          select q)
                                        .Include("Lib_BookCopy")
                                        .Include("Lib_BookAuthorMap.Lib_BookAuthor")
                                        .Include("Lib_BookSubjectMap.Lib_BookSubject")
                                        .OrderBy(x => x.Title);
            if (!string.IsNullOrEmpty(title) && !string.IsNullOrWhiteSpace(title))
            {
                query = from q in query
                        where q.Title.Contains(title)
                        select q;
            }
            if (!string.IsNullOrEmpty(barcode) && !string.IsNullOrWhiteSpace(barcode))
            {
                query = from q in query
                        where q.Title.Contains(barcode)
                        select q;
            }
            count = query.Count();
            if (pageNo.HasValue && pageSize.HasValue && pageNo.Value >= 0 && pageSize.Value >= 0)
            {
                if (pageNo <= 1)
                {
                    pageNo = 0;
                }
                else
                {
                    pageNo = pageNo - 1;
                }

                query = (from q in query
                         select q)
                        .Skip(pageNo.Value * pageSize.Value)
                    .Take(pageSize.Value);
            }
            return query.ToList();
        }

        private bool ValidateBook(Lib_Book book, out string error)
        {
            error = string.Empty;
            return true;

            if (string.IsNullOrWhiteSpace(book.Title))
            {
                error = "Enter Book Title !";
                return false;
            }

            if (book.Lib_BookAuthorList == null || book.Lib_BookAuthorList.Count == 0)
            {
                error = "Enter Book Creator(s)";
                return false;
            }

            if (book.Lib_BookSubjectList == null || book.Lib_BookSubjectList.Count == 0)
            {
                error = "Enter Book Subject(s)";
                return false;
            }

            if (string.IsNullOrWhiteSpace(book.Edition))
            {
                error = "Enter Book Edition !";
                return false;
            }

            if (string.IsNullOrWhiteSpace(book.ISBN))
            {
                error = "Enter Book ISBN !";
                return false;
            }

            //TODO check later on
            //if (string.IsNullOrWhiteSpace(bookDetails.Responsibility))
            //{
            //    error = "Enter Book Responsibility !";
            //    return false;
            //}

            //TODO check later on
            //if (string.IsNullOrWhiteSpace(bookDetails.CallNo))
            //{
            //    error = "Enter Book CallNo !";
            //    return false;
            //}

            //var existingBooks = _BookDetailsService.GetByTitle(bookDetails.Title.Trim())
            //                                       .Where(bd => bd.Edition.Trim() == bookDetails.Edition.Trim() && bd.ISBN.Trim() == bookDetails.ISBN.Trim()).ToList();

            var existingBooks = DbInstance.Lib_Book.Where(bd => bd.Title == book.Title.Trim() && bd.Edition.Trim() == book.Edition.Trim() && bd.ISBN.Trim() == book.ISBN.Trim()).ToList();


            if (existingBooks.Any(bk => bk.Id != book.Id))
            {
                error = "Book Title with same edition and ISBN already exists.";
                return false;
            }



            return true;
        }
        //editing
        public bool SaveBook(Lib_Book book, out string error)
        {
            //check permission
            error = string.Empty;
            if (ValidateBook(book, out error))
            {
                using (var scope = DbInstance.Database.BeginTransaction())
                {
                    try
                    {
                        //var bookCopies = bookCopyDataService.GetByBookId(bookDetails.Id);
                        //bookDetails.HasCopies = bookCopies.Any() ? 1 : 0;

                        bool isNewObject = true;
                        Lib_Book objToSave = null;

                        if (book.Id != 0)
                        {
                            objToSave = DbInstance.Lib_Book.SingleOrDefault(x => x.Id == book.Id);
                            isNewObject = false;
                        }

                        if (objToSave == null)
                        {
                            isNewObject = true;
                            objToSave = Lib_Book.GetNew(BigInt.NewBigInt());
                            DbInstance.Lib_Book.Add(objToSave);
                            //must set it in coming object before copy
                            objToSave.CreateDate = DateTime.Now;
                            objToSave.CreateById = Profile.UserId;
                        }

                        //book.Id = book.RecordId;
                        book.Title = book.Title.Trim();
                        if (book.Edition != null)
                            book.Edition = book.Edition.Trim();
                        if (book.ISBN != null)
                            book.ISBN = book.ISBN.Trim();

                        CopyUtil.Copy(book, objToSave);


                        //log here
                        ////LogUtil<BookDetails>.Record(bookDetails, originalEntity, originalState, Context.TheClient);
                        objToSave.LastChanged = DateTime.Now;
                        objToSave.LastChangedById = Profile.UserId;

                        // Save Book Image :
                        //SaveBookImageFileToStorage(bookDetails, out error);
                        SaveBookAuthors(objToSave);
                        SaveBookSubject(objToSave);

                        DbInstance.SaveChanges();
                        scope.Commit();

                        //CopyUtil.CopyBack(objToSave, bookDetails);
                        if (isNewObject)
                        {
                            book.Id = objToSave.Id;
                        }
                        return true;

                    }
                    catch (Exception ex)
                    {
                        error = ex.Message;
                        scope.Rollback();
                        return false;
                    }
                }

            }

            return false;
        }

        private void SaveBookAuthors(Lib_Book bookDetails)
        {
            if (bookDetails.Lib_BookAuthorList == null) return;

            var containBookAuthorMapList = new List<Lib_BookAuthorMap>();

            foreach (var eachBookAuthor in bookDetails.Lib_BookAuthorList)
            {
                var name = eachBookAuthor.AuthorName.Trim();

                // Check author name exist or not in author details : 

                var existingBookAuthor = DbInstance.Lib_BookAuthor.FirstOrDefault(b => string.Equals(b.AuthorName.ToLower(), name.ToLower()));// _AuthorDetailsService.GetByAuthorName(name).FirstOrDefault();
                if (existingBookAuthor == null)
                {
                    //Create author details new entry:
                    var authorDetails = Lib_BookAuthor.GetNew(BigInt.NewBigInt());
                    authorDetails.AuthorName = name;
                    authorDetails.Frequency = 1;
                    DbInstance.Lib_BookAuthor.Add(authorDetails);

                    ////Log author details new entry:
                    ////LogUtil<AuthorDetails>.Record(authorDetails, originalEntity, originalState, Context.TheClient);

                    // Create author new entry:
                    var bookAuthorMap = new Lib_BookAuthorMap
                    {
                        Id = BigInt.NewBigInt(),
                        BookId = bookDetails.Id,
                        AuthorId = authorDetails.Id,
                        StatusEnumId = (byte)Lib_BookAuthorMap.StatusEnum.UnChanged,
                        Remarks = string.Empty
                    };

                    DbInstance.Lib_BookAuthorMap.Add(bookAuthorMap);
                    ////Log author new entry:
                    ////LogUtil<Author>.Record(author, originaAuthorEntity, originalAuthorState, Context.TheClient);

                    // Add author to contain author list:
                    containBookAuthorMapList.Add(bookAuthorMap);
                }
                else
                {
                    // Check author (map table bookDetails and Author details) for existing map :
                    var existingBookAuthorMap = DbInstance.Lib_BookAuthorMap.FirstOrDefault(a => a.AuthorId == existingBookAuthor.Id && a.BookId == bookDetails.Id);
                    //_BookAuthorMapService.GetByAuthorIdBookId(existingAuthorDetails.Id, bookDetails.Id).FirstOrDefault();

                    if (existingBookAuthorMap != null)
                    {
                        // Add existing author to contain author list:
                        containBookAuthorMapList.Add(existingBookAuthorMap);
                        continue;
                    }
                    //Create new author entry:
                    var bookAuthorMap = new Lib_BookAuthorMap
                    {
                        Id = BigInt.NewBigInt(),
                        BookId = bookDetails.Id,
                        AuthorId = existingBookAuthor.Id,
                        Status = (byte)Lib_BookAuthorMap.StatusEnum.UnChanged,
                        Remarks = string.Empty
                    };
                    DbInstance.Lib_BookAuthorMap.Add(bookAuthorMap);
                    ////Log author new entry:
                    ////LogUtil<Author>.Record(author, originaAuthorEntity, originalAuthorState, Context.TheClient);

                    //Add author to contain author list:
                    containBookAuthorMapList.Add(bookAuthorMap);

                    //Update existing author details :
                    existingBookAuthor.Frequency = existingBookAuthor.Frequency + 1;

                    //Get existing author details original entry:
                    //Log existing author details update entry:
                    //if (originalAuthorDetailsState != EntityState.Unchanged)
                    //    //LogUtil<AuthorDetails>.Record(existingAuthorDetails, originalAuthorDetailsEntity,
                    //        originalAuthorDetailsState, Context.TheClient);
                }
            }

            var allBookAuthorMap = DbInstance.Lib_BookAuthorMap.Where(b => b.BookId == bookDetails.Id).ToList();  //_AuthorService.GetByBookId(bookDetails.Id);
            var removeBookAuthorMapList = allBookAuthorMap.Except(containBookAuthorMapList).ToList();
            //var removeBookAuthorMapList2 = allBookAuthorMap.Where(a=>a.Id=).ToList();

            //Remove all author map in the list:
            DbInstance.Lib_BookAuthorMap.RemoveRange(removeBookAuthorMapList);
            //Log author delete entry:
            ////LogUtil<Author>.RecordDelete(author, Context.TheClient);

        }

        private void SaveBookSubject(Lib_Book bookDetails)
        {
            if (bookDetails.Lib_BookSubjectList == null) return;

            var containBookSubjectMapList = new List<Lib_BookSubjectMap>();

            foreach (var eachBookSubject in bookDetails.Lib_BookSubjectList)
            {
                var name = eachBookSubject.SubjectName.Trim();

                // Check subject name exist or not in subject details : 
                var existingSubjectDetails = DbInstance.Lib_BookSubject.FirstOrDefault(b => string.Equals(b.SubjectName.ToLower(), name.ToLower()));
                //_SubjectDetailsService.GetBySubjectName(name).FirstOrDefault();
                if (existingSubjectDetails == null)
                {
                    //Create author details new entry:
                    var subjectDetails = new Lib_BookSubject
                    {
                        Id = BigInt.NewBigInt(),
                        SubjectName = name,
                        StatusEnumId = (byte)Lib_BookSubject.StatusEnum.UnDeleted
                    };
                    DbInstance.Lib_BookSubject.Add(subjectDetails);
                    ////Log author details new entry:
                    ////LogUtil<SubjectDetails>.Record(subjectDetails, originalEntity, originalState, Context.TheClient);
                    //_SubjectDetailsService.Save(subjectDetails);


                    // Create author new entry:
                    var bookSubjectMap = new Lib_BookSubjectMap
                    {
                        Id = BigInt.NewBigInt(),
                        BookId = bookDetails.Id,
                        SubjectId = subjectDetails.Id,
                        Status = (byte)Lib_BookSubjectMap.StatusEnum.UnChanged,
                        Remarks = string.Empty
                    };
                    DbInstance.Lib_BookSubjectMap.Add(bookSubjectMap);

                    //Log author new entry:
                    //_SubjectService.Save(bookSubjectMap);
                    ////LogUtil<Subject>.Record(bookSubjectMap, originaAuthorEntity, originalAuthorState, Context.TheClient);

                    // Add author to contain author list:
                    containBookSubjectMapList.Add(bookSubjectMap);
                }
                else
                {
                    // Check author (map table bookDetails and Author details) for existing map :
                    var existingSubject =
                          DbInstance.Lib_BookSubjectMap.FirstOrDefault(
                        b => b.SubjectId == existingSubjectDetails.Id && b.BookId == bookDetails.Id);

                    //_SubjectService.GetBySubjectIdBookId(existingSubjectDetails.Id, bookDetails.Id)
                    //    .FirstOrDefault();
                    if (existingSubject != null)
                    {
                        // Add existing author to contain author list:
                        containBookSubjectMapList.Add(existingSubject);
                        continue;
                    }

                    //Create new author entry:
                    var subject = new Lib_BookSubjectMap
                    {
                        Id = BigInt.NewBigInt(),
                        BookId = bookDetails.Id,
                        SubjectId = existingSubjectDetails.Id,
                        Status = (byte)Lib_BookSubjectMap.StatusEnum.UnChanged,
                        Remarks = string.Empty
                    };
                    DbInstance.Lib_BookSubjectMap.Add(subject);
                    ////Log author new entry:
                    //_SubjectService.Save(subject);
                    ////LogUtil<Subject>.Record(subject, originaSubjectEntity, originalSubjectState, Context.TheClient);

                    //Add subject to contain subject list:
                    containBookSubjectMapList.Add(subject);


                    //Log existing author details update entry:
                    //if (originalSubjectDetailsState != EntityState.Unchanged)
                    //    //LogUtil<SubjectDetails>.Record(existingSubjectDetails, originalSubjectDetailsEntity,
                    //        originalSubjectDetailsState, Context.TheClient);
                }
            }

            var allBookSubjects = DbInstance.Lib_BookSubjectMap.Where(b => b.BookId == bookDetails.Id).ToList();
            //_SubjectService.GetByBookId(bookDetails.Id);
            var removeSubjectList = allBookSubjects.Except(containBookSubjectMapList).ToList();
            DbInstance.Lib_BookSubjectMap.RemoveRange(removeSubjectList);
            ////Log subject delete entry:
            ////LogUtil<Subject>.RecordDelete(subject, Context.TheClient);

        }

        public Lib_Book GetBookByBookId(long bookId, out string error)
        {
            error = string.Empty;
            var book = DbInstance.Lib_Book
                .Include("Lib_BookAuthorMap")
                .Include("Lib_BookAuthorMap.Lib_BookAuthor")
                .Include("Lib_BookSubjectMap")
                .Include("Lib_BookSubjectMap.Lib_BookSubject")
                .SingleOrDefault(x => x.Id == bookId);
            if (book == null)
            {
                error = "Book Not Found";
                return null;
            }

            //var subjects = _SubjectService.GetByBookId(bookDetails.Id);
            //bookDetails.BookSubjects = new ObservableCollection<SubjectDetails>();
            //foreach (var sd in subjects.Select(subject1 => _SubjectDetailsService.GetById(subject1.SubjectId)))
            //{
            //    bookDetails.BookSubjects.Add(sd);
            //}
            //_BookDetailsService.DeepLoad(bookDetails, true, Data.DeepLoadType.IncludeChildren, typeof (TList<Lib_BookCopy>));

            //FillBookThumbImageFile(bookDetails, out error);

            return book;
        }

        public Lib_Book GetSearchedBookByBookId(int bookId, out string error)
        {
            error = string.Empty;

            Lib_Book book = DbInstance.Lib_Book.Find(bookId);
            if (book == null)
            {
                error = "No books found with the following Book Id";
                return null;
            }

            //var bookSearchView = _BookSearchViewService.GetByBookId(bookDetails.Id).FirstOrDefault();
            //if (bookSearchView == null) return bookDetails;

            //bookDetails.Authors = bookSearchView.AuthorNames;
            //bookDetails.Editors = bookSearchView.EditorNames;
            //bookDetails.Subjects = bookSearchView.SubjectNames;

            //if (bookDetails.Authors != null)
            //{
            //    var allAuthors = bookDetails.Authors.Trim();
            //    var authors = allAuthors.Split(';');

            //    bookDetails.BookAuthors = new ObservableCollection<string>();
            //    foreach (var eachAuthor in authors)
            //    {
            //        bookDetails.BookAuthors.Add(eachAuthor.TrimStart().TrimEnd());
            //    }
            //}

            //if (bookDetails.Subjects != null)
            //{
            //    var allSubejcts = bookDetails.Subjects.Trim();
            //    var subjects = allSubejcts.Split(';');

            //    bookDetails.BookSubjects = new ObservableCollection<string>();
            //    foreach (var eachSubject in subjects)
            //    {
            //        bookDetails.BookSubjects.Add(eachSubject.TrimStart().TrimEnd());
            //    }
            //}

            //var subjects = _SubjectService.GetByBookId(bookDetails.Id);
            //bookDetails.BookSubjects = new ObservableCollection<SubjectDetails>();
            //foreach (var sd in subjects.Select(subject1 => _SubjectDetailsService.GetById(subject1.SubjectId)))
            //{
            //    bookDetails.BookSubjects.Add(sd);
            //}

            return book;
        }

        public bool SaveBookImageFileToStorage(Lib_Book bookDetails, out string error)
        {
            error = string.Empty;

            if (bookDetails.ImageFile != null)
            {
                bookDetails.FileLocation = CreateBookPath(bookDetails.Id.ToString(CultureInfo.InvariantCulture)) + bookDetails.Id.ToString(CultureInfo.InvariantCulture) + ".jpeg";

                //Saves file from byteArray directly without loading original image:
                FileHelper.ByteArrayToFile(ConfigurationManager.AppSettings["BookImagePath"] +
                    bookDetails.FileLocation, bookDetails.ImageFile, out error);

                //Load image from byteArray then saves to file:

                //var image = new System.Drawing.Bitmap(new System.IO.MemoryStream(vehicle.ImageFile));
                //image.Save(ConfigurationManager.AppSettings["VehicleDocumentPath"] + vehicle.VehicleImageLocation, System.Drawing.Imaging.ImageFormat.Jpeg);

                //image.Dispose();
                return true;
            }

            //error = "Image file is null, please attach a image.";
            return false;
        }

        private string CreateBookPath(string imageName)
        {
            string[] folders = imageName.Split('-');
            string path = String.Empty;

            if (folders.Length > 0)
            {
                for (int i = 0; i < folders.Length; i++)
                {
                    path += folders[i] + "\\";
                    if (!System.IO.Directory.Exists(ConfigurationManager.AppSettings["BookImagePath"] + path))
                    {
                        System.IO.Directory.CreateDirectory(ConfigurationManager.AppSettings["BookImagePath"] + path);
                    }
                }
            }
            else
            {
                if (!System.IO.Directory.Exists(ConfigurationManager.AppSettings["BookImagePath"] + imageName + "\\"))
                {
                    System.IO.Directory.CreateDirectory(ConfigurationManager.AppSettings["BookImagePath"] + imageName + "\\");
                }
            }
            return path;
        }

        //public BookDetails GetBookImageByBookid(int bookId, out string error)
        //{
        //    error = string.Empty;

        //    var bookDetails = _BookDetailsService.GetById(bookId);

        //    if (bookDetails == null)
        //    {
        //        error = "Book not found in the system";
        //        return null;
        //    }

        //    if (System.IO.File.Exists(ConfigurationManager.AppSettings["BookImagePath"] + bookDetails.FileLocation))
        //    {
        //        //Getting byteArray of image from filepath without loading original image:
        //        bookDetails.ImageFile =
        //            FileToByteArrayHelper.FileToByteArray(ConfigurationManager.AppSettings["BookImagePath"]
        //                                                  + bookDetails.FileLocation, out error);

        //        return bookDetails;
        //    }

        //    return bookDetails;
        //}

        //public void FillBookThumbImageFile(BookDetails bookDetails, out string error)
        //{
        //    error = string.Empty;
        //    if (System.IO.File.Exists(ConfigurationManager.AppSettings["BookImagePath"] + bookDetails.FileLocation))
        //    {
        //        try
        //        {
        //            using (var imagee = Image.FromFile(ConfigurationManager.AppSettings["BookImagePath"] + bookDetails.FileLocation))
        //            {
        //                //Generating thumb of original image:
        //                var thumbImage = imagee.GetThumbnailImage(150, 180, new System.Drawing.Image.GetThumbnailImageAbort(CallbackFuction), IntPtr.Zero);

        //                using (var stream = new System.IO.MemoryStream())
        //                {
        //                    thumbImage.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);
        //                    bookDetails.ImageFile = stream.ToArray();

        //                    stream.Dispose();
        //                }
        //                imagee.Dispose();
        //                thumbImage.Dispose();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            error = ex.Message;
        //        }

        //    }
        //}

        #endregion Book

        #region Send Email
        public bool SendEmail(string name, string emailToAddress, string subject, string body, out string error)
        {
            error = string.Empty;
            bool result = false;
            try
            {
                MailAddress toAddress = new MailAddress(emailToAddress, name);
                using (EmailSender em = new EmailSender(DefaultEmailSettings.GetSmtpClientSettings()))
                {
                    result = em.SendMail(toAddress, subject, body, out error);
                }
                return result;
            }
            catch (Exception ex)
            {
                error = ex.ToString();
                return result;
            }
        }
        #endregion
    }
}

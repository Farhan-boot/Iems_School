using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Framework.Attributes;

namespace EMS.Framework.Permissions
{
    public partial class PermissionCollection
    {
        //TODO have to fix right permission for EMS. Not worked/used.
        [Permission(5)]
        public sealed class LibraryArea
        {
            [Permission(501)]
            public sealed class IssueBook
            {
                public const int CanIssueBook = 20101;
            }

            [Permission(502)]
            public sealed class ReturnBook
            {
                public const int CanReturnBook = 20201;
            }

            [Permission(503)]
            public sealed class ManageBook
            {
                public const int CanManageBook = 20301;
                public const int CanManageBookCopies = 20302;
                public const int CanViewCopyTransactions = 20303;
                public const int CanImportBook = 20304;
            }

            [Permission(504)]
            public sealed class LibrarySettings
            {
                public const int CanManageSettings = 20401;
            }

            [Permission(505)]
            public sealed class Borrower
            {
                public const int CanViewBorrower = 20501;
                public const int CanMakeBorrowerTemporaryInactive = 20502;
            }

            [Permission(506)]
            public sealed class Account
            {
                public const int CanMakeFinePayment = 20601;
                public const int CanViewFine = 20602;
            }

            [Permission(507)]
            public sealed class Supplier
            {
                public const int CanManageSupplier = 20701;
                public const int CanViewSupplier = 20702;
            }

            [Permission(508)]
            public sealed class BookSubject
            {
                public const int CanManageSubject = 20801;
            }

            [Permission(509)]
            public sealed class Email
            {
                public const int CanSendEmail = 20901;
            }

            [Permission(510)]
            public sealed class Synchronize
            {
                public const int CanSynchronizeBorrower = 21001;
            }

            [Permission(511)]
            public sealed class UnreturnedBooks
            {
                public const int CanViewUnReturnedBooks = 21101;
            }

            [Permission(512)]
            public sealed class ChangeBooks
            {
                public const int CanChangeBarcodedBooks = 21201;
            }

            [Permission(513)]
            public sealed class GenerateBookSpinTags
            {
                public const int CanGenerateBookSpinTags = 21301;
                public const int CanPrintBookSpinTags = 21302;
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.DataAccess.Data;
using EMS.DataService;
using EMS.Framework.Objects;
using EMS.Framework.Utils;

namespace EMS.ReportFacade
{
    public class LibraryReportFacade : BaseReportFacade
    {
        private Lib_BookDataService bookDataService = null;
        private Lib_BookCopyDataService bookCopyDataService = null;
        private Lib_BorrowerDataService borrowerDataService = null;
        private Lib_BookCopyTransactionDataService bookCopyTransactionDataService = null;
        private Lib_FineDataService fineDataService = null;
        public LibraryReportFacade(EmsDbContext emsDbContext, UserProfile usersProfile) : base(emsDbContext, usersProfile)
        {
            bookDataService = new Lib_BookDataService(DbInstance, HttpUtil.Profile);
            bookCopyDataService = new Lib_BookCopyDataService(DbInstance, HttpUtil.Profile);
            borrowerDataService = new Lib_BorrowerDataService(DbInstance, HttpUtil.Profile);
            bookCopyTransactionDataService = new Lib_BookCopyTransactionDataService(DbInstance, HttpUtil.Profile);
            fineDataService = new Lib_FineDataService(DbInstance, HttpUtil.Profile);
        }
        // public Telerik.Reporting.ReportSource Report { get; private set; }
        public object GetBarcodeLabel(string[] copies, out string message)
        {
            message = "";
            //if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionModule.Undergraduate.Applicant.CanView,out message))
            //{
            //    message = "You are not allowed to view this report!";
            //    return null;
            //}
            try
            {
                // throw new Exception("test exception");
                if (copies == null)
                {
                    message = "Please enter barcode to generate this report!";
                    return null;
                }

                var bookCopyList = new List<Lib_BookCopy>();
                List<string> incTables = new List<string>();
                incTables.Add("Lib_Book");
                foreach (var issueCopy in copies.Select(barcodeId => bookCopyDataService.GetByBarcode(barcodeId, incTables.ToArray())))
                {
                    bookCopyList.Add(issueCopy);
                }
                

                Telerik.Reporting.ReportSource report =
                    new EMS.Reporting.Library.LibraryReports().GetBarcodeLabel(bookCopyList, out message);

                return report;
            }
            catch (Exception ex)
            {
                message = "Oops Something went wrong!" + ex.ToString();
            }
            return null;
        }
        public object GetCallNoLabel(string[] copies, out string message)
        {
            message = "";
            //if (!PermissionUtil.HasPermission(PermissionCollection.AdmissionModule.Undergraduate.Applicant.CanView,out message))
            //{
            //    message = "You are not allowed to view this report!";
            //    return null;
            //}
            try
            {
                // throw new Exception("test exception");
                if (copies == null)
                {
                    message = "Please enter barcode to generate this report!";
                    return null;
                }

                var bookCopyList = new List<Lib_BookCopy>();
                List<string> incTables = new List<string>();
                incTables.Add("Lib_Book");
                foreach (var issueCopy in copies.Select(barcodeId => bookCopyDataService.GetByBarcode(barcodeId, incTables.ToArray())))
                {
                    bookCopyList.Add(issueCopy);
                }
                

                Telerik.Reporting.ReportSource report =
                    new EMS.Reporting.Library.LibraryReports().GetCallNoLabel(bookCopyList, out message);

                return report;
            }
            catch (Exception ex)
            {
                message = "Oops Something went wrong!" + ex.ToString();
            }
            return null;
        }

    }
}

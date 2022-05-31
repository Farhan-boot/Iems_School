using System;
using System.Collections.Generic;
using System.Diagnostics;
using EMS.DataAccess.Data;
using EMS.Framework.Settings;
using EMS.Reporting.Library.ReportFiles;
using EMS.Reporting.Library.Source;
using Telerik.Reporting;

namespace EMS.Reporting.Library
{
    public class LibraryReports
    {

        public ReportSource GetBarcodeLabel(List<Lib_BookCopy> bookCopies, out string message)
        {
            message = null;
            rptBarcodeLabel creport = new rptBarcodeLabel();
            dsBook dataset = new dsBook();
            try
            {
                foreach (var bookCopy in bookCopies)
                {
                    dsBook.BookRow row = dataset.Book.NewBookRow();
                    row.LibraryName = $"{SiteSettings.Instance.InstituteShortName} Central Library";
                    row.Title = bookCopy.Lib_Book.Title;
                    row.Barcode = bookCopy.Barcode;
                    dataset.Book.AddBookRow(row);
                }
                creport.DataSource = dataset;
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return creport;
        }
        public ReportSource GetCallNoLabel(List<Lib_BookCopy> bookCopies, out string message)
        {
            message = null;
            rptCallNoLabel creport = new rptCallNoLabel();
            dsBook dataset = new dsBook();
            try
            {
                foreach (var bookCopy in bookCopies)
                {
                    dsBook.BookRow row = dataset.Book.NewBookRow();
                    row.CallNo = bookCopy.CallNo;
                    dataset.Book.AddBookRow(row);
                }
                creport.DataSource = dataset;
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            return creport;
        }

    }


}

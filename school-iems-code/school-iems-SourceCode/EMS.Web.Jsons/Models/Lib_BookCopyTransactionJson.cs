using System;
using System.Collections.Generic;
using System.Linq;
using EMS.CoreLibrary.Helpers;
using EMS.Framework;
using EMS.DataAccess.Data;

namespace EMS.Web.Jsons.Models
{
	public partial class Lib_BookCopyTransactionJson
	{

	    #region Custom Variables
        public string Barcode { get; set; }
	    public string CallNo { get; set; }
	    public decimal? Price { get; set; }

        public Lib_BookCopyJson Lib_BookCopyJson { get; set; }

	    public long BookId { get; set; }
	    public string BookTitle { get; set; }
	    public string BookSubTitle { get; set; }
	    public string BookSubSubTitle { get; set; }
	    public string BookEdition { get; set; }
	    public string BookCategory { get; set; }

	    public string BookISBN;

	    //public Lib_BorrowerJson Lib_BorrowerJson { get; set; }
        #endregion
    }
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public static partial class EntityMapper
    {
        private static void MapExtra(Lib_BookCopyTransaction entity, Lib_BookCopyTransactionJson toJson)
        {
            toJson.Barcode = String.Empty;
            if (entity.Lib_BookCopy!=null)
            {
                var bookCopy = entity.Lib_BookCopy;
                toJson.Barcode = bookCopy.Barcode;
                toJson.CallNo = bookCopy.CallNo;
                toJson.Price = bookCopy.Price;
                toJson.BookId = bookCopy.BookId;

                if (bookCopy.Lib_Book != null)
                {
                    var book = bookCopy.Lib_Book;
                    toJson.BookTitle = book.Title;
                    toJson.BookSubTitle = book.SubTitle;
                    toJson.BookSubSubTitle = book.SubSubTitle;
                    toJson.BookEdition = book.Edition;
                    toJson.BookISBN = book.ISBN;
                    toJson.BookCategory = book.Category.ToString().AddSpacesToSentence();
                }
            }
        }

        private static void MapExtra(Lib_BookCopyTransactionJson json, Lib_BookCopyTransaction toEntity)
        {


        }
    }
}

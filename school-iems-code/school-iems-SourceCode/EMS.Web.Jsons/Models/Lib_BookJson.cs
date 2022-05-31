using System;
using System.Collections.Generic;
using System.Linq;

using EMS.Framework;
using EMS.DataAccess.Data;
using EMS.Web.Jsons.Models;
using EMS.Web.Jsons.Models;

namespace EMS.Web.Jsons.Models
{
	public partial class Lib_BookJson
	{

	    #region Custom Variables
	    public int Lib_BookCopyCount;
	    public int Lib_BookCopyCountAllowed;
	    public int Lib_BookCopyCountNotAllowed;
        public IList<Lib_BookAuthorJson> Lib_BookAuthorJsonList { get; set; }
        public IList<Lib_BookSubjectJson> Lib_BookSubjectJsonList { get; set; }
        //public IList<Lib_BookCopyJson> Lib_BookCopyJsonList { get; set; }
        #endregion
    }
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public static partial class EntityMapper
    {
        private static void MapExtra(Lib_Book entity, Lib_BookJson toJson)
        {
            toJson.Lib_BookCopyCount = entity.Lib_BookCopy.Count;
            toJson.Lib_BookCopyCountAllowed = entity.Lib_BookCopy.Count(x=>x.BorrowingAllowedEnumId==(byte)Lib_BookCopy.BorrowingAllowedEnum.Allowed);
            toJson.Lib_BookCopyCountNotAllowed = entity.Lib_BookCopy.Count(x=>x.BorrowingAllowedEnumId==(byte)Lib_BookCopy.BorrowingAllowedEnum.NotAllowed);
            toJson.Lib_BookAuthorJsonList = new List<Lib_BookAuthorJson>();
            if (entity.Lib_BookAuthorList != null && entity.Lib_BookAuthorList.Count != 0)
            {
                entity.Lib_BookAuthorList.Map(toJson.Lib_BookAuthorJsonList);
            }
            toJson.Lib_BookSubjectJsonList = new List<Lib_BookSubjectJson>();
            if (entity.Lib_BookSubjectList != null && entity.Lib_BookSubjectList.Count != 0)
            {
                entity.Lib_BookSubjectList.Map(toJson.Lib_BookSubjectJsonList);
            }
            //toJson.Lib_BookCopyJsonList = new List<Lib_BookCopyJson>();
            //if (entity.Lib_BookCopy != null && entity.Lib_BookCopy.Count != 0)
            //{
            //    entity.Lib_BookCopy.Map(toJson.Lib_BookCopyJsonList);
            //}
        }

        private static void MapExtra(Lib_BookJson json, Lib_Book toEntity)
        {

            toEntity.Lib_BookAuthorList = new List<Lib_BookAuthor>();
            if (json.Lib_BookAuthorJsonList != null && json.Lib_BookAuthorJsonList.Count != 0)
            {
                json.Lib_BookAuthorJsonList.Map(toEntity.Lib_BookAuthorList);
            }
            toEntity.Lib_BookSubjectList = new List<Lib_BookSubject>();
            if (json.Lib_BookSubjectJsonList != null && json.Lib_BookSubjectJsonList.Count != 0)
            {
                json.Lib_BookSubjectJsonList.Map(toEntity.Lib_BookSubjectList);
            }

            //if (json.Lib_BookCopyJsonList != null && json.Lib_BookCopyJsonList.Count != 0)
            //{
            //    toEntity.Lib_BookCopy = new List<Lib_BookCopy>();
            //    json.Lib_BookCopyJsonList.Map(toEntity.Lib_BookCopy);
            //}
        }
    }
}

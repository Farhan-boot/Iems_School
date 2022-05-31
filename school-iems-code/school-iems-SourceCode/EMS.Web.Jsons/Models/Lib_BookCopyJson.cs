using System;
using System.Collections.Generic;
using System.Linq;

using EMS.Framework;
using EMS.DataAccess.Data;

namespace EMS.Web.Jsons.Models
{
	public partial class Lib_BookCopyJson
	{
        #region Custom Variables
	    public int NoOfItems { get; set; }
        public Lib_BookJson Lib_BookJson { get; set; }
	    public string PresentLibrary { get; set; }
	    public string PermanentLibrary { get; set; }

	    #endregion
    }
    /// <summary>
    /// You can add your custom code here.
    /// </summary>
	public static partial class EntityMapper
    {
        private static void MapExtra(Lib_BookCopy entity, Lib_BookCopyJson toJson)
        {
            toJson.NoOfItems = 1;
            if (entity.Lib_Book != null)
            {
                entity.Lib_Book.Map(toJson.Lib_BookJson);
            }
            if (entity.Lib_Branch != null)
            {
                toJson.PermanentLibrary = entity.Lib_Branch.Name;
            }
            if (entity.Lib_Branch1 != null)
            {
                toJson.PresentLibrary = entity.Lib_Branch1.Name;
            }
        }

        private static void MapExtra(Lib_BookCopyJson json, Lib_BookCopy toEntity)
        {


        }
    }
}

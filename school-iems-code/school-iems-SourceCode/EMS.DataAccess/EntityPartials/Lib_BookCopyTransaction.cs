using System;
using System.Collections.Generic;
using System.Linq;

using EMS.DataAccess.Data;

namespace EMS.DataAccess.Data
{
    public partial class Lib_BookCopyTransaction
    {
        #region Enum Property
        #endregion Enum Property

        private static void GetNewExtra(Lib_BookCopyTransaction obj)
        {
            obj.ExpectedReturnDate = DateTime.Now.AddMonths(3);
        }

    }
}

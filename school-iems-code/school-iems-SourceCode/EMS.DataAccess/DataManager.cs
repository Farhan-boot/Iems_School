using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using EMS.DataAccess.Data;

namespace EMS.DataAccess
{
    public class DataManager
    {
        public static EmsDbContext Instance
        {
            get
            {
                var entities =
                    HttpContext.Current.Items["EMS.DataAccess.Data.EmsDbContext"] as
                        EmsDbContext;
                if (entities == null)
                {
                    entities = new EmsDbContext();
                    HttpContext.Current.Items.Add("EMS.DataAccess.Data.EmsDbContext", entities);
                }

                return entities;
            }
        }
        //public static EmsDbContext Instance
        //{
        //    get
        //    {
        //        var entities =
        //            HttpContext.Current.Items["EMS.DataAccess.Data.EmsDbContext"] as
        //                EmsDbContext;
        //        if (entities == null)
        //        {
        //            entities = new EmsDbContext();
        //            HttpContext.Current.Items.Add("EMS.DataAccess.Data.EmsDbContext", entities);
        //        }

        //        return entities;
        //    }
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EMS.CoreLibrary.Helpers
{
    public   class DateTimeHelper
    {
        public static IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        public static DateTime SqlMinDateTime
        {
            get
            {

                return DateTime.Parse(SqlDateTime.MinValue.ToString());
            }
        }
        public static DateTime SqlMaxDateTime
        {
            get
            {
                return DateTime.Parse(SqlDateTime.MaxValue.ToString());
            }
        }

        public static bool IsValid( DateTime value)
        {
           
            return value.IsValid(); ;
        }

        /// <summary>
        /// datetime should be "dd/MM/yyyy" formatted. 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime? ToDateTime(string value)
        {
            try
            {
                return DateTime.ParseExact(value, "dd/MM/yyyy", Thread.CurrentThread.CurrentCulture);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static DateTime? ToDateTime(string value, string format)
        {
            try
            {
                return DateTime.ParseExact(value, format, Thread.CurrentThread.CurrentCulture);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

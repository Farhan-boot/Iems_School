using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EMS
{
    public class BigInt 
    {
        /// <summary>
        /// http://stackoverflow.com/questions/1416139/how-to-get-timestamp-of-tick-precision-in-net-c
        /// </summary>

        private static long _lastTimeStamp = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmssffff"));
        public static long NewBigInt()
        {

                long original, newValue;
                do
                {
                    original = _lastTimeStamp;
                    long now = long.Parse(DateTime.Now.ToString("yyyyMMddHHmmssffff"));
                    newValue = Math.Max(now, original + 1);

                } while (Interlocked.CompareExchange
                    (ref _lastTimeStamp, newValue, original) != original);

                return newValue;


        }
        private static long _lastTimeStamp2 = DateTime.UtcNow.Ticks;
        private static long NewBigInt2
        {
            get
            {
                long original, newValue;
                do
                {
                    original = _lastTimeStamp2;
                    long now = DateTime.UtcNow.Ticks;
                    newValue = Math.Max(now, original + 1);
                } while (Interlocked.CompareExchange
                             (ref _lastTimeStamp2, newValue, original) != original);

                return newValue;
            }
        }
    }
    public class DateTimeApp
    {
        public static readonly DateTime MinValue = new DateTime(1900, 01, 01, 00, 00, 00);

        public static readonly DateTime MaxValue = new DateTime(2079, 06, 06, 23, 59, 00);
    }
}

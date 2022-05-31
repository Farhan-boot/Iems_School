using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EMS.CoreLibrary.Helpers
{
    public static class ValidationHelper
    {
        public static bool IsNull(this Object obj)
        {
            //return Object.ReferenceEquals(obj, null);
            if (obj == null)
                return true;
            return false;
        }
        public static bool IsNotNull(this Object obj)
        {
            //return Object.ReferenceEquals(obj, null);
            if (obj == null)
                return false;
            return true;
        }


        public static bool IsInt64(this string value)
        {
            Int64 result = 0;
            if (!Int64.TryParse(value, out result))
            {
                return false;
            }

            return true;
        }
        public static bool IsInt32(this string value)
        {
            Int32 result = 0;
            if (!Int32.TryParse(value, out result))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check 11 Digit minimum(without +88) and only number inputed.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsValidMobileForBD(this string value)
        {
            if (!value.IsValid())
            {
                return false;
            }
            if ((value.Length < 11))
            {
                return false;
            }
            Int32 result = 0;
            if (!Int32.TryParse(value.Trim(), out result))
            {
                return false;
            }
           

            return true;
        }

       

        public static bool IsBangla(this string value)
        {
        
            return true;
        }
       
        public static bool IsValidEmail(this string strIn)
        {
            if (!strIn.IsValid())
            {
                return false;
            }
            if (!strIn.IsNull() && new EmailAddressAttribute().IsValid(strIn.Trim()))
               return true;
            return false;
        }

        public static bool IsValid(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return false;
            else if (string.IsNullOrWhiteSpace(value))
                return false;

            return true;
        }

        public static bool IsFloat(this string value)
        {
            float result = 0;
            if (!float.TryParse(value, out result))
            {
                return false;
            }

            return true;
        }

        public static bool IsGuid(string value)
        {
            Guid result = Guid.Empty;
            if (!Guid.TryParse(value, out result))
            {
                return false;
            }

            return true;
        }
        /// <summary>
        /// Nullable Datetime have null value, will return true. But if it has value and not valid then it will return false. 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsValid(this DateTime? value)
        {
            if (value==null)
            {
                return true;
            }
            try
            {
                if (value < DateTimeHelper.SqlMinDateTime || value > DateTimeHelper.SqlMaxDateTime)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
 
        public static bool IsValid(this DateTime value)
        {
            try
            {
                if (value < DateTimeHelper.SqlMinDateTime || value > DateTimeHelper.SqlMaxDateTime)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// return new DateTime(1753, 1, 1, value.Hour, value.Minute,value.Second, 0);
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime GetTime(this DateTime value)
        {

              return new DateTime(1753, 1, 1, value.Hour, value.Minute, value.Second, 0);

        }
        /// <summary>
        /// return new DateTime(1753, 1, 1, value.Hour, value.Minute,value.Second, 0);
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime ToOnlyTime(this DateTime value)
        {

            return new DateTime(1753, 1, 1, value.Hour, value.Minute, value.Second, 0);

        }
        /// <summary>
        /// return new DateTime(value.Year, value.Month, value.Day, 0, 0, 0, 0);
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime ToOnlyDate(this DateTime value)
        {

            return new DateTime(value.Year, value.Month, value.Day, 0, 0, 0, 0);

        }
        public static DateTime RemoveMillisecond(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="formate">The format is in given data. eg: dd/MM/yyyy</param>
        /// <param name="outResultDate"></param>
        /// <returns></returns>
        public static bool ToDateTime(this string value,string formate,out DateTime outResultDate)
        {
            outResultDate = DateTimeHelper.SqlMinDateTime;
            try
            {
                outResultDate = DateTime.ParseExact(value, formate, new CultureInfo("en-US")); 
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        public enum DateTruncate
        {
            Year,
            Month,
            Day,
            Hour,
            Minute,
            Second
        }

    
        public static DateTime TruncateTo(this DateTime dt, DateTruncate truncateTo)
        {
            if (truncateTo == DateTruncate.Year)
                return new DateTime(dt.Year, 0, 0);
            else if (truncateTo == DateTruncate.Month)
                return new DateTime(dt.Year, dt.Month, 0);
            else if (truncateTo == DateTruncate.Day)
                return new DateTime(dt.Year, dt.Month, dt.Day);
            else if (truncateTo == DateTruncate.Hour)
                return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0);
            else if (truncateTo == DateTruncate.Minute)
                return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0);
            else
                return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);

        }
      
        //public static bool IsConflictTime(DateTime StartTime, DateTime EndTime, DateTime compareStartTime, DateTime compareEndTime)
        //{
        //    try
        //    {
        //        //if (StarTime.)
        //        //{

        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        return true;
        //    }
        //    return false;
        //}
    }
}

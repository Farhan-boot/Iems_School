using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace EMS.CoreLibrary.Helpers
{
    public static class StringHelper
    {
   
        [ObsoleteAttribute("This property is obsolete. Use IsValid instead.", false)]
        public static bool IsStringValid(this string value)
        {
            return value.IsValid();
        }

        public static bool IsValidPhone(this string value)
        {
            return Regex.Match(value, @"^[0-9+]+$").Success;
        }
        /// <summary>
        /// Remove All Chars Except 0-9, remove 88 from start of number
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ValidateMobile(this string str)
        {
            if (str.IsValid())
            {

                str = str.Trim();//.Replace("-", "").Replace(" ", "").Replace("+88", "").Replace("+", "");

                str = Regex.Replace(str, "[^.0-9]", "");//remove Special Chars
                if (str.Length > 11)
                {
                    int result = str.IndexOf("880");
                    if (result == 0)
                    {
                        str = str.Remove(0, 2);
                    }
                }
            }
            return str;
        }
        /// <summary>
        /// Make Title Case, Remove Extra Space
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ValidateName(this string str)
        {
            if (str.IsValid())
            {
                //1. Remove Extra Space
                //2. Add Space to Ssentance
                //3. To Lower Case
                //4. To Title Case
                //str = str.TrimRemoveExtraSpace();
                //str = str.AddSpacesToSentence();
                str = str.ToLower();
                str = str.ToTitleCase();
                //str = str.TrimRemoveExtraSpace();
            }
            return str;
        }

        /// <summary>
        /// Remove whitespace from start & end also removes consecutive whitespace characters with a single whitespace between Chars.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string TrimRemoveExtraSpace(this string value)
        {
            if (!value.IsValid())
                return value;
                value = Regex.Replace(value, @"\s+", " ");

            return value.Trim();
        }
        //[ObsoleteAttribute("This property is obsolete. Use IsValidEmail instead.", false)]
        //public static bool IsStringValidEmail(this string value)
        //{
        //    bool isEmail = Regex.IsMatch(
        //        value
        //        , @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z"
        //        , RegexOptions.IgnoreCase
        //        );
        //    return isEmail;
        //}

        public static string ToTitleCase(this string sentance)
        {
            if (!sentance.IsValid())
            {
                return string.Empty;
            }

            sentance = sentance.Replace(".", ". ");// //making space after dot
            sentance = sentance.Replace(". . ", ".");
            var titleCase = "";

            foreach (var word in sentance.Split(' '))
            {
                char[] a = word.ToCharArray();
                if (a.Count() > 0)
                {
                    a[0] = char.ToUpper(a[0]);
                }
                titleCase += " " + new string(a);
            }
            return titleCase.TrimRemoveExtraSpace();
        }
        public static string AddAndSpacesToSentence(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "";
            int unicode = 38;
            char character = (char)unicode;
            StringBuilder newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                if ((char.IsUpper(text[i]) || char.IsDigit(text[i]) || text[i] == '_') && text[i - 1] != ' ')
                {
                    newText.Append(' ').Append(character).Append(' ');
                }
                if (text[i] != '_')
                {
                    newText.Append(text[i]);
                }

            }
            return newText.ToString();
        }
        public static string AddSpacesToSentence(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "";
            text = text.Replace("Phd", "PhD").Replace("PHD", "PhD");
            text = text.Replace("phd", "PhD");
            StringBuilder newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                if ((char.IsUpper(text[i]) || char.IsDigit(text[i]) || text[i] == '_') && text[i - 1] != ' ')
                {
                    newText.Append(' ');
                }
                if (text[i] != '_')
                {
                    newText.Append(text[i]);
                }
            }
            return newText.ToString();
        }
        public static string AddOrdinal(this int num)
        {
            if (num <= 0) return num.ToString();

            switch (num % 100)
            {
                case 11:
                case 12:
                case 13:
                    return num + "th";
            }

            switch (num % 10)
            {
                case 1:
                    return num + "st";
                case 2:
                    return num + "nd";
                case 3:
                    return num + "rd";
                default:
                    return num + "th";
            }

        }
        public static string AddBloodGroupSignToSentence(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "";
            StringBuilder newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                if ((char.IsUpper(text[i]) || char.IsDigit(text[i]) || text[i] == '_') && text[i - 1] != ' ')
                {
                    newText.Append(' ');
                }
                if (text[i] != '_')
                {
                    newText.Append(text[i]);
                }

            }
            return newText.ToString().Replace("Positive", "+").Replace("Negative", "-").Replace("Negetive", "-").Replace(" ", "");
        }


        public static string TruncateEnd(this string source, int maxLength)
        {
            source = source.Trim();
            if (!string.IsNullOrEmpty(source))
                if (source.Length > maxLength)
                {
                    source = source.Substring(0, Math.Min(source.Length, maxLength));   // + " ..."
                    return source;
                }
            return source;
        }
        public static string TruncateAtWord(this string source, int length)
        {
            source = source.Trim();
            if (!source.IsValid() || source.Length < length)
                return source;
            int iNextSpace = source.LastIndexOf(" ", length, System.StringComparison.Ordinal);
            return $"{source.Substring(0, (iNextSpace > 0) ? iNextSpace : length).Trim()}";
        }


        public static string TruncateStartEnd(string source, int startIndex, int startAllowedLength, int totalAllowedLength)
        {
            if (source.Length > totalAllowedLength + 3 && startIndex > startAllowedLength)
            {
                source = "..." + source.Substring((startIndex - startAllowedLength), (source.Length - (startIndex - startAllowedLength)));
            }
            if (source.Length > totalAllowedLength)
            {
                source = StringHelper.TruncateEnd(source, totalAllowedLength);
            }
            return source;
        }

        /// <summary>
        /// Pad Left With length and digit
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <param name="padWithDigit"></param>
        /// <returns></returns>
        public static string ToStringPadLeft(this int value, int length, char padWithDigit)
        {
            string output = value.ToString().PadLeft(length, padWithDigit);
            return output;
        }
        public static string ToStringPadLeft(this long value, int length, char padWithDigit)
        {
            string output = value.ToString().PadLeft(length, padWithDigit);
            return output;
        }
        public static string AddString(this string value, string addedValue)
        {
            string output = value + addedValue;
            return output;
        }
        /// <summary>
        ///  Insert x after every y digit in a string
        /// </summary>
        /// <param name="value"></param>
        /// <param name="insertString"></param>
        /// <returns></returns>
        public static string InsertString(this string value, char insertString)
        {
            var output = string.Join(" ",
                value.ToCharArray().Aggregate("",
                    (result, c) => result += ((!string.IsNullOrEmpty(result) &&
                                               (result.Length + 1) % 3 == 0) ? insertString.ToString() : "") + c.ToString())
                    .Split(' ').ToList().Select(
                        x => x.Length == 1
                            ? string.Format("{0}" + insertString + "{1}", Int32.Parse(x) - 1, x)
                            : x).ToArray());
            return output;
        }
        public static string GetLast(this string source, int tailLength)
        {
            if (tailLength >= source.Length)
                return source;
            return source.Substring(source.Length - tailLength);
        }

        public static Guid ToGuid(this string value)
        {
            Guid output = Guid.Empty;

            Guid.TryParse(value, out output);

            return output;
        }
        public static long ToInt64(this string value)
        {
            long output = 0;
            if (!value.IsValid())
                return 0;
            long.TryParse(value, out output);
            return output;
        }
        public static bool ToBoolean(this string value)
        {
            Boolean output = false;
            if (!value.IsValid())
                return false;
            Boolean.TryParse(value, out output);
            return output;
        }
        public static int ToInt32(this string value)
        {
            int output = 0;
            if (!value.IsValid())
                return 0;
            int.TryParse(value, out output);
            return output;
        }
        public static float ToFloat(this string value)
        {
            float output = 0;
            if (!value.IsValid())
                return 0;
            float.TryParse(value, out output);
            return output;
        }
        public static double ToDouble(this string value)
        {
            double output = 0;
            if (!value.IsValid())
                return 0;
            double.TryParse(value, out output);
            return output;
        }

        public static Guid ToGuid(object value)
        {
            Guid output = Guid.Empty;
            Guid.TryParse(value.ToString(), out output);
            return output;
        }


        //public static string ToSplitCamelCase(object value)
        //{
        //    if (value == null) return string.Empty;

        //    return value.ToString();
        //}

        public static DateTime ToDateTime(string value, string format)
        {
            try
            {
                return DateTime.ParseExact(value, format, Thread.CurrentThread.CurrentCulture);
            }
            catch (Exception ex)
            {
                return System.DateTime.MinValue;
            }
        }

        public static DateTime ToDateTime(string value, string format, string languageCode)
        {
            try
            {
                return DateTime.ParseExact(value, format, new CultureInfo(languageCode).DateTimeFormat);
            }
            catch (Exception ex)
            {
                return System.DateTime.MinValue;
            }
        }

        public static bool IsValidYear(this string year)
        {
            if (year.Length !=4)
            {
                return false;
            }
                //return Regex.Match(year, @"^ (18|19|20|21)[0 - 9][0 - 9]").Success;
            if (Regex.IsMatch(year, @"^(18|19|20|21)[0-9][0-9]"))
            {
                return true;
            }
            return false;
        }

        public static string SetVariable(string text)
        {
            text = text.Trim();
            text = text.Replace(" ", "");
            return text;
        }

        public static string[] SplitStrings(string text)
        {
            text = text.Trim();
            char[] delimiterChars = { '_' };
            string[] words = text.Split(delimiterChars);
            return words;
        }

        public static string SplitStringsGetFirsString(string text)
        {
            text = text.Trim();
            char[] delimiterChars = { '_' };
            string[] words = text.Split(delimiterChars);
            return words[0];
        }

        public static bool IsNullOrEmptyOrWhiteSpace(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return true;
            else if (string.IsNullOrWhiteSpace(text))
                return true;
            else
                return false;
        }

        //improved
        public static string HtmlToPlainText(this string source)
        {
            //1. remove html comment && html tags block
            source = Regex.Replace(source, "<!--.*?-->|<.*?>", String.Empty, RegexOptions.Singleline);

            //2. eg convert '&lt;'  to '<' , '&amp;' to '&'
            source = System.Net.WebUtility.HtmlDecode(source);

            //3.finaly remove if any html tags remains 
            source = ReplaceEx(source, "<", "");
            source = ReplaceEx(source, ">", "");

            //3. replace many to single space
            source = NormalizeExtraSpacde(source);
            return source;


            //remove html tags
            //char[] array = new char[source.Length];
            //int arrayIndex = 0;
            //bool inside = false;

            //for (int i = 0; i < source.Length; i++)
            //{
            //    char let = source[i];
            //    if (let == '<')
            //    {
            //        inside = true;
            //        continue;
            //    }
            //    if (let == '>')
            //    {
            //        inside = false;
            //        continue;
            //    }
            //    if (!inside)
            //    {
            //        array[arrayIndex] = let;
            //        arrayIndex++;
            //    }
            //}
            //String code = new string(array, 0, arrayIndex);
            //replace many to single space
            //code = System.Text.RegularExpressions.Regex.Replace(source, @"\s+", " ");
            //return code;
        }

        
        /// <summary>
        /// Fastest C# code for count occurences of a string within a string.
        /// For more: http://stackoverflow.com/questions/8894329/c-sharp-how-to-count-specific-word-in-given-string
        /// </summary>
        /// <returns>total count</returns>
        public static int GetMatchCount(string source, string textToMatch)
        {
            // Loop through all instances of the string 'text'.
            int count = 0;
            int i = 0;
            source = source.ToLowerInvariant();
            while ((i = source.IndexOf(textToMatch.ToLowerInvariant(), i)) != -1)
            {
                i += textToMatch.Length;
                count++;
            }
            return count;
        }
        /// <summary>
        /// Fastest C# Case Insenstive String Replace method.
        /// For more: http://www.codeproject.com/Articles/10890/Fastest-C-Case-Insenstive-String-Replace
        /// </summary>
        /// <param name="originalTxt"></param>
        /// <param name="pattern"></param>
        /// <param name="replacementBy"></param>
        /// <returns></returns>
        public static string ReplaceEx(string originalTxt, string pattern, string replacementBy)
        {
            int count, position0, position1;
            count = position0 = position1 = 0;
            string upperString = originalTxt.ToUpper();
            string upperPattern = pattern.ToUpper();
            int inc = (originalTxt.Length / pattern.Length) *
                      (replacementBy.Length - pattern.Length);
            char[] chars = new char[originalTxt.Length + Math.Max(0, inc)];
            while ((position1 = upperString.IndexOf(upperPattern,
                                              position0)) != -1)
            {
                for (int i = position0; i < position1; ++i)
                    chars[count++] = originalTxt[i];
                for (int i = 0; i < replacementBy.Length; ++i)
                    chars[count++] = replacementBy[i];
                position0 = position1 + pattern.Length;
            }
            if (position0 == 0) return originalTxt;
            for (int i = position0; i < originalTxt.Length; ++i)
                chars[count++] = originalTxt[i];
            return new string(chars, 0, count);
        }
        static string NormalizeExtraSpacde(string source)
        {
            // Guessing as the post doesn't specify what to use
            char[] Whitespace = new char[] { ' ', '\n', '\t', '\r', '\f', '\v' };
            return string.Join(" ", source.Split(Whitespace, StringSplitOptions.RemoveEmptyEntries));
            // using Regex.Replace(source, @"\s+", " ").Trim();
        }
        /// <summary>
        /// only a-z, A-Z, 0-9, ' '(space) allowed using regx, slower for long text.
        /// more : http://stackoverflow.com/questions/1120198/most-efficient-way-to-remove-special-characters-from-string
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string RemoveSpecialCharactersRegx(string source)
        {
            return Regex.Replace(source, "[^a-zA-Z0-9 ]+", "", RegexOptions.Compiled);
        }
        /// <summary>
        /// only a-z, A-Z, 0-9, ' '(space) allowed, fastest for long text.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string RemoveSpecialCharacters(string source)
        {
            char[] buffer = new char[source.Length];
            int idx = 0;

            foreach (char c in source)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z')
                    || (c >= 'a' && c <= 'z') || (c == ' '))
                {
                    buffer[idx] = c;
                    idx++;
                }
            }

            return new string(buffer, 0, idx);
        }

        public static bool IsValidHost(string url, string compareWidth)
        {

            url = url.ToLower();
            if (url == ("www." + compareWidth) || url == compareWidth)
                return true;
            return false;
        }

        /// <summary>
        /// Replace any English digit exits in input string with current culture equivalent unicode Digit.
        /// if checkValidNumber=true it check for valid number, other wise it returns input string.
        /// eg1: input=123/12 , checkValidNumber=false, out put ১২৩/১২ (if current culture set to "bn-BD" Bangla).
        /// eg2: input=123/12 , checkValidNumber=true, out put 123/12.
        /// eg3: input=123.12 , checkValidNumber=true, out put ১২৩.১২.
        /// </summary>
        /// <param name="input">string to convert</param>
        /// <param name="checkValidNumber">convert if valid number</param>
        /// <returns>string:current culture equivalent unicode Digit</returns>
        public static string ConvertEnglishToNativeDigits(string input, CultureInfo cultureInfo = null, bool checkValidNumber = false)
        {
            if (cultureInfo == null)
                cultureInfo = CultureInfo.CurrentCulture;   //CultureInfo.GetCultures(CultureTypes.AllCultures)   new CultureInfo("bn-BD")
            //if (HttpContext.Current.Session[ConstantCollection.CULTURE_KEY] != null)
            //    cultureInfo = new CultureInfo(HttpContext.Current.Session[ConstantCollection.CULTURE_KEY].ToString());// Thread.CurrentThread.CurrentCulture; //CultureInfo.CurrentCulture;//new CultureInfo("ar-SA");//"bn-BD"

            double decimalNumber;
            if (checkValidNumber)
            {
                if (double.TryParse(input, NumberStyles.Any, cultureInfo, out decimalNumber))
                {
                    input = decimalNumber.ToString(cultureInfo.NumberFormat);
                }
                else
                    return input;
            }
            var nativeDigits = cultureInfo.NumberFormat.NativeDigits;
            var sb = new StringBuilder();
            foreach (char c in input)
            {
                if (c >= '0' && c <= '9')
                {
                    int v = (int)(c - '0');
                    string nc = nativeDigits[v];
                    sb.Append(nc);
                }
                else
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>

        public static string ToWordsInBDT(this double doubleNumber)
        {
            var beforeFloatingPoint = (int)Math.Floor(doubleNumber);
            var afterFloatingPoint = (int)((doubleNumber - beforeFloatingPoint) * 100);

            
            if (beforeFloatingPoint != 0&& afterFloatingPoint != 0)
            {
                var beforeFloatingPointWord = $"{ToWordsIn(beforeFloatingPoint)} taka";

                var afterFloatingPointWord = $"{SmallNumberToWord(afterFloatingPoint, "")} paisa";
                return $"{beforeFloatingPointWord} and {afterFloatingPointWord}";
            }
            else if(beforeFloatingPoint != 0 && afterFloatingPoint == 0)
            {
                var beforeFloatingPointWord = $"{ToWordsIn(beforeFloatingPoint)} taka";
                return $"{beforeFloatingPointWord}";
            }
            else if (beforeFloatingPoint == 0 && afterFloatingPoint != 0)
            {
                var afterFloatingPointWord = $"{SmallNumberToWord(afterFloatingPoint, "")} paisa";
                return $"{afterFloatingPointWord}";
            }
            else
            {
                return "zero taka";
            }

            
        }

        public static string ToWordsInBDT(this float floatNumber)
        {
            var val = (double) floatNumber;
            return val.ToWordsInBDT();
        }

        public static string ToWordsIn(this int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + ToWordsIn(Math.Abs(number));

            var words = "";

            if (number / 10000000 > 0)
            {
                words += ToWordsIn(number / 10000000) + " crore ";
                number %= 10000000;
            }

            if (number / 100000 > 0)
            {
                words += ToWordsIn(number / 100000) + " lakh ";
                number %= 100000;
            }

            if (number / 1000 > 0)
            {
                words += ToWordsIn(number / 1000) + " thousand ";
                number %= 1000;
            }

            if (number / 100 > 0)
            {
                words += ToWordsIn(number / 100) + " hundred ";
                number %= 100;
            }

            words = SmallNumberToWord(number, words);

            return words;
        }

        private static string SmallNumberToWord(int number, string words)
        {
            if (number <= 0) return words;
            if (words != "")
                words += " ";

            var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
            var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

            if (number < 20)
                words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                    words += "-" + unitsMap[number % 10];
            }
            return words;
        }

       
        public static string StringCrop(this string source, int length)
        {
            if (!string.IsNullOrEmpty(source))
                if (source.Length > length)
                {
                    source = source.Substring(0, length) + "...";
                }
            return source;
        }

        public static string GetDaySuffix(this int day)
        {
            switch (day)
            {
                case 1:
                case 21:
                case 31:
                    return "st";
                case 2:
                case 22:
                    return "nd";
                case 3:
                case 23:
                    return "rd";
                default:
                    return "th";
            }
        }
        public static string AddDaySuffix(this int day)
        {
            switch (day)
            {
                case 1:
                case 21:
                case 31:
                    return day+"st";
                case 2:
                case 22:
                    return day + "nd";
                case 3:
                case 23:
                    return day + "rd";
                default:
                    return day + "th";
            }
        }

    }

}

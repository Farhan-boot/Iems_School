using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EMS.CoreLibrary.Helpers;

namespace EMS.CoreLibrary.Helpers
{
    public class SerializeHelper:IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="csvStream"></param>
        /// <param name="fixedColumnSize">Give value for  validate column size in each row, 0 for no validation. </param>
        /// <param name="resultList">out the result data</param>
        /// <param name="error">error message</param>
        /// <returns>success or not</returns>
        public bool ConvertCsvToStringList(Stream csvStream,int fixedColumnSize,out List<string[]> resultList, out string error)
        {
            resultList = null;
            error = "";
            if (csvStream == null)
            {
                error = "No valid Data found in CSV file!";
                return false;
            }
            try
            {
                string csvText;
                using (var reader = new StreamReader(csvStream, Encoding.UTF8))
                {
                    csvText = reader.ReadToEnd();
                }
                //spliting rows by new lines(no problem if line)
                string[] lines = csvText.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                if (lines.Length < 2)
                {
                    error = "File Must have header line with data.";
                    return false;
                }
                //normalize data and spliting column
                //var list = lines.Select(line => Regex.Replace(line, @"\t|\n|\r", " ").Replace("\"", "").Split(',')).ToList();
                resultList = new List<string[]>();
                for (int index = 0; index < lines.Length; index++)
                {
                    var line = lines[index];
                    var row = Regex.Replace(line, @"\t|\n|\r", " ").Replace("\"", "").Split(',').ToArray();
                    if (fixedColumnSize > 0 && row.Length != fixedColumnSize)
                    {
                        error =
                            $"Wrong data found in CSV file, column size should be {fixedColumnSize} but column size found {row.Length} near row {index+1}, please open the CSV file in excel and check valid data, total column and also replace all comma(,) with semicolon(;) then try again.";

                        return false;
                    }
                    //validate csv for blak row
                    var invalidCount = 0;
                    for (int i = 0; i < row.Length; i++)
                    {
                        if (!row[i].IsValid()) invalidCount++;
                    }
                    if(invalidCount== row.Length) //not include empty row, no need.
                        continue;
                    resultList.Add(row);
                }
                return true;
            }
            catch (Exception ex)
            { 
                error = "Ivalid data in CSV file. "+ex.Message;
                return false;
            }
        }

        //public string ListToCsv<T>(IEnumerable<T> sequence)
        //{
            
        //    string csv = String.Join(",", sequence);

        //    return csv;
        //}
        public static string ListToCsv<T>(IEnumerable<T> values, string separator)
        {
            var stringValues = values.Select(item =>
                (item == null ? string.Empty : (item.ToString().IndexOf(',') != -1? "\""+item+ "\"": item.ToString())) );
            return string.Join(separator, stringValues.ToArray());
        }
        public void Dispose()
        {
            
        }
    }
}

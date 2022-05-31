using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.CoreLibrary.Helpers
{
    public class FileHelper
    {
        /// <summary>
        /// Function to get byte array from a file 
        /// </summary>
        /// <param name="_FileName">File name to get byte array</param>
        /// <returns>Byte Array</returns>
        /// 

        /**
        * Writes the specified byte[] to the specified File path.
     * 
     * @param theFile File Object representing the path to write to.
     * @param bytes The byte[] of data to write to the File.
     * @throws IOException Thrown if there is problem creating or writing the 
     * File.
     */

        public static string ByteArrayToFile(string pathToSaveImages, byte[] byteData, out string error)
        {
            error = string.Empty;
            System.IO.FileStream wFile;
            wFile = new FileStream(pathToSaveImages, FileMode.Create);
            wFile.Write(byteData, 0, byteData.Length);
            wFile.Close();

            return pathToSaveImages;
        }

        private string CreateBookPath(string imagePath)
        {
            string[] folders = imagePath.Split('-');
            string path = String.Empty;

            if (folders.Length > 0)
            {
                for (int i = 0; i < folders.Length; i++)
                {
                    path += folders[i] + "\\";
                    if (!System.IO.Directory.Exists(ConfigurationManager.AppSettings["BookImagePath"] + path))
                    {
                        System.IO.Directory.CreateDirectory(ConfigurationManager.AppSettings["BookImagePath"] + path);
                    }
                }
            }
            else
            {
                if (!System.IO.Directory.Exists(ConfigurationManager.AppSettings["BookImagePath"] + imagePath + "\\"))
                {
                    System.IO.Directory.CreateDirectory(ConfigurationManager.AppSettings["BookImagePath"] + imagePath + "\\");
                }
            }
            return path;
        }
        static readonly string[] SizeUnit =
            { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        public static string FileSize(Int64 bytes, int decimalPlaces = 1)
        {
            if (bytes < 0) { return "-" + FileSize(-bytes); }
            if (bytes == 0) { return "0.0 bytes"; }

            // mag is 0 for bytes, 1 for KB, 2, for MB, etc.
            int mag = (int)Math.Log(bytes, 1024);

            // 1L << (mag * 10) == 2 ^ (10 * mag) 
            // [i.e. the number of bytes in the unit corresponding to mag]
            decimal adjustedSize = (decimal)bytes / (1L << (mag * 10));

            // make adjustment when the value is large enough that
            // it would round up to 1000 or more
            if (Math.Round(adjustedSize, decimalPlaces) >= 1000)
            {
                mag += 1;
                adjustedSize /= 1024;
            }

            return string.Format("{0:n" + decimalPlaces + "} {1}",
                adjustedSize,SizeUnit[mag]);
        }


    }
}

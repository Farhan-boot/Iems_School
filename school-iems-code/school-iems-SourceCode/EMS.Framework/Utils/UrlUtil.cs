using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using EMS.Framework;

namespace EMS.Framework.Utils
{
    public static class UrlUtil
    {
        private static readonly char[] InvalidfilenameCharacters = Path.GetInvalidFileNameChars();
        private static readonly string _CmsRootPath = "Files/Upload";
        //public static string GetUrl(string requesturl, string uniquepath)
        //{
        //    return GetHostName(requesturl) + uniquepath;
        //}

        //private static object GetHostName(string requesturl)
        //{
        //    for (int i = 9; i < requesturl.Length; i++)
        //    {
        //        if (requesturl[i] == '/')
        //            return requesturl.Substring(0, i);
        //    }
        //    return string.Empty;
        //}



        private static string _RootPath
        {
            get { return System.Web.HttpContext.Current.Server.MapPath("~/"); }
        }

        private static string _VirtualPath
        {
            get { return VirtualPathUtility.ToAbsolute("~/"); }
        }

        //private static string _CmsRootPath
        //{
        //    get
        //    {
        //        return AppSettingsHelper.CmsFolder;
        //    }
        //}

        //private static string _CmsClientRootPath
        //{
        //    get {  return AppSettingsHelper.ClientsCmsFolderLocation; }
        //}

        //public static string GetDirectoryPathByTypeId(int typeId)
        //{
        //    string path = string.Format("{0}/{1}/{2}/", _VirtualPath,
        //        _CmsRootPath,
        //        HttpUtil.ClientID.ToString()); //

        //    switch (typeId)
        //    {
        //        case (int)EnumCollection.ContentTypeEnum.Page:
        //            path += "Pages";
        //            break;
        //        case (int)EnumCollection.ContentTypeEnum.StyleSheet:
        //            path += "StyleSheets";
        //            break;
        //        case (int)EnumCollection.ContentTypeEnum.Script:
        //            path += "JavaScripts";
        //            break;
        //        case (int)EnumCollection.ContentTypeEnum.Media:
        //            path += "Files";
        //            break;
        //        default:
        //            break;
        //    }
        //    return path;
        //}

        //public static string GetDirectoryPathByFilePath(string filepath)
        //{
        //    string path = string.Format("{0}/{1}/{2}", _CmsRootPath, HttpUtil.ClientID.ToString(), filepath.Replace("\\", "/"));
        //    return path;
        //}

        //public static string GetClientDirectoryPathByTypeId(int typeId, Guid clientId)
        //{
        //    string path = string.Format("{0}/{1}/", "",
        //        clientId.ToString()); //

        //    switch (typeId)
        //    {
        //        case (int)EnumCollection.ContentTypeEnum.Page:
        //            path += "Pages";
        //            break;
        //        case (int)EnumCollection.ContentTypeEnum.StyleSheet:
        //            path += "StyleSheets";
        //            break;
        //        case (int)EnumCollection.ContentTypeEnum.Script:
        //            path += "JavaScripts";
        //            break;
        //        case (int)EnumCollection.ContentTypeEnum.Media:
        //            path += "Files";
        //            break;
        //        default:
        //            break;
        //    }
        //    return path;
        //}

        //public static string GetDirectoryPathByFolderPath(int typeId, string folderPath)
        //{
        //    return string.Format("{0}/{1}", GetDirectoryPathByTypeId(typeId), folderPath);
        //}

        //public static string GetExtension(int typeId)
        //{
        //    string ext = string.Empty;
        //    switch (typeId)
        //    {
        //        case (int)EnumCollection.ContentTypeEnum.Page:
        //            ext = ".html";
        //            break;
        //        case (int)EnumCollection.ContentTypeEnum.StyleSheet:
        //            ext = ".css";
        //            break;
        //        case (int)EnumCollection.ContentTypeEnum.Script:
        //            ext = ".js";
        //            break;
        //        case (int)EnumCollection.ContentTypeEnum.Media:
        //            break;
        //        default:
        //            break;
        //    }
        //    return ext;
        //}

        //public static string GetRoutPath(Post parent, string name)
        //{
        //    string routePath = string.Empty;
        //    if (parent != null)
        //        routePath = string.Format("{0}/{1}", parent.RoutePath, name);
        //    else
        //        routePath = name;
        //    routePath = routePath.Replace(" ", string.Empty);
        //    return routePath;
        //}

        //public static bool ValidFileExtension(string ext, int typeId)
        //{
        //    switch (typeId)
        //    {
        //        case (int)EnumCollection.ContentTypeEnum.Page:
        //            return ext == ".html";
        //            break;
        //        case (int)EnumCollection.ContentTypeEnum.StyleSheet:
        //            return ext == ".css";
        //            break;
        //        case (int)EnumCollection.ContentTypeEnum.Script:
        //            return ext == ".js";
        //            break;
        //        case (int)EnumCollection.ContentTypeEnum.Media:
        //            return ext != ".exe" && ext != ".bat" && ext != ".csHtml" && ext != ".cs"; //
        //            break;
        //        default:
        //            break;
        //    }
        //    return true;
        //}

        public static string GetFullUrl(this Uri url, string virtualPath)
        {
            if (virtualPath[0] == '~')
                virtualPath = virtualPath.Substring(1);
            if (virtualPath[0] == '/')
                virtualPath = virtualPath.Substring(1);
            return string.Format("{0}://{1}/{2}",
                url.Scheme,
                url.Authority,
                virtualPath);
        }
        public static string GetFlashUrl(string virtualPath)
        {
            
            if (virtualPath != null && virtualPath[0] == '~')
                virtualPath = virtualPath.Substring(1);
            return virtualPath;
        }
        //public static string GetFlashUrl(string virtualPath, int size)
        //{
        //    if (virtualPath[0] == '~')
        //        virtualPath = virtualPath.Substring(1);
        //    if (virtualPath.Contains(".jpg") || virtualPath.Contains(".JPG") ||
        //        virtualPath.Contains(".jpeg") || virtualPath.Contains(".JEPG") ||
        //        virtualPath.Contains(".png") || virtualPath.Contains(".PNG") ||
        //    virtualPath.Contains(".gif") || virtualPath.Contains(".GIF") ||
        //    virtualPath.Contains(".bmp") || virtualPath.Contains(".BMP"))
        //    {
        //        switch (size)
        //        {
        //            case (int)EnumCollection.ImageSize.Thumb:
        //                virtualPath = virtualPath.Replace("Uploads", "Uploads/thumb");
        //                break;
        //            case (int)EnumCollection.ImageSize.Small:
        //                virtualPath = virtualPath.Replace("Uploads", "Uploads/small");
        //                break;
        //            case (int)EnumCollection.ImageSize.Medium:
        //                virtualPath = virtualPath.Replace("Uploads", "Uploads/medium");
        //                break;
        //            case (int)EnumCollection.ImageSize.Large:
        //                virtualPath = virtualPath.Replace("Uploads", "Uploads/large");
        //                break;
        //        }

        //    }
        //    return virtualPath;
        //}

        //public static string GetAdsolutePath(string fileName, int typeId)
        //{
        //    string path = GetDirectoryPathByTypeId(typeId);
        //    path = string.Format("{0}/{1}", path, ValidateFileName(fileName));
        //    return path;
        //}

        //public static string GetAdsolutePath(string folderpath, string fileName, int typeId)
        //{
        //    string path = GetDirectoryPathByFolderPath(typeId, folderpath);
        //    if (!string.IsNullOrEmpty(fileName))
        //    {
        //        path = string.Format("{0}/{1}", path, ValidateFileName(fileName));
        //    }
        //    return path;
        //}

        public static string CombineVirtualPath(string directoryPath, string fileName)
        {
            return string.Format("{0}/{1}", directoryPath, ValidateFileName(fileName,false));
        }

        //public static string CombineVirtualPath(string directoryPath, string folderPath, string fileName)
        //{
        //    return string.Format("{0}/{1}/{2}", directoryPath, folderPath, ValidateFileName(fileName));
        //}

        //public static string GetRoutPath(string name)
        //{
        //    return string.Format("{0}/{1}", _VirtualPath, name);
        //}

        public static string GetPhysicalPath(string virtualPath)
        {
            return HttpContext.Current.Server.MapPath(virtualPath);
        }

        //public static string GetAbsolutePath(string physicalPath)
        //{
        //    return VirtualPathUtility.ToAbsolute(physicalPath);
        //}

        public static string GetUniqueFileName(string fileName, string directoryPath)
        {
            fileName = ValidateFileName(fileName,false);
            string ext = Path.GetExtension(fileName);
            fileName = Path.GetFileNameWithoutExtension(fileName);
            string newFileName = fileName;
            string physicalPath = CombineVirtualPath(directoryPath, newFileName + ext);
            int count = 1;
            while (System.IO.File.Exists(physicalPath))
            {
                newFileName = fileName + "(" + count + ")";
                count++;
                physicalPath = CombineVirtualPath(directoryPath, newFileName + ext);
            }
            return newFileName + ext;
        }

        public static string ValidateFileName(string name, bool isUrl)
        {

            if (!isUrl)
            {
                foreach (char c in InvalidfilenameCharacters)
                {
                    name.Replace(c, '-');
                }
                name = name.Replace(' ', '-');
               // name = name.ToLower();

                return name;

            }
            else
            {
                name = name.ToLowerInvariant().Replace(" ", "-");
                name = Regex.Replace(name, @"[^0-9a-z-]", string.Empty);
            }
            return name;
        }

        public static List<string> GetTemplates()
        {
            try
            {
                var dirPath = _RootPath + "files\\Templates";
                var templateDirectories = new List<string>(Directory.EnumerateDirectories(dirPath));

                List<string> templates = new List<string>();

                foreach (var template in templateDirectories)
                {
                    var index = template.LastIndexOf("\\", System.StringComparison.Ordinal);
                    templates.Add(template.Substring(index + 1));
                }
                return templates;
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }

        public static string GetPermalink(string permalink)
        {
            if (permalink == null)
                return string.Empty;

            if (permalink.First() == '/')
                permalink = permalink.Substring(1);

            permalink = VirtualPathUtility.RemoveTrailingSlash(permalink);

            return permalink;
        }

        //public static string GetClientRootDirectory(Guid id)
        //{
        //    return string.Format("{0}/{1}/{2}", _VirtualPath, _CmsRootPath, id.ToString());
        //}

        //public static bool DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        //{
        //    DirectoryInfo dir = new DirectoryInfo(sourceDirName);
        //    DirectoryInfo[] dirs = dir.GetDirectories();

        //    // If the source directory does not exist, throw an exception.
        //    if (!dir.Exists)
        //    {
        //        return false;
        //    }

        //    // If the destination directory does not exist, create it.
        //    if (!Directory.Exists(destDirName))
        //    {
        //        Directory.CreateDirectory(destDirName);
        //    }


        //    // Get the file contents of the directory to copy.
        //    FileInfo[] files = dir.GetFiles();

        //    foreach (FileInfo file in files)
        //    {
        //        // Create the path to the new copy of the file.
        //        string temppath = Path.Combine(destDirName, file.Name);

        //        // Copy the file.
        //        file.CopyTo(temppath, true);
        //    }

        //    // If copySubDirs is true, copy the subdirectories.
        //    if (copySubDirs)
        //    {

        //        foreach (DirectoryInfo subdir in dirs)
        //        {
        //            // Create the subdirectory.
        //            string temppath = Path.Combine(destDirName, subdir.Name);

        //            // Copy the subdirectories.
        //            DirectoryCopy(subdir.FullName, temppath, copySubDirs);
        //        }
        //    }
        //    return true;
        //}

        //public static string GetCmsRoot()
        //{
        //    return string.Format("{0}/{1}/", _VirtualPath, _CmsRootPath);
        //}
        public static List<string> GetValidLinks()
        {
            var list = new List<string>
            {
                "Admin",
                "Home",
                "Search",
                "Test",
                "Category",
                "404"
            };

            return list;
        }
    }
}

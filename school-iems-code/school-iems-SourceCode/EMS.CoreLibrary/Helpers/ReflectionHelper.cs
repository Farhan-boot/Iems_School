using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Deployment.Application;
using System.Reflection;

namespace EMS.CoreLibrary.Helpers
{
    public class ReflectionHelper
    {
        public static Type GetType(string fullName)
        {
            if (string.IsNullOrEmpty(fullName))
                return null;
            Type type = Type.GetType(fullName);
            if (type == null)
            {
                string targetAssembly = fullName;
                while (type == null && targetAssembly.Length > 0)
                {
                    try
                    {
                        int dotInd = targetAssembly.LastIndexOf('.');
                        targetAssembly = dotInd >= 0 ? targetAssembly.Substring(0, dotInd) : "";
                        if (targetAssembly.Length > 0)
                            type = Type.GetType(fullName + ", " + targetAssembly);
                    }
                    catch { }
                }
            }
            return type;
        }


        public static string GetPublishedVersionNo
        {
            get
            {
                if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
                {
                    string publishVersionNo =
                    System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.Major.ToString()
                   + "." + System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.Minor.ToString()
                   + "." + System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.Build.ToString()
                   + "." + System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.Revision.ToString();
                    return publishVersionNo;
                }

                else return _AssemblyVersion;
            }
        }
        public static string PublishedVersionMajorMinor
        {
            get
            {
                if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
                {
                    return System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.Major.ToString()
                    + "." + System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.Minor.ToString();
                }

                return _AssemblyVersion;
            }
        }
        public static string GetApplicationTitle
        {
            get
            {
                return _AssemblyTitle + " " + PublishedVersionMajorMinor;
            }
        }



        #region Assembly Attribute Accessors
        //To change Assembly Attributes go Project Propertis->Applications->Assembly Information

        public static string _AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public static string _AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public static string _AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public static string _AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public static string _AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public static string _AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion
    }
}


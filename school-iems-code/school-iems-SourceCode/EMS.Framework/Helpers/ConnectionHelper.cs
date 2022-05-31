using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Web.Configuration;
using System.Xml;
using EMS.Framework.Utils;

namespace EMS.Framework.Helpers
{
    public class ConnectionHelper
    {
        public static bool CreateDatabase(string dbConnectionstring, out string error)
        {
            error = string.Empty;

            try
            {
                var context = HttpUtil.DbContext;
                context.Database.Connection.ConnectionString = dbConnectionstring;
                context.Database.CreateIfNotExists();
            }
            catch (Exception exception)
            {
                //error = "OOPS! Something Went Wrong.Please Contact With Administrator";
                error = exception.Message;
                return false;
            }

            return true;
        }

        public static bool SaveConnection(string cmsconnectionstring, string dbConnectionstring, out string error)
        {
            error = string.Empty;

            try
            {
                var configuration = WebConfigurationManager.OpenWebConfiguration("~");
                var section = (ConnectionStringsSection)configuration.GetSection("EmsDbContext");
                section.ConnectionStrings[cmsconnectionstring].ConnectionString = dbConnectionstring;
                configuration.Save();
            }
            catch (Exception exception)
            {
               // error = "OOPS! Something Went Wrong.Please Contact With Administrator";
                error = exception.Message;
                return false;
            }

            return true;
        }
    }
}
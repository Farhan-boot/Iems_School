using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Routing;
using System.Web.Security;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.Framework.Objects;
using EMS.Framework.Settings;

using Microsoft.Web.Administration;
//using Microsoft.Web.Administration;
using Newtonsoft.Json;

namespace EMS.Framework.Utils
{
    public static class HttpUtil
    {
        public static void Abandon()
        {
            if (HttpContext.Current.Session != null) HttpContext.Current.Session.Abandon();
            FormsAuthentication.SignOut();
        }
        public static void SignOutToLoginPage()
        {
            if (HttpContext.Current.Session != null) HttpContext.Current.Session.Abandon();
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
            //HttpContext.Current.Response.Redirect("~/Login/Logout");
        }

        private static EmsDbContext _dbContext2 = null;
        public static EmsDbContext DbContext
        {
            get
            {
                EmsDbContext   _dbContext = null;
                //if (_dbContext == null)
                {
                    try
                    {
                        _dbContext = new EmsDbContext();
                        _dbContext.Database.CommandTimeout = 180;
                        _dbContext.Configuration.LazyLoadingEnabled = false;
                        _dbContext.Configuration.ProxyCreationEnabled = false;
                        //dbContext.Configuration.AutoDetectChangesEnabled = false;
                        //dbContext.Configuration.ValidateOnSaveEnabled = false;
                    }
                    catch (Exception)
                    {
                        //todo log error
                    }
                }

                return _dbContext;
            }
        }

        /// <summary>
        /// This is Id of employee table, not User Table.
        /// </summary>
        //public static long EmployeeId
        //{
        //    get
        //    {
        //        long id = 0;
        //        var singleOrDefault = DbContext.User_Employee
        //            .SingleOrDefault(v => v.UserId == Profile.UserId);
        //        if (singleOrDefault != null)
        //        {
        //            id = singleOrDefault.Id;
        //        }
        //        return id;
        //    }

        //}
        //public static long GetStudentId()
        //{
        //    long id = 0;
        //    var singleOrDefault = DbContext.User_Student
        //        .SingleOrDefault(v => v.UserId == Profile.UserId);
        //    if (singleOrDefault != null)
        //    {
        //        id = singleOrDefault.Id;
        //    }
        //    return id;
        //}
        public static string GetProfilePictureUrl()
        {
            return GetProfilePictureUrlByUserId(Profile.UserId, Profile.UserTypeId);

        }
        public static string GetBase64String(string filePath)
        {
            try
            {
                if (!filePath.IsValid() || !DiskStorageSettings.RootStoragePathForProfilePictures.IsValid())
                    return null;

                var fullFilePath = Path.Combine(DiskStorageSettings.RootStoragePathForProfilePictures, filePath);

                if (!File.Exists(fullFilePath))
                    return null;

                return "data:image/jpeg;base64,"+Convert.ToBase64String(File.ReadAllBytes(fullFilePath));
            }
            catch (Exception e)
            {
                return null;
            }

        }
        public static string GetProfilePictureUrlByUserId(long userId, int userTypeEnumId)
        {
            string dir = "D:/Files/EMS/ems-published/";
            string url = "~/assets/img/avatars/BlankPerson.png";
            if (userTypeEnumId == (int)User_Account.UserTypeEnum.Student)
            {
                var profile = DbContext.User_Student
                    .Include(x=>x.User_Account)
                    .Include("User_Account.HR_Department")
                    .SingleOrDefault(v => v.UserId == userId);
                if (profile != null)
                {
                    var classRoll = profile.ClassRollNo.ToString();
                    if (classRoll.Length == 9)
                    {
                        if (classRoll.GetLast(3).ToInt32() >= 100)
                        {
                            url = "~/Uploads/ProfilePictures/Student/Official/" +
                                profile.RegistrationSession + "/" +
                                profile.User_Account.HR_Department.ShortName + "/" +
                                profile.ClassRollNo.ToString().GetLast(3) + ".jpg";
                        }
                        else
                        {
                            url = "~/Uploads/ProfilePictures/Student/Official/" +
                                profile.RegistrationSession + "/" +
                                profile.User_Account.HR_Department.ShortName + "/" +
                                profile.ClassRollNo.ToString().GetLast(2) + ".jpg";
                        }
                    }
                }
            }
            else if (userTypeEnumId == (int)User_Account.UserTypeEnum.Employee)
            {
                url = "~/Uploads/ProfilePictures/Employee/" + userId + ".jpg";
            }
            var fullPath = dir + url;
            fullPath = fullPath.Replace("~/", "");
            if (!File.Exists(fullPath))
            {
                url = "~/assets/img/avatars/BlankPerson.png";
            }
            return url;
        }

        public static EmsDbContext DbContextSession
        {
            get
            {
                EmsDbContext dbContext = null;
                try
                {

                    var con = HttpContext.Current;
                    dbContext = con.Items["EMS.EmsContext"] as EmsDbContext;
                    if (dbContext != null)
                    {
                        return dbContext;
                    }


                    dbContext = new EmsDbContext();
                    dbContext.Configuration.LazyLoadingEnabled = false;
                    dbContext.Configuration.ProxyCreationEnabled = false;

                    HttpContext.Current.Items.Add("EMS.EmsContext", dbContext);


                }
                catch (Exception)
                {


                }
                finally
                {
                    //performance tuning
                    if (dbContext != null)
                    {
                        //dbContext.Configuration.LazyLoadingEnabled = false;
                        //dbContext.Configuration.ProxyCreationEnabled = false;
                        //dbContext.Configuration.AutoDetectChangesEnabled = false;
                        //dbContext.Configuration.ValidateOnSaveEnabled = false;
                        //this.Configuration.AutoDetectChangesEnabled = false;

                        //context.CustomerOrders.AsNoTracking()
                        //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
                        //modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
                        //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

                    }
                }
                return dbContext;
            }
        }

        public static string GetQueryString(string key)
        {
            return HttpContext.Current.Request.GetQueryString(key);
        }
        public static UserProfile GetSuperUserProfile()
        {
            return new UserProfile()
            {
                Name = "System Administrator",
                UserId = SuperAdminId,
                UserName = SuperAdminUserName
            };
        }

        public static UserProfile Profile
        {
            get
            {
                var currentContext = HttpContext.Current;
                if (HttpContext.Current == null)
                    return null;

                if (!currentContext.User.Identity.IsAuthenticated)
                {
                    //throw new HttpResponseException(HttpStatusCode.Unauthorized);
                    return null;
                }
                //commented by pavel, should be implemented letter
                //var profile = con.Items["EMS.UserProfile"] as UserProfile;
                //if (profile != null)
                //    return profile;
                //profile = GetTempFrofile();
                //HttpContext.Current.Items.Add("EMS.UserProfile", profile);
                //return profile;

                var ticket = ((FormsIdentity)currentContext.User.Identity).Ticket;
                try
                {
                    var up = JsonConvert.DeserializeObject<UserProfile>(ticket.UserData);
                    if (up == null)
                    {
                        //SignOutToLoginPage();
                    }

                    if (up != null)
                    {
                        up.LoginTime = ticket.IssueDate;
                    }

                    return up;
                }
                catch (Exception)
                {
                    //SignOutToLoginPage();
                }

                return null;
            }
        }

        public const string SuperAdminUserName = "psdeveloper";
        public const int SuperAdminId = 1;
        public static bool IsSupperAdmin()
        {
            bool isSuperAdmin = Profile.UserName.Equals(SuperAdminUserName, StringComparison.InvariantCultureIgnoreCase) || Profile.UserId== SuperAdminId;
           // isSuperAdmin = DbContext.UAC_RoleUserMap.Any(
            //    x => x.Id == Profile.UserId
            //    && x.RoleId == 1);
            return isSuperAdmin;
        }

        public static bool IsSupperAdmin( string username, long id )
        {
            bool isSuperAdmin = SuperAdminUserName.Equals(SuperAdminUserName, StringComparison.InvariantCultureIgnoreCase) || id == SuperAdminId;
            return isSuperAdmin;
        }

        /*public static DbblPaymentProcessor OnlinePaymentProfile
        {
            get
            {
                DbblPaymentProcessor paymentObject = null;
                try
                {
                    // var Session = HttpContext.Current.Session;
                    paymentObject = HttpContext.Current.Session["EMS.OnlinePaymentObject"] as DbblPaymentProcessor;
                    if (paymentObject != null)
                    {

                        return paymentObject;
                    }
                    //paymentObject = new DbblGateway();
                    //HttpContext.Current.Items.Add("EMS.OnlinePaymentObject", paymentObject);
                }
                catch (Exception)
                {

                }

                return paymentObject;
            }
            set
            {

                HttpContext.Current.Session["EMS.OnlinePaymentObject"] = value;

            }
        }*/
        #region Bult In Code Property
        public static Array GetAreaList()
        {
            var areaList = RouteTable.Routes.OfType<Route>()
                .Where(d => d.DataTokens != null && d.DataTokens.ContainsKey("area"))
                .Select(r => r.DataTokens["area"])
                .ToArray();
            Array.Sort(areaList);
            return areaList;
        }
        public static string GetCurrentArea()
        {
            string area = HttpContext.Current.Request.RequestContext.RouteData.DataTokens.ContainsKey("area")
                ? HttpContext.Current.Request.RequestContext.RouteData.DataTokens["area"].ToString()
                : "";
            return area;
        }
        public static string GetCurrentController()
        {
            string controller = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();
            return controller;
        }
        public static string GetCurrentAction()
        {
            string action = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();
            return action;
        }

        #endregion
        public static string GetUserIp()
        {
            string visitorsIpAddress = string.Empty;

            //client localIp
            //IPHostEntry host = default(IPHostEntry);
            //string hostname = null;
            //hostname = System.Environment.MachineName;
            //host = Dns.GetHostEntry(hostname);
            //foreach (IPAddress ip in host.AddressList)
            //{
            //    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            //    {
            //        visitorsIpAddress = Convert.ToString(ip);
            //    }
            //}

            if (HttpContext.Current==null)
            {
                return visitorsIpAddress;
            }

            if ( !string.IsNullOrEmpty(HttpContext.Current?.Request?.UserHostAddress))
            {
                visitorsIpAddress = HttpContext.Current.Request.UserHostAddress;
            }
            else if (HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"] != null)
            {
                visitorsIpAddress = HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"];
            }
            else if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                visitorsIpAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            }
            else
            {
                visitorsIpAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            if (visitorsIpAddress.Equals(string.Empty) || visitorsIpAddress == "::1")
            {
                visitorsIpAddress = "127.0.0.1";
            }
            return visitorsIpAddress;
        }

        public static HttpBrowserCapabilities GetUserBrowser()
        {
            HttpBrowserCapabilities browserCapabilities = HttpContext.Current.Request.Browser;
            return browserCapabilities;
        }
        public static string GetUserBrowserInfo()
        {
            HttpBrowserCapabilities browserCapabilities = HttpContext.Current.Request.Browser;
            var visitorsBrowserInfo = browserCapabilities.Browser
                + ";" + browserCapabilities.Version
                + ";" + browserCapabilities.Beta
                + ";" + browserCapabilities.Id
                + ";" + browserCapabilities.Cookies
                + ";" + browserCapabilities.JavaScript
                + ";" + browserCapabilities.JScriptVersion
                + ";" + browserCapabilities.JavaApplets
                + ";" + browserCapabilities.SupportsCss
                + ";" + browserCapabilities.ActiveXControls
                + ";" + browserCapabilities.AOL
                + ";" + browserCapabilities.Crawler
                + ";" + browserCapabilities.ClrVersion
                + ";" + browserCapabilities.CDF
                + ";" + browserCapabilities.CanSendMail
                + ";" + browserCapabilities.CanInitiateVoiceCall
                + ";" + browserCapabilities.BackgroundSounds
                + ";" + browserCapabilities.Platform
                + ";" + browserCapabilities.IsColor
                + ";" + browserCapabilities.IsMobileDevice
                + ";" + browserCapabilities.MobileDeviceManufacturer
                + ";" + browserCapabilities.MobileDeviceModel
                + ";" + browserCapabilities.NumberOfSoftkeys
                + ";" + browserCapabilities.ScreenBitDepth
                + ";" + browserCapabilities.ScreenCharactersWidth
                + ";" + browserCapabilities.ScreenCharactersHeight
                + ";" + browserCapabilities.ScreenPixelsWidth
                + ";" + browserCapabilities.ScreenPixelsHeight
                + ";" + browserCapabilities.Type;
            return visitorsBrowserInfo;
        }

        /// <summary>
        ///  Gets the MAC address of the current PC.
        /// </summary>
        /// <returns></returns>
        public static string GetUserMacAddress()
        {
            var userMac = string.Empty;
            var mac = GetMacAddress();
            if (mac != null)
            {
                userMac = mac.ToString().InsertString('-');
            }
            return userMac;
        }

        private static PhysicalAddress GetMacAddress()
        {
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                // Only consider Ethernet network interfaces
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
                    nic.OperationalStatus == OperationalStatus.Up)
                {
                    return nic.GetPhysicalAddress();
                }
            }
            return null;
        }

        public static bool IsMobileBrowser()
        {
            //GETS THE CURRENT USER CONTEXT
            HttpContext context = HttpContext.Current;

            //FIRST TRY BUILT IN ASP.NT CHECK
            if (context.Request.Browser.IsMobileDevice)
            {
                return true;
            }
            //THEN TRY CHECKING FOR THE HTTP_X_WAP_PROFILE HEADER
            if (context.Request.ServerVariables["HTTP_X_WAP_PROFILE"] != null)
            {
                return true;
            }
            //THEN TRY CHECKING THAT HTTP_ACCEPT EXISTS AND CONTAINS WAP
            if (context.Request.ServerVariables["HTTP_ACCEPT"] != null &&
                context.Request.ServerVariables["HTTP_ACCEPT"].ToLower().Contains("wap"))
            {
                return true;
            }
            //AND FINALLY CHECK THE HTTP_USER_AGENT 
            //HEADER VARIABLE FOR ANY ONE OF THE FOLLOWING
            if (context.Request.ServerVariables["HTTP_USER_AGENT"] != null)
            {
                //Create a list of all mobile types
                string[] mobiles =
                    new[]
                        {
                    "midp", "j2me", "avant", "docomo",
                    "novarra", "palmos", "palmsource",
                    "240x320", "opwv", "chtml",
                    "pda", "windows ce", "mmp/",
                    "blackberry", "mib/", "symbian",
                    "wireless", "nokia", "hand", "mobi",
                    "phone", "cdm", "up.b", "audio",
                    "SIE-", "SEC-", "samsung", "HTC",
                    "mot-", "mitsu", "sagem", "sony"
                    , "alcatel", "lg", "eric", "vx",
                    "NEC", "philips", "mmm", "xx",
                    "panasonic", "sharp", "wap", "sch",
                    "rover", "pocket", "benq", "java",
                    "pt", "pg", "vox", "amoi",
                    "bird", "compal", "kg", "voda",
                    "sany", "kdd", "dbt", "sendo",
                    "sgh", "gradi", "jb", "dddi",
                    "moto", "iphone"
                        };

                //Loop through each item in the list created above 
                //and check if the header contains that text
                foreach (string s in mobiles)
                {
                    if (context.Request.ServerVariables["HTTP_USER_AGENT"].
                                                        ToLower().Contains(s.ToLower()))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool IsSearchBot()
        {
            if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Request != null)
            {
                string userAgent = System.Web.HttpContext.Current.Request.UserAgent;
                if (string.IsNullOrEmpty(userAgent))
                {
                    return false;
                }

                userAgent = userAgent.ToLower();

                string[] userAgents = new string[]
                {
            "googlebot",
            "bingbot",
            "msnbot",
            "yahoo! slurp",
            "baiduspider",
            "iaskspider",
            "ask jeeves"
                };

                foreach (string agent in userAgents)
                {
                    if (userAgent.Contains(agent))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static string GetWelcomeMessageToUser()
        {
            var message = string.Empty;
            //5AM-12PM
            if (DateTime.Now.Hour >= 5 && DateTime.Now.Hour < 12)
            {
                message = "Good Morning";
            }
            //12PM-3PM
            if (DateTime.Now.Hour >= 12 && DateTime.Now.Hour < 15)
            {
                message = "Good Noon";
            }
            //3PM-5PM
            if (DateTime.Now.Hour >= 15 && DateTime.Now.Hour < 17)
            {
                message = "Good Afternoon";
            }
            //5PM-10PM
            if (DateTime.Now.Hour >= 17 && DateTime.Now.Hour < 22)
            {
                message = "Good Evening";
            }
            //10PM-12PM
            if (DateTime.Now.Hour >= 22 && DateTime.Now.Hour < 24)
            {
                message = "Good Night";
            }
            //12AM-2AM
            if (DateTime.Now.Hour >= 24 && DateTime.Now.Hour < 2)
            {
                message = "Good Midnight";
            }
            //2AM-4AM
            if (DateTime.Now.Hour >= 2 && DateTime.Now.Hour < 4)
            {
                message = "Good Late Night";
            }
            //4AM-5AM
            if (DateTime.Now.Hour >= 4 && DateTime.Now.Hour < 5)
            {
                message = "Good Down";
            }
            return message;
        }

        //public static string CurrentCulture
        //{
        //    get
        //    {
        //        var cookie = HttpContext.Current.Request.Cookies["LanguageCookie"];
        //        return cookie == null ? "" : cookie.Value;
        //    }
        //    set
        //    {
        //        Thread.CurrentThread.CurrentCulture = new CultureInfo(value);
        //        Thread.CurrentThread.CurrentUICulture = new CultureInfo(value);

        //        var cookie = new HttpCookie("LanguageCookie")
        //                     {
        //                         Value = value, 
        //                         Expires = DateTime.Now.Add(FormsAuthentication.Timeout),
        //                         Secure = FormsAuthentication.RequireSSL
        //                     };
        //        HttpContext.Current.Response.Cookies.Remove("LanguageCookie");
        //        HttpContext.Current.Response.Cookies.Add(cookie);

        //    }
        //}



        //public static void InitializeCulture()
        //{
        //    string lang = CurrentCulture;
        //    var db = new CrsContex();
        //    if (string.IsNullOrEmpty(lang))
        //    {
        //        CurrentCulture = lang = "en-US";
        //        var obj = db.ClientDomains.SingleOrDefault(cd => cd.DomainName == HttpContext.Current.Request.Url.Host);

        //        if (obj != null)
        //        {
        //            var client = db.Clients.SingleOrDefault(c => c.ID == obj.ClientID);
        //            if (client != null)
        //            {
        //                CurrentCulture = lang = client.DefaultCulture;
        //            }
        //        }
        //    }

        //    Thread.CurrentThread.CurrentCulture = new CultureInfo(lang);
        //    Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);

        //}

        public static void OperationTerminated()
        {
            if (SiteSettings.Instance.IsOperationTerminated)
            {
                //Cache index full for huge data, Operation terminated.
                throw new Exception(SiteSettings.Instance.OperationTerminationMessage);
            }
        }
    }
}

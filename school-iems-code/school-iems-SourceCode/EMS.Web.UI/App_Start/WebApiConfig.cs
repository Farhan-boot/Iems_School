using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EMS.Web.UI
{
    public static class WebApiConfig
    {
        public static string UrlPrefix { get { return "api/"; } }
        public static string UrlPrefixRelative { get { return "~/api/"; } }
        public static void Register(HttpConfiguration config)
        {
            // Remove the JSON formatter
            //config.Formatters.Remove(config.Formatters.JsonFormatter);
            // or
            // Remove the XML formatter
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            // Web API configuration and services
            // Web API custom routes
            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{area}/{controller}/{action}/{id}",
                new { area="", id = RouteParameter.Optional}
            );
            // Uncomment the following line of code to enable query support for actions with an IQueryable or IQueryable<T> return type.
            // To avoid processing unexpected or malicious queries, use the validation settings on QueryableAttribute to validate incoming queries.
            // For more information, visit http://go.microsoft.com/fwlink/?LinkId=279712.
            //config.EnableQuerySupport();
            //// Web API configuration and services
            //// Web API routes
            //config.MapHttpAttributeRoutes();
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }
        public static bool IsWebApiRequest()
        {
            return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath != null && HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath.StartsWith(WebApiConfig.UrlPrefixRelative);
        }
    }
}

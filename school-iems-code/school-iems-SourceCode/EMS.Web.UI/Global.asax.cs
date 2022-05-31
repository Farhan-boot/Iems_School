using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.SessionState;
using EMS.Framework.Attributes;

namespace EMS.Web.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
        //json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.All;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

           

            //ViewEngines.Engines.Add(new DynamicBundles.DynamicBundlesViewEngine());
        }

        protected void Application_PostAuthorizeRequest()
        {
            //enable session in webapi
            //if (WebApiConfig.IsWebApiRequest())
            //{    
            //    HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
            //}
        }



    }
}

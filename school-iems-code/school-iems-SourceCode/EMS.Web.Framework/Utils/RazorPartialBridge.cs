using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using EMS.CoreLibrary;
using EMS.Framework.Attributes;


namespace EMS.Web.Framework.Utils
{

    public class RazorPartialBridge
    {
        public class WebFormShimController : Controller { }
        public static void RenderPartial(string partialName, object model=null)
        {
            //get a wrapper for the legacy WebForm context
            var httpCtx = new HttpContextWrapper(System.Web.HttpContext.Current);

            //create a mock route that points to the empty controller
            var rt = new RouteData();
            rt.Values.Add("controller", "WebFormShimController");

            //create a controller context for the route and http context
            var ctx = new ControllerContext(
                new RequestContext(httpCtx, rt), new WebFormShimController());

            //find the partial view using the viewengine
            var view = ViewEngines.Engines.FindPartialView(ctx, partialName).View;
             if (view == null)
                return;
            //create a view context and assign the model
            ViewContext vctx;
            if (model!=null)
            {
                 vctx = new ViewContext(ctx, view,
               new ViewDataDictionary { Model = model },
               new TempDataDictionary(), httpCtx.Response.Output);
            }
            else
            {
                vctx = new ViewContext(ctx, view,
              new ViewDataDictionary { Model = null },
              new TempDataDictionary(), httpCtx.Response.Output);
            }
           

            //render the partial view
            view.Render(vctx, System.Web.HttpContext.Current.Response.Output);
        }
    }
}

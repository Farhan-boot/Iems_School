using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

/// <summary>
/// AngularJS Web Api AntiForgeryToken CSRF 
/// http://stackoverflow.com/questions/32460196/angularjs-web-api-antiforgerytoken-csrf
/// </summary>
namespace EMS.Framework.Attributes
{
    public sealed class ValidateWebApiAntiForgery2 : ActionFilterAttribute
    {
        public override void OnActionExecuting(
            HttpActionContext actionContext)
        {
            if (actionContext == null)
            {
                throw new ArgumentNullException("actionContext");
            }

            if (actionContext.Request.Method.Method != "GET")
            {
                var headers = actionContext.Request.Headers;
                var tokenCookie = headers
                    .GetCookies()
                    .Select(c => c[AntiForgeryConfig.CookieName])
                    .FirstOrDefault();

                var tokenHeader = GetTokenHeader(actionContext);
               

                AntiForgery.Validate(
                    tokenCookie != null ? tokenCookie.Value : null, tokenHeader);
            }

            base.OnActionExecuting(actionContext);
        }

        private string GetTokenHeader(HttpActionContext actionContext)
        {
            var headers = actionContext.Request.Headers;
            var tokenHeader = string.Empty;

            IEnumerable<string> xXsrfHeaders;
            if (headers.TryGetValues("X-XSRF-Token", out xXsrfHeaders))
            {
                 tokenHeader = xXsrfHeaders.FirstOrDefault();
            }
            return tokenHeader;
        }
    }
}
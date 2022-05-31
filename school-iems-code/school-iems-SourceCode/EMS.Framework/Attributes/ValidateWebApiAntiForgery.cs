using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using EMS.CoreLibrary.Helpers;

/// <summary>
/// http://kamranicus.com/blog/posts/70/protip-using-anti-forgery-token-with-aspnet-web-ap/
/// </summary>

namespace EMS.Framework.Attributes
{

    /// <summary>
    /// Validates Anti-Forgery CSRF tokens for Web API
    /// </summary>
    /// <remarks>
    /// See MVC 4 SPA template
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class ValidateWebApiAntiForgery : FilterAttribute, IAuthorizationFilter
    {
        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            HttpRequestMessage request = actionContext.Request;
            try
            {
                //don't check anti forgery on debug mode
                if (HttpContext.Current!=null && HttpContext.Current.IsDebuggingEnabled)
                    return continuation();

                ValidateRequestHeader(request);
                //if (IsAjaxRequest(request))
                //{    
                //}
                //else {
                //    AntiForgery.Validate();
                //}
            }
            catch (Exception ex)
            {
                //LogManager.GetCurrentClassLogger().Warn("Anti-XSRF Validation Failed", ex);
                actionContext.Response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    RequestMessage = actionContext.ControllerContext.Request ,
                    Content = new StringContent(ex.Message.ToString())
                };
                return FromResult(actionContext.Response);
            }
            return continuation();
        }
        private Task<HttpResponseMessage> FromResult(HttpResponseMessage result)
        {
            var source = new TaskCompletionSource<HttpResponseMessage>();
            source.SetResult(result);
            return source.Task;
        }
        private void ValidateRequestHeader(HttpRequestMessage request)
        {
            var headers = request.Headers;
            var cookie = headers
                .GetCookies()
                .Select(c => c[AntiForgeryConfig.CookieName])
                .FirstOrDefault();

            var tokenHeader = GetTokenHeader(request);
            if (tokenHeader.IsValid())
            {
                if (cookie == null)
                {
                    throw new InvalidOperationException($"Invalid Anti Forgery Token, Hack Attempt Logged!");
                }
                AntiForgery.Validate(cookie.Value, tokenHeader);
            }
            else {
                var headerBuilder = new StringBuilder();
                headerBuilder.AppendLine("Missing Forgery Token HTTP header:");
                foreach (var header in headers)
                {
                    headerBuilder.AppendFormat("- [{0}] = {1}", header.Key, header.Value);
                    headerBuilder.AppendLine();
                }
                throw new InvalidOperationException("Invalid Anti Forgery Token, Hack Attempt Logged!");
            }
        }

        private string GetTokenHeader(HttpRequestMessage request)
        {
            var headers = request.Headers;
            var tokenHeader = string.Empty;
            //if (headers.Contains("X-XSRF-Token"))
            //{
            //    tokenHeader = headers.GetValues("X-XSRF-Token").FirstOrDefault();
            //}
            IEnumerable<string> xXsrfHeaders;

            if (headers.TryGetValues("X-XSRF-Token", out xXsrfHeaders))
            {
                tokenHeader = xXsrfHeaders.FirstOrDefault();
            }

            return tokenHeader;
        }

        private bool IsAjaxRequest(HttpRequestMessage request)
        {
            IEnumerable<string> xRequestedWithHeaders;
            if (request.Headers.TryGetValues("X-Requested-With", out xRequestedWithHeaders))
            {
                string headerValue = xRequestedWithHeaders.FirstOrDefault();
                if (!String.IsNullOrEmpty(headerValue))
                {
                    return String.Equals(headerValue, "XMLHttpRequest", StringComparison.OrdinalIgnoreCase);
                }
            }

            return false;
        }


    }
}
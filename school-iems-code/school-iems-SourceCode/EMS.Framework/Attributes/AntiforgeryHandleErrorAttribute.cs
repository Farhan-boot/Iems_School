﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using EMS.Framework.Utils;
/// <summary>
/// 
/// http://stackoverflow.com/questions/12967917/a-way-of-properly-handling-httpantiforgeryexception-in-mvc-4-application
/// </summary>
namespace EMS.Framework.Attributes
{
    [SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes",
        Justification = "This attribute is AllowMultiple = true and users might want to override behavior.")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class LoginAntiforgeryHandleErrorAttribute : FilterAttribute, IExceptionFilter
    {
        #region Implemented Interfaces

        #region IExceptionFilter

        /// <summary>
        /// </summary>
        /// <param name="filterContext">
        /// The filter context.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public virtual void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            if (filterContext.IsChildAction)
            {
                return;
            }

            // If custom errors are disabled, we need to let the normal ASP.NET exception handler
            // execute so that the user can see useful debugging information.
            if (filterContext.ExceptionHandled) //|| !filterContext.HttpContext.IsCustomErrorEnabled
            {
                return;
            }

            Exception exception = filterContext.Exception;

            // If this is not an HTTP 500 (for example, if somebody throws an HTTP 404 from an action method),
            // ignore it.
            if (new HttpException(null, exception).GetHttpCode() != 500)
            {
                return;
            }

            // check if antiforgery
            if (!(exception is  HttpAntiForgeryException))
            {
                return;
            }
            HttpUtil.Abandon();
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                    { "action", "Index" },
                    { "controller", "Login" }
                });

            filterContext.ExceptionHandled = true;
        }

        #endregion

        #endregion
    }
}
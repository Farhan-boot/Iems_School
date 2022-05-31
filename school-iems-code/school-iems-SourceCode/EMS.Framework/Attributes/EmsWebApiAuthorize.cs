using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.Framework.Settings;
using EMS.Framework.Utils;
using FilterAttribute = System.Web.Http.Filters.FilterAttribute;
using IAuthorizationFilter = System.Web.Http.Filters.IAuthorizationFilter;


namespace EMS.Framework.Attributes
{
    /// <summary>
    ///  Authorize Web API
    /// </summary>
    /// <remarks>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class EmsWebApiAuthorize : FilterAttribute, IAuthorizationFilter
    {
        private readonly User_Account.UserTypeEnum _type;
        public EmsWebApiAuthorize(User_Account.UserTypeEnum type)
        {
            _type = type;
        }
        public Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            HttpRequestMessage request = actionContext.Request;
            try
            {
                ValidateApiRequest(request);
            }
            catch (Exception ex)
            {
                //Log exception
                actionContext.Response = new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.Forbidden,
                    RequestMessage = actionContext.ControllerContext.Request,
                    Content = new StringContent(ex.Message)
                };
                AddMessageToHeader(ex.Message, request.Headers);

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
        private void ValidateApiRequest(HttpRequestMessage request)
        {
            //check HttpUtil.Profile !=null
            //if (HttpContext.Current.Session == null)
            //    throw new InvalidOperationException($"Your session has expired, try Login again!");

            if (HttpUtil.Profile == null)
            {
                throw new InvalidOperationException($"Your session has expired, try Login again!");
            }

            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                throw new InvalidOperationException($"Sorry you are not authorized for this request!");
            }

            var contex = HttpUtil.DbContext;

            var obj = contex.User_Account.Find(HttpUtil.Profile.UserId); //(ac => ac.UserName.Equals(HttpUtil.Profile.UserName))
            if (obj == null)
            {    //HttpUtil.Abandon();
                throw new InvalidOperationException($"Sorry you are not authorized for this request!");
            }

            //todo tempoary bKash security check solution
            if (!SiteSettings.Instance.IsMultiLoginAllow)
            {
                if (obj.LastLoginDate?.ToOnlyTime() != HttpUtil.Profile.LoginTime?.ToOnlyTime() || obj.LastLoginDate?.ToOnlyDate() != HttpUtil.Profile.LoginTime?.ToOnlyDate())
                {
                    HttpUtil.Abandon();
                    throw new InvalidOperationException($"Your session has expired, try Login again!");
                }
            }

            if (HttpUtil.Profile.InstituteKey != SiteSettings.InstituteKey)
            {
                HttpUtil.Abandon();
                throw new InvalidOperationException($"Wrong Institute. Your session has expired,try Login again!");
            }

            //Checking Pending Task and mvc redirect
            //redirect to change password 
            if (SiteSettings.Instance.IsForceChangeStudentPassword)
                if (!obj.PasswordSalt.IsValid())
                {
                    HttpUtil.Abandon();
                }
            //redirect to verify email
            if (SiteSettings.Instance.IsForceVarifyStudentEmail)
                if (!obj.UserEmail.IsValidEmail() || !obj.IsVerifiedUserEmaill)
                {
                    HttpUtil.Abandon();
                }

            ////redirect to verify mobile
            if (!obj.UserMobile.IsValidMobileForBD() || !obj.IsVerifiedUserMobile)
            {
                HttpUtil.Abandon();
            }
            //Checking Pending Task and mvc redirect end



            bool isStudentAllowed = false;
            bool isEmployeeAllowed = false;
            bool isGuardianAllowed = false;
            bool isApplicantAllowed = false;

            if (_type == User_Account.UserTypeEnum.Student)
                isStudentAllowed = true;
            if (_type == User_Account.UserTypeEnum.Employee)
                isEmployeeAllowed = true;

            if (_type == User_Account.UserTypeEnum.Guardian)
                isGuardianAllowed = true;
            if (_type == User_Account.UserTypeEnum.Applicant)
                isApplicantAllowed = true;


            //bool isStudentAllowed = (_type & User_Account.UserAuthorizeTypeEnum.Student) > 0;
            //bool isEmployeeAllowed = (_type & User_Account.UserAuthorizeTypeEnum.Employee) > 0;
            //bool isGuardianAllowed = (_type & User_Account.UserAuthorizeTypeEnum.Guardian) > 0;
            //bool isApplicantAllowed = (_type & User_Account.UserAuthorizeTypeEnum.Applicant) > 0;

            bool isAllowed = false;

            switch (obj.UserTypeEnumId)
            {
                case (int)User_Account.UserTypeEnum.Student:
                    isAllowed = isStudentAllowed;
                    break;
                case (int)User_Account.UserTypeEnum.Employee:
                    isAllowed = isEmployeeAllowed;
                    break;
                case (int)User_Account.UserTypeEnum.Guardian:
                    isAllowed = isGuardianAllowed;
                    break;
                case (int)User_Account.UserTypeEnum.Applicant:
                    isAllowed = isApplicantAllowed;
                    break;
            }
            if (!isAllowed)
            {
                throw new InvalidOperationException($"Sorry you are not authorized for this request!");
            }
        }
        private void AddMessageToHeader(string message, HttpRequestHeaders headers)
        {
            var headerBuilder = new StringBuilder();
            headerBuilder.AppendLine($"{message} HTTP header:");
            foreach (var header in headers)
            {
                headerBuilder.AppendFormat("- [{0}] = {1}", header.Key, header.Value);
                headerBuilder.AppendLine();
            }

        }
    }
}
using System;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using EMS.CoreLibrary;
using EMS.CoreLibrary.Helpers;
using EMS.DataAccess.Data;
using EMS.Framework.Settings;
using EMS.Framework.Utils;
using Microsoft.Web.Administration;

//using System.Web.Http.Filters;

namespace EMS.Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class EmsMvcAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        private readonly User_Account.UserTypeEnum _type;

        public EmsMvcAuthorizeAttribute(User_Account.UserTypeEnum type)
        {
            _type = type;
        }
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                //if (HttpContext.Current.Session == null)
                //    throw new InvalidOperationException($"Your session has expired, try Login again!");
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                    //HttpUtil.SignOutToLoginPage();
                    //throw new HttpResponseException(HttpStatusCode.Unauthorized);
                    return;
                }
                var contex = HttpUtil.DbContext;
                //check HttpUtil.Profile !=null
                if (HttpUtil.Profile == null)
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                    //Todo need to set message header
                    HttpUtil.Abandon();
                    return;
                }
                var obj = contex.User_Account.Find(HttpUtil.Profile.UserId);//.SingleOrDefault(ac => ac.UserName.Equals(HttpUtil.Profile.UserName));
                if (obj == null)
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                    //Todo need to set message header
                    HttpUtil.Abandon();
                    return;
                }

                //todo tempoary bKash security check solution
                if (!SiteSettings.Instance.IsMultiLoginAllow)
                {
                    if (obj.LastLoginDate?.ToOnlyTime() != HttpUtil.Profile.LoginTime?.ToOnlyTime() ||
                        obj.LastLoginDate?.ToOnlyDate() != HttpUtil.Profile.LoginTime?.ToOnlyDate())
                    {
                        filterContext.Result = new HttpUnauthorizedResult();

                        HttpUtil.Abandon();
                        return;
                    }
                }

                if (HttpUtil.Profile.InstituteKey != SiteSettings.InstituteKey)
                {
                    filterContext.Result = new HttpUnauthorizedResult();

                    HttpUtil.Abandon();
                    return;
                }


                //Checking Pending Task and mvc redirect
                //redirect to change password 
                if (SiteSettings.Instance.IsForceChangeStudentPassword)
                    if (!obj.PasswordSalt.IsValid())
                    {
                        HttpUtil.SignOutToLoginPage();
                    }
                //redirect to verify email

                if (SiteSettings.Instance.IsForceVarifyStudentEmail)
                    if (!obj.UserEmail.IsValidEmail() || !obj.IsVerifiedUserEmaill)
                    {
                        HttpUtil.SignOutToLoginPage();
                    }

                //redirect to verify mobile
                if (!obj.UserMobile.IsValidMobileForBD() || !obj.IsVerifiedUserMobile)
                {
                    HttpUtil.SignOutToLoginPage();
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
                    filterContext.Result = new HttpUnauthorizedResult();
                    //Todo need to set message header
                    //HttpUtil.Abandon();
                }


            }
            catch (Exception ex)
            {
                //todo log exception, its may occur if database connection failed
                filterContext.Result = new HttpUnauthorizedResult();
                HttpUtil.Abandon();
            }
        }
    }
}

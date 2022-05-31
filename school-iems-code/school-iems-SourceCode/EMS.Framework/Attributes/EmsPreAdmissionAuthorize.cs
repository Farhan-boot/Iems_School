using System;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using EMS.CoreLibrary;
using EMS.Framework.Utils;

namespace EMS.Framework.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    //[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    private class EmsPreAdmissionAuthorize : FilterAttribute, IAuthorizationFilter
    {       
        public EmsPreAdmissionAuthorize()
        {
             
        }
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult();
                Abandon();
                return;
            }

            var contex = HttpUtil.DbContext;
            bool isAllowed = false;
            if (HttpUtil.Profile.UserName.Contains("01"))
            {
                var studentCredentialByRule = contex.AdmUG_ApplicantByRule.SingleOrDefault(x => x.Id == HttpUtil.Profile.UserId);
                if (studentCredentialByRule == null)
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                    Abandon();
                    return;
                }
                isAllowed = true;
            }
            else {
                var studentCredential = contex.AdmUG_Applicant.SingleOrDefault(x => x.Id == HttpUtil.Profile.UserId);
                if (studentCredential == null)
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                    Abandon();
                    return;
                }
                //bool isAllowed = obj.ExamAttendanceUnitAStatus == EnumCollection.AdmissionUG.ExamAttendanceStatusEnum.Present;
                isAllowed = studentCredential.SelectedPassedInUnit != EnumCollection.AdmissionUG.FormUnitEnum.None;
            }

            if (!isAllowed) {
                filterContext.Result = new HttpUnauthorizedResult();
                Abandon();
            }
        }

        void Abandon()
        {
            HttpContext.Current.Session.Abandon();
            FormsAuthentication.SignOut();
            HttpContext.Current.Response.Redirect("~/PreAdmission/Login");
        }
    }
}

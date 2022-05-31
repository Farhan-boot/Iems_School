using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.DataAccess.Data;
using EMS.Framework.Attributes;

namespace EMS.Web.Framework.Bases.Mvc
{
    [EmsMvcAuthorize(User_Account.UserTypeEnum.Applicant)]
    public class ApplicantBaseController : BaseController
    {
    }
}

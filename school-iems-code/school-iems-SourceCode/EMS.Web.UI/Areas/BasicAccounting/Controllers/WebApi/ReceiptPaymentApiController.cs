using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EMS.CoreLibrary.Helpers;
using EMS.Framework.Objects;
using EMS.Web.Framework.Bases.WebApi;
using EMS.Web.Jsons.Models;

namespace EMS.Web.UI.Areas.BasicAccounting.Controllers.WebApi
{
    public class ReceiptPaymentApiController : EmployeeBaseApiController
    {
        public ReceiptPaymentApiController()
        { }

        #region Get Api
        public HttpListResult<BAcnt_LedgerJson> GetReceiptPaymentDataExtra()
        {
            string error = string.Empty;
            var result = new HttpListResult<BAcnt_LedgerJson>();
            try
            {

                var currentCompanyId = Facade.BasicAccountingFacade.CompanyFacade.GetCurrentCompanyId();

                var branchList = DbInstance.BAcnt_Branch
                    .Where(x => x.CompanyId == currentCompanyId && x.IsEnable && !x.IsDeleted)
                    .AsEnumerable().Select(t => new
                        { Id = t.Id, Name = t.Name }).ToList();

                result.DataExtra.BranchList = branchList;

                var startDate = DateTime.Now.ToOnlyDate();
                var endDate = DateTime.Now.ToOnlyDate();

                result.DataExtra.StartDate = startDate;
                result.DataExtra.EndDate = endDate;


            }
            catch (Exception ex)
            {
                result.HasError = true;
                result.Errors.Add(ex.GetBaseException().Message.ToString());
            }
            return result;
        }
        #endregion

    }
}
using System.Collections.Generic;
using EMS.Framework.Settings;

namespace EMS.Framework.Objects
{
    public class ReportHeaderFooterModel
    {
       
        public string InstituteName { get; set; }
        public string InstituteAddress { get; set; }
        public string InstituteDomain { get; set; }
        public string ReportNo { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string HtmlLeftArea { get; set; }
        public string HtmlRightArea { get; set; }

        public ReportHeaderFooterModel():base()
        {
            InstituteName = SiteSettings.Instance.InstituteName;
            InstituteAddress = SiteSettings.Instance.InstituteAddress;
            InstituteDomain = SiteSettings.Instance.InstituteDomain;
            ReportNo= string.Empty;
            Title = string.Empty;
            Subtitle = string.Empty;

            HtmlLeftArea = $"<img class=\"logo-size\" src=\"{SiteSettings.Instance.InstituteLogoColorSolo}\" >";
            HtmlRightArea = $"";
        }
    }
    //public class MvcReportResult<T> : MvcReportResult
    //{

    //    public T Data { get; set; }
    //}
    //public class MvcReportListResult<T> : MvcReportResult
    //{
    //    public ICollection<T> Data { get; set; }
    //    public int Count { get; set; }
    //}


}

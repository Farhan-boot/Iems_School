using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Framework.Objects
{
    public class HttpResult<T> : HttpResult
    {
        public T Data { get; set; }
    }

    public class HttpListResult<T> : HttpResult
    {
        public ICollection<T> Data { get; set; }
        public int Count { get; set; }
    }

    public class HttpResult
    {
        public HttpResult()
        {
            DataExtra = new System.Dynamic.ExpandoObject();
            HasViewPermission = true;
        }
        private List<string> _errors = new List<string>();

        public bool HasError { get; set; }
        public bool HasViewPermission { get; set; }
        public dynamic DataExtra { get; set; }
        //public DynamicObject DataExtra { get; set; }

        public List<string> Errors
        {
            get { return _errors; }
            set { _errors = value; }
        }
        public string ErrorHtml
        {
            get
            {
                var errorMsg = "";
                foreach (var error in _errors)
                {
                    errorMsg += error + "<br>";
                }
                return errorMsg;
            }
        }

    }
}

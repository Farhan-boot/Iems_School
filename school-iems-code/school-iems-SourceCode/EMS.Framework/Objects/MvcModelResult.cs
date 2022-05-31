using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.CoreLibrary.Helpers;

namespace EMS.Framework.Objects
{
    public class MvcModelResult<T> : MvcModelResult
    {
        public T Data { get; set; }
    }

    public class MvcModelListResult<T> : MvcModelResult
    {
        public ICollection<T> Data { get; set; }
        public int Count { get; set; }
    }

    public class MvcModelResult
    {
        private List<string> _errors;
        public MvcModelResult()
        {
            DataBag = new System.Dynamic.ExpandoObject();
            _errors = new List<string>();
            HasViewPermission = true;
        }
        public bool HasError { get; set; }
        public bool HasViewPermission { get; set; }
        public dynamic DataBag { get; set; }
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
                    if (error.IsValid())
                    {
                        errorMsg += error + "<br>";

                    }
                }
                return errorMsg;
            }
        }


    }
}

using System;
using System.Collections.Generic;

using System.Diagnostics;
using System.IO;
using System.Linq;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;



namespace EMS.CoreLibrary.Helpers
{
    public static class HttpRequestHelper
    {

        //public static string GetQueryString(this HttpRequestMessage request, string key)
        //{
        //    // IEnumerable<KeyValuePair<string,string>> - right!
        //    var queryStrings = request.GetQueryNameValuePairs();
        //    if (queryStrings == null)
        //        return null;

        //    var match = queryStrings.FirstOrDefault(kv => string.Compare(kv.Key, key, true) == 0);
        //    if (string.IsNullOrEmpty(match.Value))
        //        return null;

        //    return match.Value;
        //}
        public static string GetQueryString(this HttpRequest request, string key)
        {
            if (request==null)
                return "";
            var queryStrings = request[key];
            if (queryStrings.IsValid())
                return queryStrings;
                return "";
        }
    }
}

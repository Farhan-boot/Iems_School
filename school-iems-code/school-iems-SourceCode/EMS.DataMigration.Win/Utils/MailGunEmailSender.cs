using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.IO;
using RestSharp;
using RestSharp.Authenticators;

namespace EMS.DataMigration.Win.Utils
{
    public class MailGunEmailSender
    {

        public static IRestResponse SendSimpleMessage()
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator =
                new HttpBasicAuthenticator("api",
                                            "67960a4216278589ad6ea1670fac58ed-e51d0a44-6343bcb4");
            RestRequest request = new RestRequest();
            request.AddParameter("domain", "ems.pipilikasoft.com", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "EUB No Reply <eub@ems.pipilikasoft.com>");
            request.AddParameter("to", "hkpavel@gmail.com");
            request.AddParameter("to", "mohitoshpm@gmail.com");
            request.AddParameter("subject", "New Course Metarial Uploaded!");
            request.AddParameter("text", "Testing some Mailgun awesomness!");
            request.Method = Method.POST;
            var res= client.Execute(request);
            return res;
        }
    }


  
}

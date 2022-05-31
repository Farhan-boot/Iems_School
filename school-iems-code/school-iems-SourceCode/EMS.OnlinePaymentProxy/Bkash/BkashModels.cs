using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PublicAdm.OnlinePaymentProxy.Bkash
{
    public static class BkashModels
    {
        #region BkashModels
        public class TokenModel
        {
            public string token_type { get; set; }
            public string expires_in { get; set; }
            public string id_token { get; set; }
            public string refresh_token { get; set; }
        }
        public class PaymentModel
        {
            public string id_token { get; set; }
            public string paymentID { get; set; }
            public string createTime { get; set; }
            public string updateTime { get; set; }
            public string orgLogo { get; set; }
            public string orgName { get; set; }
            public string transactionStatus { get; set; }
            public string amount { get; set; }
            public string currency { get; set; }
            public string intent { get; set; }
            public string merchantInvoiceNumber { get; set; }
            public int TransactionID { get; set; }
        }
        public class ExecutePaymentModel
        {
            public string paymentID { get; set; }
            public string createTime { get; set; }
            public string updateTime { get; set; }
            public string trxID { get; set; }
            public string transactionStatus { get; set; }
            public string amount { get; set; }
            public string currency { get; set; }
            public string intent { get; set; }
            public string merchantInvoiceNumber { get; set; }
            public string refundAmount { get; set; }
        }
        public class SearchResponseModel
        {
            public string amount { get; set; }
            public string completedTime { get; set; }
            public string currency { get; set; }
            public string customerMsisdn { get; set; }
            public string initiationTime { get; set; }
            public string organizationShortCode { get; set; }
            public string transactionReference { get; set; }
            public string transactionStatus { get; set; }
            public string transactionType { get; set; }
            public string trxID { get; set; }
        }
        public class ErrorModel
        {
            public string errorCode { get; set; }
            public string errorMessage { get; set; }
        }
        #endregion

        #region Configuration Model

        public class AllConfig
        {
            public Config Ju1st_Credential { get; set; }
            public Config Ju2nd_Credential { get; set; }
        }
        public class Config
        {
            public string createURL { get; set; }
            public string executeURL { get; set; }
            public string tokenURL { get; set; }
            public string queryUrl { get; set; }
            public string searchTrnxUrl { get; set; }
            public string script { get; set; }
            public string app_key { get; set; }
            public string proxy { get; set; }
            public string app_secret { get; set; }
            public string username { get; set; }
            public string password { get; set; }
            public string token { get; set; }
            public double fee { get; set; }
            public static Config GetNew()
            {
                return new Config()
                {
                    createURL = "https://checkout.sandbox.bka.sh/v1.2.0-beta/checkout/payment/create",
                    executeURL = "https://checkout.sandbox.bka.sh/v1.2.0-beta/checkout/payment/execute/",
                    tokenURL = "https://checkout.sandbox.bka.sh/v1.2.0-beta/checkout/token/grant",
                    queryUrl = "https://checkout.sandbox.bka.sh/v1.2.0-beta/checkout/payment/query/",
                    searchTrnxUrl = "https://checkout.sandbox.bka.sh/v1.2.0-beta/checkout/payment/search/",
                    script = "https://scripts.sandbox.bka.sh/versions/1.2.0-beta/checkout/bKash-checkout-sandbox.js",
                    app_key = "5nej5keguopj928ekcj3dne8p",
                    proxy = "N/A",
                    app_secret = "1honf6u1c56mqcivtc9ffl960slp4v2756jle5925nbooa46ch62",
                    username = "testdemo",
                    password = "test%#de23@msdao",
                    token = "N/A",
                    fee = 0
                };
            }
        }
        #endregion

        //#region Util Model
        //public static class ConfigDataUtil
        //{
        //    private static AllConfig s_Config;
        //    public static AllConfig Config
        //    {
        //        get { return loadConfig(); }
        //        set { s_Config = value; }
        //    }
        //    private static AllConfig loadConfig()
        //    {
        //        string ConfigPath = HttpContext.Current.Server.MapPath("~/Content/GatewayConfigaration/Config.json");
        //        using (StreamReader file = File.OpenText(ConfigPath))
        //        {
        //            JsonSerializer serializer = new JsonSerializer();
        //            return (AllConfig)serializer.Deserialize(file, typeof(AllConfig));
        //        }
        //    }
        //}
        //#endregion
    }

}

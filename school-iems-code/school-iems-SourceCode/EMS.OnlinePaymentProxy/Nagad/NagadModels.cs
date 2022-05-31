using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace PublicAdm.OnlinePaymentProxy.Nagad
{
    public static class NagadModels
    {
        public class InitiateJson
        {
            public string merchantId { get; set; }
            public string orderId { get; set; }
            public string nagadPublicKey { get; set; }
            public string marchentPrivateKey { get; set; }
            public string clientDeviceType { get; set; }
            public string ApiVersion { get; set; }
            public string UserIP { get; set; }
            public string InitializeAPI { get; set; }
            public string CheckOutAPI { get; set; }
            public string VerificationAPI { get; set; }
            public string PaymentRefId { get; set; }
            public string Amount { get; set; }
            public string CallBackUrl { get; set; }
        }

        #region  Models

        public class ResponseJson
        {
            public string callBackUrl { get; set; }
            public string status { get; set; }
            public string paymentRefId { get; set; }
        }

        public class InitializeModel
        {
            public string merchantId { get; set; }
            public int challenge
            {
                get
                {
                    Random r = new Random();
                    return r.Next(100000000, 999999999);
                }
            }
            public string datetime
            {
                get
                {
                    DateTime date = DateTime.Now.AddMinutes(-5);
                    return date.ToString("yyyyMMddHHmmss");
                }
            }
            public string orderId { get; set; }
        }

        public class FinalInitializerModel
        {
            public string datetime
            {
                get
                {
                    DateTime date = DateTime.Now.AddMinutes(-5);
                    return date.ToString("yyyyMMddHHmmss");
                }
            }

            public string sensitiveData { get; set; }
            public string signature { get; set; }
        }

        public class PaymentModel
        {
            public string merchantId { get; set; }

            public string orderId { get; set; }

            public string currencyCode
            {
                get
                {
                    return "050";
                }
            }

            public string amount { get; set; }
            public string challenge { get; set; }
        }

        public class FinalPaymentModel
        {
            public string sensitiveData { get; set; }
            public string signature { get; set; }
            public string merchantCallbackURL { get; set; }
        }

        public class ResponseModel
        {
            public string merchant { get; set; }
            public string order_id { get; set; }
            public string payment_ref_id { get; set; }
            public string status { get; set; }
            public string message { get; set; }
            public string payment_dt { get; set; }
            public string issuer_payment_ref { get; set; }
        }

        public class CheckingResponseModel
        {
            public string merchantId { get; set; }
            public string orderId { get; set; }
            public string paymentRefId { get; set; }
            public string amount { get; set; }
            public string clientMobileNo { get; set; }
            public string merchantMobileNo { get; set; }
            public string orderDateTime { get; set; }
            public string issuerPaymentDateTime { get; set; }
            public string issuerPaymentRefNo { get; set; }
            public string additionalMerchantInfo { get; set; }
            public string status { get; set; }
            public string statusCode { get; set; }
        }

        public class ErrorModel
        {
            public string reason { get; set; }
            public string message { get; set; }
        }

        #endregion
        #region  Config Model

        public class Config
        {
            public string marchentPrivateKey { get; set; }
            public string nagadPublicKey { get; set; }
            public string MerchantId { get; set; }
            public string InitializeAPI { get; set; }
            public string ApiVersion { get; set; }
            public string CheckOutAPI { get; set; }
            public string VerificationAPI { get; set; }
            public string CallBackUrl { get; set; }
            public string WebApiInitiate { get; set; }
            public string WebApiChecking { get; set; }

            public static Config GetNew()
            {
                return new NagadModels.Config()
                {
                    marchentPrivateKey = "MIIEvAIBADANBgkqhkiG9w0BAQEFAASCBKYwgg...",
                    nagadPublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAO...",
                    MerchantId = "683002007104225",
                    ApiVersion = "v-0.2.0",
                    InitializeAPI = "http://sandbox.mynagad.com:10080/remote-payment-gateway-1.0/api/dfs/check-out/initialize/",
                    CheckOutAPI = "http://sandbox.mynagad.com:10080/remote-payment-gateway-1.0/api/dfs/check-out/complete/",
                    VerificationAPI = "http://sandbox.mynagad.com:10080/remote-payment-gateway-1.0/api/dfs/verify/payment/",
                    CallBackUrl = "http://localhost:8090/api/Student/OnlinePaymentManagerApi/GetConfirmNagadPayment",
                    WebApiInitiate = "http://localhost:19002/Api/NagadApi/NagadInitiate",
                    WebApiChecking = "http://localhost:19002/Api/NagadApi/NagadChecking"
                };
            }
        }

        #endregion

        //#region  Util

        public static class ConfigDataUtil
        {
            private static Config _Config;
            public static Config Config
            {
                get { return loadConfig(); }
                set { _Config = value; }
            }
            private static Config loadConfig()
            {
                string ConfigPath = HttpContext.Current.Server.MapPath("~/Content/GatewayConfigaration/Config.json");
                using (StreamReader file = File.OpenText(ConfigPath))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    return (Config)serializer.Deserialize(file, typeof(Config));
                }
            }
        }

        //#endregion
    }
}

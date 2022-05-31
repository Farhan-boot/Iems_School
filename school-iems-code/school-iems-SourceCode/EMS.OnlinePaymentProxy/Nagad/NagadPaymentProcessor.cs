using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PublicAdm.OnlinePaymentProxy.Nagad
{
    public class NagadPaymentProcessor
    {
        private NagadModels.Config _nagadConfig;

        public NagadPaymentProcessor(NagadModels.Config nagadConfig)
        {
            _nagadConfig = nagadConfig;
        }
        public NagadModels.ResponseJson NagadInitiate(string orderId, float amount,out string error)
        {
            error = string.Empty;
            try
            {
                NagadModels.InitiateJson initiateJson = new NagadModels.InitiateJson()
                {
                    merchantId = _nagadConfig.MerchantId,
                    orderId = orderId,
                    nagadPublicKey = _nagadConfig.nagadPublicKey,
                    marchentPrivateKey = _nagadConfig.marchentPrivateKey,
                    //clientDeviceType = HttpUtil.IsMobileBrowser() ? "MOBILE_WEB" : "PC_WEB",
                    clientDeviceType = "PC_WEB",
                    ApiVersion = _nagadConfig.ApiVersion,
                    UserIP = "103.149.104.68",
                    InitializeAPI = _nagadConfig.InitializeAPI,
                    CheckOutAPI = _nagadConfig.CheckOutAPI,
                    Amount = amount.ToString(),
                    CallBackUrl = _nagadConfig.CallBackUrl
                };
                using (var httpClient = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(initiateJson);
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var url = _nagadConfig.WebApiInitiate;
                    var response = httpClient.PostAsync(new Uri(url), content);
                    response.Wait();
                    var statusCode = response.Result.StatusCode;
                    if (statusCode == HttpStatusCode.BadRequest)
                    {
                        error = "কিছুএকটা গোলমাল হয়েছে। দয়া করে পুণরায় চেষ্টা করুন।";
                        return null;
                    }
                    if (statusCode == HttpStatusCode.OK)
                    {
                        var output = response.Result.Content.ReadAsStringAsync();
                        output.Wait();
                        var nagadResponse = JsonConvert.DeserializeObject<NagadModels.ResponseJson>(output.Result);
                        if (nagadResponse.status.ToLower().Trim() == "success")
                        {
                            return nagadResponse;
                        }
                        error = "কিছুএকটা গোলমাল হয়েছে। দয়া করে পুণরায় চেষ্টা করুন।";
                        return null;
                    }
                }
                error = "কিছুএকটা গোলমাল হয়েছে। দয়া করে পুণরায় চেষ্টা করুন।";
                return null;
            }
            catch (Exception e)
            {
                error = e.Message;
                return null;
            }
        }
        public NagadModels.CheckingResponseModel NagadChecking(string paymentRefId,out string error)
        {
            error = string.Empty;
            try
            {
                NagadModels.InitiateJson checkJson = new NagadModels.InitiateJson()
                {
                    PaymentRefId = paymentRefId,
                    VerificationAPI = _nagadConfig.VerificationAPI
                };
                using (var httpClient = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(checkJson);
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 |
                                                           SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var url = _nagadConfig.WebApiChecking;
                    var response = httpClient.PostAsync(new Uri(url), content);
                    response.Wait();
                    var statusCode = response.Result.StatusCode;
                    if (statusCode == HttpStatusCode.BadRequest)
                    {
                        error = "কিছুএকটা গোলমাল হয়েছে। দয়া করে পুণরায় চেষ্টা করুন।";
                        return null;
                    }
                    if (statusCode == HttpStatusCode.OK)
                    {
                        var output = response.Result.Content.ReadAsStringAsync();
                        output.Wait();
                        var nagadResponse = JsonConvert.DeserializeObject<NagadModels.CheckingResponseModel>(output.Result);
                        if (nagadResponse.status.ToLower().Trim() == "success")
                        {
                            return nagadResponse;
                        }
                        if (nagadResponse.status.ToLower().Trim() != "success")
                        {
                            error = "পেমেন্ট টি সম্পন্ন হয় নাই। দয়া করে পুণরায় চেষ্টা করুন।";
                            return null;
                        }
                        error = "কিছুএকটা গোলমাল হয়েছে। দয়া করে পুণরায় চেষ্টা করুন।";
                        return null;
                    }


                }
            }
            catch (Exception e)
            {
                error = e.Message;
                return null;
            }

            return null;
        }
    }
}

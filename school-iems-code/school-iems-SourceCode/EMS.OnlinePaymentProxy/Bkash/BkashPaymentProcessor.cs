using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using EMS.CoreLibrary.Helpers;
using Newtonsoft.Json;
using PublicAdm.OnlinePaymentProxy.Bkash;
using EMS.DataAccess.Data;

namespace EMS.OnlinePaymentProxy.Bkash

{
    public class BkashPaymentProcessor
    {
        private BkashModels.Config _bkshConfig;
        public BkashPaymentProcessor(BkashModels.Config bkshConfig)
        {
            _bkshConfig = bkshConfig;
        }

        public BkashModels.TokenModel GrantToken(out string error)
        {
            error=String.Empty;
            
            BkashModels.TokenModel tokenModel = null;

            var url = _bkshConfig.tokenURL;

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("username", _bkshConfig.username);
                httpClient.DefaultRequestHeaders.Add("password", _bkshConfig.password);

                var data = "{\"app_key\": \"" + _bkshConfig.app_key
                                              + "\", \"app_secret\":\"" +
                                              _bkshConfig.app_secret + "\"}";

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var content = new StringContent(data,
                    Encoding.UTF8, "application/json");

                var response = httpClient.PostAsync(new Uri(url), content);
                response.Wait();
                var result = response.Result.Content.ReadAsStringAsync();
                result.Wait();
                tokenModel = JsonConvert.DeserializeObject<BkashModels.TokenModel>(result.Result);
            }

            if (tokenModel == null)
            {
                error="কিছুএকটা গোলমাল হয়েছে। দয়া করে পুণরায় চেষ্টা করুন।";
                return null;
            }

            if (tokenModel.id_token == null)
            {
                error = "কিছুএকটা গোলমাল হয়েছে। দয়া করে পুণরায় চেষ্টা করুন।";
                return null;
            }

            return tokenModel;
        }

        public BkashModels.PaymentModel CreatePayment(string id_token, string paySlipId, double formFee, out string error)
        {
            error=String.Empty;

            var paymentModel = new BkashModels.PaymentModel();
            var url = _bkshConfig.createURL;
            try
            {
                //save transaction info
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("X-APP-Key", _bkshConfig.app_key);
                    httpClient.DefaultRequestHeaders.Add("Authorization", id_token);
                    var data = "{\"amount\": \"" + formFee +
                               "\",\"currency\": \"BDT\",\"intent\": \"sale\",\"merchantInvoiceNumber\": \"" +
                               paySlipId + "\"}";
                    var content = new StringContent(data, Encoding.UTF8, "application/json");
                    var response = httpClient.PostAsync(new Uri(url), content);
                    response.Wait();
                    var result = response.Result.Content.ReadAsStringAsync();
                    result.Wait();
                    paymentModel = JsonConvert.DeserializeObject<BkashModels.PaymentModel>(result.Result);
                    //httpResult.Data = paymentModel;

                }
                if (paymentModel == null || paymentModel.transactionStatus != "Initiated")
                {
                    error="কিছুএকটা গোলমাল হয়েছে। দয়া করে পুণরায় চেষ্টা করুন।";
                    return null;
                }
                
            }
            catch (Exception e)
            {
                error=e.Message;
                return null;
            }
            return paymentModel;
        }
        public BkashModels.ExecutePaymentModel ExecutePayment(string paymentId, string bkashTokenId, out string error)
        {
            error = String.Empty;

            var exPaymentModel = new BkashModels.ExecutePaymentModel();
            byte gatewayResponseEnumId = (byte)Acnt_StdOnlinePaySlip.GatewayResponseEnum.Initiated;
            try
            {
                
                var url = _bkshConfig.executeURL + paymentId;
                string errResult;
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("X-APP-Key", _bkshConfig.app_key);
                    httpClient.DefaultRequestHeaders.Add("Authorization", bkashTokenId);
                    var content = new StringContent("", Encoding.UTF8, "application/json");
                    httpClient.Timeout = TimeSpan.FromSeconds(30);
                    var response = httpClient.PostAsync(new Uri(url), content);
                    response.Wait();
                    var result = response.Result.Content.ReadAsStringAsync();
                    result.Wait();
                    errResult = result.Result;
                    exPaymentModel = JsonConvert.DeserializeObject<BkashModels.ExecutePaymentModel>(result.Result);
                    //save transaction log
                }
                if (exPaymentModel == null || exPaymentModel.transactionStatus == null)
                {
                    var errorObj = JsonConvert.DeserializeObject<BkashModels.ErrorModel>(errResult);
                    if (errorObj.errorMessage.IsNullOrEmptyOrWhiteSpace())
                    {
                        return RecheckQueryPayment(paymentId, bkashTokenId,out error, out gatewayResponseEnumId);
                    }
                    else
                    {
                        error = "Payment wasn't Completed. " + errorObj.errorMessage;
                        return null;
                    }
                    //set transation status Failed;
                }
                else if (exPaymentModel.transactionStatus != "Completed")
                {
                    var errorObj = JsonConvert.DeserializeObject<BkashModels.ErrorModel>(errResult);
                    error = "Payment wasn't Completed. " + errorObj.errorMessage;
                    return null;
                }
                return exPaymentModel;

            }
            catch (Exception e)
            {
                return RecheckQueryPayment(paymentId, bkashTokenId,out error, out gatewayResponseEnumId);
            }
        }

        public BkashModels.ExecutePaymentModel RecheckQueryPayment(string paymentId, string bkashTokenId, out string error,out byte gatewayResponseEnumId)
        {
            //var httpResult = new HttpResult<BkashModels.ExecutePaymentModel>();
            error=String.Empty;
            gatewayResponseEnumId = (byte)Acnt_StdOnlinePaySlip.GatewayResponseEnum.GatewayError;
            var exPaymentModel = new BkashModels.ExecutePaymentModel();
            try
            {
                var url = _bkshConfig.queryUrl + paymentId;
                string errResult;
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("X-APP-Key", _bkshConfig.app_key);
                    httpClient.DefaultRequestHeaders.Add("Authorization", bkashTokenId);
                    httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                    httpClient.Timeout = TimeSpan.FromMinutes(1);
                    var response = httpClient.GetAsync(new Uri(url));
                    response.Wait();
                    var result = response.Result.Content.ReadAsStringAsync();
                    result.Wait();
                    errResult = result.Result;
                    exPaymentModel = JsonConvert.DeserializeObject<BkashModels.ExecutePaymentModel>(result.Result);
                    //save transaction log
                }
                if (exPaymentModel.transactionStatus == null)
                {
                    var errorObj = JsonConvert.DeserializeObject<BkashModels.ErrorModel>(errResult);
                    gatewayResponseEnumId = (byte)Acnt_StdOnlinePaySlip.GatewayResponseEnum.PermanentFailed;
                    error = "Payment Failed. " + errorObj.errorMessage;
                    return null;
                }

                if (exPaymentModel.transactionStatus.ToLower().Trim() == "Completed".ToLower().Trim())
                {
                    gatewayResponseEnumId = (byte)Acnt_StdOnlinePaySlip.GatewayResponseEnum.Success;
                    return exPaymentModel;
                }

                if (exPaymentModel.transactionStatus.ToLower().Trim() == "Initiated".ToLower().Trim())
                {
                    gatewayResponseEnumId = (byte)Acnt_StdOnlinePaySlip.GatewayResponseEnum.PermanentFailed;
                    error = "Payment Initiated But Not Paid.";
                    return null;
                }
                gatewayResponseEnumId = (byte)Acnt_StdOnlinePaySlip.GatewayResponseEnum.GatewayError;
                error = "Something Went Wrong Please Try again Later.";
            }
            catch (Exception e)
            {
                gatewayResponseEnumId = (byte)Acnt_StdOnlinePaySlip.GatewayResponseEnum.GatewayError;
                error = e.GetBaseException().Message;
                return null;
            }

            return exPaymentModel;
        }
        public BkashModels.SearchResponseModel SearchTransactionDetails(string trnxId, string bkashTokenId, out string error)
        {
            error=String.Empty;
            var searchModel = new BkashModels.SearchResponseModel();

            try
            {
                var url = _bkshConfig.searchTrnxUrl + trnxId;
                string errResult;
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("X-APP-Key", _bkshConfig.app_key);
                    httpClient.DefaultRequestHeaders.Add("Authorization", bkashTokenId);
                    httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                    var response = httpClient.GetAsync(new Uri(url));
                    response.Wait();
                    var result = response.Result.Content.ReadAsStringAsync();
                    result.Wait();
                    errResult = result.Result;
                    searchModel = JsonConvert.DeserializeObject<BkashModels.SearchResponseModel>(result.Result);
                    //save transaction log
                }
                if (searchModel == null || searchModel.transactionStatus != "Completed")
                {
                    var bkashError = JsonConvert.DeserializeObject<BkashModels.ErrorModel>(errResult);
                    error="Error Message: " + bkashError.errorMessage;
                    return null;
                    //set transation status Failed;
                }
                
            }
            catch (Exception e)
            {
                error=e.Message;
                return null;
            }

            return searchModel;
        }
    }
}

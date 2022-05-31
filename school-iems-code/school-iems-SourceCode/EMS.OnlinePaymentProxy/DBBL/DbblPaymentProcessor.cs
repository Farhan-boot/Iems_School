using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;
using System.Configuration;
using System.Web;
using EMS.CoreLibrary;

namespace EMS.OnlinePaymentProxy.DBBL
{

    public class DbblPaymentProcessor : PaymentProfile
    {
        
        public DbblPaymentProcessor(PaymentProfile profile)
        {
            UserId = profile.UserId;
            CardType = profile.CardType;
            ClientIP = profile.ClientIP;
            SessionId = profile.SessionId;
            Description = profile.Description;
            OnlinePaymentFee = profile.OnlinePaymentFee;
            MainAmount = profile.MainAmount;
            CertificateDiskPath = profile.CertificateDiskPath;
            RedirectUrlPrefix = profile.RedirectUrlPrefix;
        }

        public bool InitiateTransaction()
        {
            return InitiateTransaction(this.CertificateDiskPath, CardType, TotalAmount, ClientIP, Description);
        }
        private bool InitiateTransaction(string certificatePath, DbblConstant.CardTypeEnum cardType, double totalAmount, string ip, string description)
        {
            string amountStr = Convert.ToInt32(totalAmount * 100).ToString();
            TransactionId = "";
            try
            {
                string processString =
                    $@" -jar {certificatePath}ecomm_merchant.jar {certificatePath}merchant.properties -v {amountStr
                        } 050 {ip} 1#{description}";
                using (Process p = new Process())
                {
                    p.StartInfo.WorkingDirectory = certificatePath;
                    //"C:\\dbbl";
                    //p.StartInfo.WorkingDirectory = "C:\\live";
                    //p.StartInfo = new ProcessStartInfo("java", @" -jar C:\test\ecomm_merchant.jar C:\test\merchant.properties -v " + amountStr + " 050 10.10.200.131 " + description);
                    //p.StartInfo = new ProcessStartInfo("java", @" -jar C:\live\ecomm_merchant.jar C:\live\merchant.properties -v " + amountStr + " 050 10.10.200.131 " + cardType +"^"+ desc);
                    p.StartInfo = new ProcessStartInfo("java", processString);
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    this.ExecutionResult = p.StandardOutput.ReadToEnd();
                    this.ExecutionResult = this.ExecutionResult.Trim();
                    return IsValidInitiateRequest();
                }
            }
            catch (Exception ex)
            {
                Error = ex.ToString();
                this.TransactionId = "";
                return false;
            }
        }

        public string GetRedirectUrl()
        {
            return GetRedirectUrl(this.RedirectUrlPrefix, this.TransactionId, this.CardType);
        }

        private string GetRedirectUrl(string url, string transId, DbblConstant.CardTypeEnum cardType)
        {
            transId = HttpUtility.UrlEncode(transId);
            string paymentPageUrl = $"{url}{(int)cardType}&trans_id={transId}";
            //string paymentPageUrl = "https://ecom.dutchbanglabank.com/ecomm2/ClientHandler?card_type=" + cardType + "&trans_id=" + transId;
            return paymentPageUrl;
        }

        public bool ValidateSuccessRequest()
        {
            return ValidateSuccessRequest(CertificateDiskPath, TransactionId, ClientIP);
        }

        private bool ValidateSuccessRequest(string certificatePath, string transId, string ip)
        {
            this.ExecutionResult = "";
            //Merchant tx = new Merchant(@"C:\dbbl\merchant.properties");
            //tx = new Merchant(@"C:\dbbl\merchant.properties");
            string processStr = $@" -jar {certificatePath}ecomm_merchant.jar {certificatePath}merchant.properties -c {transId} {ip}";
            try
            {
                using (Process p = new Process())
                {
                    p.StartInfo.WorkingDirectory = certificatePath;//"C:\\dbbl";
                    p.StartInfo = new ProcessStartInfo("java", processStr);
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.CreateNoWindow = true;
                    p.Start();
                    this.ExecutionResult = p.StandardOutput.ReadToEnd();
                    return IsValidSuccessRequest();
                }
            }
            catch (Exception ex)
            {
                Error = ex.ToString();
                return false;
            }
        }

        private bool IsValidInitiateRequest()
        {
            try
            {
                this.TransactionId = this.ExecutionResult.Substring(16, 28);
                if (!this.TransactionId.Equals("", StringComparison.OrdinalIgnoreCase))
                {
                    this.OnlinePaymentStatus = EnumCollection.Accounting.OnlinePaymentStatusEnum.Initiated;
                    IsSuccess = true;
                    return IsSuccess;
                }
            }
            catch (Exception ex)
            {
                Error = ex.ToString();
                this.TransactionId = "";
                // this.Exception = ex;
            }
            this.OnlinePaymentStatus = EnumCollection.Accounting.OnlinePaymentStatusEnum.InvalidInitiateTry;
            IsSuccess = false;
            return IsSuccess;
        }

        private bool IsValidSuccessRequest()
        {
            try
            {
                string sResCode = ExecutionResult.Substring(ExecutionResult.IndexOf("RESULT_CODE:", StringComparison.Ordinal) + 12, 4).Trim();
                var result = ExecutionResult.Substring(ExecutionResult.IndexOf("RESULT:", StringComparison.Ordinal) + 8, 2).Trim();
                if (result.Equals("OK", StringComparison.OrdinalIgnoreCase) &&
                    sResCode.Equals("000", StringComparison.OrdinalIgnoreCase))
                {
                    this.OnlinePaymentStatus = EnumCollection.Accounting.OnlinePaymentStatusEnum.Success;
                    IsSuccess = true;
                    return IsSuccess;
                }

            }
            catch (Exception ex)
            {
                Error = ex.ToString();
                // this.Exception = ex;
            }
            this.OnlinePaymentStatus = EnumCollection.Accounting.OnlinePaymentStatusEnum.Failed;
            IsSuccess = false;
            return IsSuccess;
        }


    }
}

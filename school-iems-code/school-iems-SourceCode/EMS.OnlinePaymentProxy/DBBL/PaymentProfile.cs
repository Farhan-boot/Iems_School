using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using EMS.CoreLibrary;

namespace EMS.OnlinePaymentProxy.DBBL
{
    public class PaymentProfile
    {
        public long UserId { get; set; }
        public string TransactionId { get; protected set; }
        public string SessionId { get; set; }
        public string ClientIP { get; set; }
        public DbblConstant.CardTypeEnum CardType { get; set; }
        public string CertificateDiskPath { get; set; }
        public double MainAmount { get; set; }
        public double OnlinePaymentFee { get; set; }
        public EnumCollection.Accounting.OnlinePaymentStatusEnum OnlinePaymentStatus { get; set; }

        public bool IsSuccess { get; protected set; }
        public double TotalAmount
        {
            get { return MainAmount + OnlinePaymentFee; }
        }
        public string RedirectUrlPrefix { get; set; }
        public string Description { get; set; }
        public string ExecutionResult { get; protected set; }

        public string Error { get; set; }
        //public string PaymentResultArray { get; protected set; }

        public EnumCollection.Accounting.OnlinePaymentStatusEnum TransactionStatus { get; set; }

        public string RedirectUrl
        {
            get
            {
                var transId = HttpUtility.UrlEncode(TransactionId);
                string paymentPageUrl = $"{RedirectUrlPrefix}{(int)CardType}&trans_id={TransactionId}";
                //string paymentPageUrl = "https://ecom.dutchbanglabank.com/ecomm2/ClientHandler?card_type=" + cardType + "&trans_id=" + transId;
                return paymentPageUrl;
            }

        }
    }
}

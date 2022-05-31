using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.OnlinePaymentProxy.DBBL
{
    public class DbblConstant
    {
        public enum CardTypeEnum
        {
            DbblNexus = 1,
            DbblMasterDebit = 2,
            DbblVisaDebit = 3,
            VISA = 4,
            MasterCard = 5,
            DbblMobile = 6
        }



        public static string TestCertificateDiskPath = @"C:\DBBL_Gateway\ecomtest\";
        public static string LiveCertificateDiskPath1 = @"C:\DBBL_Gateway\ecomlive1\";
        public static string LiveCertificateDiskPath2 = @"C:\DBBL_Gateway\ecomlive2\";

        public static string TestRedirectUrlPrefix =
            @"https://ecomtest.dutchbanglabank.com/ecomm2/ClientHandler?card_type=";

        public static string LiveRedirectUrlPrefix1 =
            @"https://ecom.dutchbanglabank.com/ecomm2/ClientHandler?card_type=";

        public static string LiveRedirectUrlPrefix2 =
            @"https://ecom1.dutchbanglabank.com/ecomm2/ClientHandler?card_type=";

        public static double GetFeeByCardType(double mainAmount, CardTypeEnum cardType)
        {
            if (cardType == CardTypeEnum.DbblNexus || cardType == CardTypeEnum.DbblMobile)
            {
                return 10;
            }

            return mainAmount*0.018;

        }
    }
}


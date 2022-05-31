using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.CoreLibrary;

namespace EMS.DataAccess.Data
{
    public partial class OnlinePaymentAudit
    {
        public EnumCollection.Accounting.OnlinePaymentStatusEnum OnlinePaymentStatus
        {
            get
            {
                return (EnumCollection.Accounting.OnlinePaymentStatusEnum)this.PaymentStatusId;
            }
            set
            {
                this.PaymentStatusId = (int)value;
            }
        }
    }
}

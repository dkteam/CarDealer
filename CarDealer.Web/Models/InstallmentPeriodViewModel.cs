using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealer.Web.Models
{
    public class InstallmentPeriodViewModel
    {
        public int ID { set; get; }

        public int InstallmentPaymentMethodID { set; get; }

        public int PeriodID { set; get; }

        public decimal InterestRate { set; get; }
        
        public virtual PeriodViewModel Periods { set; get; }
        
        public virtual InstallmentPaymentMethodViewModel InstallmentPaymentMethods { set; get; }
    }
}
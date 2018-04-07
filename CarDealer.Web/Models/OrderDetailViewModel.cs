using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealer.Web.Models
{
    public class OrderDetailViewModel
    {
        public int OrderID { set; get; }
        
        public int CarID { set; get; }

        public int Quatity { set; get; }

        public int InstallmentPeriodID { set; get; }
        
        public virtual OrderViewModel Orders { set; get; }
        
        public virtual CarViewModel Cars { set; get; }
        
        public virtual InstallmentPeriodViewModel InstallmentPeriods { set; get; }
    }
}
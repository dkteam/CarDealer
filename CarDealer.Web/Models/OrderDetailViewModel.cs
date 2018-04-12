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
        
        public virtual OrderViewModel Order { set; get; }
        
        public virtual CarViewModel Car { set; get; }
        
        public virtual InstallmentPeriodViewModel InstallmentPeriod { set; get; }
    }
}
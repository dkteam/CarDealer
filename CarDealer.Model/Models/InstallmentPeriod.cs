using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealer.Model.Models
{
    [Table("InstallmentPeriods")]
    public class InstallmentPeriod
    {
        [Key]
        public int ID { set; get; }

        public int InstallmentPaymentMethodID { set; get; }

        public int PeriodID { set; get; }

        public decimal InterestRate { set; get; }

        [ForeignKey("PeriodID")]
        public virtual Period Periods { set; get; }

        [ForeignKey("InstallmentPaymentMethodID")]
        public virtual InstallmentPaymentMethod InstallmentPaymentMethod { set; get; }
    }
}
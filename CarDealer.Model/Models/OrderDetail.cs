using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealer.Model.Models
{
    [Table("OrderDetails")]
    public class OrderDetail
    {
        [Key]
        [Column(Order = 1)]
        public int OrderID { set; get; }

        [Key]
        [Column(Order = 2)]
        public int CarID { set; get; }

        public int Quatity { set; get; }

        public int InstallmentPeriodID { set; get; }        

        [ForeignKey("OrderID")]
        public virtual Order Orders { set; get; }

        [ForeignKey("CarID")]
        public virtual Car Cars { set; get; }

        [ForeignKey("InstallmentPeriodID")]
        public virtual InstallmentPeriod InstallmentPeriods { set; get; }
    }
}
using CarDealer.Model.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealer.Model.Models
{
    [Table("InstallmentPaymentMethods")]
    public class InstallmentPaymentMethod : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(50)]
        public string Name { set; get; }

        [Required]
        [MaxLength(50)]
        [Column(TypeName = "varchar")]
        public string Alias { set; get; }

        [Required]
        [MaxLength(256)]
        public string Description { set; get; }

        public virtual IEnumerable<InstallmentPeriod> InstallmentPeriods { set; get; }
    }
}
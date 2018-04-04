using CarDealer.Model.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealer.Model.Models
{
    [Table("ManufactureYears")]
    public class ManufactureYear : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(10)]
        public string Name { set; get; }

        [Required]
        [MaxLength(10)]
        [Column(TypeName = "varchar")]
        public string Alias { set; get; }

        public string Description { set; get; }

        public virtual IEnumerable<Car> Cars { set; get; }
    }
}
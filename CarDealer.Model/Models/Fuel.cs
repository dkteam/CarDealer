using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealer.Model.Models
{
    [Table("Fuels")]
    public class Fuel
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

        [MaxLength(256)]
        public string Description { set; get; }

        public virtual IEnumerable<Car> Cars { set; get; }
    }
}
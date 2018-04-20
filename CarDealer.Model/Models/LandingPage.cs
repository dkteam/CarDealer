using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealer.Model.Models
{
    [Table("LandingPages")]
    public class LandingPage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [MaxLength(50)]
        public string Name { set; get; }

        [MaxLength(500)]
        public string Image { set; get; }

        [Required]
        [Column(TypeName = "ntext")]
        public string Content { set; get; }
    }
}

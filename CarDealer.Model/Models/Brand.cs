using CarDealer.Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealer.Model.Models
{
    [Table("Brands")]
    public class Brand : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(50)]
        public string Name { set; get; }

        [Required]
        [Column(TypeName ="varchar")]
        public string Alias { set; get; }
        
        [MaxLength(50)]
        public string Country { set; get; }
        
        [MaxLength(256)]
        public string Logo { set; get; }
        
        [MaxLength(50)]
        public string Website { set; get; }

        [MaxLength(256)]
        public string Description { set; get; }

        public int? DisplayOrder { set; get; } 
               
        public int? ParentID { set; get; }

        public bool? HomeFlag { set; get; }

        public virtual IEnumerable<Car> Cars { set; get; }
    }
}

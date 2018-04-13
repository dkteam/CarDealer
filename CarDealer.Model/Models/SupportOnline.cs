using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealer.Model.Models
{
    [Table("SupportOnlines")]
    public class SupportOnline
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(50)]
        public string Name { set; get; }

        [MaxLength(50)]
        public string Skype { set; get; }

        [Required]
        [MaxLength(50)]
        public string Mobile { set; get; }

        [Required]
        [MaxLength(256)]
        public string Address { set; get; }

        [MaxLength(50)]
        public string Email { set; get; }

        [MaxLength(50)]
        public string Zalo { set; get; }
        
        [MaxLength(50)]
        public string Facebook { set; get; }

        public int? DisplayOrder { set; get; }

        public bool Status { set; get; }
    }
}
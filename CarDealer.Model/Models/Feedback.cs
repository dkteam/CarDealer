using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealer.Model.Models
{
    [Table("Feedbacks")]
    public class Feedback
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [StringLength(100)]
        [Required]
        public string Name { set; get; }

        [StringLength(20)]
        [Required]
        public string Mobile { set; get; }

        [StringLength(50)]
        [Required]
        public string Email { set; get; }

        [StringLength(500)]
        public string Message { set; get; }
        
        public DateTime CreatedDate { set; get; }
                
        public bool Status { set; get; }
    }
}

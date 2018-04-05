using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealer.Model.Models
{
    [Table("CarTags")]
    public class CarTag
    {
        [Key]
        [Column(Order=1)]
        public int CarID { set; get; }

        [Key]
        [Column(Order = 2)]
        [MaxLength(50)]
        public string TagID { set; get; }

        [ForeignKey("CarID")]
        public virtual Car Cars { set; get; }

        [ForeignKey("TagID")]
        public virtual Tag Tags { set; get; }
    }
}
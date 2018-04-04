using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealer.Model.Models
{
    [Table("CarTags")]
    public class CarTag
    {
        [Key]
        public int CarID { set; get; }

        [Key]
        public int TagID { set; get; }

        [ForeignKey("CarID")]
        public virtual Car Cars { set; get; }

        [ForeignKey("TagID")]
        public virtual Tag Tags { set; get; }
    }
}
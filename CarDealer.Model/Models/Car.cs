using CarDealer.Model.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealer.Model.Models
{
    [Table("Cars")]
    public class Car : Auditable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        [Required]
        [MaxLength(256)]
        [Column(TypeName = "varchar")]
        public string Alias { set; get; }

        public int? BrandID { set; get; }

        public int? CategoryID { set; get; }

        [Required]
        [MaxLength(1000)]
        [Column(TypeName = "varchar")]
        public string Image { set; get; }

        [Column(TypeName = "xml")]
        public string MoreImages { set; get; }

        public bool CarStatus { set; get; }

        public int? Odo { set; get; }

        public int? FuelID { set; get; }

        public int? StyleID { set; get; }

        public int? ManufactureYearID { set; get; }

        public int? TransmissionTypeID { set; get; }

        [MaxLength(256)]
        public string Engine { set; get; }

        [MaxLength(256)]
        public string FuelConsumption { set; get; }

        [MaxLength(256)]
        public string WheelDrive { set; get; }

        public decimal? Price { set; get; }

        public decimal? PromotionPrice { set; get; }

        [MaxLength(256)]
        public string Warranty { set; get; }

        [MaxLength(500)]
        public string Description { set; get; }

        public string Content { set; get; }

        public bool? Bestseller { set; get; }

        public bool? HotFlag { set; get; }

        public bool? BestPrice { set; get; }

        public int? ViewCount { set; get; }

        public string Tags { set; get; }

        [ForeignKey("CategoryID")]
        public virtual CarCategory CarCategory { set; get; }

        [ForeignKey("BrandID")]
        public virtual Brand Brand { set; get; }

        [ForeignKey("FuelID")]
        public virtual Fuel Fuel { set; get; }

        [ForeignKey("StyleID")]
        public virtual Style Style { set; get; }

        [ForeignKey("ManufactureYearID")]
        public virtual ManufactureYear ManufactureYear { set; get; }

        [ForeignKey("TransmissionTypeID")]
        public virtual TransmissionType TransmissionType { set; get; }

        public virtual IEnumerable<OrderDetail> OrderDetails { set; get; }

        public virtual IEnumerable<CarTag> CarTags { set; get; }
    }
}
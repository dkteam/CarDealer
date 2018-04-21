using CarDealer.Web.Infrastucture.Core;
using System;
using System.Collections.Generic;

namespace CarDealer.Web.Models
{
    public class CarViewModel
    {
        public int ID { set; get; }

        public string Name { set; get; }

        public string Alias { set; get; }

        public int? BrandID { set; get; }

        public int? CategoryID { set; get; }

        public string Image { set; get; }

        public string MoreImages { set; get; }

        public bool CarStatus { set; get; }

        public int? Odo { set; get; }

        public int? FuelID { set; get; }

        public int? StyleID { set; get; }

        public int? ManufactureYearID { set; get; }

        public int? TransmissionTypeID { set; get; }

        public string Engine { set; get; }

        public string FuelConsumption { set; get; }

        public string WheelDrive { set; get; }

        public decimal? Price { set; get; }

        public decimal? PromotionPrice { set; get; }

        public string Warranty { set; get; }

        public string Description { set; get; }

        public string Content { set; get; }

        public bool? Bestseller { set; get; }

        public bool? HotFlag { set; get; }

        public bool? BestPrice { set; get; }

        public int? ViewCount { set; get; }

        public string Tags { set; get; }

        public string Features { set; get; }

        public virtual BrandViewModel Brand { set; get; }

        public virtual FuelViewModel Fuel { set; get; }

        public virtual StyleViewModel Style { set; get; }

        public virtual ManufactureYearViewModel ManufactureYear { set; get; }

        public virtual TransmissionTypeViewModel TransmissionType { set; get; }

        public DateTime? CreatedDate { set; get; }

        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }

        public string UpdatedBy { set; get; }

        public string MetaKeyword { set; get; }

        public string MetaDescription { set; get; }

        public bool Status { set; get; }

        public virtual List<OrderDetailViewModel> OrderDetails { set; get; }

        public virtual List<CarTagViewModel> CarTags { set; get; }        

        public IEnumerable<CarCategoryViewModel> Categories { set; get; }

        //for Client Site
        public IEnumerable<FuelViewModel> Fuels { set; get; }
        public IEnumerable<TransmissionTypeViewModel> TransmissionTypes { set; get; }
        public IEnumerable<ManufactureYearViewModel> ManufactureYears { set; get; }
        public IEnumerable<StyleViewModel> Styles { set; get; }
        public PaginationSet<CarViewModel> carPaginationSet { set; get; }
    }
}
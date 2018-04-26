using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealer.Web.Models
{
    public class HomeViewModel
    {
        public IEnumerable<SlideViewModel> Slides { set; get; }
        public IEnumerable<CarViewModel> BestSellerCars { set; get; }
        public IEnumerable<CarViewModel> HotCars { set; get; }
        public IEnumerable<CarViewModel> HotCarTop3 { set; get; }
        public IEnumerable<CarViewModel> BestPriceCars { set; get; }
        public IEnumerable<CarViewModel> UsedCars { set; get; }
        public IEnumerable<CarCategoryViewModel> CarCategories { set; get; }

        public IEnumerable<FuelViewModel> Fuels { set; get; }
        public IEnumerable<TransmissionTypeViewModel> TransmissionTypes { set; get; }
        public IEnumerable<ManufactureYearViewModel> ManufactureYears { set; get; }
        public IEnumerable<TotalSeatViewModel> TotalSeats { set; get; }
        public IEnumerable<StyleViewModel> Styles { set; get; }

        public LandingPageViewModel welcomeLandingPage { set; get; }
        public LandingPageViewModel featureLandingPage { set; get; }
        public LandingPageViewModel commentLandingPage { set; get; }
        public LandingPageViewModel counterLandingPage { set; get; }

        public string Title { set; get; }
        public string Metakeyword { set; get; }
        public string MetaDescription { set; get; }
    }
}
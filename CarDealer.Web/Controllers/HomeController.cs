using AutoMapper;
using CarDealer.Model.Models;
using CarDealer.Service;
using CarDealer.Web.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealer.Web.Controllers
{
    public class HomeController : Controller
    {
        ICarCategoryService _carCategoryService;
        IPostCategoryService _postCategoryService;
        ISlideService _slideService;
        IMenuService _menuService;
        IFooterService _footerService;
        ICarService _carService;
        IManufactureYearService _manufactureYearService;
        ITotalSeatService _totalSeatService;
        IStyleService _styleService;
        ITransmissionTypeService _transmissionTypeService;
        IFuelService _fuelService;
        ISupportOnlineService _supportOnline;
        IPostService _postService;
        ILandingPageService _landingPageService;

        public HomeController(  ICarCategoryService carCategoryService,
                                IPostCategoryService postCategoryService,
                                ISlideService slideService,
                                ICarService carService,
                                IManufactureYearService manufactureYearService,
                                ITotalSeatService totalSeatService,
                                IStyleService styleService,
                                ITransmissionTypeService transmissionTypeService,
                                IMenuService menuService,
                                IFuelService fuelService,
                                ISupportOnlineService supportOnline,
                                IPostService postService,
                                ILandingPageService landingPageService,
                                IFooterService footerService)
        {
            this._carCategoryService = carCategoryService;
            this._postCategoryService = postCategoryService;
            this._menuService = menuService;
            this._slideService = slideService;
            this._footerService = footerService;
            this._carService = carService;
            this._supportOnline = supportOnline;
            this._postService = postService;
            this._landingPageService = landingPageService;

            this._manufactureYearService = manufactureYearService;
            this._totalSeatService = totalSeatService;
            this._styleService = styleService;
            this._transmissionTypeService = transmissionTypeService;
            this._fuelService = fuelService;
        }

        public ActionResult Index()
        {
            //get slides
            var slideModel = _slideService.GetAll();
            var slideView = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(slideModel);
            //get bestseller
            var bestsellerCarModel = _carService.GetBestSeller(9);
            var bestsellerCarView = Mapper.Map<IEnumerable<Car>, IEnumerable<CarViewModel>>(bestsellerCarModel);

            //get hot car
            var hotCarModel = _carService.GetHot(9);
            var hotCarView = Mapper.Map<IEnumerable<Car>, IEnumerable<CarViewModel>>(hotCarModel);

            //get hot car top 3
            var hotCarTop3Model = _carService.GetHot(3);
            var hotCarTop3View = Mapper.Map<IEnumerable<Car>, IEnumerable<CarViewModel>>(hotCarTop3Model);

            //get best price
            var bestPriceCarModel = _carService.GetBestPrice(9);
            var bestPriceCarView = Mapper.Map<IEnumerable<Car>, IEnumerable<CarViewModel>>(bestPriceCarModel);

            //get best price
            var usedCarModel = _carService.GetUsedCar(9);
            var usedCarView = Mapper.Map<IEnumerable<Car>, IEnumerable<CarViewModel>>(usedCarModel);

            //get car categories
            var categoryModel = _carCategoryService.GetAll();
            var categoryView = Mapper.Map<IEnumerable<CarCategory>, IEnumerable<CarCategoryViewModel>>(categoryModel);

            //get all manufacture_years
            var manufactureYearModel = _manufactureYearService.GetAll();
            var manufactureYearView = Mapper.Map<IEnumerable<ManufactureYear>, IEnumerable<ManufactureYearViewModel>>(manufactureYearModel);

            //get all total seat
            var totalSeatModel = _totalSeatService.GetAll();
            var totalSeatView = Mapper.Map<IEnumerable<TotalSeat>, IEnumerable<TotalSeatViewModel>>(totalSeatModel);

            //get all style
            var styleModel = _styleService.GetAll();
            var styleView = Mapper.Map<IEnumerable<Style>, IEnumerable<StyleViewModel>>(styleModel);

            //get all transmission_types
            var transmissionTypeModel = _transmissionTypeService.GetAll();
            var transmissionTypeView = Mapper.Map<IEnumerable<TransmissionType>, IEnumerable<TransmissionTypeViewModel>>(transmissionTypeModel);

            //get all fules
            var fuelModel = _fuelService.GetAll();
            var fuelView = Mapper.Map<IEnumerable<Fuel>, IEnumerable<FuelViewModel>>(fuelModel);

            //get welcome landing pages 
            var landingPageModel = _landingPageService.GetById(2);
            var welcomeView = Mapper.Map<LandingPage, LandingPageViewModel>(landingPageModel);

            //get feature landing pages 
            var featureLPModel = _landingPageService.GetById(3);
            var featureView = Mapper.Map<LandingPage, LandingPageViewModel>(featureLPModel);

            //get feature landing pages 
            var commentLPModel = _landingPageService.GetById(5);
            var commentView = Mapper.Map<LandingPage, LandingPageViewModel>(commentLPModel);

            //get feature landing pages 
            var counterLPModel = _landingPageService.GetById(6);
            var counterView = Mapper.Map<LandingPage, LandingPageViewModel>(counterLPModel);

            var homeView = new HomeViewModel();
            homeView.Slides = slideView;
            homeView.BestSellerCars = bestsellerCarView;
            homeView.HotCars = hotCarView;
            homeView.HotCarTop3 = hotCarTop3View;
            homeView.BestPriceCars = bestPriceCarView;
            homeView.CarCategories = categoryView;
            homeView.UsedCars = usedCarView;
            homeView.welcomeLandingPage = welcomeView;
            homeView.featureLandingPage = featureView;
            homeView.commentLandingPage = commentView;
            homeView.counterLandingPage = counterView;

            homeView.Fuels = fuelView;
            homeView.ManufactureYears = manufactureYearView;
            homeView.TotalSeats = totalSeatView;
            homeView.TransmissionTypes = transmissionTypeView;
            homeView.Styles = styleView;

            return View(homeView);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
            var menuModel = _menuService.GetAll();
            var menuView = Mapper.Map<IEnumerable<Menu>, IEnumerable<MenuViewModel>>(menuModel);

            var carCategoryModel = _carCategoryService.GetAll();
            var carCategoryView = Mapper.Map<IEnumerable<CarCategory>, IEnumerable<CarCategoryViewModel>>(carCategoryModel);

            var postCategoryModel = _postCategoryService.GetAll();
            var postCategoryView = Mapper.Map<IEnumerable<PostCategory>, IEnumerable<PostCategoryViewModel>>(postCategoryModel);

            var supportOnlineModel = _supportOnline.GetById(1);
            var supportOnlineView = Mapper.Map<SupportOnline, SupportOnlineViewModel>(supportOnlineModel);



            var header = new HeaderViewModel();
            header.Menus = menuView;
            header.CarCategories = carCategoryView;
            header.SupportOnline = supportOnlineView;
            header.PostCategories = postCategoryView;

            return PartialView(header);
        }

        [ChildActionOnly]
        public ActionResult PreFooter()
        {
            //get infomation contact
            var supportOnlineModel = _supportOnline.GetById(1);
            var supportOnlineView = Mapper.Map<SupportOnline, SupportOnlineViewModel>(supportOnlineModel);

            //get latest car
            var latestCarModel = _carService.GetLatestCar(4);
            var latestCarView = Mapper.Map<IEnumerable<Car>, IEnumerable<CarViewModel>>(latestCarModel);

            //get latest posts
            var latestPostModel = _postService.GetLatestPosts(5);
            var latestPostView = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(latestPostModel);

            var preFooter = new PreFooterViewModel();
            preFooter.SupportOnline = supportOnlineView;
            preFooter.LatestCars = latestCarView;
            preFooter.LatestPosts = latestPostView;

            return PartialView(preFooter);
        }

        [ChildActionOnly]
        public ActionResult Footer()
        {
            var footerModel = _footerService.GetFooter();
            var footerViewModel = Mapper.Map<Footer, FooterViewModel>(footerModel);

            return PartialView(footerViewModel);
        }

        [ChildActionOnly]
        public ActionResult Category()
        {
            var model = _carCategoryService.GetAll();
            var listVm = Mapper.Map<IEnumerable<CarCategory>, IEnumerable<CarCategoryViewModel>>(model);

            return PartialView(listVm);
        }
    }
}
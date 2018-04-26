using AutoMapper;
using CarDealer.Common;
using CarDealer.Model.Models;
using CarDealer.Service;
using CarDealer.Web.Infrastucture.Core;
using CarDealer.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CarDealer.Web.Controllers
{
    public class CarController : Controller
    {
        ICarService _carService;
        ICarCategoryService _carCategoryService;

        IManufactureYearService _manufactureYearService;
        ITotalSeatService _totalSeatService;
        IStyleService _styleService;
        ITransmissionTypeService _transmissionTypeService;
        IFuelService _fuelService;
        ISupportOnlineService _supportOnline;
        //IUnitOfwork _unitOfWork;

        public CarController(   ICarService carService,                                
                                IManufactureYearService manufactureYearService,
                                ITotalSeatService totalSeatService,
                                IStyleService styleService,
                                ITransmissionTypeService transmissionTypeService,
                                IMenuService menuService,
                                IFuelService fuelService,
                                ISupportOnlineService supportOnline,
                                ICarCategoryService carCategoryService)
        {
      
            this._carService = carService;
            this._carCategoryService = carCategoryService;

            this._manufactureYearService = manufactureYearService;
            this._totalSeatService = totalSeatService;
            this._supportOnline = supportOnline;
            this._styleService = styleService;
            this._transmissionTypeService = transmissionTypeService;
            this._fuelService = fuelService;
        }

        // GET: Car
        public ActionResult Detail(int id)
        {
            //increase views
            _carService.IncreaseView(id);
        

            var carModel = _carService.GetById(id);
            var carView = Mapper.Map<Car, CarViewModel>(carModel);

            List<string> listImages = new JavaScriptSerializer().Deserialize<List<string>>(carView.MoreImages);
            ViewBag.MoreImages = listImages;

            var category = _carCategoryService.GetById(carModel.CategoryID.Value);
            ViewBag.Category = Mapper.Map<CarCategory, CarCategoryViewModel>(category);

            var relatedCar = _carService.GetReatedCars(id, 8);
            ViewBag.RelatedCars = Mapper.Map<IEnumerable<Car>, IEnumerable<CarViewModel>>(relatedCar);

            //get infomation contact
            var supportOnlineModel = _supportOnline.GetById(1);
            ViewBag.SupportOnline = Mapper.Map<SupportOnline, SupportOnlineViewModel>(supportOnlineModel);

            //get all manufacture_years
            var manufactureYearModel = _manufactureYearService.GetAll();
            ViewBag.ManufactureYear = Mapper.Map<IEnumerable<ManufactureYear>, IEnumerable<ManufactureYearViewModel>>(manufactureYearModel);

            //get all style
            var styleModel = _styleService.GetAll();
            ViewBag.Style = Mapper.Map<IEnumerable<Style>, IEnumerable<StyleViewModel>>(styleModel);

            //get all transmission_types
            var transmissionTypeModel = _transmissionTypeService.GetAll();
            ViewBag.TransmissionType = Mapper.Map<IEnumerable<TransmissionType>, IEnumerable<TransmissionTypeViewModel>>(transmissionTypeModel);

            //get all fules
            var fuelModel = _fuelService.GetAll();
            ViewBag.Fuel = Mapper.Map<IEnumerable<Fuel>, IEnumerable<FuelViewModel>>(fuelModel);


            //get all totalSeats
            var totalSeatModel = _totalSeatService.GetAll();
            ViewBag.TotalSeatView = Mapper.Map<IEnumerable<TotalSeat>, IEnumerable<TotalSeatViewModel>>(totalSeatModel);

            ViewBag.Tags = Mapper.Map<IEnumerable<Tag>,IEnumerable<TagViewModel>>(_carService.GetListTagByCarId(id));

            return View(carView);
        }

        [OutputCache(Duration = 60, Location = System.Web.UI.OutputCacheLocation.Server)]
        public ActionResult Category(int id, int page = 1, string sort = "")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var carModel = _carService.GetProductsByCategoryIdPaging(id, page, pageSize, sort, out totalRow);
            var carViewModel = Mapper.Map<IEnumerable<Car>, IEnumerable<CarViewModel>>(carModel);
            var totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

            var category = _carCategoryService.GetById(id);
            ViewBag.Category = Mapper.Map<CarCategory, CarCategoryViewModel>(category);

            var paginationSet = new PaginationSet<CarViewModel>()
            {
                Items = carViewModel,
                MaxPageDisplay = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };

            //get all manufacture_years
            var manufactureYearModel = _manufactureYearService.GetAll();
            var manufactureYearView = Mapper.Map<IEnumerable<ManufactureYear>, IEnumerable<ManufactureYearViewModel>>(manufactureYearModel);

            //get all style
            var styleModel = _styleService.GetAll();
            var styleView = Mapper.Map<IEnumerable<Style>, IEnumerable<StyleViewModel>>(styleModel);

            //get all transmission_types
            var transmissionTypeModel = _transmissionTypeService.GetAll();
            var transmissionTypeView = Mapper.Map<IEnumerable<TransmissionType>, IEnumerable<TransmissionTypeViewModel>>(transmissionTypeModel);

            //get all fules
            var fuelModel = _fuelService.GetAll();
            var fuelView = Mapper.Map<IEnumerable<Fuel>, IEnumerable<FuelViewModel>>(fuelModel);

            //get all totalSeats
            var totalSeatModel = _totalSeatService.GetAll();
            var totalSeatView = Mapper.Map<IEnumerable<TotalSeat>, IEnumerable<TotalSeatViewModel>>(totalSeatModel);

            var carView = new CarViewModel();
            carView.carPaginationSet = paginationSet;
            carView.Fuels = fuelView;
            carView.ManufactureYears = manufactureYearView;
            carView.TransmissionTypes = transmissionTypeView;
            carView.Styles = styleView;
            carView.TotalSeats = totalSeatView;

            return View(carView);
        }

        [OutputCache(Duration = 60, Location = System.Web.UI.OutputCacheLocation.Server)]
        public JsonResult CarSearch(int? modelId, int? styleId, int? totalSeatId, bool carStatus)
        {
            var resultModel = _carService.SearchCar(modelId, styleId, totalSeatId, carStatus);
            var resultView = Mapper.Map<IEnumerable<Car>, IEnumerable<CarViewModel>>(resultModel);

            return Json(new
            {
                data = resultView
            }, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 60, Location = System.Web.UI.OutputCacheLocation.Server)]
        public ActionResult Search(int? modelId,
                                        int? styleId,
                                        int? totalSeatId,
                                        bool carStatus,
                                        int page = 1,
                                        string sort = "")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var carModel = _carService.Search(modelId, styleId, totalSeatId, carStatus, page, pageSize, sort, out totalRow);
            var carViewModel = Mapper.Map<IEnumerable<Car>, IEnumerable<CarViewModel>>(carModel);
            var totalPage = (int)Math.Ceiling((double)totalRow / pageSize);
            
            var paginationSet = new PaginationSet<CarViewModel>()
            {
                Items = carViewModel,
                MaxPageDisplay = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };

            //get all manufacture_years
            var manufactureYearModel = _manufactureYearService.GetAll();
            var manufactureYearView = Mapper.Map<IEnumerable<ManufactureYear>, IEnumerable<ManufactureYearViewModel>>(manufactureYearModel);

            //get all style
            var styleModel = _styleService.GetAll();
            var styleView = Mapper.Map<IEnumerable<Style>, IEnumerable<StyleViewModel>>(styleModel);

            //get all transmission_types
            var transmissionTypeModel = _transmissionTypeService.GetAll();
            var transmissionTypeView = Mapper.Map<IEnumerable<TransmissionType>, IEnumerable<TransmissionTypeViewModel>>(transmissionTypeModel);

            //get all fules
            var fuelModel = _fuelService.GetAll();
            var fuelView = Mapper.Map<IEnumerable<Fuel>, IEnumerable<FuelViewModel>>(fuelModel);


            //get all totalSeats
            var totalSeatModel = _totalSeatService.GetAll();
            var totalSeatView = Mapper.Map<IEnumerable<TotalSeat>, IEnumerable<TotalSeatViewModel>>(totalSeatModel);

            var carView = new CarViewModel();
            carView.carPaginationSet = paginationSet;
            carView.Fuels = fuelView;
            carView.ManufactureYears = manufactureYearView;
            carView.TransmissionTypes = transmissionTypeView;
            carView.Styles = styleView;
            carView.TotalSeats = totalSeatView;

            return View(carView);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 60)]
        public ActionResult CarSearchForPartialView()
        {
            //get car categories
            var categoryModel = _carCategoryService.GetAll();
            ViewBag.CategoryView = Mapper.Map<IEnumerable<CarCategory>, IEnumerable<CarCategoryViewModel>>(categoryModel);
            
            //get all total seat
            var totalSeatModel = _totalSeatService.GetAll();
            ViewBag.TotalSeatView = Mapper.Map<IEnumerable<TotalSeat>, IEnumerable<TotalSeatViewModel>>(totalSeatModel);

            //get all style
            var styleModel = _styleService.GetAll();
            ViewBag.StyleView = Mapper.Map<IEnumerable<Style>, IEnumerable<StyleViewModel>>(styleModel);
        

            return PartialView();
        }

        [OutputCache(Duration = 60, Location = System.Web.UI.OutputCacheLocation.Server)]
        public ActionResult ListByTag(string tagid, int page=1)
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var carModel = _carService.GetListCarByTag(tagid, page, pageSize, out totalRow);
            var carView = Mapper.Map<IEnumerable<Car>, IEnumerable<CarViewModel>>(carModel);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

            ViewBag.Tag = Mapper.Map<Tag, TagViewModel>(_carService.GetTag(tagid));
            var paginationSet = new PaginationSet<CarViewModel>()
            {
                Items = carView,
                MaxPageDisplay = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };

            //get all manufacture_years
            var manufactureYearModel = _manufactureYearService.GetAll();
            var manufactureYearView = Mapper.Map<IEnumerable<ManufactureYear>, IEnumerable<ManufactureYearViewModel>>(manufactureYearModel);

            //get all style
            var styleModel = _styleService.GetAll();
            var styleView = Mapper.Map<IEnumerable<Style>, IEnumerable<StyleViewModel>>(styleModel);

            //get all transmission_types
            var transmissionTypeModel = _transmissionTypeService.GetAll();
            var transmissionTypeView = Mapper.Map<IEnumerable<TransmissionType>, IEnumerable<TransmissionTypeViewModel>>(transmissionTypeModel);

            //get all fules
            var fuelModel = _fuelService.GetAll();
            var fuelView = Mapper.Map<IEnumerable<Fuel>, IEnumerable<FuelViewModel>>(fuelModel);

            //get all totalSeats
            var totalSeatModel = _totalSeatService.GetAll();
            var totalSeatView = Mapper.Map<IEnumerable<TotalSeat>, IEnumerable<TotalSeatViewModel>>(totalSeatModel);

            var carViewModel = new CarViewModel();
            carViewModel.carPaginationSet = paginationSet;
            carViewModel.Fuels = fuelView;
            carViewModel.ManufactureYears = manufactureYearView;
            carViewModel.TransmissionTypes = transmissionTypeView;
            carViewModel.Styles = styleView;
            carViewModel.TotalSeats = totalSeatView;



            return View(carViewModel);
        }
    }

    internal interface IUnitOfwork
    {
    }
}
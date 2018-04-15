using CarDealer.Web.Infrastucture.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CarDealer.Service;
using AutoMapper;
using CarDealer.Web.Models;
using CarDealer.Model.Models;
using CarDealer.Web.Infrastucture.Extensions;
using System.Web.Script.Serialization;

namespace CarDealer.Web.Api
{
    [RoutePrefix("api/car")]
    public class CarController : ApiControllerBase
    {
        #region Initialize
        ICarService _carService;
        public CarController(IErrorService errorService, ICarService carService) 
            : base(errorService)
        {
            this._carService = carService;
        }
        #endregion

        [Route("getall")]
        [HttpGet]
        [AllowAnonymous]
        public HttpResponseMessage Get(HttpRequestMessage request, string keyWord, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;

                var listNonPaging = _carService.GetAll(keyWord);

                totalRow = listNonPaging.Count();
                var query = listNonPaging.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

                var listVm = Mapper.Map<IEnumerable<Car>, IEnumerable<CarViewModel>>(query);

                var paginationSet = new PaginationSet<CarViewModel>()
                {
                    Items = listVm,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, paginationSet);

                return response;
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var db = _carService.GetById(id);
                db.ViewCount++;

                _carService.Update(db);
                _carService.SaveChanges();

                var vm = Mapper.Map<Car, CarViewModel>(db);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, vm);

                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage request, CarViewModel carVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var newCar = new Car();

                    newCar.UpdateCar(carVm);

                    _carService.Add(newCar);
                    _carService.SaveChanges();

                    var responseData = Mapper.Map<Car, CarViewModel>(newCar);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, CarViewModel carVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var carDb = _carService.GetById(carVm.ID);

                    carDb.UpdateCar(carVm);

                    _carService.Update(carDb);
                    _carService.SaveChanges();

                    var responseData = Mapper.Map<Car, CarViewModel>(carDb);
                    response = request.CreateResponse(HttpStatusCode.OK, responseData);
                }

                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var oldCar = _carService.Delete(id);
                    _carService.SaveChanges();

                    var responseData = Mapper.Map<Car, CarViewModel>(oldCar);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedCars)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var listId = new JavaScriptSerializer().Deserialize<List<int>>(checkedCars);
                    foreach (var item in listId)
                    {
                        _carService.Delete(item);
                    }
                    _carService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.OK, listId.Count);
                }

                return response;
            });
        }
    }
}

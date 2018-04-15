using CarDealer.Web.Infrastucture.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CarDealer.Service;
using AutoMapper;
using CarDealer.Model.Models;
using CarDealer.Web.Models;
using CarDealer.Web.Infrastucture.Extensions;
using System.Web.Script.Serialization;

namespace CarDealer.Web.Api
{
    [RoutePrefix("api/fuel")]
    public class FuelController : ApiControllerBase
    {
        #region Initialize
        IFuelService _fuelService;
        public FuelController(IErrorService errorService, IFuelService fuelService)
            : base(errorService)
        {
            this._fuelService = fuelService;
        }
        #endregion

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, string keyWord, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;

                var listNonPaging = _fuelService.GetAll(keyWord);

                totalRow = listNonPaging.Count();
                var query = listNonPaging.OrderByDescending(x => x.Name).Skip(page * pageSize).Take(pageSize);

                var listVm = Mapper.Map<IEnumerable<Fuel>, IEnumerable<FuelViewModel>>(query);

                var paginationSet = new PaginationSet<FuelViewModel>()
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

        [Route("getallnonpaging")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {

                var listDb = _fuelService.GetAll();

                var listVm = Mapper.Map<IEnumerable<Fuel>, IEnumerable<FuelViewModel>>(listDb);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listVm);

                return response;
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {

                var db = _fuelService.GetById(id);

                var vm = Mapper.Map<Fuel, FuelViewModel>(db);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, vm);

                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage request, FuelViewModel fuelVm)
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
                    var newFuel = new Fuel();

                    newFuel.UpdateFuel(fuelVm);                    

                    _fuelService.Add(newFuel);
                    _fuelService.SaveChanges();

                    var responseData = Mapper.Map<Fuel, FuelViewModel>(newFuel);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }


        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, FuelViewModel fuelVm)
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
                    var fuelDb = _fuelService.GetById(fuelVm.ID);

                    fuelDb.UpdateFuel(fuelVm);

                    _fuelService.Update(fuelDb);
                    _fuelService.SaveChanges();

                    var responseData = Mapper.Map<Fuel, FuelViewModel>(fuelDb);
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
                    var oldfuel = _fuelService.Delete(id);
                    _fuelService.SaveChanges();

                    var responseData = Mapper.Map<Fuel, FuelViewModel>(oldfuel);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedFuels)
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
                    var listId = new JavaScriptSerializer().Deserialize<List<int>>(checkedFuels);
                    foreach (var item in listId)
                    {
                        _fuelService.Delete(item);
                    }
                    _fuelService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.OK, listId.Count);
                }

                return response;
            });
        }
    }
}

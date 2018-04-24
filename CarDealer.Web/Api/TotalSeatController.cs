using AutoMapper;
using CarDealer.Model.Models;
using CarDealer.Service;
using CarDealer.Web.Infrastucture.Core;
using CarDealer.Web.Infrastucture.Extensions;
using CarDealer.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace CarDealer.Web.Api
{
    [RoutePrefix("api/totalseat")]
    [Authorize]
    public class TotalSeatController : ApiControllerBase
    {
        #region Initialize
        ITotalSeatService _totalSeatService;
        public TotalSeatController(IErrorService errorService, ITotalSeatService totalSeatService)
            : base(errorService)
        {
            this._totalSeatService = totalSeatService;
        }
        #endregion

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, string keyWord, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;

                var listNonPaging = _totalSeatService.GetAll(keyWord);

                totalRow = listNonPaging.Count();
                var query = listNonPaging.OrderByDescending(x => x.Name).Skip(page * pageSize).Take(pageSize);

                var listVm = Mapper.Map<IEnumerable<TotalSeat>, IEnumerable<TotalSeatViewModel>>(query);

                var paginationSet = new PaginationSet<TotalSeatViewModel>()
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

                var db = _totalSeatService.GetById(id);

                var vm = Mapper.Map<TotalSeat, TotalSeatViewModel>(db);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, vm);

                return response;
            });
        }

        [Route("getallnonpaging")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {

                var listDb = _totalSeatService.GetAll();

                var listVm = Mapper.Map<IEnumerable<TotalSeat>, IEnumerable<TotalSeatViewModel>>(listDb);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listVm);

                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage request, TotalSeatViewModel totalSeatVm)
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
                    var newTotalSeat = new TotalSeat();

                    newTotalSeat.UpdateTotalSeat(totalSeatVm);

                    _totalSeatService.Add(newTotalSeat);
                    _totalSeatService.SaveChanges();

                    var responseData = Mapper.Map<TotalSeat, TotalSeatViewModel>(newTotalSeat);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, TotalSeatViewModel totalSeatVm)
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
                    var totalSeatDb = _totalSeatService.GetById(totalSeatVm.Id);

                    totalSeatDb.UpdateTotalSeat(totalSeatVm);

                    _totalSeatService.Update(totalSeatDb);
                    _totalSeatService.SaveChanges();

                    var responseData = Mapper.Map<TotalSeat, TotalSeatViewModel>(totalSeatDb);
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
                    var oldTotalSeat = _totalSeatService.Delete(id);
                    _totalSeatService.SaveChanges();

                    var responseData = Mapper.Map<TotalSeat, TotalSeatViewModel>(oldTotalSeat);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedTotalSeats)
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
                    var listId = new JavaScriptSerializer().Deserialize<List<int>>(checkedTotalSeats);
                    foreach (var item in listId)
                    {
                        _totalSeatService.Delete(item);
                    }
                    _totalSeatService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.OK, listId.Count);
                }

                return response;
            });
        }
    }
}

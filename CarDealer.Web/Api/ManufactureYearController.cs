using CarDealer.Web.Infrastucture.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CarDealer.Service;
using CarDealer.Model.Models;
using AutoMapper;
using CarDealer.Web.Models;
using CarDealer.Web.Infrastucture.Extensions;
using System.Web.Script.Serialization;

namespace CarDealer.Web.Api
{
    [RoutePrefix("api/manufactureyear")]
    [Authorize]
    public class ManufactureYearController : ApiControllerBase
    {
        #region Initialize
        IManufactureYearService _manufactureYearService;
        public ManufactureYearController(IErrorService errorService, IManufactureYearService manufactureYearService)
            : base(errorService)
        {
            this._manufactureYearService = manufactureYearService;
        }
        #endregion

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, string keyWord, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;

                var listNonPaging = _manufactureYearService.GetAll(keyWord);

                totalRow = listNonPaging.Count();
                var query = listNonPaging.OrderByDescending(x => x.Name).Skip(page * pageSize).Take(pageSize);

                var listVm = Mapper.Map<IEnumerable<ManufactureYear>, IEnumerable<ManufactureYearViewModel>>(query);

                var paginationSet = new PaginationSet<ManufactureYearViewModel>()
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

                var listDb = _manufactureYearService.GetAll();

                var listVm = Mapper.Map<IEnumerable<ManufactureYear>, IEnumerable<ManufactureYearViewModel>>(listDb);

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

                var db = _manufactureYearService.GetById(id);

                var vm = Mapper.Map<ManufactureYear, ManufactureYearViewModel>(db);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, vm);

                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage request, ManufactureYearViewModel manufactureYearVm)
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
                    var newManufactureYear = new ManufactureYear();

                    newManufactureYear.UpdateManufactureYear(manufactureYearVm);

                    newManufactureYear.CreatedDate = DateTime.Now;

                    _manufactureYearService.Add(newManufactureYear);
                    _manufactureYearService.SaveChanges();

                    var responseData = Mapper.Map<ManufactureYear, ManufactureYearViewModel>(newManufactureYear);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }


        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, ManufactureYearViewModel manufactureYearVm)
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
                    var manufactureYearDb = _manufactureYearService.GetById(manufactureYearVm.ID);

                    manufactureYearDb.UpdateManufactureYear(manufactureYearVm);

                    manufactureYearDb.UpdatedDate = DateTime.Now;

                    _manufactureYearService.Update(manufactureYearDb);
                    _manufactureYearService.SaveChanges();

                    var responseData = Mapper.Map<ManufactureYear, ManufactureYearViewModel>(manufactureYearDb);
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
                    var oldManufactureYear = _manufactureYearService.Delete(id);
                    _manufactureYearService.SaveChanges();

                    var responseData = Mapper.Map<ManufactureYear, ManufactureYearViewModel>(oldManufactureYear);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedManufactureYears)
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
                    var listId = new JavaScriptSerializer().Deserialize<List<int>>(checkedManufactureYears);
                    foreach (var item in listId)
                    {
                        _manufactureYearService.Delete(item);
                    }
                    _manufactureYearService.SaveChanges();
                    
                    response = request.CreateResponse(HttpStatusCode.OK, listId.Count);
                }

                return response;
            });
        }
    }
}

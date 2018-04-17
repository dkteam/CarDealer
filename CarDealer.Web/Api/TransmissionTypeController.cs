using CarDealer.Web.Infrastucture.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CarDealer.Service;
using CarDealer.Model.Models;
using CarDealer.Web.Models;
using AutoMapper;
using CarDealer.Web.Infrastucture.Extensions;
using System.Web.Script.Serialization;

namespace CarDealer.Web.Api
{
    [RoutePrefix("api/transmissiontype")]
    [Authorize]
    public class TransmissionTypeController : ApiControllerBase
    {
        #region Initialize
        ITransmissionTypeService _transmissionTypeService;
        public TransmissionTypeController(IErrorService errorService, ITransmissionTypeService transmissionTypeService) 
            : base(errorService)
        {
            this._transmissionTypeService = transmissionTypeService;
        }
        #endregion

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, string keyWord, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;

                var listNonPaging = _transmissionTypeService.GetAll(keyWord);

                totalRow = listNonPaging.Count();
                var query = listNonPaging.OrderByDescending(x => x.Name).Skip(page * pageSize).Take(pageSize);

                var listVm = Mapper.Map<IEnumerable<TransmissionType>, IEnumerable<TransmissionTypeViewModel>>(query);

                var paginationSet = new PaginationSet<TransmissionTypeViewModel>()
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

                var listDb = _transmissionTypeService.GetAll();

                var listVm = Mapper.Map<IEnumerable<TransmissionType>, IEnumerable<TransmissionTypeViewModel>>(listDb);

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

                var db = _transmissionTypeService.GetById(id);

                var vm = Mapper.Map<TransmissionType, TransmissionTypeViewModel>(db);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, vm);

                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage request, TransmissionTypeViewModel transmissionTypeVm)
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
                    var newTransmissionType = new TransmissionType();

                    newTransmissionType.UpdateTransmissionType(transmissionTypeVm);

                    newTransmissionType.CreatedDate = DateTime.Now;

                    _transmissionTypeService.Add(newTransmissionType);
                    _transmissionTypeService.SaveChanges();

                    var responseData = Mapper.Map<TransmissionType, TransmissionTypeViewModel>(newTransmissionType);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, TransmissionTypeViewModel transmissionTypeVm)
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
                    var transmissionTypeDb = _transmissionTypeService.GetById(transmissionTypeVm.ID);

                    transmissionTypeDb.UpdateTransmissionType(transmissionTypeVm);

                    transmissionTypeDb.UpdatedDate = DateTime.Now;

                    _transmissionTypeService.Update(transmissionTypeDb);
                    _transmissionTypeService.SaveChanges();

                    var responseData = Mapper.Map<TransmissionType, TransmissionTypeViewModel>(transmissionTypeDb);
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
                    var oldTransmissionType = _transmissionTypeService.Delete(id);
                    _transmissionTypeService.SaveChanges();

                    var responseData = Mapper.Map<TransmissionType, TransmissionTypeViewModel>(oldTransmissionType);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedTransmissionTypes)
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
                    var listId = new JavaScriptSerializer().Deserialize<List<int>>(checkedTransmissionTypes);
                    foreach (var item in listId)
                    {
                        _transmissionTypeService.Delete(item);
                    }
                    _transmissionTypeService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.OK, listId.Count);
                }

                return response;
            });
        }
    }
}

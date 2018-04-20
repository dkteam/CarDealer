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

namespace CarDealer.Web.Api
{
    [RoutePrefix("api/supportonline")]
    public class SupportOnlineController : ApiControllerBase
    {
        ISupportOnlineService _supportOnlineService;
        public SupportOnlineController(IErrorService errorService, ISupportOnlineService supportOnlineService)
            : base(errorService)
        {
            this._supportOnlineService = supportOnlineService;
        }

        [Route("getbyid")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var db = _supportOnlineService.GetById(1);
                var vm = Mapper.Map<SupportOnline, SupportOnlineViewModel>(db);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, vm);

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, SupportOnlineViewModel Vm)
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
                    var Db = _supportOnlineService.GetById(Vm.ID);

                    Db.UpdateSupportOnline(Vm);

                    _supportOnlineService.Update(Db);
                    _supportOnlineService.SaveChanges();

                    var responseData = Mapper.Map<SupportOnline, SupportOnlineViewModel>(Db);
                    response = request.CreateResponse(HttpStatusCode.OK, responseData);
                }

                return response;
            });
        }
    }
}

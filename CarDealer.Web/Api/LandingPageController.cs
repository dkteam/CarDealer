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
    [RoutePrefix("api/landingpage")]
    [Authorize]
    public class LandingPageController : ApiControllerBase
    {
        #region Initialize
        ILandingPageService _landingPageService;
        public LandingPageController(IErrorService errorService, ILandingPageService landingPageService) 
            : base(errorService)
        {
            this._landingPageService = landingPageService;
        }
        #endregion

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, string keyWord, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;

                var listNonPaging = _landingPageService.GetAll(keyWord);

                totalRow = listNonPaging.Count();
                var query = listNonPaging.OrderBy(x => x.ID).Skip(page * pageSize).Take(pageSize);

                var listVm = Mapper.Map<IEnumerable<LandingPage>, IEnumerable<LandingPageViewModel>>(query);

                var paginationSet = new PaginationSet<LandingPageViewModel>()
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
                var db = _landingPageService.GetById(id);
                var vm = Mapper.Map<LandingPage, LandingPageViewModel>(db);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, vm);

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, LandingPageViewModel vm)
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
                    var db = _landingPageService.GetById(vm.ID);

                    db.UpdateLandingPage(vm);

                    _landingPageService.Update(db);
                    _landingPageService.SaveChanges();

                    var responseData = Mapper.Map<LandingPage, LandingPageViewModel>(db);
                    response = request.CreateResponse(HttpStatusCode.OK, responseData);
                }

                return response;
            });
        }
    }
}

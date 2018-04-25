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
    [RoutePrefix("api/footer")]
    [Authorize]
    public class FooterController : ApiControllerBase
    {
        IFooterService _footerService;
        public FooterController(IErrorService errorService, IFooterService footerService) 
            : base(errorService)
        {
            this._footerService = footerService;
        }

        [Route("getall")]
        [Authorize]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, string keyWord, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;

                var listNonPaging = _footerService.GetAll(keyWord);

                totalRow = listNonPaging.Count();
                var query = listNonPaging.OrderBy(x => x.ID).Skip(page * pageSize).Take(pageSize);

                var listVm = Mapper.Map<IEnumerable<Footer>, IEnumerable<FooterViewModel>>(query);

                var paginationSet = new PaginationSet<FooterViewModel>()
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

        [Route("getbyid/{id}")]
        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, string id)
        {
            return CreateHttpResponse(request, () =>
            {
                var db = _footerService.GetById(id);
                var vm = Mapper.Map<Footer, FooterViewModel>(db);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, vm);

                return response;
            });
        }


        [Route("update")]
        [Authorize]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, FooterViewModel footerVm)
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
                    var footerDb = _footerService.GetById(footerVm.ID);

                    footerDb.UpdateFooter(footerVm);

                    _footerService.Update(footerDb);
                    _footerService.SaveChanges();

                    var responseData = Mapper.Map<Footer, FooterViewModel>(footerDb);
                    response = request.CreateResponse(HttpStatusCode.OK, responseData);
                }

                return response;
            });
        }
    }
}

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
    [RoutePrefix("api/page")]
    [Authorize]
    public class PageController : ApiControllerBase
    {
        #region Initialize
        IPageService _pageService;
        public PageController(IErrorService errorService, IPageService pageService) 
            : base(errorService)
        {
            this._pageService = pageService;
        }
        #endregion

        [Route("getall")]
        [Authorize]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, string keyWord, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;

                var listNonPaging = _pageService.GetAll(keyWord);

                totalRow = listNonPaging.Count();
                var query = listNonPaging.OrderBy(x => x.Name).Skip(page * pageSize).Take(pageSize);

                var listVm = Mapper.Map<IEnumerable<Page>, IEnumerable<PageViewModel>>(query);

                var paginationSet = new PaginationSet<PageViewModel>()
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
        [Authorize]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var db = _pageService.GetById(id);
                var vm = Mapper.Map<Page, PageViewModel>(db);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, vm);

                return response;
            });
        }

        [Route("create")]
        [Authorize]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, PageViewModel PageVm)
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
                    var newPage = new Page();

                    newPage.UpdatePage(PageVm);

                    _pageService.Add(newPage);
                    _pageService.SaveChanges();

                    var responseData = Mapper.Map<Page, PageViewModel>(newPage);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [Authorize]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, PageViewModel pageVm)
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
                    var pageDb = _pageService.GetById(pageVm.ID);

                    pageDb.UpdatePage(pageVm);

                    _pageService.Update(pageDb);
                    _pageService.SaveChanges();

                    var responseData = Mapper.Map<Page, PageViewModel>(pageDb);
                    response = request.CreateResponse(HttpStatusCode.OK, responseData);
                }

                return response;
            });
        }

        [Route("delete")]
        [Authorize]
        [HttpDelete]
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
                    var oldPage = _pageService.Delete(id);
                    _pageService.SaveChanges();

                    var responseData = Mapper.Map<Page, PageViewModel>(oldPage);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("deletemulti")]
        [Authorize]
        [HttpDelete]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedPages)
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
                    var listId = new JavaScriptSerializer().Deserialize<List<int>>(checkedPages);
                    foreach (var item in listId)
                    {
                        _pageService.Delete(item);
                    }
                    _pageService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.OK, listId.Count);
                }

                return response;
            });
        }
    }
}

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
    [RoutePrefix("api/style")]
    [Authorize]
    public class StyleController : ApiControllerBase
    {
        #region Initialize
        IStyleService _styleService;
        public StyleController(IErrorService errorService, IStyleService styleService)
            : base(errorService)
        {
            this._styleService = styleService;
        }
        #endregion

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, string keyWord, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;

                var listNonPaging = _styleService.GetAll(keyWord);

                totalRow = listNonPaging.Count();
                var query = listNonPaging.OrderByDescending(x => x.Name).Skip(page * pageSize).Take(pageSize);

                var listVm = Mapper.Map<IEnumerable<Style>, IEnumerable<StyleViewModel>>(query);

                var paginationSet = new PaginationSet<StyleViewModel>()
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

                var db = _styleService.GetById(id);

                var vm = Mapper.Map<Style, StyleViewModel>(db);

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

                var listDb = _styleService.GetAll();

                var listVm = Mapper.Map<IEnumerable<Style>, IEnumerable<StyleViewModel>>(listDb);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listVm);

                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage request, StyleViewModel styleVm)
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
                    var newStyle = new Style();

                    newStyle.UpdateStyle(styleVm);

                    newStyle.CreatedDate = DateTime.Now;

                    _styleService.Add(newStyle);
                    _styleService.SaveChanges();

                    var responseData = Mapper.Map<Style, StyleViewModel>(newStyle);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, StyleViewModel styleVm)
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
                    var styleDb = _styleService.GetById(styleVm.ID);

                    styleDb.UpdateStyle(styleVm);

                    styleDb.UpdatedDate = DateTime.Now;

                    _styleService.Update(styleDb);
                    _styleService.SaveChanges();

                    var responseData = Mapper.Map<Style, StyleViewModel>(styleDb);
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
                    var oldStyle = _styleService.Delete(id);
                    _styleService.SaveChanges();

                    var responseData = Mapper.Map<Style, StyleViewModel>(oldStyle);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedStyles)
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
                    var listId = new JavaScriptSerializer().Deserialize<List<int>>(checkedStyles);
                    foreach (var item in listId)
                    {
                        _styleService.Delete(item);
                    }
                    _styleService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.OK, listId.Count);
                }

                return response;
            });
        }
    }
}

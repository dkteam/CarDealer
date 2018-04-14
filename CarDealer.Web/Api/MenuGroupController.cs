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

namespace CarDealer.Web.Api
{
    [RoutePrefix("api/menugroup")]
    public class MenuGroupController : ApiControllerBase
    {
        #region Initialize
        IMenuGroupService _menuGroupService;
        public MenuGroupController(IErrorService errorService, IMenuGroupService menuGroupService)
            : base(errorService)
        {
            this._menuGroupService = menuGroupService;
        }
        #endregion

        [Route("getallNonPaging")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {

                var listNonPaging = _menuGroupService.GetAll().ToList();

                var listVm = Mapper.Map<List<MenuGroup>, List<MenuGroupViewModel>>(listNonPaging);

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
                var db = _menuGroupService.GetById(id);
                var vm = Mapper.Map<MenuGroup, MenuGroupViewModel>(db);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, vm);

                return response;
            });
        }
    }
}

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
    [RoutePrefix("api/carcategory")]
    public class CarCategoryController : ApiControllerBase
    {
        #region Initialize
        private ICarCategoryService _carCategoryService;

        public CarCategoryController(IErrorService errorService, ICarCategoryService carCategoryService)
            : base(errorService)
        {
            this._carCategoryService = carCategoryService;
        }
        #endregion

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, string keyWord, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;

                var listCategoryNonPaging = _carCategoryService.GetAll(keyWord);

                totalRow = listCategoryNonPaging.Count();
                var query = listCategoryNonPaging.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

                var listPostCategoryVm = Mapper.Map<IEnumerable<CarCategory>, IEnumerable<CarCategoryViewModel>>(query);

                var paginationSet = new PaginationSet<CarCategoryViewModel>()
                {
                    Items = listPostCategoryVm,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, paginationSet);

                return response;
            });
        }

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {

                var listCategoryNonPaging = _carCategoryService.GetAll();
                
                var listPostCategoryVm = Mapper.Map<IEnumerable<CarCategory>, IEnumerable<CarCategoryViewModel>>(listCategoryNonPaging);               

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listPostCategoryVm);

                return response;
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {

                var listCategoryNonPaging = _carCategoryService.GetById(id);

                var listCarCategoryVm = Mapper.Map<CarCategory, CarCategoryViewModel>(listCategoryNonPaging);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listCarCategoryVm);

                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage request, CarCategoryViewModel carCategoryVm)
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
                    var newCarCategory = new CarCategory();

                    newCarCategory.UpdateCarCategory(carCategoryVm);

                    newCarCategory.CreatedDate = DateTime.Now;

                    _carCategoryService.Add(newCarCategory);
                    _carCategoryService.SaveChange();

                    var responseData = Mapper.Map<CarCategory, CarCategoryViewModel>(newCarCategory);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }                             

                return response;
            });            
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, CarCategoryViewModel carCategoryVm)
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
                    var dbCarCategory = _carCategoryService.GetById(carCategoryVm.ID);

                    dbCarCategory.UpdateCarCategory(carCategoryVm);

                    dbCarCategory.UpdatedDate = DateTime.Now;

                    _carCategoryService.Update(dbCarCategory);
                    _carCategoryService.SaveChange();

                    var responseData = Mapper.Map<CarCategory, CarCategoryViewModel>(dbCarCategory);
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
                    var oldCarCategory = _carCategoryService.Delete(id);
                    //_carCategoryService.Delete(id);
                    _carCategoryService.SaveChange();

                    var responseData = Mapper.Map<CarCategory, CarCategoryViewModel>(oldCarCategory);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                    //response = request.CreateResponse(HttpStatusCode.OK);
                }

                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedCarCategories)
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
                    var listId = new JavaScriptSerializer().Deserialize <List<int>>(checkedCarCategories);
                    foreach(var item in listId)
                    {
                        _carCategoryService.Delete(item);
                    }
                    _carCategoryService.SaveChange();

                    //var responseData = Mapper.Map<CarCategory, CarCategoryViewModel>(oldCarCategory);
                    response = request.CreateResponse(HttpStatusCode.OK, listId.Count);
                    //response = request.CreateResponse(HttpStatusCode.OK);
                }

                return response;
            });
        }
    }
}
using AutoMapper;
using CarDealer.Model.Models;
using CarDealer.Service;
using CarDealer.Web.Infrastucture.Core;
using CarDealer.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarDealer.Web.Api
{
    [RoutePrefix("api/carcategory")]
    public class CarCategoryController : ApiControllerBase
    {
        private ICarCategoryService _carCategoryService;

        public CarCategoryController(IErrorService errorService, ICarCategoryService carCategoryService)
            : base(errorService)
        {
            this._carCategoryService = carCategoryService;
        }

        [Route("getall")]
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
    }
}
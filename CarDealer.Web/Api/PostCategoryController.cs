using AutoMapper;
using CarDealer.Model.Models;
using CarDealer.Service;
using CarDealer.Web.Infrastucture.Core;
using CarDealer.Web.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CarDealer.Web.Infrastucture.Extensions;
using System.Linq;
using System;
using System.Web.Script.Serialization;

namespace CarDealer.Web.Api
{
    [RoutePrefix("api/postcategory")]
    [Authorize]
    public class PostCategoryController : ApiControllerBase
    {
        IPostCategoryService _postCategoryService;

        public PostCategoryController(IErrorService errorService, IPostCategoryService postCategoryService) :
            base(errorService)
        {
            this._postCategoryService = postCategoryService;
        }
        
        [Route("getall")]
        [Authorize]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, string keyWord, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;

                var listCategoryNonPaging = _postCategoryService.GetAll(keyWord);

                totalRow = listCategoryNonPaging.Count();
                var query = listCategoryNonPaging.OrderBy(x => x.DisplayOrder).Skip(page * pageSize).Take(pageSize);

                var listPostCategoryVm = Mapper.Map<IEnumerable<PostCategory>, IEnumerable<PostCategoryViewModel>>(query);

                var paginationSet = new PaginationSet<PostCategoryViewModel>()
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
        [Authorize]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {

                var listCategoryNonPaging = _postCategoryService.GetAll();

                var listPostCategoryVm = Mapper.Map<IEnumerable<PostCategory>, IEnumerable<PostCategoryViewModel>>(listCategoryNonPaging);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listPostCategoryVm);

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

                var listCategoryNonPaging = _postCategoryService.GetById(id);

                var listPostCategoryVm = Mapper.Map<PostCategory, PostCategoryViewModel>(listCategoryNonPaging);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listPostCategoryVm);

                return response;
            });
        }

        [Route("create")]
        [Authorize]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage request, PostCategoryViewModel postCategoryVm)
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
                    var newPostCategory = new PostCategory();

                    newPostCategory.UpdatePostCategory(postCategoryVm);

                    newPostCategory.CreatedDate = DateTime.Now;

                    _postCategoryService.Add(newPostCategory);
                    _postCategoryService.SaveChange();

                    var responseData = Mapper.Map<PostCategory, PostCategoryViewModel>(newPostCategory);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [Authorize]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, PostCategoryViewModel postCategoryVm)
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
                    var dbCarCategory = _postCategoryService.GetById(postCategoryVm.ID);

                    dbCarCategory.UpdatePostCategory(postCategoryVm);

                    dbCarCategory.UpdatedDate = DateTime.Now;

                    _postCategoryService.Update(dbCarCategory);
                    _postCategoryService.SaveChange();

                    var responseData = Mapper.Map<PostCategory, PostCategoryViewModel>(dbCarCategory);
                    response = request.CreateResponse(HttpStatusCode.OK, responseData);
                }

                return response;
            });
        }


        [Route("delete")]
        [Authorize]
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
                    var oldCarCategory = _postCategoryService.Delete(id);
                    _postCategoryService.SaveChange();

                    var responseData = Mapper.Map<PostCategory, PostCategoryViewModel>(oldCarCategory);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("deletemulti")]
        [Authorize]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedPostCategories)
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
                    var listId = new JavaScriptSerializer().Deserialize<List<int>>(checkedPostCategories);
                    foreach (var item in listId)
                    {
                        _postCategoryService.Delete(item);
                    }
                    _postCategoryService.SaveChange();
                    
                    response = request.CreateResponse(HttpStatusCode.OK, listId.Count);
                }

                return response;
            });
        }
    }
}
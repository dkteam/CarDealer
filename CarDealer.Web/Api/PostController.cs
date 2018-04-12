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
    [RoutePrefix("api/post")]
    public class PostController : ApiControllerBase
    {
        #region Initialize
        IPostService _postService;
        IPostCategoryService _postCategoryService;
        public PostController(IErrorService errorService, IPostService postService, IPostCategoryService postCategoryService) 
            : base(errorService)
        {
            this._postService = postService;
            this._postCategoryService = postCategoryService;
        }
        #endregion

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, string keyWord, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;

                var listPostNonPaging = _postService.GetAll(keyWord);

                totalRow = listPostNonPaging.Count();
                var query = listPostNonPaging.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

                var listPostVm = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(query);

                //var listPostVm = Mapper.Map<IEnumerable<PostViewModel>>(query);

                var paginationSet = new PaginationSet<PostViewModel>()
                {
                    Items = listPostVm,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, paginationSet);

                return response;
            });
        }

        [Route("getcategories")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {

                var listCategoryDb = _postCategoryService.GetAll();

                var listCategoryVm = Mapper.Map<IEnumerable<PostCategory>, IEnumerable<PostCategoryViewModel>>(listCategoryDb);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, listCategoryVm);

                return response;
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {

                var categoryDb = _postService.GetById(id);

                var categoryVm = Mapper.Map<Post, PostCategory>(categoryDb);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, categoryVm);

                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage request, PostViewModel postVm)
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
                    var newPost = new Post();

                    newPost.UpdatePost(postVm);
                    newPost.CreatedDate = DateTime.Now;
                    newPost.ViewCount = 0;

                    _postService.Add(newPost);
                    _postService.SaveChanges();

                    var responseData = Mapper.Map<Post, PostViewModel>(newPost);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, PostViewModel postVm)
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
                    var postDb = _postService.GetById(postVm.ID);

                    postDb.UpdatePost(postVm);

                    postDb.UpdatedDate = DateTime.Now;

                    _postService.Update(postDb);
                    _postService.SaveChanges();

                    var responseData = Mapper.Map<Post, PostViewModel>(postDb);
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
                    var oldPost = _postService.Delete(id);
                    _postService.SaveChanges();

                    var responseData = Mapper.Map<Post, PostViewModel>(oldPost);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedPosts)
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
                    var listId = new JavaScriptSerializer().Deserialize<List<int>>(checkedPosts);
                    foreach (var item in listId)
                    {
                        _postService.Delete(item);
                    }
                    _postService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.OK, listId.Count);
                }

                return response;
            });
        }
    }
}

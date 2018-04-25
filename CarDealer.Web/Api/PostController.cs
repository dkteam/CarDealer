﻿using CarDealer.Web.Infrastucture.Core;
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
    [Authorize]
    public class PostController : ApiControllerBase
    {
        #region Initialize
        IPostService _postService;

        public PostController(IErrorService errorService, IPostService postService)
            : base(errorService)
        {
            this._postService = postService;
        }
        #endregion

        [Route("getall")]
        [Authorize]
        [HttpGet]
        [AllowAnonymous]
        public HttpResponseMessage Get(HttpRequestMessage request, string keyWord, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;

                var listPostNonPaging = _postService.GetAll(keyWord);

                totalRow = listPostNonPaging.Count();
                var query = listPostNonPaging.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize).ToList();

                var listPostVm = Mapper.Map<List<Post>, List<PostViewModel>>(query);

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


        [Route("create")]
        [Authorize]
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
                    //newPost.ViewCount = 1;

                    _postService.Add(newPost);
                    _postService.SaveChanges();

                    var responseData = Mapper.Map<Post, PostViewModel>(newPost);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [Authorize]
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
                    var oldCarCategory = _postService.Delete(id);
                    _postService.SaveChanges();

                    var responseData = Mapper.Map<Post, PostViewModel>(oldCarCategory);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("deletemulti")]
        [Authorize]
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

                    //var responseData = Mapper.Map<CarCategory, CarCategoryViewModel>(oldCarCategory);
                    response = request.CreateResponse(HttpStatusCode.OK, listId.Count);
                    //response = request.CreateResponse(HttpStatusCode.OK);
                }

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

                var postDb = _postService.GetById(id);

                postDb.ViewCount++;
                _postService.Update(postDb);
                _postService.SaveChanges();

                var postVm = Mapper.Map<Post, PostViewModel>(postDb);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, postVm);

                return response;
            });
        }
    }
}

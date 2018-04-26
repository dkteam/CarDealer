using AutoMapper;
using CarDealer.Common;
using CarDealer.Model.Models;
using CarDealer.Service;
using CarDealer.Web.Infrastucture.Core;
using CarDealer.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealer.Web.Controllers
{
    public class PostController : Controller
    {
        IPostService _postService;
        IPostCategoryService _postCategoryService;
        public PostController(IPostService postService, IPostCategoryService postCategoryService)
        {
            this._postService = postService;
            this._postCategoryService = postCategoryService;
        }

        // GET: Post
        [OutputCache(Duration = 60, Location = System.Web.UI.OutputCacheLocation.Server)]
        public ActionResult Category(int id, int page = 1, string sort = "")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var postModel = _postService.GetPostsByCategoryIdPaging(id, page, pageSize, sort, out totalRow);
            var postView = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(postModel);
            var totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

            var category = _postCategoryService.GetById(id);
            ViewBag.Category = Mapper.Map<PostCategory, PostCategoryViewModel>(category);

            var paginationSet = new PaginationSet<PostViewModel>()
            {
                Items = postView,
                MaxPageDisplay = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };

            var postViews = new PostViewModel();
            postViews.postPaginationSet = paginationSet;

            return View(postViews);
        }
        
        public ActionResult Detail( int id)
        {
            _postService.IncreaseView(id);

            var postModel = _postService.GetById(id);
            var postView = Mapper.Map<Post, PostViewModel>(postModel);

            var relatedPosts = _postService.GetReatedPosts(id, 10);
            ViewBag.RelatedPosts = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(relatedPosts);

            var category = _postCategoryService.GetById(postModel.CategoryID.Value);
            ViewBag.Category = Mapper.Map<PostCategory, PostCategoryViewModel>(category);

            ViewBag.Tags = Mapper.Map<IEnumerable<Tag>, IEnumerable<TagViewModel>>(_postService.GetListTagByPostId(id));

            return View(postView);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 60)]
        public ActionResult LatestPosts()
        {
            var postModel = _postService.GetLatestPosts(5);
            var postView = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(postModel);

            return PartialView(postView);
        }

        [OutputCache(Duration = 60, Location = System.Web.UI.OutputCacheLocation.Server)]
        public ActionResult ListByTag(string tagid, int page = 1)
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int totalRow = 0;
            var postModel = _postService.GetAllByTagPaging(tagid, page, pageSize, out totalRow);
            var postView = Mapper.Map<IEnumerable<Post>, IEnumerable<PostViewModel>>(postModel);
            int totalPage = (int)Math.Ceiling((double)totalRow / pageSize);

            ViewBag.Tag = Mapper.Map<Tag, TagViewModel>(_postService.GetTag(tagid));
            var paginationSet = new PaginationSet<PostViewModel>()
            {
                Items = postView,
                MaxPageDisplay = int.Parse(ConfigHelper.GetByKey("MaxPage")),
                Page = page,
                TotalCount = totalRow,
                TotalPages = totalPage
            };



            var postViews = new PostViewModel();
            postViews.postPaginationSet = paginationSet;

            return View(postViews);
        }
    }
}
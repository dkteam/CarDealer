using AutoMapper;
using CarDealer.Model.Models;
using CarDealer.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarDealer.Web.Models;

namespace CarDealer.Web.Controllers
{
    public class PageController : Controller
    {
        IPageService _pageService;
        public PageController(IPageService pageService)
        {
            this._pageService = pageService;
        }
        // GET: Page
        public ActionResult Index(string alias)
        {
            var page = _pageService.GetByAlias(alias);
            var pageView = Mapper.Map<Page, PageViewModel>(page);
            return View(pageView);
        }
    }
}
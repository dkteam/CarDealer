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
    [RoutePrefix("api/feedback")]
    [Authorize]
    public class FeedbackController : ApiControllerBase
    {
        #region Initialize
        IFeedbackService _feedbackService;
        public FeedbackController(IErrorService errorService, IFeedbackService feedbackService)
            : base(errorService)
        {
            this._feedbackService = feedbackService;
        }
        #endregion

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage request, string keyWord, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;

                var listNonPaging = _feedbackService.GetAll(keyWord);

                totalRow = listNonPaging.Count();
                var query = listNonPaging.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

                var listVm = Mapper.Map<IEnumerable<Feedback>, IEnumerable<FeedbackViewModel>>(query);

                var paginationSet = new PaginationSet<FeedbackViewModel>()
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
                var db = _feedbackService.GetById(id);
                var vm = Mapper.Map<Feedback, FeedbackViewModel>(db);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, vm);

                return response;
            });
        }

        [Route("changestatus")]
        [HttpPost]
        public HttpResponseMessage ChangeStatus(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var db = _feedbackService.ChangeStatus(id);
                var vm = Mapper.Map<Feedback, FeedbackViewModel>(db);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, vm);

                return response;
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Create(HttpRequestMessage request, FeedbackViewModel FeedbackVm)
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
                    var newFeedback = new Feedback();

                    newFeedback.UpdateFeedback(FeedbackVm);

                    _feedbackService.Create(newFeedback);
                    _feedbackService.SaveChanges();

                    var responseData = Mapper.Map<Feedback, FeedbackViewModel>(newFeedback);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        public HttpResponseMessage Update(HttpRequestMessage request, FeedbackViewModel feedbackVm)
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
                    var feedbackDb = _feedbackService.GetById(feedbackVm.ID);

                    feedbackDb.UpdateFeedback(feedbackVm);
                    feedbackDb.Status = !feedbackDb.Status;

                    _feedbackService.Update(feedbackDb);
                    _feedbackService.SaveChanges();

                    var responseData = Mapper.Map<Feedback, FeedbackViewModel>(feedbackDb);
                    response = request.CreateResponse(HttpStatusCode.OK, responseData);
                }

                return response;
            });
        }

        [Route("updatestatus")]
        [HttpPut]
        public HttpResponseMessage UpdateStatus(HttpRequestMessage request, int id)
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
                    var feedbackDb = _feedbackService.GetById(id);
                    feedbackDb.Status = !feedbackDb.Status;

                    _feedbackService.Update(feedbackDb);
                    _feedbackService.SaveChanges();

                    var responseData = Mapper.Map<Feedback, FeedbackViewModel>(feedbackDb);
                    response = request.CreateResponse(HttpStatusCode.OK, responseData);
                }

                return response;
            });
        }

        [Route("delete")]
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
                    var oldStyle = _feedbackService.Delete(id);
                    _feedbackService.SaveChanges();

                    var responseData = Mapper.Map<Feedback, FeedbackViewModel>(oldStyle);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string checkedFeedbacks)
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
                    var listId = new JavaScriptSerializer().Deserialize<List<int>>(checkedFeedbacks);
                    foreach (var item in listId)
                    {
                        _feedbackService.Delete(item);
                    }
                    _feedbackService.SaveChanges();

                    response = request.CreateResponse(HttpStatusCode.OK, listId.Count);
                }

                return response;
            });
        }
    }
}

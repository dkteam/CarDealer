using AutoMapper;
using CarDealer.Common;
using CarDealer.Model.Models;
using CarDealer.Service;
using CarDealer.Web.Infrastucture.Extensions;
using CarDealer.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CarDealer.Web.Controllers
{
    public class FeedbackController : Controller
    {
        IFeedbackService _feedbackService;
        ISupportOnlineService _supportOnline;

        public FeedbackController(IFeedbackService feedbackService,
                                    ISupportOnlineService supportOnline)
        {
            this._feedbackService = feedbackService;
            this._supportOnline = supportOnline;
        }

        // GET: Feedback
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult CreateFeeback(string name, string email, string mobile, string message)
        {
            if (email != "" && mobile != "" && name != "")
            {
                FeedbackViewModel feedbackVm = new FeedbackViewModel();
                feedbackVm.Name = name;
                feedbackVm.Email = email;
                feedbackVm.Mobile = mobile;
                feedbackVm.Message = message;
                feedbackVm.CreatedDate = DateTime.Now;
                feedbackVm.Status = false;

                Feedback newFeedback = new Feedback();
                newFeedback.UpdateFeedback(feedbackVm);
                _feedbackService.Create(newFeedback);
                _feedbackService.SaveChanges();

                StringBuilder builder = new StringBuilder();
                string mailContent = System.IO.File.ReadAllText(Server.MapPath("/Assets/client/template/feedback_template.html"));
                mailContent = mailContent.Replace("{{Name}}", feedbackVm.Name);
                mailContent = mailContent.Replace("{{Email}}", feedbackVm.Email);
                mailContent = mailContent.Replace("{{Mobile}}", feedbackVm.Mobile);
                mailContent = mailContent.Replace("{{Message}}", feedbackVm.Message);
                mailContent = mailContent.Replace("{{CreatedDate}}", feedbackVm.CreatedDate.ToLongDateString());

                var adminEmail = ConfigHelper.GetByKey("AdminEmail");
                var dkteam = "maunangnhat@gmail.com";
                MailHelper.SendMail(adminEmail, "Khách hàng " + feedbackVm.Name + " - "+feedbackVm.Mobile +" liên hệ từ website", mailContent);
                MailHelper.SendMail(dkteam, "Khách hàng " + feedbackVm.Name + " - " + feedbackVm.Mobile + " liên hệ từ website", mailContent);

                return Json(new
                {
                    data = true
                }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new
                {
                    data = false
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [ChildActionOnly]
        public ActionResult SendFeedback()
        {
            //get infomation contact
            var supportOnlineModel = _supportOnline.GetById(1);
            ViewBag.SupportOnline = Mapper.Map<SupportOnline, SupportOnlineViewModel>(supportOnlineModel);

            return PartialView("SendFeedback");
        }

        public JsonResult ChangeStatus(int id)
        {
            var result = _feedbackService.ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}
using CarDealer.Data.Infrastructure;
using CarDealer.Data.Repositories;
using CarDealer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealer.Service
{
    public interface IFeedbackService
    {
        Feedback Create(Feedback feedback);
        void SaveChanges();

    }
    public class FeedbackService : IFeedbackService
    {
        IFeedbackRepository _feedbackRepository;
        IUnitOfWork _unitOfWork;
        public FeedbackService(IFeedbackRepository feedbackRepository, IUnitOfWork unitOfWork)
        {
            this._feedbackRepository = feedbackRepository;
            this._unitOfWork = unitOfWork;
        }
        public Feedback Create(Feedback feedback)
        {
            return _feedbackRepository.Add(feedback);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}

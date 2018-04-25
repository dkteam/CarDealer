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

        void Update(Feedback feedback);               

        Feedback Delete(int id);

        IEnumerable<Feedback> GetAll();

        IEnumerable<Feedback> GetAll(string keyWord);

        Feedback GetById(int id);

        Feedback ChangeStatus(int id);

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

        public Feedback Delete(int id)
        {
            return _feedbackRepository.Delete(id);
        }

        public IEnumerable<Feedback> GetAll()
        {
            return _feedbackRepository.GetAll().OrderByDescending(x => x.CreatedDate);
        }

        public IEnumerable<Feedback> GetAll(string keyWord)
        {
            if (!string.IsNullOrEmpty(keyWord))
                return _feedbackRepository.GetMulti(x => x.Name.Contains(keyWord));
            else
                return _feedbackRepository.GetAll();
        }

        public Feedback GetById(int id)
        {
            return _feedbackRepository.GetSingleById(id);
        }

        public void Update(Feedback feedback)
        {
            _feedbackRepository.Update(feedback);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public Feedback ChangeStatus(int id)
        {
            var feedback = _feedbackRepository.GetSingleById(id);
            feedback.Status = !feedback.Status;
            _unitOfWork.Commit();

            return feedback;
        }
    }
}

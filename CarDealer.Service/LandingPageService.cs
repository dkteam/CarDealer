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
    public interface ILandingPageService
    {
        LandingPage Add(LandingPage landingPage);

        void Update(LandingPage landingPage);

        LandingPage Delete(int id);

        IEnumerable<LandingPage> GetAll();

        IEnumerable<LandingPage> GetAll(string keyWord);        

        LandingPage GetById(int id);

        void SaveChanges();
    }

    public class LandingPageService : ILandingPageService
    {
        private ILandingPageRepository _landingPageRepository;
        private IUnitOfWork _unitOfWork;

        public LandingPageService(ILandingPageRepository landingPageRepository, IUnitOfWork unitOfWork)
        {
            this._landingPageRepository = landingPageRepository;
            this._unitOfWork = unitOfWork;
        }

        public LandingPage Add(LandingPage landingPage)
        {
            return _landingPageRepository.Add(landingPage);
        }

        public LandingPage Delete(int id)
        {
            return _landingPageRepository.Delete(id);
        }

        public IEnumerable<LandingPage> GetAll()
        {
            return _landingPageRepository.GetAll();
        }

        public IEnumerable<LandingPage> GetAll(string keyWord)
        {
            if (!string.IsNullOrEmpty(keyWord))
                return _landingPageRepository.GetMulti(x => x.Name.Contains(keyWord));
            else
                return _landingPageRepository.GetAll();
        }

        public LandingPage GetById(int id)
        {
            return _landingPageRepository.GetSingleByCondition(x => x.ID == id);
        }        

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(LandingPage landingPage)
        {
            _landingPageRepository.Update(landingPage);
        }
    }
}

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
    public interface ISlideService
    {
        Slide Add(Slide carCategory);

        void Update(Slide carCategory);

        Slide Delete(int id);

        IEnumerable<Slide> GetAll();

        IEnumerable<Slide> GetAll(string keyWord);

        Slide GetById(int id);

        void SaveChanges();
    }

    public class SlideService : ISlideService
    {
        private ISlideRepository _slide;
        private IUnitOfWork _unitOfWork;

        public SlideService(ISlideRepository slide, IUnitOfWork unitOfWork)
        {
            this._slide = slide;
            this._unitOfWork = unitOfWork;
        }

        public Slide Add(Slide style)
        {
            return _slide.Add(style);
        }

        public Slide Delete(int id)
        {
            return _slide.Delete(id);
        }

        public IEnumerable<Slide> GetAll()
        {
            return _slide.GetAll();
        }

        public IEnumerable<Slide> GetAll(string keyWord)
        {
            if (!string.IsNullOrEmpty(keyWord))
                return _slide.GetMulti(x => x.Name.Contains(keyWord) || x.Description.Contains(keyWord));
            else
                return _slide.GetAll();
        }

        public Slide GetById(int id)
        {
            return _slide.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Slide style)
        {
            _slide.Update(style);
        }
    }
}

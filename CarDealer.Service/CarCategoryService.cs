using CarDealer.Data.Infrastructure;
using CarDealer.Data.Repositories;
using CarDealer.Model.Models;
using System.Collections.Generic;
using System;

namespace CarDealer.Service
{
    public interface ICarCategoryService
    {
        CarCategory Add(CarCategory carCategory);

        void Update(CarCategory carCategory);

        CarCategory Delete(int id);

        IEnumerable<CarCategory> GetAll();

        IEnumerable<CarCategory> GetAll(string keyWord);

        IEnumerable<CarCategory> GetAllByParentId(int parentId);

        CarCategory GetById(int id);

        void SaveChange();
    }

    public class CarCategoryService : ICarCategoryService
    {
        private ICarCategoryRepository _carCategoryRepository;
        private IUnitOfWork _unitOfWork;

        public CarCategoryService(ICarCategoryRepository carCategoryRepository, IUnitOfWork unitOfWork)
        {
            this._carCategoryRepository = carCategoryRepository;
            this._unitOfWork = unitOfWork;
        }

        public CarCategory Add(CarCategory carCategory)
        {
            return _carCategoryRepository.Add(carCategory);
        }

        public CarCategory Delete(int id)
        {
            return _carCategoryRepository.Delete(id);
        }

        public IEnumerable<CarCategory> GetAll()
        {
            return _carCategoryRepository.GetAll();
        }

        public IEnumerable<CarCategory> GetAll(string keyWord)
        {
            if (!string.IsNullOrEmpty(keyWord))
                return _carCategoryRepository.GetMulti(x => x.Name.Contains(keyWord) || x.Description.Contains(keyWord));
            else
                return _carCategoryRepository.GetAll();
        }

        public IEnumerable<CarCategory> GetAllByParentId(int parentId)
        {
            return _carCategoryRepository.GetMulti(x => x.Status && x.ParentID == parentId);
        }

        public CarCategory GetById(int id)
        {
            return _carCategoryRepository.GetSingleById(id);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public void Update(CarCategory carCategory)
        {
            _carCategoryRepository.Update(carCategory);
        }
    }
}
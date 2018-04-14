using CarDealer.Data.Infrastructure;
using CarDealer.Data.Repositories;
using CarDealer.Model.Models;
using System.Collections.Generic;

namespace CarDealer.Service
{
    public interface ICarService
    {
        Car Add(Car car);

        void Update(Car car);

        Car Delete(int id);

        IEnumerable<Car> GetAll();

        IEnumerable<Car> GetAll(string keyWord);

        Car GetById(int id);

        void SaveChanges();
    }

    public class CarService : ICarService
    {
        private ICarRepository _carRepository;
        private IUnitOfWork _unitOfWork;

        public CarService(ICarRepository carRepository, IUnitOfWork unitOfWork)
        {
            this._carRepository = carRepository;
            this._unitOfWork = unitOfWork;
        }

        public Car Add(Car car)
        {
            return _carRepository.Add(car);
        }

        public Car Delete(int id)
        {
            return _carRepository.Delete(id);
        }

        public IEnumerable<Car> GetAll()
        {
            return _carRepository.GetAll();
        }

        public IEnumerable<Car> GetAll(string keyWord)
        {
            if (!string.IsNullOrEmpty(keyWord))
                return _carRepository.GetMulti(x => x.Name.Contains(keyWord) || x.Description.Contains(keyWord));
            else
                return _carRepository.GetAll();
        }

        public Car GetById(int id)
        {
            return _carRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Car car)
        {
            _carRepository.Update(car);
        }
    }
}
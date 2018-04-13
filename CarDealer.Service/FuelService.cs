using CarDealer.Data.Infrastructure;
using CarDealer.Data.Repositories;
using CarDealer.Model.Models;
using System.Collections.Generic;

namespace CarDealer.Service
{
    public interface IFuelService
    {
        Fuel Add(Fuel carCategory);

        void Update(Fuel carCategory);

        Fuel Delete(int id);

        IEnumerable<Fuel> GetAll();

        IEnumerable<Fuel> GetAll(string keyWord);

        Fuel GetById(int id);

        void SaveChanges();
    }

    public class FuelService : IFuelService
    {
        private IFuelRepository _fuelService;
        private IUnitOfWork _unitOfWork;

        public FuelService(IFuelRepository fuelService, IUnitOfWork unitOfWork)
        {
            this._fuelService = fuelService;
            this._unitOfWork = unitOfWork;
        }

        public Fuel Add(Fuel fuel)
        {
            return _fuelService.Add(fuel);
        }

        public Fuel Delete(int id)
        {
            return _fuelService.Delete(id);
        }

        public IEnumerable<Fuel> GetAll()
        {
            return _fuelService.GetAll();
        }

        public IEnumerable<Fuel> GetAll(string keyWord)
        {
            if (!string.IsNullOrEmpty(keyWord))
                return _fuelService.GetMulti(x => x.Name.Contains(keyWord) || x.Description.Contains(keyWord));
            else
                return _fuelService.GetAll();
        }

        public Fuel GetById(int id)
        {
            return _fuelService.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Fuel fuel)
        {
            _fuelService.Update(fuel);
        }
    }
}
using CarDealer.Data.Infrastructure;
using CarDealer.Data.Repositories;
using CarDealer.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace CarDealer.Service
{
    public interface ITotalSeatService
    {
        TotalSeat Add(TotalSeat totalSeat);

        void Update(TotalSeat totalSeat);

        TotalSeat Delete(int id);

        IEnumerable<TotalSeat> GetAll();

        IEnumerable<TotalSeat> GetAll(string keyWord);

        TotalSeat GetById(int id);

        void SaveChanges();
    }

    public class TotalSeatService : ITotalSeatService
    {
        private ITotalSeatRepository _totalSeat;
        private IUnitOfWork _unitOfWork;

        public TotalSeatService(ITotalSeatRepository totalSeat, IUnitOfWork unitOfWork)
        {
            this._totalSeat = totalSeat;
            this._unitOfWork = unitOfWork;
        }

        public TotalSeat Add(TotalSeat totalSeat)
        {
            return _totalSeat.Add(totalSeat);
        }

        public TotalSeat Delete(int id)
        {
            return _totalSeat.Delete(id);
        }

        public IEnumerable<TotalSeat> GetAll()
        {
            return _totalSeat.GetAll().OrderBy(x=>x.Name);
        }

        public IEnumerable<TotalSeat> GetAll(string keyWord)
        {
            if (!string.IsNullOrEmpty(keyWord))
                return _totalSeat.GetMulti(x => x.Name.Contains(keyWord));
            else
                return _totalSeat.GetAll();
        }

        public TotalSeat GetById(int id)
        {
            return _totalSeat.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(TotalSeat totalSeat)
        {
            _totalSeat.Update(totalSeat);
        }
    }
}
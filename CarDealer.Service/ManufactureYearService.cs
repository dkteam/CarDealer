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
    public interface IManufactureYearService
    {
        ManufactureYear Add(ManufactureYear carCategory);

        void Update(ManufactureYear carCategory);

        ManufactureYear Delete(int id);

        IEnumerable<ManufactureYear> GetAll();

        IEnumerable<ManufactureYear> GetAll(string keyWord);

        //IEnumerable<ManufactureYear> GetAllByParentId(int parentId);

        ManufactureYear GetById(int id);

        void SaveChanges();
    }
    public class ManufactureYearService : IManufactureYearService
    {
        private IManufactureYearRepository _manufactureYear;
        private IUnitOfWork _unitOfWork;

        public ManufactureYearService(IManufactureYearRepository manufactureYear, IUnitOfWork unitOfWork)
        {
            this._manufactureYear = manufactureYear;
            this._unitOfWork = unitOfWork;
        }

        public ManufactureYear Add(ManufactureYear carCategory)
        {
            return _manufactureYear.Add(carCategory);
        }

        public ManufactureYear Delete(int id)
        {
            return _manufactureYear.Delete(id);
        }

        public IEnumerable<ManufactureYear> GetAll()
        {
            return _manufactureYear.GetAll();
        }

        public IEnumerable<ManufactureYear> GetAll(string keyWord)
        {
            if (!string.IsNullOrEmpty(keyWord))
                return _manufactureYear.GetMulti(x => x.Name.Contains(keyWord) || x.Description.Contains(keyWord));
            else
                return _manufactureYear.GetAll();
        }

        //public IEnumerable<ManufactureYear> GetAllByParentId(int parentId)
        //{
        //    return _manufactureYear.GetMulti(x => x.Status && x.ParentID == parentId);
        //}

        public ManufactureYear GetById(int id)
        {
            return _manufactureYear.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(ManufactureYear carCategory)
        {
            _manufactureYear.Update(carCategory);
        }
    }
}

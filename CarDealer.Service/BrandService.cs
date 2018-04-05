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
    public interface IBrandService
    {
        void Add(Brand brand);
        void Update(Brand brand);
        void Delete(int id);
        IEnumerable<Brand> GetAll();
        IEnumerable<Brand> GetAllPaging(int page, int pageSize, out int totalRow);
        Brand GetById(int id);
        IEnumerable<Brand> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);
        void SaveChange();
    }
    public class BrandService : IBrandService
    {
        IBrandRepository _brandRepository;
        IUnitOfWork _unitOfWork;

        public BrandService(IBrandRepository brandRepository, IUnitOfWork unitOfWork)
        {
            this._brandRepository = brandRepository;
            this._unitOfWork = unitOfWork;
        }
        public void Add(Brand brand)
        {
            _brandRepository.Add(brand);
        }

        public void Delete(int id)
        {
            _brandRepository.Delete(id);
        }

        public IEnumerable<Brand> GetAll()
        {
            return _brandRepository.GetAll();
        }

        public IEnumerable<Brand> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow)
        {
            //TODO: Select all post by tag
            return _brandRepository.GetMultiPaging(x => x.Status, out totalRow, page, pageSize);
        }

        public IEnumerable<Brand> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            return _brandRepository.GetMultiPaging(x => x.Status, out totalRow, page, pageSize);
        }

        public Brand GetById(int id)
        {
            return _brandRepository.GetSingleById(id);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public void Update(Brand brand)
        {
            _brandRepository.Update(brand);
        }
    }
}

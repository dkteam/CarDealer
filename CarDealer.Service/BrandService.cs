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

        void Delte(int id);

        IEnumerable<Brand> GetAll();

        IEnumerable<Brand> GetAllPaging(int page, int pageSize, out int totalRow);

        Post GetById(int id);

        IEnumerable<Brand> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow);

        void SaveChange();
    }
    public class BrandService : IBrandService
    {
        IPostRepository _postRepository;
        public void Add(Brand brand)
        {
            throw new NotImplementedException();
        }

        public void Delte(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Brand> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Brand> GetAllByTagPaging(string tag, int page, int pageSize, out int totalRow)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Brand> GetAllPaging(int page, int pageSize, out int totalRow)
        {
            throw new NotImplementedException();
        }

        public Post GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void SaveChange()
        {
            throw new NotImplementedException();
        }

        public void Update(Brand brand)
        {
            throw new NotImplementedException();
        }
    }
}

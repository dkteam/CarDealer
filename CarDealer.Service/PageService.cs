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
    public interface IPageService
    {
        Page Add(Page page);

        void Update(Page page);

        Page Delete(int id);

        IEnumerable<Page> GetAll();

        IEnumerable<Page> GetAll(string keyWord);

        Page GetById(int id);

        Page GetByAlias(string alias);

        void SaveChanges();
    }

    public class PageService : IPageService
    {
        private IPageRepository _pageRepository;
        private IUnitOfWork _unitOfWork;

        public PageService(IPageRepository pageRepository, IUnitOfWork unitOfWork)
        {
            this._pageRepository = pageRepository;
            this._unitOfWork = unitOfWork;
        }

        public Page Add(Page page)
        {
            return _pageRepository.Add(page);
        }

        public Page Delete(int id)
        {
            return _pageRepository.Delete(id);
        }

        public IEnumerable<Page> GetAll()
        {
            return _pageRepository.GetAll();
        }

        public IEnumerable<Page> GetAll(string keyWord)
        {
            if (!string.IsNullOrEmpty(keyWord))
                return _pageRepository.GetMulti(x => x.Name.Contains(keyWord));
            else
                return _pageRepository.GetAll();
        }

        public Page GetByAlias(string alias)
        {
            return _pageRepository.GetSingleByCondition(x => x.Alias == alias);
        }

        public Page GetById(int id)
        {
            return _pageRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Page page)
        {
            _pageRepository.Update(page);
        }
    }
}

using CarDealer.Common;
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
    public interface IFooterService
    {
        Footer Add(Footer footer);

        void Update(Footer footer);

        Footer Delete(int id);

        IEnumerable<Footer> GetAll();

        IEnumerable<Footer> GetAll(string keyWord);

        Footer GetFooter();

        Footer GetPreFooter();

        Footer GetById(string id);

        void SaveChanges();
    }

    public class FooterService : IFooterService
    {
        private IFooterRepository _footerRepository;
        private IUnitOfWork _unitOfWork;

        public FooterService(IFooterRepository FooterRepository, IUnitOfWork unitOfWork)
        {
            this._footerRepository = FooterRepository;
            this._unitOfWork = unitOfWork;
        }

        public Footer Add(Footer footer)
        {
            return _footerRepository.Add(footer);
        }

        public Footer Delete(int id)
        {
            return _footerRepository.Delete(id);
        }

        public IEnumerable<Footer> GetAll()
        {
            return _footerRepository.GetAll();
        }

        public IEnumerable<Footer> GetAll(string keyWord)
        {
            if (!string.IsNullOrEmpty(keyWord))
                return _footerRepository.GetMulti(x => x.ID.Contains(keyWord));
            else
                return _footerRepository.GetAll();
        }

        public Footer GetById(string id)
        {
            return _footerRepository.GetSingleByCondition(x => x.ID == id);
        }

        public Footer GetFooter()
        {
            return _footerRepository.GetSingleByCondition(x => x.ID == CommonConstants.Footer);
        }

        public Footer GetPreFooter()
        {
            return _footerRepository.GetSingleByCondition(x => x.ID == CommonConstants.PreFooter);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Footer footer)
        {
            _footerRepository.Update(footer);
        }
    }
}

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
    public interface IUserService
    {
        ApplicationUser Add(ApplicationUser user);

        void Update(ApplicationUser user);

        ApplicationUser Delete(int id);

        IEnumerable<ApplicationUser> GetAll();

        IEnumerable<ApplicationUser> GetAll(string keyWord);

        ApplicationUser GetById(int id);

        void SaveChanges();
    }

    public class UserService : IUserService
    {
        private IUserRepository _UserRepository;
        private IUnitOfWork _unitOfWork;

        public UserService(IUserRepository UserRepository, IUnitOfWork unitOfWork)
        {
            this._UserRepository = UserRepository;
            this._unitOfWork = unitOfWork;
        }

        public ApplicationUser Add(ApplicationUser user)
        {
            return _UserRepository.Add(user);
        }

        public ApplicationUser Delete(int id)
        {
            return _UserRepository.Delete(id);
        }

        public IEnumerable<ApplicationUser> GetAll()
        {
            return _UserRepository.GetAll();
        }

        public IEnumerable<ApplicationUser> GetAll(string keyWord)
        {
            if (!string.IsNullOrEmpty(keyWord))
                return _UserRepository.GetMulti(x => x.FullName.Contains(keyWord));
            else
                return _UserRepository.GetAll();
        }

        public ApplicationUser GetById(int id)
        {
            return _UserRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(ApplicationUser user)
        {
            _UserRepository.Update(user);
        }
    }
}

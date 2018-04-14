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
    public interface IMenuService
    {
        Menu Add(Menu menu);

        void Update(Menu menu);

        Menu Delete(int id);

        IEnumerable<Menu> GetAll();

        IEnumerable<Menu> GetAll(string keyWord);

        Menu GetById(int id);

        void SaveChanges();
    }

    public class MenuService : IMenuService
    {
        private IMenuRepositoy _menu;
        private IUnitOfWork _unitOfWork;

        public MenuService(IMenuRepositoy menu, IUnitOfWork unitOfWork)
        {
            this._menu = menu;
            this._unitOfWork = unitOfWork;
        }

        public Menu Add(Menu menu)
        {
            return _menu.Add(menu);
        }

        public Menu Delete(int id)
        {
            return _menu.Delete(id);
        }

        public IEnumerable<Menu> GetAll()
        {
            return _menu.GetAll();
        }

        public IEnumerable<Menu> GetAll(string keyWord)
        {
            if (!string.IsNullOrEmpty(keyWord))
                return _menu.GetMulti(x => x.Name.Contains(keyWord));
            else
                return _menu.GetAll();
        }

        public Menu GetById(int id)
        {
            return _menu.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Menu menu)
        {
            _menu.Update(menu);
        }
    }
}

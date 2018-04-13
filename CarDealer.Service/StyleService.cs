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
    public interface IStyleService
    {
        Style Add(Style carCategory);

        void Update(Style carCategory);

        Style Delete(int id);

        IEnumerable<Style> GetAll();

        IEnumerable<Style> GetAll(string keyWord);

        Style GetById(int id);

        void SaveChanges();
    }

    public class StyleService : IStyleService
    {
        private IStyleRepository _style;
        private IUnitOfWork _unitOfWork;

        public StyleService(IStyleRepository style, IUnitOfWork unitOfWork)
        {
            this._style = style;
            this._unitOfWork = unitOfWork;
        }

        public Style Add(Style style)
        {
            return _style.Add(style);
        }

        public Style Delete(int id)
        {
            return _style.Delete(id);
        }

        public IEnumerable<Style> GetAll()
        {
            return _style.GetAll();
        }

        public IEnumerable<Style> GetAll(string keyWord)
        {
            if (!string.IsNullOrEmpty(keyWord))
                return _style.GetMulti(x => x.Name.Contains(keyWord) || x.Description.Contains(keyWord));
            else
                return _style.GetAll();
        }

        public Style GetById(int id)
        {
            return _style.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Style style)
        {
            _style.Update(style);
        }
    }
}

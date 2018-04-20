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
    public interface ISupportOnlineService
    {
        SupportOnline Add(SupportOnline supportOnline);

        void Update(SupportOnline supportOnline);

        SupportOnline Delete(int id);

        IEnumerable<SupportOnline> GetAll();        

        SupportOnline GetById(int id);

        void SaveChanges();
    }

    public class SupportOnlineService : ISupportOnlineService
    {
        private ISupportOnlineRepository _supportOnline;
        private IUnitOfWork _unitOfWork;

        public SupportOnlineService(ISupportOnlineRepository supportOnline, IUnitOfWork unitOfWork)
        {
            this._supportOnline = supportOnline;
            this._unitOfWork = unitOfWork;
        }

        public SupportOnline Add(SupportOnline supportOnline)
        {
            return _supportOnline.Add(supportOnline);
        }

        public SupportOnline Delete(int id)
        {
            return _supportOnline.Delete(id);
        }

        public IEnumerable<SupportOnline> GetAll()
        {
            return _supportOnline.GetAll();
        }

        public SupportOnline GetById(int id)
        {
            return _supportOnline.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(SupportOnline supportOnline)
        {
            _supportOnline.Update(supportOnline);
        }
    }
}
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
    public interface ITransmissionTypeService
    {
        TransmissionType Add(TransmissionType carCategory);

        void Update(TransmissionType carCategory);

        TransmissionType Delete(int id);

        IEnumerable<TransmissionType> GetAll();

        IEnumerable<TransmissionType> GetAll(string keyWord);

        TransmissionType GetById(int id);

        void SaveChanges();
    }

    public class TransmissionTypeService : ITransmissionTypeService
    {
        private ITransmissionTypeRepository _transmissionTypeService;
        private IUnitOfWork _unitOfWork;

        public TransmissionTypeService(ITransmissionTypeRepository transmissionTypeService, IUnitOfWork unitOfWork)
        {
            this._transmissionTypeService = transmissionTypeService;
            this._unitOfWork = unitOfWork;
        }

        public TransmissionType Add(TransmissionType carCategory)
        {
            return _transmissionTypeService.Add(carCategory);
        }

        public TransmissionType Delete(int id)
        {
            return _transmissionTypeService.Delete(id);
        }

        public IEnumerable<TransmissionType> GetAll()
        {
            return _transmissionTypeService.GetAll();
        }

        public IEnumerable<TransmissionType> GetAll(string keyWord)
        {
            if (!string.IsNullOrEmpty(keyWord))
                return _transmissionTypeService.GetMulti(x => x.Name.Contains(keyWord) || x.Description.Contains(keyWord));
            else
                return _transmissionTypeService.GetAll();
        }

        public TransmissionType GetById(int id)
        {
            return _transmissionTypeService.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(TransmissionType carCategory)
        {
            _transmissionTypeService.Update(carCategory);
        }
    }
}

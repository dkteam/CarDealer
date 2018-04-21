using CarDealer.Common;
using CarDealer.Data.Infrastructure;
using CarDealer.Data.Repositories;
using CarDealer.Model.Models;
using System.Collections.Generic;
using System;
using System.Linq;

namespace CarDealer.Service
{
    public interface ICarService
    {
        Car Add(Car car);

        void Update(Car car);

        Car Delete(int id);

        IEnumerable<Car> GetAll();

        IEnumerable<Car> GetAll(string keyWord);

        IEnumerable<Car> GetBestSeller(int top);

        IEnumerable<Car> GetHot(int top);

        IEnumerable<Car> GetLatestCar(int top);

        IEnumerable<Car> GetBestPrice(int top);

        IEnumerable<Car> GetReatedCars(int id, int top);

        IEnumerable<Car> GetProductsByCategoryIdPaging(int categoryId, int page, int pageSize, string sort, out int totalRow);

        Car GetById(int id);

        void SaveChanges();
    }

    public class CarService : ICarService
    {
        private ICarRepository _carRepository;
        private ITagRepository _tagRepository;
        private ICarTagRepository _carTagRepository;
        private IUnitOfWork _unitOfWork;

        public CarService(  ICarRepository carRepository, 
                            ICarTagRepository carTagRepository, 
                            ITagRepository tagRepository, 
                            IUnitOfWork unitOfWork)
        {
            this._carRepository = carRepository;
            this._tagRepository = tagRepository;
            this._carTagRepository = carTagRepository;
            this._unitOfWork = unitOfWork;
        }

        public Car Add(Car car)
        {

            var newCar = _carRepository.Add(car);
            _unitOfWork.Commit();

            if (!string.IsNullOrEmpty(car.Tags))
            {
                string[] tags = car.Tags.Split(',');
                for (var i = 0; i < tags.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(tags[i]);
                    if (_tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        Tag tag = new Tag();
                        tag.ID = tagId;
                        tag.Name = tags[i];
                        tag.Type = CommonConstants.CarTag;

                        _tagRepository.Add(tag);
                    }
                    PostTag postTag = new PostTag();
                    postTag.PostID = car.ID;
                    postTag.TagID = tagId;

                    CarTag carTag = new CarTag();
                    carTag.CarID = car.ID;
                    carTag.TagID = tagId;

                    _carTagRepository.Add(carTag);
                }
            }
            return newCar;
        }

        public Car Delete(int id)
        {
            return _carRepository.Delete(id);
        }

        public IEnumerable<Car> GetAll()
        {
            return _carRepository.GetAll();
        }

        public IEnumerable<Car> GetAll(string keyWord)
        {
            if (!string.IsNullOrEmpty(keyWord))
                return _carRepository.GetMulti(x => x.Name.Contains(keyWord) || x.Description.Contains(keyWord) || x.CarCategory.Name.Contains(keyWord));
            else
                return _carRepository.GetAll();
        }

        public Car GetById(int id)
        {
            return _carRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Car car)
        {
            _carRepository.Update(car);
            
            if (!string.IsNullOrEmpty(car.Tags))
            {
                string[] tags = car.Tags.Split(',');
                for (var i = 0; i < tags.Length; i++)
                {
                    var tagId = StringHelper.ToUnsignString(tags[i]);
                    if (_tagRepository.Count(x => x.ID == tagId) == 0)
                    {
                        Tag tag = new Tag();
                        tag.ID = tagId;
                        tag.Name = tags[i];
                        tag.Type = CommonConstants.CarTag;

                        _tagRepository.Add(tag);
                    }
                    _carTagRepository.DeleteMulti(x => x.CarID == car.ID);
                    CarTag carTag = new CarTag();
                    carTag.CarID = car.ID;
                    carTag.TagID = tagId;

                    _carTagRepository.Add(carTag);
                }

            }
        }

        public IEnumerable<Car> GetBestSeller(int top)
        {
            return _carRepository.GetMulti(x => x.Status && x.Bestseller == true).OrderByDescending(x => x.UpdatedDate).Take(top);
        }

        public IEnumerable<Car> GetHot(int top)
        {
            return _carRepository.GetMulti(x => x.Status && x.HotFlag == true).OrderByDescending(x => x.UpdatedDate).Take(top);
        }

        public IEnumerable<Car> GetBestPrice(int top)
        {
            return _carRepository.GetMulti(x => x.Status && x.BestPrice == true).OrderByDescending(x => x.UpdatedDate).Take(top);
        }

        public IEnumerable<Car> GetLatestCar(int top)
        {
            return _carRepository.GetMulti(x => x.Status).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Car> GetProductsByCategoryIdPaging(int categoryId, int page, int pageSize, string sort, out int totalRow)
        {
            var query = _carRepository.GetMulti(x => x.Status && x.CategoryID == categoryId);

            switch (sort)
            {
                case "new":
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;
                case "pricedescending":
                    query = query.OrderByDescending(x => x.Price);
                    break;
                case "price":
                    query = query.OrderBy(x => x.Price);
                    break;
                case "bestseller":
                    query = query.OrderBy(x => x.Bestseller);
                    break;
                case "hot":
                    query = query.OrderBy(x => x.HotFlag);
                    break;
                default:
                    query = query.OrderByDescending(x => x.CreatedDate);
                    break;

            }

            totalRow = query.Count();

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public IEnumerable<Car> GetReatedCars(int id, int top)
        {
            var product = _carRepository.GetSingleById(id);
            return _carRepository.GetMulti(x => x.Status && x.ID != id && x.CategoryID == product.CategoryID).OrderByDescending(x => x.CreatedDate).Take(top);
        }
    }
}
using CarDealer.Data.Infrastructure;
using CarDealer.Model.Models;
using System.Collections;
using System;
using System.Linq;
using System.Collections.Generic;

namespace CarDealer.Data.Repositories
{
    public interface ICarRepository : IRepository<Car>
    {
        IEnumerable<Car> GetListCarByTag(string tagId, int page, int pageSize, out int totalRow);
    }

    public class CarRepository : RepositoryBase<Car>, ICarRepository
    {
        public CarRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        public IEnumerable<Car> GetListCarByTag(string tagId, int page, int pageSize, out int totalRow)
        {
            var query = from c in DbContext.Cars
                        join ct in DbContext.CarTags
                        on c.ID equals ct.CarID
                        where ct.TagID == tagId && c.Status
                        select c;

            totalRow = query.Count();

            return query.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
using CarDealer.Data.Infrastructure;
using CarDealer.Model.Models;

namespace CarDealer.Data.Repositories
{
    public interface ICarRepository : IRepository<Car>
    {
    }

    public class CarRepository : RepositoryBase<Car>, ICarRepository
    {
        public CarRepository(IDbFactory dbFactory) 
            : base(dbFactory)
        {
        }
    }
}
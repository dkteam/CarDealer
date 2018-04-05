using CarDealer.Data.Infrastructure;
using CarDealer.Model.Models;

namespace CarDealer.Data.Repositories
{
    public interface ICarRepository : IRepository<Car>
    {
    }

    public class CarRepo : RepositoryBase<Car>, ICarRepository
    {
        public CarRepo(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
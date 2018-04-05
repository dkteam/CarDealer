using CarDealer.Data.Infrastructure;
using CarDealer.Model.Models;

namespace CarDealer.Data.Repositories
{
    public interface ICarTagRepository
    {
    }

    public class CarTagRepository : RepositoryBase<CarTag>, ICarTagRepository
    {
        public CarTagRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
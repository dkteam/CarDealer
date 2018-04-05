using CarDealer.Data.Infrastructure;
using CarDealer.Model.Models;

namespace CarDealer.Data.Repositories
{
    public interface IFuelRepository
    {
    }

    public class FuelRepository : RepositoryBase<Fuel>, IFuelRepository
    {
        public FuelRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
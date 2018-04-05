using CarDealer.Data.Infrastructure;
using CarDealer.Model.Models;

namespace CarDealer.Data.Repositories
{
    public interface IManufactureYearRepository
    {
    }

    public class ManufactureYearRepository : RepositoryBase<ManufactureYear>, IManufactureYearRepository
    {
        public ManufactureYearRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
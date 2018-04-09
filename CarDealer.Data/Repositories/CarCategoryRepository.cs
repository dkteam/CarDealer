using CarDealer.Data.Infrastructure;
using CarDealer.Model.Models;

namespace CarDealer.Data.Repositories
{
    public interface ICarCategoryRepository : IRepository<CarCategory>
    {
    }

    public class CarCategoryRepository : RepositoryBase<CarCategory>, ICarCategoryRepository
    {
        public CarCategoryRepository(IDbFactory dbFactory) 
            : base(dbFactory)
        {
        }
    }
}
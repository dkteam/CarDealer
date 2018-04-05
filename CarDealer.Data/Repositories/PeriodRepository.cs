using CarDealer.Data.Infrastructure;
using CarDealer.Model.Models;

namespace CarDealer.Data.Repositories
{
    public interface IPeriodRepository : IRepository<Period>
    {
    }

    public class PeriodRepository : RepositoryBase<Period>, IPeriodRepository
    {
        public PeriodRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
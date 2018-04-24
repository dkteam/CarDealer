using CarDealer.Data.Infrastructure;
using CarDealer.Model.Models;

namespace CarDealer.Data.Repositories
{
    public interface ITotalSeatRepository : IRepository<TotalSeat>
    {
    }

    public class TotalSeatRepository : RepositoryBase<TotalSeat>, ITotalSeatRepository
    {
        public TotalSeatRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
    }
}
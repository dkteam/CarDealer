using CarDealer.Data.Infrastructure;
using CarDealer.Model.Models;

namespace CarDealer.Data.Repositories
{
    public interface IStyleRepository : IRepository<Style>
    {
    }

    public class StyleRepository : RepositoryBase<Style>, IStyleRepository
    {
        public StyleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
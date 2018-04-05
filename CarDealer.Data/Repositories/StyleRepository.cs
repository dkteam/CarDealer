using CarDealer.Data.Infrastructure;
using CarDealer.Model.Models;

namespace CarDealer.Data.Repositories
{
    public interface IStyle
    {
    }

    public class StyleRepository : RepositoryBase<Style>, IStyle
    {
        public StyleRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
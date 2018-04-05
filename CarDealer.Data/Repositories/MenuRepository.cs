using CarDealer.Data.Infrastructure;
using CarDealer.Model.Models;

namespace CarDealer.Data.Repositories
{
    public interface IMenuRepositoy
    {
    }

    public class MenuRepository : RepositoryBase<Menu>, IMenuRepositoy
    {
        public MenuRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
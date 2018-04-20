using CarDealer.Data.Infrastructure;
using CarDealer.Model.Models;

namespace CarDealer.Data.Repositories
{
    public interface ILandingPageRepository : IRepository<LandingPage>
    {
    }

    public class LandingPageRepository : RepositoryBase<LandingPage>, ILandingPageRepository
    {
        public LandingPageRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
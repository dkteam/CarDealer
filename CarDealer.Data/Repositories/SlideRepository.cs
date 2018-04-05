using CarDealer.Data.Infrastructure;
using CarDealer.Model.Models;

namespace CarDealer.Data.Repositories
{
    public interface ISlide : IRepository<Slide>
    {
    }

    public class SlideRepository : RepositoryBase<Slide>, ISlide
    {
        public SlideRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
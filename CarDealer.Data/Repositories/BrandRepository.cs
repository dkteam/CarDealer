using CarDealer.Data.Infrastructure;
using CarDealer.Model.Models;
using System.Collections.Generic;
using System.Linq;

namespace CarDealer.Data.Repositories
{
    public interface IBrandRepo
    {
        IEnumerable<Brand> GetByAlias(string alias);
    }

    public class BrandRepo : RepositoryBase<Brand>, IBrandRepo
    {
        public BrandRepo(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        public IEnumerable<Brand> GetByAlias(string alias)
        {
            return this.DbContext.Brands.Where(x => x.Alias == alias);
        }
    }
}
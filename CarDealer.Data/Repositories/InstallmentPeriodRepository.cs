using CarDealer.Data.Infrastructure;
using CarDealer.Model.Models;

namespace CarDealer.Data.Repositories
{
    public interface IInstallmentPeriodRepository
    {
    }

    public class InstallmentPeriodRepository : RepositoryBase<InstallmentPeriod>, IInstallmentPeriodRepository
    {
        public InstallmentPeriodRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
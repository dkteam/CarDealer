using CarDealer.Data.Infrastructure;
using CarDealer.Model.Models;

namespace CarDealer.Data.Repositories
{
    public interface IInstallmentPaymentMethodRepository
    {
    }

    public class InstallmentPaymentMethodRepository : RepositoryBase<InstallmentPaymentMethod>, IInstallmentPaymentMethodRepository
    {
        public InstallmentPaymentMethodRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
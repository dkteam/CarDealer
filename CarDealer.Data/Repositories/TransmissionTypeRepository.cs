using CarDealer.Data.Infrastructure;
using CarDealer.Model.Models;

namespace CarDealer.Data.Repositories
{
    public interface ITransmissionTypeRepository : IRepository<TransmissionType>
    {
    }

    public class TransmissionTypeRepository : RepositoryBase<TransmissionType>, ITransmissionTypeRepository
    {
        public TransmissionTypeRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
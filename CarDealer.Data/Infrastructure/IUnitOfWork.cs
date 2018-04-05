namespace CarDealer.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
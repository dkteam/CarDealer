namespace CarDealer.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private CarDealerDbContext dbContext;

        public CarDealerDbContext Init()
        {
            return dbContext ?? (dbContext = new CarDealerDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
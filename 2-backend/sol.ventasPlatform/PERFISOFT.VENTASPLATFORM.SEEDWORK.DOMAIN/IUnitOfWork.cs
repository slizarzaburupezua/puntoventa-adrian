namespace PERFISOFT.VENTASPLATFORM.SEEDWORK.DOMAIN
{
    public interface IUnitOfWork
    {
        void Save();
        Task SaveAsync();

        void BeginTransaction();
        Task BeginTransactionAsync();

        void CommitTransaction();
        Task CommitTransactionAsync();

        void RollbackTransaction();
        Task RollbackTransactionAsync();

        void DetectChanges();
    }
}

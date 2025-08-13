using PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.REPOSITORY.Context;
using PERFISOFT.VENTASPLATFORM.SEEDWORK.DOMAIN;

namespace PERFISOFT.VENTASPLATFORM.INFRASTRUCTURE.REPOSITORY.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private VentasPlatformContext _context;
        public UnitOfWork(VentasPlatformContext context)
        {
            _context = context;
        }

        #region Save Changes

        public void Save()
        {
            _context.SaveChanges();
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        #endregion

        #region Transactions
        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }

        public async Task BeginTransactionAsync()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public void CommitTransaction()
        {
            _context.Database.CommitTransaction();
        }

        public async Task CommitTransactionAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public void RollbackTransaction()
        {
            _context.Database.RollbackTransaction();
        }

        public async Task RollbackTransactionAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }

        #endregion

        #region Detect Changes
        public void DetectChanges()
        {
            _context.ChangeTracker.DetectChanges();
        }
        #endregion

        #region DISPOSED

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion  

    }
}

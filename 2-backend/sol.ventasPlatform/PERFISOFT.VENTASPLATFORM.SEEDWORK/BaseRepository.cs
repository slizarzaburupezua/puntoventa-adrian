using Microsoft.EntityFrameworkCore;
using PERFISOFT.VENTASPLATFORM.SEEDWORK.DOMAIN;

namespace PERFISOFT.VENTASPLATFORM.SEEDWORK
{
    public class BaseRepository
    {
        private readonly DbContext _context;

        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        public IQueryable<T> All<T>() where T : Entity
        {
            return _context.Set<T>().AsQueryable();
        }

        public async Task<IQueryable<T>> AllAsync<T>() where T : Entity
        {
            return await Task.Factory.StartNew(() => _context.Set<T>().AsQueryable());
        }

        public List<T> Find<T>(Filter<T> filter) where T : Entity
        {
            var query = _context.Set<T>().AsEnumerable();
            var pager = filter?.Pagination;
            filter.Conditions?.ForEach(f => query = query.Where(f));
            if (pager != null)
                query = query.Skip((pager.Page - 1) * pager.Quantity).Take(pager.Quantity);
            return query.ToList();
        }

        public async Task<List<T>> FindAsync<T>(Filter<T> filter) where T : Entity
        {
            return await Task.Factory.StartNew(() => Find(filter));
        }
        public T FindById<T>(int id) where T : Entity
        {
            return _context.Set<T>().Find(id);
        }

        public async Task<T> FindByIdAsync<T>(int id) where T : Entity
        {
            return await Task.Factory.StartNew(() => FindById<T>(id));
        }

        public void Delete<T>(IEnumerable<T> list) where T : Entity
        {
            list.ToList().ForEach(f => Delete<T>(f));
        }

        public void Delete<T>(T entity) where T : Entity
        {
            if (entity == null) throw new ArgumentNullException("entity must be not null");
            _context.Entry((object)entity).State = EntityState.Deleted;
        }

        public async Task DeleteAsync<T>(IEnumerable<T> list) where T : Entity
        {
            await Task.Factory.StartNew(() => Delete(list));
        }

        public async Task DeleteAsync<T>(T entity) where T : Entity
        {
            await Task.Factory.StartNew(() => Delete(entity));
        }

        public void Insert<T>(IEnumerable<T> list) where T : Entity
        {
            list.ToList().ForEach(f => Insert(f));
        }

        public void Insert<T>(T entity) where T : Entity
        {
            _context.Set<T>().Add(entity);
            _context.Entry(entity).State = EntityState.Added;
        }

        public async Task InsertAsync<T>(IEnumerable<T> list) where T : Entity
        {
            await Task.Factory.StartNew(() => Insert(list));
        }

        public async Task InsertAsync<T>(T entity) where T : Entity
        {
            await Task.Factory.StartNew(() => Insert(entity));
        }

        public void Update<T>(T entity) where T : Entity
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Update<T>(IEnumerable<T> list) where T : Entity
        {
            list.ToList().ForEach(f => Update(f));
        }

        public async Task UpdateAsync<T>(T entity) where T : Entity
        {
            await Task.Factory.StartNew(() => Update(entity));
        }

        public async Task UpdateAsync<T>(IEnumerable<T> list) where T : Entity
        {
            await Task.Factory.StartNew(() => Update(list));
        }

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

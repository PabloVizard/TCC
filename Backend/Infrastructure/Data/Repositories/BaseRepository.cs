using Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T>, IDisposable where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;
        public BaseRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public bool Any(Func<T, bool> lambda)
        {
            return _context.Set<T>().Any(lambda);
        }
        public T Find(int id)
        {
            return _context.Set<T>().Find(id);
        }
        public Task<T> FindByAtribute(Expression<Func<T, bool>> lambda)
        {
            return _context.Set<T>().FirstOrDefaultAsync(lambda);
        }
        public List<T> ToList()
        {
            return _context.Set<T>().ToList();
        }

        public Task<List<T>> ToListAsync()
        {
            return _context.Set<T>().ToListAsync();
        }

        public Task<List<T>> ToListAsNoTracking()
        {
            return _context.Set<T>().AsNoTracking().ToListAsync();
        }
        public IQueryable<T> AsNoTracking()
        {
            return _context.Set<T>().AsNoTracking();
        }
        public void Add(T item)
        {
            _context.Set<T>().Add(item);
        }
        public void Update(T item)
        {
            _context.Set<T>().Update(item);
        }
        public void Entry(T item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
        public void Remove(int id)
        {
            _context.Set<T>().Remove(_context.Set<T>().Find(id));
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }

        #region Disposed https://docs.microsoft.com/pt-br/dotnet/standard/garbage-collection/implementing-dispose
        // Flag: Has Dispose already been called?
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);


        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //
            }

            disposed = true;
        }
        #endregion
    }
}
